// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.SignalR
// 文件名称：ITaktOnlineService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线用户应用服务接口，定义在线用户管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线用户应用服务接口
/// </summary>
public interface ITaktOnlineService
{
    /// <summary>
    /// 获取在线用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktOnlineDto>> GetListAsync(TaktOnlineQueryDto queryDto);

    /// <summary>
    /// 根据ID获取在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>在线用户DTO</returns>
    Task<TaktOnlineDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据连接ID获取在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>在线用户DTO</returns>
    Task<TaktOnlineDto?> GetByConnectionIdAsync(string connectionId);

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="dto">创建在线用户DTO</param>
    /// <returns>在线用户DTO</returns>
    Task<TaktOnlineDto> CreateAsync(TaktOnlineCreateDto dto);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 根据连接ID删除在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    Task DeleteByConnectionIdAsync(string connectionId);

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>任务</returns>
    Task DeleteBatchAsync(List<long> ids);

    /// <summary>
    /// 更新最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>任务</returns>
    Task UpdateLastActiveTimeAsync(string connectionId);

    /// <summary>
    /// 导出在线用户
    /// </summary>
    /// <param name="query">在线用户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktOnlineQueryDto query, string? sheetName, string? fileName);
}
