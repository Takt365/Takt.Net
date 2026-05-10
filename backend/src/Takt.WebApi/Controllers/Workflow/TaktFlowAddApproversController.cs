// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowAddApproversController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：流程加签表控制器，提供FlowAddApprover管理的RESTful API接口
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
/// 流程加签表控制器
/// </summary>
[Route("api/[controller]", Name = "流程加签表")]
[ApiModule("System", "系统管理")]
[TaktPermission("workflow:flowaddapprover", "流程加签表管理")]
public class TaktFlowAddApproversController : TaktControllerBase
{
    private readonly ITaktFlowAddApproverService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproversController(
        ITaktFlowAddApproverService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取流程加签表(FlowAddApprover)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:flowaddapprover:list", "查询流程加签表(FlowAddApprover)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowAddApproverDto>>> GetFlowAddApproverListAsync([FromQuery] TaktFlowAddApproverQueryDto queryDto)
    {
        var result = await _service.GetFlowAddApproverListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取流程加签表(FlowAddApprover)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:flowaddapprover:query", "查询流程加签表(FlowAddApprover)详情")]
    public async Task<ActionResult<TaktFlowAddApproverDto>> GetFlowAddApproverByIdAsync(long id)
    {
        var item = await _service.GetFlowAddApproverByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取流程加签表(FlowAddApprover)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:flowaddapprover:query", "查询流程加签表(FlowAddApprover)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFlowAddApproverOptionsAsync()
    {
        var result = await _service.GetFlowAddApproverOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建流程加签表(FlowAddApprover)
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:flowaddapprover:create", "创建流程加签表(FlowAddApprover)")]
    public async Task<ActionResult<TaktFlowAddApproverDto>> CreateFlowAddApproverAsync([FromBody] TaktFlowAddApproverCreateDto dto)
    {
        var result = await _service.CreateFlowAddApproverAsync(dto);
        return CreatedAtAction(nameof(GetFlowAddApproverByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新流程加签表(FlowAddApprover)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:flowaddapprover:update", "更新流程加签表(FlowAddApprover)")]
    public async Task<ActionResult<TaktFlowAddApproverDto>> UpdateFlowAddApproverAsync(long id, [FromBody] TaktFlowAddApproverUpdateDto dto)
    {
        var result = await _service.UpdateFlowAddApproverAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除流程加签表(FlowAddApprover)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:flowaddapprover:delete", "删除流程加签表(FlowAddApprover)")]
    public async Task<ActionResult> DeleteFlowAddApproverByIdAsync(long id)
    {
        await _service.DeleteFlowAddApproverByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除流程加签表(FlowAddApprover)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("workflow:flowaddapprover:delete", "批量删除流程加签表(FlowAddApprover)")]
    public async Task<ActionResult> DeleteFlowAddApproverBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFlowAddApproverBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新流程加签表(FlowAddApprover)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("workflow:flowaddapprover:update", "更新流程加签表(FlowAddApprover)状态")]
    public async Task<ActionResult<TaktFlowAddApproverDto>> UpdateFlowAddApproverStatusAsync([FromBody] TaktFlowAddApproverStatusDto dto)
    {
        var result = await _service.UpdateFlowAddApproverStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取流程加签表(FlowAddApprover)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:flowaddapprover:import", "获取流程加签表(FlowAddApprover)导入模板")]
    public async Task<IActionResult> GetFlowAddApproverTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFlowAddApproverTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入流程加签表(FlowAddApprover)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:flowaddapprover:import", "导入流程加签表(FlowAddApprover)")]
    public async Task<ActionResult<object>> ImportFlowAddApproverAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFlowAddApproverAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出流程加签表(FlowAddApprover)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:flowaddapprover:export", "导出流程加签表(FlowAddApprover)")]
    public async Task<IActionResult> ExportFlowAddApproverAsync([FromBody] TaktFlowAddApproverQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFlowAddApproverAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
