// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseOrderDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：采购订单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购订单表Dto
/// </summary>
public partial class TaktPurchaseOrderDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderDto()
    {
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
    }

    /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }
    /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }
    /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }
    /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }
    /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }
    /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }
    /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }
    /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }
    /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }
    /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }
    /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal PaidAmount { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }
    /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }
    /// <summary>
    /// 支付方式
    /// </summary>
    public int PaymentMethod { get; set; }
    /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }
    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 订单明细列表（主子表关系，一个订单可以有多个明细）（外键在子表 TaktPurchaseOrderItemDto.PurchaseOrderId）
    /// </summary>
    public List<TaktPurchaseOrderItemDto>? Items { get; set; }

    /// <summary>
    /// 采购订单变更记录列表（外键在子表 ）（外键在子表 TaktPurchaseOrderChangeLogDto.PurchaseOrderId）
    /// </summary>
    public List<TaktPurchaseOrderChangeLogDto>? ChangeLogs { get; set; }
}

/// <summary>
/// 采购订单表查询DTO
/// </summary>
public partial class TaktPurchaseOrderQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 订单编码
    /// </summary>
    public string? OrderCode { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }
    /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// 订单日期开始时间
    /// </summary>
    public DateTime? OrderDateStart { get; set; }
    /// <summary>
    /// 订单日期结束时间
    /// </summary>
    public DateTime? OrderDateEnd { get; set; }
    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 要求到货日期开始时间
    /// </summary>
    public DateTime? RequiredArrivalDateStart { get; set; }
    /// <summary>
    /// 要求到货日期结束时间
    /// </summary>
    public DateTime? RequiredArrivalDateEnd { get; set; }
    /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

    /// <summary>
    /// 实际到货日期开始时间
    /// </summary>
    public DateTime? ActualArrivalDateStart { get; set; }
    /// <summary>
    /// 实际到货日期结束时间
    /// </summary>
    public DateTime? ActualArrivalDateEnd { get; set; }
    /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }
    /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal? TotalQuantity { get; set; }
    /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal? TotalAmount { get; set; }
    /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal? DiscountAmount { get; set; }
    /// <summary>
    /// 税费
    /// </summary>
    public decimal? TaxAmount { get; set; }
    /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal? ActualAmount { get; set; }
    /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal? ReceivedQuantity { get; set; }
    /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal? ReceivedAmount { get; set; }
    /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal? PaidAmount { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public int? OrderStatus { get; set; }
    /// <summary>
    /// 交货状态
    /// </summary>
    public int? DeliveryStatus { get; set; }
    /// <summary>
    /// 支付方式
    /// </summary>
    public int? PaymentMethod { get; set; }
    /// <summary>
    /// 交货方式
    /// </summary>
    public int? DeliveryMethod { get; set; }
    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

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
/// Takt创建采购订单表DTO
/// </summary>
public partial class TaktPurchaseOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderCreateDto()
    {
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

        /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal PaidAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 支付方式
    /// </summary>
    public int PaymentMethod { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 订单明细列表（主子表关系，一个订单可以有多个明细）（外键在子表 TaktPurchaseOrderItemCreateDto.PurchaseOrderId）
    /// </summary>
    public List<TaktPurchaseOrderItemCreateDto>? Items { get; set; }


    /// <summary>
    /// 采购订单变更记录列表（外键在子表 ）（外键在子表 TaktPurchaseOrderChangeLogCreateDto.PurchaseOrderId）
    /// </summary>
    public List<TaktPurchaseOrderChangeLogCreateDto>? ChangeLogs { get; set; }

}

/// <summary>
/// Takt更新采购订单表DTO
/// </summary>
public partial class TaktPurchaseOrderUpdateDto : TaktPurchaseOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderUpdateDto()
    {
    }

        /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; } = 0;
}

/// <summary>
/// 采购订单表订单状态DTO
/// </summary>
public partial class TaktPurchaseOrderOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderOrderStatusDto()
    {
    }

        /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; } = 0;

    /// <summary>
    /// 订单状态（0=禁用，1=启用）
    /// </summary>
    public int OrderStatus { get; set; }
}

/// <summary>
/// 采购订单表交货状态DTO
/// </summary>
public partial class TaktPurchaseOrderDeliveryStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderDeliveryStatusDto()
    {
    }

        /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; } = 0;

    /// <summary>
    /// 交货状态（0=禁用，1=启用）
    /// </summary>
    public int DeliveryStatus { get; set; }
}

/// <summary>
/// 采购订单表导入模板DTO
/// </summary>
public partial class TaktPurchaseOrderTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderTemplateDto()
    {
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

        /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal PaidAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 支付方式
    /// </summary>
    public int PaymentMethod { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

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
/// 采购订单表导入DTO
/// </summary>
public partial class TaktPurchaseOrderImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderImportDto()
    {
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

        /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal PaidAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 支付方式
    /// </summary>
    public int PaymentMethod { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

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
/// 采购订单表导出DTO
/// </summary>
public partial class TaktPurchaseOrderExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderExportDto()
    {
        CreatedAt = DateTime.Now;
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

        /// <summary>
    /// 采购组代码
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 订单总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 订单总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 折扣金额
    /// </summary>
    public decimal DiscountAmount { get; set; }

        /// <summary>
    /// 税费
    /// </summary>
    public decimal TaxAmount { get; set; }

        /// <summary>
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }

        /// <summary>
    /// 已入库数量
    /// </summary>
    public decimal ReceivedQuantity { get; set; }

        /// <summary>
    /// 已入库金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 已付款金额
    /// </summary>
    public decimal PaidAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 支付方式
    /// </summary>
    public int PaymentMethod { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}