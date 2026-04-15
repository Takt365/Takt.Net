// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourcesController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤源记录 REST API（与 TaktHolidaysController 结构一致）。
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
/// 考勤源记录控制器，提供设备下载的原始打卡行的 RESTful API。
/// </summary>
[Route("api/[controller]", Name = "考勤源记录")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
[TaktPermission("humanresource:attendanceleave:attendancesource:list", "考勤源记录")]
public class TaktAttendanceSourcesController : TaktControllerBase
{
    private readonly ITaktAttendanceSourceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">考勤源记录应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceSourcesController(
        ITaktAttendanceSourceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取考勤源记录列表（分页）
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:list", "查询源记录列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceSourceDto>>> GetListAsync([FromQuery] TaktAttendanceSourceQueryDto queryDto)
    {
        return Ok(await _service.GetAttendanceSourceListAsync(queryDto));
    }

    /// <summary>
    /// 根据 ID 获取考勤源记录详情
    /// </summary>
    /// <param name="id">源记录主键</param>
    /// <returns>源记录 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:detail", "查询源记录详情")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> GetByIdAsync(long id)
    {
        var dto = await _service.GetAttendanceSourceByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建考勤源记录
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的源记录 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancesource:create", "创建源记录")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> CreateAsync([FromBody] TaktAttendanceSourceCreateDto dto)
    {
        return Ok(await _service.CreateAttendanceSourceAsync(dto));
    }

    /// <summary>
    /// 更新考勤源记录
    /// </summary>
    /// <param name="id">路由中的源记录主键，须与 dto.SourceId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的源记录 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:update", "更新源记录")]
    public async Task<ActionResult<TaktAttendanceSourceDto>> UpdateAsync(long id, [FromBody] TaktAttendanceSourceUpdateDto dto)
    {
        if (dto.SourceId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        try
        {
            return Ok(await _service.UpdateAttendanceSourceAsync(id, dto));
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除指定考勤源记录
    /// </summary>
    /// <param name="id">源记录主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:delete", "删除源记录")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteAttendanceSourceByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除考勤源记录
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:delete", "批量删除源记录")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteAttendanceSourceBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取考勤源记录 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:template", "获取源记录导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetAttendanceSourceTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 从 Excel 导入考勤源记录
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:import", "导入源记录")]
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
            var (success, fail, errors) = await _service.ImportAttendanceSourceAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 按条件导出考勤源记录为 Excel
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 或 zip（与 <c>TaktExcelHelper.ExportAsync</c> 行数上限策略一致）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:export", "导出源记录")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktAttendanceSourceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAttendanceSourceAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
