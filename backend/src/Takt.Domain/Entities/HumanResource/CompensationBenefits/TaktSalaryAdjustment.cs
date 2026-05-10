// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustment.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资调整实体，记录员工薪资调整历史、调薪原因、审批流程等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资调整实体
/// </summary>
[SugarTable("takt_human_resource_salary_adjustment", "薪资调整表")]
[SugarIndex("ix_takt_human_resource_salary_adjustment_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_salary_adjustment_adjustment_date", nameof(AdjustmentDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_salary_adjustment_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_salary_adjustment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalaryAdjustment : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调整类型(年度调薪/晋升调薪/岗位变动调薪/绩效调薪/特殊调薪/试用期转正调薪)
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_type", ColumnDescription = "调整类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AdjustmentType { get; set; } = string.Empty;

    /// <summary>
    /// 调整日期
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_date", ColumnDescription = "调整日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime AdjustmentDate { get; set; }

    /// <summary>
    /// 调整前薪资
    /// </summary>
    [SugarColumn(ColumnName = "previous_salary", ColumnDescription = "调整前薪资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PreviousSalary { get; set; } = 0;

    /// <summary>
    /// 调整后薪资
    /// </summary>
    [SugarColumn(ColumnName = "new_salary", ColumnDescription = "调整后薪资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal NewSalary { get; set; } = 0;

    /// <summary>
    /// 调整金额
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_amount", ColumnDescription = "调整金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AdjustmentAmount { get; set; } = 0;

    /// <summary>
    /// 调整比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_percentage", ColumnDescription = "调整比例", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AdjustmentPercentage { get; set; } = 0;

    /// <summary>
    /// 调薪原因
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_reason", ColumnDescription = "调薪原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AdjustmentReason { get; set; } = string.Empty;

    /// <summary>
    /// 调整前薪资等级
    /// </summary>
    [SugarColumn(ColumnName = "previous_salary_level", ColumnDescription = "调整前薪资等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PreviousSalaryLevel { get; set; } = string.Empty;

    /// <summary>
    /// 调整后薪资等级
    /// </summary>
    [SugarColumn(ColumnName = "new_salary_level", ColumnDescription = "调整后薪资等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string NewSalaryLevel { get; set; } = string.Empty;

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApproverId { get; set; }

    /// <summary>
    /// 审批日期
    /// </summary>
    [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ApprovalDate { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "approval_comments", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApprovalComments { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 状态(0=待审批 1=审批中 2=已批准 3=已拒绝 4=已撤销)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
