// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationReliabilityDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质业务信赖性评价ORT费用明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质业务信赖性评价ORT费用明细表Dto
/// </summary>
public partial class TaktQualityOperationReliabilityDto : TaktDtosEntityBase
{
    /// <summary>
    /// 品质业务信赖性评价ORT费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationReliabilityId { get; set; } = 0;

    /// <summary>
    /// 品质业务主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal TestCost { get; set; }
    /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }
    /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }
    /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    public TaktQualityOperationDto? Operation { get; set; }
}

/// <summary>
/// 品质业务信赖性评价ORT费用明细表查询DTO
/// </summary>
public partial class TaktQualityOperationReliabilityQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationReliabilityQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 品质业务主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? QualityOperationId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal? TestCost { get; set; }
    /// <summary>
    /// 评价作业时间
    /// </summary>
    public int? WorkTimeMinutes { get; set; }
    /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal? OtherExpenses { get; set; }
    /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

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
/// Takt创建品质业务信赖性评价ORT费用明细表DTO
/// </summary>
public partial class TaktQualityOperationReliabilityCreateDto
{
        /// <summary>
    /// 品质业务主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal TestCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

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
/// Takt更新品质业务信赖性评价ORT费用明细表DTO
/// </summary>
public partial class TaktQualityOperationReliabilityUpdateDto : TaktQualityOperationReliabilityCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationReliabilityUpdateDto()
    {
    }

        /// <summary>
    /// 品质业务信赖性评价ORT费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationReliabilityId { get; set; } = 0;
}

/// <summary>
/// 品质业务信赖性评价ORT费用明细表导入模板DTO
/// </summary>
public partial class TaktQualityOperationReliabilityTemplateDto
{
        /// <summary>
    /// 品质业务主表ID
    /// </summary>
    public long QualityOperationId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal TestCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

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
/// 品质业务信赖性评价ORT费用明细表导入DTO
/// </summary>
public partial class TaktQualityOperationReliabilityImportDto
{
        /// <summary>
    /// 品质业务主表ID
    /// </summary>
    public long QualityOperationId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal TestCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

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
/// 品质业务信赖性评价ORT费用明细表导出DTO
/// </summary>
public partial class TaktQualityOperationReliabilityExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationReliabilityExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 品质业务主表ID
    /// </summary>
    public long QualityOperationId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 信赖性评价ORT业务费用
    /// </summary>
    public decimal TestCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 信赖性评价备注
    /// </summary>
    public string? ReliabilityNote { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}