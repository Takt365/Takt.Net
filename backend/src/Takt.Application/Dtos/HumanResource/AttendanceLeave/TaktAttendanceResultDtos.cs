// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResultDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤结果表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤结果表Dto
/// </summary>
public partial class TaktAttendanceResultDto : TaktDtosEntityBase
{
    /// <summary>
    /// 考勤结果表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceResultId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }
    /// <summary>
    /// 排班ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }
    /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }
    /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }
    /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }
    /// <summary>
    /// 出勤分钟
    /// </summary>
    public int WorkMinutes { get; set; }
    /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }
}

/// <summary>
/// 考勤结果表查询DTO
/// </summary>
public partial class TaktAttendanceResultQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 考勤结果表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceResultId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime? AttendanceDate { get; set; }

    /// <summary>
    /// 考勤日期开始时间
    /// </summary>
    public DateTime? AttendanceDateStart { get; set; }
    /// <summary>
    /// 考勤日期结束时间
    /// </summary>
    public DateTime? AttendanceDateEnd { get; set; }
    /// <summary>
    /// 排班ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }
    /// <summary>
    /// 出勤状态
    /// </summary>
    public int? AttendanceStatus { get; set; }
    /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 上班打卡开始时间
    /// </summary>
    public DateTime? FirstInTimeStart { get; set; }
    /// <summary>
    /// 上班打卡结束时间
    /// </summary>
    public DateTime? FirstInTimeEnd { get; set; }
    /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 下班打卡开始时间
    /// </summary>
    public DateTime? LastOutTimeStart { get; set; }
    /// <summary>
    /// 下班打卡结束时间
    /// </summary>
    public DateTime? LastOutTimeEnd { get; set; }
    /// <summary>
    /// 出勤分钟
    /// </summary>
    public int? WorkMinutes { get; set; }
    /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

    /// <summary>
    /// 计算时间开始时间
    /// </summary>
    public DateTime? CalculatedAtStart { get; set; }
    /// <summary>
    /// 计算时间结束时间
    /// </summary>
    public DateTime? CalculatedAtEnd { get; set; }

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
/// Takt创建考勤结果表DTO
/// </summary>
public partial class TaktAttendanceResultCreateDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

        /// <summary>
    /// 排班ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

        /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

        /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }

        /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }

        /// <summary>
    /// 出勤分钟
    /// </summary>
    public int WorkMinutes { get; set; }

        /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

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
/// Takt更新考勤结果表DTO
/// </summary>
public partial class TaktAttendanceResultUpdateDto : TaktAttendanceResultCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultUpdateDto()
    {
    }

        /// <summary>
    /// 考勤结果表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceResultId { get; set; }
}

/// <summary>
/// 考勤结果表出勤状态DTO
/// </summary>
public partial class TaktAttendanceResultAttendanceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultAttendanceStatusDto()
    {
    }

        /// <summary>
    /// 考勤结果表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceResultId { get; set; }

    /// <summary>
    /// 出勤状态（0=禁用，1=启用）
    /// </summary>
    public int AttendanceStatus { get; set; }
}

/// <summary>
/// 考勤结果表导入模板DTO
/// </summary>
public partial class TaktAttendanceResultTemplateDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

        /// <summary>
    /// 排班ID
    /// </summary>
    public long? ShiftScheduleId { get; set; }

        /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

        /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }

        /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }

        /// <summary>
    /// 出勤分钟
    /// </summary>
    public int WorkMinutes { get; set; }

        /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

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
/// 考勤结果表导入DTO
/// </summary>
public partial class TaktAttendanceResultImportDto
{
        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

        /// <summary>
    /// 排班ID
    /// </summary>
    public long? ShiftScheduleId { get; set; }

        /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

        /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }

        /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }

        /// <summary>
    /// 出勤分钟
    /// </summary>
    public int WorkMinutes { get; set; }

        /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

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
/// 考勤结果表导出DTO
/// </summary>
public partial class TaktAttendanceResultExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

        /// <summary>
    /// 排班ID
    /// </summary>
    public long? ShiftScheduleId { get; set; }

        /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

        /// <summary>
    /// 上班打卡
    /// </summary>
    public DateTime? FirstInTime { get; set; }

        /// <summary>
    /// 下班打卡
    /// </summary>
    public DateTime? LastOutTime { get; set; }

        /// <summary>
    /// 出勤分钟
    /// </summary>
    public int WorkMinutes { get; set; }

        /// <summary>
    /// 计算时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}