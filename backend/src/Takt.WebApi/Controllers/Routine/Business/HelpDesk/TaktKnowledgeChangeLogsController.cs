// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：知识库变更日志表控制器，提供KnowledgeChangeLog管理的RESTful API接口
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
/// 知识库变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "知识库变更日志表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:helpdesk:knowledgechangelog", "知识库变更日志表管理")]
public class TaktKnowledgeChangeLogsController : TaktControllerBase
{
    private readonly ITaktKnowledgeChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKnowledgeChangeLogsController(
        ITaktKnowledgeChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取知识库变更日志表(KnowledgeChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:list", "查询知识库变更日志表(KnowledgeChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktKnowledgeChangeLogDto>>> GetKnowledgeChangeLogListAsync([FromQuery] TaktKnowledgeChangeLogQueryDto queryDto)
    {
        var result = await _service.GetKnowledgeChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:query", "查询知识库变更日志表(KnowledgeChangeLog)详情")]
    public async Task<ActionResult<TaktKnowledgeChangeLogDto>> GetKnowledgeChangeLogByIdAsync(long id)
    {
        var item = await _service.GetKnowledgeChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取知识库变更日志表(KnowledgeChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:query", "查询知识库变更日志表(KnowledgeChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetKnowledgeChangeLogOptionsAsync()
    {
        var result = await _service.GetKnowledgeChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:create", "创建知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<ActionResult<TaktKnowledgeChangeLogDto>> CreateKnowledgeChangeLogAsync([FromBody] TaktKnowledgeChangeLogCreateDto dto)
    {
        var result = await _service.CreateKnowledgeChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetKnowledgeChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:update", "更新知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<ActionResult<TaktKnowledgeChangeLogDto>> UpdateKnowledgeChangeLogAsync(long id, [FromBody] TaktKnowledgeChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateKnowledgeChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:delete", "删除知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<ActionResult> DeleteKnowledgeChangeLogByIdAsync(long id)
    {
        await _service.DeleteKnowledgeChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:delete", "批量删除知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<ActionResult> DeleteKnowledgeChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteKnowledgeChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取知识库变更日志表(KnowledgeChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:import", "获取知识库变更日志表(KnowledgeChangeLog)导入模板")]
    public async Task<IActionResult> GetKnowledgeChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetKnowledgeChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:import", "导入知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<ActionResult<object>> ImportKnowledgeChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportKnowledgeChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出知识库变更日志表(KnowledgeChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:helpdesk:knowledgechangelog:export", "导出知识库变更日志表(KnowledgeChangeLog)")]
    public async Task<IActionResult> ExportKnowledgeChangeLogAsync([FromBody] TaktKnowledgeChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportKnowledgeChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
