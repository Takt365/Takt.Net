// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueMeetingDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：质量问题会议调查试验费用明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Cost;

/// <summary>
/// 质量问题会议调查试验费用明细表Dto
/// </summary>
public partial class TaktQualityIssueMeetingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 质量问题会议调查试验费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueMeetingId { get; set; } = 0;

    /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }
    /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal MeetingInvestigationCost { get; set; }
    /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int MeetingTimeMinutes { get; set; }
    /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int DirectParticipantCount { get; set; }
    /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int IndirectParticipantCount { get; set; }
    /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int InvestigationWorkTimeMinutes { get; set; }
    /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }
    /// <summary>
    /// 其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }
    /// <summary>
    /// 其他作业时间
    /// </summary>
    public int OtherWorkTimeMinutes { get; set; }
    /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal OtherApparatusCost { get; set; }
    /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

    /// <summary>
    /// 质量问题主表（导航属性）
    /// </summary>
    public TaktQualityIssueDto? Issue { get; set; }
}

/// <summary>
/// 质量问题会议调查试验费用明细表查询DTO
/// </summary>
public partial class TaktQualityIssueMeetingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueMeetingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? QualityIssueId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal? DirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal? IndirectManpowerCostPerMinute { get; set; }
    /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }
    /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal? MeetingInvestigationCost { get; set; }
    /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int? MeetingTimeMinutes { get; set; }
    /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int? DirectParticipantCount { get; set; }
    /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int? IndirectParticipantCount { get; set; }
    /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int? InvestigationWorkTimeMinutes { get; set; }
    /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal? TravelCost { get; set; }
    /// <summary>
    /// 其他费用
    /// </summary>
    public decimal? OtherExpenses { get; set; }
    /// <summary>
    /// 其他作业时间
    /// </summary>
    public int? OtherWorkTimeMinutes { get; set; }
    /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal? OtherApparatusCost { get; set; }
    /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

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
/// Takt创建质量问题会议调查试验费用明细表DTO
/// </summary>
public partial class TaktQualityIssueMeetingCreateDto
{
        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }

        /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal MeetingInvestigationCost { get; set; }

        /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int MeetingTimeMinutes { get; set; }

        /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int DirectParticipantCount { get; set; }

        /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int IndirectParticipantCount { get; set; }

        /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int InvestigationWorkTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 其他作业时间
    /// </summary>
    public int OtherWorkTimeMinutes { get; set; }

        /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal OtherApparatusCost { get; set; }

        /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

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
/// Takt更新质量问题会议调查试验费用明细表DTO
/// </summary>
public partial class TaktQualityIssueMeetingUpdateDto : TaktQualityIssueMeetingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueMeetingUpdateDto()
    {
    }

        /// <summary>
    /// 质量问题会议调查试验费用明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QualityIssueMeetingId { get; set; } = 0;
}

/// <summary>
/// 质量问题会议调查试验费用明细表导入模板DTO
/// </summary>
public partial class TaktQualityIssueMeetingTemplateDto
{
        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }

        /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal MeetingInvestigationCost { get; set; }

        /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int MeetingTimeMinutes { get; set; }

        /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int DirectParticipantCount { get; set; }

        /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int IndirectParticipantCount { get; set; }

        /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int InvestigationWorkTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 其他作业时间
    /// </summary>
    public int OtherWorkTimeMinutes { get; set; }

        /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal OtherApparatusCost { get; set; }

        /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

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
/// 质量问题会议调查试验费用明细表导入DTO
/// </summary>
public partial class TaktQualityIssueMeetingImportDto
{
        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }

        /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal MeetingInvestigationCost { get; set; }

        /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int MeetingTimeMinutes { get; set; }

        /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int DirectParticipantCount { get; set; }

        /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int IndirectParticipantCount { get; set; }

        /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int InvestigationWorkTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 其他作业时间
    /// </summary>
    public int OtherWorkTimeMinutes { get; set; }

        /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal OtherApparatusCost { get; set; }

        /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

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
/// 质量问题会议调查试验费用明细表导出DTO
/// </summary>
public partial class TaktQualityIssueMeetingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueMeetingExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 品质问题主表ID
    /// </summary>
    public long QualityIssueId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 直接人员费率
    /// </summary>
    public decimal DirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 间接人员费率
    /// </summary>
    public decimal IndirectManpowerCostPerMinute { get; set; }

        /// <summary>
    /// 讨论调查试验内容
    /// </summary>
    public string? MeetingInvestigationContent { get; set; }

        /// <summary>
    /// 讨论调查试验费用
    /// </summary>
    public decimal MeetingInvestigationCost { get; set; }

        /// <summary>
    /// 检讨会使用时间
    /// </summary>
    public int MeetingTimeMinutes { get; set; }

        /// <summary>
    /// 直接人员参加人数
    /// </summary>
    public int DirectParticipantCount { get; set; }

        /// <summary>
    /// 间接人员参加人数
    /// </summary>
    public int IndirectParticipantCount { get; set; }

        /// <summary>
    /// 调查评价试验工作时间
    /// </summary>
    public int InvestigationWorkTimeMinutes { get; set; }

        /// <summary>
    /// 交通费旅费
    /// </summary>
    public decimal TravelCost { get; set; }

        /// <summary>
    /// 其他费用
    /// </summary>
    public decimal OtherExpenses { get; set; }

        /// <summary>
    /// 其他作业时间
    /// </summary>
    public int OtherWorkTimeMinutes { get; set; }

        /// <summary>
    /// 其他设备工程搬运费
    /// </summary>
    public decimal OtherApparatusCost { get; set; }

        /// <summary>
    /// 品质问题对应记录者
    /// </summary>
    public string? MeetingRecorder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}