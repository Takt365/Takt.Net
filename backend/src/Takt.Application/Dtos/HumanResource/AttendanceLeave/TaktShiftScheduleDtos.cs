// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktShiftScheduleDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：排班计划 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 排班计划 DTO（列表/详情；按排班类别区分部门/人员）
/// </summary>
public class TaktShiftScheduleDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleDto()
    {
        ConfigId = "0";
    }

    /// <summary>
    /// 排班记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftScheduleId { get; set; }

    /// <summary>
    /// 排班类别（0=部门，1=人员）
    /// </summary>
    public int ScheduleType { get; set; }

    /// <summary>
    /// 部门 ID（ScheduleType=0 时使用）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID（ScheduleType=1 时使用）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期（日期部分有效）
    /// </summary>
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次 ID（对应 TaktWorkShift）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }

    /// <summary>
    /// 班次名称（列表展示用，非表字段，服务层填充）
    /// </summary>
    public string? ShiftName { get; set; }
}

/// <summary>
/// 排班计划分页查询 DTO。
/// 继承的 <see cref="TaktPagedQuery.KeyWords"/> 在应用服务查询表达式中用于匹配备注（Remark）。
/// </summary>
public class TaktShiftScheduleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleQueryDto()
    {
    }

    /// <summary>
    /// 排班类别（精确）
    /// </summary>
    public int? ScheduleType { get; set; }

    /// <summary>
    /// 部门 ID（精确）
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 班次 ID（精确）
    /// </summary>
    public long? ShiftId { get; set; }

    /// <summary>
    /// 排班日期起（含，按日期部分比较）
    /// </summary>
    public DateTime? ScheduleDateFrom { get; set; }

    /// <summary>
    /// 排班日期止（含，按日期部分比较）
    /// </summary>
    public DateTime? ScheduleDateTo { get; set; }
}

/// <summary>
/// 创建排班计划 DTO
/// </summary>
public class TaktShiftScheduleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleCreateDto()
    {
    }

    /// <summary>
    /// 排班类别（0=部门，1=人员）
    /// </summary>
    public int ScheduleType { get; set; } = 1;

    /// <summary>
    /// 部门 ID（ScheduleType=0 时必填）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID（ScheduleType=1 时必填）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新排班计划 DTO
/// </summary>
public class TaktShiftScheduleUpdateDto : TaktShiftScheduleCreateDto
{
    /// <summary>
    /// 排班记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftScheduleId { get; set; }
}

/// <summary>
/// 排班计划导出 DTO（Excel 列）
/// </summary>
public class TaktShiftScheduleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleExportDto()
    {
    }

    /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }

    /// <summary>
    /// 部门 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }

    /// <summary>
    /// 班次名称（导出展示，服务层可填充）
    /// </summary>
    public string? ShiftName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// 排班导入模板 DTO（Excel 列；班次以编码解析为班次 ID）
/// </summary>
public class TaktShiftScheduleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleTemplateDto()
    {
        ShiftCode = string.Empty;
    }

    /// <summary>
    /// 排班类别（0=部门，1=人员）
    /// </summary>
    public int ScheduleType { get; set; } = 1;

    /// <summary>
    /// 部门 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次编码（与 TaktWorkShift.shift_code 对应）
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 排班导入行 DTO（与模板列一致）
/// </summary>
public class TaktShiftScheduleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleImportDto()
    {
        ShiftCode = string.Empty;
    }

    /// <summary>
    /// 排班类别（0=部门，1=人员）
    /// </summary>
    public int ScheduleType { get; set; } = 1;

    /// <summary>
    /// 部门 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次编码
    /// </summary>
    public string ShiftCode { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}
