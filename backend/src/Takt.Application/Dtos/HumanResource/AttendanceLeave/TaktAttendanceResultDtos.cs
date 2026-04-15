// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResultDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤日结结果 DTO（列表/详情；与领域实体 TaktAttendanceResult 字段含义对齐）
/// </summary>
public class TaktAttendanceResultDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultDto()
    {
        ConfigId = "0";
    }

    /// <summary>
    /// 结果记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ResultId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 归属考勤日期（日期部分有效）
    /// </summary>
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 排班 ID（可选，无排班时为空）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

    /// <summary>
    /// 出勤状态（0=正常 1=迟到 2=早退 3=缺卡 4=旷工 5=加班，与实体注释一致）
    /// </summary>
    public int AttendanceStatus { get; set; }

    /// <summary>
    /// 首次上班时间
    /// </summary>
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 末次下班时间
    /// </summary>
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 计薪/计出勤分钟数
    /// </summary>
    public int WorkMinutes { get; set; }

    /// <summary>
    /// 结果计算完成时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }
}

/// <summary>
/// 考勤结果分页查询 DTO
/// </summary>
public class TaktAttendanceResultQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultQueryDto()
    {
    }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 考勤日期起（含，按日期部分比较）
    /// </summary>
    public DateTime? AttendanceDateFrom { get; set; }

    /// <summary>
    /// 考勤日期止（含，按日期部分比较）
    /// </summary>
    public DateTime? AttendanceDateTo { get; set; }

    /// <summary>
    /// 出勤状态
    /// </summary>
    public int? AttendanceStatus { get; set; }
}

/// <summary>
/// 创建考勤日结结果 DTO
/// </summary>
public class TaktAttendanceResultCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultCreateDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 归属考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 排班 ID（可选）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

    /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

    /// <summary>
    /// 首次上班时间
    /// </summary>
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 末次下班时间
    /// </summary>
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 计薪/计出勤分钟数
    /// </summary>
    public int WorkMinutes { get; set; }

    /// <summary>
    /// 结果计算完成时间（可由日结任务写入）
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新考勤日结结果 DTO
/// </summary>
public class TaktAttendanceResultUpdateDto : TaktAttendanceResultCreateDto
{
    /// <summary>
    /// 结果记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ResultId { get; set; }
}

/// <summary>
/// 考勤结果导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendanceResultTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultTemplateDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 归属考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 排班 ID（可选）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

    /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

    /// <summary>
    /// 首次上班时间
    /// </summary>
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 末次下班时间
    /// </summary>
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 计薪/计出勤分钟数
    /// </summary>
    public int WorkMinutes { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤结果导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendanceResultImportDto : TaktAttendanceResultTemplateDto
{
}

/// <summary>
/// 考勤结果导出 DTO（Excel 列）
/// </summary>
public class TaktAttendanceResultExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultExportDto()
    {
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 归属考勤日期
    /// </summary>
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 排班 ID（可选）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

    /// <summary>
    /// 出勤状态
    /// </summary>
    public int AttendanceStatus { get; set; }

    /// <summary>
    /// 首次上班时间
    /// </summary>
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 末次下班时间
    /// </summary>
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 计薪/计出勤分钟数
    /// </summary>
    public int WorkMinutes { get; set; }

    /// <summary>
    /// 结果计算完成时间
    /// </summary>
    public DateTime? CalculatedAt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
