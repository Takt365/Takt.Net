// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktCountersignDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：会签单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// 会签单表Dto
/// </summary>
public partial class TaktCountersignDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignDto()
    {
        CountersignCode = string.Empty;
    }

    /// <summary>
    /// 会签单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignId { get; set; } = 0;

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
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }
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
/// 会签单表查询DTO
/// </summary>
public partial class TaktCountersignQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

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
    public long? ApplicantId { get; set; }
    /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }
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
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建会签单表DTO
/// </summary>
public partial class TaktCountersignCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignCreateDto()
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
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

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
/// Takt更新会签单表DTO
/// </summary>
public partial class TaktCountersignUpdateDto : TaktCountersignCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignUpdateDto()
    {
    }

        /// <summary>
    /// 会签单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignId { get; set; } = 0;
}

/// <summary>
/// 会签单表会签单状态DTO
/// </summary>
public partial class TaktCountersignStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignStatusDto()
    {
    }

        /// <summary>
    /// 会签单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CountersignId { get; set; } = 0;

    /// <summary>
    /// 会签单状态（0=禁用，1=启用）
    /// </summary>
    public int CountersignStatus { get; set; }
}

/// <summary>
/// 会签单表导入模板DTO
/// </summary>
public partial class TaktCountersignTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignTemplateDto()
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
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

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
/// 会签单表导入DTO
/// </summary>
public partial class TaktCountersignImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignImportDto()
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
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

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
/// 会签单表导出DTO
/// </summary>
public partial class TaktCountersignExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignExportDto()
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
    public long? ApplicantId { get; set; }

        /// <summary>
    /// 申请人
    /// </summary>
    public string? ApplicantBy { get; set; }

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