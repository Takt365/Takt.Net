// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDevice.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备主数据（与源记录下载关联）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备（指纹/人脸机等，用于标识源记录来源）。
/// </summary>
[SugarTable("takt_humanresource_attendance_device", "考勤设备表")]
[SugarIndex("ix_takt_humanresource_attendance_device_code", nameof(DeviceCode), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_device_type", nameof(DeviceType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_device_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_device_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceDevice : TaktEntityBase
{
    /// <summary>
    /// 设备编码（业务唯一）
    /// </summary>
    [SugarColumn(ColumnName = "device_code", ColumnDescription = "设备编码", ColumnDataType = "nvarchar", Length = 64, IsNullable = false)]
    public string DeviceCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "device_name", ColumnDescription = "设备名称", ColumnDataType = "nvarchar", Length = 128, IsNullable = false)]
    public string DeviceName { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型（如 Generic/DingTalk/WeCom/Hikvision）
    /// </summary>
    [SugarColumn(ColumnName = "device_type", ColumnDescription = "设备类型", ColumnDataType = "nvarchar", Length = 64, IsNullable = false, DefaultValue = "Generic")]
    public string DeviceType { get; set; } = "Generic";

    /// <summary>
    /// 设备厂商
    /// </summary>
    [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "设备厂商", ColumnDataType = "nvarchar", Length = 64, IsNullable = true)]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// IP 地址
    /// </summary>
    [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", ColumnDataType = "nvarchar", Length = 64, IsNullable = true)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 通讯端口
    /// </summary>
    [SugarColumn(ColumnName = "port", ColumnDescription = "端口", ColumnDataType = "int", IsNullable = true)]
    public int? Port { get; set; }

    /// <summary>
    /// 设备型号/固件说明
    /// </summary>
    [SugarColumn(ColumnName = "device_model", ColumnDescription = "型号", ColumnDataType = "nvarchar", Length = 128, IsNullable = true)]
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 设备接入密钥（用于签名校验）
    /// </summary>
    [SugarColumn(ColumnName = "api_secret", ColumnDescription = "接入密钥", ColumnDataType = "nvarchar", Length = 256, IsNullable = true)]
    public string? ApiSecret { get; set; }

    /// <summary>
    /// 设备扩展配置 JSON
    /// </summary>
    [SugarColumn(ColumnName = "config_json", ColumnDescription = "设备配置", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ConfigJson { get; set; }

    /// <summary>
    /// 设备状态：0=停用 1=正常 2=故障
    /// </summary>
    [SugarColumn(ColumnName = "device_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int DeviceStatus { get; set; } = 1;

    /// <summary>
    /// 是否启用推送接收
    /// </summary>
    [SugarColumn(ColumnName = "is_push_enabled", ColumnDescription = "启用推送", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsPushEnabled { get; set; } = 1;

    /// <summary>
    /// 上次从设备拉取原始记录时间
    /// </summary>
    [SugarColumn(ColumnName = "last_pull_at", ColumnDescription = "上次拉取时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastPullAt { get; set; }

    /// <summary>
    /// 上次接收设备推送时间
    /// </summary>
    [SugarColumn(ColumnName = "last_push_at", ColumnDescription = "上次推送时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastPushAt { get; set; }
}
