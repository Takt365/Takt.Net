// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.SignalR
// 文件名称：ITaktMessageService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：在线消息表应用服务接口，定义Message管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Tasks.SignalR;

/// <summary>
/// 在线消息表应用服务接口
/// </summary>
public interface ITaktMessageService
{
    /// <summary>
    /// 获取在线消息表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktMessageDto>> GetMessageListAsync(TaktMessageQueryDto queryDto);

    /// <summary>
    /// 根据ID获取在线消息表
    /// </summary>
    /// <param name="id">在线消息表ID</param>
    /// <returns>在线消息表DTO</returns>
    Task<TaktMessageDto?> GetMessageByIdAsync(long id);

    /// <summary>
    /// 获取在线消息表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>在线消息表选项列表</returns>
    Task<List<TaktSelectOption>> GetMessageOptionsAsync();

    /// <summary>
    /// 创建在线消息表
    /// </summary>
    /// <param name="dto">创建在线消息表DTO</param>
    /// <returns>在线消息表DTO</returns>
    Task<TaktMessageDto> CreateMessageAsync(TaktMessageCreateDto dto);

    /// <summary>
    /// 更新在线消息表
    /// </summary>
    /// <param name="id">在线消息表ID</param>
    /// <param name="dto">更新在线消息表DTO</param>
    /// <returns>在线消息表DTO</returns>
    Task<TaktMessageDto> UpdateMessageAsync(long id, TaktMessageUpdateDto dto);

    /// <summary>
    /// 删除在线消息表(Message)
    /// </summary>
    /// <param name="id">在线消息表(Message)ID</param>
    /// <returns>任务</returns>
    Task DeleteMessageByIdAsync(long id);

    /// <summary>
    /// 批量删除在线消息表(Message)
    /// </summary>
    /// <param name="ids">在线消息表(Message)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteMessageBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新在线消息表(Message)ReadStatus
    /// </summary>
    /// <param name="dto">在线消息表(Message)ReadStatusDTO</param>
    /// <returns>在线消息表(Message)DTO</returns>
    Task<TaktMessageDto> UpdateMessageReadStatusAsync(TaktMessageReadStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetMessageTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入在线消息表(Message)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportMessageAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出在线消息表(Message)
    /// </summary>
    /// <param name="query">在线消息表(Message)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportMessageAsync(TaktMessageQueryDto query, string? sheetName, string? fileName);
}

