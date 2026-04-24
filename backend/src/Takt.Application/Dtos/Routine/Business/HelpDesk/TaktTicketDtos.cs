// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktTicketDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 工单表Dto
/// </summary>
public partial class TaktTicketDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketDto()
    {
        TicketNo = string.Empty;
        Title = string.Empty;
    }

    /// <summary>
    /// 工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 工单编号
    /// </summary>
    public string TicketNo { get; set; }
    /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }
    /// <summary>
    /// 工单状态
    /// </summary>
    public int TicketStatus { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }
    /// <summary>
    /// 工单来源
    /// </summary>
    public int TicketSource { get; set; }
    /// <summary>
    /// 提交人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SubmitterId { get; set; }
    /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }
    /// <summary>
    /// 处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }
    /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }
    /// <summary>
    /// 关联知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }
    /// <summary>
    /// 父工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }
    /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }
    /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }
    /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }
    /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }
    /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }
    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }
    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 子工单列表（父工单时有效；外键：本表 Id = 子工单 ParentTicketId）
    /// </summary>
    public List<long>? ChildTicketIds { get; set; }
}

/// <summary>
/// 工单表查询DTO
/// </summary>
public partial class TaktTicketQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }
    /// <summary>
    /// 工单标题
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }
    /// <summary>
    /// 工单状态
    /// </summary>
    public int? TicketStatus { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int? Priority { get; set; }
    /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }
    /// <summary>
    /// 工单来源
    /// </summary>
    public int? TicketSource { get; set; }
    /// <summary>
    /// 提交人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SubmitterId { get; set; }
    /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }
    /// <summary>
    /// 处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }
    /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }
    /// <summary>
    /// 关联知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }
    /// <summary>
    /// 父工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }
    /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

    /// <summary>
    /// 首次响应时间开始时间
    /// </summary>
    public DateTime? FirstResponseAtStart { get; set; }
    /// <summary>
    /// 首次响应时间结束时间
    /// </summary>
    public DateTime? FirstResponseAtEnd { get; set; }
    /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

    /// <summary>
    /// 首次响应期限开始时间
    /// </summary>
    public DateTime? FirstResponseDueByStart { get; set; }
    /// <summary>
    /// 首次响应期限结束时间
    /// </summary>
    public DateTime? FirstResponseDueByEnd { get; set; }
    /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// 解决时间开始时间
    /// </summary>
    public DateTime? ResolvedAtStart { get; set; }
    /// <summary>
    /// 解决时间结束时间
    /// </summary>
    public DateTime? ResolvedAtEnd { get; set; }
    /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

    /// <summary>
    /// 解决期限开始时间
    /// </summary>
    public DateTime? ResolutionDueByStart { get; set; }
    /// <summary>
    /// 解决期限结束时间
    /// </summary>
    public DateTime? ResolutionDueByEnd { get; set; }
    /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }

    /// <summary>
    /// 关闭时间开始时间
    /// </summary>
    public DateTime? ClosedAtStart { get; set; }
    /// <summary>
    /// 关闭时间结束时间
    /// </summary>
    public DateTime? ClosedAtEnd { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }
    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }
    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 申请日期开始时间
    /// </summary>
    public DateTime? ApplicationDateStart { get; set; }
    /// <summary>
    /// 申请日期结束时间
    /// </summary>
    public DateTime? ApplicationDateEnd { get; set; }

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
/// Takt创建工单表DTO
/// </summary>
public partial class TaktTicketCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCreateDto()
    {
        TicketNo = string.Empty;
        Title = string.Empty;
    }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string TicketNo { get; set; }

        /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 工单状态
    /// </summary>
    public int TicketStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 工单来源
    /// </summary>
    public int TicketSource { get; set; }

        /// <summary>
    /// 提交人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SubmitterId { get; set; }

        /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }

        /// <summary>
    /// 处理人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }

        /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 关联知识ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

        /// <summary>
    /// 父工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ParentTicketId { get; set; }

        /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

        /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

        /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

        /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

        /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

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
/// Takt更新工单表DTO
/// </summary>
public partial class TaktTicketUpdateDto : TaktTicketCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketUpdateDto()
    {
    }

        /// <summary>
    /// 工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }
}

