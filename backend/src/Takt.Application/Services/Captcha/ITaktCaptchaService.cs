// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Captcha
// 文件名称：ITaktCaptchaService.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt验证码服务接口，提供验证码生成和验证功能
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Captcha;

/// <summary>
/// 验证码生成结果
/// </summary>
public class CaptchaGenerateResult
{
    /// <summary>
    /// 验证码ID（用于后续验证）
    /// </summary>
    public string CaptchaId { get; set; } = string.Empty;

    /// <summary>
    /// 验证码类型：Slider（滑块验证码）或 Behavior（行为验证码）
    /// </summary>
    public string Type { get; set; } = "Behavior";

    /// <summary>
    /// 背景图片（Base64编码）
    /// </summary>
    public string BackgroundImage { get; set; } = string.Empty;

    /// <summary>
    /// 滑块图片（Base64编码，仅滑块验证码）
    /// </summary>
    public string? SliderImage { get; set; }

    /// <summary>
    /// 目标位置（百分比，仅滑块验证码）
    /// </summary>
    public int? TargetPosition { get; set; }
}

/// <summary>
/// 验证码验证请求
/// </summary>
public class CaptchaVerifyRequest
{
    /// <summary>
    /// 验证码ID
    /// </summary>
    public string CaptchaId { get; set; } = string.Empty;

    /// <summary>
    /// 用户输入的位置（滑块验证码）或行为数据（行为验证码）
    /// </summary>
    public object? UserInput { get; set; }
}

/// <summary>
/// 验证码验证结果
/// </summary>
public class CaptchaVerifyResult
{
    /// <summary>
    /// 是否验证通过
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 验证消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 验证分数（行为验证码）
    /// </summary>
    public double? Score { get; set; }
}

/// <summary>
/// Takt验证码服务接口
/// </summary>
public interface ITaktCaptchaService
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码生成结果</returns>
    Task<CaptchaGenerateResult> GenerateAsync();

    /// <summary>
    /// 验证验证码
    /// </summary>
    /// <param name="request">验证请求</param>
    /// <returns>验证结果</returns>
    Task<CaptchaVerifyResult> VerifyAsync(CaptchaVerifyRequest request);
}
