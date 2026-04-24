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
    Task<TaktPagedResult<TaktOnlineDto>> GetOnlineListAsync(TaktOnlineQueryDto queryDto);

    /// <summary>
    /// 根据ID获取在线用户
    /// </summary>
    Task<TaktOnlineDto?> GetOnlineByIdAsync(long id);

    /// <summary>
    /// 根据连接ID获取在线用户
    /// </summary>
    Task<TaktOnlineDto?> GetOnlineByConnectionIdAsync(string connectionId);

    /// <summary>
    /// 创建在线用户
    /// </summary>
    Task<TaktOnlineDto> CreateOnlineAsync(TaktOnlineCreateDto dto);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    Task DeleteOnlineByIdAsync(long id);

    /// <summary>
    /// 根据连接ID删除在线用户
    /// </summary>
    Task DeleteOnlineByConnectionIdAsync(string connectionId);

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    Task DeleteOnlineBatchAsync(List<long> ids);

    /// <summary>
    /// 更新最后活动时间
    /// </summary>
    Task UpdateOnlineLastActiveTimeAsync(string connectionId);

    /// <summary>
    /// 导出在线用户
    /// </summary>
    Task<(string fileName, byte[] content)> ExportOnlineAsync(TaktOnlineQueryDto query, string? sheetName, string? fileName);
}
