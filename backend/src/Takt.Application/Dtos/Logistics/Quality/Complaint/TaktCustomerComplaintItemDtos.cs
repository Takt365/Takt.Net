// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客诉明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 客诉明细表Dto
/// </summary>
public partial class TaktCustomerComplaintItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemDto()
    {
        CustomerComplaintCode = string.Empty;
        DefectDescription = string.Empty;
        DefectLevel = string.Empty;
    }

    /// <summary>
    /// 客诉明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintItemId { get; set; } = 0;

    /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ComplaintId { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }
    /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 不良项目类型
    /// </summary>
    public int ItemType { get; set; }
    /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }
    /// <summary>
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }
    /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }
    /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }
    /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }
    /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }
    /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }
    /// <summary>
    /// 改善状态
    /// </summary>
    public int ImprovementStatus { get; set; }
    /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 客诉主表
    /// </summary>
    public TaktCustomerComplaintDto? Complaint { get; set; }
}

/// <summary>
/// 客诉明细表查询DTO
/// </summary>
public partial class TaktCustomerComplaintItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ComplaintId { get; set; }
    /// <summary>
    /// 客诉单号
    /// </summary>
    public string? CustomerComplaintCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }
    /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 不良项目类型
    /// </summary>
    public int? ItemType { get; set; }
    /// <summary>
    /// 不良现象描述
    /// </summary>
    public string? DefectDescription { get; set; }
    /// <summary>
    /// 缺点等级
    /// </summary>
    public string? DefectLevel { get; set; }
    /// <summary>
    /// 不良数量
    /// </summary>
    public int? DefectQuantity { get; set; }
    /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }
    /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }
    /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }
    /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }
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
    /// 改善状态
    /// </summary>
    public int? ImprovementStatus { get; set; }
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
/// Takt创建客诉明细表DTO
/// </summary>
public partial class TaktCustomerComplaintItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemCreateDto()
    {
        CustomerComplaintCode = string.Empty;
        DefectDescription = string.Empty;
        DefectLevel = string.Empty;
    }

        /// <summary>
    /// 客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 不良项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }

        /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 改善状态
    /// </summary>
    public int ImprovementStatus { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新客诉明细表DTO
/// </summary>
public partial class TaktCustomerComplaintItemUpdateDto : TaktCustomerComplaintItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemUpdateDto()
    {
    }

        /// <summary>
    /// 客诉明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintItemId { get; set; } = 0;
}

/// <summary>
/// 客诉明细表改善状态DTO
/// </summary>
public partial class TaktCustomerComplaintItemImprovementStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemImprovementStatusDto()
    {
    }

        /// <summary>
    /// 客诉明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintItemId { get; set; } = 0;

    /// <summary>
    /// 改善状态（0=禁用，1=启用）
    /// </summary>
    public int ImprovementStatus { get; set; }
}

/// <summary>
/// 客诉明细表排序DTO
/// </summary>
public partial class TaktCustomerComplaintItemSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemSortDto()
    {
    }

        /// <summary>
    /// 客诉明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerComplaintItemId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 客诉明细表导入模板DTO
/// </summary>
public partial class TaktCustomerComplaintItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemTemplateDto()
    {
        CustomerComplaintCode = string.Empty;
        DefectDescription = string.Empty;
        DefectLevel = string.Empty;
    }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 不良项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }

        /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 改善状态
    /// </summary>
    public int ImprovementStatus { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 客诉明细表导入DTO
/// </summary>
public partial class TaktCustomerComplaintItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemImportDto()
    {
        CustomerComplaintCode = string.Empty;
        DefectDescription = string.Empty;
        DefectLevel = string.Empty;
    }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 不良项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }

        /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 改善状态
    /// </summary>
    public int ImprovementStatus { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 客诉明细表导出DTO
/// </summary>
public partial class TaktCustomerComplaintItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemExportDto()
    {
        CreatedAt = DateTime.Now;
        CustomerComplaintCode = string.Empty;
        DefectDescription = string.Empty;
        DefectLevel = string.Empty;
    }

        /// <summary>
    /// 客诉ID
    /// </summary>
    public long ComplaintId { get; set; }

        /// <summary>
    /// 客诉单号
    /// </summary>
    public string CustomerComplaintCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 不良项目类型
    /// </summary>
    public int ItemType { get; set; }

        /// <summary>
    /// 不良现象描述
    /// </summary>
    public string DefectDescription { get; set; }

        /// <summary>
    /// 缺点等级
    /// </summary>
    public string DefectLevel { get; set; }

        /// <summary>
    /// 不良数量
    /// </summary>
    public int DefectQuantity { get; set; }

        /// <summary>
    /// 不良率
    /// </summary>
    public decimal? DefectRate { get; set; }

        /// <summary>
    /// 原因分析
    /// </summary>
    public string? CauseAnalysis { get; set; }

        /// <summary>
    /// 改善对策
    /// </summary>
    public string? ImprovementAction { get; set; }

        /// <summary>
    /// 改善责任人
    /// </summary>
    public string? ImprovementResponsible { get; set; }

        /// <summary>
    /// 计划完成日期
    /// </summary>
    public DateTime? PlannedCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

        /// <summary>
    /// 改善状态
    /// </summary>
    public int ImprovementStatus { get; set; }

        /// <summary>
    /// 附件路径
    /// </summary>
    public string? AttachmentPaths { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}