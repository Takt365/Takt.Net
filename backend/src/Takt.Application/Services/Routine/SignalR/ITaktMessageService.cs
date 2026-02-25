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

using Takt.Application.Dtos.Routine.SignalR;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.SignalR;

/// <summary>
/// Takt在线消息应用服务接口
/// </summary>
public interface ITaktMessageService
{
    /// <summary>
    /// 获取消息列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMessageDto>> GetListAsync(TaktMessageQueryDto queryDto);

    /// <summary>
    /// 根据ID获取消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息DTO</returns>
    Task<TaktMessageDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="dto">创建消息DTO</param>
    /// <returns>消息DTO</returns>
    Task<TaktMessageDto> CreateAsync(TaktMessageCreateDto dto);

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <param name="dto">更新消息DTO</param>
    /// <returns>消息DTO</returns>
    Task<TaktMessageDto> UpdateAsync(long id, TaktMessageUpdateDto dto);

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除消息
    /// </summary>
    /// <param name="ids">消息ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="dto">消息已读DTO</param>
    /// <returns>消息DTO</returns>
    Task<TaktMessageDto> MarkAsReadAsync(TaktMessageReadDto dto);

    /// <summary>
    /// 导出消息
    /// </summary>
    /// <param name="query">消息查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktMessageQueryDto query, string? sheetName, string? fileName);
}
