// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowInstancesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程实例表控制器，提供FlowInstance管理的RESTful API接口
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
/// 流程实例表控制器
/// </summary>
[Route("api/[controller]", Name = "流程实例表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowinstance", "流程实例表管理")]
public class TaktFlowInstancesController : TaktControllerBase
{
    private readonly ITaktFlowInstanceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstancesController(
        ITaktFlowInstanceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程实例表(FlowInstance)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowinstance:list", "查询流程实例表(FlowInstance)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowInstanceDto>>> GetFlowInstanceListAsync([FromQuery] TaktFlowInstanceQueryDto queryDto)
    {
        var result = await _service.GetFlowInstanceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程实例表(FlowInstance)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowinstance:query", "查询流程实例表(FlowInstance)详情")]
    public async Task<ActionResult<TaktFlowInstanceDto>> GetFlowInstanceByIdAsync(long id)
    {
        var item = await _service.GetFlowInstanceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程实例表(FlowInstance)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowinstance:query", "查询流程实例表(FlowInstance)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowInstanceOptionsAsync()
    {
        var result = await _service.GetFlowInstanceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程实例表(FlowInstance)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowinstance:create", "创建流程实例表(FlowInstance)")]
    public async Task<ActionResult<TaktFlowInstanceDto>> CreateFlowInstanceAsync([FromBody] TaktFlowInstanceCreateDto dto)
    {
        var result = await _service.CreateFlowInstanceAsync(dto);
        return CreatedAtAction(nameof(GetFlowInstanceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程实例表(FlowInstance)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowinstance:update", "更新流程实例表(FlowInstance)")]
    public async Task<ActionResult<TaktFlowInstanceDto>> UpdateFlowInstanceAsync(long id, [FromBody] TaktFlowInstanceUpdateDto dto)
    {
        var result = await _service.UpdateFlowInstanceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程实例表(FlowInstance)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowinstance:delete", "删除流程实例表(FlowInstance)")]
    public async Task<ActionResult> DeleteFlowInstanceByIdAsync(long id)
    {
        await _service.DeleteFlowInstanceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程实例表(FlowInstance)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowinstance:delete", "批量删除流程实例表(FlowInstance)")]
    public async Task<ActionResult> DeleteFlowInstanceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowInstanceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新流程实例表(FlowInstance)Instance
    /// </summary>
    [HttpPut("status-instance")]
    [TaktPermission("workflow:flowinstance:update", "更新流程实例表(FlowInstance)Instance")]
    public async Task<ActionResult<TaktFlowInstanceDto>> UpdateFlowInstanceInstanceStatusAsync([FromBody] TaktFlowInstanceInstanceStatusDto dto)
    {
        var result = await _service.UpdateFlowInstanceInstanceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取流程实例表(FlowInstance)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowinstance:import", "获取流程实例表(FlowInstance)导入模板")]
    public async Task<IActionResult> GetFlowInstanceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowInstanceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程实例表(FlowInstance)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowinstance:import", "导入流程实例表(FlowInstance)")]
    public async Task<ActionResult<object>> ImportFlowInstanceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowInstanceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程实例表(FlowInstance)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowinstance:export", "导出流程实例表(FlowInstance)")]
    public async Task<IActionResult> ExportFlowInstanceAsync([FromBody] TaktFlowInstanceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowInstanceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
