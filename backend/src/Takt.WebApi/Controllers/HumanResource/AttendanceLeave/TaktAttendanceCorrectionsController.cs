// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡记录表控制器，提供AttendanceCorrection管理的RESTful API接口
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
/// 补卡记录表控制器
/// </summary>
[Route("api/[controller]", Name = "补卡记录表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:attendancecorrection", "补卡记录表管理")]
public class TaktAttendanceCorrectionsController : TaktControllerBase
{
    private readonly ITaktAttendanceCorrectionService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
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
    /// 获取补卡记录表(AttendanceCorrection)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:list", "查询补卡记录表(AttendanceCorrection)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAttendanceCorrectionDto>>> GetAttendanceCorrectionListAsync([FromQuery] TaktAttendanceCorrectionQueryDto queryDto)
    {
        var result = await _service.GetAttendanceCorrectionListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:query", "查询补卡记录表(AttendanceCorrection)详情")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> GetAttendanceCorrectionByIdAsync(long id)
    {
        var item = await _service.GetAttendanceCorrectionByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取补卡记录表(AttendanceCorrection)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:query", "查询补卡记录表(AttendanceCorrection)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAttendanceCorrectionOptionsAsync()
    {
        var result = await _service.GetAttendanceCorrectionOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:create", "创建补卡记录表(AttendanceCorrection)")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> CreateAttendanceCorrectionAsync([FromBody] TaktAttendanceCorrectionCreateDto dto)
    {
        var result = await _service.CreateAttendanceCorrectionAsync(dto);
        return CreatedAtAction(nameof(GetAttendanceCorrectionByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:update", "更新补卡记录表(AttendanceCorrection)")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> UpdateAttendanceCorrectionAsync(long id, [FromBody] TaktAttendanceCorrectionUpdateDto dto)
    {
        var result = await _service.UpdateAttendanceCorrectionAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:delete", "删除补卡记录表(AttendanceCorrection)")]
    public async Task<ActionResult> DeleteAttendanceCorrectionByIdAsync(long id)
    {
        await _service.DeleteAttendanceCorrectionByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:delete", "批量删除补卡记录表(AttendanceCorrection)")]
    public async Task<ActionResult> DeleteAttendanceCorrectionBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAttendanceCorrectionBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新补卡记录表(AttendanceCorrection)Approval
    /// </summary>
    [HttpPut("status-approval")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:update", "更新补卡记录表(AttendanceCorrection)Approval")]
    public async Task<ActionResult<TaktAttendanceCorrectionDto>> UpdateAttendanceCorrectionApprovalStatusAsync([FromBody] TaktAttendanceCorrectionApprovalStatusDto dto)
    {
        var result = await _service.UpdateAttendanceCorrectionApprovalStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取补卡记录表(AttendanceCorrection)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:import", "获取补卡记录表(AttendanceCorrection)导入模板")]
    public async Task<IActionResult> GetAttendanceCorrectionTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAttendanceCorrectionTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:import", "导入补卡记录表(AttendanceCorrection)")]
    public async Task<ActionResult<object>> ImportAttendanceCorrectionAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAttendanceCorrectionAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出补卡记录表(AttendanceCorrection)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:attendancecorrection:export", "导出补卡记录表(AttendanceCorrection)")]
    public async Task<IActionResult> ExportAttendanceCorrectionAsync([FromBody] TaktAttendanceCorrectionQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAttendanceCorrectionAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
