// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：采购订单和价格DTO业务扩展字段
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// Takt采购订单DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchaseOrderDto
{
    /// <summary>
    /// 订单ID（非数据库字段，适配字段）
    /// </summary>
    public long? OrderId { get; set; }
    
    /// <summary>
    /// 订单明细列表（非数据库字段）
    /// </summary>
    public List<TaktPurchaseOrderItemDto>? Items { get; set; }
}

/// <summary>
/// Takt采购订单创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchaseOrderCreateDto
{
    /// <summary>
    /// 订单明细列表（非数据库字段）
    /// </summary>
    public List<TaktPurchaseOrderItemDto>? Items { get; set; }
}

/// <summary>
/// Takt采购订单更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchaseOrderUpdateDto
{
    // Items 已从 TaktPurchaseOrderCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt采购订单状态DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchaseOrderStatusDto
{
    /// <summary>
    /// 订单ID（非数据库字段，适配字段）
    /// </summary>
    public long? OrderId { get; set; }
}

/// <summary>
/// Takt采购价格DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceDto
{
    /// <summary>
    /// 价格ID（非数据库字段，适配字段）
    /// </summary>
    public long? PriceId { get; set; }
    
    /// <summary>
    /// 价格明细列表（非数据库字段）
    /// </summary>
    public List<TaktPurchasePriceItemDto>? Items { get; set; }
}

/// <summary>
/// Takt采购价格创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceCreateDto
{
    /// <summary>
    /// 价格明细列表（非数据库字段）
    /// </summary>
    public List<TaktPurchasePriceItemDto>? Items { get; set; }
}

/// <summary>
/// Takt采购价格更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceUpdateDto
{
    // Items 已从 TaktPurchasePriceCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt采购价格状态DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceStatusDto
{
    /// <summary>
    /// 价格ID（非数据库字段，适配字段）
    /// </summary>
    public long? PriceId { get; set; }
}

/// <summary>
/// Takt采购价格明细DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceItemDto
{
    /// <summary>
    /// 明细ID（非数据库字段，适配字段）
    /// </summary>
    public long? ItemId { get; set; }
    
    /// <summary>
    /// 规格列表（非数据库字段）
    /// </summary>
    public List<TaktPurchasePriceScaleDto>? Scales { get; set; }
}

/// <summary>
/// Takt采购价格导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchasePriceExportDto
{
    /// <summary>
    /// 价格类型字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? PriceTypeString { get; set; }
    
    /// <summary>
    /// 价格状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? PriceStatusString { get; set; }
    
    /// <summary>
    /// 是否启用字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? IsEnabledString { get; set; }
}

/// <summary>
/// Takt采购订单导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPurchaseOrderExportDto
{
    /// <summary>
    /// 订单状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? OrderStatusString { get; set; }
    
    /// <summary>
    /// 支付状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? PaymentStatusString { get; set; }
    
    /// <summary>
    /// 支付方式字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? PaymentMethodString { get; set; }
    
    /// <summary>
    /// 交货方式字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? DeliveryMethodString { get; set; }
}
