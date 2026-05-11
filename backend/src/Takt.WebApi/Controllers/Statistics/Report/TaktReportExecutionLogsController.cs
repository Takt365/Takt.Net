// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Report
// 文件名称：TaktReportExecutionLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：报表执行日志表控制器，提供ReportExecutionLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Report;
using Takt.Application.Services.Statistics.Report;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Report;

/// <summary>
/// 报表执行日志表控制器
/// </summary>
[Route("api/[controller]", Name = "报表执行日志表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:report:reportexecutionlog", "报表执行日志表管理")]
public class TaktReportExecutionLogsController : TaktControllerBase
{
    private readonly ITaktReportExecutionLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogsController(
        ITaktReportExecutionLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取报表执行日志表(ReportExecutionLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:report:reportexecutionlog:list", "查询报表执行日志表(ReportExecutionLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktReportExecutionLogDto>>> GetReportExecutionLogListAsync([FromQuery] TaktReportExecutionLogQueryDto queryDto)
    {
        var result = await _service.GetReportExecutionLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:report:reportexecutionlog:query", "查询报表执行日志表(ReportExecutionLog)详情")]
    public async Task<ActionResult<TaktReportExecutionLogDto>> GetReportExecutionLogByIdAsync(long id)
    {
        var item = await _service.GetReportExecutionLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取报表执行日志表(ReportExecutionLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:report:reportexecutionlog:query", "查询报表执行日志表(ReportExecutionLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetReportExecutionLogOptionsAsync()
    {
        var result = await _service.GetReportExecutionLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:report:reportexecutionlog:create", "创建报表执行日志表(ReportExecutionLog)")]
    public async Task<ActionResult<TaktReportExecutionLogDto>> CreateReportExecutionLogAsync([FromBody] TaktReportExecutionLogCreateDto dto)
    {
        var result = await _service.CreateReportExecutionLogAsync(dto);
        return CreatedAtAction(nameof(GetReportExecutionLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:report:reportexecutionlog:update", "更新报表执行日志表(ReportExecutionLog)")]
    public async Task<ActionResult<TaktReportExecutionLogDto>> UpdateReportExecutionLogAsync(long id, [FromBody] TaktReportExecutionLogUpdateDto dto)
    {
        var result = await _service.UpdateReportExecutionLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:report:reportexecutionlog:delete", "删除报表执行日志表(ReportExecutionLog)")]
    public async Task<ActionResult> DeleteReportExecutionLogByIdAsync(long id)
    {
        await _service.DeleteReportExecutionLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:report:reportexecutionlog:delete", "批量删除报表执行日志表(ReportExecutionLog)")]
    public async Task<ActionResult> DeleteReportExecutionLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteReportExecutionLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取报表执行日志表(ReportExecutionLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:report:reportexecutionlog:import", "获取报表执行日志表(ReportExecutionLog)导入模板")]
    public async Task<IActionResult> GetReportExecutionLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetReportExecutionLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:report:reportexecutionlog:import", "导入报表执行日志表(ReportExecutionLog)")]
    public async Task<ActionResult<object>> ImportReportExecutionLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportReportExecutionLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出报表执行日志表(ReportExecutionLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:report:reportexecutionlog:export", "导出报表执行日志表(ReportExecutionLog)")]
    public async Task<IActionResult> ExportReportExecutionLogAsync([FromBody] TaktReportExecutionLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportReportExecutionLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
