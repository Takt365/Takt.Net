// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktTicketChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工单变更日志表控制器，提供TicketChangeLog管理的RESTful API接口
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
/// 工单变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "工单变更日志表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:ticketchangelog", "工单变更日志表管理")]
public class TaktTicketChangeLogsController : TaktControllerBase
{
    private readonly ITaktTicketChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketChangeLogsController(
        ITaktTicketChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工单变更日志表(TicketChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:list", "查询工单变更日志表(TicketChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTicketChangeLogDto>>> GetTicketChangeLogListAsync([FromQuery] TaktTicketChangeLogQueryDto queryDto)
    {
        var result = await _service.GetTicketChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:query", "查询工单变更日志表(TicketChangeLog)详情")]
    public async Task<ActionResult<TaktTicketChangeLogDto>> GetTicketChangeLogByIdAsync(long id)
    {
        var item = await _service.GetTicketChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工单变更日志表(TicketChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:query", "查询工单变更日志表(TicketChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTicketChangeLogOptionsAsync()
    {
        var result = await _service.GetTicketChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:create", "创建工单变更日志表(TicketChangeLog)")]
    public async Task<ActionResult<TaktTicketChangeLogDto>> CreateTicketChangeLogAsync([FromBody] TaktTicketChangeLogCreateDto dto)
    {
        var result = await _service.CreateTicketChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetTicketChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:update", "更新工单变更日志表(TicketChangeLog)")]
    public async Task<ActionResult<TaktTicketChangeLogDto>> UpdateTicketChangeLogAsync(long id, [FromBody] TaktTicketChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateTicketChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:delete", "删除工单变更日志表(TicketChangeLog)")]
    public async Task<ActionResult> DeleteTicketChangeLogByIdAsync(long id)
    {
        await _service.DeleteTicketChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:delete", "批量删除工单变更日志表(TicketChangeLog)")]
    public async Task<ActionResult> DeleteTicketChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTicketChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取工单变更日志表(TicketChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:import", "获取工单变更日志表(TicketChangeLog)导入模板")]
    public async Task<IActionResult> GetTicketChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTicketChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:import", "导入工单变更日志表(TicketChangeLog)")]
    public async Task<ActionResult<object>> ImportTicketChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTicketChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工单变更日志表(TicketChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:ticketchangelog:export", "导出工单变更日志表(TicketChangeLog)")]
    public async Task<IActionResult> ExportTicketChangeLogAsync([FromBody] TaktTicketChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTicketChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
