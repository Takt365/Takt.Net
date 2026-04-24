// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤源记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤源记录表Dto
/// </summary>
public partial class TaktAttendanceSourceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceDto()
    {
        EnrollNumber = string.Empty;
    }

    /// <summary>
    /// 考勤源记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSourceId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeviceId { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 登记号
    /// </summary>
    public string EnrollNumber { get; set; }
    /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }
    /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }
    /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }
    /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }
    /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }
}

/// <summary>
/// 考勤源记录表查询DTO
/// </summary>
public partial class TaktAttendanceSourceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 考勤源记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSourceId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeviceId { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 登记号
    /// </summary>
    public string? EnrollNumber { get; set; }
    /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime? RawPunchTime { get; set; }

    /// <summary>
    /// 原始打卡时间开始时间
    /// </summary>
    public DateTime? RawPunchTimeStart { get; set; }
    /// <summary>
    /// 原始打卡时间结束时间
    /// </summary>
    public DateTime? RawPunchTimeEnd { get; set; }
    /// <summary>
    /// 验证方式
    /// </summary>
    public int? VerifyMode { get; set; }
    /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }
    /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }
    /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }

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
/// Takt创建考勤源记录表DTO
/// </summary>
public partial class TaktAttendanceSourceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceCreateDto()
    {
        EnrollNumber = string.Empty;
    }

        /// <summary>
    /// 设备ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeviceId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 登记号
    /// </summary>
    public string EnrollNumber { get; set; }

        /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

        /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

        /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }

        /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }

        /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }

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
/// Takt更新考勤源记录表DTO
/// </summary>
public partial class TaktAttendanceSourceUpdateDto : TaktAttendanceSourceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceUpdateDto()
    {
    }

        /// <summary>
    /// 考勤源记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceSourceId { get; set; }
}

/// <summary>
/// 考勤源记录表导入模板DTO
/// </summary>
public partial class TaktAttendanceSourceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceTemplateDto()
    {
        EnrollNumber = string.Empty;
    }

        /// <summary>
    /// 设备ID
    /// </summary>
    public long DeviceId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 登记号
    /// </summary>
    public string EnrollNumber { get; set; }

        /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

        /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

        /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }

        /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }

        /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }

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
/// 考勤源记录表导入DTO
/// </summary>
public partial class TaktAttendanceSourceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceImportDto()
    {
        EnrollNumber = string.Empty;
    }

        /// <summary>
    /// 设备ID
    /// </summary>
    public long DeviceId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 登记号
    /// </summary>
    public string EnrollNumber { get; set; }

        /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

        /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

        /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }

        /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }

        /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }

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
/// 考勤源记录表导出DTO
/// </summary>
public partial class TaktAttendanceSourceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourceExportDto()
    {
        CreatedAt = DateTime.Now;
        EnrollNumber = string.Empty;
    }

        /// <summary>
    /// 设备ID
    /// </summary>
    public long DeviceId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 登记号
    /// </summary>
    public string EnrollNumber { get; set; }

        /// <summary>
    /// 原始打卡时间
    /// </summary>
    public DateTime RawPunchTime { get; set; }

        /// <summary>
    /// 验证方式
    /// </summary>
    public int VerifyMode { get; set; }

        /// <summary>
    /// 外部记录键
    /// </summary>
    public string? ExternalRecordKey { get; set; }

        /// <summary>
    /// 下载批次
    /// </summary>
    public string? DownloadBatchNo { get; set; }

        /// <summary>
    /// 原始JSON
    /// </summary>
    public string? RawPayloadJson { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}