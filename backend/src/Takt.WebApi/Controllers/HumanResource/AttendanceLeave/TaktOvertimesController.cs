// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：加班信息表控制器，提供Overtime管理的RESTful API接口
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
/// 加班信息表控制器
/// </summary>
[Route("api/[controller]", Name = "加班信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:overtime", "加班信息表管理")]
public class TaktOvertimesController : TaktControllerBase
{
    private readonly ITaktOvertimeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimesController(
        ITaktOvertimeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取加班信息表(Overtime)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:overtime:list", "查询加班信息表(Overtime)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOvertimeDto>>> GetOvertimeListAsync([FromQuery] TaktOvertimeQueryDto queryDto)
    {
        var result = await _service.GetOvertimeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取加班信息表(Overtime)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:query", "查询加班信息表(Overtime)详情")]
    public async Task<ActionResult<TaktOvertimeDto>> GetOvertimeByIdAsync(long id)
    {
        var item = await _service.GetOvertimeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取加班信息表(Overtime)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:overtime:query", "查询加班信息表(Overtime)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOvertimeOptionsAsync()
    {
        var result = await _service.GetOvertimeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建加班信息表(Overtime)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:overtime:create", "创建加班信息表(Overtime)")]
    public async Task<ActionResult<TaktOvertimeDto>> CreateOvertimeAsync([FromBody] TaktOvertimeCreateDto dto)
    {
        var result = await _service.CreateOvertimeAsync(dto);
        return CreatedAtAction(nameof(GetOvertimeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新加班信息表(Overtime)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:update", "更新加班信息表(Overtime)")]
    public async Task<ActionResult<TaktOvertimeDto>> UpdateOvertimeAsync(long id, [FromBody] TaktOvertimeUpdateDto dto)
    {
        var result = await _service.UpdateOvertimeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除加班信息表(Overtime)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:delete", "删除加班信息表(Overtime)")]
    public async Task<ActionResult> DeleteOvertimeByIdAsync(long id)
    {
        await _service.DeleteOvertimeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除加班信息表(Overtime)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:overtime:delete", "批量删除加班信息表(Overtime)")]
    public async Task<ActionResult> DeleteOvertimeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteOvertimeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新加班信息表(Overtime)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:attendanceleave:overtime:update", "更新加班信息表(Overtime)状态")]
    public async Task<ActionResult<TaktOvertimeDto>> UpdateOvertimeStatusAsync([FromBody] TaktOvertimeStatusDto dto)
    {
        var result = await _service.UpdateOvertimeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取加班信息表(Overtime)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:overtime:import", "获取加班信息表(Overtime)导入模板")]
    public async Task<IActionResult> GetOvertimeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetOvertimeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入加班信息表(Overtime)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:overtime:import", "导入加班信息表(Overtime)")]
    public async Task<ActionResult<object>> ImportOvertimeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportOvertimeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出加班信息表(Overtime)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:overtime:export", "导出加班信息表(Overtime)")]
    public async Task<IActionResult> ExportOvertimeAsync([FromBody] TaktOvertimeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportOvertimeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
