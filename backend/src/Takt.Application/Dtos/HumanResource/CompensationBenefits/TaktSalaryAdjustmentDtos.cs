// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustmentDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪资调整表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资调整表Dto
/// </summary>
public partial class TaktSalaryAdjustmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentDto()
    {
        AdjustmentType = string.Empty;
        AdjustmentReason = string.Empty;
        PreviousSalaryLevel = string.Empty;
        NewSalaryLevel = string.Empty;
        ApprovalComments = string.Empty;
    }

    /// <summary>
    /// 薪资调整表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryAdjustmentId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 调整类型
    /// </summary>
    public string AdjustmentType { get; set; }
    /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime AdjustmentDate { get; set; }
    /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal PreviousSalary { get; set; }
    /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal NewSalary { get; set; }
    /// <summary>
    /// 调整金额
    /// </summary>
    public decimal AdjustmentAmount { get; set; }
    /// <summary>
    /// 调整比例
    /// </summary>
    public decimal AdjustmentPercentage { get; set; }
    /// <summary>
    /// 调薪原因
    /// </summary>
    public string AdjustmentReason { get; set; }
    /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string PreviousSalaryLevel { get; set; }
    /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string NewSalaryLevel { get; set; }
    /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverId { get; set; }
    /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime ApprovalDate { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string ApprovalComments { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪资调整表查询DTO
/// </summary>
public partial class TaktSalaryAdjustmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 调整类型
    /// </summary>
    public string? AdjustmentType { get; set; }
    /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime? AdjustmentDate { get; set; }

    /// <summary>
    /// 调整日期开始时间
    /// </summary>
    public DateTime? AdjustmentDateStart { get; set; }
    /// <summary>
    /// 调整日期结束时间
    /// </summary>
    public DateTime? AdjustmentDateEnd { get; set; }
    /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal? PreviousSalary { get; set; }
    /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal? NewSalary { get; set; }
    /// <summary>
    /// 调整金额
    /// </summary>
    public decimal? AdjustmentAmount { get; set; }
    /// <summary>
    /// 调整比例
    /// </summary>
    public decimal? AdjustmentPercentage { get; set; }
    /// <summary>
    /// 调薪原因
    /// </summary>
    public string? AdjustmentReason { get; set; }
    /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string? PreviousSalaryLevel { get; set; }
    /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string? NewSalaryLevel { get; set; }
    /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
    /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// 审批日期开始时间
    /// </summary>
    public DateTime? ApprovalDateStart { get; set; }
    /// <summary>
    /// 审批日期结束时间
    /// </summary>
    public DateTime? ApprovalDateEnd { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? ApprovalComments { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveDateEnd { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建薪资调整表DTO
/// </summary>
public partial class TaktSalaryAdjustmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentCreateDto()
    {
        AdjustmentType = string.Empty;
        AdjustmentReason = string.Empty;
        PreviousSalaryLevel = string.Empty;
        NewSalaryLevel = string.Empty;
        ApprovalComments = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调整类型
    /// </summary>
    public string AdjustmentType { get; set; }

        /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime AdjustmentDate { get; set; }

        /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal PreviousSalary { get; set; }

        /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal NewSalary { get; set; }

        /// <summary>
    /// 调整金额
    /// </summary>
    public decimal AdjustmentAmount { get; set; }

        /// <summary>
    /// 调整比例
    /// </summary>
    public decimal AdjustmentPercentage { get; set; }

        /// <summary>
    /// 调薪原因
    /// </summary>
    public string AdjustmentReason { get; set; }

        /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string PreviousSalaryLevel { get; set; }

        /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string NewSalaryLevel { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverId { get; set; }

        /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime ApprovalDate { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string ApprovalComments { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新薪资调整表DTO
/// </summary>
public partial class TaktSalaryAdjustmentUpdateDto : TaktSalaryAdjustmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentUpdateDto()
    {
    }

        /// <summary>
    /// 薪资调整表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryAdjustmentId { get; set; } = 0;
}

/// <summary>
/// 薪资调整表状态DTO
/// </summary>
public partial class TaktSalaryAdjustmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentStatusDto()
    {
    }

        /// <summary>
    /// 薪资调整表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryAdjustmentId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪资调整表导入模板DTO
/// </summary>
public partial class TaktSalaryAdjustmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentTemplateDto()
    {
        AdjustmentType = string.Empty;
        AdjustmentReason = string.Empty;
        PreviousSalaryLevel = string.Empty;
        NewSalaryLevel = string.Empty;
        ApprovalComments = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调整类型
    /// </summary>
    public string AdjustmentType { get; set; }

        /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime AdjustmentDate { get; set; }

        /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal PreviousSalary { get; set; }

        /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal NewSalary { get; set; }

        /// <summary>
    /// 调整金额
    /// </summary>
    public decimal AdjustmentAmount { get; set; }

        /// <summary>
    /// 调整比例
    /// </summary>
    public decimal AdjustmentPercentage { get; set; }

        /// <summary>
    /// 调薪原因
    /// </summary>
    public string AdjustmentReason { get; set; }

        /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string PreviousSalaryLevel { get; set; }

        /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string NewSalaryLevel { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

        /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime ApprovalDate { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string ApprovalComments { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 薪资调整表导入DTO
/// </summary>
public partial class TaktSalaryAdjustmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentImportDto()
    {
        AdjustmentType = string.Empty;
        AdjustmentReason = string.Empty;
        PreviousSalaryLevel = string.Empty;
        NewSalaryLevel = string.Empty;
        ApprovalComments = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调整类型
    /// </summary>
    public string AdjustmentType { get; set; }

        /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime AdjustmentDate { get; set; }

        /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal PreviousSalary { get; set; }

        /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal NewSalary { get; set; }

        /// <summary>
    /// 调整金额
    /// </summary>
    public decimal AdjustmentAmount { get; set; }

        /// <summary>
    /// 调整比例
    /// </summary>
    public decimal AdjustmentPercentage { get; set; }

        /// <summary>
    /// 调薪原因
    /// </summary>
    public string AdjustmentReason { get; set; }

        /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string PreviousSalaryLevel { get; set; }

        /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string NewSalaryLevel { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

        /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime ApprovalDate { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string ApprovalComments { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 薪资调整表导出DTO
/// </summary>
public partial class TaktSalaryAdjustmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryAdjustmentExportDto()
    {
        CreatedAt = DateTime.Now;
        AdjustmentType = string.Empty;
        AdjustmentReason = string.Empty;
        PreviousSalaryLevel = string.Empty;
        NewSalaryLevel = string.Empty;
        ApprovalComments = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 调整类型
    /// </summary>
    public string AdjustmentType { get; set; }

        /// <summary>
    /// 调整日期
    /// </summary>
    public DateTime AdjustmentDate { get; set; }

        /// <summary>
    /// 调整前薪资
    /// </summary>
    public decimal PreviousSalary { get; set; }

        /// <summary>
    /// 调整后薪资
    /// </summary>
    public decimal NewSalary { get; set; }

        /// <summary>
    /// 调整金额
    /// </summary>
    public decimal AdjustmentAmount { get; set; }

        /// <summary>
    /// 调整比例
    /// </summary>
    public decimal AdjustmentPercentage { get; set; }

        /// <summary>
    /// 调薪原因
    /// </summary>
    public string AdjustmentReason { get; set; }

        /// <summary>
    /// 调整前薪资等级
    /// </summary>
    public string PreviousSalaryLevel { get; set; }

        /// <summary>
    /// 调整后薪资等级
    /// </summary>
    public string NewSalaryLevel { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

        /// <summary>
    /// 审批日期
    /// </summary>
    public DateTime ApprovalDate { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string ApprovalComments { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}