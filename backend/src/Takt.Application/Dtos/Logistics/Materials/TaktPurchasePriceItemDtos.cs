// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchasePriceItemDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：采购价格明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购价格明细表Dto
/// </summary>
public partial class TaktPurchasePriceItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = string.Empty;
    }

    /// <summary>
    /// 采购价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceItemId { get; set; }

    /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }
    /// <summary>
    /// 采购价格
    /// </summary>
    public decimal PurchasePrice { get; set; }
    /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; }
    /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 价格阶梯列表（主子表关系，一个物料价格可以有多个阶梯）
    /// </summary>
    public List<long>? ScaleIds { get; set; }
}

/// <summary>
/// 采购价格明细表查询DTO
/// </summary>
public partial class TaktPurchasePriceItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 采购价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceItemId { get; set; }

    /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PriceId { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 采购单位
    /// </summary>
    public string? PurchaseUnit { get; set; }
    /// <summary>
    /// 采购价格
    /// </summary>
    public decimal? PurchasePrice { get; set; }
    /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal? MinPurchaseQuantity { get; set; }
    /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal? MaxPurchaseQuantity { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建采购价格明细表DTO
/// </summary>
public partial class TaktPurchasePriceItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemCreateDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 采购价格
    /// </summary>
    public decimal PurchasePrice { get; set; }

        /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; }

        /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; }

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
/// Takt更新采购价格明细表DTO
/// </summary>
public partial class TaktPurchasePriceItemUpdateDto : TaktPurchasePriceItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemUpdateDto()
    {
    }

        /// <summary>
    /// 采购价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceItemId { get; set; }
}

/// <summary>
/// 采购价格明细表导入模板DTO
/// </summary>
public partial class TaktPurchasePriceItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemTemplateDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 采购价格
    /// </summary>
    public decimal PurchasePrice { get; set; }

        /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; }

        /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; }

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
/// 采购价格明细表导入DTO
/// </summary>
public partial class TaktPurchasePriceItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemImportDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 采购价格
    /// </summary>
    public decimal PurchasePrice { get; set; }

        /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; }

        /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; }

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
/// 采购价格明细表导出DTO
/// </summary>
public partial class TaktPurchasePriceItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemExportDto()
    {
        CreatedAt = DateTime.Now;
        MaterialCode = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 采购价格
    /// </summary>
    public decimal PurchasePrice { get; set; }

        /// <summary>
    /// 最小采购量
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; }

        /// <summary>
    /// 最大采购量
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}