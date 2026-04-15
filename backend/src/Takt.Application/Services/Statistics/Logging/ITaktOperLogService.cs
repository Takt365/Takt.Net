// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Logging
// 文件名称：ITaktOperLogService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志应用服务接口，定义操作日志管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Logging;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Logging;

/// <summary>
/// Takt操作日志应用服务接口
/// </summary>
public interface ITaktOperLogService
{
    /// <summary>
    /// 获取操作日志列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktOperLogDto>> GetListAsync(TaktOperLogQueryDto queryDto);

    /// <summary>
    /// 创建操作日志
    /// </summary>
    /// <param name="dto">创建操作日志DTO</param>
    /// <returns>操作日志DTO</returns>
    Task<TaktOperLogDto> CreateAsync(TaktCreateOperLogDto dto);

    /// <summary>
    /// 根据ID获取操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>操作日志DTO</returns>
    Task<TaktOperLogDto?> GetByIdAsync(long id);

    /// <summary>
    /// 删除操作日志
    /// </summary>
    /// <param name="id">日志ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除操作日志
    /// </summary>
    /// <param name="ids">日志ID列表</param>
    /// <returns>任务</returns>
    Task DeleteBatchAsync(List<long> ids);

    /// <summary>
    /// 导出操作日志
    /// </summary>
    /// <param name="query">操作日志查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktOperLogQueryDto query, string? sheetName, string? fileName);
}
