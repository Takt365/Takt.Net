// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktEmployeeBenefitDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：员工福利表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 员工福利表Dto
/// </summary>
public partial class TaktEmployeeBenefitDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitDto()
    {
        BenefitType = string.Empty;
        BenefitName = string.Empty;
        DistributionMethod = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 员工福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeBenefitId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 福利方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BenefitPlanId { get; set; }
    /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }
    /// <summary>
    /// 福利名称
    /// </summary>
    public string BenefitName { get; set; }
    /// <summary>
    /// 福利金额
    /// </summary>
    public decimal BenefitAmount { get; set; }
    /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime ExpiryDate { get; set; }
    /// <summary>
    /// 福利说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 员工福利表查询DTO
/// </summary>
public partial class TaktEmployeeBenefitQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 福利方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? BenefitPlanId { get; set; }
    /// <summary>
    /// 福利类型
    /// </summary>
    public string? BenefitType { get; set; }
    /// <summary>
    /// 福利名称
    /// </summary>
    public string? BenefitName { get; set; }
    /// <summary>
    /// 福利金额
    /// </summary>
    public decimal? BenefitAmount { get; set; }
    /// <summary>
    /// 发放方式
    /// </summary>
    public string? DistributionMethod { get; set; }
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
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 到期日期开始时间
    /// </summary>
    public DateTime? ExpiryDateStart { get; set; }
    /// <summary>
    /// 到期日期结束时间
    /// </summary>
    public DateTime? ExpiryDateEnd { get; set; }
    /// <summary>
    /// 福利说明
    /// </summary>
    public string? Description { get; set; }
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
/// Takt创建员工福利表DTO
/// </summary>
public partial class TaktEmployeeBenefitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitCreateDto()
    {
        BenefitType = string.Empty;
        BenefitName = string.Empty;
        DistributionMethod = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 福利方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BenefitPlanId { get; set; }

        /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

        /// <summary>
    /// 福利名称
    /// </summary>
    public string BenefitName { get; set; }

        /// <summary>
    /// 福利金额
    /// </summary>
    public decimal BenefitAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime ExpiryDate { get; set; }

        /// <summary>
    /// 福利说明
    /// </summary>
    public string Description { get; set; }

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
/// Takt更新员工福利表DTO
/// </summary>
public partial class TaktEmployeeBenefitUpdateDto : TaktEmployeeBenefitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitUpdateDto()
    {
    }

        /// <summary>
    /// 员工福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeBenefitId { get; set; } = 0;
}

/// <summary>
/// 员工福利表状态DTO
/// </summary>
public partial class TaktEmployeeBenefitStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitStatusDto()
    {
    }

        /// <summary>
    /// 员工福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeBenefitId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 员工福利表导入模板DTO
/// </summary>
public partial class TaktEmployeeBenefitTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitTemplateDto()
    {
        BenefitType = string.Empty;
        BenefitName = string.Empty;
        DistributionMethod = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 福利方案ID
    /// </summary>
    public long BenefitPlanId { get; set; }

        /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

        /// <summary>
    /// 福利名称
    /// </summary>
    public string BenefitName { get; set; }

        /// <summary>
    /// 福利金额
    /// </summary>
    public decimal BenefitAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime ExpiryDate { get; set; }

        /// <summary>
    /// 福利说明
    /// </summary>
    public string Description { get; set; }

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
/// 员工福利表导入DTO
/// </summary>
public partial class TaktEmployeeBenefitImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitImportDto()
    {
        BenefitType = string.Empty;
        BenefitName = string.Empty;
        DistributionMethod = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 福利方案ID
    /// </summary>
    public long BenefitPlanId { get; set; }

        /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

        /// <summary>
    /// 福利名称
    /// </summary>
    public string BenefitName { get; set; }

        /// <summary>
    /// 福利金额
    /// </summary>
    public decimal BenefitAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime ExpiryDate { get; set; }

        /// <summary>
    /// 福利说明
    /// </summary>
    public string Description { get; set; }

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
/// 员工福利表导出DTO
/// </summary>
public partial class TaktEmployeeBenefitExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeBenefitExportDto()
    {
        CreatedAt = DateTime.Now;
        BenefitType = string.Empty;
        BenefitName = string.Empty;
        DistributionMethod = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 福利方案ID
    /// </summary>
    public long BenefitPlanId { get; set; }

        /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

        /// <summary>
    /// 福利名称
    /// </summary>
    public string BenefitName { get; set; }

        /// <summary>
    /// 福利金额
    /// </summary>
    public decimal BenefitAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime ExpiryDate { get; set; }

        /// <summary>
    /// 福利说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}