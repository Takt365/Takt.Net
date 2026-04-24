// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDept.cs
// 功能描述：设变-部门通用实体，按 DeptCode 区分部门；顺序：技术(Eng)、生管(Pmc)、采购(Mp)、Iqc、部管(Mc)、制二(Pcba)、制一(Assy)、Qa、制技(Te)。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变-部门通用实体。部门顺序（严格）：技术(Eng)、生管(Pmc)、采购(Mp)、Iqc、部管(Mc)、制二(Pcba)、制一(Assy)、Qa、制技(Te)。通过 DeptCode 区分。
/// </summary>
[SugarTable("takt_logistics_manufacturing_ecn_dept", "设变部门表")]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_dept_ecn_detail_id", nameof(EcnDetailId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_dept_dept_code", nameof(DeptCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_dept_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_dept_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEcDept : TaktEntityBase
{
    /// <summary>
    /// 设变明细ID（TaktEcDetail 主键）
    /// </summary>
    [SugarColumn(ColumnName = "ecn_detail_id", ColumnDescription = "设变明细ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EcnDetailId { get; set; }

    /// <summary>
    /// 部门编码。顺序严格为：Eng=技术, Pmc=生管, Mp=采购, Iqc=受检, Mc=部管, Pcba=制二, Assy=制一, Qa=品管, Te=制技。
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否实施（0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_implemented", ColumnDescription = "是否实施", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsImplemented { get; set; } = 0;

    /// <summary>
    /// 内容（各部门通用）
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "内容", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? Content { get; set; }

    // ---------- 技术(Eng)：无额外字段 ----------

    // ---------- 生管(Pmc) ----------
    /// <summary>预计生产日期</summary>
    [SugarColumn(ColumnName = "scheduled_production_date", ColumnDescription = "预计生产日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ScheduledProductionDate { get; set; }
    /// <summary>预定批次</summary>
    [SugarColumn(ColumnName = "scheduled_batch", ColumnDescription = "预定批次", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ScheduledBatch { get; set; }
    /// <summary>Po残（采购订单残）</summary>
    [SugarColumn(ColumnName = "po_remainder", ColumnDescription = "Po残", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? PoRemainder { get; set; }
    /// <summary>结余</summary>
    [SugarColumn(ColumnName = "balance", ColumnDescription = "结余", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Balance { get; set; }
    /// <summary>旧品处理</summary>
    [SugarColumn(ColumnName = "old_product_handling", ColumnDescription = "旧品处理", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? OldProductHandling { get; set; }

    // ---------- 采购(Mp) ----------
    /// <summary>采购订单发行日期</summary>
    [SugarColumn(ColumnName = "purchase_order_issue_date", ColumnDescription = "采购订单发行日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? PurchaseOrderIssueDate { get; set; }
    /// <summary>供应商</summary>
    [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Supplier { get; set; }
    /// <summary>采购订单号码</summary>
    [SugarColumn(ColumnName = "purchase_order_no", ColumnDescription = "采购订单号码", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PurchaseOrderNo { get; set; }

    // ---------- Iqc（受检） ----------
    /// <summary>受检单号</summary>
    [SugarColumn(ColumnName = "iqc_order_no", ColumnDescription = "受检单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? IqcOrderNo { get; set; }
    /// <summary>检验/检查日期</summary>
    [SugarColumn(ColumnName = "inspection_date", ColumnDescription = "检验日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? InspectionDate { get; set; }

    // ---------- 部管(Mc) ----------
    /// <summary>出库批次</summary>
    [SugarColumn(ColumnName = "outbound_batch", ColumnDescription = "出库批次", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OutboundBatch { get; set; }
    /// <summary>出库日期</summary>
    [SugarColumn(ColumnName = "outbound_date", ColumnDescription = "出库日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? OutboundDate { get; set; }

    // ---------- 制二(Pcba) ----------
    /// <summary>生产日期</summary>
    [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ProductionDate { get; set; }
    /// <summary>生产批次</summary>
    [SugarColumn(ColumnName = "production_batch", ColumnDescription = "生产批次", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ProductionBatch { get; set; }
    /// <summary>出库单号</summary>
    [SugarColumn(ColumnName = "outbound_order_no", ColumnDescription = "出库单号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OutboundOrderNo { get; set; }

    // ---------- 制一(Assy) ----------
    /// <summary>生产班组</summary>
    [SugarColumn(ColumnName = "production_team", ColumnDescription = "生产班组", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ProductionTeam { get; set; }
    /// <summary>实施日期</summary>
    [SugarColumn(ColumnName = "implementation_date", ColumnDescription = "实施日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ImplementationDate { get; set; }

    // ---------- Qa（品管） ----------
    /// <summary>检验批次</summary>
    [SugarColumn(ColumnName = "inspection_batch", ColumnDescription = "检验批次", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? InspectionBatch { get; set; }
    /// <summary>抽样号码</summary>
    [SugarColumn(ColumnName = "sampling_no", ColumnDescription = "抽样号码", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? SamplingNo { get; set; }

    // ---------- 制技(Te) ----------
    /// <summary>是否更新SOP（0=否 1=是）</summary>
    [SugarColumn(ColumnName = "is_sop_updated", ColumnDescription = "是否更新SOP", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSopUpdated { get; set; } = 0;

    /// <summary>
    /// 设变明细（多对一）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(EcnDetailId))]
    public TaktEcDetail? EcnDetail { get; set; }
}
