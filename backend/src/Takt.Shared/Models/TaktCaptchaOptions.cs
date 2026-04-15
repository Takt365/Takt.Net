// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Models
// 文件名称：TaktCaptchaOptions.cs
// 创建时间：2025-01-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt验证码配置选项，用于配置验证码相关设置
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Models;

/// <summary>
/// Takt验证码配置选项
/// </summary>
public class TaktCaptchaOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaOptions()
    {
        Slider = new TaktCaptchaSliderOptions();
        Behavior = new TaktCaptchaBehaviorOptions();
    }

    /// <summary>
    /// 是否启用验证码
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 验证码类型：Slider（滑块验证码）或 Behavior（行为验证码）
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 验证码过期时间（分钟），统一应用于所有验证码类型
    /// </summary>
    public int ExpirationMinutes { get; set; }

    /// <summary>
    /// 滑块验证码配置
    /// </summary>
    public TaktCaptchaSliderOptions Slider { get; set; } = new();

    /// <summary>
    /// 行为验证码配置
    /// </summary>
    public TaktCaptchaBehaviorOptions Behavior { get; set; } = new();
}

/// <summary>
/// 滑块验证码配置选项
/// </summary>
public class TaktCaptchaSliderOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaSliderOptions()
    {
        BackgroundImages = new TaktCaptchaBackgroundImagesOptions();
    }

    /// <summary>
    /// 验证码图片宽度（像素）
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 验证码图片高度（像素）
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 滑块宽度（像素）
    /// </summary>
    public int SliderWidth { get; set; }

    /// <summary>
    /// 滑块高度（像素）。如果未设置，则使用 SliderWidth（正方形滑块）
    /// </summary>
    public int? SliderHeight { get; set; }

    /// <summary>
    /// 容差值（像素），允许的误差范围
    /// </summary>
    public int Tolerance { get; set; }

    /// <summary>
    /// 最短完成时间（秒）。从生成到提交验证的间隔小于此值视为机器人，直接拒绝。
    /// </summary>
    public double MinCompleteSeconds { get; set; }

    /// <summary>
    /// 是否要求前端提交行为数据（timeSpent、mouseTrajectory）。为 true 时仅传 position 将拒绝。
    /// </summary>
    public bool RequireBehaviorData { get; set; }

    /// <summary>
    /// 前端上报的最短操作时长（秒）。小于此值视为机器人。
    /// </summary>
    public double MinTimeSpentSeconds { get; set; }

    /// <summary>
    /// 背景图片配置
    /// </summary>
    public TaktCaptchaBackgroundImagesOptions BackgroundImages { get; set; } = new();
}

/// <summary>
/// 背景图片配置选项
/// </summary>
public class TaktCaptchaBackgroundImagesOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaBackgroundImagesOptions()
    {
        DownloadUrl = string.Empty;
        StoragePath = string.Empty;
        FileExtension = string.Empty;
        Template = new TaktCaptchaTemplateOptions();
    }

    /// <summary>
    /// 启动时是否重新下载背景图片
    /// </summary>
    public bool RedownloadOnStartup { get; set; }

    /// <summary>
    /// 最小图片数量
    /// </summary>
    public int MinCount { get; set; }

    /// <summary>
    /// 下载URL模板，支持 {width} 和 {height} 占位符
    /// </summary>
    public string DownloadUrl { get; set; } = string.Empty;

    /// <summary>
    /// 存储路径（相对于应用程序根目录）
    /// </summary>
    public string StoragePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件扩展名（如 .jpg, .png）
    /// </summary>
    public string FileExtension { get; set; } = string.Empty;

    /// <summary>
    /// 模板配置
    /// </summary>
    public TaktCaptchaTemplateOptions Template { get; set; } = new();
}

/// <summary>
/// 模板配置选项
/// </summary>
public class TaktCaptchaTemplateOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCaptchaTemplateOptions()
    {
        TemplatePath = string.Empty;
    }

    /// <summary>
    /// 是否使用模板
    /// </summary>
    public bool UseTemplate { get; set; }

    /// <summary>
    /// 模板路径（相对于应用程序根目录）
    /// </summary>
    public string TemplatePath { get; set; } = string.Empty;

    /// <summary>
    /// 分组数量
    /// </summary>
    public int GroupCount { get; set; }
}

/// <summary>
/// 行为验证码配置选项
/// </summary>
public class TaktCaptchaBehaviorOptions
{
    /// <summary>
    /// 分数阈值，低于此分数将被判定为机器人
    /// </summary>
    public double ScoreThreshold { get; set; }

    /// <summary>
    /// 是否启用机器学习
    /// </summary>
    public bool EnableMachineLearning { get; set; }

    /// <summary>
    /// 最短完成时间（秒）。从生成到提交验证的间隔小于此值视为机器人，直接拒绝。
    /// </summary>
    public double MinCompleteSeconds { get; set; }

    /// <summary>
    /// 前端上报的最短操作时长（秒）。小于此值视为机器人，该项不计分。
    /// </summary>
    public double MinTimeSpentSeconds { get; set; }

    /// <summary>
    /// 鼠标轨迹最少采样点数。少于此次数视为无效轨迹，该项不计分。
    /// </summary>
    public int MinTrajectoryPoints { get; set; }

    /// <summary>
    /// 是否强制要求提交 timeSpent。未提交或无效时直接失败。
    /// </summary>
    public bool RequireTimeSpent { get; set; }

    /// <summary>
    /// 是否强制要求提交 mouseTrajectory。未提交或无效时直接失败。
    /// </summary>
    public bool RequireTrajectory { get; set; }
}
