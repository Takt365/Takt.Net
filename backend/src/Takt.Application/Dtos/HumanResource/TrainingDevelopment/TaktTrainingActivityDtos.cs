// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingActivityDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：培训活动表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训活动表Dto
/// </summary>
public partial class TaktTrainingActivityDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityDto()
    {
        ActivityCode = string.Empty;
        ActivityName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        TrainingLocation = string.Empty;
        Instructor = string.Empty;
        ContentSummary = string.Empty;
        TrainingMaterials = string.Empty;
        EffectivenessEvaluation = string.Empty;
        ParticipantFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

    /// <summary>
    /// 培训活动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingActivityId { get; set; } = 0;

    /// <summary>
    /// 活动编码
    /// </summary>
    public string ActivityCode { get; set; }
    /// <summary>
    /// 活动名称
    /// </summary>
    public string ActivityName { get; set; }
    /// <summary>
    /// 培训课程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; }
    /// <summary>
    /// 培训计划ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingPlanId { get; set; }
    /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }
    /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }
    /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }
    /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }
    /// <summary>
    /// 计划人数
    /// </summary>
    public int PlannedAttendees { get; set; }
    /// <summary>
    /// 实际签到人数
    /// </summary>
    public int ActualAttendees { get; set; }
    /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }
    /// <summary>
    /// 培训费用
    /// </summary>
    public decimal TrainingCost { get; set; }
    /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string ContentSummary { get; set; }
    /// <summary>
    /// 培训材料
    /// </summary>
    public string TrainingMaterials { get; set; }
    /// <summary>
    /// 培训效果评估
    /// </summary>
    public string EffectivenessEvaluation { get; set; }
    /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string ParticipantFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }
    /// <summary>
    /// 组织者ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrganizerId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训活动表查询DTO
