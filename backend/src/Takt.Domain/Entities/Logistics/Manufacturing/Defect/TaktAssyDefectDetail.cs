// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetail.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：组立不良明细实体，记录不良区分、数量、随机卡号、发生工程、测试步骤、症状、个所、原因、修理员等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Defect;

/// <summary>
/// 组立不良明细实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_defect_assy_defect_detail", "组立不良明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_defect_assy_defect_detail_assy_defect_id", nameof(AssyDefectId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_defect_assy_defect_detail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_defect_assy_defect_detail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAssyDefectDetail : TaktEntityBase
{
    /// <summary>
    /// 组立不良日报ID（主表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "assy_defect_id", ColumnDescription = "组立不良ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AssyDefectId { get; set; }

    /// <summary>
    /// 不良区分
    /// </summary>
    [SugarColumn(ColumnName = "defect_category", ColumnDescription = "不良区分", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectCategory { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "defect_qty", ColumnDescription = "不良数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal DefectQty { get; set; } = 0;

    /// <summary>
    /// 累计不良
    /// </summary>
    [SugarColumn(ColumnName = "cumulative_defect_qty", ColumnDescription = "累计不良", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal CumulativeDefectQty { get; set; } = 0;

    /// <summary>
    /// 随机卡号
    /// </summary>
    [SugarColumn(ColumnName = "random_card_no", ColumnDescription = "随机卡号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? RandomCardNo { get; set; }

    /// <summary>
    /// 发生工程
    /// </summary>
    [SugarColumn(ColumnName = "occurrence_engineering", ColumnDescription = "发生工程", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? OccurrenceEngineering { get; set; }

    /// <summary>
    /// 测试步骤
    /// </summary>
    [SugarColumn(ColumnName = "test_step", ColumnDescription = "测试步骤", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? TestStep { get; set; }

    /// <summary>
    /// 不良症状
    /// </summary>
    [SugarColumn(ColumnName = "defect_symptom", ColumnDescription = "不良症状", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectSymptom { get; set; }

    /// <summary>
    /// 不良个所
    /// </summary>
    [SugarColumn(ColumnName = "defect_location", ColumnDescription = "不良个所", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectLocation { get; set; }

    /// <summary>
    /// 不良原因
    /// </summary>
    [SugarColumn(ColumnName = "defect_reason", ColumnDescription = "不良原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DefectReason { get; set; }

    /// <summary>
    /// 修理员
    /// </summary>
    [SugarColumn(ColumnName = "repair_operator", ColumnDescription = "修理员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? RepairOperator { get; set; }

    /// <summary>
    /// 状态(0=正常 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 组立不良日报（主表）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(AssyDefectId))]
    public TaktAssyDefect? AssyDefect { get; set; }
}
