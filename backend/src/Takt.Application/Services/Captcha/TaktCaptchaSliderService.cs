// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Captcha
// 文件名称：TaktCaptchaSliderService.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt滑块验证码服务实现，使用SixLabors.ImageSharp生成滑块验证码
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using Takt.Shared.Helpers;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Color = SixLabors.ImageSharp.Color;

namespace Takt.Application.Services.Captcha;

/// <summary>
/// 滑块验证码数据（存储在内存中）
/// </summary>
internal class SliderCaptchaData
{
    /// <summary>
    /// 目标位置X（像素）
    /// </summary>
    public int TargetX { get; set; }

    /// <summary>
    /// 目标位置Y（像素）
    /// </summary>
    public int TargetY { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Takt滑块验证码服务实现
/// </summary>
public class TaktCaptchaSliderService : TaktServiceBase, ITaktCaptchaService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment? _webHostEnvironment;
    private readonly TaktCaptchaSliderOptions _options;
    private readonly int _expirationMinutes;
    private readonly ConcurrentDictionary<string, SliderCaptchaData> _captchaStore = new();
    private readonly Random _random = new();
    private readonly HttpClient _httpClient = new();
    private readonly List<int> _availableTemplateGroups = new(); // 可用的模板组列表

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaSliderService(
        IConfiguration configuration,
        IOptions<Takt.Shared.Models.TaktCaptchaOptions> captchaOptions,
        IWebHostEnvironment? webHostEnvironment = null)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;

        var captchaSection = _configuration.GetSection("Captcha:Slider");
        _options = new TaktCaptchaSliderOptions();
        captchaSection.Bind(_options);

        // 从配置选项读取统一的过期时间
        _expirationMinutes = captchaOptions.Value.ExpirationMinutes;

        // 获取 wwwroot 路径：优先使用 IWebHostEnvironment，否则使用 AppContext.BaseDirectory
        string wwwrootPath;
        if (_webHostEnvironment != null)
        {
            wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            TaktLogger.Debug("[Captcha Slider] 使用 IWebHostEnvironment 获取路径: ContentRootPath={ContentRootPath}, WebRootPath={WebRootPath}", 
                _webHostEnvironment.ContentRootPath, _webHostEnvironment.WebRootPath);
        }
        else
        {
            var baseDir = AppContext.BaseDirectory;
            wwwrootPath = Path.Combine(baseDir, "wwwroot");
            TaktLogger.Debug("[Captcha Slider] 使用 AppContext.BaseDirectory 获取路径: BaseDirectory={BaseDir}", baseDir);
        }
        
        // TemplatePath 配置是相对于 wwwroot 的路径，如 "slide/template"
        var templateBasePath = Path.Combine(wwwrootPath, _options.BackgroundImages.Template.TemplatePath);
        // StoragePath 配置是相对于 wwwroot 的路径，如 "slide/background"
        var storagePath = Path.Combine(wwwrootPath, _options.BackgroundImages.StoragePath);
        
        TaktLogger.Information("[Captcha Slider] 滑块验证码服务初始化: WwwRootPath={WwwRootPath}, TemplatePath={TemplatePath}, StoragePath={StoragePath}, GroupCount={GroupCount}",
            wwwrootPath, templateBasePath, storagePath, _options.BackgroundImages.Template.GroupCount);
        
        // 确保目录存在
        if (!Directory.Exists(templateBasePath))
        {
            TaktLogger.Warning("[Captcha Slider] 模板目录不存在，尝试创建: {TemplatePath}", templateBasePath);
            try
            {
                Directory.CreateDirectory(templateBasePath);
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Slider] 创建模板目录失败: {TemplatePath}", templateBasePath);
            }
        }
        
