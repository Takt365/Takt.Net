// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktBudget.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：预算实体，定义预算领域模型；按年度、成本中心、成本要素设定预算金额，与费用 TaktExpense 对应用于预算控制
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 预算实体
/// </summary>
/// <remarks>按会计年度、成本中心、成本要素维度设定预算金额；已用金额可由费用汇总或冗余更新。工作流：创建流程实例时 BusinessKey = 本实体 Id，ProcessKey = "Budget"（需在 TaktFlowScheme 中配置）；发起时回写 InstanceId，结束时按 InstanceId 更新 BudgetStatus。</remarks>
[SugarTable("takt_accounting_controlling_budget", "预算表")]
[SugarIndex("ix_takt_accounting_controlling_budget_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_fiscal_year", nameof(FiscalYear), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_cost_center_id", nameof(CostCenterId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_cost_element_id", nameof(CostElementId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_budget_status", nameof(BudgetStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_budget_year_center_element", nameof(CompanyCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, nameof(CostCenterId), OrderByType.Asc, nameof(CostElementId), OrderByType.Asc, true)]
public class TaktBudget : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 会计年度（如 2025）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_year", ColumnDescription = "会计年度", ColumnDataType = "int", IsNullable = false)]
    public int FiscalYear { get; set; }

    /// <summary>
    /// 成本中心ID（关联 TaktCostCenter.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_id", ColumnDescription = "成本中心ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CostCenterId { get; set; }

    /// <summary>
    /// 成本中心编码（冗余，便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CostCenterCode { get; set; }

    /// <summary>
    /// 成本要素ID（关联 TaktCostElement.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_id", ColumnDescription = "成本要素ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CostElementId { get; set; }

    /// <summary>
    /// 成本要素编码（冗余，便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CostElementCode { get; set; }

    /// <summary>
    /// 预算金额
    /// </summary>
    [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false)]
    public decimal BudgetAmount { get; set; }

    /// <summary>
    /// 已用金额（可由费用按年度+成本中心+成本要素汇总更新，或应用层维护）
    /// </summary>
    [SugarColumn(ColumnName = "used_amount", ColumnDescription = "已用金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal UsedAmount { get; set; }

    /// <summary>
    /// 关联工作流实例ID（对应 TaktFlowInstance.Id，0=未关联；流程处理见 TaktFlowInstanceService：发起时按 ProcessKey+BusinessKey 回写本字段，结束时按本字段查找并更新 BudgetStatus；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 预算状态（0=草稿，1=审批中，2=已发布，3=已驳回；与工作流审批衔接；已发布后可锁定由应用层控制）
    /// </summary>
    [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int BudgetStatus { get; set; } = 0;

    /// <summary>
    /// 工厂ID（关联 TaktPlant.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "plant_id", ColumnDescription = "工厂ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码（冗余，便于列表展示；来源于 TaktPlant.PlantCode）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }
}
