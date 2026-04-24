// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimesController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：加班 REST API（权限码与菜单「加班管理」一致：overtime；与 TaktHolidaysController 结构一致）。
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
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 加班控制器，提供加班申请与记录的 RESTful API。
/// </summary>
[Route("api/[controller]", Name = "加班")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:overtime:list", "加班管理")]
public class TaktOvertimesController : TaktControllerBase
{
    private readonly ITaktOvertimeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">加班应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
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
    /// 获取加班记录列表（分页）
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:overtime:list", "查询加班列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOvertimeDto>>> GetListAsync([FromQuery] TaktOvertimeQueryDto queryDto)
    {
        return Ok(await _service.GetOvertimeListAsync(queryDto));
    }

    /// <summary>
    /// 根据 ID 获取加班记录详情
    /// </summary>
    /// <param name="id">加班主键</param>
    /// <returns>加班 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:detail", "查询加班详情")]
    public async Task<ActionResult<TaktOvertimeDto>> GetByIdAsync(long id)
    {
        var dto = await _service.GetOvertimeByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建加班记录
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的加班 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:overtime:create", "创建加班")]
    public async Task<ActionResult<TaktOvertimeDto>> CreateAsync([FromBody] TaktOvertimeCreateDto dto)
    {
        return Ok(await _service.CreateOvertimeAsync(dto));
    }

    /// <summary>
    /// 更新加班记录
    /// </summary>
    /// <param name="id">路由中的加班主键，须与 dto.OvertimeId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的加班 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:update", "更新加班")]
    public async Task<ActionResult<TaktOvertimeDto>> UpdateAsync(long id, [FromBody] TaktOvertimeUpdateDto dto)
    {
        if (dto.OvertimeId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        try
        {
            return Ok(await _service.UpdateOvertimeAsync(id, dto));
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除指定加班记录
    /// </summary>
    /// <param name="id">加班主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtime:delete", "删除加班")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteOvertimeByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除加班记录
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:overtime:delete", "批量删除加班")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteOvertimeBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取加班 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:overtime:template", "获取加班导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetOvertimeTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 从 Excel 导入加班记录
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:overtime:import", "导入加班")]
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
            var (success, fail, errors) = await _service.ImportOvertimeAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 按条件导出加班记录为 Excel
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 或 zip（与 <c>TaktExcelHelper.ExportAsync</c> 行数上限策略一致）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:overtime:export", "导出加班")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktOvertimeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportOvertimeAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    #region 统计分析

    /// <summary>
    /// 按加班类型统计昨天的加班总数（小时数）
    /// </summary>
    /// <returns>加班类型统计</returns>
    [HttpGet("stats/yesterday-by-type")]
    [TaktPermission("humanresource:attendanceleave:overtime:list", "统计昨天加班")]
    public async Task<ActionResult<Dictionary<int, decimal>>> GetYesterdayOvertimeHoursByTypeAsync()
    {
        var stats = await _service.GetYesterdayOvertimeHoursByTypeAsync();
        return Ok(stats);
    }

    #endregion
}
