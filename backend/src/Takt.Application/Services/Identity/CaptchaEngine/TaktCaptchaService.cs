// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Captcha
// 文件名称：TaktCaptchaService.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt验证码服务实现，支持滑块验证码和行为验证码
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Color = SixLabors.ImageSharp.Color;
using Takt.Shared.Helpers;

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
/// 行为验证码数据（存储在内存中）
/// </summary>
internal class BehaviorCaptchaData
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 行为数据
    /// </summary>
    public Dictionary<string, object> BehaviorData { get; set; } = new();
}

/// <summary>
/// Takt验证码服务实现（支持滑块和行为验证码）
/// </summary>
public class TaktCaptchaService : TaktServiceBase, ITaktCaptchaService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment? _webHostEnvironment;
    private readonly TaktCaptchaOptions _captchaOptions;
    private readonly TaktCaptchaSliderOptions _sliderOptions;
    private readonly TaktCaptchaBehaviorOptions _behaviorOptions;
    private readonly int _expirationMinutes;
    
    // 滑块验证码相关
    private readonly ConcurrentDictionary<string, SliderCaptchaData> _sliderCaptchaStore = new();
    private readonly Random _sliderRandom = new();
    private readonly HttpClient _httpClient = new();
    private readonly List<int> _availableTemplateGroups = new();
    
    // 行为验证码相关
    private readonly ConcurrentDictionary<string, BehaviorCaptchaData> _behaviorCaptchaStore = new();
    private readonly Random _behaviorRandom = new();
    private volatile bool _cleanupTaskStarted = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaService(
        IConfiguration configuration,
        IOptions<TaktCaptchaOptions> captchaOptions,
        IWebHostEnvironment? webHostEnvironment = null)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
        _captchaOptions = captchaOptions.Value;
        _sliderOptions = _captchaOptions.Slider;
        _behaviorOptions = _captchaOptions.Behavior;
        _expirationMinutes = _captchaOptions.ExpirationMinutes;
        
        // 初始化所有验证码服务（Slider 和 Behavior）
        InitializeSliderCaptcha();
        InitializeBehaviorCaptcha();
    }

    #region 滑块验证码初始化

    /// <summary>
    /// 初始化滑块验证码
    /// </summary>
    private void InitializeSliderCaptcha()
    {
        var captchaSection = _configuration.GetSection("Captcha:Slider");
        captchaSection.Bind(_sliderOptions);

        // 获取 wwwroot 路径
        string wwwrootPath = GetWwwRootPath();
        
        var templateBasePath = Path.Combine(wwwrootPath, _sliderOptions.BackgroundImages.Template.TemplatePath);
        var storagePath = Path.Combine(wwwrootPath, _sliderOptions.BackgroundImages.StoragePath);
        
        TaktLogger.Information("[Captcha] 滑块验证码服务初始化: WwwRootPath={WwwRootPath}, TemplatePath={TemplatePath}, StoragePath={StoragePath}, GroupCount={GroupCount}",
            wwwrootPath, templateBasePath, storagePath, _sliderOptions.BackgroundImages.Template.GroupCount);
        
        // 确保目录存在
        EnsureDirectoryExists(templateBasePath, "模板目录");
        EnsureDirectoryExists(storagePath, "存储目录");

        // 验证模板路径，记录可用的模板组
        for (int i = 1; i <= _sliderOptions.BackgroundImages.Template.GroupCount; i++)
        {
            var templateGroupPath = Path.Combine(templateBasePath, i.ToString());
            var holePath = Path.Combine(templateGroupPath, "hole.png");
            var sliderPath = Path.Combine(templateGroupPath, "slider.png");
            
            if (File.Exists(holePath) && File.Exists(sliderPath))
            {
                _availableTemplateGroups.Add(i);
                TaktLogger.Information("[Captcha] 模板组 {Group} 存在", i);
            }
            else
            {
                TaktLogger.Warning("[Captcha] 模板组 {Group} 缺失", i);
            }
        }
        
        if (_availableTemplateGroups.Count == 0)
        {
            TaktLogger.Error("[Captcha] 没有可用的模板组！请检查模板文件是否存在。");
        }
        else
        {
            TaktLogger.Information("[Captcha] 可用模板组数量: {Count}, 组列表: {Groups}", 
                _availableTemplateGroups.Count, string.Join(", ", _availableTemplateGroups));
        }

        TaktLogger.Information("[Captcha] 滑块验证码构造完成，背景图片将在启动时由 TaktCaptchaInitializer 初始化");
    }

    #endregion

    #region 行为验证码初始化

    /// <summary>
    /// 初始化行为验证码
    /// </summary>
    private void InitializeBehaviorCaptcha()
    {
        var captchaSection = _configuration.GetSection("Captcha:Behavior");
        captchaSection.Bind(_behaviorOptions);

        // 注意：不在这里启动清理任务，避免在构造函数中启动后台线程导致启动阻塞
        // 清理任务将在首次生成验证码时按需启动
        
        TaktLogger.Information("[Captcha] 行为验证码服务初始化完成");
    }

    #endregion

    #region 公共接口方法

    /// <summary>
    /// 生成验证码
    /// </summary>
    public async Task<CaptchaGenerateResult> GenerateAsync()
    {
        var captchaType = _captchaOptions.Type;
        
        return captchaType.ToLower() switch
        {
            "slider" => await GenerateSliderCaptchaAsync(),
            "behavior" => GenerateBehaviorCaptcha(),
            _ => throw new InvalidOperationException($"不支持的验证码类型: {captchaType}")
        };
    }

    /// <summary>
    /// 验证验证码
    /// </summary>
    public Task<CaptchaVerifyResult> VerifyAsync(CaptchaVerifyRequest request)
    {
        var captchaType = _captchaOptions.Type;
        
        return captchaType.ToLower() switch
        {
            "slider" => VerifySliderCaptcha(request),
            "behavior" => VerifyBehaviorCaptcha(request),
            _ => throw new InvalidOperationException($"不支持的验证码类型: {captchaType}")
        };
    }

    #endregion

    #region 滑块验证码方法

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    private async Task<CaptchaGenerateResult> GenerateSliderCaptchaAsync()
    {
        var startTime = DateTime.UtcNow;
        try
        {
            TaktLogger.Debug("[Captcha Slider] 开始生成验证码");
            
            // 生成验证码ID
            var captchaId = GenerateCaptchaId();
            TaktLogger.Debug("[Captcha Slider] 步骤1-生成ID完成");

            // 计算滑块尺寸
            var sliderSize = _sliderOptions.SliderHeight ?? _sliderOptions.SliderWidth;
            var margin = sliderSize;
            var minX = margin;
            var maxX = _sliderOptions.Width - sliderSize - margin;

            if (maxX <= minX)
            {
                minX = 0;
                maxX = _sliderOptions.Width - sliderSize;
            }

            // 生成随机位置
            var targetX = _sliderRandom.Next(minX, maxX + 1);
            var targetPosition = (int)((double)targetX / _sliderOptions.Width * 100);
            var targetY = (_sliderOptions.Height - sliderSize) / 2;
            
            TaktLogger.Debug("[Captcha Slider] 目标位置: TargetX={TargetX}px, TargetY={TargetY}px, TargetPosition={TargetPos}%",
                targetX, targetY, targetPosition);

            // 存储验证码数据
            _sliderCaptchaStore.TryAdd(captchaId, new SliderCaptchaData
            {
                TargetX = targetX,
                TargetY = targetY,
                CreatedAt = DateTime.UtcNow
            });

            // 生成验证码图片
            var (backgroundImage, sliderImage) = await GenerateCaptchaImagesAsync(targetX, targetY);
            
            TaktLogger.Information("[Captcha Slider] 验证码生成完成: CaptchaId={CaptchaId}, TargetPosition={TargetPos}%",
                captchaId, targetPosition);

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
            TaktLogger.Error(ex, "[Captcha Slider] 生成滑块验证码失败");
            throw;
        }
    }

    /// <summary>
    /// 验证滑块验证码
    /// </summary>
    private Task<CaptchaVerifyResult> VerifySliderCaptcha(CaptchaVerifyRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.CaptchaId))
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaIdRequired"
                });
            }

            if (!_sliderCaptchaStore.TryGetValue(request.CaptchaId, out var captchaData))
            {
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
                _sliderCaptchaStore.TryRemove(request.CaptchaId, out _);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaExpired"
                });
            }

            // 防机器人检查
            var elapsedSeconds = (DateTime.UtcNow - captchaData.CreatedAt).TotalSeconds;
            if (elapsedSeconds < _sliderOptions.MinCompleteSeconds)
            {
                _sliderCaptchaStore.TryRemove(request.CaptchaId, out _);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaTooFastRetry"
                });
            }

            // 解析用户输入
            if (request.UserInput == null)
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaUserInputEmpty"
                });
            }

            int userX;
            if (_sliderOptions.RequireBehaviorData)
            {
                var parsed = ParseSliderBehaviorInput(request.UserInput);
                if (parsed == null)
                {
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaNeedPositionTimeSpent"
                    });
                }
                userX = parsed.Value.position;
            }
            else
            {
                if (!TryParseSliderPosition(request.UserInput, out userX))
                {
                    return Task.FromResult(new CaptchaVerifyResult
                    {
                        Success = false,
                        Message = "validation.captchaInvalidUserInputFormat"
                    });
                }
            }

            // 验证位置
            var userPositionX = (int)((double)userX / 100 * _sliderOptions.Width);
            var difference = Math.Abs(userPositionX - captchaData.TargetX);
            var success = difference <= _sliderOptions.Tolerance;

            // 删除验证码数据（一次性使用）
            _sliderCaptchaStore.TryRemove(request.CaptchaId, out _);

            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = success,
                Message = success ? "validation.captchaVerifySuccess" : "validation.captchaVerifyPositionMismatch"
            });
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Slider] 验证滑块验证码失败");
            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = false,
                Message = "validation.captchaVerifyProcessError"
            });
        }
    }

    /// <summary>
    /// 生成验证码图片
    /// </summary>
    private async Task<(string backgroundImage, string sliderImage)> GenerateCaptchaImagesAsync(int targetX, int targetY)
    {
        // 加载背景图片
        var backgroundImage = await LoadBackgroundImageAsync();

        // 随机选择模板组
        if (_availableTemplateGroups.Count == 0)
        {
            throw new InvalidOperationException("没有可用的模板组");
        }
        
        var templateGroup = _availableTemplateGroups[_sliderRandom.Next(_availableTemplateGroups.Count)];

        // 加载模板图片
        var (holeTemplate, sliderTemplate) = await LoadTemplateImagesAsync(templateGroup);

        // 应用模板
        var backgroundWithHole = ApplyHoleTemplate(backgroundImage, holeTemplate, targetX, targetY);
        var sliderImage = ApplySliderTemplate(sliderTemplate);

        // 转换为Base64
        var backgroundBase64 = ImageToBase64(backgroundWithHole, _sliderOptions.BackgroundImages.FileExtension);
        var sliderBase64 = ImageToBase64(sliderImage, ".png");

        return (backgroundBase64, sliderBase64);
    }

    /// <summary>
    /// 加载背景图片
    /// </summary>
    private async Task<Image<Rgba32>> LoadBackgroundImageAsync()
    {
        string wwwrootPath = GetWwwRootPath();
        var storagePath = Path.Combine(wwwrootPath, _sliderOptions.BackgroundImages.StoragePath);
        
        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }

        var imageFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_sliderOptions.BackgroundImages.FileExtension}")))
            .Where(f => !Path.GetFileName(f).StartsWith("template_"))
            .ToArray();

        if (imageFiles.Length < _sliderOptions.BackgroundImages.MinCount)
        {
            await DownloadBackgroundImagesAsync();
            imageFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_sliderOptions.BackgroundImages.FileExtension}")))
                .Where(f => !Path.GetFileName(f).StartsWith("template_"))
                .ToArray();
        }

        if (imageFiles.Length == 0)
        {
            return GenerateRandomImage();
        }

        var selectedFile = imageFiles[_sliderRandom.Next(imageFiles.Length)];
        return await Image.LoadAsync<Rgba32>(selectedFile);
    }

    /// <summary>
    /// 下载背景图片
    /// </summary>
    private async Task DownloadBackgroundImagesAsync(bool forceRedownload = false)
    {
        try
        {
            string wwwrootPath = GetWwwRootPath();
            var storagePath = Path.Combine(wwwrootPath, _sliderOptions.BackgroundImages.StoragePath);
            
            if (!await Task.Run(() => Directory.Exists(storagePath)))
            {
                await Task.Run(() => Directory.CreateDirectory(storagePath));
            }

            var baseUrl = _sliderOptions.BackgroundImages.DownloadUrl
                .Replace("{width}", _sliderOptions.Width.ToString())
                .Replace("{height}", _sliderOptions.Height.ToString());

            var existingFiles = (await Task.Run(() => Directory.GetFiles(storagePath, $"*{_sliderOptions.BackgroundImages.FileExtension}")))
                .Where(f => !Path.GetFileName(f).StartsWith("template_"))
                .ToArray();
            
            int needDownload;
            if (forceRedownload)
            {
                foreach (var file in existingFiles)
                {
                    try { await Task.Run(() => File.Delete(file)); }
                    catch { }
                }
                needDownload = _sliderOptions.BackgroundImages.MinCount;
            }
            else
            {
                needDownload = Math.Max(0, _sliderOptions.BackgroundImages.MinCount - existingFiles.Length);
            }

            if (needDownload <= 0) return;

            TaktLogger.Information("[Captcha Slider] 开始下载 {Count} 张背景图片", needDownload);

            for (int i = 0; i < needDownload; i++)
            {
                try
                {
                    var randomSeed = _sliderRandom.Next(1, 1000000);
                    var downloadUrl = $"{baseUrl}?random={randomSeed}";

                    var response = await _httpClient.GetAsync(downloadUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var imageBytes = await response.Content.ReadAsByteArrayAsync();
                        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                        var randomSuffix = _sliderRandom.Next(1000, 9999);
                        var fileName = $"bg_{timestamp}_{randomSuffix}{_sliderOptions.BackgroundImages.FileExtension}";
                        var filePath = Path.Combine(storagePath, fileName);
                        
                        await File.WriteAllBytesAsync(filePath, imageBytes);
                    }

                    if (i < needDownload - 1)
                    {
                        await Task.Delay(100);
                    }
                }
                catch (Exception ex)
                {
                    TaktLogger.Warning(ex, "[Captcha Slider] 下载背景图片失败");
                }
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Slider] 下载背景图片失败");
        }
    }

    /// <summary>
    /// 加载模板图片
    /// </summary>
    private async Task<(Image<Rgba32> holeTemplate, Image<Rgba32> sliderTemplate)> LoadTemplateImagesAsync(int templateGroup)
    {
        string wwwrootPath = GetWwwRootPath();
        var templateBasePath = Path.Combine(wwwrootPath, _sliderOptions.BackgroundImages.Template.TemplatePath);
        var templatePath = Path.Combine(templateBasePath, templateGroup.ToString());
        
        var holePath = Path.Combine(templatePath, "hole.png");
        var sliderPath = Path.Combine(templatePath, "slider.png");

        if (!File.Exists(holePath) || !File.Exists(sliderPath))
        {
            throw new FileNotFoundException($"模板图片不存在: Group {templateGroup}");
        }

        var holeTemplate = await Image.LoadAsync<Rgba32>(holePath);
        var sliderTemplate = await Image.LoadAsync<Rgba32>(sliderPath);

        return (holeTemplate, sliderTemplate);
    }

    /// <summary>
    /// 应用hole模板
    /// </summary>
    private Image<Rgba32> ApplyHoleTemplate(Image<Rgba32> background, Image<Rgba32> holeTemplate, int targetX, int targetY)
    {
        var sliderSize = _sliderOptions.SliderHeight ?? _sliderOptions.SliderWidth;
        
        var result = background.Clone();
        result.Mutate(ctx =>
        {
            var resizedHole = holeTemplate.Clone();
            resizedHole.Mutate(holeCtx => holeCtx.Resize(new ResizeOptions
            {
                Size = new Size(sliderSize, sliderSize),
                Mode = ResizeMode.Stretch
            }));

            ctx.DrawImage(resizedHole, new Point(targetX, targetY), 0.8f);
        });
        return result;
    }

    /// <summary>
    /// 应用slider模板
    /// </summary>
    private Image<Rgba32> ApplySliderTemplate(Image<Rgba32> sliderTemplate)
    {
        var sliderSize = _sliderOptions.SliderHeight ?? _sliderOptions.SliderWidth;
        
        var sliderImage = new Image<Rgba32>(sliderSize, sliderSize);
        sliderImage.Mutate(ctx =>
        {
            ctx.Clear(Color.Transparent);
            ctx.DrawImage(sliderTemplate, new Point(0, 0), 1f);
        });

        return sliderImage;
    }

    /// <summary>
    /// 生成随机图片
    /// </summary>
    private Image<Rgba32> GenerateRandomImage()
    {
        var image = new Image<Rgba32>(_sliderOptions.Width, _sliderOptions.Height);
        image.Mutate(ctx =>
        {
            for (int y = 0; y < _sliderOptions.Height; y++)
            {
                for (int x = 0; x < _sliderOptions.Width; x++)
                {
                    var r = (byte)_sliderRandom.Next(200, 256);
                    var g = (byte)_sliderRandom.Next(200, 256);
                    var b = (byte)_sliderRandom.Next(200, 256);
                    image[x, y] = new Rgba32(r, g, b);
                }
            }
        });
        return image;
    }

    /// <summary>
    /// 图片转Base64
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

    /// <summary>
    /// 解析滑块行为输入
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
                try { timeSpent = tsToken.ToObject<double>(); }
                catch { }
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
    /// 解析滑块位置
    /// </summary>
    private bool TryParseSliderPosition(object userInput, out int userX)
    {
        userX = 0;
        if (userInput is JValue jv && (jv.Type == JTokenType.Integer || jv.Type == JTokenType.Float))
        {
            try { userX = jv.ToObject<int>(); return true; }
            catch { }
        }
        if (userInput is int i)
        {
            userX = i;
            return true;
        }
        return int.TryParse(userInput?.ToString(), out userX);
    }

    #endregion

    #region 行为验证码方法

    /// <summary>
    /// 生成行为验证码
    /// </summary>
    private CaptchaGenerateResult GenerateBehaviorCaptcha()
    {
        var startTime = DateTime.UtcNow;
        try
        {
            // 首次生成验证码时启动清理任务
            if (!_cleanupTaskStarted)
            {
                lock (_behaviorCaptchaStore)
                {
                    if (!_cleanupTaskStarted)
                    {
                        TaktLogger.Debug("[Captcha Behavior] 启动过期数据清理任务");
                        _ = Task.Run(async () => await CleanupExpiredBehaviorDataAsync());
                        _cleanupTaskStarted = true;
                    }
                }
            }
            
            TaktLogger.Debug("[Captcha Behavior] 开始生成验证码");
            
            var captchaId = GenerateCaptchaId();
            var targetPosition = _behaviorRandom.Next(60, 91);

            _behaviorCaptchaStore.TryAdd(captchaId, new BehaviorCaptchaData
            {
                CreatedAt = DateTime.UtcNow,
                BehaviorData = new Dictionary<string, object>
                {
                    { "targetPosition", targetPosition }
                }
            });

            TaktLogger.Information("[Captcha Behavior] 验证码生成完成: CaptchaId={CaptchaId}, TargetPosition={TargetPos}%",
                captchaId, targetPosition);

            return new CaptchaGenerateResult
            {
                CaptchaId = captchaId,
                Type = "Behavior",
                BackgroundImage = string.Empty,
                TargetPosition = targetPosition
            };
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Behavior] 生成行为验证码失败");
            throw;
        }
    }

    /// <summary>
    /// 验证行为验证码
    /// </summary>
    private Task<CaptchaVerifyResult> VerifyBehaviorCaptcha(CaptchaVerifyRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.CaptchaId))
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaIdRequired"
                });
            }

            if (!_behaviorCaptchaStore.TryGetValue(request.CaptchaId, out var captchaData))
            {
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
                _behaviorCaptchaStore.TryRemove(request.CaptchaId, out _);
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaExpired"
                });
            }

            // 防机器人检查
            var elapsedSeconds = (DateTime.UtcNow - captchaData.CreatedAt).TotalSeconds;
            if (elapsedSeconds < _behaviorOptions.MinCompleteSeconds)
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaTooFastRetry"
                });
            }

            if (request.UserInput == null)
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = "validation.captchaUserInputEmpty"
                });
            }

            // 验证行为数据
            var validationFail = ValidateBehaviorInput(request.UserInput, out var failMessage);
            if (validationFail)
            {
                return Task.FromResult(new CaptchaVerifyResult
                {
                    Success = false,
                    Message = failMessage
                });
            }

            // 计算行为分数
            var score = CalculateBehaviorScore(request.UserInput, captchaData);
            var success = score >= _behaviorOptions.ScoreThreshold;

            if (success)
            {
                _behaviorCaptchaStore.TryRemove(request.CaptchaId, out _);
            }

            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = success,
                Message = success ? "validation.captchaVerifySuccess" : "validation.captchaVerifyBehaviorMismatch",
                Score = score
            });
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Behavior] 验证行为验证码失败");
            return Task.FromResult(new CaptchaVerifyResult
            {
                Success = false,
                Message = "validation.captchaVerifyProcessError"
            });
        }
    }

    /// <summary>
    /// 验证行为输入
    /// </summary>
    private bool ValidateBehaviorInput(object userInput, out string message)
    {
        message = string.Empty;
        if (userInput is not JObject jo)
        {
            message = "validation.captchaBehaviorDataInvalid";
            return true;
        }

        if (_behaviorOptions.RequireTimeSpent)
        {
            var tsToken = jo["timeSpent"];
            if (tsToken == null || (tsToken.Type != JTokenType.Float && tsToken.Type != JTokenType.Integer))
            {
                message = "validation.captchaNeedTimeSpent";
                return true;
            }
            double timeSpent;
            try { timeSpent = tsToken.ToObject<double>(); }
            catch
            {
                message = "validation.captchaTimeSpentInvalid";
                return true;
            }
            if (timeSpent < _behaviorOptions.MinTimeSpentSeconds)
            {
                message = "validation.captchaBehaviorTooFast";
                return true;
            }
        }

        if (_behaviorOptions.RequireTrajectory)
        {
            var trToken = jo["mouseTrajectory"];
            if (trToken == null || trToken.Type != JTokenType.Array)
            {
                message = "validation.captchaMouseTrajectoryRequired";
                return true;
            }
            var count = trToken.Children().Count();
            if (count < _behaviorOptions.MinTrajectoryPoints)
            {
                message = "validation.captchaTrajectoryInsufficient";
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 计算行为分数
    /// </summary>
    private double CalculateBehaviorScore(object userInput, BehaviorCaptchaData captchaData)
    {
        try
        {
            double score = 0.0;
            var factors = 0;

            var userInputDict = new Dictionary<string, object>();
            if (userInput is JObject jo)
            {
                foreach (var prop in jo.Properties())
                {
                    userInputDict[prop.Name] = prop.Value?.ToString() ?? string.Empty;
                }
            }
            else if (userInput is Dictionary<string, object> dict)
            {
                userInputDict = dict;
            }

            // 1. 位置准确性（40%）
            if (userInputDict.TryGetValue("position", out var positionObj) &&
                captchaData.BehaviorData.TryGetValue("targetPosition", out var targetPositionObj))
            {
                if (int.TryParse(positionObj?.ToString(), out var userPosition) &&
                    int.TryParse(targetPositionObj?.ToString(), out var targetPosition))
                {
                    var positionDiff = Math.Abs(userPosition - targetPosition);
                    var positionScore = Math.Max(0, 1.0 - (positionDiff / 50.0));
                    score += positionScore * 0.4;
                    factors++;
                }
            }

            // 2. 鼠标轨迹（30%）
            if (userInputDict.TryGetValue("mouseTrajectory", out var trajectoryObj))
            {
                var trajectoryScore = AnalyzeMouseTrajectory(trajectoryObj);
                score += trajectoryScore * 0.3;
                factors++;
            }

            // 3. 时间特征（20%）
            if (userInputDict.TryGetValue("timeSpent", out var timeSpentObj))
            {
                if (double.TryParse(timeSpentObj?.ToString(), out var timeSpent))
                {
                    if (timeSpent < _behaviorOptions.MinTimeSpentSeconds)
                    {
                        timeSpent = 0;
                    }
                    var timeScore = timeSpent >= 1.0 && timeSpent <= 5.0 ? 1.0 :
                                   timeSpent < 1.0 ? timeSpent :
                                   Math.Max(0, 1.0 - (timeSpent - 5.0) / 10.0);
                    score += timeScore * 0.2;
                    factors++;
                }
            }

            // 4. 机器学习（10%）
            if (_behaviorOptions.EnableMachineLearning && userInputDict.Count > 0)
            {
                var mlScore = AnalyzeWithMachineLearning(userInputDict);
                score += mlScore * 0.1;
                factors++;
            }

            if (factors == 0) return 0.0;

            return Math.Min(1.0, score);
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[Captcha Behavior] 计算行为分数失败");
            return 0.0;
        }
    }

    /// <summary>
    /// 分析鼠标轨迹
    /// </summary>
    private double AnalyzeMouseTrajectory(object trajectoryObj)
    {
        try
        {
            if (trajectoryObj is JArray jArray)
            {
                var points = new List<(double x, double y)>();
                foreach (var item in jArray.Children())
                {
                    if (item is JObject pointObj)
                    {
                        var xToken = pointObj["x"];
                        var yToken = pointObj["y"];
                        var x = xToken != null && (xToken.Type == JTokenType.Float || xToken.Type == JTokenType.Integer) ? xToken.ToObject<double>() : 0;
                        var y = yToken != null && (yToken.Type == JTokenType.Float || yToken.Type == JTokenType.Integer) ? yToken.ToObject<double>() : 0;
                        points.Add((x, y));
                    }
                }

                if (points.Count < 2) return 0.0;
                if (points.Count < _behaviorOptions.MinTrajectoryPoints) return 0.0;

                return CalculateTrajectorySmoothness(points);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "[Captcha Behavior] 分析鼠标轨迹失败");
        }

        return 0.5;
    }

    /// <summary>
    /// 计算轨迹平滑度
    /// </summary>
    private double CalculateTrajectorySmoothness(List<(double x, double y)> points)
    {
        if (points.Count < 3) return 0.5;

        var angleChanges = new List<double>();
        for (int i = 1; i < points.Count - 1; i++)
        {
            var dx1 = points[i].x - points[i - 1].x;
            var dy1 = points[i].y - points[i - 1].y;
            var dx2 = points[i + 1].x - points[i].x;
            var dy2 = points[i + 1].y - points[i].y;

            var angle1 = Math.Atan2(dy1, dx1);
            var angle2 = Math.Atan2(dy2, dx2);
            var angleDiff = Math.Abs(angle2 - angle1);
            if (angleDiff > Math.PI) angleDiff = 2 * Math.PI - angleDiff;
            angleChanges.Add(angleDiff);
        }

        var avgAngleChange = angleChanges.Average();
        return Math.Max(0, 1.0 - (avgAngleChange / Math.PI));
    }

    /// <summary>
    /// 机器学习分析（简化版）
    /// </summary>
    private double AnalyzeWithMachineLearning(Dictionary<string, object> behaviorData)
    {
        var dataCompleteness = behaviorData.Count / 10.0;
        return Math.Min(1.0, dataCompleteness);
    }

    /// <summary>
    /// 清理过期行为验证码数据
    /// </summary>
    private async Task CleanupExpiredBehaviorDataAsync()
    {
        while (true)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMinutes(5));

                var expiredKeys = new List<string>();
                var expirationTime = DateTime.UtcNow.AddMinutes(-_expirationMinutes);

                foreach (var kvp in _behaviorCaptchaStore)
                {
                    if (kvp.Value.CreatedAt < expirationTime)
                    {
                        expiredKeys.Add(kvp.Key);
                    }
                }

                foreach (var key in expiredKeys)
                {
                    _behaviorCaptchaStore.TryRemove(key, out _);
                }

                if (expiredKeys.Count > 0)
                {
                    TaktLogger.Debug("[Captcha Behavior] 清理了 {Count} 个过期的行为验证码数据", expiredKeys.Count);
                }
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Behavior] 清理过期数据失败");
            }
        }
    }

    #endregion

    #region 辅助方法

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
    /// 获取wwwroot路径
    /// </summary>
    private string GetWwwRootPath()
    {
        // 使用 TaktFileHelper 提供的统一路径解析
        return TaktFileHelper.GetWwwRootPath(
            _webHostEnvironment?.ContentRootPath,
            _webHostEnvironment?.ContentRootPath ?? AppContext.BaseDirectory);
    }

    /// <summary>
    /// 确保目录存在
    /// </summary>
    private void EnsureDirectoryExists(string path, string directoryName)
    {
        if (!Directory.Exists(path))
        {
            TaktLogger.Warning("[Captcha] {Name}不存在，尝试创建: {Path}", directoryName, path);
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha] 创建{Name}失败: {Path}", directoryName, path);
            }
        }
    }

    #endregion
}
