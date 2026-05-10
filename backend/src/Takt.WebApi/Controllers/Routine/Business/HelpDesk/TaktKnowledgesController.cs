// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：知识库表控制器，提供Knowledge管理的RESTful API接口
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
/// 知识库表控制器
/// </summary>
[Route("api/[controller]", Name = "知识库表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:knowledge", "知识库表管理")]
public class TaktKnowledgesController : TaktControllerBase
{
    private readonly ITaktKnowledgeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgesController(
        ITaktKnowledgeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取知识库表(Knowledge)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:knowledge:list", "查询知识库表(Knowledge)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktKnowledgeDto>>> GetKnowledgeListAsync([FromQuery] TaktKnowledgeQueryDto queryDto)
    {
        var result = await _service.GetKnowledgeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取知识库表(Knowledge)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledge:query", "查询知识库表(Knowledge)详情")]
    public async Task<ActionResult<TaktKnowledgeDto>> GetKnowledgeByIdAsync(long id)
    {
        var item = await _service.GetKnowledgeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取知识库表(Knowledge)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:knowledge:query", "查询知识库表(Knowledge)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetKnowledgeOptionsAsync()
    {
        var result = await _service.GetKnowledgeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建知识库表(Knowledge)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:knowledge:create", "创建知识库表(Knowledge)")]
    public async Task<ActionResult<TaktKnowledgeDto>> CreateKnowledgeAsync([FromBody] TaktKnowledgeCreateDto dto)
    {
        var result = await _service.CreateKnowledgeAsync(dto);
        return CreatedAtAction(nameof(GetKnowledgeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新知识库表(Knowledge)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledge:update", "更新知识库表(Knowledge)")]
    public async Task<ActionResult<TaktKnowledgeDto>> UpdateKnowledgeAsync(long id, [FromBody] TaktKnowledgeUpdateDto dto)
    {
        var result = await _service.UpdateKnowledgeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除知识库表(Knowledge)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledge:delete", "删除知识库表(Knowledge)")]
    public async Task<ActionResult> DeleteKnowledgeByIdAsync(long id)
    {
        await _service.DeleteKnowledgeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除知识库表(Knowledge)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:knowledge:delete", "批量删除知识库表(Knowledge)")]
    public async Task<ActionResult> DeleteKnowledgeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteKnowledgeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新知识库表(Knowledge)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:helpdesk:knowledge:update", "更新知识库表(Knowledge)状态")]
    public async Task<ActionResult<TaktKnowledgeDto>> UpdateKnowledgeStatusAsync([FromBody] TaktKnowledgeStatusDto dto)
    {
        var result = await _service.UpdateKnowledgeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新知识库表(Knowledge)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:helpdesk:knowledge:update", "更新知识库表(Knowledge)排序")]
    public async Task<ActionResult<TaktKnowledgeDto>> UpdateKnowledgeSortAsync([FromBody] TaktKnowledgeSortDto dto)
    {
        var result = await _service.UpdateKnowledgeSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取知识库表(Knowledge)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:knowledge:import", "获取知识库表(Knowledge)导入模板")]
    public async Task<IActionResult> GetKnowledgeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetKnowledgeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入知识库表(Knowledge)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:knowledge:import", "导入知识库表(Knowledge)")]
    public async Task<ActionResult<object>> ImportKnowledgeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportKnowledgeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出知识库表(Knowledge)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:knowledge:export", "导出知识库表(Knowledge)")]
    public async Task<IActionResult> ExportKnowledgeAsync([FromBody] TaktKnowledgeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportKnowledgeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
