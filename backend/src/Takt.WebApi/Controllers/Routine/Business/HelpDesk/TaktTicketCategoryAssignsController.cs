// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktTicketCategoryAssignsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工单分类默认处理人表控制器，提供TicketCategoryAssign管理的RESTful API接口
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
/// 工单分类默认处理人表控制器
/// </summary>
[Route("api/[controller]", Name = "工单分类默认处理人表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:ticketcategoryassign", "工单分类默认处理人表管理")]
public class TaktTicketCategoryAssignsController : TaktControllerBase
{
    private readonly ITaktTicketCategoryAssignService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketCategoryAssignsController(
        ITaktTicketCategoryAssignService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工单分类默认处理人表(TicketCategoryAssign)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:list", "查询工单分类默认处理人表(TicketCategoryAssign)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTicketCategoryAssignDto>>> GetTicketCategoryAssignListAsync([FromQuery] TaktTicketCategoryAssignQueryDto queryDto)
    {
        var result = await _service.GetTicketCategoryAssignListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:query", "查询工单分类默认处理人表(TicketCategoryAssign)详情")]
    public async Task<ActionResult<TaktTicketCategoryAssignDto>> GetTicketCategoryAssignByIdAsync(long id)
    {
        var item = await _service.GetTicketCategoryAssignByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工单分类默认处理人表(TicketCategoryAssign)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:query", "查询工单分类默认处理人表(TicketCategoryAssign)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTicketCategoryAssignOptionsAsync()
    {
        var result = await _service.GetTicketCategoryAssignOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:create", "创建工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<ActionResult<TaktTicketCategoryAssignDto>> CreateTicketCategoryAssignAsync([FromBody] TaktTicketCategoryAssignCreateDto dto)
    {
        var result = await _service.CreateTicketCategoryAssignAsync(dto);
        return CreatedAtAction(nameof(GetTicketCategoryAssignByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:update", "更新工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<ActionResult<TaktTicketCategoryAssignDto>> UpdateTicketCategoryAssignAsync(long id, [FromBody] TaktTicketCategoryAssignUpdateDto dto)
    {
        var result = await _service.UpdateTicketCategoryAssignAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:delete", "删除工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<ActionResult> DeleteTicketCategoryAssignByIdAsync(long id)
    {
        await _service.DeleteTicketCategoryAssignByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:delete", "批量删除工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<ActionResult> DeleteTicketCategoryAssignBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTicketCategoryAssignBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工单分类默认处理人表(TicketCategoryAssign)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:update", "更新工单分类默认处理人表(TicketCategoryAssign)排序")]
    public async Task<ActionResult<TaktTicketCategoryAssignDto>> UpdateTicketCategoryAssignSortAsync([FromBody] TaktTicketCategoryAssignSortDto dto)
    {
        var result = await _service.UpdateTicketCategoryAssignSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工单分类默认处理人表(TicketCategoryAssign)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:import", "获取工单分类默认处理人表(TicketCategoryAssign)导入模板")]
    public async Task<IActionResult> GetTicketCategoryAssignTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTicketCategoryAssignTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:import", "导入工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<ActionResult<object>> ImportTicketCategoryAssignAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTicketCategoryAssignAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工单分类默认处理人表(TicketCategoryAssign)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:ticketcategoryassign:export", "导出工单分类默认处理人表(TicketCategoryAssign)")]
    public async Task<IActionResult> ExportTicketCategoryAssignAsync([FromBody] TaktTicketCategoryAssignQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTicketCategoryAssignAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