/// </summary>
public partial class TaktTrainingActivityQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 活动编码
    /// </summary>
    public string? ActivityCode { get; set; }
    /// <summary>
    /// 活动名称
    /// </summary>
    public string? ActivityName { get; set; }
    /// <summary>
    /// 培训课程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TrainingCourseId { get; set; }
    /// <summary>
    /// 培训计划ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TrainingPlanId { get; set; }
    /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime? TrainingDate { get; set; }

    /// <summary>
    /// 培训日期开始时间
    /// </summary>
    public DateTime? TrainingDateStart { get; set; }
    /// <summary>
    /// 培训日期结束时间
    /// </summary>
    public DateTime? TrainingDateEnd { get; set; }
    /// <summary>
    /// 开始时间
    /// </summary>
    public string? StartTime { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public string? EndTime { get; set; }
    /// <summary>
    /// 培训地点
    /// </summary>
    public string? TrainingLocation { get; set; }
    /// <summary>
    /// 培训讲师
    /// </summary>
    public string? Instructor { get; set; }
    /// <summary>
    /// 计划人数
    /// </summary>
    public int? PlannedAttendees { get; set; }
    /// <summary>
    /// 实际签到人数
    /// </summary>
    public int? ActualAttendees { get; set; }
    /// <summary>
    /// 培训时长
    /// </summary>
    public decimal? TrainingHours { get; set; }
    /// <summary>
    /// 培训费用
    /// </summary>
    public decimal? TrainingCost { get; set; }
    /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string? ContentSummary { get; set; }
    /// <summary>
    /// 培训材料
    /// </summary>
    public string? TrainingMaterials { get; set; }
    /// <summary>
    /// 培训效果评估
    /// </summary>
    public string? EffectivenessEvaluation { get; set; }
    /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string? ParticipantFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestions { get; set; }
    /// <summary>
    /// 组织者ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? OrganizerId { get; set; }
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
/// Takt创建培训活动表DTO
/// </summary>
public partial class TaktTrainingActivityCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityCreateDto()
    {
        ActivityCode = string.Empty;
        ActivityName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        TrainingLocation = string.Empty;
        Instructor = string.Empty;
        ContentSummary = string.Empty;
        TrainingMaterials = string.Empty;
        EffectivenessEvaluation = string.Empty;
        ParticipantFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 活动编码
    /// </summary>
    public string ActivityCode { get; set; }

        /// <summary>
    /// 活动名称
    /// </summary>
    public string ActivityName { get; set; }

        /// <summary>
    /// 培训课程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; }

        /// <summary>
    /// 培训计划ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingPlanId { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }

        /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 计划人数
    /// </summary>
    public int PlannedAttendees { get; set; }

        /// <summary>
    /// 实际签到人数
    /// </summary>
    public int ActualAttendees { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训费用
    /// </summary>
    public decimal TrainingCost { get; set; }

        /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string ContentSummary { get; set; }

        /// <summary>
    /// 培训材料
    /// </summary>
    public string TrainingMaterials { get; set; }

        /// <summary>
    /// 培训效果评估
    /// </summary>
    public string EffectivenessEvaluation { get; set; }

        /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string ParticipantFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 组织者ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OrganizerId { get; set; }

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
/// Takt更新培训活动表DTO
/// </summary>
public partial class TaktTrainingActivityUpdateDto : TaktTrainingActivityCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityUpdateDto()
    {
    }

        /// <summary>
    /// 培训活动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingActivityId { get; set; } = 0;
}

/// <summary>
/// 培训活动表状态DTO
/// </summary>
public partial class TaktTrainingActivityStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityStatusDto()
    {
    }

        /// <summary>
    /// 培训活动表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingActivityId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训活动表导入模板DTO
/// </summary>
public partial class TaktTrainingActivityTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityTemplateDto()
    {
        ActivityCode = string.Empty;
        ActivityName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        TrainingLocation = string.Empty;
        Instructor = string.Empty;
        ContentSummary = string.Empty;
        TrainingMaterials = string.Empty;
        EffectivenessEvaluation = string.Empty;
        ParticipantFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 活动编码
    /// </summary>
    public string ActivityCode { get; set; }

        /// <summary>
    /// 活动名称
    /// </summary>
    public string ActivityName { get; set; }

        /// <summary>
    /// 培训课程ID
    /// </summary>
    public long TrainingCourseId { get; set; }

        /// <summary>
    /// 培训计划ID
    /// </summary>
    public long TrainingPlanId { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }

        /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 计划人数
    /// </summary>
    public int PlannedAttendees { get; set; }

        /// <summary>
    /// 实际签到人数
    /// </summary>
    public int ActualAttendees { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训费用
    /// </summary>
    public decimal TrainingCost { get; set; }

        /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string ContentSummary { get; set; }

        /// <summary>
    /// 培训材料
    /// </summary>
    public string TrainingMaterials { get; set; }

        /// <summary>
    /// 培训效果评估
    /// </summary>
    public string EffectivenessEvaluation { get; set; }

        /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string ParticipantFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 组织者ID
    /// </summary>
    public long OrganizerId { get; set; }

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
/// 培训活动表导入DTO
/// </summary>
public partial class TaktTrainingActivityImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityImportDto()
    {
        ActivityCode = string.Empty;
        ActivityName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        TrainingLocation = string.Empty;
        Instructor = string.Empty;
        ContentSummary = string.Empty;
        TrainingMaterials = string.Empty;
        EffectivenessEvaluation = string.Empty;
        ParticipantFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 活动编码
    /// </summary>
    public string ActivityCode { get; set; }

        /// <summary>
    /// 活动名称
    /// </summary>
    public string ActivityName { get; set; }

        /// <summary>
    /// 培训课程ID
    /// </summary>
    public long TrainingCourseId { get; set; }

        /// <summary>
    /// 培训计划ID
    /// </summary>
    public long TrainingPlanId { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }

        /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 计划人数
    /// </summary>
    public int PlannedAttendees { get; set; }

        /// <summary>
    /// 实际签到人数
    /// </summary>
    public int ActualAttendees { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训费用
    /// </summary>
    public decimal TrainingCost { get; set; }

        /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string ContentSummary { get; set; }

        /// <summary>
    /// 培训材料
    /// </summary>
    public string TrainingMaterials { get; set; }

        /// <summary>
    /// 培训效果评估
    /// </summary>
    public string EffectivenessEvaluation { get; set; }

        /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string ParticipantFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 组织者ID
    /// </summary>
    public long OrganizerId { get; set; }

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
/// 培训活动表导出DTO
/// </summary>
public partial class TaktTrainingActivityExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivityExportDto()
    {
        CreatedAt = DateTime.Now;
        ActivityCode = string.Empty;
        ActivityName = string.Empty;
        StartTime = string.Empty;
        EndTime = string.Empty;
        TrainingLocation = string.Empty;
        Instructor = string.Empty;
        ContentSummary = string.Empty;
        TrainingMaterials = string.Empty;
        EffectivenessEvaluation = string.Empty;
        ParticipantFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 活动编码
    /// </summary>
    public string ActivityCode { get; set; }

        /// <summary>
    /// 活动名称
    /// </summary>
    public string ActivityName { get; set; }

        /// <summary>
    /// 培训课程ID
    /// </summary>
    public long TrainingCourseId { get; set; }

        /// <summary>
    /// 培训计划ID
    /// </summary>
    public long TrainingPlanId { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; }

        /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 计划人数
    /// </summary>
    public int PlannedAttendees { get; set; }

        /// <summary>
    /// 实际签到人数
    /// </summary>
    public int ActualAttendees { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训费用
    /// </summary>
    public decimal TrainingCost { get; set; }

        /// <summary>
    /// 培训内容摘要
    /// </summary>
    public string ContentSummary { get; set; }

        /// <summary>
    /// 培训材料
    /// </summary>
    public string TrainingMaterials { get; set; }

        /// <summary>
    /// 培训效果评估
    /// </summary>
    public string EffectivenessEvaluation { get; set; }

        /// <summary>
    /// 学员反馈意见
    /// </summary>
    public string ParticipantFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 组织者ID
    /// </summary>
    public long OrganizerId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}