// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Ecn
// 文件名称：TaktEc.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：设变（ECN）主表实体，记录设变单号、工厂、发行/录入日期、标题、详情、负责人、审核状态等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变（ECN）主表实体。FlowInstanceId 存流程实例 Id，由业务方在发起流程后写入；流程引擎不识别本表，BusinessKey/BusinessType 与“设变”的对应由调用方（设变业务模块）约定并实现。联络等文档见附件表 Attachments。
/// </summary>
[SugarTable("takt_logistics_manufacturing_ecn", "设变主表")]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_ecn_no", nameof(EcnNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_ecn_issue_date", nameof(EcnIssueDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_ecn_entry_date", nameof(EcnEntryDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_change_status", nameof(ChangeStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_ec_status", nameof(EcStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEc : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
  /// 设变单号（唯一）
  /// </summary>
  [SugarColumn(ColumnName = "ecn_no", ColumnDescription = "设变单号", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnNo { get; set; } = string.Empty;

  /// <summary>
  /// 发行日期
  /// </summary>
  [SugarColumn(ColumnName = "ecn_issue_date", ColumnDescription = "发行日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EcnIssueDate { get; set; }

    /// <summary>
    /// 变更状态(1=工作的 2=取消的 3=发行的 4=P.P中变更的 5=固定的 6=挂起的 7=拒绝的)
    /// </summary>
    [SugarColumn(ColumnName = "change_status", ColumnDescription = "变更状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ChangeStatus { get; set; } = 1;

    /// <summary>
    /// 设变主题/标题
    /// </summary>
    [SugarColumn(ColumnName = "ecn_title", ColumnDescription = "设变主题", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnTitle { get; set; } = string.Empty;

    /// <summary>
    /// 设变详情/详细说明
    /// </summary>
    [SugarColumn(ColumnName = "ecn_details", ColumnDescription = "设变详情", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnDetailText { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "ecn_leader", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnLeader { get; set; } = string.Empty;

    /// <summary>
    /// 损失金额
    /// </summary>
    [SugarColumn(ColumnName = "ecn_loss_amount", ColumnDescription = "损失金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal EcnLossAmount { get; set; } = 0;

    /// <summary>
    /// 区分/类别
    /// 1:全仕向，2：部管，3：内部，4：技术
    /// </summary>
    [SugarColumn(ColumnName = "ecn_distinction", ColumnDescription = "区分", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnDistinction { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }


  /// <summary>
  /// 录入日期
  /// </summary>
  [SugarColumn(ColumnName = "ecn_entry_date", ColumnDescription = "录入日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EcnEntryDate { get; set; }

    /// <summary>
    /// 流程实例ID（关联工作流）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; } = 0;

    /// <summary>
    /// 设变状态（0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回）
    /// </summary>
    [SugarColumn(ColumnName = "ec_status", ColumnDescription = "设变状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EcStatus { get; set; } = 0;

    /// <summary>
    /// 设变明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEcDetail.EcnId))]
    public List<TaktEcDetail>? EcnDetails { get; set; }

    /// <summary>
    /// 设变附件列表（一个设变可对应多个附件）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEcAttachment.EcnId))]
    public List<TaktEcAttachment>? Attachments { get; set; }
}
