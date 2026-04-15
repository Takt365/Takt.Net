// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktLeaveDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt请假DTO，包含请假相关的数据传输对象（查询、创建、更新、状态、导入导出、提交流程）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// Takt请假DTO
/// </summary>
public class TaktLeaveDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveDto()
    {
        LeaveType = string.Empty;
    }

    /// <summary>
    /// 请假ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    public int LeaveStatus { get; set; }
}

/// <summary>
/// Takt请假查询DTO
/// </summary>
public class TaktLeaveQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveQueryDto()
    {
    }

    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（精确）
    /// </summary>
    public string? LeaveType { get; set; }

    /// <summary>
    /// 请假状态（精确）
    /// </summary>
    public int? LeaveStatus { get; set; }

    /// <summary>
    /// 开始日期起（闭区间）
    /// </summary>
    public DateTime? StartDateFrom { get; set; }

    /// <summary>
    /// 开始日期止（闭区间）
    /// </summary>
    public DateTime? StartDateTo { get; set; }
}

/// <summary>
/// Takt创建请假DTO
/// </summary>
public class TaktLeaveCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveCreateDto()
    {
        LeaveType = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }
}

/// <summary>
/// Takt更新请假DTO
/// </summary>
public class TaktLeaveUpdateDto : TaktLeaveCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveUpdateDto()
    {
    }

    /// <summary>
    /// 请假ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }
}

/// <summary>
/// Takt请假状态DTO（更新请假状态，需与流程实例同步）
/// </summary>
public class TaktLeaveStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveStatusDto()
    {
    }

    /// <summary>
    /// 请假ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回（与流程实例状态对应同步）
    /// </summary>
    public int LeaveStatus { get; set; }

    /// <summary>
    /// 流程实例ID（可选；同步时传入可校验该请假记录所属实例，避免误更新）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
}

/// <summary>
/// Takt请假导入模板DTO
/// </summary>
public class TaktLeaveTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveTemplateDto()
    {
        LeaveType = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON（可选）
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }
}

/// <summary>
/// Takt请假导入DTO
/// </summary>
public class TaktLeaveImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveImportDto()
    {
        LeaveType = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON（可选）
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }
}

/// <summary>
/// Takt请假导出DTO
/// </summary>
public class TaktLeaveExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveExportDto()
    {
        LeaveType = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 请假ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    public int LeaveStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Takt请假提交入参（新增请假并发起流程）
/// </summary>
public class TaktLeaveSubmitDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveSubmitDto()
    {
        LeaveType = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型（affair/sick/annual 等）
    /// </summary>
    public string LeaveType { get; set; } = string.Empty;

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
    public string? Reason { get; set; }

    /// <summary>
    /// 证明附件 JSON
    /// </summary>
    public string? ProofAttachmentsJson { get; set; }

    /// <summary>
    /// 流程标题（可选，默认自动生成）
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 流程表单数据 JSON（可选）
    /// </summary>
    public string? FrmData { get; set; }
}

/// <summary>
/// Takt请假提交结果（含流程实例信息，用于匹配）
/// </summary>
public class TaktLeaveSubmitResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeaveSubmitResultDto()
    {
        InstanceCode = string.Empty;
        ProcessKey = "Leave";
        ProcessName = string.Empty;
    }

    /// <summary>
    /// 请假主键
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LeaveId { get; set; }

    /// <summary>
    /// 流程实例ID（与 TaktLeave.FlowInstanceId 一致）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }


    /// <summary>
    /// 流程Key（Leave）
    /// </summary>
    public string ProcessKey { get; set; } = "Leave";

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;
}
