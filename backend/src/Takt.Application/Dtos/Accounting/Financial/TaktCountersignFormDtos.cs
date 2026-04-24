// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktCountersignFormDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：会签表单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// 会签表单表Dto
/// </summary>
public partial class TaktCountersignFormDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormDto()
    {
        CountersignCode = string.Empty;
    }

    /// <summary>
    /// 会签表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignFormId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 会签编号
    /// </summary>
    public string CountersignCode { get; set; }
    /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }
    /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }
    /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }
    /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }
    /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }
    /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }
    /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }
    /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }
    /// <summary>
    /// 是否有预算
    /// </summary>
    public int IsBudget { get; set; }
    /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }
    /// <summary>
    /// 预算金额
    /// </summary>
    public decimal BudgetAmount { get; set; }
    /// <summary>
    /// 申请金额
    /// </summary>
    public decimal ApplicationAmount { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }
    /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }
    /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }
    /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }
    /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 会签单状态
    /// </summary>
    public int CountersignStatus { get; set; }
}

/// <summary>
/// 会签表单表查询DTO
/// </summary>
public partial class TaktCountersignFormQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 会签表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignFormId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 会签编号
    /// </summary>
    public string? CountersignCode { get; set; }
    /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }
    /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }
    /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }
    /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }
    /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// 承认日期开始时间
    /// </summary>
    public DateTime? ApprovalDateStart { get; set; }
    /// <summary>
    /// 承认日期结束时间
    /// </summary>
    public DateTime? ApprovalDateEnd { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 申请日期开始时间
    /// </summary>
    public DateTime? ApplicationDateStart { get; set; }
    /// <summary>
    /// 申请日期结束时间
    /// </summary>
    public DateTime? ApplicationDateEnd { get; set; }
    /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }
    /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }
    /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }
    /// <summary>
    /// 是否有预算
    /// </summary>
    public int? IsBudget { get; set; }
    /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }
    /// <summary>
    /// 预算金额
    /// </summary>
    public decimal? BudgetAmount { get; set; }
    /// <summary>
    /// 申请金额
    /// </summary>
    public decimal? ApplicationAmount { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }
    /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }
    /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }
    /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }
    /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    /// <summary>
    /// 会签单状态
    /// </summary>
    public int? CountersignStatus { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建会签表单表DTO
/// </summary>
public partial class TaktCountersignFormCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormCreateDto()
    {
        CountersignCode = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 会签编号
    /// </summary>
    public string CountersignCode { get; set; }

        /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }

        /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }

        /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }

        /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }

        /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

        /// <summary>
    /// 申请人员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }

        /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }

        /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }

        /// <summary>
    /// 是否有预算
    /// </summary>
    public int IsBudget { get; set; }

        /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }

        /// <summary>
    /// 预算金额
    /// </summary>
    public decimal BudgetAmount { get; set; }

        /// <summary>
    /// 申请金额
    /// </summary>
    public decimal ApplicationAmount { get; set; }

        /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }

        /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }

        /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }

        /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 会签单状态
    /// </summary>
    public int CountersignStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新会签表单表DTO
/// </summary>
public partial class TaktCountersignFormUpdateDto : TaktCountersignFormCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormUpdateDto()
    {
    }

        /// <summary>
    /// 会签表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignFormId { get; set; }
}

/// <summary>
/// 会签表单表会签单状态DTO
/// </summary>
public partial class TaktCountersignFormCountersignStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormCountersignStatusDto()
    {
    }

        /// <summary>
    /// 会签表单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignFormId { get; set; }

    /// <summary>
    /// 会签单状态（0=禁用，1=启用）
    /// </summary>
    public int CountersignStatus { get; set; }
}

/// <summary>
/// 会签表单表导入模板DTO
/// </summary>
public partial class TaktCountersignFormTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormTemplateDto()
    {
        CountersignCode = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 会签编号
    /// </summary>
    public string CountersignCode { get; set; }

        /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }

        /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }

        /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }

        /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }

        /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }

        /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }

        /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }

        /// <summary>
    /// 是否有预算
    /// </summary>
    public int IsBudget { get; set; }

        /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }

        /// <summary>
    /// 预算金额
    /// </summary>
    public decimal BudgetAmount { get; set; }

        /// <summary>
    /// 申请金额
    /// </summary>
    public decimal ApplicationAmount { get; set; }

        /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }

        /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }

        /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }

        /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 会签单状态
    /// </summary>
    public int CountersignStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 会签表单表导入DTO
/// </summary>
public partial class TaktCountersignFormImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormImportDto()
    {
        CountersignCode = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 会签编号
    /// </summary>
    public string CountersignCode { get; set; }

        /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }

        /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }

        /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }

        /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }

        /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }

        /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }

        /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }

        /// <summary>
    /// 是否有预算
    /// </summary>
    public int IsBudget { get; set; }

        /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }

        /// <summary>
    /// 预算金额
    /// </summary>
    public decimal BudgetAmount { get; set; }

        /// <summary>
    /// 申请金额
    /// </summary>
    public decimal ApplicationAmount { get; set; }

        /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }

        /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }

        /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }

        /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 会签单状态
    /// </summary>
    public int CountersignStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 会签表单表导出DTO
/// </summary>
public partial class TaktCountersignFormExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignFormExportDto()
    {
        CreatedAt = DateTime.Now;
        CountersignCode = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 会签编号
    /// </summary>
    public string CountersignCode { get; set; }

        /// <summary>
    /// 会签部门
    /// </summary>
    public string? CountersignDepts { get; set; }

        /// <summary>
    /// 财务部门
    /// </summary>
    public string? FinanceDept { get; set; }

        /// <summary>
    /// 预算审核意见
    /// </summary>
    public string? BudgetReviewComment { get; set; }

        /// <summary>
    /// 总经室
    /// </summary>
    public string? ExecutiveOffice { get; set; }

        /// <summary>
    /// 承认日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? ApplicationDate { get; set; }

        /// <summary>
    /// 申请人员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

        /// <summary>
    /// 申请者名
    /// </summary>
    public string? ApplicantName { get; set; }

        /// <summary>
    /// 申请部门
    /// </summary>
    public string? ApplicationDept { get; set; }

        /// <summary>
    /// 经费负担部门
    /// </summary>
    public string? CostBearerDept { get; set; }

        /// <summary>
    /// 是否有预算
    /// </summary>
    public int IsBudget { get; set; }

        /// <summary>
    /// 预算项目
    /// </summary>
    public string? BudgetItem { get; set; }

        /// <summary>
    /// 预算金额
    /// </summary>
    public decimal BudgetAmount { get; set; }

        /// <summary>
    /// 申请金额
    /// </summary>
    public decimal ApplicationAmount { get; set; }

        /// <summary>
    /// 标题
    /// </summary>
    public string? CountersignTitle { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? ApplicationReason { get; set; }

        /// <summary>
    /// 预算使用说明
    /// </summary>
    public string? BudgetUsageDescription { get; set; }

        /// <summary>
    /// 目标与预期效益
    /// </summary>
    public string? TargetAndExpectedBenefit { get; set; }

        /// <summary>
    /// 附件
    /// </summary>
    public string? Attachments { get; set; }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long? FlowInstanceId { get; set; }

        /// <summary>
    /// 会签单状态
    /// </summary>
    public int CountersignStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}