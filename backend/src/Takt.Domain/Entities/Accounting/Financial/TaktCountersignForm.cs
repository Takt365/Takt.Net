// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktCountersignForm.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会签单实体，财务管理中的会签申请单，与流程表单无直接关系，业务上需审批时由流程引用
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// Takt会签单实体（财务管理，会签申请单）
/// </summary>
/// <remarks>
/// 归属财务管理模块，与 TaktFlowForm 无直接关系。业务上如需审批，由流程在发起时引用本表（如会签编号），流程仅负责审批环节。
/// </remarks>
[SugarTable("takt_accounting_financial_countersign_form", "会签单表")]
[SugarIndex("ix_takt_accounting_financial_countersign_form_countersign_code", nameof(CountersignCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_application_date", nameof(ApplicationDate), OrderByType.Desc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_countersign_status", nameof(CountersignStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_countersign_form_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
public class TaktCountersignForm : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 会签编号（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "countersign_code", ColumnDescription = "会签编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CountersignCode { get; set; } = string.Empty;

    /// <summary>
    /// 会签部门（JSON，保存每个会签部门及其承认日期）
    /// </summary>
    /// <remarks>
    /// 结构示例：[ { \"deptId\": 1, \"deptName\": \"制造部\", \"approvedDate\": \"2025-03-16\" }, ... ]
    /// </remarks>
    [SugarColumn(ColumnName = "countersign_depts", ColumnDescription = "会签部门", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? CountersignDepts { get; set; }

    /// <summary>
    /// 财务部（JSON：财务负责人/审核金额/审核日期等）
    /// </summary>
    /// <remarks>
    /// 结构示例：{ \"auditor\": \"财务部长\", \"approvedAmount\": 100000, \"approvedDate\": \"2025-03-16\" }
    /// </remarks>
    [SugarColumn(ColumnName = "finance_dept", ColumnDescription = "财务部门", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? FinanceDept { get; set; }

    /// <summary>
    /// 预算审核意见
    /// </summary>
    [SugarColumn(ColumnName = "budget_review_comment", ColumnDescription = "预算审核意见", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? BudgetReviewComment { get; set; }

    /// <summary>
    /// 总经室（总裁办）会签信息（JSON：副总/总经理/董事长及承认日期）
    /// </summary>
    /// <remarks>
    /// 结构示例：{ \"vicePresident\": { \"name\": \"张三\", \"approvedDate\": \"2025-03-16\" }, \"generalManager\": { ... }, \"chairman\": { ... } }
    /// </remarks>
    [SugarColumn(ColumnName = "executive_office", ColumnDescription = "总经室", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ExecutiveOffice { get; set; }

    /// <summary>
    /// 承认日期
    /// </summary>
    [SugarColumn(ColumnName = "approval_date", ColumnDescription = "承认日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 申请人员工ID（关联 TaktEmployee，与当前人员信息一致，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "申请人员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 申请者名
    /// </summary>
    [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请者名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ApplicantName { get; set; }

    /// <summary>
    /// 申请部门
    /// </summary>
    [SugarColumn(ColumnName = "application_dept", ColumnDescription = "申请部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ApplicationDept { get; set; }

    /// <summary>
    /// 经费负担部门
    /// </summary>
    [SugarColumn(ColumnName = "cost_bearer_dept", ColumnDescription = "经费负担部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CostBearerDept { get; set; }

    /// <summary>
    /// 是否有预算（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_budget", ColumnDescription = "是否有预算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBudget { get; set; } = 0;    

    /// <summary>
    /// 预算项目
    /// </summary>
    [SugarColumn(ColumnName = "budget_item", ColumnDescription = "预算项目", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? BudgetItem { get; set; }

    /// <summary>
    /// 预算金额
    /// </summary>
    [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal BudgetAmount { get; set; } = 0;

    /// <summary>
    /// 申请金额
    /// </summary>
    [SugarColumn(ColumnName = "application_amount", ColumnDescription = "申请金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ApplicationAmount { get; set; } = 0;

    /// <summary>
    /// 标题
    /// </summary>
    [SugarColumn(ColumnName = "countersign_title", ColumnDescription = "标题", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? CountersignTitle { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    [SugarColumn(ColumnName = "application_reason", ColumnDescription = "申请原因", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ApplicationReason { get; set; }

    /// <summary>
    /// 预算使用说明
    /// </summary>
    [SugarColumn(ColumnName = "budget_usage_description", ColumnDescription = "预算使用说明", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? BudgetUsageDescription { get; set; }

    /// <summary>
    /// 目标与预期效益
    /// </summary>
    [SugarColumn(ColumnName = "target_and_expected_benefit", ColumnDescription = "目标与预期效益", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? TargetAndExpectedBenefit { get; set; }

    /// <summary>
    /// 附件（JSON，用于上传合同、申请、报价单、合同草案、调查报告等）
    /// </summary>
    /// <remarks>
    /// 建议结构示例：
    /// [
    ///   { \"fileId\": 1, \"fileName\": \"报价单.pdf\", \"fileType\": \"quotation\" },
    ///   { \"fileId\": 2, \"fileName\": \"合同草案.docx\", \"fileType\": \"contract_draft\" }
    /// ]
    /// 其中 fileId 关联 TaktFile.Id。
    /// </remarks>
    [SugarColumn(ColumnName = "attachments", ColumnDescription = "附件", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? Attachments { get; set; }

    /// <summary>
    /// 流程实例ID（关联 TaktFlowInstance，发起审批后由业务写入，用于审批流程）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 会签单状态（0=草稿，1=审批中，2=已承认，3=已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "countersign_status", ColumnDescription = "会签单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CountersignStatus { get; set; } = 0;
}
