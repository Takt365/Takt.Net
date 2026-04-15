// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktCaptchaController.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt验证码控制器，提供验证码生成和验证的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Takt.Application.Services.Captcha;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 验证码控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-27
/// </remarks>
[Route("api/[controller]")]
[AllowAnonymous] // 验证码接口不需要认证
public class TaktCaptchaController : TaktControllerBase
{
    private readonly ITaktCaptchaService _captchaService;
    private readonly IConfiguration _configuration;
    private readonly IComponentContext _componentContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="captchaService">验证码服务（默认服务，用于生成）</param>
    /// <param name="configuration">配置</param>
    /// <param name="componentContext">Autofac组件上下文（用于解析特定类型的服务）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktCaptchaController(
        ITaktCaptchaService captchaService,
        IConfiguration configuration,
        IComponentContext componentContext,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _captchaService = captchaService;
        _configuration = configuration;
        _componentContext = componentContext;
    }

    private static bool IsCaptchaNotFoundOrExpiredMessage(string? message) =>
        string.Equals(message, "validation.captchaNotFoundOrExpired", StringComparison.Ordinal);

    private string LocalizeValidationMessage(string? keyOrText)
    {
        if (string.IsNullOrEmpty(keyOrText))
            return keyOrText ?? string.Empty;
        if (keyOrText.StartsWith("validation.", StringComparison.Ordinal))
            return GetLocalizedString(keyOrText, "Frontend");
        return keyOrText;
    }

    private CaptchaVerifyResult WithLocalizedMessage(CaptchaVerifyResult r) =>
        new()
        {
            Success = r.Success,
            Score = r.Score,
            Message = LocalizeValidationMessage(r.Message)
        };

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码生成结果</returns>
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateAsync()
    {
        try
        {
            TaktLogger.Debug("[Captcha Generate] 收到生成验证码请求");
            
            // 检查验证码是否启用
            var captchaEnabled = _configuration.GetValue<bool>("Captcha:Enabled", false);
            var captchaType = _configuration.GetValue<string>("Captcha:Type") ?? "Behavior";
            
            TaktLogger.Debug("[Captcha Generate] 配置检查: Enabled={Enabled}, Type={Type}", captchaEnabled, captchaType);
            
            if (!captchaEnabled)
            {
                TaktLogger.Information("[Captcha Generate] 验证码未启用，返回空数据");
                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        captchaId = string.Empty,
                        type = captchaType,
                        backgroundImage = string.Empty,
                        sliderImage = (string?)null,
                        targetPosition = (int?)null,
                        enabled = false
                    }
                });
            }

            var result = await _captchaService.GenerateAsync();
            
            TaktLogger.Information("[Captcha Generate] 验证码生成成功: CaptchaId={CaptchaId}, Type={Type}, TargetPosition={TargetPosition}, HasBackgroundImage={HasBg}, HasSliderImage={HasSlider}",
                result.CaptchaId ?? "null",
                result.Type ?? "null",
                result.TargetPosition ?? 0,
                !string.IsNullOrEmpty(result.BackgroundImage),
                !string.IsNullOrEmpty(result.SliderImage));
            
            // 确保返回的 type 和 enabled 与配置一致
            return Ok(new
            {
                success = true,
                data = new
                {
                    captchaId = result.CaptchaId,
                    type = captchaType, // 使用配置中的类型，确保与 appsettings.json 一致
                    backgroundImage = result.BackgroundImage,
                    sliderImage = result.SliderImage,
                    targetPosition = result.TargetPosition,
                    enabled = captchaEnabled // 使用配置中的启用状态，确保与 appsettings.json 一致
                }
            });
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Generate] 生成验证码失败");
            return StatusCode(500, new
            {
                success = false,
                message = GetLocalizedString("validation.captchaGenerateFailed", "Frontend"),
                error = ex.Message
            });
        }
    }

    /// <summary>
    /// 验证验证码
    /// </summary>
    /// <param name="request">验证请求</param>
    /// <returns>验证结果</returns>
    [HttpPost("verify")]
    public async Task<IActionResult> VerifyAsync([FromBody] CaptchaVerifyRequest request)
    {
        try
        {
            TaktLogger.Information("[Captcha Verify] 收到验证请求: CaptchaId={CaptchaId}, UserInputType={UserInputType}",
                request?.CaptchaId ?? "null",
                request?.UserInput?.GetType().Name ?? "null");
            
            if (request == null)
            {
                TaktLogger.Warning("[Captcha Verify] 验证请求为null");
                return StatusCode(400, new
                {
                    success = false,
                    message = GetLocalizedString("validation.captchaVerifyRequestRequired", "Frontend")
                });
            }

            // 检查验证码是否启用
            var captchaEnabled = _configuration.GetValue<bool>("Captcha:Enabled", false);
            var captchaIdForLog = request?.CaptchaId ?? "null";
            
            if (!captchaEnabled)
            {
                TaktLogger.Information("[Captcha Verify] 验证码未启用，自动通过: CaptchaId={CaptchaId}", captchaIdForLog);
                return Ok(new
                {
                    success = true,
                    data = new CaptchaVerifyResult
                    {
                        Success = true,
                        Message = GetLocalizedString("validation.captchaDisabledAutoPass", "Frontend")
                    }
                });
            }

            // 记录用户输入的详细信息
            if (request?.UserInput != null)
            {
                TaktLogger.Debug("[Captcha Verify] 用户输入详情: {UserInputJson}", 
                    JsonConvert.SerializeObject(request.UserInput));
            }

            // 根据验证码ID确定使用哪个服务进行验证
            // 尝试在两个服务中查找验证码，找到哪个就用哪个
            CaptchaVerifyResult result;

            // 先尝试 Slider 服务
            var sliderService = _componentContext.ResolveNamed<ITaktCaptchaService>("Slider");
            result = await sliderService.VerifyAsync(request ?? new CaptchaVerifyRequest());
            
            // 如果 Slider 服务返回"验证码不存在"，尝试 Behavior 服务
            if (!result.Success && IsCaptchaNotFoundOrExpiredMessage(result.Message))
            {
                TaktLogger.Debug("[Captcha Verify] Slider 服务中未找到验证码，尝试 Behavior 服务: CaptchaId={CaptchaId}", captchaIdForLog);
                var behaviorService = _componentContext.ResolveNamed<ITaktCaptchaService>("Behavior");
                result = await behaviorService.VerifyAsync(request ?? new CaptchaVerifyRequest());
                
                if (result.Success || !IsCaptchaNotFoundOrExpiredMessage(result.Message))
                {
                    TaktLogger.Debug("[Captcha Verify] 使用 Behavior 服务验证: CaptchaId={CaptchaId}, Success={Success}", 
                        captchaIdForLog, result.Success);
                }
                else
                {
                    TaktLogger.Warning("[Captcha Verify] 在两个服务中都找不到验证码: CaptchaId={CaptchaId}", captchaIdForLog);
                }
            }
            else
            {
                TaktLogger.Debug("[Captcha Verify] 使用 Slider 服务验证: CaptchaId={CaptchaId}, Success={Success}", 
                    captchaIdForLog, result.Success);
            }
            
            var captchaId = request?.CaptchaId ?? "null";
            var message = result.Message ?? "null";
            var score = result.Score ?? 0.0;
            TaktLogger.Information("[Captcha Verify] 验证完成: CaptchaId={CaptchaId}, Success={Success}, Message={Message}, Score={Score}",
                captchaId ?? "null", result.Success, message ?? "null", score);

            return Ok(new
            {
                success = true,
                data = WithLocalizedMessage(result)
            });
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[Captcha Verify] 验证验证码异常: CaptchaId={CaptchaId}", request?.CaptchaId ?? "null");
            return StatusCode(500, new
            {
                success = false,
                message = GetLocalizedString("validation.captchaVerifyEndpointFailed", "Frontend"),
                error = ex.Message
            });
        }
    }
}
