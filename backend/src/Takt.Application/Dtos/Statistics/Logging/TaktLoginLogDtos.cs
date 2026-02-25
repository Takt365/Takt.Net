// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logging
// 文件名称：TaktLoginLogDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt登录日志DTO，包含登录日志相关的数据传输对象（查询、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// Takt登录日志DTO
/// </summary>
public class TaktLoginLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogDto()
    {
        UserName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 登录日志ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LoginLogId { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 登录方式（如：Password=账号密码，RefreshToken=刷新令牌，Sms=手机验证码，Email=邮箱验证码）
    /// </summary>
    public string? LoginType { get; set; }

    /// <summary>
    /// User-Agent（原始请求头）
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 登录状态（0=成功，1=失败）
    /// </summary>
    public int LoginStatus { get; set; }

    /// <summary>
    /// 登录消息（成功或失败的原因）
    /// </summary>
    public string? LoginMsg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 退出时间（如果已退出）
    /// </summary>
    public DateTime? LogoutTime { get; set; }

    /// <summary>
    /// 会话时长（秒，从登录到退出的时长）
    /// </summary>
    public int? SessionDuration { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt登录日志查询DTO
/// </summary>
public class TaktLoginLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、登录IP中模糊查询

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public string? LoginType { get; set; }

    /// <summary>
    /// 登录状态（0=成功，1=失败）
    /// </summary>
    public int? LoginStatus { get; set; }

    /// <summary>
    /// 登录时间开始
    /// </summary>
    public DateTime? LoginTimeStart { get; set; }

    /// <summary>
    /// 登录时间结束
    /// </summary>
    public DateTime? LoginTimeEnd { get; set; }
}

/// <summary>
/// Takt创建登录日志DTO
/// </summary>
public class TaktCreateLoginLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCreateLoginLogDto()
    {
        UserName = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 登录方式（如：Password=账号密码，RefreshToken=刷新令牌，Sms=手机验证码，Email=邮箱验证码）
    /// </summary>
    public string? LoginType { get; set; }

    /// <summary>
    /// User-Agent（原始请求头）
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 登录状态（0=成功，1=失败）
    /// </summary>
    public int LoginStatus { get; set; }

    /// <summary>
    /// 登录消息（成功或失败的原因）
    /// </summary>
    public string? LoginMsg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime? LoginTime { get; set; }

    /// <summary>
    /// 退出时间（如果已退出）
    /// </summary>
    public DateTime? LogoutTime { get; set; }

    /// <summary>
    /// 会话时长（秒，从登录到退出的时长）
    /// </summary>
    public int? SessionDuration { get; set; }
}

/// <summary>
/// Takt登录日志导出DTO
/// </summary>
public class TaktLoginLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginLogExportDto()
    {
        UserName = string.Empty;
        LoginIp = string.Empty;
        LoginLocation = string.Empty;
        LoginType = string.Empty;
        LoginStatus = string.Empty;
        LoginMsg = string.Empty;
        LoginTime = DateTime.Now;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string LoginIp { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    public string LoginLocation { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public string LoginType { get; set; }

    /// <summary>
    /// 登录状态
    /// </summary>
    public string LoginStatus { get; set; }

    /// <summary>
    /// 登录消息
    /// </summary>
    public string LoginMsg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 退出时间
    /// </summary>
    public DateTime? LogoutTime { get; set; }

    /// <summary>
    /// 会话时长（秒）
    /// </summary>
    public int? SessionDuration { get; set; }
}
