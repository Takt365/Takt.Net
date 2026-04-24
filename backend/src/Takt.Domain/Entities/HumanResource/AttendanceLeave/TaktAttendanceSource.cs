// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSource.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录（从设备下载的原始打卡行，未参与日结计算前写入本表）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤源记录（设备原始数据；可与 <see cref="TaktAttendanceDevice"/> 关联）。
/// </summary>
[SugarTable("takt_humanresource_attendance_source", "考勤源记录表")]
[SugarIndex("ix_takt_humanresource_attendance_source_device_id", nameof(DeviceId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_source_punch_time", nameof(RawPunchTime), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_source_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_source_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceSource : TaktEntityBase
{
    /// <summary>
    /// 设备 ID
    /// </summary>
    [SugarColumn(ColumnName = "device_id", ColumnDescription = "设备ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeviceId { get; set; }

    /// <summary>
    /// 员工 ID（已解析时填写；未解析可为 0）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 设备登记号（机内用户号）
    /// </summary>
    [SugarColumn(ColumnName = "enroll_number", ColumnDescription = "登记号", ColumnDataType = "nvarchar", Length = 64, IsNullable = false, DefaultValue = "")]
    public string EnrollNumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备原始打卡时间
    /// </summary>
    [SugarColumn(ColumnName = "raw_punch_time", ColumnDescription = "原始打卡时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime RawPunchTime { get; set; }

    /// <summary>
    /// 验证方式（0=未知 1=指纹 2=人脸 3=密码 4=卡）
    /// </summary>
    [SugarColumn(ColumnName = "verify_mode", ColumnDescription = "验证方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int VerifyMode { get; set; }

    /// <summary>
    /// 设备侧记录唯一键/指纹（用于去重，可为空）
    /// </summary>
    [SugarColumn(ColumnName = "external_record_key", ColumnDescription = "外部记录键", ColumnDataType = "nvarchar", Length = 128, IsNullable = true)]
    public string? ExternalRecordKey { get; set; }

    /// <summary>
    /// 下载批次号
    /// </summary>
    [SugarColumn(ColumnName = "download_batch_no", ColumnDescription = "下载批次", ColumnDataType = "nvarchar", Length = 64, IsNullable = true)]
    public string? DownloadBatchNo { get; set; }

    /// <summary>
    /// 原始报文 JSON（可选）
    /// </summary>
    [SugarColumn(ColumnName = "raw_payload_json", ColumnDescription = "原始JSON", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? RawPayloadJson { get; set; }
}
