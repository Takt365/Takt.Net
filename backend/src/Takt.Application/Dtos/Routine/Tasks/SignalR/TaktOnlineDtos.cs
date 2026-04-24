// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.SignalR
// 文件名称：TaktOnlineDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：在线用户表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.SignalR;

/// <summary>
/// 在线用户表Dto
/// </summary>
public partial class TaktOnlineDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineDto()
    {
        ConnectionId = string.Empty;
        UserName = string.Empty;
    }

    /// <summary>
    /// 在线用户表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }
    /// <summary>
    /// 在线状态
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
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }
    /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }
    /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }
    /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }
    /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }
    /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }
    /// <summary>
    /// 操作系统
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
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }
}

/// <summary>
/// 在线用户表查询DTO
/// </summary>
public partial class TaktOnlineQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 在线用户表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// SignalR连接ID
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
    /// 在线状态
    /// </summary>
    public int? OnlineStatus { get; set; }
    /// <summary>
    /// 连接IP地址
    /// </summary>
    public string? ConnectIp { get; set; }
    /// <summary>
    /// 连接地点
    /// </summary>
    public string? ConnectLocation { get; set; }
    /// <summary>
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }
    /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }
    /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }
    /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }
    /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }
    /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }
    /// <summary>
    /// 操作系统
    /// </summary>
    public string? OperatingSystem { get; set; }
    /// <summary>
    /// 连接时间
    /// </summary>
    public DateTime? ConnectTime { get; set; }

    /// <summary>
    /// 连接时间开始时间
    /// </summary>
    public DateTime? ConnectTimeStart { get; set; }
    /// <summary>
    /// 连接时间结束时间
    /// </summary>
    public DateTime? ConnectTimeEnd { get; set; }
    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime? LastActiveTime { get; set; }

    /// <summary>
    /// 最后活动时间开始时间
    /// </summary>
    public DateTime? LastActiveTimeStart { get; set; }
    /// <summary>
    /// 最后活动时间结束时间
    /// </summary>
    public DateTime? LastActiveTimeEnd { get; set; }
    /// <summary>
    /// 断开时间
    /// </summary>
    public DateTime? DisconnectTime { get; set; }

    /// <summary>
    /// 断开时间开始时间
    /// </summary>
    public DateTime? DisconnectTimeStart { get; set; }
    /// <summary>
    /// 断开时间结束时间
    /// </summary>
    public DateTime? DisconnectTimeEnd { get; set; }
    /// <summary>
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建在线用户表DTO
/// </summary>
public partial class TaktOnlineCreateDto
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
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

        /// <summary>
    /// 在线状态
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
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }

        /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }

        /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }

        /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

        /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }

        /// <summary>
    /// 操作系统
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
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新在线用户表DTO
/// </summary>
public partial class TaktOnlineUpdateDto : TaktOnlineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineUpdateDto()
    {
    }

        /// <summary>
    /// 在线用户表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }
}

/// <summary>
/// 在线用户表在线状态DTO
/// </summary>
public partial class TaktOnlineStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineStatusDto()
    {
    }

        /// <summary>
    /// 在线用户表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OnlineId { get; set; }

    /// <summary>
    /// 在线状态（0=禁用，1=启用）
    /// </summary>
    public int OnlineStatus { get; set; }
}

/// <summary>
/// 在线用户表导入模板DTO
/// </summary>
public partial class TaktOnlineTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineTemplateDto()
    {
        ConnectionId = string.Empty;
        UserName = string.Empty;
    }

        /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

        /// <summary>
    /// 在线状态
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
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }

        /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }

        /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }

        /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

        /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }

        /// <summary>
    /// 操作系统
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
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 在线用户表导入DTO
/// </summary>
public partial class TaktOnlineImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineImportDto()
    {
        ConnectionId = string.Empty;
        UserName = string.Empty;
    }

        /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

        /// <summary>
    /// 在线状态
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
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }

        /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }

        /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }

        /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

        /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }

        /// <summary>
    /// 操作系统
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
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 在线用户表导出DTO
/// </summary>
public partial class TaktOnlineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOnlineExportDto()
    {
        CreatedAt = DateTime.Now;
        ConnectionId = string.Empty;
        UserName = string.Empty;
    }

        /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; }

        /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

        /// <summary>
    /// 在线状态
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
    /// 连接国家
    /// </summary>
    public string? ConnectCountry { get; set; }

        /// <summary>
    /// 连接省份
    /// </summary>
    public string? ConnectProvince { get; set; }

        /// <summary>
    /// 连接城市
    /// </summary>
    public string? ConnectCity { get; set; }

        /// <summary>
    /// 连接ISP
    /// </summary>
    public string? ConnectIsp { get; set; }

        /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }

        /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? BrowserType { get; set; }

        /// <summary>
    /// 操作系统
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
    /// 连接时长
    /// </summary>
    public int? ConnectionDuration { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}