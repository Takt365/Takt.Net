// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityIssuePcbaReworkDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：质量问题PCBA不良改修费用明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 质量问题PCBA不良改修费用明细表Dto
/// </summary>
public partial class TaktQualityIssuePcbaReworkDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkDto()
    {
        QualityIssueCode = string.Empty;
    }

    /// <summary>
    /// 质量问题PCBA不良改修费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssuePcbaReworkId { get; set; } = 0;

    /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; }
    /// <summary>
    /// 品质问题编码
    /// </summary>
    public string QualityIssueCode { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }
    /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal PcbaReworkCost { get; set; }
    /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int PcbaReworkTimeMinutes { get; set; }
    /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int PcbaReinspectionTimeMinutes { get; set; }
    /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal PcbaTravelCost { get; set; }
    /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal PcbaWarehouseCost { get; set; }
    /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal PcbaOtherExpenses { get; set; }
    /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }
    /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal PcbaScrapCost { get; set; }
    /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }
    /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }
    /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal PcbaOtherExpenses2 { get; set; }
    /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }
    /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

    /// <summary>
    /// 质量问题主表（导航属性）
    /// </summary>
    public TaktQualityIssueDto? Issue { get; set; }
}

/// <summary>
/// 质量问题PCBA不良改修费用明细表查询DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? QualityIssueId { get; set; }
    /// <summary>
    /// 品质问题编码
    /// </summary>
    public string? QualityIssueCode { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }
    /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal? PcbaReworkCost { get; set; }
    /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int? PcbaReworkTimeMinutes { get; set; }
    /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int? PcbaReinspectionTimeMinutes { get; set; }
    /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal? PcbaTravelCost { get; set; }
    /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal? PcbaWarehouseCost { get; set; }
    /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal? PcbaOtherExpenses { get; set; }
    /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }
    /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal? PcbaScrapCost { get; set; }
    /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }
    /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }
    /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal? PcbaOtherExpenses2 { get; set; }
    /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }
    /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

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
/// Takt创建质量问题PCBA不良改修费用明细表DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkCreateDto()
    {
        QualityIssueCode = string.Empty;
    }

        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 品质问题编码
    /// </summary>
    public string QualityIssueCode { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }

        /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal PcbaReworkCost { get; set; }

        /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int PcbaReworkTimeMinutes { get; set; }

        /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int PcbaReinspectionTimeMinutes { get; set; }

        /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal PcbaTravelCost { get; set; }

        /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal PcbaWarehouseCost { get; set; }

        /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal PcbaOtherExpenses { get; set; }

        /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }

        /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal PcbaScrapCost { get; set; }

        /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }

        /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }

        /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal PcbaOtherExpenses2 { get; set; }

        /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }

        /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

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
/// Takt更新质量问题PCBA不良改修费用明细表DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkUpdateDto : TaktQualityIssuePcbaReworkCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkUpdateDto()
    {
    }

        /// <summary>
    /// 质量问题PCBA不良改修费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssuePcbaReworkId { get; set; } = 0;
}

/// <summary>
/// 质量问题PCBA不良改修费用明细表导入模板DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkTemplateDto()
    {
        QualityIssueCode = string.Empty;
    }

        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 品质问题编码
    /// </summary>
    public string QualityIssueCode { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }

        /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal PcbaReworkCost { get; set; }

        /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int PcbaReworkTimeMinutes { get; set; }

        /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int PcbaReinspectionTimeMinutes { get; set; }

        /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal PcbaTravelCost { get; set; }

        /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal PcbaWarehouseCost { get; set; }

        /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal PcbaOtherExpenses { get; set; }

        /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }

        /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal PcbaScrapCost { get; set; }

        /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }

        /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }

        /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal PcbaOtherExpenses2 { get; set; }

        /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }

        /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

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
/// 质量问题PCBA不良改修费用明细表导入DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkImportDto()
    {
        QualityIssueCode = string.Empty;
    }

        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 品质问题编码
    /// </summary>
    public string QualityIssueCode { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }

        /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal PcbaReworkCost { get; set; }

        /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int PcbaReworkTimeMinutes { get; set; }

        /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int PcbaReinspectionTimeMinutes { get; set; }

        /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal PcbaTravelCost { get; set; }

        /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal PcbaWarehouseCost { get; set; }

        /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal PcbaOtherExpenses { get; set; }

        /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }

        /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal PcbaScrapCost { get; set; }

        /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }

        /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }

        /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal PcbaOtherExpenses2 { get; set; }

        /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }

        /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

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
/// 质量问题PCBA不良改修费用明细表导出DTO
/// </summary>
public partial class TaktQualityIssuePcbaReworkExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworkExportDto()
    {
        CreatedAt = DateTime.Now;
        QualityIssueCode = string.Empty;
    }

        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 品质问题编码
    /// </summary>
    public string QualityIssueCode { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// PCBA不良内容
    /// </summary>
    public string? PcbaDefectParts { get; set; }

        /// <summary>
    /// PCBA选别改修费用
    /// </summary>
    public decimal PcbaReworkCost { get; set; }

        /// <summary>
    /// PCBA选别改修时间
    /// </summary>
    public int PcbaReworkTimeMinutes { get; set; }

        /// <summary>
    /// PCBA再检查时间
    /// </summary>
    public int PcbaReinspectionTimeMinutes { get; set; }

        /// <summary>
    /// PCBA交通费旅费
    /// </summary>
    public decimal PcbaTravelCost { get; set; }

        /// <summary>
    /// PCBA仓库管理费
    /// </summary>
    public decimal PcbaWarehouseCost { get; set; }

        /// <summary>
    /// PCBA选别改修其他费用
    /// </summary>
    public decimal PcbaOtherExpenses { get; set; }

        /// <summary>
    /// PCBA选别改修备注
    /// </summary>
    public string? PcbaReworkNote { get; set; }

        /// <summary>
    /// PCBA向顾客费用请求
    /// </summary>
    public decimal PcbaScrapCost { get; set; }

        /// <summary>
    /// PCBA顾客名
    /// </summary>
    public string? PcbaCustomerName { get; set; }

        /// <summary>
    /// PCBA Debit Note No
    /// </summary>
    public string? PcbaDebitNoteNo { get; set; }

        /// <summary>
    /// PCBA其他费用
    /// </summary>
    public decimal PcbaOtherExpenses2 { get; set; }

        /// <summary>
    /// PCBA备注
    /// </summary>
    public string? PcbaNote { get; set; }

        /// <summary>
    /// PCBA不良改修对应记录者
    /// </summary>
    public string? PcbaRecorder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}