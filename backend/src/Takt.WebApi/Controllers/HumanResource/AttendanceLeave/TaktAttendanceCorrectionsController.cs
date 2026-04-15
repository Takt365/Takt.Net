// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionsController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡管理 REST API（与 TaktHolidaysController 结构一致）。
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
/// 补卡管理控制器，提供补卡记录的 RESTful API。
/// </summary>
[Route("api/[controller]", Name = "补卡")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
[TaktPermission("humanresource:attendanceleave:attendancecorrection:list", "补卡管理")]
public class TaktAttendanceCorrectionsController : TaktControllerBase
{
    private readonly ITaktAttendanceCorrectionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">补卡应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceCorrectionsController(
        ITaktAttendanceCorrectionService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取补卡记录列表（分页）
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:list", "查询补卡列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceCorrectionDto>>> GetListAsync([FromQuery] TaktAttendanceCorrectionQueryDto queryDto)
    {
        return Ok(await _service.GetAttendanceCorrectionListAsync(queryDto));
    }

    /// <summary>
    /// 根据 ID 获取补卡记录详情
    /// </summary>
    /// <param name="id">补卡主键</param>
    /// <returns>补卡 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:detail", "查询补卡详情")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> GetByIdAsync(long id)
    {
        var dto = await _service.GetAttendanceCorrectionByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建补卡记录
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的补卡 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:create", "创建补卡")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> CreateAsync([FromBody] TaktAttendanceCorrectionCreateDto dto)
    {
        return Ok(await _service.CreateAttendanceCorrectionAsync(dto));
    }

    /// <summary>
    /// 更新补卡记录
    /// </summary>
    /// <param name="id">路由中的补卡主键，须与 dto.CorrectionId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的补卡 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:update", "更新补卡")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> UpdateAsync(long id, [FromBody] TaktAttendanceCorrectionUpdateDto dto)
    {
        if (dto.CorrectionId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        try
        {
            return Ok(await _service.UpdateAttendanceCorrectionAsync(id, dto));
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除指定补卡记录
    /// </summary>
    /// <param name="id">补卡主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:delete", "删除补卡")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteAttendanceCorrectionByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除补卡记录
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:delete", "批量删除补卡")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteAttendanceCorrectionBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取补卡 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:template", "获取补卡导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetAttendanceCorrectionTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 从 Excel 导入补卡记录
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:import", "导入补卡")]
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
            var (success, fail, errors) = await _service.ImportAttendanceCorrectionAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 按条件导出补卡记录为 Excel
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 或 zip（与 <c>TaktExcelHelper.ExportAsync</c> 行数上限策略一致）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:export", "导出补卡")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktAttendanceCorrectionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAttendanceCorrectionAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
