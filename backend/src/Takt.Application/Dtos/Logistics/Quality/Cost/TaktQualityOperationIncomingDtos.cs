// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationIncomingDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：品质业务来料检验费用明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 品质业务来料检验费用明细表Dto
/// </summary>
public partial class TaktQualityOperationIncomingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 品质业务来料检验费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationIncomingId { get; set; } = 0;

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
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal IncomingInspectionCost { get; set; }
    /// <summary>
    /// 检查时间
    /// </summary>
    public int InspectionTimeMinutes { get; set; }
    /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }
    /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }
    /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    public TaktQualityOperationDto? Operation { get; set; }
}

/// <summary>
/// 品质业务来料检验费用明细表查询DTO
/// </summary>
public partial class TaktQualityOperationIncomingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationIncomingQueryDto()
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
    /// 直接人员费率
    /// </summary>
    public decimal? DirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal? IncomingInspectionCost { get; set; }
    /// <summary>
    /// 检查时间
    /// </summary>
    public int? InspectionTimeMinutes { get; set; }
    /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal? TravelCost { get; set; }
    /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal? OtherExpenses { get; set; }
    /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

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
/// Takt创建品质业务来料检验费用明细表DTO
/// </summary>
public partial class TaktQualityOperationIncomingCreateDto
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
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal IncomingInspectionCost { get; set; }

        /// <summary>
    /// 检查时间
    /// </summary>
    public int InspectionTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

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
/// Takt更新品质业务来料检验费用明细表DTO
/// </summary>
public partial class TaktQualityOperationIncomingUpdateDto : TaktQualityOperationIncomingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationIncomingUpdateDto()
    {
    }

        /// <summary>
    /// 品质业务来料检验费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityOperationIncomingId { get; set; } = 0;
}

/// <summary>
/// 品质业务来料检验费用明细表导入模板DTO
/// </summary>
public partial class TaktQualityOperationIncomingTemplateDto
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
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal IncomingInspectionCost { get; set; }

        /// <summary>
    /// 检查时间
    /// </summary>
    public int InspectionTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

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
/// 品质业务来料检验费用明细表导入DTO
/// </summary>
public partial class TaktQualityOperationIncomingImportDto
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
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal IncomingInspectionCost { get; set; }

        /// <summary>
    /// 检查时间
    /// </summary>
    public int InspectionTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

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
/// 品质业务来料检验费用明细表导出DTO
/// </summary>
public partial class TaktQualityOperationIncomingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationIncomingExportDto()
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
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 来料检验业务费用
    /// </summary>
    public decimal IncomingInspectionCost { get; set; }

        /// <summary>
    /// 检查时间
    /// </summary>
    public int InspectionTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 检查其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 来料检验备注
    /// </summary>
    public string? IncomingNote { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}