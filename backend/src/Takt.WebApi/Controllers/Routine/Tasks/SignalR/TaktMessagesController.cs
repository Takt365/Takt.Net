// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.SignalR
// 文件名称：TaktMessagesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt在线消息控制器，提供在线消息管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.SignalR;

/// <summary>
/// 在线消息控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "在线消息")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:message", "在线消息管理")]
public class TaktMessagesController : TaktControllerBase
{
    private readonly ITaktMessageService _messageService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="messageService">消息服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktMessagesController(
        ITaktMessageService messageService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _messageService = messageService;
    }

    /// <summary>
    /// 获取消息列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:message:list", "查询消息列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMessageDto>>> GetMessageListAsync([FromQuery] TaktMessageQueryDto queryDto)
    {
        var result = await _messageService.GetMessageListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:message:query", "查询消息详情")]
    public async Task<ActionResult<TaktMessageDto>> GetMessageByIdAsync(long id)
    {
        var message = await _messageService.GetMessageByIdAsync(id);
        if (message == null)
            return NotFound();
        return Ok(message);
    }

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="dto">创建消息DTO</param>
    /// <returns>消息DTO</returns>
    [HttpPost]
    [TaktPermission("routine:tasks:message:create", "创建消息")]
    public async Task<ActionResult<TaktMessageDto>> CreateMessageAsync([FromBody] TaktMessageCreateDto dto)
    {
        try
        {
            var message = await _messageService.CreateMessageAsync(dto);
            return CreatedAtAction(nameof(GetMessageByIdAsync), new { id = message.MessageId }, message);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <param name="dto">更新消息DTO</param>
    /// <returns>消息DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:message:update", "更新消息")]
    public async Task<ActionResult<TaktMessageDto>> UpdateMessageAsync(long id, [FromBody] TaktMessageUpdateDto dto)
    {
        try
        {
            var message = await _messageService.UpdateMessageAsync(id, dto);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:message:delete", "删除消息")]
    public async Task<IActionResult> DeleteMessageByIdAsync(long id)
    {
        try
        {
            await _messageService.DeleteMessageByIdAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="dto">消息已读DTO</param>
    /// <returns>消息DTO</returns>
    [HttpPut("read")]
    [TaktPermission("routine:tasks:message:read", "标记消息已读")]
    public async Task<ActionResult<TaktMessageDto>> MarkMessageAsReadAsync([FromBody] TaktMessageReadDto dto)
    {
        try
        {
            var message = await _messageService.MarkMessageAsReadAsync(dto);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出消息
    /// </summary>
    /// <param name="query">消息查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:message:export", "导出消息")]
    public async Task<IActionResult> ExportMessageAsync([FromBody] TaktMessageQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _messageService.ExportMessageAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
