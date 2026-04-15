// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.SignalR
// 文件名称：TaktOnline.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线用户实体，用于通过SignalR管理在线用户
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线用户实体
/// </summary>
[SugarTable("takt_routine_signalr_online", "在线用户表")]
[SugarIndex("ix_takt_routine_signalr_online_connection_id", nameof(ConnectionId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_signalr_online_user_name", nameof(UserName), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_online_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_online_online_status", nameof(OnlineStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_online_connect_time", nameof(ConnectTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_signalr_online_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_signalr_online_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktOnline : TaktEntityBase
{
    /// <summary>
    /// SignalR连接ID（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "connection_id", ColumnDescription = "SignalR连接ID", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 在线状态（0=在线，1=离线，2=离开）
    /// </summary>
    [SugarColumn(ColumnName = "online_status", ColumnDescription = "在线状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OnlineStatus { get; set; } = 0;

    /// <summary>
    /// 连接IP地址
    /// </summary>
    [SugarColumn(ColumnName = "connect_ip", ColumnDescription = "连接IP地址", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ConnectIp { get; set; }

    /// <summary>
    /// 连接地点
    /// </summary>
    [SugarColumn(ColumnName = "connect_location", ColumnDescription = "连接地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ConnectLocation { get; set; }

    /// <summary>
    /// 连接国家
    /// </summary>
    [SugarColumn(ColumnName = "connect_country", ColumnDescription = "连接国家", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ConnectCountry { get; set; }

    /// <summary>
    /// 连接省份
    /// </summary>
    [SugarColumn(ColumnName = "connect_province", ColumnDescription = "连接省份", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ConnectProvince { get; set; }

    /// <summary>
    /// 连接城市
    /// </summary>
    [SugarColumn(ColumnName = "connect_city", ColumnDescription = "连接城市", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ConnectCity { get; set; }

    /// <summary>
    /// 连接ISP（互联网服务提供商）
    /// </summary>
    [SugarColumn(ColumnName = "connect_isp", ColumnDescription = "连接ISP", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ConnectIsp { get; set; }

    /// <summary>
    /// User-Agent（浏览器信息）
    /// </summary>
    [SugarColumn(ColumnName = "user_agent", ColumnDescription = "User-Agent", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// 设备类型（如：PC、Mobile、Tablet）
    /// </summary>
    [SugarColumn(ColumnName = "device_type", ColumnDescription = "设备类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeviceType { get; set; }

    /// <summary>
    /// 浏览器类型（如：Chrome、Firefox、Safari）
    /// </summary>
    [SugarColumn(ColumnName = "browser_type", ColumnDescription = "浏览器类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BrowserType { get; set; }

    /// <summary>
    /// 操作系统（如：Windows、macOS、Linux、iOS、Android）
    /// </summary>
    [SugarColumn(ColumnName = "operating_system", ColumnDescription = "操作系统", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OperatingSystem { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    [SugarColumn(ColumnName = "connect_time", ColumnDescription = "连接时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ConnectTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 最后活动时间
    /// </summary>
    [SugarColumn(ColumnName = "last_active_time", ColumnDescription = "最后活动时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastActiveTime { get; set; }

    /// <summary>
    /// 断开时间
    /// </summary>
    [SugarColumn(ColumnName = "disconnect_time", ColumnDescription = "断开时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DisconnectTime { get; set; }

    /// <summary>
    /// 连接时长（秒，从连接到断开的时长）
    /// </summary>
    [SugarColumn(ColumnName = "connection_duration", ColumnDescription = "连接时长", ColumnDataType = "int", IsNullable = true)]
    public int? ConnectionDuration { get; set; }
}
