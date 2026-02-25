// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Oph
// 文件名称：TaktAssyOutput.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报（产出）主表实体，按工厂、生产日期、生产线等记录生产订单与标准产能
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Oph;

/// <summary>
/// 组立日报（产出）主表实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_oph_assy_output", "组立日报表")]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_prod_date", nameof(ProdDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_prod_line", nameof(ProdLine), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_prod_order_code", nameof(ProdOrderCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_status", nameof(Status), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_oph_assy_output_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAssyOutput : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 生产类别
    /// RD: 研发  EVT: 工程验证测试  DVT: 设计验证测试  EPP: 工程试产  PP: 试产  FPP: 正式生产  MP: 大规模生产  RPR: 维修生产  RWR: 返工生产
    /// </summary>
    [SugarColumn(ColumnName = "prod_category", ColumnDescription = "生产类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ProdCategory { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "prod_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ProdDate { get; set; }

    /// <summary>
    /// 生产线
    /// </summary>
    [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ProdLine { get; set; } = string.Empty;

    /// <summary>
    /// 直接人员
    /// </summary>
    [SugarColumn(ColumnName = "direct_labor", ColumnDescription = "直接人员", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DirectLabor { get; set; } = 0;

    /// <summary>
    /// 间接人员
    /// </summary>
    [SugarColumn(ColumnName = "indirect_labor", ColumnDescription = "间接人员", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IndirectLabor { get; set; } = 0;

    /// <summary>
    /// 班次(1=早班 2=中班 3=晚班)
    /// </summary>
    [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ShiftNo { get; set; } = 1;

    /// <summary>
    /// 生产订单类型
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_type", ColumnDescription = "生产订单类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ProdOrderType { get; set; }

    /// <summary>
    /// 生产工单号
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ProdOrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 机种
    /// </summary>
    [SugarColumn(ColumnName = "model_code", ColumnDescription = "机种", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ModelCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? BatchNo { get; set; }

    /// <summary>
    /// 订单数量
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_qty", ColumnDescription = "订单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
    public decimal ProdOrderQty { get; set; } = 0;

    /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "std_minutes", ColumnDescription = "标准工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
    public decimal StdMinutes { get; set; } = 0;

    /// <summary>
    /// 标准产能
    /// </summary>
    [SugarColumn(ColumnName = "std_capacity", ColumnDescription = "标准产能", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
    public decimal StdCapacity { get; set; } = 0;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 组立日报明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktAssyOutputDetail.AssyOutputId))]
    public List<TaktAssyOutputDetail>? AssyOutputDetails { get; set; }
}
