// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录表控制器，提供AttendancePunch管理的RESTful API接口
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
/// 打卡记录表控制器
/// </summary>
[Route("api/[controller]", Name = "打卡记录表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendancepunch", "打卡记录表管理")]
public class TaktAttendancePunchsController : TaktControllerBase
{
    private readonly ITaktAttendancePunchService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendancePunchsController(
        ITaktAttendancePunchService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取打卡记录表(AttendancePunch)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:list", "查询打卡记录表(AttendancePunch)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendancePunchDto>>> GetAttendancePunchListAsync([FromQuery] TaktAttendancePunchQueryDto queryDto)
    {
        var result = await _service.GetAttendancePunchListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取打卡记录表(AttendancePunch)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:query", "查询打卡记录表(AttendancePunch)详情")]
    public async Task<ActionResult<TaktAttendancePunchDto>> GetAttendancePunchByIdAsync(long id)
    {
        var item = await _service.GetAttendancePunchByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取打卡记录表(AttendancePunch)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:query", "查询打卡记录表(AttendancePunch)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendancePunchOptionsAsync()
    {
        var result = await _service.GetAttendancePunchOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建打卡记录表(AttendancePunch)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:create", "创建打卡记录表(AttendancePunch)")]
    public async Task<ActionResult<TaktAttendancePunchDto>> CreateAttendancePunchAsync([FromBody] TaktAttendancePunchCreateDto dto)
    {
        var result = await _service.CreateAttendancePunchAsync(dto);
        return CreatedAtAction(nameof(GetAttendancePunchByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新打卡记录表(AttendancePunch)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:update", "更新打卡记录表(AttendancePunch)")]
    public async Task<ActionResult<TaktAttendancePunchDto>> UpdateAttendancePunchAsync(long id, [FromBody] TaktAttendancePunchUpdateDto dto)
    {
        var result = await _service.UpdateAttendancePunchAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除打卡记录表(AttendancePunch)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:delete", "删除打卡记录表(AttendancePunch)")]
    public async Task<ActionResult> DeleteAttendancePunchByIdAsync(long id)
    {
        await _service.DeleteAttendancePunchByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除打卡记录表(AttendancePunch)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:delete", "批量删除打卡记录表(AttendancePunch)")]
    public async Task<ActionResult> DeleteAttendancePunchBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendancePunchBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取打卡记录表(AttendancePunch)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:import", "获取打卡记录表(AttendancePunch)导入模板")]
    public async Task<IActionResult> GetAttendancePunchTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendancePunchTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入打卡记录表(AttendancePunch)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:import", "导入打卡记录表(AttendancePunch)")]
    public async Task<ActionResult<object>> ImportAttendancePunchAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendancePunchAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出打卡记录表(AttendancePunch)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancepunch:export", "导出打卡记录表(AttendancePunch)")]
    public async Task<IActionResult> ExportAttendancePunchAsync([FromBody] TaktAttendancePunchQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendancePunchAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
