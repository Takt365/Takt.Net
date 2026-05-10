// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseOrderItemDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：采购订单明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购订单明细表Dto
/// </summary>
public partial class TaktPurchaseOrderItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemDto()
    {
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = string.Empty;
    }

    /// <summary>
    /// 采购订单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderItemId { get; set; } = 0;

    /// <summary>
    /// 采购订单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }
    /// <summary>
    /// 订购数量
    /// </summary>
    public decimal OrderQuantity { get; set; }
    /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }
    /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; set; }
    /// <summary>
    /// 折扣率
    /// </summary>
    public decimal DiscountRate { get; set; }
    /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }
    /// <summary>
    /// 税费率
    /// </summary>
    public decimal TaxRate { get; set; }
    /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }
    /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubtotalAmount { get; set; }
    /// <summary>
    /// 行交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }
}

/// <summary>
/// 采购订单明细表查询DTO
/// </summary>
public partial class TaktPurchaseOrderItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 采购订单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseOrderId { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }
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
    /// 订购数量
    /// </summary>
    public decimal? OrderQuantity { get; set; }
    /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal? ReceivedQuantity { get; set; }
    /// <summary>
    /// 单价
    /// </summary>
    public decimal? UnitPrice { get; set; }
    /// <summary>
    /// 折扣率
    /// </summary>
    public decimal? DiscountRate { get; set; }
    /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal? DiscountAmount { get; set; }
    /// <summary>
    /// 税费率
    /// </summary>
    public decimal? TaxRate { get; set; }
    /// <summary>
    /// 税费
    /// </summary>
    public decimal? TaxAmount { get; set; }
    /// <summary>
    /// 小计金额
    /// </summary>
    public decimal? SubtotalAmount { get; set; }
    /// <summary>
    /// 行交货状态
    /// </summary>
    public int? DeliveryStatus { get; set; }

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
/// Takt创建采购订单明细表DTO
/// </summary>
public partial class TaktPurchaseOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemCreateDto()
    {
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 采购订单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 订购数量
    /// </summary>
    public decimal OrderQuantity { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; set; }

        /// <summary>
    /// 折扣率
    /// </summary>
    public decimal DiscountRate { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubtotalAmount { get; set; }

        /// <summary>
    /// 行交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

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
/// Takt更新采购订单明细表DTO
/// </summary>
public partial class TaktPurchaseOrderItemUpdateDto : TaktPurchaseOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemUpdateDto()
    {
    }

        /// <summary>
    /// 采购订单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderItemId { get; set; } = 0;
}

/// <summary>
/// 采购订单明细表行交货状态DTO
/// </summary>
public partial class TaktPurchaseOrderItemDeliveryStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemDeliveryStatusDto()
    {
    }

        /// <summary>
    /// 采购订单明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderItemId { get; set; } = 0;

    /// <summary>
    /// 行交货状态（0=禁用，1=启用）
    /// </summary>
    public int DeliveryStatus { get; set; }
}

/// <summary>
/// 采购订单明细表导入模板DTO
/// </summary>
public partial class TaktPurchaseOrderItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemTemplateDto()
    {
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 采购订单ID
    /// </summary>
    public long PurchaseOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 订购数量
    /// </summary>
    public decimal OrderQuantity { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; set; }

        /// <summary>
    /// 折扣率
    /// </summary>
    public decimal DiscountRate { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubtotalAmount { get; set; }

        /// <summary>
    /// 行交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

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
/// 采购订单明细表导入DTO
/// </summary>
public partial class TaktPurchaseOrderItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemImportDto()
    {
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 采购订单ID
    /// </summary>
    public long PurchaseOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 订购数量
    /// </summary>
    public decimal OrderQuantity { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; set; }

        /// <summary>
    /// 折扣率
    /// </summary>
    public decimal DiscountRate { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubtotalAmount { get; set; }

        /// <summary>
    /// 行交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

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
/// 采购订单明细表导出DTO
/// </summary>
public partial class TaktPurchaseOrderItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemExportDto()
    {
        CreatedAt = DateTime.Now;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = string.Empty;
    }

        /// <summary>
    /// 采购订单ID
    /// </summary>
    public long PurchaseOrderId { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 来源请购编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 来源请购行号
    /// </summary>
    public int? RequestLineNumber { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

        /// <summary>
    /// 订购数量
    /// </summary>
    public decimal OrderQuantity { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 单价
    /// </summary>
    public decimal UnitPrice { get; set; }

        /// <summary>
    /// 折扣率
    /// </summary>
    public decimal DiscountRate { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 小计金额
    /// </summary>
    public decimal SubtotalAmount { get; set; }

        /// <summary>
    /// 行交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}