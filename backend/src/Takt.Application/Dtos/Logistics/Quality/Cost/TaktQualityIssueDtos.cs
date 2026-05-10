// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质问题应对主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质问题应对主表Dto
/// </summary>
public partial class TaktQualityIssueDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueDto()
    {
        PlantCode = string.Empty;
        IssueNo = string.Empty;
        Model = string.Empty;
        Lot = string.Empty;
        CostCurrency = string.Empty;
    }

    /// <summary>
    /// 品质问题应对主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 问题编号
    /// </summary>
    public string IssueNo { get; set; }
    /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime IssueDate { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string Lot { get; set; }
    /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }
    /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }
    /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }
    /// <summary>
    /// 总时间
    /// </summary>
    public int TotalTimeMinutes { get; set; }
    /// <summary>
    /// 总费用
    /// </summary>
    public decimal TotalCost { get; set; }
    /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 会议/调查/试验费用明细列表（外键在子表 TaktQualityIssueMeetingDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssueMeetingDto>? MeetingItems { get; set; }

    /// <summary>
    /// 组装不良改修应对明细列表（外键在子表 TaktQualityIssueAssyReworkDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssueAssyReworkDto>? AssyReworkItems { get; set; }

    /// <summary>
    /// PCBA不良改修应对明细列表（外键在子表 TaktQualityIssuePcbaReworkDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssuePcbaReworkDto>? PcbaReworkItems { get; set; }
}

/// <summary>
/// 品质问题应对主表查询DTO
/// </summary>
public partial class TaktQualityIssueQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 问题编号
    /// </summary>
    public string? IssueNo { get; set; }
    /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// 问题日期开始时间
    /// </summary>
    public DateTime? IssueDateStart { get; set; }
    /// <summary>
    /// 问题日期结束时间
    /// </summary>
    public DateTime? IssueDateEnd { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? Lot { get; set; }
    /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }
    /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }
    /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }
    /// <summary>
    /// 总时间
    /// </summary>
    public int? TotalTimeMinutes { get; set; }
    /// <summary>
    /// 总费用
    /// </summary>
    public decimal? TotalCost { get; set; }
    /// <summary>
    /// 成本币种
    /// </summary>
    public string? CostCurrency { get; set; }

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
/// Takt创建品质问题应对主表DTO
/// </summary>
public partial class TaktQualityIssueCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueCreateDto()
    {
        PlantCode = string.Empty;
        IssueNo = string.Empty;
        Model = string.Empty;
        Lot = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 问题编号
    /// </summary>
    public string IssueNo { get; set; }

        /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime IssueDate { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string Lot { get; set; }

        /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }

        /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }

        /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }

        /// <summary>
    /// 总时间
    /// </summary>
    public int TotalTimeMinutes { get; set; }

        /// <summary>
    /// 总费用
    /// </summary>
    public decimal TotalCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 会议/调查/试验费用明细列表（外键在子表 TaktQualityIssueMeetingCreateDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssueMeetingCreateDto>? MeetingItems { get; set; }


    /// <summary>
    /// 组装不良改修应对明细列表（外键在子表 TaktQualityIssueAssyReworkCreateDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssueAssyReworkCreateDto>? AssyReworkItems { get; set; }


    /// <summary>
    /// PCBA不良改修应对明细列表（外键在子表 TaktQualityIssuePcbaReworkCreateDto.QualityIssueId）
    /// </summary>
    public List<TaktQualityIssuePcbaReworkCreateDto>? PcbaReworkItems { get; set; }

}

/// <summary>
/// Takt更新品质问题应对主表DTO
/// </summary>
public partial class TaktQualityIssueUpdateDto : TaktQualityIssueCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueUpdateDto()
    {
    }

        /// <summary>
    /// 品质问题应对主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; } = 0;
}

/// <summary>
/// 品质问题应对主表导入模板DTO
/// </summary>
public partial class TaktQualityIssueTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueTemplateDto()
    {
        PlantCode = string.Empty;
        IssueNo = string.Empty;
        Model = string.Empty;
        Lot = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 问题编号
    /// </summary>
    public string IssueNo { get; set; }

        /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime IssueDate { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string Lot { get; set; }

        /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }

        /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }

        /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }

        /// <summary>
    /// 总时间
    /// </summary>
    public int TotalTimeMinutes { get; set; }

        /// <summary>
    /// 总费用
    /// </summary>
    public decimal TotalCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

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
/// 品质问题应对主表导入DTO
/// </summary>
public partial class TaktQualityIssueImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueImportDto()
    {
        PlantCode = string.Empty;
        IssueNo = string.Empty;
        Model = string.Empty;
        Lot = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 问题编号
    /// </summary>
    public string IssueNo { get; set; }

        /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime IssueDate { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string Lot { get; set; }

        /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }

        /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }

        /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }

        /// <summary>
    /// 总时间
    /// </summary>
    public int TotalTimeMinutes { get; set; }

        /// <summary>
    /// 总费用
    /// </summary>
    public decimal TotalCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

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
/// 品质问题应对主表导出DTO
/// </summary>
public partial class TaktQualityIssueExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        IssueNo = string.Empty;
        Model = string.Empty;
        Lot = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 问题编号
    /// </summary>
    public string IssueNo { get; set; }

        /// <summary>
    /// 问题日期
    /// </summary>
    public DateTime IssueDate { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 批次号
    /// </summary>
    public string Lot { get; set; }

        /// <summary>
    /// 品质问题应对
    /// </summary>
    public string? QualityProblemsResponse { get; set; }

        /// <summary>
    /// 不良改修应对
    /// </summary>
    public string? ReworkDueToDefects { get; set; }

        /// <summary>
    /// 是否需要不良改修应对
    /// </summary>
    public string? NeedRework { get; set; }

        /// <summary>
    /// 总时间
    /// </summary>
    public int TotalTimeMinutes { get; set; }

        /// <summary>
    /// 总费用
    /// </summary>
    public decimal TotalCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}