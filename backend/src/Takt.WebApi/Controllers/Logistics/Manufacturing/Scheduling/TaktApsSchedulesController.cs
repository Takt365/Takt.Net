// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsSchedulesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程主表控制器，提供ApsSchedule管理的RESTful API接口
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
/// APS排程主表控制器
/// </summary>
[Route("api/[controller]", Name = "APS排程主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:scheduling:apsschedule", "APS排程主表管理")]
public class TaktApsSchedulesController : TaktControllerBase
{
    private readonly ITaktApsScheduleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsSchedulesController(
        ITaktApsScheduleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:list", "查询APS排程主表(ApsSchedule)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktApsScheduleDto>>> GetApsScheduleListAsync([FromQuery] TaktApsScheduleQueryDto queryDto)
    {
        var result = await _service.GetApsScheduleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取APS排程主表(ApsSchedule)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:query", "查询APS排程主表(ApsSchedule)详情")]
    public async Task<ActionResult<TaktApsScheduleDto>> GetApsScheduleByIdAsync(long id)
    {
        var item = await _service.GetApsScheduleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:query", "查询APS排程主表(ApsSchedule)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetApsScheduleOptionsAsync()
    {
        var result = await _service.GetApsScheduleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建APS排程主表(ApsSchedule)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:create", "创建APS排程主表(ApsSchedule)")]
    public async Task<ActionResult<TaktApsScheduleDto>> CreateApsScheduleAsync([FromBody] TaktApsScheduleCreateDto dto)
    {
        var result = await _service.CreateApsScheduleAsync(dto);
        return CreatedAtAction(nameof(GetApsScheduleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新APS排程主表(ApsSchedule)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:update", "更新APS排程主表(ApsSchedule)")]
    public async Task<ActionResult<TaktApsScheduleDto>> UpdateApsScheduleAsync(long id, [FromBody] TaktApsScheduleUpdateDto dto)
    {
        var result = await _service.UpdateApsScheduleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除APS排程主表(ApsSchedule)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:delete", "删除APS排程主表(ApsSchedule)")]
    public async Task<ActionResult> DeleteApsScheduleByIdAsync(long id)
    {
        await _service.DeleteApsScheduleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除APS排程主表(ApsSchedule)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:delete", "批量删除APS排程主表(ApsSchedule)")]
    public async Task<ActionResult> DeleteApsScheduleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteApsScheduleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新APS排程主表(ApsSchedule)Schedule
    /// </summary>
    [HttpPut("status-schedule")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:update", "更新APS排程主表(ApsSchedule)Schedule")]
    public async Task<ActionResult<TaktApsScheduleDto>> UpdateApsScheduleScheduleStatusAsync([FromBody] TaktApsScheduleScheduleStatusDto dto)
    {
        var result = await _service.UpdateApsScheduleScheduleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取APS排程主表(ApsSchedule)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:import", "获取APS排程主表(ApsSchedule)导入模板")]
    public async Task<IActionResult> GetApsScheduleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetApsScheduleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入APS排程主表(ApsSchedule)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:import", "导入APS排程主表(ApsSchedule)")]
    public async Task<ActionResult<object>> ImportApsScheduleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportApsScheduleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出APS排程主表(ApsSchedule)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:scheduling:apsschedule:export", "导出APS排程主表(ApsSchedule)")]
    public async Task<IActionResult> ExportApsScheduleAsync([FromBody] TaktApsScheduleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportApsScheduleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
