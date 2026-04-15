// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Ecn
// 文件名称：TaktEcnDetail.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：设变（ECN）子表实体，参照 Ec_ 子表字段：BOM 编号/日期、变更内容、新旧料号/数量/单位、采购、位置、录入日期等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变（ECN）子表实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_ecn_detail", "设变明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_detail_ecn_id", nameof(EcnId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_detail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_detail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEcDetail : TaktEntityBase
{
    /// <summary>
    /// 设变主表ID（主表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "ecn_id", ColumnDescription = "设变ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EcnId { get; set; }

    /// <summary>
    /// 设变编号（Ec_no）
    /// </summary>
    [SugarColumn(ColumnName = "ec_no", ColumnDescription = "设变编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnNo { get; set; }

    /// <summary>
    /// 型号（Ec_model）
    /// </summary>
    [SugarColumn(ColumnName = "ec_model", ColumnDescription = "型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnModel { get; set; }

    /// <summary>
    /// BOM 主项料号（Ec_bomitem）
    /// </summary>
    [SugarColumn(ColumnName = "ec_bomitem", ColumnDescription = "BOM主项料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnBomItem { get; set; }

    /// <summary>
    /// BOM 子项料号（Ec_bomsubitem）
    /// </summary>
    [SugarColumn(ColumnName = "ec_bomsubitem", ColumnDescription = "BOM子项料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnBomSubItem { get; set; }

    /// <summary>
    /// BOM 编号（Ec_bomno）
    /// </summary>
    [SugarColumn(ColumnName = "ec_bomno", ColumnDescription = "BOM编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnBomNo { get; set; }

    /// <summary>
    /// 变更内容（Ec_change）
    /// </summary>
    [SugarColumn(ColumnName = "ec_change", ColumnDescription = "变更内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnChange { get; set; }

    /// <summary>
    /// 本地/现场（Ec_local）
    /// </summary>
    [SugarColumn(ColumnName = "ec_local", ColumnDescription = "本地现场", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnLocal { get; set; }

    /// <summary>
    /// 备注（Ec_note）
    /// </summary>
    [SugarColumn(ColumnName = "ec_note", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnNote { get; set; }

    /// <summary>
    /// 工序（Ec_process）
    /// </summary>
    [SugarColumn(ColumnName = "ec_process", ColumnDescription = "工序", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnProcess { get; set; }

    /// <summary>
    /// BOM 日期（Ec_bomdate）
    /// </summary>
    [SugarColumn(ColumnName = "ec_bomdate", ColumnDescription = "BOM日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EcnBomDate { get; set; }

    /// <summary>
    /// 录入日期（Ec_entrydate）
    /// </summary>
    [SugarColumn(ColumnName = "ec_entrydate", ColumnDescription = "录入日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EcnEntryDate { get; set; }

    /// <summary>
    /// 旧料号（Ec_olditem）
    /// </summary>
    [SugarColumn(ColumnName = "ec_olditem", ColumnDescription = "旧料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnOldItem { get; set; }

    /// <summary>
    /// 旧料号描述（Ec_oldtext）
    /// </summary>
    [SugarColumn(ColumnName = "ec_oldtext", ColumnDescription = "旧料号描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnOldText { get; set; }

    /// <summary>
    /// 旧数量（Ec_oldqty）
    /// </summary>
    [SugarColumn(ColumnName = "ec_oldqty", ColumnDescription = "旧数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = true)]
    public decimal? EcnOldQty { get; set; }

    /// <summary>
    /// 旧单位/设置（Ec_oldset）
    /// </summary>
    [SugarColumn(ColumnName = "ec_oldset", ColumnDescription = "旧单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnOldSet { get; set; }

    /// <summary>
    /// 新料号（Ec_newitem）
    /// </summary>
    [SugarColumn(ColumnName = "ec_newitem", ColumnDescription = "新料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnNewItem { get; set; }

    /// <summary>
    /// 新料号描述（Ec_newtext）
    /// </summary>
    [SugarColumn(ColumnName = "ec_newtext", ColumnDescription = "新料号描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnNewText { get; set; }

    /// <summary>
    /// 新数量（Ec_newqty）
    /// </summary>
    [SugarColumn(ColumnName = "ec_newqty", ColumnDescription = "新数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = true)]
    public decimal? EcnNewQty { get; set; }

    /// <summary>
    /// 新单位/设置（Ec_newset）
    /// </summary>
    [SugarColumn(ColumnName = "ec_newset", ColumnDescription = "新单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnNewSet { get; set; }

    /// <summary>
    /// 是否采购（0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "ec_is_procurement", ColumnDescription = "是否采购", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsProcurement { get; set; } = 0;

    /// <summary>
    /// 是否检查（IsCheck）
    /// </summary>
    [SugarColumn(ColumnName = "is_check", ColumnDescription = "是否检查", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? IsCheck { get; set; }

    /// <summary>
    /// 仓库（Ec_warehouse）
    /// </summary>
    [SugarColumn(ColumnName = "ec_warehouse", ColumnDescription = "仓库", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnWarehouse { get; set; }

    /// <summary>
    /// EOL（End of Line，0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "ec_is_end_of_line", ColumnDescription = "EOL", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsEndOfLine { get; set; } = 0;    

    /// <summary>
    /// 设变主表
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EcnId))]
    public TaktEc? Ecn { get; set; }

    /// <summary>
    /// 设变明细-部门记录列表（按 DeptCode 区分部门：Assy/It/Cus/Fins/Gas/Iqc/Mc/Mp/Pcba/Pmc/Qa/Te/Eng）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEcDept.EcnDetailId))]
    public List<TaktEcDept>? DeptRecords { get; set; }
}
