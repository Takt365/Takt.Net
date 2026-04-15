// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logistics.Material
// 文件名称：TaktPurchaseOrderDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单DTO，包含采购订单相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// Takt采购订单DTO（主表）
/// </summary>
public class TaktPurchaseOrderDto : TaktDtoBase
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
        ConfigId = "0";
        Items = new List<TaktPurchaseOrderItemDto>();
    }

    /// <summary>
    /// 订单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrderId { get; set; }

    /// <summary>
    /// 订单编码（唯一索引）
    /// </summary>
    public string OrderCode { get; set; }

    /// <summary>
    /// 采购申请ID（序列化为string以避免Javascript精度问题，如果为空则表示直接采购）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }

    /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

    /// <summary>
    /// 供应商ID（序列化为string以避免Javascript精度问题）
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
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 实际到货日期
    /// </summary>
    public DateTime? ActualArrivalDate { get; set; }

    /// <summary>
    /// 采购员ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseUserId { get; set; }

    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

    /// <summary>
    /// 采购部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseDeptId { get; set; }

    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

    /// <summary>
    /// 订单总数量（基本单位数量）
    /// </summary>
    public decimal TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 订单总金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// 折扣金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal DiscountAmount { get; set; } = 0;

    /// <summary>
    /// 税费（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal TaxAmount { get; set; } = 0;

    /// <summary>
    /// 订单实付金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal ActualAmount { get; set; } = 0;

    /// <summary>
    /// 已入库数量（基本单位数量）
    /// </summary>
    public decimal ReceivedQuantity { get; set; } = 0;

    /// <summary>
    /// 已入库金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal ReceivedAmount { get; set; } = 0;

    /// <summary>
    /// 已付款金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal PaidAmount { get; set; } = 0;

    /// <summary>
    /// 订单状态（0=草稿，1=待审核，2=已审核，3=已入库，4=已完成，5=已取消，6=已关闭）
    /// </summary>
    public int OrderStatus { get; set; } = 0;

    /// <summary>
    /// 支付状态（0=未支付，1=部分支付，2=已支付）
    /// </summary>
    public int PaymentStatus { get; set; } = 0;

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用证，4=其他）
    /// </summary>
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=供应商送货，2=物流配送，3=快递）
    /// </summary>
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 订单明细列表（主子表关系）
    /// </summary>
    public List<TaktPurchaseOrderItemDto> Items { get; set; }
}

/// <summary>
/// Takt采购订单明细DTO（子表）
/// </summary>
public class TaktPurchaseOrderItemDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemDto()
    {
        OrderCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = "个";
    }

    /// <summary>
    /// 明细ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ItemId { get; set; }

    /// <summary>
    /// 订单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrderId { get; set; }

    /// <summary>
    /// 订单编码
    /// </summary>
    public string OrderCode { get; set; }

    /// <summary>
    /// 物料ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaterialId { get; set; }

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
    /// 订购数量（基本单位数量）
    /// </summary>
    public decimal OrderQuantity { get; set; } = 0;

    /// <summary>
    /// 已入库数量（基本单位数量）
    /// </summary>
    public decimal ReceivedQuantity { get; set; } = 0;

    /// <summary>
    /// 单价（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal UnitPrice { get; set; } = 0;

    /// <summary>
    /// 折扣率（0-100，表示折扣百分比）
    /// </summary>
    public decimal DiscountRate { get; set; } = 0;

    /// <summary>
    /// 折扣金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal DiscountAmount { get; set; } = 0;

    /// <summary>
    /// 税费率（0-100，表示税费百分比）
    /// </summary>
    public decimal TaxRate { get; set; } = 0;

    /// <summary>
    /// 税费（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal TaxAmount { get; set; } = 0;

    /// <summary>
    /// 小计金额（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal SubtotalAmount { get; set; } = 0;

    /// <summary>
    /// 行号（订单明细行号）
    /// </summary>
    public int LineNumber { get; set; } = 0;
}

/// <summary>
/// Takt采购订单查询DTO
/// </summary>
public class TaktPurchaseOrderQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在订单编码、供应商名称中模糊查询

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
    /// 采购申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }

    /// <summary>
    /// 采购员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseUserId { get; set; }

    /// <summary>
    /// 订单状态（0=草稿，1=待审核，2=已审核，3=已入库，4=已完成，5=已取消，6=已关闭）
    /// </summary>
    public int? OrderStatus { get; set; }

    /// <summary>
    /// 支付状态（0=未支付，1=部分支付，2=已支付）
    /// </summary>
    public int? PaymentStatus { get; set; }

    /// <summary>
    /// 订单日期开始
    /// </summary>
    public DateTime? OrderDateStart { get; set; }

    /// <summary>
    /// 订单日期结束
    /// </summary>
    public DateTime? OrderDateEnd { get; set; }
}

