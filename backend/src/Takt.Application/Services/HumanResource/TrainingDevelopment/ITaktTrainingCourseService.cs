// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：ITaktTrainingCourseService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：培训课程表应用服务接口，定义TrainingCourse管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训课程表应用服务接口
/// </summary>
public interface ITaktTrainingCourseService
{
    /// <summary>
    /// 获取培训课程表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTrainingCourseDto>> GetTrainingCourseListAsync(TaktTrainingCourseQueryDto queryDto);

    /// <summary>
    /// 根据ID获取培训课程表
    /// </summary>
    /// <param name="id">培训课程表ID</param>
    /// <returns>培训课程表DTO</returns>
    Task<TaktTrainingCourseDto?> GetTrainingCourseByIdAsync(long id);

    /// <summary>
    /// 获取培训课程表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>培训课程表选项列表</returns>
    Task<List<TaktSelectOption>> GetTrainingCourseOptionsAsync();

    /// <summary>
    /// 创建培训课程表
    /// </summary>
    /// <param name="dto">创建培训课程表DTO</param>
    /// <returns>培训课程表DTO</returns>
    Task<TaktTrainingCourseDto> CreateTrainingCourseAsync(TaktTrainingCourseCreateDto dto);

    /// <summary>
    /// 更新培训课程表
    /// </summary>
    /// <param name="id">培训课程表ID</param>
    /// <param name="dto">更新培训课程表DTO</param>
    /// <returns>培训课程表DTO</returns>
    Task<TaktTrainingCourseDto> UpdateTrainingCourseAsync(long id, TaktTrainingCourseUpdateDto dto);

    /// <summary>
    /// 删除培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="id">培训课程表(TrainingCourse)ID</param>
    /// <returns>任务</returns>
    Task DeleteTrainingCourseByIdAsync(long id);

    /// <summary>
    /// 批量删除培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="ids">培训课程表(TrainingCourse)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTrainingCourseBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新培训课程表(TrainingCourse)Status
    /// </summary>
    /// <param name="dto">培训课程表(TrainingCourse)StatusDTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    Task<TaktTrainingCourseDto> UpdateTrainingCourseStatusAsync(TaktTrainingCourseStatusDto dto);

    /// <summary>
    /// 更新培训课程表(TrainingCourse)排序
    /// </summary>
    /// <param name="dto">培训课程表(TrainingCourse)排序DTO</param>
    /// <returns>培训课程表(TrainingCourse)DTO</returns>
    Task<TaktTrainingCourseDto> UpdateTrainingCourseSortAsync(TaktTrainingCourseSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTrainingCourseTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTrainingCourseAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出培训课程表(TrainingCourse)
    /// </summary>
    /// <param name="query">培训课程表(TrainingCourse)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTrainingCourseAsync(TaktTrainingCourseQueryDto query, string? sheetName, string? fileName);
}

