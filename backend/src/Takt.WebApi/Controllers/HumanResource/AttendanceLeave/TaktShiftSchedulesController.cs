// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktShiftSchedulesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：排班信息表控制器，提供ShiftSchedule管理的RESTful API接口
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
/// 排班信息表控制器
/// </summary>
[Route("api/[controller]", Name = "排班信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:shiftschedule", "排班信息表管理")]
public class TaktShiftSchedulesController : TaktControllerBase
{
    private readonly ITaktShiftScheduleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktShiftSchedulesController(
        ITaktShiftScheduleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取排班信息表(ShiftSchedule)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:list", "查询排班信息表(ShiftSchedule)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktShiftScheduleDto>>> GetShiftScheduleListAsync([FromQuery] TaktShiftScheduleQueryDto queryDto)
    {
        var result = await _service.GetShiftScheduleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取排班信息表(ShiftSchedule)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:query", "查询排班信息表(ShiftSchedule)详情")]
    public async Task<ActionResult<TaktShiftScheduleDto>> GetShiftScheduleByIdAsync(long id)
    {
        var item = await _service.GetShiftScheduleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取排班信息表(ShiftSchedule)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:query", "查询排班信息表(ShiftSchedule)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetShiftScheduleOptionsAsync()
    {
        var result = await _service.GetShiftScheduleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建排班信息表(ShiftSchedule)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:create", "创建排班信息表(ShiftSchedule)")]
    public async Task<ActionResult<TaktShiftScheduleDto>> CreateShiftScheduleAsync([FromBody] TaktShiftScheduleCreateDto dto)
    {
        var result = await _service.CreateShiftScheduleAsync(dto);
        return CreatedAtAction(nameof(GetShiftScheduleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新排班信息表(ShiftSchedule)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:update", "更新排班信息表(ShiftSchedule)")]
    public async Task<ActionResult<TaktShiftScheduleDto>> UpdateShiftScheduleAsync(long id, [FromBody] TaktShiftScheduleUpdateDto dto)
    {
        var result = await _service.UpdateShiftScheduleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除排班信息表(ShiftSchedule)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:delete", "删除排班信息表(ShiftSchedule)")]
    public async Task<ActionResult> DeleteShiftScheduleByIdAsync(long id)
    {
        await _service.DeleteShiftScheduleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除排班信息表(ShiftSchedule)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:delete", "批量删除排班信息表(ShiftSchedule)")]
    public async Task<ActionResult> DeleteShiftScheduleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteShiftScheduleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取排班信息表(ShiftSchedule)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:import", "获取排班信息表(ShiftSchedule)导入模板")]
    public async Task<IActionResult> GetShiftScheduleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetShiftScheduleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入排班信息表(ShiftSchedule)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:import", "导入排班信息表(ShiftSchedule)")]
    public async Task<ActionResult<object>> ImportShiftScheduleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportShiftScheduleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出排班信息表(ShiftSchedule)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:shiftschedule:export", "导出排班信息表(ShiftSchedule)")]
    public async Task<IActionResult> ExportShiftScheduleAsync([FromBody] TaktShiftScheduleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportShiftScheduleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
