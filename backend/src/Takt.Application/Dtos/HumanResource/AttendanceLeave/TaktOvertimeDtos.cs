// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：加班 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 加班 DTO（列表/详情）
/// </summary>
public class TaktOvertimeDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeDto()
    {
        Reason = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 加班记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OvertimeId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期（日期部分有效）
    /// </summary>
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数（可后填）
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 状态（0=草稿 1=已提交 2=已通过 3=已驳回，与实体注释一致）
    /// </summary>
    public int OvertimeStatus { get; set; }
}

/// <summary>
/// 加班分页查询 DTO
/// </summary>
public class TaktOvertimeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeQueryDto()
    {
    }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 加班状态
    /// </summary>
    public int? OvertimeStatus { get; set; }

    /// <summary>
    /// 加班日期起（含，按日期部分比较）
    /// </summary>
    public DateTime? OvertimeDateFrom { get; set; }

    /// <summary>
    /// 加班日期止（含，按日期部分比较）
    /// </summary>
    public DateTime? OvertimeDateTo { get; set; }
}

/// <summary>
/// 创建加班 DTO
/// </summary>
public class TaktOvertimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeCreateDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数（可选）
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新加班 DTO
/// </summary>
public class TaktOvertimeUpdateDto : TaktOvertimeCreateDto
{
    /// <summary>
    /// 加班记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OvertimeId { get; set; }
}

/// <summary>
/// 加班导出 DTO（Excel 列）
/// </summary>
public class TaktOvertimeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeExportDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// 加班导入模板 DTO（Excel 列）
/// </summary>
public class TaktOvertimeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeTemplateDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数（可选）
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 加班导入行 DTO（与模板列一致）
/// </summary>
public class TaktOvertimeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeImportDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数（可选）
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}
