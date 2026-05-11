// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowExecutionsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程执行记录表控制器，提供FlowExecution管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 流程执行记录表控制器
/// </summary>
[Route("api/[controller]", Name = "流程执行记录表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowexecution", "流程执行记录表管理")]
public class TaktFlowExecutionsController : TaktControllerBase
{
    private readonly ITaktFlowExecutionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionsController(
        ITaktFlowExecutionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程执行记录表(FlowExecution)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowexecution:list", "查询流程执行记录表(FlowExecution)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowExecutionDto>>> GetFlowExecutionListAsync([FromQuery] TaktFlowExecutionQueryDto queryDto)
    {
        var result = await _service.GetFlowExecutionListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程执行记录表(FlowExecution)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowexecution:query", "查询流程执行记录表(FlowExecution)详情")]
    public async Task<ActionResult<TaktFlowExecutionDto>> GetFlowExecutionByIdAsync(long id)
    {
        var item = await _service.GetFlowExecutionByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程执行记录表(FlowExecution)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowexecution:query", "查询流程执行记录表(FlowExecution)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowExecutionOptionsAsync()
    {
        var result = await _service.GetFlowExecutionOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程执行记录表(FlowExecution)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowexecution:create", "创建流程执行记录表(FlowExecution)")]
    public async Task<ActionResult<TaktFlowExecutionDto>> CreateFlowExecutionAsync([FromBody] TaktFlowExecutionCreateDto dto)
    {
        var result = await _service.CreateFlowExecutionAsync(dto);
        return CreatedAtAction(nameof(GetFlowExecutionByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程执行记录表(FlowExecution)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowexecution:update", "更新流程执行记录表(FlowExecution)")]
    public async Task<ActionResult<TaktFlowExecutionDto>> UpdateFlowExecutionAsync(long id, [FromBody] TaktFlowExecutionUpdateDto dto)
    {
        var result = await _service.UpdateFlowExecutionAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程执行记录表(FlowExecution)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowexecution:delete", "删除流程执行记录表(FlowExecution)")]
    public async Task<ActionResult> DeleteFlowExecutionByIdAsync(long id)
    {
        await _service.DeleteFlowExecutionByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程执行记录表(FlowExecution)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowexecution:delete", "批量删除流程执行记录表(FlowExecution)")]
    public async Task<ActionResult> DeleteFlowExecutionBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowExecutionBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取流程执行记录表(FlowExecution)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowexecution:import", "获取流程执行记录表(FlowExecution)导入模板")]
    public async Task<IActionResult> GetFlowExecutionTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowExecutionTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程执行记录表(FlowExecution)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowexecution:import", "导入流程执行记录表(FlowExecution)")]
    public async Task<ActionResult<object>> ImportFlowExecutionAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowExecutionAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程执行记录表(FlowExecution)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowexecution:export", "导出流程执行记录表(FlowExecution)")]
    public async Task<IActionResult> ExportFlowExecutionAsync([FromBody] TaktFlowExecutionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowExecutionAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
