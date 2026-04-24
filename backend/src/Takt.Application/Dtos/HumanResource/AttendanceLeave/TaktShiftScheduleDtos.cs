// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktShiftScheduleDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：排班信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 排班信息表Dto
/// </summary>
public partial class TaktShiftScheduleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 排班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ShiftScheduleId { get; set; }

    /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }
    /// <summary>
    /// 班次ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ShiftId { get; set; }
}

/// <summary>
/// 排班信息表查询DTO
/// </summary>
public partial class TaktShiftScheduleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 排班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ShiftScheduleId { get; set; }

    /// <summary>
    /// 排班类别
    /// </summary>
    public int? ScheduleType { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime? ScheduleDate { get; set; }

    /// <summary>
    /// 排班日期开始时间
    /// </summary>
    public DateTime? ScheduleDateStart { get; set; }
    /// <summary>
    /// 排班日期结束时间
    /// </summary>
    public DateTime? ScheduleDateEnd { get; set; }
    /// <summary>
    /// 班次ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ShiftId { get; set; }

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
/// Takt创建排班信息表DTO
/// </summary>
public partial class TaktShiftScheduleCreateDto
{
        /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

        /// <summary>
    /// 班次ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ShiftId { get; set; }

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
/// Takt更新排班信息表DTO
/// </summary>
public partial class TaktShiftScheduleUpdateDto : TaktShiftScheduleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleUpdateDto()
    {
    }

        /// <summary>
    /// 排班信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ShiftScheduleId { get; set; }
}

/// <summary>
/// 排班信息表导入模板DTO
/// </summary>
public partial class TaktShiftScheduleTemplateDto
{
        /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

        /// <summary>
    /// 班次ID
    /// </summary>
    public long ShiftId { get; set; }

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
/// 排班信息表导入DTO
/// </summary>
public partial class TaktShiftScheduleImportDto
{
        /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

        /// <summary>
    /// 班次ID
    /// </summary>
    public long ShiftId { get; set; }

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
/// 排班信息表导出DTO
/// </summary>
public partial class TaktShiftScheduleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftScheduleExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 排班类别
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 排班日期
    /// </summary>
    public DateTime ScheduleDate { get; set; }

        /// <summary>
    /// 班次ID
    /// </summary>
    public long ShiftId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}