/// <summary>
/// Takt创建采购订单明细DTO（子表）
/// </summary>
public class TaktPurchaseOrderItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemCreateDto()
    {
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        PurchaseUnit = "个";
    }

    /// <summary>
    /// 物料ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaterialId { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

    /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; } = "个";

    /// <summary>
    /// 订购数量（基本单位数量）
    /// </summary>
    public decimal OrderQuantity { get; set; } = 0;

    /// <summary>
    /// 单价（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal UnitPrice { get; set; } = 0;

    /// <summary>
    /// 折扣率（0-100，表示折扣百分比）
    /// </summary>
    public decimal DiscountRate { get; set; } = 0;

    /// <summary>
    /// 税费率（0-100，表示税费百分比）
    /// </summary>
    public decimal TaxRate { get; set; } = 0;

    /// <summary>
    /// 行号（订单明细行号）
    /// </summary>
    public int LineNumber { get; set; } = 0;
}

/// <summary>
/// Takt创建采购订单DTO（主表）
/// </summary>
public class TaktPurchaseOrderCreateDto
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
        Items = new List<TaktPurchaseOrderItemCreateDto>();
    }

    /// <summary>
    /// 订单编码（唯一索引，如果为空则由系统生成）
    /// </summary>
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 采购申请ID（序列化为string以避免Javascript精度问题，如果为空则表示直接采购）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }

    /// <summary>
    /// 采购申请编码
    /// </summary>
    public string? RequestCode { get; set; }

    /// <summary>
    /// 供应商ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierId { get; set; }

    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; } = string.Empty;

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
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 采购员ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseUserId { get; set; }

    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; } = string.Empty;

    /// <summary>
    /// 采购部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PurchaseDeptId { get; set; }

    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用证，4=其他）
    /// </summary>
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=供应商送货，2=物流配送，3=快递）
    /// </summary>
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 订单明细列表（主子表关系）
    /// </summary>
    public List<TaktPurchaseOrderItemCreateDto> Items { get; set; }
}

/// <summary>
/// Takt更新采购订单DTO
/// </summary>
public class TaktPurchaseOrderUpdateDto : TaktPurchaseOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderUpdateDto()
    {
    }

    /// <summary>
    /// 订单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrderId { get; set; }
}

/// <summary>
/// Takt采购订单状态DTO
/// </summary>
public class TaktPurchaseOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderStatusDto()
    {
    }

    /// <summary>
    /// 订单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrderId { get; set; }

    /// <summary>
    /// 订单状态（0=草稿，1=待审核，2=已审核，3=已入库，4=已完成，5=已取消，6=已关闭）
    /// </summary>
    public int OrderStatus { get; set; }
}

/// <summary>
/// Takt采购订单模板DTO
/// </summary>
public class TaktPurchaseOrderTemplateDto
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
        Remark = string.Empty;
    }

    /// <summary>
    /// 订单编码（唯一索引）
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
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用证，4=其他）
    /// </summary>
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=供应商送货，2=物流配送，3=快递）
    /// </summary>
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt采购订单导入DTO
/// </summary>
public class TaktPurchaseOrderImportDto
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
        Remark = string.Empty;
    }

    /// <summary>
    /// 订单编码（唯一索引）
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
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

    /// <summary>
    /// 采购部门名称
    /// </summary>
    public string? PurchaseDeptName { get; set; }

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用证，4=其他）
    /// </summary>
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=供应商送货，2=物流配送，3=快递）
    /// </summary>
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt采购订单导出DTO
/// </summary>
public class TaktPurchaseOrderExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderExportDto()
    {
        OrderCode = string.Empty;
        SupplierCode = string.Empty;
        SupplierName = string.Empty;
        PurchaseUserName = string.Empty;
        OrderStatus = string.Empty;
        PaymentStatus = string.Empty;
        PaymentMethod = string.Empty;
        DeliveryMethod = string.Empty;
        CreatedAt = DateTime.Now;
    }

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
    /// 供应商联系人
    /// </summary>
    public string? SupplierContact { get; set; }

    /// <summary>
    /// 供应商联系电话
    /// </summary>
    public string? SupplierPhone { get; set; }

    /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 采购员姓名
    /// </summary>
    public string PurchaseUserName { get; set; }

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
    /// 订单实付金额
    /// </summary>
    public decimal ActualAmount { get; set; }

    /// <summary>
    /// 订单状态
    /// </summary>
    public string OrderStatus { get; set; }

    /// <summary>
    /// 支付状态
    /// </summary>
    public string PaymentStatus { get; set; }

    /// <summary>
    /// 支付方式
    /// </summary>
    public string PaymentMethod { get; set; }

    /// <summary>
    /// 交货方式
    /// </summary>
    public string DeliveryMethod { get; set; }

    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
