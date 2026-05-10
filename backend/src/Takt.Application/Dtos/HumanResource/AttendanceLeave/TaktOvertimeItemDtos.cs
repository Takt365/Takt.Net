// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItemDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：加班明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 加班明细表Dto
/// </summary>
public partial class TaktOvertimeItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemDto()
    {
        EmployeeName = string.Empty;
    }

    /// <summary>
    /// 加班明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeItemId { get; set; } = 0;

    /// <summary>
    /// 加班申请单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }
    /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }
    /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班主表（导航属性）
    /// </summary>
    public TaktOvertimeDto? Overtime { get; set; }
}

/// <summary>
/// 加班明细表查询DTO
/// </summary>
public partial class TaktOvertimeItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 加班申请单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? OvertimeId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 员工姓名
    /// </summary>
    public string? EmployeeName { get; set; }
    /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal? PlannedHours { get; set; }
    /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建加班明细表DTO
/// </summary>
public partial class TaktOvertimeItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemCreateDto()
    {
        EmployeeName = string.Empty;
    }

        /// <summary>
    /// 加班申请单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

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
/// Takt更新加班明细表DTO
/// </summary>
public partial class TaktOvertimeItemUpdateDto : TaktOvertimeItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemUpdateDto()
    {
    }

        /// <summary>
    /// 加班明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeItemId { get; set; } = 0;
}

/// <summary>
/// 加班明细表导入模板DTO
/// </summary>
public partial class TaktOvertimeItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemTemplateDto()
    {
        EmployeeName = string.Empty;
    }

        /// <summary>
    /// 加班申请单ID
    /// </summary>
    public long OvertimeId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

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
/// 加班明细表导入DTO
/// </summary>
public partial class TaktOvertimeItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemImportDto()
    {
        EmployeeName = string.Empty;
    }

        /// <summary>
    /// 加班申请单ID
    /// </summary>
    public long OvertimeId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

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
/// 加班明细表导出DTO
/// </summary>
public partial class TaktOvertimeItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemExportDto()
    {
        CreatedAt = DateTime.Now;
        EmployeeName = string.Empty;
    }

        /// <summary>
    /// 加班申请单ID
    /// </summary>
    public long OvertimeId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmployeeName { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}