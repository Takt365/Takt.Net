// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤设备表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备表Dto
/// </summary>
public partial class TaktAttendanceDeviceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = string.Empty;
    }

    /// <summary>
    /// 考勤设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceDeviceId { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }
    /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }
    /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }
    /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }
    /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }
    /// <summary>
    /// 启用推送
    /// </summary>
    public int IsPushEnabled { get; set; }
    /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }
    /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }
}

/// <summary>
/// 考勤设备表查询DTO
/// </summary>
public partial class TaktAttendanceDeviceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 考勤设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceDeviceId { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    public string? DeviceCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? DeviceName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }
    /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }
    /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }
    /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }
    /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int? DeviceStatus { get; set; }
    /// <summary>
    /// 启用推送
    /// </summary>
    public int? IsPushEnabled { get; set; }
    /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

    /// <summary>
    /// 上次拉取时间开始时间
    /// </summary>
    public DateTime? LastPullAtStart { get; set; }
    /// <summary>
    /// 上次拉取时间结束时间
    /// </summary>
    public DateTime? LastPullAtEnd { get; set; }
    /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }

    /// <summary>
    /// 上次推送时间开始时间
    /// </summary>
    public DateTime? LastPushAtStart { get; set; }
    /// <summary>
    /// 上次推送时间结束时间
    /// </summary>
    public DateTime? LastPushAtEnd { get; set; }

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
/// Takt创建考勤设备表DTO
/// </summary>
public partial class TaktAttendanceDeviceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceCreateDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

        /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }

        /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

        /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

        /// <summary>
    /// 启用推送
    /// </summary>
    public int IsPushEnabled { get; set; }

        /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

        /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }

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
/// Takt更新考勤设备表DTO
/// </summary>
public partial class TaktAttendanceDeviceUpdateDto : TaktAttendanceDeviceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceUpdateDto()
    {
    }

        /// <summary>
    /// 考勤设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceDeviceId { get; set; }
}

/// <summary>
/// 考勤设备表设备状态DTO
/// </summary>
public partial class TaktAttendanceDeviceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceStatusDto()
    {
    }

        /// <summary>
    /// 考勤设备表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceDeviceId { get; set; }

    /// <summary>
    /// 设备状态（0=禁用，1=启用）
    /// </summary>
    public int DeviceStatus { get; set; }
}

/// <summary>
/// 考勤设备表导入模板DTO
/// </summary>
public partial class TaktAttendanceDeviceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceTemplateDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

        /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }

        /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

        /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

        /// <summary>
    /// 启用推送
    /// </summary>
    public int IsPushEnabled { get; set; }

        /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

        /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }

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
/// 考勤设备表导入DTO
/// </summary>
public partial class TaktAttendanceDeviceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceImportDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

        /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }

        /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

        /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

        /// <summary>
    /// 启用推送
    /// </summary>
    public int IsPushEnabled { get; set; }

        /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

        /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }

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
/// 考勤设备表导出DTO
/// </summary>
public partial class TaktAttendanceDeviceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceExportDto()
    {
        CreatedAt = DateTime.Now;
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = string.Empty;
    }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string DeviceCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

        /// <summary>
    /// 设备厂商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 端口
    /// </summary>
    public int? Port { get; set; }

        /// <summary>
    /// 型号
    /// </summary>
    public string? DeviceModel { get; set; }

        /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

        /// <summary>
    /// 设备配置
    /// </summary>
    public string? ConfigJson { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

        /// <summary>
    /// 启用推送
    /// </summary>
    public int IsPushEnabled { get; set; }

        /// <summary>
    /// 上次拉取时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

        /// <summary>
    /// 上次推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}