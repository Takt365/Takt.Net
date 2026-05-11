// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktQuartzLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：任务日志表控制器，提供QuartzLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 任务日志表控制器
/// </summary>
[Route("api/[controller]", Name = "任务日志表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:logging:quartzlog", "任务日志表管理")]
public class TaktQuartzLogsController : TaktControllerBase
{
    private readonly ITaktQuartzLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogsController(
        ITaktQuartzLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取任务日志表(QuartzLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:logging:quartzlog:list", "查询任务日志表(QuartzLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQuartzLogDto>>> GetQuartzLogListAsync([FromQuery] TaktQuartzLogQueryDto queryDto)
    {
        var result = await _service.GetQuartzLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取任务日志表(QuartzLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:logging:quartzlog:query", "查询任务日志表(QuartzLog)详情")]
    public async Task<ActionResult<TaktQuartzLogDto>> GetQuartzLogByIdAsync(long id)
    {
        var item = await _service.GetQuartzLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取任务日志表(QuartzLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:logging:quartzlog:query", "查询任务日志表(QuartzLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQuartzLogOptionsAsync()
    {
        var result = await _service.GetQuartzLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建任务日志表(QuartzLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:logging:quartzlog:create", "创建任务日志表(QuartzLog)")]
    public async Task<ActionResult<TaktQuartzLogDto>> CreateQuartzLogAsync([FromBody] TaktQuartzLogCreateDto dto)
    {
        var result = await _service.CreateQuartzLogAsync(dto);
        return CreatedAtAction(nameof(GetQuartzLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新任务日志表(QuartzLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:logging:quartzlog:update", "更新任务日志表(QuartzLog)")]
    public async Task<ActionResult<TaktQuartzLogDto>> UpdateQuartzLogAsync(long id, [FromBody] TaktQuartzLogUpdateDto dto)
    {
        var result = await _service.UpdateQuartzLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除任务日志表(QuartzLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:logging:quartzlog:delete", "删除任务日志表(QuartzLog)")]
    public async Task<ActionResult> DeleteQuartzLogByIdAsync(long id)
    {
        await _service.DeleteQuartzLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除任务日志表(QuartzLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:logging:quartzlog:delete", "批量删除任务日志表(QuartzLog)")]
    public async Task<ActionResult> DeleteQuartzLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQuartzLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新任务日志表(QuartzLog)Execute
    /// </summary>
    [HttpPut("status-execute")]
    [TaktPermission("statistics:logging:quartzlog:update", "更新任务日志表(QuartzLog)Execute")]
    public async Task<ActionResult<TaktQuartzLogDto>> UpdateQuartzLogExecuteStatusAsync([FromBody] TaktQuartzLogExecuteStatusDto dto)
    {
        var result = await _service.UpdateQuartzLogExecuteStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取任务日志表(QuartzLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:logging:quartzlog:import", "获取任务日志表(QuartzLog)导入模板")]
    public async Task<IActionResult> GetQuartzLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQuartzLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入任务日志表(QuartzLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:logging:quartzlog:import", "导入任务日志表(QuartzLog)")]
    public async Task<ActionResult<object>> ImportQuartzLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQuartzLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出任务日志表(QuartzLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:logging:quartzlog:export", "导出任务日志表(QuartzLog)")]
    public async Task<IActionResult> ExportQuartzLogAsync([FromBody] TaktQuartzLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQuartzLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
