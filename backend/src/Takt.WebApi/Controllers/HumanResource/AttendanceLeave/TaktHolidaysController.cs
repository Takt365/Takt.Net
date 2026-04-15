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
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 假日控制器
/// </summary>
[Route("api/[controller]", Name = "假日")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
[TaktPermission("humanresource:attendanceleave:holiday:list", "假日管理")]
public class TaktHolidaysController : TaktControllerBase
{
    private readonly ITaktHolidayService _holidayService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="holidayService">假日应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
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
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:holiday:list", "查询假日列表")]
    public async Task<ActionResult<TaktPagedResult<TaktHolidayDto>>> GetHolidayListAsync([FromQuery] TaktHolidayQueryDto queryDto)
    {
        var result = await _holidayService.GetHolidayListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取假日
    /// </summary>
    /// <param name="id">假日主键</param>
    /// <returns>假日 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:detail", "查询假日详情")]
    public async Task<ActionResult<TaktHolidayDto>> GetHolidayByIdAsync(long id)
    {
        var holiday = await _holidayService.GetHolidayByIdAsync(id);
        if (holiday == null)
            return NotFound();
        return Ok(holiday);
    }

    /// <summary>
    /// 获取假日选项列表（用于下拉框等）
    /// </summary>
    /// <param name="region">地区（可选）</param>
    /// <returns>选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:holiday:list", "查询假日选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetHolidayOptionsAsync([FromQuery] string? region = null)
    {
        var options = await _holidayService.GetHolidayOptionsAsync(region);
        return Ok(options);
    }

    /// <summary>
    /// 创建假日
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的假日 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:holiday:create", "创建假日")]
    public async Task<ActionResult<TaktHolidayDto>> CreateHolidayAsync([FromBody] TaktHolidayCreateDto dto)
    {
        var holiday = await _holidayService.CreateHolidayAsync(dto);
        // 使用 Ok 而不是 CreatedAtAction，避免路由匹配问题（与 TaktFilesController 一致）
        return Ok(holiday);
    }

    /// <summary>
    /// 更新假日
    /// </summary>
    /// <param name="id">假日主键</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的假日 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:update", "更新假日")]
    public async Task<ActionResult<TaktHolidayDto>> UpdateHolidayAsync(long id, [FromBody] TaktHolidayUpdateDto dto)
    {
        try
        {
            var holiday = await _holidayService.UpdateHolidayAsync(id, dto);
            return Ok(holiday);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除假日
    /// </summary>
    /// <param name="id">假日主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "删除假日")]
    public async Task<IActionResult> DeleteHolidayByIdAsync(long id)
    {
        await _holidayService.DeleteHolidayByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除假日
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:holiday:delete", "批量删除假日")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _holidayService.DeleteHolidayBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:holiday:template", "获取假日导入模板")]
    public async Task<IActionResult> GetHolidayTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _holidayService.GetHolidayTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入假日
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:holiday:import", "导入假日")]
    public async Task<ActionResult<object>> ImportHolidayAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _holidayService.ImportHolidayAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出假日
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:holiday:export", "导出假日")]
    public async Task<IActionResult> ExportHolidayAsync([FromBody] TaktHolidayQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _holidayService.ExportHolidayAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取指定日期的假日主题色（用于前端根据假日动态显示主色调，支持未登录访问）
    /// </summary>
    /// <param name="date">日期（可选，默认当天）</param>
    /// <param name="region">地区（可选）</param>
    /// <returns>假日 DTO；无假日时无内容</returns>
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