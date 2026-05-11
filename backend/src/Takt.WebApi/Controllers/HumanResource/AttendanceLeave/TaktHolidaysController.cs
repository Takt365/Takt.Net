// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktHolidaysController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：假日信息表控制器，提供Holiday管理的RESTful API接口
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
/// 假日信息表控制器
/// </summary>
[Route("api/[controller]", Name = "假日信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:holiday", "假日信息表管理")]
public class TaktHolidaysController : TaktControllerBase
{
    private readonly ITaktHolidayService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidaysController(
        ITaktHolidayService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取假日信息表(Holiday)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:holiday:list", "查询假日信息表(Holiday)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktHolidayDto>>> GetHolidayListAsync([FromQuery] TaktHolidayQueryDto queryDto)
    {
        var result = await _service.GetHolidayListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取假日信息表(Holiday)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:query", "查询假日信息表(Holiday)详情")]
    public async Task<ActionResult<TaktHolidayDto>> GetHolidayByIdAsync(long id)
    {
        var item = await _service.GetHolidayByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取假日信息表(Holiday)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:holiday:query", "查询假日信息表(Holiday)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetHolidayOptionsAsync()
    {
        var result = await _service.GetHolidayOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建假日信息表(Holiday)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:holiday:create", "创建假日信息表(Holiday)")]
    public async Task<ActionResult<TaktHolidayDto>> CreateHolidayAsync([FromBody] TaktHolidayCreateDto dto)
    {
        var result = await _service.CreateHolidayAsync(dto);
        return CreatedAtAction(nameof(GetHolidayByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新假日信息表(Holiday)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:update", "更新假日信息表(Holiday)")]
    public async Task<ActionResult<TaktHolidayDto>> UpdateHolidayAsync(long id, [FromBody] TaktHolidayUpdateDto dto)
    {
        var result = await _service.UpdateHolidayAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除假日信息表(Holiday)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "删除假日信息表(Holiday)")]
    public async Task<ActionResult> DeleteHolidayByIdAsync(long id)
    {
        await _service.DeleteHolidayByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除假日信息表(Holiday)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "批量删除假日信息表(Holiday)")]
    public async Task<ActionResult> DeleteHolidayBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteHolidayBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取假日信息表(Holiday)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:holiday:import", "获取假日信息表(Holiday)导入模板")]
    public async Task<IActionResult> GetHolidayTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetHolidayTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入假日信息表(Holiday)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:holiday:import", "导入假日信息表(Holiday)")]
    public async Task<ActionResult<object>> ImportHolidayAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportHolidayAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出假日信息表(Holiday)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:holiday:export", "导出假日信息表(Holiday)")]
    public async Task<IActionResult> ExportHolidayAsync([FromBody] TaktHolidayQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportHolidayAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
