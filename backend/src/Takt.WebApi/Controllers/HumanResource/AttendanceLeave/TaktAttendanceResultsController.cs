// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResultsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤结果表控制器，提供AttendanceResult管理的RESTful API接口
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
/// 考勤结果表控制器
/// </summary>
[Route("api/[controller]", Name = "考勤结果表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendanceresult", "考勤结果表管理")]
public class TaktAttendanceResultsController : TaktControllerBase
{
    private readonly ITaktAttendanceResultService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceResultsController(
        ITaktAttendanceResultService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取考勤结果表(AttendanceResult)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:list", "查询考勤结果表(AttendanceResult)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceResultDto>>> GetAttendanceResultListAsync([FromQuery] TaktAttendanceResultQueryDto queryDto)
    {
        var result = await _service.GetAttendanceResultListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取考勤结果表(AttendanceResult)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:query", "查询考勤结果表(AttendanceResult)详情")]
    public async Task<ActionResult<TaktAttendanceResultDto>> GetAttendanceResultByIdAsync(long id)
    {
        var item = await _service.GetAttendanceResultByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取考勤结果表(AttendanceResult)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:query", "查询考勤结果表(AttendanceResult)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceResultOptionsAsync()
    {
        var result = await _service.GetAttendanceResultOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建考勤结果表(AttendanceResult)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:create", "创建考勤结果表(AttendanceResult)")]
    public async Task<ActionResult<TaktAttendanceResultDto>> CreateAttendanceResultAsync([FromBody] TaktAttendanceResultCreateDto dto)
    {
        var result = await _service.CreateAttendanceResultAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceResultByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新考勤结果表(AttendanceResult)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:update", "更新考勤结果表(AttendanceResult)")]
    public async Task<ActionResult<TaktAttendanceResultDto>> UpdateAttendanceResultAsync(long id, [FromBody] TaktAttendanceResultUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceResultAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除考勤结果表(AttendanceResult)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:delete", "删除考勤结果表(AttendanceResult)")]
    public async Task<ActionResult> DeleteAttendanceResultByIdAsync(long id)
    {
        await _service.DeleteAttendanceResultByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除考勤结果表(AttendanceResult)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:delete", "批量删除考勤结果表(AttendanceResult)")]
    public async Task<ActionResult> DeleteAttendanceResultBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceResultBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新考勤结果表(AttendanceResult)Attendance
    /// </summary>
    [HttpPut("status-attendance")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:update", "更新考勤结果表(AttendanceResult)Attendance")]
    public async Task<ActionResult<TaktAttendanceResultDto>> UpdateAttendanceResultAttendanceStatusAsync([FromBody] TaktAttendanceResultAttendanceStatusDto dto)
    {
        var result = await _service.UpdateAttendanceResultAttendanceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取考勤结果表(AttendanceResult)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:import", "获取考勤结果表(AttendanceResult)导入模板")]
    public async Task<IActionResult> GetAttendanceResultTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceResultTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入考勤结果表(AttendanceResult)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:import", "导入考勤结果表(AttendanceResult)")]
    public async Task<ActionResult<object>> ImportAttendanceResultAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceResultAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出考勤结果表(AttendanceResult)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendanceresult:export", "导出考勤结果表(AttendanceResult)")]
    public async Task<IActionResult> ExportAttendanceResultAsync([FromBody] TaktAttendanceResultQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceResultAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
