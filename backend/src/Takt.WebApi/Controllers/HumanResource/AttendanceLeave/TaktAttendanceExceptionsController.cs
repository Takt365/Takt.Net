// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常表控制器，提供AttendanceException管理的RESTful API接口
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
/// 考勤异常表控制器
/// </summary>
[Route("api/[controller]", Name = "考勤异常表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendanceexception", "考勤异常表管理")]
public class TaktAttendanceExceptionsController : TaktControllerBase
{
    private readonly ITaktAttendanceExceptionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceExceptionsController(
        ITaktAttendanceExceptionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取考勤异常表(AttendanceException)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:list", "查询考勤异常表(AttendanceException)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceExceptionDto>>> GetAttendanceExceptionListAsync([FromQuery] TaktAttendanceExceptionQueryDto queryDto)
    {
        var result = await _service.GetAttendanceExceptionListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取考勤异常表(AttendanceException)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:query", "查询考勤异常表(AttendanceException)详情")]
    public async Task<ActionResult<TaktAttendanceExceptionDto>> GetAttendanceExceptionByIdAsync(long id)
    {
        var item = await _service.GetAttendanceExceptionByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取考勤异常表(AttendanceException)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:query", "查询考勤异常表(AttendanceException)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceExceptionOptionsAsync()
    {
        var result = await _service.GetAttendanceExceptionOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建考勤异常表(AttendanceException)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:create", "创建考勤异常表(AttendanceException)")]
    public async Task<ActionResult<TaktAttendanceExceptionDto>> CreateAttendanceExceptionAsync([FromBody] TaktAttendanceExceptionCreateDto dto)
    {
        var result = await _service.CreateAttendanceExceptionAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceExceptionByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新考勤异常表(AttendanceException)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:update", "更新考勤异常表(AttendanceException)")]
    public async Task<ActionResult<TaktAttendanceExceptionDto>> UpdateAttendanceExceptionAsync(long id, [FromBody] TaktAttendanceExceptionUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceExceptionAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除考勤异常表(AttendanceException)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:delete", "删除考勤异常表(AttendanceException)")]
    public async Task<ActionResult> DeleteAttendanceExceptionByIdAsync(long id)
    {
        await _service.DeleteAttendanceExceptionByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除考勤异常表(AttendanceException)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:delete", "批量删除考勤异常表(AttendanceException)")]
    public async Task<ActionResult> DeleteAttendanceExceptionBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceExceptionBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新考勤异常表(AttendanceException)Handle
    /// </summary>
    [HttpPut("status-handle")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:update", "更新考勤异常表(AttendanceException)Handle")]
    public async Task<ActionResult<TaktAttendanceExceptionDto>> UpdateAttendanceExceptionHandleStatusAsync([FromBody] TaktAttendanceExceptionHandleStatusDto dto)
    {
        var result = await _service.UpdateAttendanceExceptionHandleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取考勤异常表(AttendanceException)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:import", "获取考勤异常表(AttendanceException)导入模板")]
    public async Task<IActionResult> GetAttendanceExceptionTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceExceptionTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入考勤异常表(AttendanceException)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:import", "导入考勤异常表(AttendanceException)")]
    public async Task<ActionResult<object>> ImportAttendanceExceptionAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceExceptionAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出考勤异常表(AttendanceException)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendanceexception:export", "导出考勤异常表(AttendanceException)")]
    public async Task<IActionResult> ExportAttendanceExceptionAsync([FromBody] TaktAttendanceExceptionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceExceptionAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
