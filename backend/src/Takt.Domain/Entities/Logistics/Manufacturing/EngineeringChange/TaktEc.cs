// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange
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
/// 设变（ECN）主表实体。参照 Ec_ 项目（统一 ec 前缀）：发行日期、设变单号、状态、标题、详情、负责人、损失金额、区分、设变PDF文档、联络文档JSON（EcDocs）、录入日期等。
/// </summary>
[SugarTable("takt_logistics_manufacturing_ec", "设变主表")]
[SugarIndex("ix_takt_logistics_manufacturing_ec_ec_no", nameof(EcNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_ec_issue_date", nameof(EcIssueDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_ec_entry_date", nameof(EcEntryDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_approval_status", nameof(ApprovalStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_status", nameof(Status), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ec_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEc : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
  /// 设变单号（唯一）
  /// </summary>
  [SugarColumn(ColumnName = "ec_no", ColumnDescription = "设变单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcNo { get; set; } = string.Empty;

    /// <summary>
    /// 设变PDF文档
    /// </summary>
    [SugarColumn(ColumnName = "ec_pdf", ColumnDescription = "设变文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcPdf { get; set; }
  /// <summary>
  /// 发行日期
  /// </summary>
  [SugarColumn(ColumnName = "ec_issue_date", ColumnDescription = "发行日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EcIssueDate { get; set; }

    /// <summary>
    /// 状态(0=正常 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 设变主题/标题
    /// </summary>
    [SugarColumn(ColumnName = "ec_title", ColumnDescription = "设变主题", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcTitle { get; set; }

    /// <summary>
    /// 设变详情/详细说明
    /// </summary>
    [SugarColumn(ColumnName = "ec_details", ColumnDescription = "设变详情", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcDetailText { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "ec_leader", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcLeader { get; set; }

    /// <summary>
    /// 损失金额
    /// </summary>
    [SugarColumn(ColumnName = "ec_loss_amount", ColumnDescription = "损失金额", ColumnDataType = "decimal(18,4)", IsNullable = true)]
    public decimal? EcLossAmount { get; set; }

    /// <summary>
    /// 区分/类别
    /// 1:全仕向，2：部管，3：内部，4：技术
    /// </summary>
    [SugarColumn(ColumnName = "ec_distinction", ColumnDescription = "区分", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcDistinction { get; set; }

    /// <summary>
    /// 联络文档集合（JSON）。格式示例：{"联络":{"编号":"...","文档":"..."},"EPP":{"编号":"...","文档":"..."},"TCJ":{"编号":"...","文档":"..."},"外部":{"编号":"...","文档":"..."}}
    /// </summary>
    [SugarColumn(ColumnName = "ec_docs", ColumnDescription = "联络文档JSON", ColumnDataType = "nvarchar(max)", IsNullable = true)]
    public string? EcDocs { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EffectiveDate { get; set; }


  /// <summary>
  /// 录入日期
  /// </summary>
  [SugarColumn(ColumnName = "ec_entry_date", ColumnDescription = "录入日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EcEntryDate { get; set; }

  /// <summary>
  /// 申请人（用户名）
  /// </summary>
  [SugarColumn(ColumnName = "applicant", ColumnDescription = "申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Applicant { get; set; }

    /// <summary>
    /// 申请部门
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept", ColumnDescription = "申请部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ApplicantDept { get; set; }

    /// <summary>
    /// 审核状态（如：草稿、待审、已批准、已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审核状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ApprovalStatus { get; set; }



    /// <summary>
    /// 设变明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEcDetail.EcId))]
    public List<TaktEcDetail>? EcDetails { get; set; }
}
