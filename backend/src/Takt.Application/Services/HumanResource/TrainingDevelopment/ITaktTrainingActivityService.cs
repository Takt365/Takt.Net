// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.TrainingDevelopment
// 文件名称：ITaktTrainingActivityService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：培训活动表应用服务接口，定义TrainingActivity管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训活动表应用服务接口
/// </summary>
public interface ITaktTrainingActivityService
{
    /// <summary>
    /// 获取培训活动表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktTrainingActivityDto>> GetTrainingActivityListAsync(TaktTrainingActivityQueryDto queryDto);

    /// <summary>
    /// 根据ID获取培训活动表
    /// </summary>
    /// <param name="id">培训活动表ID</param>
    /// <returns>培训活动表DTO</returns>
    Task<TaktTrainingActivityDto?> GetTrainingActivityByIdAsync(long id);

    /// <summary>
    /// 获取培训活动表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>培训活动表选项列表</returns>
    Task<List<TaktSelectOption>> GetTrainingActivityOptionsAsync();

    /// <summary>
    /// 创建培训活动表
    /// </summary>
    /// <param name="dto">创建培训活动表DTO</param>
    /// <returns>培训活动表DTO</returns>
    Task<TaktTrainingActivityDto> CreateTrainingActivityAsync(TaktTrainingActivityCreateDto dto);

    /// <summary>
    /// 更新培训活动表
    /// </summary>
    /// <param name="id">培训活动表ID</param>
    /// <param name="dto">更新培训活动表DTO</param>
    /// <returns>培训活动表DTO</returns>
    Task<TaktTrainingActivityDto> UpdateTrainingActivityAsync(long id, TaktTrainingActivityUpdateDto dto);

    /// <summary>
    /// 删除培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="id">培训活动表(TrainingActivity)ID</param>
    /// <returns>任务</returns>
    Task DeleteTrainingActivityByIdAsync(long id);

    /// <summary>
    /// 批量删除培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="ids">培训活动表(TrainingActivity)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteTrainingActivityBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新培训活动表(TrainingActivity)Status
    /// </summary>
    /// <param name="dto">培训活动表(TrainingActivity)StatusDTO</param>
    /// <returns>培训活动表(TrainingActivity)DTO</returns>
    Task<TaktTrainingActivityDto> UpdateTrainingActivityStatusAsync(TaktTrainingActivityStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTrainingActivityTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportTrainingActivityAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出培训活动表(TrainingActivity)
    /// </summary>
    /// <param name="query">培训活动表(TrainingActivity)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportTrainingActivityAsync(TaktTrainingActivityQueryDto query, string? sheetName, string? fileName);
}

