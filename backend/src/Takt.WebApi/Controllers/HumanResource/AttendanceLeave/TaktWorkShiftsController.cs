// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftsController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：班次定义 REST API（与 TaktHolidaysController 结构一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 班次控制器，提供排班用班次定义的 RESTful API。
/// </summary>
[Route("api/[controller]", Name = "班次")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
[TaktPermission("humanresource:attendanceleave:workshift:list", "班次管理")]
public class TaktWorkShiftsController : TaktControllerBase
{
    private readonly ITaktWorkShiftService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">班次应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
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
    /// 获取班次列表（分页）
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:workshift:list", "查询班次列表")]
    public async Task<ActionResult<TaktPagedResult<TaktWorkShiftDto>>> GetListAsync([FromQuery] TaktWorkShiftQueryDto queryDto)
    {
        return Ok(await _service.GetWorkShiftListAsync(queryDto));
    }

    /// <summary>
    /// 根据 ID 获取班次详情
    /// </summary>
    /// <param name="id">班次主键</param>
    /// <returns>班次 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:detail", "查询班次详情")]
    public async Task<ActionResult<TaktWorkShiftDto>> GetByIdAsync(long id)
    {
        var dto = await _service.GetWorkShiftByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 获取班次下拉选项（用于排班等场景）
    /// </summary>
    /// <returns>选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:workshift:list", "班次下拉选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        return Ok(await _service.GetWorkShiftOptionsAsync());
    }

    /// <summary>
    /// 创建班次
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的班次 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:workshift:create", "创建班次")]
    public async Task<ActionResult<TaktWorkShiftDto>> CreateAsync([FromBody] TaktWorkShiftCreateDto dto)
    {
        return Ok(await _service.CreateWorkShiftAsync(dto));
    }

    /// <summary>
    /// 更新班次
    /// </summary>
    /// <param name="id">路由中的班次主键，须与 dto.ShiftId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的班次 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:update", "更新班次")]
    public async Task<ActionResult<TaktWorkShiftDto>> UpdateAsync(long id, [FromBody] TaktWorkShiftUpdateDto dto)
    {
        if (dto.ShiftId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        try
        {
            return Ok(await _service.UpdateWorkShiftAsync(id, dto));
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除指定班次
    /// </summary>
    /// <param name="id">班次主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:workshift:delete", "删除班次")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteWorkShiftByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除班次
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:workshift:delete", "批量删除班次")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteWorkShiftBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取班次 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:workshift:template", "获取班次导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetWorkShiftTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 从 Excel 导入班次
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:workshift:import", "导入班次")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _service.ImportWorkShiftAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 按条件导出班次为 Excel
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 或 zip（与 <c>TaktExcelHelper.ExportAsync</c> 行数上限策略一致）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:workshift:export", "导出班次")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktWorkShiftQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportWorkShiftAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
