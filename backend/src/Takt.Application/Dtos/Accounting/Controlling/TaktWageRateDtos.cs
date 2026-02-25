// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Controlling
// 文件名称：TaktWageRateDtos.cs
// 功能描述：Takt工资率DTO，包含工资率相关的数据传输对象（查询、创建、更新、导入、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Controlling;

/// <summary>
/// Takt工资率DTO
/// </summary>
public class TaktWageRateDto
{
    /// <summary>
    /// 工资率ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WageRateId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（格式yyyyMM）
    /// </summary>
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int WageRateType { get; set; }

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
    /// 直接工资率（元/小时）
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
    /// 间接工资率（元/小时）
    /// </summary>
    public decimal IndirectWageRate { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt工资率查询DTO
/// </summary>
public class TaktWageRateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 年月（yyyyMM）
    /// </summary>
    public string? YearMonth { get; set; }

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int? WageRateType { get; set; }
}

/// <summary>
/// Takt创建工资率DTO
/// </summary>
public class TaktWageRateCreateDto
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（yyyyMM）
    /// </summary>
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int WageRateType { get; set; } = 0;

    /// <summary>
    /// 工作天数
    /// </summary>
    public decimal WorkingDays { get; set; } = 21.7m;

    /// <summary>
    /// 销售额
    /// </summary>
    public decimal SalesAmount { get; set; } = 0;

    /// <summary>
    /// 直接人数
    /// </summary>
    public int DirectLaborCount { get; set; } = 0;

    /// <summary>
    /// 直接工资
    /// </summary>
    public decimal DirectLaborWage { get; set; } = 0;

    /// <summary>
    /// 直接加班小时
    /// </summary>
    public decimal DirectOvertimeHours { get; set; } = 0;

    /// <summary>
    /// 直接加班总额
    /// </summary>
    public decimal DirectOvertimeTotal { get; set; } = 0;

    /// <summary>
    /// 直接工资率（元/小时）
    /// </summary>
    public decimal DirectWageRate { get; set; } = 0;

    /// <summary>
    /// 间接人数
    /// </summary>
    public int IndirectLaborCount { get; set; } = 0;

    /// <summary>
    /// 间接工资
    /// </summary>
    public decimal IndirectLaborWage { get; set; } = 0;

    /// <summary>
    /// 间接加班小时
    /// </summary>
    public decimal IndirectOvertimeHours { get; set; } = 0;

    /// <summary>
    /// 间接加班总额
    /// </summary>
    public decimal IndirectOvertimeTotal { get; set; } = 0;

    /// <summary>
    /// 间接工资率（元/小时）
    /// </summary>
    public decimal IndirectWageRate { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新工资率DTO
/// </summary>
public class TaktWageRateUpdateDto : TaktWageRateCreateDto
{
    /// <summary>
    /// 工资率ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WageRateId { get; set; }
}

/// <summary>
/// Takt工资率导入模板DTO
/// </summary>
public class TaktWageRateTemplateDto
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（yyyyMM）
    /// </summary>
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int WageRateType { get; set; }

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
    /// 直接工资率（元/小时）
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
    /// 间接工资率（元/小时）
    /// </summary>
    public decimal IndirectWageRate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt工资率导入DTO
/// </summary>
public class TaktWageRateImportDto
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（yyyyMM）
    /// </summary>
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int WageRateType { get; set; }

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
    /// 直接工资率（元/小时）
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
    /// 间接工资率（元/小时）
    /// </summary>
    public decimal IndirectWageRate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt工资率导出DTO
/// </summary>
public class TaktWageRateExportDto
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（yyyyMM）
    /// </summary>
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    public int WageRateType { get; set; }

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
    /// 直接工资率（元/小时）
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
    /// 间接工资率（元/小时）
    /// </summary>
    public decimal IndirectWageRate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
