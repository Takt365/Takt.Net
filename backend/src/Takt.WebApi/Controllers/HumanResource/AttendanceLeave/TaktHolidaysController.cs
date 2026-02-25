// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktHolidaysController.cs
// 功能描述：假日控制器，提供假日管理的 RESTful API（参照 TaktPostsController）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 假日控制器
/// </summary>
[Route("api/[controller]", Name = "假日")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
[TaktPermission("humanresource:attendanceleave:holiday", "假日管理")]
public class TaktHolidaysController : TaktControllerBase
{
    private readonly ITaktHolidayService _holidayService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktHolidaysController(
        ITaktHolidayService holidayService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _holidayService = holidayService;
    }

    /// <summary>
    /// 获取假日列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:holiday:list", "查询假日列表")]
    public async Task<ActionResult<TaktPagedResult<TaktHolidayDto>>> GetListAsync([FromQuery] TaktHolidayQueryDto queryDto)
    {
        var result = await _holidayService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取假日
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:query", "查询假日详情")]
    public async Task<ActionResult<TaktHolidayDto>> GetByIdAsync(long id)
    {
        var holiday = await _holidayService.GetByIdAsync(id);
        if (holiday == null)
            return NotFound();
        return Ok(holiday);
    }

    /// <summary>
    /// 获取假日选项列表（用于下拉框等）
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:holiday:list", "查询假日选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync([FromQuery] string? region = null)
    {
        var options = await _holidayService.GetOptionsAsync(region);
        return Ok(options);
    }

    /// <summary>
    /// 创建假日
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:holiday:create", "创建假日")]
    public async Task<ActionResult<TaktHolidayDto>> CreateAsync([FromBody] TaktHolidayCreateDto dto)
    {
        var holiday = await _holidayService.CreateAsync(dto);
        // 使用 Ok 而不是 CreatedAtAction，避免路由匹配问题（与 TaktFilesController 一致）
        return Ok(holiday);
    }

    /// <summary>
    /// 更新假日
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:update", "更新假日")]
    public async Task<ActionResult<TaktHolidayDto>> UpdateAsync(long id, [FromBody] TaktHolidayUpdateDto dto)
    {
        try
        {
            var holiday = await _holidayService.UpdateAsync(id, dto);
            return Ok(holiday);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除假日
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "删除假日")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _holidayService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除假日
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "批量删除假日")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _holidayService.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:holiday:template", "获取假日导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _holidayService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入假日
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:holiday:import", "导入假日")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _holidayService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出假日
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:holiday:export", "导出假日")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktHolidayQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _holidayService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取指定日期的假日主题色（用于前端根据假日动态显示主色调，支持未登录访问）
    /// </summary>
    [HttpGet("theme")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktHolidayDto>> GetHolidayThemeAsync([FromQuery] DateTime? date, [FromQuery] string? region = null)
    {
        var targetDate = date?.Date ?? DateTime.Now.Date;
        var dto = await _holidayService.GetHolidayThemeAsync(targetDate, region);
        if (dto == null)
            return NoContent();
        return Ok(dto);
    }
}
