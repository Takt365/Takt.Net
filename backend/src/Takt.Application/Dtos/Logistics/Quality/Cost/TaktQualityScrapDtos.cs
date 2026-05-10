// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质废弃主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃主表Dto
/// </summary>
public partial class TaktQualityScrapDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapDto()
    {
        PlantCode = string.Empty;
        ScrapNo = string.Empty;
        Model = string.Empty;
        CostCurrency = string.Empty;
    }

    /// <summary>
    /// 品质废弃主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityScrapId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 废弃单号
    /// </summary>
    public string ScrapNo { get; set; }
    /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime ScrapDate { get; set; }
    /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }
    /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }
    /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal TotalScrapQuantity { get; set; }
    /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal TotalScrapCost { get; set; }
    /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 废弃明细列表（外键在子表 TaktQualityScrapItemDto.QualityScrapId）
    /// </summary>
    public List<TaktQualityScrapItemDto>? ScrapItems { get; set; }
}

/// <summary>
/// 品质废弃主表查询DTO
/// </summary>
public partial class TaktQualityScrapQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 废弃单号
    /// </summary>
    public string? ScrapNo { get; set; }
    /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime? ScrapDate { get; set; }

    /// <summary>
    /// 废弃日期开始时间
    /// </summary>
    public DateTime? ScrapDateStart { get; set; }
    /// <summary>
    /// 废弃日期结束时间
    /// </summary>
    public DateTime? ScrapDateEnd { get; set; }
    /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal? IndirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }
    /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal? TotalScrapQuantity { get; set; }
    /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal? TotalScrapCost { get; set; }
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
/// Takt创建品质废弃主表DTO
/// </summary>
public partial class TaktQualityScrapCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapCreateDto()
    {
        PlantCode = string.Empty;
        ScrapNo = string.Empty;
        Model = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 废弃单号
    /// </summary>
    public string ScrapNo { get; set; }

        /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime ScrapDate { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }

        /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal TotalScrapQuantity { get; set; }

        /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal TotalScrapCost { get; set; }

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
    /// 废弃明细列表（外键在子表 TaktQualityScrapItemCreateDto.QualityScrapId）
    /// </summary>
    public List<TaktQualityScrapItemCreateDto>? ScrapItems { get; set; }

}

/// <summary>
/// Takt更新品质废弃主表DTO
/// </summary>
public partial class TaktQualityScrapUpdateDto : TaktQualityScrapCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapUpdateDto()
    {
    }

        /// <summary>
    /// 品质废弃主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityScrapId { get; set; } = 0;
}

/// <summary>
/// 品质废弃主表导入模板DTO
/// </summary>
public partial class TaktQualityScrapTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapTemplateDto()
    {
        PlantCode = string.Empty;
        ScrapNo = string.Empty;
        Model = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 废弃单号
    /// </summary>
    public string ScrapNo { get; set; }

        /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime ScrapDate { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }

        /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal TotalScrapQuantity { get; set; }

        /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal TotalScrapCost { get; set; }

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
/// 品质废弃主表导入DTO
/// </summary>
public partial class TaktQualityScrapImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapImportDto()
    {
        PlantCode = string.Empty;
        ScrapNo = string.Empty;
        Model = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 废弃单号
    /// </summary>
    public string ScrapNo { get; set; }

        /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime ScrapDate { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }

        /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal TotalScrapQuantity { get; set; }

        /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal TotalScrapCost { get; set; }

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
/// 品质废弃主表导出DTO
/// </summary>
public partial class TaktQualityScrapExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        ScrapNo = string.Empty;
        Model = string.Empty;
        CostCurrency = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 废弃单号
    /// </summary>
    public string ScrapNo { get; set; }

        /// <summary>
    /// 废弃日期
    /// </summary>
    public DateTime ScrapDate { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string Model { get; set; }

        /// <summary>
    /// 事故内容
    /// </summary>
    public string? ScrapReason { get; set; }

        /// <summary>
    /// 废弃总数
    /// </summary>
    public decimal TotalScrapQuantity { get; set; }

        /// <summary>
    /// 总废弃费用
    /// </summary>
    public decimal TotalScrapCost { get; set; }

        /// <summary>
    /// 成本币种
    /// </summary>
    public string CostCurrency { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}