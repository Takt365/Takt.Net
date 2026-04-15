// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备 DTO（列表/详情）
/// </summary>
public class TaktAttendanceDeviceDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = "Generic";
        ConfigId = "0";
    }

    /// <summary>
    /// 设备 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeviceId { get; set; }

    /// <summary>
    /// 设备编码（业务唯一）
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
    /// IP 地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态（0=停用 1=正常 2=故障）
    /// </summary>
    public int DeviceStatus { get; set; }

    /// <summary>
    /// 是否启用推送接收
    /// </summary>
    public int IsPushEnabled { get; set; }

    /// <summary>
    /// 上次从设备拉取原始记录时间
    /// </summary>
    public DateTime? LastPullAt { get; set; }

    /// <summary>
    /// 上次接收设备推送时间
    /// </summary>
    public DateTime? LastPushAt { get; set; }
}

/// <summary>
/// 考勤设备分页查询 DTO。
/// 继承的 <see cref="TaktPagedQuery.KeyWords"/> 在应用服务查询表达式中用于匹配设备编码、设备名称（与 DeviceCode、DeviceName 条件可同时使用）。
/// </summary>
public class TaktAttendanceDeviceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceQueryDto()
    {
    }

    /// <summary>
    /// 设备编码（模糊）
    /// </summary>
    public string? DeviceCode { get; set; }

    /// <summary>
    /// 设备名称（模糊）
    /// </summary>
    public string? DeviceName { get; set; }

    /// <summary>
    /// 设备类型（精确）
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 设备品牌（精确：Hikvision / Deli / ZKTeco）
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public int? DeviceStatus { get; set; }
}

/// <summary>
/// 创建考勤设备 DTO
/// </summary>
public class TaktAttendanceDeviceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceCreateDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = "Generic";
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
    /// IP 地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 接入密钥（用于签名校验）
    /// </summary>
    public string? ApiSecret { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态（默认 1=正常）
    /// </summary>
    public int DeviceStatus { get; set; } = 1;

    /// <summary>
    /// 是否启用推送接收（默认 1）
    /// </summary>
    public int IsPushEnabled { get; set; } = 1;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新考勤设备 DTO
/// </summary>
public class TaktAttendanceDeviceUpdateDto : TaktAttendanceDeviceCreateDto
{
    /// <summary>
    /// 设备 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeviceId { get; set; }
}

/// <summary>
/// 考勤设备导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendanceDeviceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceTemplateDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = "Generic";
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
    /// IP 地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

    /// <summary>
    /// 是否启用推送接收
    /// </summary>
    public int IsPushEnabled { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤设备导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendanceDeviceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceImportDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = "Generic";
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
    /// IP 地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 接入密钥
    /// </summary>
    public string? ApiSecret { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

    /// <summary>
    /// 是否启用推送接收
    /// </summary>
    public int IsPushEnabled { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤设备导出 DTO（Excel 列）
/// </summary>
public class TaktAttendanceDeviceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDeviceExportDto()
    {
        DeviceCode = string.Empty;
        DeviceName = string.Empty;
        DeviceType = "Generic";
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
    /// IP 地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

    /// <summary>
    /// 是否启用推送接收
    /// </summary>
    public int IsPushEnabled { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
