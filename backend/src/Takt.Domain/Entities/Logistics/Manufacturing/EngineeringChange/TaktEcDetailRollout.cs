// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailRollout.cs
// 功能描述：设变明细-部门 rollout（推出/实施推广）记录，记录各部门对设变明细的 rollout 过程（通用字段 + 部门专属 rollout_data JSON）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变明细-部门 rollout 记录。记录各部门对某条设变明细的推出/实施推广过程，通过 dept_code 区分部门（如 Fins/It/Gas/Cus/Pmc/Mc/Mp/Eng/Assy/Pcba/Te/Iqc/Qa），部门专属字段存 rollout_data JSON。
/// </summary>
[SugarTable("takt_logistics_manufacturing_ec_detail_rollout", "设变明细部门rollout表")]
[SugarIndex("ix_takt_logistics_manufacturing_ec_detail_rollout_ec_detail_id", nameof(EcDetailId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_detail_rollout_dept_code", nameof(DeptCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_detail_rollout_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_detail_rollout_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEcDetailRollout : TaktEntityBase
{
    /// <summary>
    /// 设变明细ID（TaktEcDetail 主键）
    /// </summary>
    [SugarColumn(ColumnName = "ec_detail_id", ColumnDescription = "设变明细ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EcDetailId { get; set; }

    /// <summary>
    /// 部门代码（如 Fins/It/Gas/Cus/Pmc/Mc/Mp/Eng/Assy/Pcba/Te/Iqc/Qa）
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否实施（0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_implemented", ColumnDescription = "是否实施", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsImplemented { get; set; } = 0;

    /// <summary>
    /// rollout 内容（通用）
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "rollout内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 部门 rollout 专属数据（JSON）。如生管：scheduled_production_date/scheduled_batch/po_remainder/balance/old_product_handling；部管：outbound_batch/outbound_date；采购：purchase_order_issue_date/supplier/purchase_order_no/old_product_handling；制造：production_team/production_batch/implementation_date 或 production_date/outbound_order_no；制造技术：is_sop_updated；受检：iqc_order_no/inspection_date；品管：inspection_date/inspection_batch/sampling_no 等。
    /// </summary>
    [SugarColumn(ColumnName = "rollout_data", ColumnDescription = "部门rollout数据JSON", ColumnDataType = "nvarchar(max)", IsNullable = true)]
    public string? RolloutData { get; set; }

    /// <summary>
    /// 设变明细
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(EcDetailId))]
    public TaktEcDetail? EcDetail { get; set; }
}
