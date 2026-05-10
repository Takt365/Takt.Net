// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Report
// 文件名称：TaktReportSchemesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：报表方案表控制器，提供ReportScheme管理的RESTful API接口
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
/// 报表方案表控制器
/// </summary>
[Route("api/[controller]", Name = "报表方案表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:report:reportscheme", "报表方案表管理")]
public class TaktReportSchemesController : TaktControllerBase
{
    private readonly ITaktReportSchemeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportSchemesController(
        ITaktReportSchemeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:report:reportscheme:list", "查询报表方案表(ReportScheme)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktReportSchemeDto>>> GetReportSchemeListAsync([FromQuery] TaktReportSchemeQueryDto queryDto)
    {
        var result = await _service.GetReportSchemeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取报表方案表(ReportScheme)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:report:reportscheme:query", "查询报表方案表(ReportScheme)详情")]
    public async Task<ActionResult<TaktReportSchemeDto>> GetReportSchemeByIdAsync(long id)
    {
        var item = await _service.GetReportSchemeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:report:reportscheme:query", "查询报表方案表(ReportScheme)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetReportSchemeOptionsAsync()
    {
        var result = await _service.GetReportSchemeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建报表方案表(ReportScheme)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:report:reportscheme:create", "创建报表方案表(ReportScheme)")]
    public async Task<ActionResult<TaktReportSchemeDto>> CreateReportSchemeAsync([FromBody] TaktReportSchemeCreateDto dto)
    {
        var result = await _service.CreateReportSchemeAsync(dto);
        return CreatedAtAction(nameof(GetReportSchemeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:report:reportscheme:update", "更新报表方案表(ReportScheme)")]
    public async Task<ActionResult<TaktReportSchemeDto>> UpdateReportSchemeAsync(long id, [FromBody] TaktReportSchemeUpdateDto dto)
    {
        var result = await _service.UpdateReportSchemeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除报表方案表(ReportScheme)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:report:reportscheme:delete", "删除报表方案表(ReportScheme)")]
    public async Task<ActionResult> DeleteReportSchemeByIdAsync(long id)
    {
        await _service.DeleteReportSchemeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除报表方案表(ReportScheme)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:report:reportscheme:delete", "批量删除报表方案表(ReportScheme)")]
    public async Task<ActionResult> DeleteReportSchemeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteReportSchemeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("statistics:report:reportscheme:update", "更新报表方案表(ReportScheme)状态")]
    public async Task<ActionResult<TaktReportSchemeDto>> UpdateReportSchemeStatusAsync([FromBody] TaktReportSchemeStatusDto dto)
    {
        var result = await _service.UpdateReportSchemeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新报表方案表(ReportScheme)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("statistics:report:reportscheme:update", "更新报表方案表(ReportScheme)排序")]
    public async Task<ActionResult<TaktReportSchemeDto>> UpdateReportSchemeSortAsync([FromBody] TaktReportSchemeSortDto dto)
    {
        var result = await _service.UpdateReportSchemeSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取报表方案表(ReportScheme)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:report:reportscheme:import", "获取报表方案表(ReportScheme)导入模板")]
    public async Task<IActionResult> GetReportSchemeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetReportSchemeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入报表方案表(ReportScheme)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:report:reportscheme:import", "导入报表方案表(ReportScheme)")]
    public async Task<ActionResult<object>> ImportReportSchemeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportReportSchemeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出报表方案表(ReportScheme)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:report:reportscheme:export", "导出报表方案表(ReportScheme)")]
    public async Task<IActionResult> ExportReportSchemeAsync([FromBody] TaktReportSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportReportSchemeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
