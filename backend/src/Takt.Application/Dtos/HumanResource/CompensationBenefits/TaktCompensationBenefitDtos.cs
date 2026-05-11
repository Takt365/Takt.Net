// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationBenefitDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪酬福利表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬福利表Dto
/// </summary>
public partial class TaktCompensationBenefitDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitDto()
    {
        OtherBenefits = string.Empty;
    }

    /// <summary>
    /// 薪酬福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompensationBenefitId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 基本工资
    /// </summary>
    public decimal BaseSalary { get; set; }
    /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal PositionAllowance { get; set; }
    /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal PerformanceBonus { get; set; }
    /// <summary>
    /// 加班费
    /// </summary>
    public decimal OvertimePay { get; set; }
    /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal TransportAllowance { get; set; }
    /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal MealAllowance { get; set; }
    /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal HousingAllowance { get; set; }
    /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal SocialSecurityBase { get; set; }
    /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal HousingFundBase { get; set; }
    /// <summary>
    /// 其他福利说明
    /// </summary>
    public string OtherBenefits { get; set; }
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
/// 薪酬福利表查询DTO
/// </summary>
public partial class TaktCompensationBenefitQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 基本工资
    /// </summary>
    public decimal? BaseSalary { get; set; }
    /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal? PositionAllowance { get; set; }
    /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal? PerformanceBonus { get; set; }
    /// <summary>
    /// 加班费
    /// </summary>
    public decimal? OvertimePay { get; set; }
    /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal? TransportAllowance { get; set; }
    /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal? MealAllowance { get; set; }
    /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal? HousingAllowance { get; set; }
    /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal? SocialSecurityBase { get; set; }
    /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal? HousingFundBase { get; set; }
    /// <summary>
    /// 其他福利说明
    /// </summary>
    public string? OtherBenefits { get; set; }
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
/// Takt创建薪酬福利表DTO
/// </summary>
public partial class TaktCompensationBenefitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitCreateDto()
    {
        OtherBenefits = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 基本工资
    /// </summary>
    public decimal BaseSalary { get; set; }

        /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal PositionAllowance { get; set; }

        /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal PerformanceBonus { get; set; }

        /// <summary>
    /// 加班费
    /// </summary>
    public decimal OvertimePay { get; set; }

        /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal TransportAllowance { get; set; }

        /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal MealAllowance { get; set; }

        /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal HousingAllowance { get; set; }

        /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal SocialSecurityBase { get; set; }

        /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal HousingFundBase { get; set; }

        /// <summary>
    /// 其他福利说明
    /// </summary>
    public string OtherBenefits { get; set; }

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
/// Takt更新薪酬福利表DTO
/// </summary>
public partial class TaktCompensationBenefitUpdateDto : TaktCompensationBenefitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitUpdateDto()
    {
    }

        /// <summary>
    /// 薪酬福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompensationBenefitId { get; set; } = 0;
}

/// <summary>
/// 薪酬福利表状态DTO
/// </summary>
public partial class TaktCompensationBenefitStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitStatusDto()
    {
    }

        /// <summary>
    /// 薪酬福利表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CompensationBenefitId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪酬福利表导入模板DTO
/// </summary>
public partial class TaktCompensationBenefitTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitTemplateDto()
    {
        OtherBenefits = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 基本工资
    /// </summary>
    public decimal BaseSalary { get; set; }

        /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal PositionAllowance { get; set; }

        /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal PerformanceBonus { get; set; }

        /// <summary>
    /// 加班费
    /// </summary>
    public decimal OvertimePay { get; set; }

        /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal TransportAllowance { get; set; }

        /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal MealAllowance { get; set; }

        /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal HousingAllowance { get; set; }

        /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal SocialSecurityBase { get; set; }

        /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal HousingFundBase { get; set; }

        /// <summary>
    /// 其他福利说明
    /// </summary>
    public string OtherBenefits { get; set; }

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
/// 薪酬福利表导入DTO
/// </summary>
public partial class TaktCompensationBenefitImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitImportDto()
    {
        OtherBenefits = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 基本工资
    /// </summary>
    public decimal BaseSalary { get; set; }

        /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal PositionAllowance { get; set; }

        /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal PerformanceBonus { get; set; }

        /// <summary>
    /// 加班费
    /// </summary>
    public decimal OvertimePay { get; set; }

        /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal TransportAllowance { get; set; }

        /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal MealAllowance { get; set; }

        /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal HousingAllowance { get; set; }

        /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal SocialSecurityBase { get; set; }

        /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal HousingFundBase { get; set; }

        /// <summary>
    /// 其他福利说明
    /// </summary>
    public string OtherBenefits { get; set; }

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
/// 薪酬福利表导出DTO
/// </summary>
public partial class TaktCompensationBenefitExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCompensationBenefitExportDto()
    {
        CreatedAt = DateTime.Now;
        OtherBenefits = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 基本工资
    /// </summary>
    public decimal BaseSalary { get; set; }

        /// <summary>
    /// 岗位津贴
    /// </summary>
    public decimal PositionAllowance { get; set; }

        /// <summary>
    /// 绩效奖金
    /// </summary>
    public decimal PerformanceBonus { get; set; }

        /// <summary>
    /// 加班费
    /// </summary>
    public decimal OvertimePay { get; set; }

        /// <summary>
    /// 交通补贴
    /// </summary>
    public decimal TransportAllowance { get; set; }

        /// <summary>
    /// 餐费补贴
    /// </summary>
    public decimal MealAllowance { get; set; }

        /// <summary>
    /// 住房补贴
    /// </summary>
    public decimal HousingAllowance { get; set; }

        /// <summary>
    /// 社保缴纳基数
    /// </summary>
    public decimal SocialSecurityBase { get; set; }

        /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    public decimal HousingFundBase { get; set; }

        /// <summary>
    /// 其他福利说明
    /// </summary>
    public string OtherBenefits { get; set; }

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