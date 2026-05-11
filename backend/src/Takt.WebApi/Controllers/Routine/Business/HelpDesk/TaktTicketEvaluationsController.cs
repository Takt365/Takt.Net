// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktTicketEvaluationsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工单服务评价表控制器，提供TicketEvaluation管理的RESTful API接口
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
/// 工单服务评价表控制器
/// </summary>
[Route("api/[controller]", Name = "工单服务评价表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:ticketevaluation", "工单服务评价表管理")]
public class TaktTicketEvaluationsController : TaktControllerBase
{
    private readonly ITaktTicketEvaluationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketEvaluationsController(
        ITaktTicketEvaluationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工单服务评价表(TicketEvaluation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:list", "查询工单服务评价表(TicketEvaluation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTicketEvaluationDto>>> GetTicketEvaluationListAsync([FromQuery] TaktTicketEvaluationQueryDto queryDto)
    {
        var result = await _service.GetTicketEvaluationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:query", "查询工单服务评价表(TicketEvaluation)详情")]
    public async Task<ActionResult<TaktTicketEvaluationDto>> GetTicketEvaluationByIdAsync(long id)
    {
        var item = await _service.GetTicketEvaluationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工单服务评价表(TicketEvaluation)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:query", "查询工单服务评价表(TicketEvaluation)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTicketEvaluationOptionsAsync()
    {
        var result = await _service.GetTicketEvaluationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:create", "创建工单服务评价表(TicketEvaluation)")]
    public async Task<ActionResult<TaktTicketEvaluationDto>> CreateTicketEvaluationAsync([FromBody] TaktTicketEvaluationCreateDto dto)
    {
        var result = await _service.CreateTicketEvaluationAsync(dto);
        return CreatedAtAction(nameof(GetTicketEvaluationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:update", "更新工单服务评价表(TicketEvaluation)")]
    public async Task<ActionResult<TaktTicketEvaluationDto>> UpdateTicketEvaluationAsync(long id, [FromBody] TaktTicketEvaluationUpdateDto dto)
    {
        var result = await _service.UpdateTicketEvaluationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:delete", "删除工单服务评价表(TicketEvaluation)")]
    public async Task<ActionResult> DeleteTicketEvaluationByIdAsync(long id)
    {
        await _service.DeleteTicketEvaluationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:delete", "批量删除工单服务评价表(TicketEvaluation)")]
    public async Task<ActionResult> DeleteTicketEvaluationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTicketEvaluationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取工单服务评价表(TicketEvaluation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:import", "获取工单服务评价表(TicketEvaluation)导入模板")]
    public async Task<IActionResult> GetTicketEvaluationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTicketEvaluationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:import", "导入工单服务评价表(TicketEvaluation)")]
    public async Task<ActionResult<object>> ImportTicketEvaluationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTicketEvaluationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工单服务评价表(TicketEvaluation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:ticketevaluation:export", "导出工单服务评价表(TicketEvaluation)")]
    public async Task<IActionResult> ExportTicketEvaluationAsync([FromBody] TaktTicketEvaluationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTicketEvaluationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
