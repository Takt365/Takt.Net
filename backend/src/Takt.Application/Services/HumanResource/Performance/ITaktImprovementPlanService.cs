// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：ITaktImprovementPlanService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：改进计划表应用服务接口，定义ImprovementPlan管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 改进计划表应用服务接口
/// </summary>
public interface ITaktImprovementPlanService
{
    /// <summary>
    /// 获取改进计划表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktImprovementPlanDto>> GetImprovementPlanListAsync(TaktImprovementPlanQueryDto queryDto);

    /// <summary>
    /// 根据ID获取改进计划表
    /// </summary>
    /// <param name="id">改进计划表ID</param>
    /// <returns>改进计划表DTO</returns>
    Task<TaktImprovementPlanDto?> GetImprovementPlanByIdAsync(long id);

    /// <summary>
    /// 获取改进计划表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>改进计划表选项列表</returns>
    Task<List<TaktSelectOption>> GetImprovementPlanOptionsAsync();

    /// <summary>
    /// 创建改进计划表
    /// </summary>
    /// <param name="dto">创建改进计划表DTO</param>
    /// <returns>改进计划表DTO</returns>
    Task<TaktImprovementPlanDto> CreateImprovementPlanAsync(TaktImprovementPlanCreateDto dto);

    /// <summary>
    /// 更新改进计划表
    /// </summary>
    /// <param name="id">改进计划表ID</param>
    /// <param name="dto">更新改进计划表DTO</param>
    /// <returns>改进计划表DTO</returns>
    Task<TaktImprovementPlanDto> UpdateImprovementPlanAsync(long id, TaktImprovementPlanUpdateDto dto);

    /// <summary>
    /// 删除改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="id">改进计划表(ImprovementPlan)ID</param>
    /// <returns>任务</returns>
    Task DeleteImprovementPlanByIdAsync(long id);

    /// <summary>
    /// 批量删除改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="ids">改进计划表(ImprovementPlan)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteImprovementPlanBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新改进计划表(ImprovementPlan)Status
    /// </summary>
    /// <param name="dto">改进计划表(ImprovementPlan)StatusDTO</param>
    /// <returns>改进计划表(ImprovementPlan)DTO</returns>
    Task<TaktImprovementPlanDto> UpdateImprovementPlanStatusAsync(TaktImprovementPlanStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetImprovementPlanTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportImprovementPlanAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出改进计划表(ImprovementPlan)
    /// </summary>
    /// <param name="query">改进计划表(ImprovementPlan)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportImprovementPlanAsync(TaktImprovementPlanQueryDto query, string? sheetName, string? fileName);
}

