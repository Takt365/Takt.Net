// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤异常表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常表Dto
/// </summary>
public partial class TaktAttendanceExceptionDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionDto()
    {
        Summary = string.Empty;
    }

    /// <summary>
    /// 考勤异常表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceExceptionId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 异常日期
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
}

/// <summary>
/// 考勤异常表查询DTO
/// </summary>
public partial class TaktAttendanceExceptionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 考勤异常表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceExceptionId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 异常日期
    /// </summary>
    public DateTime? ExceptionDate { get; set; }

    /// <summary>
    /// 异常日期开始时间
    /// </summary>
    public DateTime? ExceptionDateStart { get; set; }
    /// <summary>
    /// 异常日期结束时间
    /// </summary>
    public DateTime? ExceptionDateEnd { get; set; }
    /// <summary>
    /// 异常类型
    /// </summary>
    public int? ExceptionType { get; set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string? Summary { get; set; }
    /// <summary>
    /// 处理状态
    /// </summary>
    public int? HandleStatus { get; set; }

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
/// Takt创建考勤异常表DTO
/// </summary>
public partial class TaktAttendanceExceptionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionCreateDto()
    {
        Summary = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 异常日期
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新考勤异常表DTO
/// </summary>
public partial class TaktAttendanceExceptionUpdateDto : TaktAttendanceExceptionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionUpdateDto()
    {
    }

        /// <summary>
    /// 考勤异常表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceExceptionId { get; set; }
}

/// <summary>
/// 考勤异常表处理状态DTO
/// </summary>
public partial class TaktAttendanceExceptionHandleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionHandleStatusDto()
    {
    }

        /// <summary>
    /// 考勤异常表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AttendanceExceptionId { get; set; }

    /// <summary>
    /// 处理状态（0=禁用，1=启用）
    /// </summary>
    public int HandleStatus { get; set; }
}

/// <summary>
/// 考勤异常表导入模板DTO
/// </summary>
public partial class TaktAttendanceExceptionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionTemplateDto()
    {
        Summary = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 异常日期
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤异常表导入DTO
/// </summary>
public partial class TaktAttendanceExceptionImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionImportDto()
    {
        Summary = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 异常日期
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 考勤异常表导出DTO
/// </summary>
public partial class TaktAttendanceExceptionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionExportDto()
    {
        CreatedAt = DateTime.Now;
        Summary = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 异常日期
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
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}