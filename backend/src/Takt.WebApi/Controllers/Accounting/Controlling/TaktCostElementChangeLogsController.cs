// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostElementChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素变更记录表控制器，提供CostElementChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 成本要素变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "成本要素变更记录表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:costelementchangelog", "成本要素变更记录表管理")]
public class TaktCostElementChangeLogsController : TaktControllerBase
{
    private readonly ITaktCostElementChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementChangeLogsController(
        ITaktCostElementChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取成本要素变更记录表(CostElementChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costelementchangelog:list", "查询成本要素变更记录表(CostElementChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostElementChangeLogDto>>> GetCostElementChangeLogListAsync([FromQuery] TaktCostElementChangeLogQueryDto queryDto)
    {
        var result = await _service.GetCostElementChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costelementchangelog:query", "查询成本要素变更记录表(CostElementChangeLog)详情")]
    public async Task<ActionResult<TaktCostElementChangeLogDto>> GetCostElementChangeLogByIdAsync(long id)
    {
        var item = await _service.GetCostElementChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取成本要素变更记录表(CostElementChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:costelementchangelog:query", "查询成本要素变更记录表(CostElementChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCostElementChangeLogOptionsAsync()
    {
        var result = await _service.GetCostElementChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:costelementchangelog:create", "创建成本要素变更记录表(CostElementChangeLog)")]
    public async Task<ActionResult<TaktCostElementChangeLogDto>> CreateCostElementChangeLogAsync([FromBody] TaktCostElementChangeLogCreateDto dto)
    {
        var result = await _service.CreateCostElementChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetCostElementChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costelementchangelog:update", "更新成本要素变更记录表(CostElementChangeLog)")]
    public async Task<ActionResult<TaktCostElementChangeLogDto>> UpdateCostElementChangeLogAsync(long id, [FromBody] TaktCostElementChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateCostElementChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costelementchangelog:delete", "删除成本要素变更记录表(CostElementChangeLog)")]
    public async Task<ActionResult> DeleteCostElementChangeLogByIdAsync(long id)
    {
        await _service.DeleteCostElementChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:costelementchangelog:delete", "批量删除成本要素变更记录表(CostElementChangeLog)")]
    public async Task<ActionResult> DeleteCostElementChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCostElementChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取成本要素变更记录表(CostElementChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costelementchangelog:import", "获取成本要素变更记录表(CostElementChangeLog)导入模板")]
    public async Task<IActionResult> GetCostElementChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCostElementChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costelementchangelog:import", "导入成本要素变更记录表(CostElementChangeLog)")]
    public async Task<ActionResult<object>> ImportCostElementChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCostElementChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出成本要素变更记录表(CostElementChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costelementchangelog:export", "导出成本要素变更记录表(CostElementChangeLog)")]
    public async Task<IActionResult> ExportCostElementChangeLogAsync([FromBody] TaktCostElementChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCostElementChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
