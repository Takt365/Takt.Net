// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.SignalR
// 文件名称：TaktOnlineDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线用户DTO，包含在线用户相关的数据传输对象（查询、创建、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.SignalR;

/// <summary>
/// Takt在线用户DTO
/// </summary>
public class TaktOnlineDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineDto()
    {
        ConnectionId = string.Empty;
        UserName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 在线用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// SignalR连接ID（唯一索引）
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 在线状态（0=在线，1=离线，2=离开）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 连接IP地址
    /// </summary>
    public string? ConnectIp { get; set; }

    /// <summary>
    /// 连接地点
    /// </summary>
    public string? ConnectLocation { get; set; }

    /// <summary>
    /// User-Agent（浏览器信息）
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 设备类型（如：PC、Mobile、Tablet）
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 浏览器类型（如：Chrome、Firefox、Safari）
    /// </summary>
    public string? BrowserType { get; set; }

    /// <summary>
    /// 操作系统（如：Windows、macOS、Linux、iOS、Android）
    /// </summary>
    public string? OperatingSystem { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime ConnectTime { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime? LastActiveTime { get; set; }

    /// <summary>
    /// 断开时间
    /// </summary>
    public DateTime? DisconnectTime { get; set; }

    /// <summary>
    /// 连接时长（秒，从连接到断开的时长）
    /// </summary>
    public int? ConnectionDuration { get; set; }

    // ----- 审计字段（与 TaktEntityBase 一致，统一放在最后） -----

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt在线用户查询DTO
/// </summary>
public class TaktOnlineQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、连接ID、连接IP、连接地点中模糊查询

    /// <summary>
    /// 连接ID
    /// </summary>
    public string? ConnectionId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 在线状态（0=在线，1=离线，2=离开）
    /// </summary>
    public int? OnlineStatus { get; set; }

    /// <summary>
    /// 连接时间开始
    /// </summary>
    public DateTime? ConnectTimeStart { get; set; }

    /// <summary>
    /// 连接时间结束
    /// </summary>
    public DateTime? ConnectTimeEnd { get; set; }
}

/// <summary>
/// Takt创建在线用户DTO
/// </summary>
public class TaktOnlineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineCreateDto()
    {
        ConnectionId = string.Empty;
        UserName = string.Empty;
    }

    /// <summary>
    /// SignalR连接ID（唯一索引）
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 在线状态（0=在线，1=离线，2=离开）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 连接IP地址
    /// </summary>
    public string? ConnectIp { get; set; }

    /// <summary>
    /// 连接地点
    /// </summary>
    public string? ConnectLocation { get; set; }

    /// <summary>
    /// User-Agent（浏览器信息）
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// 设备类型（如：PC、Mobile、Tablet）
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 浏览器类型（如：Chrome、Firefox、Safari）
    /// </summary>
    public string? BrowserType { get; set; }

    /// <summary>
    /// 操作系统（如：Windows、macOS、Linux、iOS、Android）
    /// </summary>
    public string? OperatingSystem { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime? ConnectTime { get; set; }
}

/// <summary>
/// Takt在线用户状态DTO
/// </summary>
public class TaktOnlineStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineStatusDto()
    {
    }

    /// <summary>
    /// 在线用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// 在线状态（0=在线，1=离线，2=离开）
    /// </summary>
    public int OnlineStatus { get; set; }
}

/// <summary>
/// Takt在线用户最后活动时间DTO
/// </summary>
public class TaktOnlineLastDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineLastDto()
    {
        ConnectionId = string.Empty;
    }

    /// <summary>
    /// 连接ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActiveTime { get; set; }
}

/// <summary>
/// Takt在线用户连接时长DTO
/// </summary>
public class TaktOnlineDurationDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineDurationDto()
    {
    }

    /// <summary>
    /// 在线用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// 断开时间
    /// </summary>
    public DateTime DisconnectTime { get; set; }

    /// <summary>
    /// 连接时长（秒，从连接到断开的时长）
    /// </summary>
    public int ConnectionDuration { get; set; }
}

/// <summary>
/// Takt在线用户导出DTO
/// </summary>
public class TaktOnlineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineExportDto()
    {
        UserName = string.Empty;
        OnlineStatus = string.Empty;
        ConnectIp = string.Empty;
        ConnectLocation = string.Empty;
        DeviceType = string.Empty;
        BrowserType = string.Empty;
        OperatingSystem = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 在线状态
    /// </summary>
    public string OnlineStatus { get; set; }

    /// <summary>
    /// 连接IP地址
    /// </summary>
    public string ConnectIp { get; set; }

    /// <summary>
    /// 连接地点
    /// </summary>
    public string ConnectLocation { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public string BrowserType { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string OperatingSystem { get; set; }

    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime ConnectTime { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime? LastActiveTime { get; set; }

    /// <summary>
    /// 断开时间
    /// </summary>
    public DateTime? DisconnectTime { get; set; }

    /// <summary>
    /// 连接时长（秒）
    /// </summary>
    public int? ConnectionDuration { get; set; }
}
