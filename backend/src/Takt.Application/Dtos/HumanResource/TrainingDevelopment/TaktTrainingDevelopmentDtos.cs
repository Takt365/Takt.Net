// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingDevelopmentDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：培训发展表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训发展表Dto
/// </summary>
public partial class TaktTrainingDevelopmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentDto()
    {
        CourseName = string.Empty;
        TrainingType = string.Empty;
        Instructor = string.Empty;
        TrainingLocation = string.Empty;
        CertificateNo = string.Empty;
        TrainingEvaluation = string.Empty;
        ImprovementSuggestions = string.Empty;
        DevelopmentPlan = string.Empty;
    }

    /// <summary>
    /// 培训发展表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingDevelopmentId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 培训课程名称
    /// </summary>
    public string CourseName { get; set; }
    /// <summary>
    /// 培训类型
    /// </summary>
    public string TrainingType { get; set; }
    /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }
    /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime TrainingStartDate { get; set; }
    /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime TrainingEndDate { get; set; }
    /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }
    /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }
    /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }
    /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal TrainingScore { get; set; }
    /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }
    /// <summary>
    /// 培训评价
    /// </summary>
    public string TrainingEvaluation { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }
    /// <summary>
    /// 发展计划
    /// </summary>
    public string DevelopmentPlan { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训发展表查询DTO
/// </summary>
public partial class TaktTrainingDevelopmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 培训课程名称
    /// </summary>
    public string? CourseName { get; set; }
    /// <summary>
    /// 培训类型
    /// </summary>
    public string? TrainingType { get; set; }
    /// <summary>
    /// 培训讲师
    /// </summary>
    public string? Instructor { get; set; }
    /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime? TrainingStartDate { get; set; }

    /// <summary>
    /// 培训开始日期开始时间
    /// </summary>
    public DateTime? TrainingStartDateStart { get; set; }
    /// <summary>
    /// 培训开始日期结束时间
    /// </summary>
    public DateTime? TrainingStartDateEnd { get; set; }
    /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime? TrainingEndDate { get; set; }

    /// <summary>
    /// 培训结束日期开始时间
    /// </summary>
    public DateTime? TrainingEndDateStart { get; set; }
    /// <summary>
    /// 培训结束日期结束时间
    /// </summary>
    public DateTime? TrainingEndDateEnd { get; set; }
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
    /// 培训时长
    /// </summary>
    public decimal? TrainingHours { get; set; }
    /// <summary>
    /// 培训地点
    /// </summary>
    public string? TrainingLocation { get; set; }
    /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal? TrainingScore { get; set; }
    /// <summary>
    /// 是否通过
    /// </summary>
    public int? IsPassed { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
    /// <summary>
    /// 培训评价
    /// </summary>
    public string? TrainingEvaluation { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestions { get; set; }
    /// <summary>
    /// 发展计划
    /// </summary>
    public string? DevelopmentPlan { get; set; }
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
/// Takt创建培训发展表DTO
/// </summary>
public partial class TaktTrainingDevelopmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentCreateDto()
    {
        CourseName = string.Empty;
        TrainingType = string.Empty;
        Instructor = string.Empty;
        TrainingLocation = string.Empty;
        CertificateNo = string.Empty;
        TrainingEvaluation = string.Empty;
        ImprovementSuggestions = string.Empty;
        DevelopmentPlan = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 培训课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 培训类型
    /// </summary>
    public string TrainingType { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime TrainingStartDate { get; set; }

        /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime TrainingEndDate { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal TrainingScore { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 培训评价
    /// </summary>
    public string TrainingEvaluation { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 发展计划
    /// </summary>
    public string DevelopmentPlan { get; set; }

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
/// Takt更新培训发展表DTO
/// </summary>
public partial class TaktTrainingDevelopmentUpdateDto : TaktTrainingDevelopmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentUpdateDto()
    {
    }

        /// <summary>
    /// 培训发展表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingDevelopmentId { get; set; } = 0;
}

/// <summary>
/// 培训发展表状态DTO
/// </summary>
public partial class TaktTrainingDevelopmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentStatusDto()
    {
    }

        /// <summary>
    /// 培训发展表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingDevelopmentId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训发展表导入模板DTO
/// </summary>
public partial class TaktTrainingDevelopmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentTemplateDto()
    {
        CourseName = string.Empty;
        TrainingType = string.Empty;
        Instructor = string.Empty;
        TrainingLocation = string.Empty;
        CertificateNo = string.Empty;
        TrainingEvaluation = string.Empty;
        ImprovementSuggestions = string.Empty;
        DevelopmentPlan = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 培训课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 培训类型
    /// </summary>
    public string TrainingType { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime TrainingStartDate { get; set; }

        /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime TrainingEndDate { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal TrainingScore { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 培训评价
    /// </summary>
    public string TrainingEvaluation { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 发展计划
    /// </summary>
    public string DevelopmentPlan { get; set; }

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
/// 培训发展表导入DTO
/// </summary>
public partial class TaktTrainingDevelopmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentImportDto()
    {
        CourseName = string.Empty;
        TrainingType = string.Empty;
        Instructor = string.Empty;
        TrainingLocation = string.Empty;
        CertificateNo = string.Empty;
        TrainingEvaluation = string.Empty;
        ImprovementSuggestions = string.Empty;
        DevelopmentPlan = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 培训课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 培训类型
    /// </summary>
    public string TrainingType { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime TrainingStartDate { get; set; }

        /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime TrainingEndDate { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal TrainingScore { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 培训评价
    /// </summary>
    public string TrainingEvaluation { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 发展计划
    /// </summary>
    public string DevelopmentPlan { get; set; }

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
/// 培训发展表导出DTO
/// </summary>
public partial class TaktTrainingDevelopmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentExportDto()
    {
        CreatedAt = DateTime.Now;
        CourseName = string.Empty;
        TrainingType = string.Empty;
        Instructor = string.Empty;
        TrainingLocation = string.Empty;
        CertificateNo = string.Empty;
        TrainingEvaluation = string.Empty;
        ImprovementSuggestions = string.Empty;
        DevelopmentPlan = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 培训课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 培训类型
    /// </summary>
    public string TrainingType { get; set; }

        /// <summary>
    /// 培训讲师
    /// </summary>
    public string Instructor { get; set; }

        /// <summary>
    /// 培训开始日期
    /// </summary>
    public DateTime TrainingStartDate { get; set; }

        /// <summary>
    /// 培训结束日期
    /// </summary>
    public DateTime TrainingEndDate { get; set; }

        /// <summary>
    /// 培训日期
    /// </summary>
    public DateTime TrainingDate { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训地点
    /// </summary>
    public string TrainingLocation { get; set; }

        /// <summary>
    /// 培训成绩
    /// </summary>
    public decimal TrainingScore { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 培训评价
    /// </summary>
    public string TrainingEvaluation { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 发展计划
    /// </summary>
    public string DevelopmentPlan { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}