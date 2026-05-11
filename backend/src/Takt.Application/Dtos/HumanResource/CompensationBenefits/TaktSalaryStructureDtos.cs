// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructureDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：薪资结构表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资结构表Dto
/// </summary>
public partial class TaktSalaryStructureDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureDto()
    {
        StructureCode = string.Empty;
        StructureName = string.Empty;
        SalaryLevel = string.Empty;
        SalaryGrade = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 薪资结构表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryStructureId { get; set; } = 0;

    /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string StructureCode { get; set; }
    /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string StructureName { get; set; }
    /// <summary>
    /// 薪资等级
    /// </summary>
    public string SalaryLevel { get; set; }
    /// <summary>
    /// 薪资档次
    /// </summary>
    public string SalaryGrade { get; set; }
    /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal MinSalary { get; set; }
    /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal MidSalary { get; set; }
    /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal MaxSalary { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }
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
/// 薪资结构表查询DTO
/// </summary>
public partial class TaktSalaryStructureQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string? StructureCode { get; set; }
    /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string? StructureName { get; set; }
    /// <summary>
    /// 薪资等级
    /// </summary>
    public string? SalaryLevel { get; set; }
    /// <summary>
    /// 薪资档次
    /// </summary>
    public string? SalaryGrade { get; set; }
    /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal? MinSalary { get; set; }
    /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal? MidSalary { get; set; }
    /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal? MaxSalary { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string? ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string? ApplicablePosition { get; set; }
    /// <summary>
    /// 说明
    /// </summary>
    public string? Description { get; set; }
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
/// Takt创建薪资结构表DTO
/// </summary>
public partial class TaktSalaryStructureCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureCreateDto()
    {
        StructureCode = string.Empty;
        StructureName = string.Empty;
        SalaryLevel = string.Empty;
        SalaryGrade = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string StructureCode { get; set; }

        /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string StructureName { get; set; }

        /// <summary>
    /// 薪资等级
    /// </summary>
    public string SalaryLevel { get; set; }

        /// <summary>
    /// 薪资档次
    /// </summary>
    public string SalaryGrade { get; set; }

        /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal MinSalary { get; set; }

        /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal MidSalary { get; set; }

        /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal MaxSalary { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }

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
/// Takt更新薪资结构表DTO
/// </summary>
public partial class TaktSalaryStructureUpdateDto : TaktSalaryStructureCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureUpdateDto()
    {
    }

        /// <summary>
    /// 薪资结构表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryStructureId { get; set; } = 0;
}

/// <summary>
/// 薪资结构表状态DTO
/// </summary>
public partial class TaktSalaryStructureStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureStatusDto()
    {
    }

        /// <summary>
    /// 薪资结构表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalaryStructureId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 薪资结构表导入模板DTO
/// </summary>
public partial class TaktSalaryStructureTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureTemplateDto()
    {
        StructureCode = string.Empty;
        StructureName = string.Empty;
        SalaryLevel = string.Empty;
        SalaryGrade = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string StructureCode { get; set; }

        /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string StructureName { get; set; }

        /// <summary>
    /// 薪资等级
    /// </summary>
    public string SalaryLevel { get; set; }

        /// <summary>
    /// 薪资档次
    /// </summary>
    public string SalaryGrade { get; set; }

        /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal MinSalary { get; set; }

        /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal MidSalary { get; set; }

        /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal MaxSalary { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }

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
/// 薪资结构表导入DTO
/// </summary>
public partial class TaktSalaryStructureImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureImportDto()
    {
        StructureCode = string.Empty;
        StructureName = string.Empty;
        SalaryLevel = string.Empty;
        SalaryGrade = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string StructureCode { get; set; }

        /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string StructureName { get; set; }

        /// <summary>
    /// 薪资等级
    /// </summary>
    public string SalaryLevel { get; set; }

        /// <summary>
    /// 薪资档次
    /// </summary>
    public string SalaryGrade { get; set; }

        /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal MinSalary { get; set; }

        /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal MidSalary { get; set; }

        /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal MaxSalary { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }

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
/// 薪资结构表导出DTO
/// </summary>
public partial class TaktSalaryStructureExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalaryStructureExportDto()
    {
        CreatedAt = DateTime.Now;
        StructureCode = string.Empty;
        StructureName = string.Empty;
        SalaryLevel = string.Empty;
        SalaryGrade = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 薪资结构编码
    /// </summary>
    public string StructureCode { get; set; }

        /// <summary>
    /// 薪资结构名称
    /// </summary>
    public string StructureName { get; set; }

        /// <summary>
    /// 薪资等级
    /// </summary>
    public string SalaryLevel { get; set; }

        /// <summary>
    /// 薪资档次
    /// </summary>
    public string SalaryGrade { get; set; }

        /// <summary>
    /// 最低薪资
    /// </summary>
    public decimal MinSalary { get; set; }

        /// <summary>
    /// 中位薪资
    /// </summary>
    public decimal MidSalary { get; set; }

        /// <summary>
    /// 最高薪资
    /// </summary>
    public decimal MaxSalary { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }

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