// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourcesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录表控制器，提供AttendanceSource管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤源记录表控制器
/// </summary>
[Route("api/[controller]", Name = "考勤源记录表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendancesource", "考勤源记录表管理")]
public class TaktAttendanceSourcesController : TaktControllerBase
{
    private readonly ITaktAttendanceSourceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSourcesController(
        ITaktAttendanceSourceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取考勤源记录表(AttendanceSource)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:list", "查询考勤源记录表(AttendanceSource)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceSourceDto>>> GetAttendanceSourceListAsync([FromQuery] TaktAttendanceSourceQueryDto queryDto)
    {
        var result = await _service.GetAttendanceSourceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:query", "查询考勤源记录表(AttendanceSource)详情")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> GetAttendanceSourceByIdAsync(long id)
    {
        var item = await _service.GetAttendanceSourceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取考勤源记录表(AttendanceSource)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:query", "查询考勤源记录表(AttendanceSource)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceSourceOptionsAsync()
    {
        var result = await _service.GetAttendanceSourceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancesource:create", "创建考勤源记录表(AttendanceSource)")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> CreateAttendanceSourceAsync([FromBody] TaktAttendanceSourceCreateDto dto)
    {
        var result = await _service.CreateAttendanceSourceAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceSourceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:update", "更新考勤源记录表(AttendanceSource)")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> UpdateAttendanceSourceAsync(long id, [FromBody] TaktAttendanceSourceUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceSourceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:delete", "删除考勤源记录表(AttendanceSource)")]
    public async Task<ActionResult> DeleteAttendanceSourceByIdAsync(long id)
    {
        await _service.DeleteAttendanceSourceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:delete", "批量删除考勤源记录表(AttendanceSource)")]
    public async Task<ActionResult> DeleteAttendanceSourceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceSourceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取考勤源记录表(AttendanceSource)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:import", "获取考勤源记录表(AttendanceSource)导入模板")]
    public async Task<IActionResult> GetAttendanceSourceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceSourceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:import", "导入考勤源记录表(AttendanceSource)")]
    public async Task<ActionResult<object>> ImportAttendanceSourceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceSourceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出考勤源记录表(AttendanceSource)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:export", "导出考勤源记录表(AttendanceSource)")]
    public async Task<IActionResult> ExportAttendanceSourceAsync([FromBody] TaktAttendanceSourceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceSourceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