        if (!Directory.Exists(storagePath))
        {
            TaktLogger.Warning("[Captcha Slider] 存储目录不存在，尝试创建: {StoragePath}", storagePath);
            try
            {
                Directory.CreateDirectory(storagePath);
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Slider] 创建存储目录失败: {StoragePath}", storagePath);
            }
        }

        // 验证模板路径是否存在，并记录可用的模板组
        TaktLogger.Information("[Captcha Slider] 开始检查模板组: TemplateBasePath={TemplateBasePath}, GroupCount={GroupCount}", 
            templateBasePath, _options.BackgroundImages.Template.GroupCount);
        
        for (int i = 1; i <= _options.BackgroundImages.Template.GroupCount; i++)
        {
            var templateGroupPath = Path.Combine(templateBasePath, i.ToString());
            var holePath = Path.Combine(templateGroupPath, "hole.png");
            var sliderPath = Path.Combine(templateGroupPath, "slider.png");
            
            var holeExists = File.Exists(holePath);
            var sliderExists = File.Exists(sliderPath);
            var groupDirExists = Directory.Exists(templateGroupPath);
            
            if (holeExists && sliderExists)
            {
                _availableTemplateGroups.Add(i);
                TaktLogger.Information("[Captcha Slider] 模板组 {Group} 存在: Hole={HolePath}, Slider={SliderPath}", i, holePath, sliderPath);
            }
            else
            {
                TaktLogger.Warning("[Captcha Slider] 模板组 {Group} 缺失: GroupDir={GroupDir} (存在={GroupDirExists}), Hole={HolePath} (存在={HoleExists}), Slider={SliderPath} (存在={SliderExists})",
                    i, templateGroupPath, groupDirExists, holePath, holeExists, sliderPath, sliderExists);
            }
        }
        
        if (_availableTemplateGroups.Count == 0)
        {
            TaktLogger.Error("[Captcha Slider] 没有可用的模板组！请检查模板文件是否存在。");
        }
        else
        {
            TaktLogger.Information("[Captcha Slider] 可用模板组数量: {Count}, 组列表: {Groups}", 
                _availableTemplateGroups.Count, string.Join(", ", _availableTemplateGroups));
        }

        // 注意：背景图片初始化已移至 TaktCaptchaInitializer (IHostedService)
        // 在应用启动时统一初始化，避免在构造函数中异步操作
        TaktLogger.Information("[Captcha Slider] 验证码服务构造完成，背景图片将在启动时由 TaktCaptchaInitializer 初始化");
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    public async Task<CaptchaGenerateResult> GenerateAsync()
    {
        var startTime = DateTime.UtcNow;
        try
        {
            TaktLogger.Debug("[Captcha Slider] 开始生成验证码");
            
            // 生成验证码ID
            var captchaId = GenerateCaptchaId();
            var step1Time = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Debug("[Captcha Slider] 步骤1-生成ID完成: {Time}ms", step1Time);

            // 计算滑块尺寸（如果未配置高度则使用宽度，形成正方形）
            var sliderSize = _options.SliderHeight ?? _options.SliderWidth;

            // 为了避免验证码出现在背景图片的最边缘（0% 或接近 100%），
            // 这里在左右各预留一个 sliderSize 像素的安全边距。
            // 这样既不会影响前后端坐标体系的一致性，又能避免极端位置带来的视觉和交互问题。
            var margin = sliderSize;

            // 计算有效的 X 坐标范围：
            // 左边界：至少为 margin
            // 右边界：最多为 Width - sliderSize - margin
            var minX = margin;
            var maxX = _options.Width - sliderSize - margin;

            // 安全兜底：如果配置过小导致范围非法，则退回到原始逻辑（允许从最左到最右）
            if (maxX <= minX)
            {
                minX = 0;
                maxX = _options.Width - sliderSize;
            }

            // 生成随机的 hole 位置（左上角 X 坐标）
            var targetX = _random.Next(minX, maxX + 1);
            var targetPosition = (int)((double)targetX / _options.Width * 100);
            var step2Time = (DateTime.UtcNow - startTime).TotalMilliseconds;
            // 计算Y坐标（固定在中间位置，参照参考代码）
            var targetY = (_options.Height - sliderSize) / 2;
            
            TaktLogger.Debug("[Captcha Slider] 步骤2-计算目标位置完成: TargetX={TargetX}px, TargetY={TargetY}px, TargetPosition={TargetPos}%, SliderSize={SliderSize}, 位置范围: {MinX}-{MaxX}, 总耗时={Time}ms",
                targetX, targetY, targetPosition, sliderSize, minX, maxX, step2Time);

            // 存储验证码数据
            _captchaStore.TryAdd(captchaId, new SliderCaptchaData
            {
                TargetX = targetX,
                TargetY = targetY,
                CreatedAt = DateTime.UtcNow
            });
            var step3Time = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Debug("[Captcha Slider] 步骤3-存储验证码数据完成: 总耗时={Time}ms", step3Time);

            // 生成验证码图片
            var imageStartTime = DateTime.UtcNow;
            var (backgroundImage, sliderImage) = await GenerateCaptchaImagesAsync(targetX, targetY);
            var imageTime = (DateTime.UtcNow - imageStartTime).TotalMilliseconds;
            var totalTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Information("[Captcha Slider] 验证码生成完成: CaptchaId={CaptchaId}, TargetPosition={TargetPos}%, 图片生成耗时={ImageTime}ms, 总耗时={TotalTime}ms",
                captchaId, targetPosition, imageTime, totalTime);

            return new CaptchaGenerateResult
            {
                CaptchaId = captchaId,
                Type = "Slider",
                BackgroundImage = backgroundImage,
                SliderImage = sliderImage,
                TargetPosition = targetPosition
            };
        }
        catch (Exception ex)
        {
            var totalTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
            TaktLogger.Error(ex, "[Captcha Slider] 生成滑块验证码失败，耗时={Time}ms", totalTime);
            throw;
        }
    }

    /// <summary>
    /// 验证验证码
    /// </summary>
    public Task<CaptchaVerifyResult> VerifyAsync(CaptchaVerifyRequest request)
    {
        try
        {
            var captchaId = request.CaptchaId ?? "null";
            TaktLogger.Debug("[Captcha Slider] 开始验证: CaptchaId={CaptchaId}", captchaId);
            
            if (string.IsNullOrEmpty(request.CaptchaId))
            {
                TaktLogger.Warning("[Captcha Slider] 验证码ID为空");
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaIdRequired"
                });
            }

            // 获取验证码数据
            if (!_captchaStore.TryGetValue(captchaId, out var captchaData))
            {
                TaktLogger.Warning("[Captcha Slider] 验证码不存在: CaptchaId={CaptchaId}", captchaId);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaNotFoundOrExpired"
                });
            }

            // 检查是否过期
            var expirationTime = captchaData.CreatedAt.AddMinutes(_expirationMinutes);
            if (DateTime.UtcNow > expirationTime)
            {
                _captchaStore.TryRemove(captchaId, out _);
                TaktLogger.Warning("[Captcha Slider] 验证码已过期: CaptchaId={CaptchaId}, CreatedAt={CreatedAt}, ExpirationTime={ExpirationTime}",
                    captchaId, captchaData.CreatedAt, expirationTime);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaExpired"
                });
            }

            // 防机器人：生成到验证的最短间隔。过短视为自动化脚本
            var elapsedSeconds = (DateTime.UtcNow - captchaData.CreatedAt).TotalSeconds;
            TaktLogger.Debug("[Captcha Slider] 时间检查: Elapsed={Elapsed:F2}s, MinComplete={Min}s",
                elapsedSeconds, _options.MinCompleteSeconds);
            
            if (elapsedSeconds < _options.MinCompleteSeconds)
            {
                _captchaStore.TryRemove(captchaId, out _);
                TaktLogger.Warning("[Captcha Slider] 完成过快，疑似机器人: CaptchaId={CaptchaId}, Elapsed={Elapsed:F2}s < {Min}s",
                    captchaId, elapsedSeconds, _options.MinCompleteSeconds);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaTooFastRetry"
                });
            }

            // 解析用户输入
            if (request.UserInput == null)
            {
                TaktLogger.Warning("[Captcha Slider] 用户输入为空: CaptchaId={CaptchaId}", request.CaptchaId);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaUserInputEmpty"
                });
            }

            // 记录用户输入的详细信息
            TaktLogger.Debug("[Captcha Slider] 用户输入数据: CaptchaId={CaptchaId}, UserInput={UserInputJson}",
                captchaId, JsonConvert.SerializeObject(request.UserInput));

            int userX;
            double? timeSpentSec = null;
            object? trajectory = null;

            if (_options.RequireBehaviorData)
            {
                // 要求行为数据：UserInput 为对象 { position, timeSpent, mouseTrajectory? }
                var parsed = ParseSliderBehaviorInput(request.UserInput);
                if (parsed == null)
                {
                    TaktLogger.Warning("[Captcha Slider] 解析行为数据失败: CaptchaId={CaptchaId}", captchaId);
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaNeedPositionTimeSpent"
                    });
                }
                userX = parsed.Value.position;
                timeSpentSec = parsed.Value.timeSpent;
                trajectory = parsed.Value.trajectory;

                TaktLogger.Debug("[Captcha Slider] 解析行为数据: CaptchaId={CaptchaId}, Position={Position}%, TimeSpent={TimeSpent}s, HasTrajectory={HasTraj}",
                    captchaId, userX, timeSpentSec ?? 0.0, trajectory != null);

                if (!timeSpentSec.HasValue)
                {
                    _captchaStore.TryRemove(captchaId, out _);
                    TaktLogger.Warning("[Captcha Slider] 缺少操作时长: CaptchaId={CaptchaId}", captchaId);
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaNeedTimeSpent"
                    });
                }

                if (timeSpentSec.Value < _options.MinTimeSpentSeconds)
                {
                    _captchaStore.TryRemove(captchaId, out _);
                    TaktLogger.Warning("[Captcha Slider] 操作时长过短，疑似机器人: CaptchaId={CaptchaId}, TimeSpent={Spent:F2}s < {Min}s",
                        captchaId, timeSpentSec.Value, _options.MinTimeSpentSeconds);
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaSlideTooFast"
                    });
                }
            }
            else
            {
                if (!TryParseSliderPosition(request.UserInput, out userX))
                {
                    TaktLogger.Warning("[Captcha Slider] 解析位置失败: CaptchaId={CaptchaId}, UserInput={UserInput}",
                        captchaId, request.UserInput?.ToString() ?? "null");
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaInvalidUserInputFormat"
                    });
                }
                
                TaktLogger.Debug("[Captcha Slider] 解析位置: CaptchaId={CaptchaId}, Position={Position}%",
                    captchaId, userX);
            }

            // 计算用户输入的位置（像素）
            var userPositionX = (int)((double)userX / 100 * _options.Width);

            // 验证位置是否在容差范围内
            var difference = Math.Abs(userPositionX - captchaData.TargetX);
            var success = difference <= _options.Tolerance;

            TaktLogger.Debug("[Captcha Slider] 位置验证: CaptchaId={CaptchaId}, UserPosition={UserPos}% ({UserPosX}px), TargetX={TargetX}px, Difference={Diff}px, Tolerance={Tolerance}px, Success={Success}",
                captchaId, userX, userPositionX, captchaData.TargetX, difference, _options.Tolerance, success);

            // 删除验证码数据（一次性使用）
            _captchaStore.TryRemove(captchaId, out _);

            TaktLogger.Information("[Captcha Slider] 验证完成: CaptchaId={CaptchaId}, Success={Success}, UserPosition={UserPos}% ({UserPosX}px), TargetX={TargetX}px, Difference={Diff}px, Tolerance={Tolerance}px, Message={Message}",
                captchaId, success, userX, userPositionX, captchaData.TargetX, difference, _options.Tolerance, success ? "验证成功" : "验证失败，位置不匹配");

            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = success,
                Message = success ? "validation.captchaVerifySuccess" : "validation.captchaVerifyPositionMismatch"
            });
        }
        catch (Exception ex)
        {
            var captchaId = request?.CaptchaId ?? "null";
            TaktLogger.Error(ex, "[Captcha Slider] 验证滑块验证码失败: CaptchaId={CaptchaId}", captchaId);
            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = false,
                Message = "validation.captchaVerifyProcessError"
            });
        }
    }

    /// <summary>
    /// 生成验证码ID
    /// </summary>
    private string GenerateCaptchaId()
    {
        var bytes = new byte[16];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    /// <summary>
    /// 解析滑块行为数据。期望 { position, timeSpent?, mouseTrajectory? }。
    /// </summary>
    private (int position, double? timeSpent, object? trajectory)? ParseSliderBehaviorInput(object userInput)
    {
        if (userInput is not JObject jo)
            return null;
        
        var posToken = jo["position"];
        if (posToken == null || (posToken.Type != JTokenType.Integer && posToken.Type != JTokenType.Float))
            return null;
        try
        {
            var pos = posToken.ToObject<int>();
            double? timeSpent = null;
            var tsToken = jo["timeSpent"];
            if (tsToken != null && (tsToken.Type == JTokenType.Float || tsToken.Type == JTokenType.Integer))
            {
                try
                {
                    timeSpent = tsToken.ToObject<double>();
                }
                catch
                {
                    // 忽略转换错误
                }
            }
            
            object? trajectory = null;
            var trToken = jo["mouseTrajectory"];
            if (trToken != null && trToken.Type == JTokenType.Array)
                trajectory = trToken;
            
            return (pos, timeSpent, trajectory);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 解析纯数字位置（兼容仅传 position 的情况）。
    /// </summary>
    private bool TryParseSliderPosition(object userInput, out int userX)
    {
        userX = 0;
        if (userInput is JValue jv && (jv.Type == JTokenType.Integer || jv.Type == JTokenType.Float))
        {
            try
            {
                userX = jv.ToObject<int>();
                return true;
            }
            catch
            {
                // 忽略转换错误
            }
        }
        if (userInput is int i)
        {
            userX = i;
            return true;
        }
        return int.TryParse(userInput?.ToString(), out userX);
    }

    /// <summary>
    /// 生成验证码图片（始终使用模板）
    /// </summary>
    private async Task<(string backgroundImage, string sliderImage)> GenerateCaptchaImagesAsync(int targetX, int targetY)
    {
        var startTime = DateTime.UtcNow;
        
        // 加载背景图片
        var bgStartTime = DateTime.UtcNow;
        var backgroundImage = await LoadBackgroundImageAsync();
        var bgTime = (DateTime.UtcNow - bgStartTime).TotalMilliseconds;
        TaktLogger.Debug("[Captcha Slider] 加载背景图片完成: 耗时={Time}ms", bgTime);

        // 从可用的模板组中随机选择一个
        if (_availableTemplateGroups.Count == 0)
        {
            TaktLogger.Error("[Captcha Slider] 没有可用的模板组，无法生成验证码");
            throw new InvalidOperationException("没有可用的模板组，请检查模板文件是否存在");
        }
        
        var templateGroup = _availableTemplateGroups[_random.Next(_availableTemplateGroups.Count)];
        TaktLogger.Debug("[Captcha Slider] 随机选择模板组: {TemplateGroup} (可用组: {AvailableGroups})",
            templateGroup, string.Join(", ", _availableTemplateGroups));

        // 加载模板图片
        var templateStartTime = DateTime.UtcNow;
        var (holeTemplate, sliderTemplate) = await LoadTemplateImagesAsync(templateGroup);
        var templateTime = (DateTime.UtcNow - templateStartTime).TotalMilliseconds;
        TaktLogger.Debug("[Captcha Slider] 加载模板图片完成: 耗时={Time}ms", templateTime);

        // 将 hole 模板合成到背景图片的目标位置
        var applyStartTime = DateTime.UtcNow;
        var backgroundWithHole = ApplyHoleTemplate(backgroundImage, holeTemplate, targetX, targetY);
        var applyTime = (DateTime.UtcNow - applyStartTime).TotalMilliseconds;
        TaktLogger.Debug("[Captcha Slider] 应用hole模板完成: 耗时={Time}ms", applyTime);

        // 创建滑块图片（参照参考代码：直接使用 slider 模板，不提取背景区域）
        var sliderStartTime = DateTime.UtcNow;
        var sliderImage = ApplySliderTemplate(sliderTemplate);
        var sliderTime = (DateTime.UtcNow - sliderStartTime).TotalMilliseconds;
        TaktLogger.Debug("[Captcha Slider] 应用slider模板完成: 耗时={Time}ms", sliderTime);

        // 转换为Base64
        // 注意：滑块图片始终保存为 PNG 格式，以保持原样的颜色和透明度
        var base64StartTime = DateTime.UtcNow;
        var backgroundBase64 = ImageToBase64(backgroundWithHole, _options.BackgroundImages.FileExtension);
        var sliderBase64 = ImageToBase64(sliderImage, ".png"); // 滑块图片始终使用 PNG 格式，保持原样
        var base64Time = (DateTime.UtcNow - base64StartTime).TotalMilliseconds;
        var totalTime = (DateTime.UtcNow - startTime).TotalMilliseconds;
        TaktLogger.Debug("[Captcha Slider] Base64转换完成: 耗时={Time}ms, 总耗时={TotalTime}ms", base64Time, totalTime);

        return (backgroundBase64, sliderBase64);
    }

    /// <summary>
    /// 加载背景图片
    /// </summary>
    private async Task<Image<Rgba32>> LoadBackgroundImageAsync()
    {
        // StoragePath 配置是相对于 wwwroot 的路径，如 "slide/background"
        string wwwrootPath;
        if (_webHostEnvironment != null)
        {
            wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
        }
        else
        {
            var baseDir = AppContext.BaseDirectory;
            wwwrootPath = Path.Combine(baseDir, "wwwroot");
        }
        var storagePath = Path.Combine(wwwrootPath, _options.BackgroundImages.StoragePath);
        
        if (!Directory.Exists(storagePath))
        {
            TaktLogger.Warning("[Captcha Slider] 存储目录不存在，尝试创建: {StoragePath}", storagePath);
            try
            {
                Directory.CreateDirectory(storagePath);
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Slider] 创建存储目录失败: {StoragePath}", storagePath);
            }
        }

        // 获取所有背景图片文件
        var imageFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_options.BackgroundImages.FileExtension}")))
            .Where(f => !Path.GetFileName(f).StartsWith("template_"))
            .ToArray();

        // 如果图片数量不足，下载新图片
        if (imageFiles.Length < _options.BackgroundImages.MinCount)
        {
            await DownloadBackgroundImagesAsync();
            imageFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_options.BackgroundImages.FileExtension}")))
                .Where(f => !Path.GetFileName(f).StartsWith("template_"))
                .ToArray();
        }

        if (imageFiles.Length == 0)
        {
            // 如果没有图片，生成一个随机图片
            return GenerateRandomImage();
        }

        // 随机选择一张图片
        var selectedFile = imageFiles[_random.Next(imageFiles.Length)];
        return await Image.LoadAsync<Rgba32>(selectedFile);
    }

    /// <summary>
    /// 下载背景图片
    /// </summary>
    /// <param name="forceRedownload">是否强制重新下载（删除现有图片后重新下载）</param>
    private async Task DownloadBackgroundImagesAsync(bool forceRedownload = false)
    {
        try
        {
            // StoragePath 配置是相对于 wwwroot 的路径，如 "slide/background"
            string wwwrootPath;
            if (_webHostEnvironment != null)
            {
                wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            }
            else
            {
                var baseDir = AppContext.BaseDirectory;
                wwwrootPath = Path.Combine(baseDir, "wwwroot");
            }
            var storagePath = Path.Combine(wwwrootPath, _options.BackgroundImages.StoragePath);
            
            if (!await Task.Run(() => Directory.Exists(storagePath)))
            {
                TaktLogger.Warning("[Captcha Slider] 存储目录不存在，尝试创建: {StoragePath}", storagePath);
                try
                {
                    await Task.Run(() => Directory.CreateDirectory(storagePath));
                }
                catch (Exception ex)
                {
                    TaktLogger.Error(ex, "[Captcha Slider] 创建存储目录失败: {StoragePath}", storagePath);
                }
            }

            var baseUrl = _options.BackgroundImages.DownloadUrl
                .Replace("{width}", _options.Width.ToString())
                .Replace("{height}", _options.Height.ToString());

            // 计算需要下载的图片数量
            var existingFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_options.BackgroundImages.FileExtension}")))
                .Where(f => !Path.GetFileName(f).StartsWith("template_"))
                .ToArray();
            
            int needDownload;
            if (forceRedownload)
            {
                // 强制重新下载：删除所有现有图片，下载 MinCount 张新图片
                TaktLogger.Information("[Captcha Slider] 强制重新下载，删除现有 {Count} 张背景图片", existingFiles.Length);
                foreach (var file in existingFiles)
                {
                    try
                    {
                        await Task.Run(() => File.Delete(file));
                        TaktLogger.Debug("[Captcha Slider] 删除现有背景图片: {FileName}", Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        TaktLogger.Warning(ex, "[Captcha Slider] 删除背景图片失败: {FileName}", Path.GetFileName(file));
                    }
                }
                needDownload = _options.BackgroundImages.MinCount;
            }
            else
            {
                // 按需下载：只下载不足的部分
                needDownload = Math.Max(0, _options.BackgroundImages.MinCount - existingFiles.Length);
            }

            if (needDownload <= 0)
            {
                TaktLogger.Debug("[Captcha Slider] 背景图片数量充足，无需下载");
                return;
            }

            TaktLogger.Information("[Captcha Slider] 开始下载 {Count} 张背景图片", needDownload);

            for (int i = 0; i < needDownload; i++)
            {
                try
                {
                    // 为每次请求添加随机种子参数，确保获取不同的随机图片
                    // picsum.photos 支持 ?random= 参数来获取不同的随机图片
                    var randomSeed = _random.Next(1, 1000000);
                    var downloadUrl = $"{baseUrl}?random={randomSeed}";

                    TaktLogger.Debug("[Captcha Slider] 下载背景图片 {Index}/{Total}: {Url}", i + 1, needDownload, downloadUrl);

                    var response = await _httpClient.GetAsync(downloadUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var imageBytes = await response.Content.ReadAsByteArrayAsync();
                        
                        // 使用更精确的时间戳和随机数确保文件名唯一性
                        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                        var randomSuffix = _random.Next(1000, 9999);
                        var fileName = $"bg_{timestamp}_{randomSuffix}{_options.BackgroundImages.FileExtension}";
                        var filePath = Path.Combine(storagePath, fileName);
                        
                        await File.WriteAllBytesAsync(filePath, imageBytes);
                        TaktLogger.Debug("[Captcha Slider] 成功下载并保存背景图片: {FileName}", fileName);
                    }
                    else
                    {
                        TaktLogger.Warning("[Captcha Slider] 下载背景图片失败，HTTP状态码: {StatusCode}, URL: {Url}", 
                            response.StatusCode, downloadUrl);
                    }

                    // 添加短暂延迟，避免请求过快
                    if (i < needDownload - 1)
                    {
                        await Task.Delay(100); // 延迟100毫秒
                    }
                }
                catch (Exception ex)
                {
                    TaktLogger.Warning(ex, "[Captcha Slider] 下载背景图片失败: {Index}/{Total}", i + 1, needDownload);
                }
            }

            TaktLogger.Information("[Captcha Slider] 背景图片下载完成");
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Slider] 下载背景图片过程中发生错误");
        }
    }

    /// <summary>
    /// 初始化背景图片
    /// </summary>
    private async Task InitializeBackgroundImagesAsync()
    {
        try
        {
            TaktLogger.Information("[Captcha Slider] 验证码图片初始化开始");
            
            if (_options.BackgroundImages.RedownloadOnStartup)
            {
                TaktLogger.Information("[Captcha Slider] 配置要求启动时重新下载背景图片，开始下载");
                // RedownloadOnStartup=true 时，强制重新下载所有背景图片
                await DownloadBackgroundImagesAsync(forceRedownload: true);
            }
            else
            {
                TaktLogger.Information("[Captcha Slider] 配置不要求启动时重新下载背景图片，跳过下载");
                // RedownloadOnStartup=false 时，不下载，只检查现有图片
                // 如果图片数量不足，会在 LoadBackgroundImageAsync 中按需下载
            }
            
            TaktLogger.Information("[Captcha Slider] 验证码图片初始化完成");
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Slider] 验证码图片初始化失败");
        }
    }

    /// <summary>
    /// 加载模板图片
    /// </summary>
    private async Task<(Image<Rgba32> holeTemplate, Image<Rgba32> sliderTemplate)> LoadTemplateImagesAsync(int templateGroup)
    {
        // 使用与构造函数中相同的路径计算方式
        string wwwrootPath;
        if (_webHostEnvironment != null)
        {
            wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
        }
        else
        {
            var baseDir = AppContext.BaseDirectory;
            wwwrootPath = Path.Combine(baseDir, "wwwroot");
        }
        
        // TemplatePath 配置是相对于 wwwroot 的路径，如 "slide/template"
        var templateBasePath = Path.Combine(wwwrootPath, _options.BackgroundImages.Template.TemplatePath);
        var templatePath = Path.Combine(templateBasePath, templateGroup.ToString());
        
        var holePath = Path.Combine(templatePath, "hole.png");
        var sliderPath = Path.Combine(templatePath, "slider.png");

        TaktLogger.Debug("[Captcha Slider] 加载模板图片: Group={TemplateGroup}, TemplateBasePath={TemplateBasePath}, TemplatePath={TemplatePath}, HolePath={HolePath}, SliderPath={SliderPath}", 
            templateGroup, templateBasePath, templatePath, holePath, sliderPath);

        if (!File.Exists(holePath) || !File.Exists(sliderPath))
        {
            TaktLogger.Error("[Captcha Slider] 模板图片不存在: Group={Group}, TemplatePath={TemplatePath}, HolePath={HolePath} (存在={HoleExists}), SliderPath={SliderPath} (存在={SliderExists})", 
                templateGroup, templatePath, holePath, File.Exists(holePath), sliderPath, File.Exists(sliderPath));
            throw new FileNotFoundException($"模板图片不存在: Group {templateGroup}, TemplatePath: {templatePath}, HolePath: {holePath}, SliderPath: {sliderPath}");
        }

        var holeTemplate = await Image.LoadAsync<Rgba32>(holePath);
        var sliderTemplate = await Image.LoadAsync<Rgba32>(sliderPath);

        TaktLogger.Debug("[Captcha Slider] 成功加载模板图片: Group={TemplateGroup}, HoleSize={HoleSize}, SliderSize={SliderSize}", 
            templateGroup, $"{holeTemplate.Width}x{holeTemplate.Height}", $"{sliderTemplate.Width}x{sliderTemplate.Height}");

        return (holeTemplate, sliderTemplate);
    }

    /// <summary>
    /// 将 hole 模板应用到背景图片
    /// </summary>
    private Image<Rgba32> ApplyHoleTemplate(Image<Rgba32> background, Image<Rgba32> holeTemplate, int targetX, int targetY)
    {
        // 计算滑块尺寸（如果未配置高度则使用宽度，形成正方形）
        var sliderSize = _options.SliderHeight ?? _options.SliderWidth;
        
        var result = background.Clone();
        result.Mutate(ctx =>
        {
            // 调整模板大小以匹配目标尺寸
            var resizedHole = holeTemplate.Clone();
            resizedHole.Mutate(holeCtx => holeCtx.Resize(new ResizeOptions
            {
                Size = new Size(sliderSize, sliderSize),
                Mode = ResizeMode.Stretch
            }));

            // 使用 DrawImage 方法合成图片，使用透明度 0.8f（参照参考代码）
            ctx.DrawImage(resizedHole, new Point(targetX, targetY), 0.8f);
        });
        return result;
    }

    /// <summary>
    /// 将 slider 模板应用到背景图片的目标区域（参照参考代码，完全保持原样）
    /// </summary>
    private Image<Rgba32> ApplySliderTemplate(Image<Rgba32> sliderTemplate)
    {
        // 计算滑块尺寸（如果未配置高度则使用宽度，形成正方形）
        var sliderSize = _options.SliderHeight ?? _options.SliderWidth;
        
        // 参照参考代码：创建滑块图片（保持原始大小）
        var sliderImage = new Image<Rgba32>(sliderSize, sliderSize);
        sliderImage.Mutate(ctx =>
        {
            // 清除为透明背景
            ctx.Clear(Color.Transparent);
            
            // 直接绘制 slider 模板，完全保持原样（参照参考代码，不进行任何尺寸调整）
            ctx.DrawImage(sliderTemplate, new Point(0, 0), 1f);
        });

        TaktLogger.Debug("[Captcha Slider] 应用slider模板: 模板尺寸={TemplateWidth}x{TemplateHeight}, 目标尺寸={TargetSize}x{TargetSize}", 
            sliderTemplate.Width, sliderTemplate.Height, sliderSize, sliderSize);

        return sliderImage;
    }

    /// <summary>
    /// 生成随机图片（当没有背景图片时使用）
    /// </summary>
    private Image<Rgba32> GenerateRandomImage()
    {
        var image = new Image<Rgba32>(_options.Width, _options.Height);
        image.Mutate(ctx =>
        {
            // 生成随机颜色渐变
            for (int y = 0; y < _options.Height; y++)
            {
                for (int x = 0; x < _options.Width; x++)
                {
                    var r = (byte)_random.Next(200, 256);
                    var g = (byte)_random.Next(200, 256);
                    var b = (byte)_random.Next(200, 256);
                    image[x, y] = new Rgba32(r, g, b);
                }
            }
        });
        return image;
    }

    /// <summary>
    /// 将图片转换为Base64字符串
    /// </summary>
    private string ImageToBase64(Image<Rgba32> image, string fileExtension)
    {
        using var ms = new MemoryStream();
        
        if (fileExtension.Equals(".png", StringComparison.OrdinalIgnoreCase))
        {
            image.SaveAsPng(ms);
            return $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
        }
        else
        {
            image.SaveAsJpeg(ms, new JpegEncoder { Quality = 90 });
            return $"data:image/jpeg;base64,{Convert.ToBase64String(ms.ToArray())}";
        }
    }
}
