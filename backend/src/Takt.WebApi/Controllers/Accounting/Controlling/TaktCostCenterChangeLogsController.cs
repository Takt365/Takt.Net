// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostCenterChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：成本中心变更记录表控制器，提供CostCenterChangeLog管理的RESTful API接口
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
/// 成本中心变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "成本中心变更记录表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:costcenterchangelog", "成本中心变更记录表管理")]
public class TaktCostCenterChangeLogsController : TaktControllerBase
{
    private readonly ITaktCostCenterChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostCenterChangeLogsController(
        ITaktCostCenterChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取成本中心变更记录表(CostCenterChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costcenterchangelog:list", "查询成本中心变更记录表(CostCenterChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostCenterChangeLogDto>>> GetCostCenterChangeLogListAsync([FromQuery] TaktCostCenterChangeLogQueryDto queryDto)
    {
        var result = await _service.GetCostCenterChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costcenterchangelog:query", "查询成本中心变更记录表(CostCenterChangeLog)详情")]
    public async Task<ActionResult<TaktCostCenterChangeLogDto>> GetCostCenterChangeLogByIdAsync(long id)
    {
        var item = await _service.GetCostCenterChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取成本中心变更记录表(CostCenterChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:costcenterchangelog:query", "查询成本中心变更记录表(CostCenterChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCostCenterChangeLogOptionsAsync()
    {
        var result = await _service.GetCostCenterChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:costcenterchangelog:create", "创建成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<ActionResult<TaktCostCenterChangeLogDto>> CreateCostCenterChangeLogAsync([FromBody] TaktCostCenterChangeLogCreateDto dto)
    {
        var result = await _service.CreateCostCenterChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetCostCenterChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costcenterchangelog:update", "更新成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<ActionResult<TaktCostCenterChangeLogDto>> UpdateCostCenterChangeLogAsync(long id, [FromBody] TaktCostCenterChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateCostCenterChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costcenterchangelog:delete", "删除成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<ActionResult> DeleteCostCenterChangeLogByIdAsync(long id)
    {
        await _service.DeleteCostCenterChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:costcenterchangelog:delete", "批量删除成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<ActionResult> DeleteCostCenterChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCostCenterChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取成本中心变更记录表(CostCenterChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costcenterchangelog:import", "获取成本中心变更记录表(CostCenterChangeLog)导入模板")]
    public async Task<IActionResult> GetCostCenterChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCostCenterChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costcenterchangelog:import", "导入成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<ActionResult<object>> ImportCostCenterChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCostCenterChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出成本中心变更记录表(CostCenterChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costcenterchangelog:export", "导出成本中心变更记录表(CostCenterChangeLog)")]
    public async Task<IActionResult> ExportCostCenterChangeLogAsync([FromBody] TaktCostCenterChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCostCenterChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
