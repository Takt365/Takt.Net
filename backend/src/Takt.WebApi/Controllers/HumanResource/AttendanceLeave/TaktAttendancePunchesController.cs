// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchesController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：打卡记录 REST API（权限码与菜单「打卡签到」一致：clockin；与 TaktHolidaysController 结构一致）。
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
/// 打卡记录控制器，提供员工打卡数据的 RESTful API。
/// </summary>
[Route("api/[controller]", Name = "打卡")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:clockin:list", "打卡签到")]
public class TaktAttendancePunchesController : TaktControllerBase
{
    private readonly ITaktAttendancePunchService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="service">打卡应用服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendancePunchesController(
        ITaktAttendancePunchService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取打卡记录列表（分页）
    /// </summary>
    /// <param name="queryDto">分页与查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:clockin:list", "查询打卡列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendancePunchDto>>> GetListAsync([FromQuery] TaktAttendancePunchQueryDto queryDto)
    {
        return Ok(await _service.GetAttendancePunchListAsync(queryDto));
    }

    /// <summary>
    /// 根据 ID 获取打卡记录详情
    /// </summary>
    /// <param name="id">打卡主键</param>
    /// <returns>打卡 DTO；不存在时返回 404</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:clockin:detail", "查询打卡详情")]
    public async Task<ActionResult<TaktAttendancePunchDto>> GetByIdAsync(long id)
    {
        var dto = await _service.GetAttendancePunchByIdAsync(id);
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// 创建打卡记录
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>创建后的打卡 DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:clockin:create", "创建打卡")]
    public async Task<ActionResult<TaktAttendancePunchDto>> CreateAsync([FromBody] TaktAttendancePunchCreateDto dto)
    {
        return Ok(await _service.CreateAttendancePunchAsync(dto));
    }

    /// <summary>
    /// 更新打卡记录
    /// </summary>
    /// <param name="id">路由中的打卡主键，须与 dto.PunchId 一致</param>
    /// <param name="dto">更新 DTO</param>
    /// <returns>更新后的打卡 DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:clockin:update", "更新打卡")]
    public async Task<ActionResult<TaktAttendancePunchDto>> UpdateAsync(long id, [FromBody] TaktAttendancePunchUpdateDto dto)
    {
        if (dto.AttendancePunchId != id)
            return BadRequest(GetLocalizedString("validation.idRouteMismatch", "Frontend"));
        try
        {
            return Ok(await _service.UpdateAttendancePunchAsync(id, dto));
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除指定打卡记录
    /// </summary>
    /// <param name="id">打卡主键</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:clockin:delete", "删除打卡")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteAttendancePunchByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除打卡记录
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>无内容表示成功</returns>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:clockin:delete", "批量删除打卡")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] long[] ids)
    {
        await _service.DeleteAttendancePunchBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 获取打卡 Excel 导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:clockin:template", "获取打卡导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.GetAttendancePunchTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 从 Excel 导入打卡记录
    /// </summary>
    /// <param name="file">Excel 文件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <returns>成功数、失败数、错误信息列表</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:clockin:import", "导入打卡")]
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
            var (success, fail, errors) = await _service.ImportAttendancePunchAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 按条件导出打卡记录为 Excel
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>Excel 或 zip（与 <c>TaktExcelHelper.ExportAsync</c> 行数上限策略一致）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:clockin:export", "导出打卡")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktAttendancePunchQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAttendancePunchAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
