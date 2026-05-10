// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowSchemesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案表控制器，提供FlowScheme管理的RESTful API接口
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
/// 流程方案表控制器
/// </summary>
[Route("api/[controller]", Name = "流程方案表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowscheme", "流程方案表管理")]
public class TaktFlowSchemesController : TaktControllerBase
{
    private readonly ITaktFlowSchemeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowSchemesController(
        ITaktFlowSchemeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程方案表(FlowScheme)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowscheme:list", "查询流程方案表(FlowScheme)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowSchemeDto>>> GetFlowSchemeListAsync([FromQuery] TaktFlowSchemeQueryDto queryDto)
    {
        var result = await _service.GetFlowSchemeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程方案表(FlowScheme)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowscheme:query", "查询流程方案表(FlowScheme)详情")]
    public async Task<ActionResult<TaktFlowSchemeDto>> GetFlowSchemeByIdAsync(long id)
    {
        var item = await _service.GetFlowSchemeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程方案表(FlowScheme)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowscheme:query", "查询流程方案表(FlowScheme)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowSchemeOptionsAsync()
    {
        var result = await _service.GetFlowSchemeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程方案表(FlowScheme)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowscheme:create", "创建流程方案表(FlowScheme)")]
    public async Task<ActionResult<TaktFlowSchemeDto>> CreateFlowSchemeAsync([FromBody] TaktFlowSchemeCreateDto dto)
    {
        var result = await _service.CreateFlowSchemeAsync(dto);
        return CreatedAtAction(nameof(GetFlowSchemeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowscheme:update", "更新流程方案表(FlowScheme)")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateFlowSchemeAsync(long id, [FromBody] TaktFlowSchemeUpdateDto dto)
    {
        var result = await _service.UpdateFlowSchemeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程方案表(FlowScheme)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowscheme:delete", "删除流程方案表(FlowScheme)")]
    public async Task<ActionResult> DeleteFlowSchemeByIdAsync(long id)
    {
        await _service.DeleteFlowSchemeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程方案表(FlowScheme)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowscheme:delete", "批量删除流程方案表(FlowScheme)")]
    public async Task<ActionResult> DeleteFlowSchemeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowSchemeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)Scheme
    /// </summary>
    [HttpPut("status-scheme")]
    [TaktPermission("workflow:flowscheme:update", "更新流程方案表(FlowScheme)Scheme")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateFlowSchemeSchemeStatusAsync([FromBody] TaktFlowSchemeSchemeStatusDto dto)
    {
        var result = await _service.UpdateFlowSchemeSchemeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新流程方案表(FlowScheme)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("workflow:flowscheme:update", "更新流程方案表(FlowScheme)排序")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateFlowSchemeSortAsync([FromBody] TaktFlowSchemeSortDto dto)
    {
        var result = await _service.UpdateFlowSchemeSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取流程方案表(FlowScheme)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowscheme:import", "获取流程方案表(FlowScheme)导入模板")]
    public async Task<IActionResult> GetFlowSchemeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowSchemeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程方案表(FlowScheme)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowscheme:import", "导入流程方案表(FlowScheme)")]
    public async Task<ActionResult<object>> ImportFlowSchemeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowSchemeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程方案表(FlowScheme)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowscheme:export", "导出流程方案表(FlowScheme)")]
    public async Task<IActionResult> ExportFlowSchemeAsync([FromBody] TaktFlowSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowSchemeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
