// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintHandlingDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客诉处理记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 客诉处理记录表Dto
/// </summary>
public partial class TaktCustomerComplaintHandlingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingDto()
    {
        ComplaintHandlingCode = string.Empty;
        ComplaintNo = string.Empty;
        HandlingDescription = string.Empty;
    }

    /// <summary>
    /// 客诉处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintHandlingId { get; set; } = 0;

    /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string ComplaintHandlingCode { get; set; }
    /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ComplaintId { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string ComplaintNo { get; set; }
    /// <summary>
    /// 客诉明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ComplaintItemId { get; set; }
    /// <summary>
    /// 处理阶段
    /// </summary>
    public int HandlingStage { get; set; }
    /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }
    /// <summary>
    /// 处理说明
    /// </summary>
    public string HandlingDescription { get; set; }
    /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }
    /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }
    /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }
    /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }
    /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }
    /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }
    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }
    /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }
    /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }
    /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }
    /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }
    /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

    /// <summary>
    /// 客诉主表
    /// </summary>
    public TaktCustomerComplaintDto? Complaint { get; set; }
}

/// <summary>
/// 客诉处理记录表查询DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string? ComplaintHandlingCode { get; set; }
    /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ComplaintId { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string? ComplaintNo { get; set; }
    /// <summary>
    /// 客诉明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ComplaintItemId { get; set; }
    /// <summary>
    /// 处理阶段
    /// </summary>
    public int? HandlingStage { get; set; }
    /// <summary>
    /// 处理方式
    /// </summary>
    public int? HandlingMethod { get; set; }
    /// <summary>
    /// 处理说明
    /// </summary>
    public string? HandlingDescription { get; set; }
    /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }
    /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }
    /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }
    /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }
    /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }
    /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }
    /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

    /// <summary>
    /// 处理时间开始时间
    /// </summary>
    public DateTime? HandlingTimeStart { get; set; }
    /// <summary>
    /// 处理时间结束时间
    /// </summary>
    public DateTime? HandlingTimeEnd { get; set; }
    /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

    /// <summary>
    /// 计划完成日期开始时间
    /// </summary>
    public DateTime? PlannedCompletionDateStart { get; set; }
    /// <summary>
    /// 计划完成日期结束时间
    /// </summary>
    public DateTime? PlannedCompletionDateEnd { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

    /// <summary>
    /// 实际完成日期开始时间
    /// </summary>
    public DateTime? ActualCompletionDateStart { get; set; }
    /// <summary>
    /// 实际完成日期结束时间
    /// </summary>
    public DateTime? ActualCompletionDateEnd { get; set; }
    /// <summary>
    /// 处理状态
    /// </summary>
    public int? HandlingStatus { get; set; }
    /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }
    /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }
    /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

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
/// Takt创建客诉处理记录表DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingCreateDto()
    {
        ComplaintHandlingCode = string.Empty;
        ComplaintNo = string.Empty;
        HandlingDescription = string.Empty;
    }

        /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string ComplaintHandlingCode { get; set; }

        /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string ComplaintNo { get; set; }

        /// <summary>
    /// 客诉明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ComplaintItemId { get; set; }

        /// <summary>
    /// 处理阶段
    /// </summary>
    public int HandlingStage { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string HandlingDescription { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

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
/// Takt更新客诉处理记录表DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingUpdateDto : TaktCustomerComplaintHandlingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingUpdateDto()
    {
    }

        /// <summary>
    /// 客诉处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintHandlingId { get; set; } = 0;
}

/// <summary>
/// 客诉处理记录表处理状态DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingHandlingStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingHandlingStatusDto()
    {
    }

        /// <summary>
    /// 客诉处理记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintHandlingId { get; set; } = 0;

    /// <summary>
    /// 处理状态（0=禁用，1=启用）
    /// </summary>
    public int HandlingStatus { get; set; }
}

/// <summary>
/// 客诉处理记录表导入模板DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingTemplateDto()
    {
        ComplaintHandlingCode = string.Empty;
        ComplaintNo = string.Empty;
        HandlingDescription = string.Empty;
    }

        /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string ComplaintHandlingCode { get; set; }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string ComplaintNo { get; set; }

        /// <summary>
    /// 客诉明细ID
    /// </summary>
    public long? ComplaintItemId { get; set; }

        /// <summary>
    /// 处理阶段
    /// </summary>
    public int HandlingStage { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string HandlingDescription { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

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
/// 客诉处理记录表导入DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingImportDto()
    {
        ComplaintHandlingCode = string.Empty;
        ComplaintNo = string.Empty;
        HandlingDescription = string.Empty;
    }

        /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string ComplaintHandlingCode { get; set; }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string ComplaintNo { get; set; }

        /// <summary>
    /// 客诉明细ID
    /// </summary>
    public long? ComplaintItemId { get; set; }

        /// <summary>
    /// 处理阶段
    /// </summary>
    public int HandlingStage { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string HandlingDescription { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

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
/// 客诉处理记录表导出DTO
/// </summary>
public partial class TaktCustomerComplaintHandlingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintHandlingExportDto()
    {
        CreatedAt = DateTime.Now;
        ComplaintHandlingCode = string.Empty;
        ComplaintNo = string.Empty;
        HandlingDescription = string.Empty;
    }

        /// <summary>
    /// 客诉处理记录编码
    /// </summary>
    public string ComplaintHandlingCode { get; set; }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string ComplaintNo { get; set; }

        /// <summary>
    /// 客诉明细ID
    /// </summary>
    public long? ComplaintItemId { get; set; }

        /// <summary>
    /// 处理阶段
    /// </summary>
    public int HandlingStage { get; set; }

        /// <summary>
    /// 处理方式
    /// </summary>
    public int HandlingMethod { get; set; }

        /// <summary>
    /// 处理说明
    /// </summary>
    public string HandlingDescription { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 纠正措施
    /// </summary>
    public string? CorrectiveAction { get; set; }

        /// <summary>
    /// 预防措施
    /// </summary>
    public string? PreventiveAction { get; set; }

        /// <summary>
    /// 责任部门
    /// </summary>
    public string? ResponsibleDept { get; set; }

        /// <summary>
    /// 责任人
    /// </summary>
    public string? ResponsibleBy { get; set; }

        /// <summary>
    /// 处理人
    /// </summary>
    public string? HandlerBy { get; set; }

        /// <summary>
    /// 处理时间
    /// </summary>
    public DateTime? HandlingTime { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 处理状态
    /// </summary>
    public int HandlingStatus { get; set; }

        /// <summary>
    /// 处理成本
    /// </summary>
    public decimal? HandlingCost { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 客户满意度
    /// </summary>
    public int? CustomerSatisfaction { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}