// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDevicesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备表控制器，提供AttendanceDevice管理的RESTful API接口
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
/// 考勤设备表控制器
/// </summary>
[Route("api/[controller]", Name = "考勤设备表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendancedevice", "考勤设备表管理")]
public class TaktAttendanceDevicesController : TaktControllerBase
{
    private readonly ITaktAttendanceDeviceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceDevicesController(
        ITaktAttendanceDeviceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取考勤设备表(AttendanceDevice)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:list", "查询考勤设备表(AttendanceDevice)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceDeviceDto>>> GetAttendanceDeviceListAsync([FromQuery] TaktAttendanceDeviceQueryDto queryDto)
    {
        var result = await _service.GetAttendanceDeviceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:query", "查询考勤设备表(AttendanceDevice)详情")]
    public async Task<ActionResult<TaktAttendanceDeviceDto>> GetAttendanceDeviceByIdAsync(long id)
    {
        var item = await _service.GetAttendanceDeviceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取考勤设备表(AttendanceDevice)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:query", "查询考勤设备表(AttendanceDevice)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceDeviceOptionsAsync()
    {
        var result = await _service.GetAttendanceDeviceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:create", "创建考勤设备表(AttendanceDevice)")]
    public async Task<ActionResult<TaktAttendanceDeviceDto>> CreateAttendanceDeviceAsync([FromBody] TaktAttendanceDeviceCreateDto dto)
    {
        var result = await _service.CreateAttendanceDeviceAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceDeviceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:update", "更新考勤设备表(AttendanceDevice)")]
    public async Task<ActionResult<TaktAttendanceDeviceDto>> UpdateAttendanceDeviceAsync(long id, [FromBody] TaktAttendanceDeviceUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceDeviceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:delete", "删除考勤设备表(AttendanceDevice)")]
    public async Task<ActionResult> DeleteAttendanceDeviceByIdAsync(long id)
    {
        await _service.DeleteAttendanceDeviceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:delete", "批量删除考勤设备表(AttendanceDevice)")]
    public async Task<ActionResult> DeleteAttendanceDeviceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceDeviceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新考勤设备表(AttendanceDevice)Device
    /// </summary>
    [HttpPut("status-device")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:update", "更新考勤设备表(AttendanceDevice)Device")]
    public async Task<ActionResult<TaktAttendanceDeviceDto>> UpdateAttendanceDeviceDeviceStatusAsync([FromBody] TaktAttendanceDeviceDeviceStatusDto dto)
    {
        var result = await _service.UpdateAttendanceDeviceDeviceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取考勤设备表(AttendanceDevice)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:import", "获取考勤设备表(AttendanceDevice)导入模板")]
    public async Task<IActionResult> GetAttendanceDeviceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceDeviceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:import", "导入考勤设备表(AttendanceDevice)")]
    public async Task<ActionResult<object>> ImportAttendanceDeviceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceDeviceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出考勤设备表(AttendanceDevice)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:export", "导出考勤设备表(AttendanceDevice)")]
    public async Task<IActionResult> ExportAttendanceDeviceAsync([FromBody] TaktAttendanceDeviceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceDeviceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
