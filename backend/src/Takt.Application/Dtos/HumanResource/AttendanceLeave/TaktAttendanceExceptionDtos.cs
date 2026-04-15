// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionDtos.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常 DTO，包含查询、创建、更新、模板、导入、导出（与 TaktWorkShiftDtos 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常 DTO（列表/详情）
/// </summary>
public class TaktAttendanceExceptionDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionDto()
    {
        Summary = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 异常记录 ID（适配实体主键 Id，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ExceptionId { get; set; }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 异常归属日期（日期部分有效）
    /// </summary>
    public DateTime ExceptionDate { get; set; }

    /// <summary>
    /// 异常类型（1=上班缺卡 2=下班缺卡 3=迟到 4=早退 5=旷工 9=其他）
    /// </summary>
    public int ExceptionType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 处理状态（0=待处理 1=已处理 2=已忽略）
    /// </summary>
    public int HandleStatus { get; set; }
}

/// <summary>
/// 考勤异常分页查询 DTO。
/// 继承的 <see cref="TaktPagedQuery.KeyWords"/> 在应用服务查询表达式中用于匹配说明（Summary）、备注。
/// </summary>
public class TaktAttendanceExceptionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionQueryDto()
    {
    }

    /// <summary>
    /// 员工 ID（精确）
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 异常日期起（含，按日期部分比较）
    /// </summary>
    public DateTime? ExceptionDateFrom { get; set; }

    /// <summary>
    /// 异常日期止（含，按日期部分比较）
    /// </summary>
    public DateTime? ExceptionDateTo { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public int? ExceptionType { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    public int? HandleStatus { get; set; }
}

/// <summary>
/// 创建考勤异常 DTO
/// </summary>
public class TaktAttendanceExceptionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionCreateDto()
    {
        Summary = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 异常归属日期
    /// </summary>
    public DateTime ExceptionDate { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public int ExceptionType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    public int HandleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 更新考勤异常 DTO
/// </summary>
public class TaktAttendanceExceptionUpdateDto : TaktAttendanceExceptionCreateDto
{
    /// <summary>
    /// 异常记录 ID（适配实体主键 Id）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ExceptionId { get; set; }
}

/// <summary>
/// 考勤异常导入模板 DTO（Excel 列）
/// </summary>
public class TaktAttendanceExceptionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionTemplateDto()
    {
        Summary = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 异常归属日期
    /// </summary>
    public DateTime ExceptionDate { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public int ExceptionType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    public int HandleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤异常导入行 DTO（与模板列一致）
/// </summary>
public class TaktAttendanceExceptionImportDto : TaktAttendanceExceptionTemplateDto
{
}

/// <summary>
/// 考勤异常导出 DTO（Excel 列）
/// </summary>
public class TaktAttendanceExceptionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionExportDto()
    {
        Summary = string.Empty;
    }

    /// <summary>
    /// 员工 ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 异常归属日期
    /// </summary>
    public DateTime ExceptionDate { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public int ExceptionType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 处理状态
    /// </summary>
    public int HandleStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
