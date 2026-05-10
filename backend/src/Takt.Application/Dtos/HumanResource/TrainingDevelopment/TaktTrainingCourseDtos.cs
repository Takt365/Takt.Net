// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCourseDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：培训课程表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训课程表Dto
/// </summary>
public partial class TaktTrainingCourseDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseDto()
    {
        CourseCode = string.Empty;
        CourseName = string.Empty;
        CourseType = string.Empty;
        CourseLevel = string.Empty;
        CourseDescription = string.Empty;
        CourseObjectives = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        MainInstructor = string.Empty;
        TrainingMethod = string.Empty;
        AssessmentMethod = string.Empty;
        CourseOutline = string.Empty;
        MaterialList = string.Empty;
    }

    /// <summary>
    /// 培训课程表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; } = 0;

    /// <summary>
    /// 课程编码
    /// </summary>
    public string CourseCode { get; set; }
    /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }
    /// <summary>
    /// 课程类型
    /// </summary>
    public string CourseType { get; set; }
    /// <summary>
    /// 课程级别
    /// </summary>
    public string CourseLevel { get; set; }
    /// <summary>
    /// 课程描述
    /// </summary>
    public string CourseDescription { get; set; }
    /// <summary>
    /// 课程目标
    /// </summary>
    public string CourseObjectives { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }
    /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }
    /// <summary>
    /// 培训天数
    /// </summary>
    public decimal TrainingDays { get; set; }
    /// <summary>
    /// 主讲讲师
    /// </summary>
    public string MainInstructor { get; set; }
    /// <summary>
    /// 培训方式
    /// </summary>
    public string TrainingMethod { get; set; }
    /// <summary>
    /// 考核方式
    /// </summary>
    public string AssessmentMethod { get; set; }
    /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal PassingScore { get; set; }
    /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int IsCertification { get; set; }
    /// <summary>
    /// 课程大纲
    /// </summary>
    public string CourseOutline { get; set; }
    /// <summary>
    /// 培训材料清单
    /// </summary>
    public string MaterialList { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训课程表查询DTO
/// </summary>
public partial class TaktTrainingCourseQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 课程编码
    /// </summary>
    public string? CourseCode { get; set; }
    /// <summary>
    /// 课程名称
    /// </summary>
    public string? CourseName { get; set; }
    /// <summary>
    /// 课程类型
    /// </summary>
    public string? CourseType { get; set; }
    /// <summary>
    /// 课程级别
    /// </summary>
    public string? CourseLevel { get; set; }
    /// <summary>
    /// 课程描述
    /// </summary>
    public string? CourseDescription { get; set; }
    /// <summary>
    /// 课程目标
    /// </summary>
    public string? CourseObjectives { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string? ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string? ApplicablePosition { get; set; }
    /// <summary>
    /// 培训时长
    /// </summary>
    public decimal? TrainingHours { get; set; }
    /// <summary>
    /// 培训天数
    /// </summary>
    public decimal? TrainingDays { get; set; }
    /// <summary>
    /// 主讲讲师
    /// </summary>
    public string? MainInstructor { get; set; }
    /// <summary>
    /// 培训方式
    /// </summary>
    public string? TrainingMethod { get; set; }
    /// <summary>
    /// 考核方式
    /// </summary>
    public string? AssessmentMethod { get; set; }
    /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal? PassingScore { get; set; }
    /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int? IsCertification { get; set; }
    /// <summary>
    /// 课程大纲
    /// </summary>
    public string? CourseOutline { get; set; }
    /// <summary>
    /// 培训材料清单
    /// </summary>
    public string? MaterialList { get; set; }
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
/// Takt创建培训课程表DTO
/// </summary>
public partial class TaktTrainingCourseCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseCreateDto()
    {
        CourseCode = string.Empty;
        CourseName = string.Empty;
        CourseType = string.Empty;
        CourseLevel = string.Empty;
        CourseDescription = string.Empty;
        CourseObjectives = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        MainInstructor = string.Empty;
        TrainingMethod = string.Empty;
        AssessmentMethod = string.Empty;
        CourseOutline = string.Empty;
        MaterialList = string.Empty;
    }

        /// <summary>
    /// 课程编码
    /// </summary>
    public string CourseCode { get; set; }

        /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 课程类型
    /// </summary>
    public string CourseType { get; set; }

        /// <summary>
    /// 课程级别
    /// </summary>
    public string CourseLevel { get; set; }

        /// <summary>
    /// 课程描述
    /// </summary>
    public string CourseDescription { get; set; }

        /// <summary>
    /// 课程目标
    /// </summary>
    public string CourseObjectives { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训天数
    /// </summary>
    public decimal TrainingDays { get; set; }

        /// <summary>
    /// 主讲讲师
    /// </summary>
    public string MainInstructor { get; set; }

        /// <summary>
    /// 培训方式
    /// </summary>
    public string TrainingMethod { get; set; }

        /// <summary>
    /// 考核方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal PassingScore { get; set; }

        /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int IsCertification { get; set; }

        /// <summary>
    /// 课程大纲
    /// </summary>
    public string CourseOutline { get; set; }

        /// <summary>
    /// 培训材料清单
    /// </summary>
    public string MaterialList { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新培训课程表DTO
/// </summary>
public partial class TaktTrainingCourseUpdateDto : TaktTrainingCourseCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseUpdateDto()
    {
    }

        /// <summary>
    /// 培训课程表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; } = 0;
}

/// <summary>
/// 培训课程表状态DTO
/// </summary>
public partial class TaktTrainingCourseStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseStatusDto()
    {
    }

        /// <summary>
    /// 培训课程表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训课程表排序DTO
/// </summary>
public partial class TaktTrainingCourseSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseSortDto()
    {
    }

        /// <summary>
    /// 培训课程表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingCourseId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 培训课程表导入模板DTO
/// </summary>
public partial class TaktTrainingCourseTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseTemplateDto()
    {
        CourseCode = string.Empty;
        CourseName = string.Empty;
        CourseType = string.Empty;
        CourseLevel = string.Empty;
        CourseDescription = string.Empty;
        CourseObjectives = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        MainInstructor = string.Empty;
        TrainingMethod = string.Empty;
        AssessmentMethod = string.Empty;
        CourseOutline = string.Empty;
        MaterialList = string.Empty;
    }

        /// <summary>
    /// 课程编码
    /// </summary>
    public string CourseCode { get; set; }

        /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 课程类型
    /// </summary>
    public string CourseType { get; set; }

        /// <summary>
    /// 课程级别
    /// </summary>
    public string CourseLevel { get; set; }

        /// <summary>
    /// 课程描述
    /// </summary>
    public string CourseDescription { get; set; }

        /// <summary>
    /// 课程目标
    /// </summary>
    public string CourseObjectives { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训天数
    /// </summary>
    public decimal TrainingDays { get; set; }

        /// <summary>
    /// 主讲讲师
    /// </summary>
    public string MainInstructor { get; set; }

        /// <summary>
    /// 培训方式
    /// </summary>
    public string TrainingMethod { get; set; }

        /// <summary>
    /// 考核方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal PassingScore { get; set; }

        /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int IsCertification { get; set; }

        /// <summary>
    /// 课程大纲
    /// </summary>
    public string CourseOutline { get; set; }

        /// <summary>
    /// 培训材料清单
    /// </summary>
    public string MaterialList { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 培训课程表导入DTO
/// </summary>
public partial class TaktTrainingCourseImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseImportDto()
    {
        CourseCode = string.Empty;
        CourseName = string.Empty;
        CourseType = string.Empty;
        CourseLevel = string.Empty;
        CourseDescription = string.Empty;
        CourseObjectives = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        MainInstructor = string.Empty;
        TrainingMethod = string.Empty;
        AssessmentMethod = string.Empty;
        CourseOutline = string.Empty;
        MaterialList = string.Empty;
    }

        /// <summary>
    /// 课程编码
    /// </summary>
    public string CourseCode { get; set; }

        /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 课程类型
    /// </summary>
    public string CourseType { get; set; }

        /// <summary>
    /// 课程级别
    /// </summary>
    public string CourseLevel { get; set; }

        /// <summary>
    /// 课程描述
    /// </summary>
    public string CourseDescription { get; set; }

        /// <summary>
    /// 课程目标
    /// </summary>
    public string CourseObjectives { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训天数
    /// </summary>
    public decimal TrainingDays { get; set; }

        /// <summary>
    /// 主讲讲师
    /// </summary>
    public string MainInstructor { get; set; }

        /// <summary>
    /// 培训方式
    /// </summary>
    public string TrainingMethod { get; set; }

        /// <summary>
    /// 考核方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal PassingScore { get; set; }

        /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int IsCertification { get; set; }

        /// <summary>
    /// 课程大纲
    /// </summary>
    public string CourseOutline { get; set; }

        /// <summary>
    /// 培训材料清单
    /// </summary>
    public string MaterialList { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 培训课程表导出DTO
/// </summary>
public partial class TaktTrainingCourseExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCourseExportDto()
    {
        CreatedAt = DateTime.Now;
        CourseCode = string.Empty;
        CourseName = string.Empty;
        CourseType = string.Empty;
        CourseLevel = string.Empty;
        CourseDescription = string.Empty;
        CourseObjectives = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        MainInstructor = string.Empty;
        TrainingMethod = string.Empty;
        AssessmentMethod = string.Empty;
        CourseOutline = string.Empty;
        MaterialList = string.Empty;
    }

        /// <summary>
    /// 课程编码
    /// </summary>
    public string CourseCode { get; set; }

        /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

        /// <summary>
    /// 课程类型
    /// </summary>
    public string CourseType { get; set; }

        /// <summary>
    /// 课程级别
    /// </summary>
    public string CourseLevel { get; set; }

        /// <summary>
    /// 课程描述
    /// </summary>
    public string CourseDescription { get; set; }

        /// <summary>
    /// 课程目标
    /// </summary>
    public string CourseObjectives { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 培训时长
    /// </summary>
    public decimal TrainingHours { get; set; }

        /// <summary>
    /// 培训天数
    /// </summary>
    public decimal TrainingDays { get; set; }

        /// <summary>
    /// 主讲讲师
    /// </summary>
    public string MainInstructor { get; set; }

        /// <summary>
    /// 培训方式
    /// </summary>
    public string TrainingMethod { get; set; }

        /// <summary>
    /// 考核方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 及格分数线
    /// </summary>
    public decimal PassingScore { get; set; }

        /// <summary>
    /// 是否颁发证书
    /// </summary>
    public int IsCertification { get; set; }

        /// <summary>
    /// 课程大纲
    /// </summary>
    public string CourseOutline { get; set; }

        /// <summary>
    /// 培训材料清单
    /// </summary>
    public string MaterialList { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}