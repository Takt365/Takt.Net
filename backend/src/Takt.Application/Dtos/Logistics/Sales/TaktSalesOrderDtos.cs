// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Sales
// 文件名称：TaktSalesOrderDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：销售订单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Sales;

/// <summary>
/// 销售订单表Dto
/// </summary>
public partial class TaktSalesOrderDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderDto()
    {
        SalesOrderCode = string.Empty;
        CustomerCode = string.Empty;
        CustomerName = string.Empty;
    }

    /// <summary>
    /// 销售订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesOrderId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 销售订单编码
    /// </summary>
    public string SalesOrderCode { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string CustomerCode { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }
    /// <summary>
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }
    /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }
    /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }
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
    /// 已发货数量
    /// </summary>
    public decimal ShippedQuantity { get; set; }
    /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal ShippedAmount { get; set; }
    /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }
    /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }
    /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }
    /// <summary>
    /// 收款方式
    /// </summary>
    public int PaymentMethod { get; set; }
    /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 销售订单明细列表（主子表关系，一个订单可以有多个明细）（外键在子表 TaktSalesOrderItemDto.SalesOrderId）
    /// </summary>
    public List<TaktSalesOrderItemDto>? Items { get; set; }

    /// <summary>
    /// 销售订单变更记录列表（外键在子表 ）（外键在子表 TaktSalesOrderChangeLogDto.SalesOrderId）
    /// </summary>
    public List<TaktSalesOrderChangeLogDto>? ChangeLogs { get; set; }
}

/// <summary>
/// 销售订单表查询DTO
/// </summary>
public partial class TaktSalesOrderQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 销售订单编码
    /// </summary>
    public string? SalesOrderCode { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string? CustomerName { get; set; }
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
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }

    /// <summary>
    /// 要求交货日期开始时间
    /// </summary>
    public DateTime? RequiredDeliveryDateStart { get; set; }
    /// <summary>
    /// 要求交货日期结束时间
    /// </summary>
    public DateTime? RequiredDeliveryDateEnd { get; set; }
    /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

    /// <summary>
    /// 实际交货日期开始时间
    /// </summary>
    public DateTime? ActualDeliveryDateStart { get; set; }
    /// <summary>
    /// 实际交货日期结束时间
    /// </summary>
    public DateTime? ActualDeliveryDateEnd { get; set; }
    /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }
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
    /// 已发货数量
    /// </summary>
    public decimal? ShippedQuantity { get; set; }
    /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal? ShippedAmount { get; set; }
    /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal? ReceivedAmount { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public int? OrderStatus { get; set; }
    /// <summary>
    /// 交货状态
    /// </summary>
    public int? DeliveryStatus { get; set; }
    /// <summary>
    /// 交货方式
    /// </summary>
    public int? DeliveryMethod { get; set; }
    /// <summary>
    /// 收款方式
    /// </summary>
    public int? PaymentMethod { get; set; }
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
/// Takt创建销售订单表DTO
/// </summary>
public partial class TaktSalesOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderCreateDto()
    {
        SalesOrderCode = string.Empty;
        CustomerCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 销售订单编码
    /// </summary>
    public string SalesOrderCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string CustomerCode { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }

        /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }

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
    /// 已发货数量
    /// </summary>
    public decimal ShippedQuantity { get; set; }

        /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal ShippedAmount { get; set; }

        /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 收款方式
    /// </summary>
    public int PaymentMethod { get; set; }

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
    /// 销售订单明细列表（主子表关系，一个订单可以有多个明细）（外键在子表 TaktSalesOrderItemCreateDto.SalesOrderId）
    /// </summary>
    public List<TaktSalesOrderItemCreateDto>? Items { get; set; }


    /// <summary>
    /// 销售订单变更记录列表（外键在子表 ）（外键在子表 TaktSalesOrderChangeLogCreateDto.SalesOrderId）
    /// </summary>
    public List<TaktSalesOrderChangeLogCreateDto>? ChangeLogs { get; set; }

}

/// <summary>
/// Takt更新销售订单表DTO
/// </summary>
public partial class TaktSalesOrderUpdateDto : TaktSalesOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderUpdateDto()
    {
    }

        /// <summary>
    /// 销售订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesOrderId { get; set; } = 0;
}

/// <summary>
/// 销售订单表订单状态DTO
/// </summary>
public partial class TaktSalesOrderOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderOrderStatusDto()
    {
    }

        /// <summary>
    /// 销售订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesOrderId { get; set; } = 0;

    /// <summary>
    /// 订单状态（0=禁用，1=启用）
    /// </summary>
    public int OrderStatus { get; set; }
}

/// <summary>
/// 销售订单表交货状态DTO
/// </summary>
public partial class TaktSalesOrderDeliveryStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderDeliveryStatusDto()
    {
    }

        /// <summary>
    /// 销售订单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesOrderId { get; set; } = 0;

    /// <summary>
    /// 交货状态（0=禁用，1=启用）
    /// </summary>
    public int DeliveryStatus { get; set; }
}

/// <summary>
/// 销售订单表导入模板DTO
/// </summary>
public partial class TaktSalesOrderTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderTemplateDto()
    {
        SalesOrderCode = string.Empty;
        CustomerCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 销售订单编码
    /// </summary>
    public string SalesOrderCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string CustomerCode { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }

        /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }

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
    /// 已发货数量
    /// </summary>
    public decimal ShippedQuantity { get; set; }

        /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal ShippedAmount { get; set; }

        /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 收款方式
    /// </summary>
    public int PaymentMethod { get; set; }

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
/// 销售订单表导入DTO
/// </summary>
public partial class TaktSalesOrderImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderImportDto()
    {
        SalesOrderCode = string.Empty;
        CustomerCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 销售订单编码
    /// </summary>
    public string SalesOrderCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string CustomerCode { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }

        /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }

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
    /// 已发货数量
    /// </summary>
    public decimal ShippedQuantity { get; set; }

        /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal ShippedAmount { get; set; }

        /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 收款方式
    /// </summary>
    public int PaymentMethod { get; set; }

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
/// 销售订单表导出DTO
/// </summary>
public partial class TaktSalesOrderExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderExportDto()
    {
        CreatedAt = DateTime.Now;
        SalesOrderCode = string.Empty;
        CustomerCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 销售订单编码
    /// </summary>
    public string SalesOrderCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string CustomerCode { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 订单日期
    /// </summary>
    public DateTime OrderDate { get; set; }

        /// <summary>
    /// 要求交货日期
    /// </summary>
    public DateTime? RequiredDeliveryDate { get; set; }

        /// <summary>
    /// 实际交货日期
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
    /// 销售员
    /// </summary>
    public string? SalesBy { get; set; }

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
    /// 已发货数量
    /// </summary>
    public decimal ShippedQuantity { get; set; }

        /// <summary>
    /// 已发货金额
    /// </summary>
    public decimal ShippedAmount { get; set; }

        /// <summary>
    /// 已收款金额
    /// </summary>
    public decimal ReceivedAmount { get; set; }

        /// <summary>
    /// 订单状态
    /// </summary>
    public int OrderStatus { get; set; }

        /// <summary>
    /// 交货状态
    /// </summary>
    public int DeliveryStatus { get; set; }

        /// <summary>
    /// 交货方式
    /// </summary>
    public int DeliveryMethod { get; set; }

        /// <summary>
    /// 收款方式
    /// </summary>
    public int PaymentMethod { get; set; }

        /// <summary>
    /// 交货地址
    /// </summary>
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}