// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktProfitCenterChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：利润中心变更记录表控制器，提供ProfitCenterChangeLog管理的RESTful API接口
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
/// 利润中心变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "利润中心变更记录表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:profitcenterchangelog", "利润中心变更记录表管理")]
public class TaktProfitCenterChangeLogsController : TaktControllerBase
{
    private readonly ITaktProfitCenterChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProfitCenterChangeLogsController(
        ITaktProfitCenterChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:list", "查询利润中心变更记录表(ProfitCenterChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProfitCenterChangeLogDto>>> GetProfitCenterChangeLogListAsync([FromQuery] TaktProfitCenterChangeLogQueryDto queryDto)
    {
        var result = await _service.GetProfitCenterChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:query", "查询利润中心变更记录表(ProfitCenterChangeLog)详情")]
    public async Task<ActionResult<TaktProfitCenterChangeLogDto>> GetProfitCenterChangeLogByIdAsync(long id)
    {
        var item = await _service.GetProfitCenterChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:query", "查询利润中心变更记录表(ProfitCenterChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProfitCenterChangeLogOptionsAsync()
    {
        var result = await _service.GetProfitCenterChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:profitcenterchangelog:create", "创建利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<ActionResult<TaktProfitCenterChangeLogDto>> CreateProfitCenterChangeLogAsync([FromBody] TaktProfitCenterChangeLogCreateDto dto)
    {
        var result = await _service.CreateProfitCenterChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetProfitCenterChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:update", "更新利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<ActionResult<TaktProfitCenterChangeLogDto>> UpdateProfitCenterChangeLogAsync(long id, [FromBody] TaktProfitCenterChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateProfitCenterChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:delete", "删除利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<ActionResult> DeleteProfitCenterChangeLogByIdAsync(long id)
    {
        await _service.DeleteProfitCenterChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:delete", "批量删除利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<ActionResult> DeleteProfitCenterChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProfitCenterChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取利润中心变更记录表(ProfitCenterChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:import", "获取利润中心变更记录表(ProfitCenterChangeLog)导入模板")]
    public async Task<IActionResult> GetProfitCenterChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProfitCenterChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:import", "导入利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<ActionResult<object>> ImportProfitCenterChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProfitCenterChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出利润中心变更记录表(ProfitCenterChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:profitcenterchangelog:export", "导出利润中心变更记录表(ProfitCenterChangeLog)")]
    public async Task<IActionResult> ExportProfitCenterChangeLogAsync([FromBody] TaktProfitCenterChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProfitCenterChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
