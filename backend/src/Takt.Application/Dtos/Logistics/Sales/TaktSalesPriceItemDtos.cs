// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Sales
// 文件名称：TaktSalesPriceItemDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：销售价格明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Sales;

/// <summary>
/// 销售价格明细表Dto
/// </summary>
public partial class TaktSalesPriceItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemDto()
    {
        MaterialCode = string.Empty;
        SalesUnit = string.Empty;
    }

    /// <summary>
    /// 销售价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceItemId { get; set; }

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
    /// 销售单位
    /// </summary>
    public string SalesUnit { get; set; }
    /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }
    /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }
    /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal MaxOrderQuantity { get; set; }
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
/// 销售价格明细表查询DTO
/// </summary>
public partial class TaktSalesPriceItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 销售价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceItemId { get; set; }

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
    /// 销售单位
    /// </summary>
    public string? SalesUnit { get; set; }
    /// <summary>
    /// 销售价格
    /// </summary>
    public decimal? SalesPrice { get; set; }
    /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal? MinOrderQuantity { get; set; }
    /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal? MaxOrderQuantity { get; set; }
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
/// Takt创建销售价格明细表DTO
/// </summary>
public partial class TaktSalesPriceItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemCreateDto()
    {
        MaterialCode = string.Empty;
        SalesUnit = string.Empty;
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
    /// 销售单位
    /// </summary>
    public string SalesUnit { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal MaxOrderQuantity { get; set; }

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
/// Takt更新销售价格明细表DTO
/// </summary>
public partial class TaktSalesPriceItemUpdateDto : TaktSalesPriceItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemUpdateDto()
    {
    }

        /// <summary>
    /// 销售价格明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceItemId { get; set; }
}

/// <summary>
/// 销售价格明细表导入模板DTO
/// </summary>
public partial class TaktSalesPriceItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemTemplateDto()
    {
        MaterialCode = string.Empty;
        SalesUnit = string.Empty;
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
    /// 销售单位
    /// </summary>
    public string SalesUnit { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal MaxOrderQuantity { get; set; }

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
/// 销售价格明细表导入DTO
/// </summary>
public partial class TaktSalesPriceItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemImportDto()
    {
        MaterialCode = string.Empty;
        SalesUnit = string.Empty;
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
    /// 销售单位
    /// </summary>
    public string SalesUnit { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal MaxOrderQuantity { get; set; }

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
/// 销售价格明细表导出DTO
/// </summary>
public partial class TaktSalesPriceItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemExportDto()
    {
        CreatedAt = DateTime.Now;
        MaterialCode = string.Empty;
        SalesUnit = string.Empty;
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
    /// 销售单位
    /// </summary>
    public string SalesUnit { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 最小订购量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 最大订购量
    /// </summary>
    public decimal MaxOrderQuantity { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}