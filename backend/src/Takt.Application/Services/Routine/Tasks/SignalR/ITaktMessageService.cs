// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.SignalR
// 文件名称：ITaktMessageService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线消息应用服务接口，定义在线消息管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// Takt在线消息应用服务接口
/// </summary>
public interface ITaktMessageService
{
    /// <summary>
    /// 获取消息列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktMessageDto>> GetMessageListAsync(TaktMessageQueryDto queryDto);

    /// <summary>
    /// 根据ID获取消息
    /// </summary>
    Task<TaktMessageDto?> GetMessageByIdAsync(long id);

    /// <summary>
    /// 创建消息
    /// </summary>
    Task<TaktMessageDto> CreateMessageAsync(TaktMessageCreateDto dto);

    /// <summary>
    /// 更新消息
    /// </summary>
    Task<TaktMessageDto> UpdateMessageAsync(long id, TaktMessageUpdateDto dto);

    /// <summary>
    /// 删除消息
    /// </summary>
    Task DeleteMessageByIdAsync(long id);

    /// <summary>
    /// 批量删除消息
    /// </summary>
    Task DeleteMessageBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    Task<TaktMessageDto> MarkMessageAsReadAsync(TaktMessageReadDto dto);

    /// <summary>
    /// 导出消息
    /// </summary>
    Task<(string fileName, byte[] content)> ExportMessageAsync(TaktMessageQueryDto query, string? sheetName, string? fileName);
}
