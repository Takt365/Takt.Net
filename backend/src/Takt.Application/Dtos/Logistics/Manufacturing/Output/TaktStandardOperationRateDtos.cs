// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktStandardOperationRateDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：标准生产稼动率表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 标准生产稼动率表Dto
/// </summary>
public partial class TaktStandardOperationRateDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateDto()
    {
        PlantCode = string.Empty;
        FinancialYear = string.Empty;
    }

    /// <summary>
    /// 标准生产稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationRateId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 财务年度
    /// </summary>
    public string FinancialYear { get; set; }
    /// <summary>
    /// 稼动率类型
    /// </summary>
    public int OperationType { get; set; }
    /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal OperationRate { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 标准生产稼动率表查询DTO
/// </summary>
public partial class TaktStandardOperationRateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 标准生产稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationRateId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 财务年度
    /// </summary>
    public string? FinancialYear { get; set; }
    /// <summary>
    /// 稼动率类型
    /// </summary>
    public int? OperationType { get; set; }
    /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal? OperationRate { get; set; }
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
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? ExpiryDateStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? ExpiryDateEnd { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建标准生产稼动率表DTO
/// </summary>
public partial class TaktStandardOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateCreateDto()
    {
        PlantCode = string.Empty;
        FinancialYear = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 财务年度
    /// </summary>
    public string FinancialYear { get; set; }

        /// <summary>
    /// 稼动率类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal OperationRate { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// Takt更新标准生产稼动率表DTO
/// </summary>
public partial class TaktStandardOperationRateUpdateDto : TaktStandardOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateUpdateDto()
    {
    }

        /// <summary>
    /// 标准生产稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationRateId { get; set; }
}

/// <summary>
/// 标准生产稼动率表状态DTO
/// </summary>
public partial class TaktStandardOperationRateStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateStatusDto()
    {
    }

        /// <summary>
    /// 标准生产稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationRateId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 标准生产稼动率表导入模板DTO
/// </summary>
public partial class TaktStandardOperationRateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateTemplateDto()
    {
        PlantCode = string.Empty;
        FinancialYear = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 财务年度
    /// </summary>
    public string FinancialYear { get; set; }

        /// <summary>
    /// 稼动率类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal OperationRate { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// 标准生产稼动率表导入DTO
/// </summary>
public partial class TaktStandardOperationRateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateImportDto()
    {
        PlantCode = string.Empty;
        FinancialYear = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 财务年度
    /// </summary>
    public string FinancialYear { get; set; }

        /// <summary>
    /// 稼动率类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal OperationRate { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

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
/// 标准生产稼动率表导出DTO
/// </summary>
public partial class TaktStandardOperationRateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationRateExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        FinancialYear = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 财务年度
    /// </summary>
    public string FinancialYear { get; set; }

        /// <summary>
    /// 稼动率类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 稼动率(%)
    /// </summary>
    public decimal OperationRate { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}