// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：ITaktTrainingPlanService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：培训计划表应用服务接口，定义TrainingPlan管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训计划表应用服务接口
/// </summary>
public interface ITaktTrainingPlanService
{
    /// <summary>
    /// 获取培训计划表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTrainingPlanDto>> GetTrainingPlanListAsync(TaktTrainingPlanQueryDto queryDto);

    /// <summary>
    /// 根据ID获取培训计划表
    /// </summary>
    /// <param name="id">培训计划表ID</param>
    /// <returns>培训计划表DTO</returns>
    Task<TaktTrainingPlanDto?> GetTrainingPlanByIdAsync(long id);

    /// <summary>
    /// 获取培训计划表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>培训计划表选项列表</returns>
    Task<List<TaktSelectOption>> GetTrainingPlanOptionsAsync();

    /// <summary>
    /// 创建培训计划表
    /// </summary>
    /// <param name="dto">创建培训计划表DTO</param>
    /// <returns>培训计划表DTO</returns>
    Task<TaktTrainingPlanDto> CreateTrainingPlanAsync(TaktTrainingPlanCreateDto dto);

    /// <summary>
    /// 更新培训计划表
    /// </summary>
    /// <param name="id">培训计划表ID</param>
    /// <param name="dto">更新培训计划表DTO</param>
    /// <returns>培训计划表DTO</returns>
    Task<TaktTrainingPlanDto> UpdateTrainingPlanAsync(long id, TaktTrainingPlanUpdateDto dto);

    /// <summary>
    /// 删除培训计划表(TrainingPlan)
    /// </summary>
    /// <param name="id">培训计划表(TrainingPlan)ID</param>
    /// <returns>任务</returns>
    Task DeleteTrainingPlanByIdAsync(long id);

    /// <summary>
    /// 批量删除培训计划表(TrainingPlan)
    /// </summary>
    /// <param name="ids">培训计划表(TrainingPlan)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTrainingPlanBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新培训计划表(TrainingPlan)Status
    /// </summary>
    /// <param name="dto">培训计划表(TrainingPlan)StatusDTO</param>
    /// <returns>培训计划表(TrainingPlan)DTO</returns>
    Task<TaktTrainingPlanDto> UpdateTrainingPlanStatusAsync(TaktTrainingPlanStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTrainingPlanTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入培训计划表(TrainingPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTrainingPlanAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出培训计划表(TrainingPlan)
    /// </summary>
    /// <param name="query">培训计划表(TrainingPlan)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTrainingPlanAsync(TaktTrainingPlanQueryDto query, string? sheetName, string? fileName);
}

