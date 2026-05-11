// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowFormsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单表控制器，提供FlowForm管理的RESTful API接口
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
/// 流程表单表控制器
/// </summary>
[Route("api/[controller]", Name = "流程表单表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowform", "流程表单表管理")]
public class TaktFlowFormsController : TaktControllerBase
{
    private readonly ITaktFlowFormService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowFormsController(
        ITaktFlowFormService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowform:list", "查询流程表单表(FlowForm)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowFormDto>>> GetFlowFormListAsync([FromQuery] TaktFlowFormQueryDto queryDto)
    {
        var result = await _service.GetFlowFormListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程表单表(FlowForm)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowform:query", "查询流程表单表(FlowForm)详情")]
    public async Task<ActionResult<TaktFlowFormDto>> GetFlowFormByIdAsync(long id)
    {
        var item = await _service.GetFlowFormByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowform:query", "查询流程表单表(FlowForm)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowFormOptionsAsync()
    {
        var result = await _service.GetFlowFormOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程表单表(FlowForm)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowform:create", "创建流程表单表(FlowForm)")]
    public async Task<ActionResult<TaktFlowFormDto>> CreateFlowFormAsync([FromBody] TaktFlowFormCreateDto dto)
    {
        var result = await _service.CreateFlowFormAsync(dto);
        return CreatedAtAction(nameof(GetFlowFormByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowform:update", "更新流程表单表(FlowForm)")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateFlowFormAsync(long id, [FromBody] TaktFlowFormUpdateDto dto)
    {
        var result = await _service.UpdateFlowFormAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程表单表(FlowForm)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowform:delete", "删除流程表单表(FlowForm)")]
    public async Task<ActionResult> DeleteFlowFormByIdAsync(long id)
    {
        await _service.DeleteFlowFormByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程表单表(FlowForm)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowform:delete", "批量删除流程表单表(FlowForm)")]
    public async Task<ActionResult> DeleteFlowFormBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowFormBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)Form
    /// </summary>
    [HttpPut("status-form")]
    [TaktPermission("workflow:flowform:update", "更新流程表单表(FlowForm)Form")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateFlowFormFormStatusAsync([FromBody] TaktFlowFormFormStatusDto dto)
    {
        var result = await _service.UpdateFlowFormFormStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新流程表单表(FlowForm)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("workflow:flowform:update", "更新流程表单表(FlowForm)排序")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateFlowFormSortAsync([FromBody] TaktFlowFormSortDto dto)
    {
        var result = await _service.UpdateFlowFormSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取流程表单表(FlowForm)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowform:import", "获取流程表单表(FlowForm)导入模板")]
    public async Task<IActionResult> GetFlowFormTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowFormTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程表单表(FlowForm)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowform:import", "导入流程表单表(FlowForm)")]
    public async Task<ActionResult<object>> ImportFlowFormAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowFormAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程表单表(FlowForm)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowform:export", "导出流程表单表(FlowForm)")]
    public async Task<IActionResult> ExportFlowFormAsync([FromBody] TaktFlowFormQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowFormAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
