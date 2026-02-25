// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktExpense.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：费用实体，定义费用发生领域模型；关联成本中心、成本要素，用于成本归集与核算
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 费用实体
/// </summary>
/// <remarks>记录单笔费用发生，关联成本中心、成本要素；可按期间汇总分析。工作流：创建流程实例时 BusinessKey = 本实体 Id，ProcessKey = "Expense"（需在 TaktFlowScheme 中配置）；发起时回写 InstanceId，结束时按 InstanceId 更新 ExpenseStatus。</remarks>
[SugarTable("takt_accounting_controlling_expense", "费用表")]
[SugarIndex("ix_takt_accounting_controlling_expense_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_company_code_expense_code", nameof(CompanyCode), OrderByType.Asc, nameof(ExpenseCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_expense_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_cost_center_id", nameof(CostCenterId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_cost_element_id", nameof(CostElementId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_expense_date", nameof(ExpenseDate), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_fiscal_year", nameof(FiscalYear), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_expense_status", nameof(ExpenseStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_expense_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktExpense : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 费用单编码（同一公司内唯一，可由单据编码规则生成）
    /// </summary>
    [SugarColumn(ColumnName = "expense_code", ColumnDescription = "费用单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ExpenseCode { get; set; } = string.Empty;

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
    /// 费用金额
    /// </summary>
    [SugarColumn(ColumnName = "amount", ColumnDescription = "费用金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false)]
    public decimal Amount { get; set; }

    /// <summary>
    /// 币种（如 CNY、USD；可关联字典或默认本币）
    /// </summary>
    [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? Currency { get; set; }

    /// <summary>
    /// 费用日期（发生日期）
    /// </summary>
    [SugarColumn(ColumnName = "expense_date", ColumnDescription = "费用日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ExpenseDate { get; set; }

    /// <summary>
    /// 会计年度（如 2025）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_year", ColumnDescription = "会计年度", ColumnDataType = "int", IsNullable = false)]
    public int FiscalYear { get; set; }

    /// <summary>
    /// 会计月份（1～12，0 表示按年不按月）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_month", ColumnDescription = "会计月份", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FiscalMonth { get; set; }

    /// <summary>
    /// 费用类型（0=实际发生，1=预提，2=摊销等；可关联字典）
    /// </summary>
    [SugarColumn(ColumnName = "expense_type", ColumnDescription = "费用类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExpenseType { get; set; } = 0;

    /// <summary>
    /// 关联工作流实例ID（对应 TaktFlowInstance.Id，0=未关联；流程处理见 TaktFlowInstanceService：发起时按 ProcessKey+BusinessKey 回写本字段，结束时按本字段查找并更新 ExpenseStatus；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 费用状态（0=草稿，1=审批中，2=已审核，3=已驳回；与工作流审批衔接）
    /// </summary>
    [SugarColumn(ColumnName = "expense_status", ColumnDescription = "费用状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExpenseStatus { get; set; } = 0;

    /// <summary>
    /// 申请人/经办人ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantId { get; set; }

    /// <summary>
    /// 申请人/经办人姓名
    /// </summary>
    [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ApplicantName { get; set; }

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
