// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.SignalR
// 文件名称：TaktMessagesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：在线消息表控制器，提供Message管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

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
/// 在线消息表控制器
/// </summary>
[Route("api/[controller]", Name = "在线消息表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:signalr:message", "在线消息表管理")]
public class TaktMessagesController : TaktControllerBase
{
    private readonly ITaktMessageService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMessagesController(
        ITaktMessageService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取在线消息表(Message)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:signalr:message:list", "查询在线消息表(Message)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMessageDto>>> GetMessageListAsync([FromQuery] TaktMessageQueryDto queryDto)
    {
        var result = await _service.GetMessageListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取在线消息表(Message)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:signalr:message:query", "查询在线消息表(Message)详情")]
    public async Task<ActionResult<TaktMessageDto>> GetMessageByIdAsync(long id)
    {
        var item = await _service.GetMessageByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取在线消息表(Message)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:signalr:message:query", "查询在线消息表(Message)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMessageOptionsAsync()
    {
        var result = await _service.GetMessageOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建在线消息表(Message)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:signalr:message:create", "创建在线消息表(Message)")]
    public async Task<ActionResult<TaktMessageDto>> CreateMessageAsync([FromBody] TaktMessageCreateDto dto)
    {
        var result = await _service.CreateMessageAsync(dto);
        return CreatedAtAction(nameof(GetMessageByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新在线消息表(Message)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:signalr:message:update", "更新在线消息表(Message)")]
    public async Task<ActionResult<TaktMessageDto>> UpdateMessageAsync(long id, [FromBody] TaktMessageUpdateDto dto)
    {
        var result = await _service.UpdateMessageAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除在线消息表(Message)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:signalr:message:delete", "删除在线消息表(Message)")]
    public async Task<ActionResult> DeleteMessageByIdAsync(long id)
    {
        await _service.DeleteMessageByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除在线消息表(Message)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:signalr:message:delete", "批量删除在线消息表(Message)")]
    public async Task<ActionResult> DeleteMessageBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMessageBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新在线消息表(Message)Read
    /// </summary>
    [HttpPut("status-read")]
    [TaktPermission("routine:tasks:signalr:message:update", "更新在线消息表(Message)Read")]
    public async Task<ActionResult<TaktMessageDto>> UpdateMessageReadStatusAsync([FromBody] TaktMessageReadStatusDto dto)
    {
        var result = await _service.UpdateMessageReadStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取在线消息表(Message)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:signalr:message:import", "获取在线消息表(Message)导入模板")]
    public async Task<IActionResult> GetMessageTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMessageTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入在线消息表(Message)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:signalr:message:import", "导入在线消息表(Message)")]
    public async Task<ActionResult<object>> ImportMessageAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
        }

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportMessageAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出在线消息表(Message)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:signalr:message:export", "导出在线消息表(Message)")]
    public async Task<IActionResult> ExportMessageAsync([FromBody] TaktMessageQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMessageAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
