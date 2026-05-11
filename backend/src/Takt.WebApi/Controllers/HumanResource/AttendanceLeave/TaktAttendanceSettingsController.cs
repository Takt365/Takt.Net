// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置表控制器，提供AttendanceSetting管理的RESTful API接口
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
/// 考勤设置表控制器
/// </summary>
[Route("api/[controller]", Name = "考勤设置表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendancesetting", "考勤设置表管理")]
public class TaktAttendanceSettingsController : TaktControllerBase
{
    private readonly ITaktAttendanceSettingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceSettingsController(
        ITaktAttendanceSettingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取考勤设置表(AttendanceSetting)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:list", "查询考勤设置表(AttendanceSetting)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceSettingDto>>> GetAttendanceSettingListAsync([FromQuery] TaktAttendanceSettingQueryDto queryDto)
    {
        var result = await _service.GetAttendanceSettingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:query", "查询考勤设置表(AttendanceSetting)详情")]
    public async Task<ActionResult<TaktAttendanceSettingDto>> GetAttendanceSettingByIdAsync(long id)
    {
        var item = await _service.GetAttendanceSettingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取考勤设置表(AttendanceSetting)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:query", "查询考勤设置表(AttendanceSetting)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceSettingOptionsAsync()
    {
        var result = await _service.GetAttendanceSettingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:create", "创建考勤设置表(AttendanceSetting)")]
    public async Task<ActionResult<TaktAttendanceSettingDto>> CreateAttendanceSettingAsync([FromBody] TaktAttendanceSettingCreateDto dto)
    {
        var result = await _service.CreateAttendanceSettingAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceSettingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:update", "更新考勤设置表(AttendanceSetting)")]
    public async Task<ActionResult<TaktAttendanceSettingDto>> UpdateAttendanceSettingAsync(long id, [FromBody] TaktAttendanceSettingUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceSettingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:delete", "删除考勤设置表(AttendanceSetting)")]
    public async Task<ActionResult> DeleteAttendanceSettingByIdAsync(long id)
    {
        await _service.DeleteAttendanceSettingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:delete", "批量删除考勤设置表(AttendanceSetting)")]
    public async Task<ActionResult> DeleteAttendanceSettingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceSettingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新考勤设置表(AttendanceSetting)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:update", "更新考勤设置表(AttendanceSetting)排序")]
    public async Task<ActionResult<TaktAttendanceSettingDto>> UpdateAttendanceSettingSortAsync([FromBody] TaktAttendanceSettingSortDto dto)
    {
        var result = await _service.UpdateAttendanceSettingSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取考勤设置表(AttendanceSetting)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:import", "获取考勤设置表(AttendanceSetting)导入模板")]
    public async Task<IActionResult> GetAttendanceSettingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceSettingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:import", "导入考勤设置表(AttendanceSetting)")]
    public async Task<ActionResult<object>> ImportAttendanceSettingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceSettingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出考勤设置表(AttendanceSetting)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancesetting:export", "导出考勤设置表(AttendanceSetting)")]
    public async Task<IActionResult> ExportAttendanceSettingAsync([FromBody] TaktAttendanceSettingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceSettingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
