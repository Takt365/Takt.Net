// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktStandardWageRateDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：标准工资率表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// 标准工资率表Dto
/// </summary>
public partial class TaktStandardWageRateDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateDto()
    {
        YearMonth = string.Empty;
    }

    /// <summary>
    /// 标准工资率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardWageRateId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 年月
    /// </summary>
    public string YearMonth { get; set; }
    /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; }
    /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; }
    /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; }
    /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; }
    /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; }
    /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; }
    /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal DirectWageRate { get; set; }
    /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; }
    /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; }
    /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; }
    /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; }
    /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal IndirectWageRate { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
}

/// <summary>
/// 标准工资率表查询DTO
/// </summary>
public partial class TaktStandardWageRateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 标准工资率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardWageRateId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 年月
    /// </summary>
    public string? YearMonth { get; set; }
    /// <summary>
    /// 工作天数
    /// </summary>
    public decimal? WorkingDays { get; set; }
    /// <summary>
    /// 销售额
    /// </summary>
    public decimal? SalesAmount { get; set; }
    /// <summary>
    /// 直接人数
    /// </summary>
    public int? DirectLaborCount { get; set; }
    /// <summary>
    /// 直接工资
    /// </summary>
    public decimal? DirectLaborWage { get; set; }
    /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal? DirectOvertimeHours { get; set; }
    /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal? DirectOvertimeTotal { get; set; }
    /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal? DirectWageRate { get; set; }
    /// <summary>
    /// 间接人数
    /// </summary>
    public int? IndirectLaborCount { get; set; }
    /// <summary>
    /// 间接工资
    /// </summary>
    public decimal? IndirectLaborWage { get; set; }
    /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal? IndirectOvertimeHours { get; set; }
    /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal? IndirectOvertimeTotal { get; set; }
    /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal? IndirectWageRate { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// Takt创建标准工资率表DTO
/// </summary>
public partial class TaktStandardWageRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateCreateDto()
    {
        YearMonth = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 年月
    /// </summary>
    public string YearMonth { get; set; }

        /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; }

        /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; }

        /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; }

        /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; }

        /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; }

        /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; }

        /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal DirectWageRate { get; set; }

        /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; }

        /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; }

        /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; }

        /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; }

        /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal IndirectWageRate { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// Takt更新标准工资率表DTO
/// </summary>
public partial class TaktStandardWageRateUpdateDto : TaktStandardWageRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateUpdateDto()
    {
    }

        /// <summary>
    /// 标准工资率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardWageRateId { get; set; }
}

/// <summary>
/// 标准工资率表导入模板DTO
/// </summary>
public partial class TaktStandardWageRateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateTemplateDto()
    {
        YearMonth = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 年月
    /// </summary>
    public string YearMonth { get; set; }

        /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; }

        /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; }

        /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; }

        /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; }

        /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; }

        /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; }

        /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal DirectWageRate { get; set; }

        /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; }

        /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; }

        /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; }

        /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; }

        /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal IndirectWageRate { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// 标准工资率表导入DTO
/// </summary>
public partial class TaktStandardWageRateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateImportDto()
    {
        YearMonth = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 年月
    /// </summary>
    public string YearMonth { get; set; }

        /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; }

        /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; }

        /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; }

        /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; }

        /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; }

        /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; }

        /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal DirectWageRate { get; set; }

        /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; }

        /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; }

        /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; }

        /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; }

        /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal IndirectWageRate { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// 标准工资率表导出DTO
/// </summary>
public partial class TaktStandardWageRateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardWageRateExportDto()
    {
        CreatedAt = DateTime.Now;
        YearMonth = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 年月
    /// </summary>
    public string YearMonth { get; set; }

        /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; }

        /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; }

        /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; }

        /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; }

        /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; }

        /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; }

        /// <summary>
    /// 直接工资率
    /// </summary>
    public decimal DirectWageRate { get; set; }

        /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; }

        /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; }

        /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; }

        /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; }

        /// <summary>
    /// 间接工资率
    /// </summary>
    public decimal IndirectWageRate { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}