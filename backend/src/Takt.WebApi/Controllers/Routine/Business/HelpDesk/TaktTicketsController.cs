// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktTicketsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工单表控制器，提供Ticket管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Application.Services.Routine.Business.HelpDesk;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.HelpDesk;

/// <summary>
/// 工单表控制器
/// </summary>
[Route("api/[controller]", Name = "工单表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:ticket", "工单表管理")]
public class TaktTicketsController : TaktControllerBase
{
    private readonly ITaktTicketService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketsController(
        ITaktTicketService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工单表(Ticket)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:ticket:list", "查询工单表(Ticket)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTicketDto>>> GetTicketListAsync([FromQuery] TaktTicketQueryDto queryDto)
    {
        var result = await _service.GetTicketListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工单表(Ticket)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:ticket:query", "查询工单表(Ticket)详情")]
    public async Task<ActionResult<TaktTicketDto>> GetTicketByIdAsync(long id)
    {
        var item = await _service.GetTicketByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工单表(Ticket)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:ticket:query", "查询工单表(Ticket)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTicketOptionsAsync()
    {
        var result = await _service.GetTicketOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工单表(Ticket)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:ticket:create", "创建工单表(Ticket)")]
    public async Task<ActionResult<TaktTicketDto>> CreateTicketAsync([FromBody] TaktTicketCreateDto dto)
    {
        var result = await _service.CreateTicketAsync(dto);
        return CreatedAtAction(nameof(GetTicketByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工单表(Ticket)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:ticket:update", "更新工单表(Ticket)")]
    public async Task<ActionResult<TaktTicketDto>> UpdateTicketAsync(long id, [FromBody] TaktTicketUpdateDto dto)
    {
        var result = await _service.UpdateTicketAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工单表(Ticket)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:ticket:delete", "删除工单表(Ticket)")]
    public async Task<ActionResult> DeleteTicketByIdAsync(long id)
    {
        await _service.DeleteTicketByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工单表(Ticket)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:ticket:delete", "批量删除工单表(Ticket)")]
    public async Task<ActionResult> DeleteTicketBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTicketBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工单表(Ticket)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:helpdesk:ticket:update", "更新工单表(Ticket)状态")]
    public async Task<ActionResult<TaktTicketDto>> UpdateTicketStatusAsync([FromBody] TaktTicketStatusDto dto)
    {
        var result = await _service.UpdateTicketStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工单表(Ticket)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:ticket:import", "获取工单表(Ticket)导入模板")]
    public async Task<IActionResult> GetTicketTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTicketTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工单表(Ticket)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:ticket:import", "导入工单表(Ticket)")]
    public async Task<ActionResult<object>> ImportTicketAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTicketAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工单表(Ticket)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:ticket:export", "导出工单表(Ticket)")]
    public async Task<IActionResult> ExportTicketAsync([FromBody] TaktTicketQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTicketAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
