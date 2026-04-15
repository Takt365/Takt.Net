// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logging
// 文件名称：TaktLoginLog.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt登录日志实体，记录用户登录信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Statistics.Logging;

/// <summary>
/// Takt登录日志实体
/// </summary>
[SugarTable("takt_statistics_logging_login_log", "登录日志表")]
[SugarIndex("ix_takt_statistics_logging_login_log_user_name", nameof(UserName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_login_log_login_ip", nameof(LoginIp), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_login_log_login_type", nameof(LoginType), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_login_log_login_status", nameof(LoginStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_login_log_login_time", nameof(LoginTime), OrderByType.Desc)]
[SugarIndex("ix_takt_statistics_logging_login_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_login_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktLoginLog : TaktEntityBase
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 登录IP
    /// </summary>
    [SugarColumn(ColumnName = "login_ip", ColumnDescription = "登录IP", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    [SugarColumn(ColumnName = "login_location", ColumnDescription = "登录地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 登录国家
    /// </summary>
    [SugarColumn(ColumnName = "login_country", ColumnDescription = "登录国家", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? LoginCountry { get; set; }

    /// <summary>
    /// 登录省份
    /// </summary>
    [SugarColumn(ColumnName = "login_province", ColumnDescription = "登录省份", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? LoginProvince { get; set; }

    /// <summary>
    /// 登录城市
    /// </summary>
    [SugarColumn(ColumnName = "login_city", ColumnDescription = "登录城市", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? LoginCity { get; set; }

    /// <summary>
    /// 登录ISP（互联网服务提供商）
    /// </summary>
    [SugarColumn(ColumnName = "login_isp", ColumnDescription = "登录ISP", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? LoginIsp { get; set; }

    /// <summary>
    /// 登录方式（如：Password=账号密码，RefreshToken=刷新令牌，Sms=手机验证码，Email=邮箱验证码）
    /// </summary>
    [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录方式", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? LoginType { get; set; }

    /// <summary>
    /// User-Agent（原始请求头，用于解析浏览器、操作系统、设备等信息）
    /// </summary>
    [SugarColumn(ColumnName = "user_agent", ColumnDescription = "User-Agent", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 登录状态（0=成功，1=失败）
    /// </summary>
    [SugarColumn(ColumnName = "login_status", ColumnDescription = "登录状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LoginStatus { get; set; } = 0;

    /// <summary>
    /// 登录消息（成功或失败的原因）
    /// </summary>
    [SugarColumn(ColumnName = "login_msg", ColumnDescription = "登录消息", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LoginMsg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    [SugarColumn(ColumnName = "login_time", ColumnDescription = "登录时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime LoginTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 退出时间（如果已退出）
    /// </summary>
    [SugarColumn(ColumnName = "logout_time", ColumnDescription = "退出时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LogoutTime { get; set; }

    /// <summary>
    /// 会话时长（秒，从登录到退出的时长）
    /// </summary>
    [SugarColumn(ColumnName = "session_duration", ColumnDescription = "会话时长", ColumnDataType = "int", IsNullable = true)]
    public int? SessionDuration { get; set; }
}
