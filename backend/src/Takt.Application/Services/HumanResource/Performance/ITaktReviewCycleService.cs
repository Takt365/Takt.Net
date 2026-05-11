// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.HumanResource.Performance
// 文件名称：ITaktReviewCycleService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：评审周期表应用服务接口，定义ReviewCycle管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.Performance;

/// <summary>
/// 评审周期表应用服务接口
/// </summary>
public interface ITaktReviewCycleService
{
    /// <summary>
    /// 获取评审周期表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktReviewCycleDto>> GetReviewCycleListAsync(TaktReviewCycleQueryDto queryDto);

    /// <summary>
    /// 根据ID获取评审周期表
    /// </summary>
    /// <param name="id">评审周期表ID</param>
    /// <returns>评审周期表DTO</returns>
    Task<TaktReviewCycleDto?> GetReviewCycleByIdAsync(long id);

    /// <summary>
    /// 获取评审周期表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>评审周期表选项列表</returns>
    Task<List<TaktSelectOption>> GetReviewCycleOptionsAsync();

    /// <summary>
    /// 创建评审周期表
    /// </summary>
    /// <param name="dto">创建评审周期表DTO</param>
    /// <returns>评审周期表DTO</returns>
    Task<TaktReviewCycleDto> CreateReviewCycleAsync(TaktReviewCycleCreateDto dto);

    /// <summary>
    /// 更新评审周期表
    /// </summary>
    /// <param name="id">评审周期表ID</param>
    /// <param name="dto">更新评审周期表DTO</param>
    /// <returns>评审周期表DTO</returns>
    Task<TaktReviewCycleDto> UpdateReviewCycleAsync(long id, TaktReviewCycleUpdateDto dto);

    /// <summary>
    /// 删除评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="id">评审周期表(ReviewCycle)ID</param>
    /// <returns>任务</returns>
    Task DeleteReviewCycleByIdAsync(long id);

    /// <summary>
    /// 批量删除评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="ids">评审周期表(ReviewCycle)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteReviewCycleBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新评审周期表(ReviewCycle)Status
    /// </summary>
    /// <param name="dto">评审周期表(ReviewCycle)StatusDTO</param>
    /// <returns>评审周期表(ReviewCycle)DTO</returns>
    Task<TaktReviewCycleDto> UpdateReviewCycleStatusAsync(TaktReviewCycleStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetReviewCycleTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportReviewCycleAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出评审周期表(ReviewCycle)
    /// </summary>
    /// <param name="query">评审周期表(ReviewCycle)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportReviewCycleAsync(TaktReviewCycleQueryDto query, string? sheetName, string? fileName);
}

