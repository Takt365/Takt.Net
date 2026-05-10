// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：ITaktQuartzLogService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：任务日志表应用服务接口，定义QuartzLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// 任务日志表应用服务接口
/// </summary>
public interface ITaktQuartzLogService
{
    /// <summary>
    /// 获取任务日志表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktQuartzLogDto>> GetQuartzLogListAsync(TaktQuartzLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取任务日志表
    /// </summary>
    /// <param name="id">任务日志表ID</param>
    /// <returns>任务日志表DTO</returns>
    Task<TaktQuartzLogDto?> GetQuartzLogByIdAsync(long id);

    /// <summary>
    /// 获取任务日志表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>任务日志表选项列表</returns>
    Task<List<TaktSelectOption>> GetQuartzLogOptionsAsync();

    /// <summary>
    /// 创建任务日志表
    /// </summary>
    /// <param name="dto">创建任务日志表DTO</param>
    /// <returns>任务日志表DTO</returns>
    Task<TaktQuartzLogDto> CreateQuartzLogAsync(TaktQuartzLogCreateDto dto);

    /// <summary>
    /// 更新任务日志表
    /// </summary>
    /// <param name="id">任务日志表ID</param>
    /// <param name="dto">更新任务日志表DTO</param>
    /// <returns>任务日志表DTO</returns>
    Task<TaktQuartzLogDto> UpdateQuartzLogAsync(long id, TaktQuartzLogUpdateDto dto);

    /// <summary>
    /// 删除任务日志表(QuartzLog)
    /// </summary>
    /// <param name="id">任务日志表(QuartzLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteQuartzLogByIdAsync(long id);

    /// <summary>
    /// 批量删除任务日志表(QuartzLog)
    /// </summary>
    /// <param name="ids">任务日志表(QuartzLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteQuartzLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新任务日志表(QuartzLog)ExecuteStatus
    /// </summary>
    /// <param name="dto">任务日志表(QuartzLog)ExecuteStatusDTO</param>
    /// <returns>任务日志表(QuartzLog)DTO</returns>
    Task<TaktQuartzLogDto> UpdateQuartzLogExecuteStatusAsync(TaktQuartzLogExecuteStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetQuartzLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入任务日志表(QuartzLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportQuartzLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出任务日志表(QuartzLog)
    /// </summary>
    /// <param name="query">任务日志表(QuartzLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportQuartzLogAsync(TaktQuartzLogQueryDto query, string? sheetName, string? fileName);
}

