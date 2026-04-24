// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseOrderDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：采购订单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

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
        PurchaseUserName = string.Empty;
    }

    /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }
    /// <summary>
    /// 采购申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }
    /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierId { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }
    /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }
    /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }
    /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }
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
    /// 采购员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseUserId { get; set; }
    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }
    /// <summary>
    /// 采购部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseDeptId { get; set; }
    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }
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
    /// 支付状态
    /// </summary>
    public int PaymentStatus { get; set; }
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
    /// 订单明细列表（主子表关系，一个订单可以有多个明细）
    /// </summary>
    public List<long>? ItemIds { get; set; }
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
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 订单编码
    /// </summary>
    public string? OrderCode { get; set; }
    /// <summary>
    /// 采购申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }
    /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SupplierId { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }
    /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }
    /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }
    /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }
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
    /// 采购员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseUserId { get; set; }
    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string? PurchaseUserName { get; set; }
    /// <summary>
    /// 采购部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseDeptId { get; set; }
    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }
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
    /// 支付状态
    /// </summary>
    public int? PaymentStatus { get; set; }
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
        PurchaseUserName = string.Empty;
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
    /// 采购申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }

        /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }

        /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }

        /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }

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
    /// 采购员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseUserId { get; set; }

        /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

        /// <summary>
    /// 采购部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseDeptId { get; set; }

        /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

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
    /// 支付状态
    /// </summary>
    public int PaymentStatus { get; set; }

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
    public long PurchaseOrderId { get; set; }
}

/// <summary>
/// 采购订单表订单状态DTO
/// </summary>
public partial class TaktPurchaseOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderStatusDto()
    {
    }

        /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

    /// <summary>
    /// 订单状态（0=禁用，1=启用）
    /// </summary>
    public int OrderStatus { get; set; }
}

/// <summary>
/// 采购订单表支付状态DTO
/// </summary>
public partial class TaktPurchaseOrderPaymentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderPaymentStatusDto()
    {
    }

        /// <summary>
    /// 采购订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

    /// <summary>
    /// 支付状态（0=禁用，1=启用）
    /// </summary>
    public int PaymentStatus { get; set; }
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
        PurchaseUserName = string.Empty;
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
    /// 采购申请ID
    /// </summary>
    public long? RequestId { get; set; }

        /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }

        /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }

        /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }

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
    /// 采购员ID
    /// </summary>
    public long PurchaseUserId { get; set; }

        /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

        /// <summary>
    /// 采购部门ID
    /// </summary>
    public long? PurchaseDeptId { get; set; }

        /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

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
    /// 支付状态
    /// </summary>
    public int PaymentStatus { get; set; }

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
        PurchaseUserName = string.Empty;
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
    /// 采购申请ID
    /// </summary>
    public long? RequestId { get; set; }

        /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }

        /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }

        /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }

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
    /// 采购员ID
    /// </summary>
    public long PurchaseUserId { get; set; }

        /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

        /// <summary>
    /// 采购部门ID
    /// </summary>
    public long? PurchaseDeptId { get; set; }

        /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

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
    /// 支付状态
    /// </summary>
    public int PaymentStatus { get; set; }

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
        PurchaseUserName = string.Empty;
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
    /// 采购申请ID
    /// </summary>
    public long? RequestId { get; set; }

        /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }

        /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }

        /// <summary>
    /// 供应商地址
    /// </summary>
    public string? SupplierAddress { get; set; }

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
    /// 采购员ID
    /// </summary>
    public long PurchaseUserId { get; set; }

        /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

        /// <summary>
    /// 采购部门ID
    /// </summary>
    public long? PurchaseDeptId { get; set; }

        /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

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
    /// 支付状态
    /// </summary>
    public int PaymentStatus { get; set; }

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