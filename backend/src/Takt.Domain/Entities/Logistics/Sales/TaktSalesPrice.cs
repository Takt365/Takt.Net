// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Sales
// 文件名称：TaktSalesPrice.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt销售价格实体，定义销售价格领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Sales;

/// <summary>
/// Takt销售价格实体（客户价格主表，一个客户可以有多个物料价格）
/// </summary>
[SugarTable("takt_logistics_sales_price", "销售价格表")]
[SugarIndex("ix_takt_logistics_sales_price_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_customer_code", nameof(CustomerCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_price_type", nameof(PriceType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_effective_start_date", nameof(EffectiveStartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_effective_end_date", nameof(EffectiveEndDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_price_status", nameof(PriceStatus), OrderByType.Asc)]
public class TaktSalesPrice : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 客户编码（如果为空则表示通用价格）
    /// </summary>
    [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerCode { get; set; }

    /// <summary>
    /// 价格类型（0=标准价格，1=客户价格，2=促销价格，3=合同价格，4=临时价格）
    /// </summary>
    [SugarColumn(ColumnName = "price_type", ColumnDescription = "价格类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_start_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveStartDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（空表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "effective_end_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EffectiveEndDate { get; set; }

    /// <summary>
    /// 价格状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "price_status", ColumnDescription = "价格状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PriceStatus { get; set; } = 1;

    /// <summary>
    /// 物料价格明细列表（主子表关系，一个客户价格可以有多个物料价格）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktSalesPriceItem.SalesPriceId))]
    public List<TaktSalesPriceItem>? Items { get; set; }

    /// <summary>
    /// 销售价格变更记录列表（外键在子表 <see cref="TaktSalesPriceChangeLog.PriceId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktSalesPriceChangeLog.SalesPriceId))]
    public List<TaktSalesPriceChangeLog>? ChangeLogs { get; set; }
}
