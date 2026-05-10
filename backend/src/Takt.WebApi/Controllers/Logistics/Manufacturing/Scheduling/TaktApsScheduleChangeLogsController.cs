// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程变更日志表控制器，提供ApsScheduleChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Application.Services.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程变更日志表控制器
/// </summary>
[Route("api/[controller]", Name = "APS排程变更日志表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog", "APS排程变更日志表管理")]
public class TaktApsScheduleChangeLogsController : TaktControllerBase
{
    private readonly ITaktApsScheduleChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleChangeLogsController(
        ITaktApsScheduleChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:list", "查询APS排程变更日志表(ApsScheduleChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktApsScheduleChangeLogDto>>> GetApsScheduleChangeLogListAsync([FromQuery] TaktApsScheduleChangeLogQueryDto queryDto)
    {
        var result = await _service.GetApsScheduleChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:query", "查询APS排程变更日志表(ApsScheduleChangeLog)详情")]
    public async Task<ActionResult<TaktApsScheduleChangeLogDto>> GetApsScheduleChangeLogByIdAsync(long id)
    {
        var item = await _service.GetApsScheduleChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:query", "查询APS排程变更日志表(ApsScheduleChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetApsScheduleChangeLogOptionsAsync()
    {
        var result = await _service.GetApsScheduleChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:create", "创建APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<ActionResult<TaktApsScheduleChangeLogDto>> CreateApsScheduleChangeLogAsync([FromBody] TaktApsScheduleChangeLogCreateDto dto)
    {
        var result = await _service.CreateApsScheduleChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetApsScheduleChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:update", "更新APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<ActionResult<TaktApsScheduleChangeLogDto>> UpdateApsScheduleChangeLogAsync(long id, [FromBody] TaktApsScheduleChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateApsScheduleChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:delete", "删除APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<ActionResult> DeleteApsScheduleChangeLogByIdAsync(long id)
    {
        await _service.DeleteApsScheduleChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:delete", "批量删除APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<ActionResult> DeleteApsScheduleChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteApsScheduleChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取APS排程变更日志表(ApsScheduleChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:import", "获取APS排程变更日志表(ApsScheduleChangeLog)导入模板")]
    public async Task<IActionResult> GetApsScheduleChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetApsScheduleChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:import", "导入APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<ActionResult<object>> ImportApsScheduleChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportApsScheduleChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出APS排程变更日志表(ApsScheduleChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedulechangelog:export", "导出APS排程变更日志表(ApsScheduleChangeLog)")]
    public async Task<IActionResult> ExportApsScheduleChangeLogAsync([FromBody] TaktApsScheduleChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportApsScheduleChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
