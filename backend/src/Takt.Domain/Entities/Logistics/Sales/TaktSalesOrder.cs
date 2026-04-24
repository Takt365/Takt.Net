// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Sales
// 文件名称：TaktSalesOrder.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt销售订单实体，定义销售订单领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Sales;

/// <summary>
/// Takt销售订单实体
/// </summary>
[SugarTable("takt_logistics_sales_order", "销售订单表")]
[SugarIndex("ix_takt_logistics_sales_order_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_order_code", nameof(OrderCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_sales_order_customer_id", nameof(CustomerId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_order_date", nameof(OrderDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_sales_order_order_status", nameof(OrderStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_sales_user_id", nameof(SalesUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalesOrder : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 订单编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "order_code", ColumnDescription = "订单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "customer_id", ColumnDescription = "客户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CustomerId { get; set; }

    /// <summary>
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户名称
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 客户联系人
    /// </summary>
    [SugarColumn(ColumnName = "customer_contact", ColumnDescription = "客户联系人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerContact { get; set; }

    /// <summary>
    /// 客户联系电话
    /// </summary>
    [SugarColumn(ColumnName = "customer_phone", ColumnDescription = "客户联系电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerPhone { get; set; }

    /// <summary>
    /// 客户地址
    /// </summary>
    [SugarColumn(ColumnName = "customer_address", ColumnDescription = "客户地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// 订单日期
    /// </summary>
    [SugarColumn(ColumnName = "order_date", ColumnDescription = "订单日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求交货日期
    /// </summary>
    [SugarColumn(ColumnName = "required_delivery_date", ColumnDescription = "要求交货日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RequiredDeliveryDate { get; set; }

    /// <summary>
    /// 实际交货日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_delivery_date", ColumnDescription = "实际交货日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualDeliveryDate { get; set; }

    /// <summary>
    /// 销售员ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "sales_user_id", ColumnDescription = "销售员ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SalesUserId { get; set; }

    /// <summary>
    /// 销售员姓名
    /// </summary>
    [SugarColumn(ColumnName = "sales_user_name", ColumnDescription = "销售员姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SalesUserName { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "sales_dept_id", ColumnDescription = "销售部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? SalesDeptId { get; set; }

    /// <summary>
    /// 销售部门名称
    /// </summary>
    [SugarColumn(ColumnName = "sales_dept_name", ColumnDescription = "销售部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? SalesDeptName { get; set; }

    /// <summary>
    /// 订单总数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "订单总数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 订单总金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "total_amount", ColumnDescription = "订单总金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// 折扣金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DiscountAmount { get; set; } = 0;

    /// <summary>
    /// 税费（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxAmount { get; set; } = 0;

    /// <summary>
    /// 订单实付金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "actual_amount", ColumnDescription = "订单实付金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualAmount { get; set; } = 0;

    /// <summary>
    /// 已发货数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "shipped_quantity", ColumnDescription = "已发货数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ShippedQuantity { get; set; } = 0;

    /// <summary>
    /// 已发货金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "shipped_amount", ColumnDescription = "已发货金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ShippedAmount { get; set; } = 0;

    /// <summary>
    /// 已收款金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "received_amount", ColumnDescription = "已收款金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ReceivedAmount { get; set; } = 0;

    /// <summary>
    /// 订单状态（0=草稿，1=待审核，2=已审核，3=已发货，4=已完成，5=已取消，6=已关闭）
    /// </summary>
    [SugarColumn(ColumnName = "order_status", ColumnDescription = "订单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderStatus { get; set; } = 0;

    /// <summary>
    /// 支付状态（0=未支付，1=部分支付，2=已支付）
    /// </summary>
    [SugarColumn(ColumnName = "payment_status", ColumnDescription = "支付状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PaymentStatus { get; set; } = 0;

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用卡，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "payment_method", ColumnDescription = "支付方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=送货上门，2=物流配送，3=快递）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_method", ColumnDescription = "交货方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    [SugarColumn(ColumnName = "delivery_address", ColumnDescription = "交货地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? DeliveryAddress { get; set; }
}
