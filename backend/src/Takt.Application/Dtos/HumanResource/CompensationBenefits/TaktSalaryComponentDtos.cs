// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryComponentDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：薪资组成表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资组成表Dto
/// </summary>
public partial class TaktSalaryComponentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentDto()
    {
        ComponentCode = string.Empty;
        ComponentName = string.Empty;
        ComponentType = string.Empty;
        CalculationMethod = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 薪资组成表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryComponentId { get; set; } = 0;

    /// <summary>
    /// 组成编码
    /// </summary>
    public string ComponentCode { get; set; }
    /// <summary>
    /// 组成名称
    /// </summary>
    public string ComponentName { get; set; }
    /// <summary>
    /// 组成类型
    /// </summary>
    public string ComponentType { get; set; }
    /// <summary>
    /// 计算方式
    /// </summary>
    public string CalculationMethod { get; set; }
    /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }
    /// <summary>
    /// 固定金额
    /// </summary>
    public decimal FixedAmount { get; set; }
    /// <summary>
    /// 比例
    /// </summary>
    public decimal Percentage { get; set; }
    /// <summary>
    /// 是否计税
    /// </summary>
    public int IsTaxable { get; set; }
    /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int IsSocialSecurityBase { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪资组成表查询DTO
/// </summary>
public partial class TaktSalaryComponentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 组成编码
    /// </summary>
    public string? ComponentCode { get; set; }
    /// <summary>
    /// 组成名称
    /// </summary>
    public string? ComponentName { get; set; }
    /// <summary>
    /// 组成类型
    /// </summary>
    public string? ComponentType { get; set; }
    /// <summary>
    /// 计算方式
    /// </summary>
    public string? CalculationMethod { get; set; }
    /// <summary>
    /// 计算公式
    /// </summary>
    public string? CalculationFormula { get; set; }
    /// <summary>
    /// 固定金额
    /// </summary>
    public decimal? FixedAmount { get; set; }
    /// <summary>
    /// 比例
    /// </summary>
    public decimal? Percentage { get; set; }
    /// <summary>
    /// 是否计税
    /// </summary>
    public int? IsTaxable { get; set; }
    /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int? IsSocialSecurityBase { get; set; }
    /// <summary>
    /// 说明
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
/// Takt创建薪资组成表DTO
/// </summary>
public partial class TaktSalaryComponentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentCreateDto()
    {
        ComponentCode = string.Empty;
        ComponentName = string.Empty;
        ComponentType = string.Empty;
        CalculationMethod = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 组成编码
    /// </summary>
    public string ComponentCode { get; set; }

        /// <summary>
    /// 组成名称
    /// </summary>
    public string ComponentName { get; set; }

        /// <summary>
    /// 组成类型
    /// </summary>
    public string ComponentType { get; set; }

        /// <summary>
    /// 计算方式
    /// </summary>
    public string CalculationMethod { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 固定金额
    /// </summary>
    public decimal FixedAmount { get; set; }

        /// <summary>
    /// 比例
    /// </summary>
    public decimal Percentage { get; set; }

        /// <summary>
    /// 是否计税
    /// </summary>
    public int IsTaxable { get; set; }

        /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int IsSocialSecurityBase { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 说明
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
/// Takt更新薪资组成表DTO
/// </summary>
public partial class TaktSalaryComponentUpdateDto : TaktSalaryComponentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentUpdateDto()
    {
    }

        /// <summary>
    /// 薪资组成表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryComponentId { get; set; } = 0;
}

/// <summary>
/// 薪资组成表状态DTO
/// </summary>
public partial class TaktSalaryComponentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentStatusDto()
    {
    }

        /// <summary>
    /// 薪资组成表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryComponentId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪资组成表排序DTO
/// </summary>
public partial class TaktSalaryComponentSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentSortDto()
    {
    }

        /// <summary>
    /// 薪资组成表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryComponentId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 薪资组成表导入模板DTO
/// </summary>
public partial class TaktSalaryComponentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentTemplateDto()
    {
        ComponentCode = string.Empty;
        ComponentName = string.Empty;
        ComponentType = string.Empty;
        CalculationMethod = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 组成编码
    /// </summary>
    public string ComponentCode { get; set; }

        /// <summary>
    /// 组成名称
    /// </summary>
    public string ComponentName { get; set; }

        /// <summary>
    /// 组成类型
    /// </summary>
    public string ComponentType { get; set; }

        /// <summary>
    /// 计算方式
    /// </summary>
    public string CalculationMethod { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 固定金额
    /// </summary>
    public decimal FixedAmount { get; set; }

        /// <summary>
    /// 比例
    /// </summary>
    public decimal Percentage { get; set; }

        /// <summary>
    /// 是否计税
    /// </summary>
    public int IsTaxable { get; set; }

        /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int IsSocialSecurityBase { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 说明
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
/// 薪资组成表导入DTO
/// </summary>
public partial class TaktSalaryComponentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentImportDto()
    {
        ComponentCode = string.Empty;
        ComponentName = string.Empty;
        ComponentType = string.Empty;
        CalculationMethod = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 组成编码
    /// </summary>
    public string ComponentCode { get; set; }

        /// <summary>
    /// 组成名称
    /// </summary>
    public string ComponentName { get; set; }

        /// <summary>
    /// 组成类型
    /// </summary>
    public string ComponentType { get; set; }

        /// <summary>
    /// 计算方式
    /// </summary>
    public string CalculationMethod { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 固定金额
    /// </summary>
    public decimal FixedAmount { get; set; }

        /// <summary>
    /// 比例
    /// </summary>
    public decimal Percentage { get; set; }

        /// <summary>
    /// 是否计税
    /// </summary>
    public int IsTaxable { get; set; }

        /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int IsSocialSecurityBase { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 说明
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
/// 薪资组成表导出DTO
/// </summary>
public partial class TaktSalaryComponentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryComponentExportDto()
    {
        CreatedAt = DateTime.Now;
        ComponentCode = string.Empty;
        ComponentName = string.Empty;
        ComponentType = string.Empty;
        CalculationMethod = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 组成编码
    /// </summary>
    public string ComponentCode { get; set; }

        /// <summary>
    /// 组成名称
    /// </summary>
    public string ComponentName { get; set; }

        /// <summary>
    /// 组成类型
    /// </summary>
    public string ComponentType { get; set; }

        /// <summary>
    /// 计算方式
    /// </summary>
    public string CalculationMethod { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 固定金额
    /// </summary>
    public decimal FixedAmount { get; set; }

        /// <summary>
    /// 比例
    /// </summary>
    public decimal Percentage { get; set; }

        /// <summary>
    /// 是否计税
    /// </summary>
    public int IsTaxable { get; set; }

        /// <summary>
    /// 是否纳入社保基数
    /// </summary>
    public int IsSocialSecurityBase { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 说明
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