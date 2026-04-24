// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetail.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA改修明细实体，记录板别、生产实绩、不良症状、检出工程、责任归属等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Defect;

/// <summary>
/// PCBA改修明细实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_defect_pcba_repair_detail", "PCBA改修明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_defect_pcba_repair_detail_pcba_repair_id", nameof(PcbaRepairId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_defect_pcba_repair_detail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_defect_pcba_repair_detail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPcbaRepairDetail : TaktEntityBase
{
    /// <summary>
    /// PCBA改修日报ID（主表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "pcba_repair_id", ColumnDescription = "PCBA改修ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PcbaRepairId { get; set; }

    /// <summary>
    /// PCBA板别
    /// </summary>
    [SugarColumn(ColumnName = "pcba_board_type", ColumnDescription = "PCBA板别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? PcbaBoardType { get; set; }

    /// <summary>
    /// 生产实绩
    /// </summary>
    [SugarColumn(ColumnName = "prod_actual_qty", ColumnDescription = "生产实绩", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal ProdActualQty { get; set; } = 0;

    /// <summary>
    /// 生产线
    /// </summary>
    [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ProdLine { get; set; }

    /// <summary>
    /// 卡号
    /// </summary>
    [SugarColumn(ColumnName = "card_no", ColumnDescription = "卡号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? CardNo { get; set; }

    /// <summary>
    /// 不良症状
    /// </summary>
    [SugarColumn(ColumnName = "defect_symptom", ColumnDescription = "不良症状", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectSymptom { get; set; }

    /// <summary>
    /// 检出工程
    /// </summary>
    [SugarColumn(ColumnName = "defect_engineering", ColumnDescription = "检出工程", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectEngineering { get; set; }

    /// <summary>
    /// 不良原因
    /// </summary>
    [SugarColumn(ColumnName = "defect_reason", ColumnDescription = "不良原因", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectReason { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "defect_qty", ColumnDescription = "不良数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal DefectQty { get; set; } = 0;

    /// <summary>
    /// 责任归属
    /// </summary>
    [SugarColumn(ColumnName = "defect_responsibility", ColumnDescription = "责任归属", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectResponsibility { get; set; }

    /// <summary>
    /// 不良性质
    /// </summary>
    [SugarColumn(ColumnName = "defect_nature", ColumnDescription = "不良性质", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectNature { get; set; }

    /// <summary>
    /// 修理员
    /// </summary>
    [SugarColumn(ColumnName = "repair_operator", ColumnDescription = "修理员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? RepairOperator { get; set; }

    /// <summary>
    /// PCBA改修日报（主表）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(PcbaRepairId))]
    public TaktPcbaRepair? PcbaRepair { get; set; }
}
