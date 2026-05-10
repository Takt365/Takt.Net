// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationCustomerResponseDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质业务顾客品质要求对应费用明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质业务顾客品质要求对应费用明细表Dto
/// </summary>
public partial class TaktQualityOperationCustomerResponseDto : TaktDtosEntityBase
{
    /// <summary>
    /// 品质业务顾客品质要求对应费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationCustomerResponseId { get; set; } = 0;

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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal ResponseCost { get; set; }
    /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }
    /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }
    /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    public TaktQualityOperationDto? Operation { get; set; }
}

/// <summary>
/// 品质业务顾客品质要求对应费用明细表查询DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCustomerResponseQueryDto()
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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal? ResponseCost { get; set; }
    /// <summary>
    /// 评价作业时间
    /// </summary>
    public int? WorkTimeMinutes { get; set; }
    /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal? OtherExpenses { get; set; }
    /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

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
/// Takt创建品质业务顾客品质要求对应费用明细表DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseCreateDto
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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal ResponseCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

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
/// Takt更新品质业务顾客品质要求对应费用明细表DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseUpdateDto : TaktQualityOperationCustomerResponseCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCustomerResponseUpdateDto()
    {
    }

        /// <summary>
    /// 品质业务顾客品质要求对应费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationCustomerResponseId { get; set; } = 0;
}

/// <summary>
/// 品质业务顾客品质要求对应费用明细表导入模板DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseTemplateDto
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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal ResponseCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

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
/// 品质业务顾客品质要求对应费用明细表导入DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseImportDto
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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal ResponseCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

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
/// 品质业务顾客品质要求对应费用明细表导出DTO
/// </summary>
public partial class TaktQualityOperationCustomerResponseExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCustomerResponseExportDto()
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
    /// 顾客品质要求对应业务费用
    /// </summary>
    public decimal ResponseCost { get; set; }

        /// <summary>
    /// 评价作业时间
    /// </summary>
    public int WorkTimeMinutes { get; set; }

        /// <summary>
    /// 评价其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 顾客应对备注
    /// </summary>
    public string? CustomerResponseNote { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}