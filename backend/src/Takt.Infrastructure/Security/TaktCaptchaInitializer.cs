// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Security
// 文件名称：TaktCaptchaInitializer.cs
// 创建时间：2025-01-28
// 创建人：Takt365(Cursor AI)
// 功能描述：验证码初始化服务，在应用启动时初始化验证码资源
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Takt.Application.Services.Captcha;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Infrastructure.Security;

/// <summary>
/// 验证码初始化服务
/// </summary>
public class TaktCaptchaInitializer : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly TaktCaptchaOptions _options;
    private readonly HttpClient _httpClient;
    private readonly string _backgroundImagesPath;
    private readonly string _templatePath;

    /// <summary>
    /// 初始化验证码初始化服务
    /// </summary>
    public TaktCaptchaInitializer(
        IServiceScopeFactory serviceScopeFactory,
        IWebHostEnvironment webHostEnvironment,
        IOptions<TaktCaptchaOptions> options,
        IHttpClientFactory httpClientFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _webHostEnvironment = webHostEnvironment;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClientFactory.CreateClient();

        // 获取 wwwroot 路径：优先使用 ContentRootPath，与 TaktCaptchaSliderService 保持一致
        var wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
        
        // TemplatePath 配置是相对于 wwwroot 的路径，如 "slide/template"
        _templatePath = Path.Combine(wwwrootPath, _options.Slider.BackgroundImages.Template.TemplatePath);
        // StoragePath 配置是相对于 wwwroot 的路径，如 "slide/background"
        _backgroundImagesPath = Path.Combine(wwwrootPath, _options.Slider.BackgroundImages.StoragePath);

        TaktLogger.Information("[Captcha Initializer] 验证码初始化服务构造完成: TemplatePath={TemplatePath}, BackgroundPath={BackgroundPath}",
            MaskPath(_templatePath), MaskPath(_backgroundImagesPath));
    }

    /// <summary>
    /// 启动验证码初始化服务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>初始化任务</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TaktLogger.Information("[Captcha Initializer] 开始初始化验证码服务...");
        try
        {
            // 初始化资源
            await InitializeResourcesAsync(cancellationToken);

            // 初始化服务
            using var scope = _serviceScopeFactory.CreateScope();
            var captchaService = scope.ServiceProvider.GetRequiredService<ITaktCaptchaService>();

            // 生成一个验证码来验证服务
            TaktLogger.Information("[Captcha Initializer] 正在生成首个验证码以验证服务...");
            await captchaService.GenerateAsync();

            TaktLogger.Information("[Captcha Initializer] 验证码服务初始化完成");
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Initializer] 初始化验证码服务时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 停止验证码初始化服务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>停止任务</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task InitializeResourcesAsync(CancellationToken cancellationToken)
    {
        try
        {
            TaktLogger.Information("[Captcha Initializer] 开始初始化验证码资源...");

            // 确保 wwwroot 目录存在
            var wwwrootPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            if (!await Task.Run(() => Directory.Exists(wwwrootPath), cancellationToken))
            {
                TaktLogger.Warning("[Captcha Initializer] wwwroot 目录不存在: {Path}", MaskPath(wwwrootPath));
                await Task.Run(() => Directory.CreateDirectory(wwwrootPath), cancellationToken);
                TaktLogger.Information("[Captcha Initializer] 已创建 wwwroot 目录");
            }

            // 验证模板
            await ValidateTemplatesAsync(cancellationToken);

            // 初始化背景图片
            await InitializeBackgroundImagesAsync(cancellationToken);

            TaktLogger.Information("[Captcha Initializer] 验证码资源初始化完成");
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Initializer] 初始化验证码资源时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task ValidateTemplatesAsync(CancellationToken cancellationToken)
    {
        try
        {
            TaktLogger.Information("[Captcha Initializer] 开始验证滑块验证码模板...");

            // 检查并创建模板目录
            if (!await Task.Run(() => Directory.Exists(_templatePath), cancellationToken))
            {
                TaktLogger.Information("[Captcha Initializer] 创建模板目录: {Path}", MaskPath(_templatePath));
                await Task.Run(() => Directory.CreateDirectory(_templatePath), cancellationToken);
            }

            // 检查每个模板组目录
            var validGroupCount = 0;
            for (int i = 1; i <= _options.Slider.BackgroundImages.Template.GroupCount; i++)
            {
                var groupPath = Path.Combine(_templatePath, i.ToString());
                TaktLogger.Information("[Captcha Initializer] 检查模板组目录 {Group}: {Path}", i, MaskPath(groupPath));

                if (!await Task.Run(() => Directory.Exists(groupPath), cancellationToken))
                {
                    TaktLogger.Warning("[Captcha Initializer] 模板组目录 {Group} 不存在: {Path}", i, MaskPath(groupPath));
                    continue;
                }

                // 检查模板组中的图片文件
                var holePath = Path.Combine(groupPath, "hole.png");
                var sliderPath = Path.Combine(groupPath, "slider.png");

                if (!await Task.Run(() => File.Exists(holePath), cancellationToken))
                {
                    TaktLogger.Warning("[Captcha Initializer] 模板组 {Group} 缺少挖空背景图: {Path}", i, MaskPath(holePath));
                    continue;
                }

                if (!await Task.Run(() => File.Exists(sliderPath), cancellationToken))
                {
                    TaktLogger.Warning("[Captcha Initializer] 模板组 {Group} 缺少滑块图片: {Path}", i, MaskPath(sliderPath));
                    continue;
                }

                validGroupCount++;
            }

            var isValid = validGroupCount > 0;
            TaktLogger.Information("[Captcha Initializer] 模板验证{Result}, 有效模板组数量: {Count}",
                isValid ? "通过" : "失败", validGroupCount);

            if (!isValid)
            {
                TaktLogger.Error("[Captcha Initializer] 验证码模板验证失败: 模板路径={TemplatePath}, 请确保模板文件存在于以下路径:", MaskPath(_templatePath));
                for (int i = 1; i <= _options.Slider.BackgroundImages.Template.GroupCount; i++)
                {
                    var expectedGroupPath = Path.Combine(_templatePath, i.ToString());
                    var expectedHolePath = Path.Combine(expectedGroupPath, "hole.png");
                    var expectedSliderPath = Path.Combine(expectedGroupPath, "slider.png");
                    TaktLogger.Error("[Captcha Initializer]   组 {Group}: {GroupPath}", i, MaskPath(expectedGroupPath));
                    TaktLogger.Error("[Captcha Initializer]     - hole.png: {HolePath}", MaskPath(expectedHolePath));
                    TaktLogger.Error("[Captcha Initializer]     - slider.png: {SliderPath}", MaskPath(expectedSliderPath));
                }
                throw new InvalidOperationException($"验证码模板验证失败: 未找到任何有效的模板组。请检查模板文件是否存在于 {MaskPath(_templatePath)} 目录下。");
            }

            TaktLogger.Information("[Captcha Initializer] 滑块验证码模板验证完成");
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Initializer] 验证滑块验证码模板时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task InitializeBackgroundImagesAsync(CancellationToken cancellationToken)
    {
        try
        {
            // 检查目录中的图片数量
            TaktLogger.Information("[Captcha Initializer] 开始检查背景图片目录: {Path}", MaskPath(_backgroundImagesPath));
            var existingFiles = await Task.Run(() => Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}"), cancellationToken);
            TaktLogger.Information("[Captcha Initializer] 当前背景图片数量: {Count}", existingFiles.Length);

            // 如果配置了启动时重新下载，则删除现有图片
            if (_options.Slider.BackgroundImages.RedownloadOnStartup && existingFiles.Length > 0)
            {
                TaktLogger.Information("[Captcha Initializer] 配置为启动时重新下载图片，正在删除现有图片...");
                foreach (var file in existingFiles)
                {
                    try
                    {
                        await Task.Run(() => File.Delete(file), cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        TaktLogger.Error(ex, "[Captcha Initializer] 删除图片文件时发生错误: {File}, {Message}", MaskPath(file), ex.Message);
                    }
                }
                existingFiles = Array.Empty<string>();
                TaktLogger.Information("[Captcha Initializer] 现有图片已删除");
            }

            // 如果图片数量不足，则下载新图片
            if (existingFiles.Length < _options.Slider.BackgroundImages.MinCount)
            {
                TaktLogger.Information("[Captcha Initializer] 背景图片数量不足{Count}张，开始下载新图片", _options.Slider.BackgroundImages.MinCount);
                await DownloadBackgroundImagesAsync(cancellationToken);
            }
            else
            {
                TaktLogger.Information("[Captcha Initializer] 背景图片数量已足够，无需下载");
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Initializer] 检查背景图片时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task DownloadBackgroundImagesAsync(CancellationToken cancellationToken)
    {
        // 检查目录中的图片数量
        var existingFiles = await Task.Run(() => Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}"), cancellationToken);
        if (existingFiles.Length >= _options.Slider.BackgroundImages.MinCount)
        {
            TaktLogger.Debug("[Captcha Initializer] 背景图片数量已足够，跳过下载");
            return;
        }

        TaktLogger.Information("[Captcha Initializer] 开始下载滑块验证码背景图片，当前已有图片数量: {Count}", existingFiles.Length);

        var downloadedCount = 0;
        var neededCount = _options.Slider.BackgroundImages.MinCount - existingFiles.Length;

        // 确保目录存在
        if (!await Task.Run(() => Directory.Exists(_backgroundImagesPath), cancellationToken))
        {
            await Task.Run(() => Directory.CreateDirectory(_backgroundImagesPath), cancellationToken);
            TaktLogger.Information("[Captcha Initializer] 已创建背景图片目录: {Path}", MaskPath(_backgroundImagesPath));
        }

        for (int i = 0; i < neededCount; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            var fileName = $"{Guid.NewGuid()}{_options.Slider.BackgroundImages.FileExtension}";
            var filePath = Path.Combine(_backgroundImagesPath, fileName);

            try
            {
                // 替换URL中的占位符
                var url = _options.Slider.BackgroundImages.DownloadUrl
                    .Replace("{width}", _options.Slider.Width.ToString())
                    .Replace("{height}", _options.Slider.Height.ToString());

                TaktLogger.Debug("[Captcha Initializer] 开始下载背景图片: {Url}", url);

                // 下载图片
                var response = await _httpClient.GetAsync(url, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    TaktLogger.Error("[Captcha Initializer] 下载背景图片失败: {StatusCode}", response.StatusCode);
                    continue;
                }

                // 保存图片
                await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                await using var fileStream = File.Create(filePath);
                await stream.CopyToAsync(fileStream, cancellationToken);

                TaktLogger.Debug("[Captcha Initializer] 背景图片下载成功: {Path}", MaskPath(filePath));
                downloadedCount++;
            }
            catch (Exception ex)
            {
                TaktLogger.Error(ex, "[Captcha Initializer] 下载背景图片时发生错误: {Message}", ex.Message);
            }
        }

        TaktLogger.Information("[Captcha Initializer] 背景图片下载完成，成功下载 {Count} 张图片", downloadedCount);
    }

    /// <summary>
    /// 掩码路径（隐藏敏感路径信息）
    /// </summary>
    private static string MaskPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return path ?? string.Empty;

        // 只显示最后两级目录
        var parts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        if (parts.Length <= 2)
            return path;

        return "..." + Path.DirectorySeparatorChar + string.Join(Path.DirectorySeparatorChar, parts.Skip(parts.Length - 2));
    }
}
