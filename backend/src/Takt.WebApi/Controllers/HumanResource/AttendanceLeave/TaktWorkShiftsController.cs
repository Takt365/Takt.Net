// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：班次信息表控制器，提供WorkShift管理的RESTful API接口
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
/// 班次信息表控制器
/// </summary>
[Route("api/[controller]", Name = "班次信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:workshift", "班次信息表管理")]
public class TaktWorkShiftsController : TaktControllerBase
{
    private readonly ITaktWorkShiftService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkShiftsController(
        ITaktWorkShiftService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取班次信息表(WorkShift)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:workshift:list", "查询班次信息表(WorkShift)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktWorkShiftDto>>> GetWorkShiftListAsync([FromQuery] TaktWorkShiftQueryDto queryDto)
    {
        var result = await _service.GetWorkShiftListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取班次信息表(WorkShift)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:query", "查询班次信息表(WorkShift)详情")]
    public async Task<ActionResult<TaktWorkShiftDto>> GetWorkShiftByIdAsync(long id)
    {
        var item = await _service.GetWorkShiftByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取班次信息表(WorkShift)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:workshift:query", "查询班次信息表(WorkShift)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetWorkShiftOptionsAsync()
    {
        var result = await _service.GetWorkShiftOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建班次信息表(WorkShift)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:workshift:create", "创建班次信息表(WorkShift)")]
    public async Task<ActionResult<TaktWorkShiftDto>> CreateWorkShiftAsync([FromBody] TaktWorkShiftCreateDto dto)
    {
        var result = await _service.CreateWorkShiftAsync(dto);
        return CreatedAtAction(nameof(GetWorkShiftByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新班次信息表(WorkShift)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:update", "更新班次信息表(WorkShift)")]
    public async Task<ActionResult<TaktWorkShiftDto>> UpdateWorkShiftAsync(long id, [FromBody] TaktWorkShiftUpdateDto dto)
    {
        var result = await _service.UpdateWorkShiftAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除班次信息表(WorkShift)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:delete", "删除班次信息表(WorkShift)")]
    public async Task<ActionResult> DeleteWorkShiftByIdAsync(long id)
    {
        await _service.DeleteWorkShiftByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除班次信息表(WorkShift)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:workshift:delete", "批量删除班次信息表(WorkShift)")]
    public async Task<ActionResult> DeleteWorkShiftBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteWorkShiftBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新班次信息表(WorkShift)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:attendanceleave:workshift:update", "更新班次信息表(WorkShift)排序")]
    public async Task<ActionResult<TaktWorkShiftDto>> UpdateWorkShiftSortAsync([FromBody] TaktWorkShiftSortDto dto)
    {
        var result = await _service.UpdateWorkShiftSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取班次信息表(WorkShift)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:workshift:import", "获取班次信息表(WorkShift)导入模板")]
    public async Task<IActionResult> GetWorkShiftTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetWorkShiftTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入班次信息表(WorkShift)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:workshift:import", "导入班次信息表(WorkShift)")]
    public async Task<ActionResult<object>> ImportWorkShiftAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportWorkShiftAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出班次信息表(WorkShift)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:workshift:export", "导出班次信息表(WorkShift)")]
    public async Task<IActionResult> ExportWorkShiftAsync([FromBody] TaktWorkShiftQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportWorkShiftAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
