// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：加班信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 加班信息表Dto
/// </summary>
public partial class TaktOvertimeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeDto()
    {
        Reason = string.Empty;
    }

    /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }
    /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }
    /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }
    /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }
    /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }
}

/// <summary>
/// 加班信息表查询DTO
/// </summary>
public partial class TaktOvertimeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime? OvertimeDate { get; set; }

    /// <summary>
    /// 加班日期开始时间
    /// </summary>
    public DateTime? OvertimeDateStart { get; set; }
    /// <summary>
    /// 加班日期结束时间
    /// </summary>
    public DateTime? OvertimeDateEnd { get; set; }
    /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal? PlannedHours { get; set; }
    /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }
    /// <summary>
    /// 加班类型
    /// </summary>
    public int? OvertimeType { get; set; }
    /// <summary>
    /// 加班原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? OvertimeStatus { get; set; }

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
/// Takt创建加班信息表DTO
/// </summary>
public partial class TaktOvertimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeCreateDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// Takt更新加班信息表DTO
/// </summary>
public partial class TaktOvertimeUpdateDto : TaktOvertimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeUpdateDto()
    {
    }

        /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }
}

/// <summary>
/// 加班信息表状态DTO
/// </summary>
public partial class TaktOvertimeStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeStatusDto()
    {
    }

        /// <summary>
    /// 加班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OvertimeId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int OvertimeStatus { get; set; }
}

/// <summary>
/// 加班信息表导入模板DTO
/// </summary>
public partial class TaktOvertimeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeTemplateDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// 加班信息表导入DTO
/// </summary>
public partial class TaktOvertimeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeImportDto()
    {
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

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
/// 加班信息表导出DTO
/// </summary>
public partial class TaktOvertimeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeExportDto()
    {
        CreatedAt = DateTime.Now;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 加班日期
    /// </summary>
    public DateTime OvertimeDate { get; set; }

        /// <summary>
    /// 计划小时数
    /// </summary>
    public decimal PlannedHours { get; set; }

        /// <summary>
    /// 实际小时数
    /// </summary>
    public decimal? ActualHours { get; set; }

        /// <summary>
    /// 加班类型
    /// </summary>
    public int OvertimeType { get; set; }

        /// <summary>
    /// 加班原因
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int OvertimeStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}