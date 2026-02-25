// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Workflow
// 文件名称：TaktFlowFormsController.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程表单控制器，提供表单 CRUD 与按编码查询（供流程方案绑定、发起流程加载表单）
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
/// 工作流流程表单控制器
/// </summary>
[Route("api/[controller]", Name = "流程表单")]
[ApiModule("Workflow", "工作流")]
[TaktPermission("workflow:form", "流程表单")]
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
    /// 获取流程表单列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("workflow:form:list", "查询流程表单列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFlowFormDto>>> GetListAsync([FromQuery] TaktFlowFormQueryDto query)
    {
        var result = await _service.GetListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取流程表单
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("workflow:form:query", "查询流程表单详情")]
    public async Task<ActionResult<TaktFlowFormDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 根据表单编码获取流程表单（仅已发布，供流程方案绑定、发起流程时加载）
    /// </summary>
    [HttpGet("by-code/{formCode}")]
    [TaktPermission("workflow:form:query", "根据编码查询流程表单")]
    public async Task<ActionResult<TaktFlowFormDto>> GetByFormCodeAsync(string formCode)
    {
        var item = await _service.GetByFormCodeAsync(formCode);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 获取流程表单选项列表（仅已发布，用于下拉框等）
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("workflow:form:list", "查询流程表单选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var list = await _service.GetOptionsAsync();
        return Ok(list);
    }

    /// <summary>
    /// 创建流程表单
    /// </summary>
    [HttpPost]
    [TaktPermission("workflow:form:create", "创建流程表单")]
    public async Task<ActionResult<TaktFlowFormDto>> CreateAsync([FromBody] TaktFlowFormCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.FormId }, item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程表单
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("workflow:form:update", "更新流程表单")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateAsync(long id, [FromBody] TaktFlowFormUpdateDto dto)
    {
        try
        {
            dto.FormId = id;
            var item = await _service.UpdateAsync(dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新流程表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("workflow:form:update", "更新流程表单状态")]
    public async Task<ActionResult<TaktFlowFormDto>> UpdateStatusAsync([FromBody] TaktFlowFormStatusDto dto)
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
    /// 软删除流程表单
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("workflow:form:delete", "删除流程表单")]
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
    /// 批量软删除流程表单
    /// </summary>
    /// <param name="ids">表单ID列表</param>
    [HttpDelete("batch")]
    [TaktPermission("workflow:form:delete", "批量删除流程表单")]
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
    [TaktPermission("workflow:form:template", "获取导入模板")]
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
    /// 导入流程表单
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("workflow:form:import", "导入流程表单")]
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
    /// 导出流程表单
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("workflow:form:export", "导出流程表单")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktFlowFormQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
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
