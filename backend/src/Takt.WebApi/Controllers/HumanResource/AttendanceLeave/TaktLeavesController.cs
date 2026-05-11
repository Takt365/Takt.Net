// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktLeavesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：请假信息表控制器，提供Leave管理的RESTful API接口
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
/// 请假信息表控制器
/// </summary>
[Route("api/[controller]", Name = "请假信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:leave", "请假信息表管理")]
public class TaktLeavesController : TaktControllerBase
{
    private readonly ITaktLeaveService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLeavesController(
        ITaktLeaveService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取请假信息表(Leave)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:leave:list", "查询请假信息表(Leave)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktLeaveDto>>> GetLeaveListAsync([FromQuery] TaktLeaveQueryDto queryDto)
    {
        var result = await _service.GetLeaveListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取请假信息表(Leave)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:query", "查询请假信息表(Leave)详情")]
    public async Task<ActionResult<TaktLeaveDto>> GetLeaveByIdAsync(long id)
    {
        var item = await _service.GetLeaveByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取请假信息表(Leave)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:leave:query", "查询请假信息表(Leave)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetLeaveOptionsAsync()
    {
        var result = await _service.GetLeaveOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建请假信息表(Leave)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:leave:create", "创建请假信息表(Leave)")]
    public async Task<ActionResult<TaktLeaveDto>> CreateLeaveAsync([FromBody] TaktLeaveCreateDto dto)
    {
        var result = await _service.CreateLeaveAsync(dto);
        return CreatedAtAction(nameof(GetLeaveByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新请假信息表(Leave)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:update", "更新请假信息表(Leave)")]
    public async Task<ActionResult<TaktLeaveDto>> UpdateLeaveAsync(long id, [FromBody] TaktLeaveUpdateDto dto)
    {
        var result = await _service.UpdateLeaveAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除请假信息表(Leave)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:leave:delete", "删除请假信息表(Leave)")]
    public async Task<ActionResult> DeleteLeaveByIdAsync(long id)
    {
        await _service.DeleteLeaveByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除请假信息表(Leave)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:leave:delete", "批量删除请假信息表(Leave)")]
    public async Task<ActionResult> DeleteLeaveBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteLeaveBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新请假信息表(Leave)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:attendanceleave:leave:update", "更新请假信息表(Leave)状态")]
    public async Task<ActionResult<TaktLeaveDto>> UpdateLeaveStatusAsync([FromBody] TaktLeaveStatusDto dto)
    {
        var result = await _service.UpdateLeaveStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取请假信息表(Leave)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:leave:import", "获取请假信息表(Leave)导入模板")]
    public async Task<IActionResult> GetLeaveTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetLeaveTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入请假信息表(Leave)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:leave:import", "导入请假信息表(Leave)")]
    public async Task<ActionResult<object>> ImportLeaveAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportLeaveAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出请假信息表(Leave)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:leave:export", "导出请假信息表(Leave)")]
    public async Task<IActionResult> ExportLeaveAsync([FromBody] TaktLeaveQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportLeaveAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
