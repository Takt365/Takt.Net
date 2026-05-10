// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowOperationsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：流程操作历史表控制器，提供FlowOperation管理的RESTful API接口
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
/// 流程操作历史表控制器
/// </summary>
[Route("api/[controller]", Name = "流程操作历史表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowoperation", "流程操作历史表管理")]
public class TaktFlowOperationsController : TaktControllerBase
{
    private readonly ITaktFlowOperationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationsController(
        ITaktFlowOperationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程操作历史表(FlowOperation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowoperation:list", "查询流程操作历史表(FlowOperation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowOperationDto>>> GetFlowOperationListAsync([FromQuery] TaktFlowOperationQueryDto queryDto)
    {
        var result = await _service.GetFlowOperationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程操作历史表(FlowOperation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowoperation:query", "查询流程操作历史表(FlowOperation)详情")]
    public async Task<ActionResult<TaktFlowOperationDto>> GetFlowOperationByIdAsync(long id)
    {
        var item = await _service.GetFlowOperationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程操作历史表(FlowOperation)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowoperation:query", "查询流程操作历史表(FlowOperation)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowOperationOptionsAsync()
    {
        var result = await _service.GetFlowOperationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程操作历史表(FlowOperation)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowoperation:create", "创建流程操作历史表(FlowOperation)")]
    public async Task<ActionResult<TaktFlowOperationDto>> CreateFlowOperationAsync([FromBody] TaktFlowOperationCreateDto dto)
    {
        var result = await _service.CreateFlowOperationAsync(dto);
        return CreatedAtAction(nameof(GetFlowOperationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程操作历史表(FlowOperation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowoperation:update", "更新流程操作历史表(FlowOperation)")]
    public async Task<ActionResult<TaktFlowOperationDto>> UpdateFlowOperationAsync(long id, [FromBody] TaktFlowOperationUpdateDto dto)
    {
        var result = await _service.UpdateFlowOperationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程操作历史表(FlowOperation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowoperation:delete", "删除流程操作历史表(FlowOperation)")]
    public async Task<ActionResult> DeleteFlowOperationByIdAsync(long id)
    {
        await _service.DeleteFlowOperationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程操作历史表(FlowOperation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowoperation:delete", "批量删除流程操作历史表(FlowOperation)")]
    public async Task<ActionResult> DeleteFlowOperationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowOperationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取流程操作历史表(FlowOperation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowoperation:import", "获取流程操作历史表(FlowOperation)导入模板")]
    public async Task<IActionResult> GetFlowOperationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowOperationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程操作历史表(FlowOperation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowoperation:import", "导入流程操作历史表(FlowOperation)")]
    public async Task<ActionResult<object>> ImportFlowOperationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowOperationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程操作历史表(FlowOperation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowoperation:export", "导出流程操作历史表(FlowOperation)")]
    public async Task<IActionResult> ExportFlowOperationAsync([FromBody] TaktFlowOperationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowOperationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
