// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktLeaveDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：请假信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// 请假信息表Dto
/// </summary>
public partial class TaktLeaveDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveDto()
    {
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

    /// <summary>
    /// 请假信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; }
    /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 请假状态
    /// </summary>
    public int LeaveStatus { get; set; }
}

/// <summary>
/// 请假信息表查询DTO
/// </summary>
public partial class TaktLeaveQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 请假信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 请假类型
    /// </summary>
    public string? LeaveType { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 请假事由
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 请假状态
    /// </summary>
    public int? LeaveStatus { get; set; }

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
/// Takt创建请假信息表DTO
/// </summary>
public partial class TaktLeaveCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveCreateDto()
    {
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 请假状态
    /// </summary>
    public int LeaveStatus { get; set; }

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
/// Takt更新请假信息表DTO
/// </summary>
public partial class TaktLeaveUpdateDto : TaktLeaveCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveUpdateDto()
    {
    }

        /// <summary>
    /// 请假信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LeaveId { get; set; }
}

/// <summary>
/// 请假信息表请假状态DTO
/// </summary>
public partial class TaktLeaveStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveStatusDto()
    {
    }

        /// <summary>
    /// 请假信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 请假状态（0=禁用，1=启用）
    /// </summary>
    public int LeaveStatus { get; set; }
}

/// <summary>
/// 请假信息表导入模板DTO
/// </summary>
public partial class TaktLeaveTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveTemplateDto()
    {
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 请假状态
    /// </summary>
    public int LeaveStatus { get; set; }

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
/// 请假信息表导入DTO
/// </summary>
public partial class TaktLeaveImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveImportDto()
    {
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 请假状态
    /// </summary>
    public int LeaveStatus { get; set; }

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
/// 请假信息表导出DTO
/// </summary>
public partial class TaktLeaveExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveExportDto()
    {
        CreatedAt = DateTime.Now;
        LeaveType = string.Empty;
        Reason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 请假事由
    /// </summary>
    public string Reason { get; set; }

        /// <summary>
    /// 证明附件JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 请假状态
    /// </summary>
    public int LeaveStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}