/// <summary>
/// 工单表工单状态DTO
/// </summary>
public partial class TaktTicketStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketStatusDto()
    {
    }

        /// <summary>
    /// 工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 工单状态（0=禁用，1=启用）
    /// </summary>
    public int TicketStatus { get; set; }
}

/// <summary>
/// 工单表导入模板DTO
/// </summary>
public partial class TaktTicketTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketTemplateDto()
    {
        TicketNo = string.Empty;
        Title = string.Empty;
    }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string TicketNo { get; set; }

        /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 工单状态
    /// </summary>
    public int TicketStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 工单来源
    /// </summary>
    public int TicketSource { get; set; }

        /// <summary>
    /// 提交人ID
    /// </summary>
    public long SubmitterId { get; set; }

        /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }

        /// <summary>
    /// 处理人ID
    /// </summary>
    public long? AssigneeId { get; set; }

        /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 关联知识ID
    /// </summary>
    public long? KnowledgeId { get; set; }

        /// <summary>
    /// 父工单ID
    /// </summary>
    public long? ParentTicketId { get; set; }

        /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

        /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

        /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

        /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

        /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long? ApplicantDeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

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
/// 工单表导入DTO
/// </summary>
public partial class TaktTicketImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketImportDto()
    {
        TicketNo = string.Empty;
        Title = string.Empty;
    }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string TicketNo { get; set; }

        /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 工单状态
    /// </summary>
    public int TicketStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 工单来源
    /// </summary>
    public int TicketSource { get; set; }

        /// <summary>
    /// 提交人ID
    /// </summary>
    public long SubmitterId { get; set; }

        /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }

        /// <summary>
    /// 处理人ID
    /// </summary>
    public long? AssigneeId { get; set; }

        /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 关联知识ID
    /// </summary>
    public long? KnowledgeId { get; set; }

        /// <summary>
    /// 父工单ID
    /// </summary>
    public long? ParentTicketId { get; set; }

        /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

        /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

        /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

        /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

        /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long? ApplicantDeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

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
/// 工单表导出DTO
/// </summary>
public partial class TaktTicketExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketExportDto()
    {
        CreatedAt = DateTime.Now;
        TicketNo = string.Empty;
        Title = string.Empty;
    }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string TicketNo { get; set; }

        /// <summary>
    /// 工单标题
    /// </summary>
    public string Title { get; set; }

        /// <summary>
    /// 工单内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 附件列表JSON
    /// </summary>
    public string? AttachmentsJson { get; set; }

        /// <summary>
    /// 工单状态
    /// </summary>
    public int TicketStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 分类编码
    /// </summary>
    public string? CategoryCode { get; set; }

        /// <summary>
    /// 工单来源
    /// </summary>
    public int TicketSource { get; set; }

        /// <summary>
    /// 提交人ID
    /// </summary>
    public long SubmitterId { get; set; }

        /// <summary>
    /// 提交人姓名
    /// </summary>
    public string? SubmitterName { get; set; }

        /// <summary>
    /// 处理人ID
    /// </summary>
    public long? AssigneeId { get; set; }

        /// <summary>
    /// 处理人姓名
    /// </summary>
    public string? AssigneeName { get; set; }

        /// <summary>
    /// 关联知识ID
    /// </summary>
    public long? KnowledgeId { get; set; }

        /// <summary>
    /// 父工单ID
    /// </summary>
    public long? ParentTicketId { get; set; }

        /// <summary>
    /// 首次响应时间
    /// </summary>
    public DateTime? FirstResponseAt { get; set; }

        /// <summary>
    /// 首次响应期限
    /// </summary>
    public DateTime? FirstResponseDueBy { get; set; }

        /// <summary>
    /// 解决时间
    /// </summary>
    public DateTime? ResolvedAt { get; set; }

        /// <summary>
    /// 解决期限
    /// </summary>
    public DateTime? ResolutionDueBy { get; set; }

        /// <summary>
    /// 关闭时间
    /// </summary>
    public DateTime? ClosedAt { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long? ApplicantDeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? ApplicantDeptName { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? Applicant { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}