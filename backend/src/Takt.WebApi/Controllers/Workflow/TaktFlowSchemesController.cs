// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowSchemesController.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程方案控制器，提供已发布流程方案列表供前端选择
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流流程方案控制器（流程建模：列表/详情/创建/更新/按Key查询/选项；BPM/BPMN 为必选，BpmnXml 由前端设计器或导入写入，ProcessJson 备用）
/// </summary>
[Route("api/[controller]", Name = "流程方案")]
[ApiModule("Workflow", "工作流")]
[TaktPermission("workflow:scheme", "流程方案")]
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
    /// 获取流程方案列表（分页，供流程建模/绘制页使用）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:scheme:list", "查询流程方案列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowSchemeDto>>> GetListAsync([FromQuery] TaktFlowSchemeQueryDto query)
    {
        var result = await _service.GetListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取流程方案（含 BpmnXml/ProcessJson，供设计器加载）
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:scheme:query", "查询流程方案详情")]
    public async Task<ActionResult<TaktFlowSchemeDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 获取流程方案选项列表（仅已发布，用于下拉框等）
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:scheme:list", "查询流程方案选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var list = await _service.GetOptionsAsync();
        return Ok(list);
    }

    /// <summary>
    /// 根据流程Key获取流程方案（仅已发布，供发起流程时使用）
    /// </summary>
    [HttpGet("by-key/{processKey}")]
    [TaktPermission("workflow:scheme:query", "根据Key查询流程方案")]
    public async Task<ActionResult<TaktFlowSchemeDto>> GetByProcessKeyAsync(string processKey)
    {
        var item = await _service.GetByProcessKeyAsync(processKey);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建流程方案（流程建模：可提交 BpmnXml、ProcessJson 等）
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:scheme:create", "创建流程方案")]
    public async Task<ActionResult<TaktFlowSchemeDto>> CreateAsync([FromBody] TaktFlowSchemeCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.SchemeId }, item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程方案（流程绘制保存：可更新 BpmnXml、ProcessJson 等）
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:scheme:update", "更新流程方案")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateAsync(long id, [FromBody] TaktFlowSchemeUpdateDto dto)
    {
        try
        {
            dto.SchemeId = id;
            var item = await _service.UpdateAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程方案状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("workflow:scheme:update", "更新流程方案状态")]
    public async Task<ActionResult<TaktFlowSchemeDto>> UpdateStatusAsync([FromBody] TaktFlowSchemeStatusDto dto)
    {
        try
        {
            var item = await _service.UpdateStatusAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 软删除流程方案
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:scheme:delete", "删除流程方案")]
    public async Task<ActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量软删除流程方案
    /// </summary>
    /// <param name="ids">方案ID列表</param>
    [HttpDelete("batch")]
    [TaktPermission("workflow:scheme:delete", "批量删除流程方案")]
    public async Task<ActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _service.DeleteAsync(ids ?? Array.Empty<long>());
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("workflow:scheme:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入流程方案
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:scheme:import", "导入流程方案")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _service.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出流程方案
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:scheme:export", "导出流程方案")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktFlowSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
