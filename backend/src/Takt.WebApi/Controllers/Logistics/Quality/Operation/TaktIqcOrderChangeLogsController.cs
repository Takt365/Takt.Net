// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单变更日志表控制器，提供IqcOrderChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 进货检验单变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "进货检验单变更日志表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:iqcorderchangelog", "进货检验单变更日志表管理")]
public class TaktIqcOrderChangeLogsController : TaktControllerBase
{
    private readonly ITaktIqcOrderChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIqcOrderChangeLogsController(
        ITaktIqcOrderChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取进货检验单变更日志表(IqcOrderChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:list", "查询进货检验单变更日志表(IqcOrderChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIqcOrderChangeLogDto>>> GetIqcOrderChangeLogListAsync([FromQuery] TaktIqcOrderChangeLogQueryDto queryDto)
    {
        var result = await _service.GetIqcOrderChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:query", "查询进货检验单变更日志表(IqcOrderChangeLog)详情")]
    public async Task<ActionResult<TaktIqcOrderChangeLogDto>> GetIqcOrderChangeLogByIdAsync(long id)
    {
        var item = await _service.GetIqcOrderChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取进货检验单变更日志表(IqcOrderChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:query", "查询进货检验单变更日志表(IqcOrderChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIqcOrderChangeLogOptionsAsync()
    {
        var result = await _service.GetIqcOrderChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:create", "创建进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<ActionResult<TaktIqcOrderChangeLogDto>> CreateIqcOrderChangeLogAsync([FromBody] TaktIqcOrderChangeLogCreateDto dto)
    {
        var result = await _service.CreateIqcOrderChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetIqcOrderChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:update", "更新进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<ActionResult<TaktIqcOrderChangeLogDto>> UpdateIqcOrderChangeLogAsync(long id, [FromBody] TaktIqcOrderChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateIqcOrderChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:delete", "删除进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteIqcOrderChangeLogByIdAsync(long id)
    {
        await _service.DeleteIqcOrderChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:delete", "批量删除进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<ActionResult> DeleteIqcOrderChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIqcOrderChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取进货检验单变更日志表(IqcOrderChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:import", "获取进货检验单变更日志表(IqcOrderChangeLog)导入模板")]
    public async Task<IActionResult> GetIqcOrderChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIqcOrderChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:import", "导入进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<ActionResult<object>> ImportIqcOrderChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIqcOrderChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出进货检验单变更日志表(IqcOrderChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:iqcorderchangelog:export", "导出进货检验单变更日志表(IqcOrderChangeLog)")]
    public async Task<IActionResult> ExportIqcOrderChangeLogAsync([FromBody] TaktIqcOrderChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIqcOrderChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
