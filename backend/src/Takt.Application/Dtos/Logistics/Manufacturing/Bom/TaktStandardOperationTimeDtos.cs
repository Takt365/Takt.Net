// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Bom
// 文件名称：TaktStandardOperationTimeDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：标准工序时间表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom;

/// <summary>
/// 标准工序时间表Dto
/// </summary>
public partial class TaktStandardOperationTimeDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        WorkCenter = string.Empty;
        TimeUnit = string.Empty;
        PointsUnit = string.Empty;
    }

    /// <summary>
    /// 标准工序时间表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationTimeId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 工作中心
    /// </summary>
    public string WorkCenter { get; set; }
    /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }
    /// <summary>
    /// 标准工时
    /// </summary>
    public decimal StandardMinutes { get; set; }
    /// <summary>
    /// 工时单位
    /// </summary>
    public string TimeUnit { get; set; }
    /// <summary>
    /// 标准点数
    /// </summary>
    public int StandardShorts { get; set; }
    /// <summary>
    /// 点数单位
    /// </summary>
    public string PointsUnit { get; set; }
    /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal PointsToMinutesRate { get; set; }
    /// <summary>
    /// 转换工时
    /// </summary>
    public decimal ConvertedMinutes { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
    /// <summary>
    /// 审核状态
    /// </summary>
    public int ApprovalStatus { get; set; }
    /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }
    /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }
}

/// <summary>
/// 标准工序时间表查询DTO
/// </summary>
public partial class TaktStandardOperationTimeQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 标准工序时间表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationTimeId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }
    /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }
    /// <summary>
    /// 标准工时
    /// </summary>
    public decimal? StandardMinutes { get; set; }
    /// <summary>
    /// 工时单位
    /// </summary>
    public string? TimeUnit { get; set; }
    /// <summary>
    /// 标准点数
    /// </summary>
    public int? StandardShorts { get; set; }
    /// <summary>
    /// 点数单位
    /// </summary>
    public string? PointsUnit { get; set; }
    /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal? PointsToMinutesRate { get; set; }
    /// <summary>
    /// 转换工时
    /// </summary>
    public decimal? ConvertedMinutes { get; set; }
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
    /// 审核状态
    /// </summary>
    public int? ApprovalStatus { get; set; }
    /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }
    /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// 审核日期开始时间
    /// </summary>
    public DateTime? ApprovalDateStart { get; set; }
    /// <summary>
    /// 审核日期结束时间
    /// </summary>
    public DateTime? ApprovalDateEnd { get; set; }

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
/// Takt创建标准工序时间表DTO
/// </summary>
public partial class TaktStandardOperationTimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeCreateDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        WorkCenter = string.Empty;
        TimeUnit = string.Empty;
        PointsUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string WorkCenter { get; set; }

        /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }

        /// <summary>
    /// 标准工时
    /// </summary>
    public decimal StandardMinutes { get; set; }

        /// <summary>
    /// 工时单位
    /// </summary>
    public string TimeUnit { get; set; }

        /// <summary>
    /// 标准点数
    /// </summary>
    public int StandardShorts { get; set; }

        /// <summary>
    /// 点数单位
    /// </summary>
    public string PointsUnit { get; set; }

        /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal PointsToMinutesRate { get; set; }

        /// <summary>
    /// 转换工时
    /// </summary>
    public decimal ConvertedMinutes { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public int ApprovalStatus { get; set; }

        /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }

        /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

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
/// Takt更新标准工序时间表DTO
/// </summary>
public partial class TaktStandardOperationTimeUpdateDto : TaktStandardOperationTimeCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeUpdateDto()
    {
    }

        /// <summary>
    /// 标准工序时间表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationTimeId { get; set; }
}

/// <summary>
/// 标准工序时间表审核状态DTO
/// </summary>
public partial class TaktStandardOperationTimeApprovalStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeApprovalStatusDto()
    {
    }

        /// <summary>
    /// 标准工序时间表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StandardOperationTimeId { get; set; }

    /// <summary>
    /// 审核状态（0=禁用，1=启用）
    /// </summary>
    public int ApprovalStatus { get; set; }
}

/// <summary>
/// 标准工序时间表导入模板DTO
/// </summary>
public partial class TaktStandardOperationTimeTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeTemplateDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        WorkCenter = string.Empty;
        TimeUnit = string.Empty;
        PointsUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string WorkCenter { get; set; }

        /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }

        /// <summary>
    /// 标准工时
    /// </summary>
    public decimal StandardMinutes { get; set; }

        /// <summary>
    /// 工时单位
    /// </summary>
    public string TimeUnit { get; set; }

        /// <summary>
    /// 标准点数
    /// </summary>
    public int StandardShorts { get; set; }

        /// <summary>
    /// 点数单位
    /// </summary>
    public string PointsUnit { get; set; }

        /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal PointsToMinutesRate { get; set; }

        /// <summary>
    /// 转换工时
    /// </summary>
    public decimal ConvertedMinutes { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public int ApprovalStatus { get; set; }

        /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }

        /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

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
/// 标准工序时间表导入DTO
/// </summary>
public partial class TaktStandardOperationTimeImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeImportDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        WorkCenter = string.Empty;
        TimeUnit = string.Empty;
        PointsUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string WorkCenter { get; set; }

        /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }

        /// <summary>
    /// 标准工时
    /// </summary>
    public decimal StandardMinutes { get; set; }

        /// <summary>
    /// 工时单位
    /// </summary>
    public string TimeUnit { get; set; }

        /// <summary>
    /// 标准点数
    /// </summary>
    public int StandardShorts { get; set; }

        /// <summary>
    /// 点数单位
    /// </summary>
    public string PointsUnit { get; set; }

        /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal PointsToMinutesRate { get; set; }

        /// <summary>
    /// 转换工时
    /// </summary>
    public decimal ConvertedMinutes { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public int ApprovalStatus { get; set; }

        /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }

        /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

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
/// 标准工序时间表导出DTO
/// </summary>
public partial class TaktStandardOperationTimeExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktStandardOperationTimeExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        WorkCenter = string.Empty;
        TimeUnit = string.Empty;
        PointsUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string WorkCenter { get; set; }

        /// <summary>
    /// 工序描述
    /// </summary>
    public string? OperationDesc { get; set; }

        /// <summary>
    /// 标准工时
    /// </summary>
    public decimal StandardMinutes { get; set; }

        /// <summary>
    /// 工时单位
    /// </summary>
    public string TimeUnit { get; set; }

        /// <summary>
    /// 标准点数
    /// </summary>
    public int StandardShorts { get; set; }

        /// <summary>
    /// 点数单位
    /// </summary>
    public string PointsUnit { get; set; }

        /// <summary>
    /// 转换汇率
    /// </summary>
    public decimal PointsToMinutesRate { get; set; }

        /// <summary>
    /// 转换工时
    /// </summary>
    public decimal ConvertedMinutes { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

        /// <summary>
    /// 审核状态
    /// </summary>
    public int ApprovalStatus { get; set; }

        /// <summary>
    /// 审核人
    /// </summary>
    public string? Approver { get; set; }

        /// <summary>
    /// 审核日期
    /// </summary>
    public DateTime? ApprovalDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}