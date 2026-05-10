// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItemsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：加班明细表控制器，提供OvertimeItem管理的RESTful API接口
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
/// 加班明细表控制器
/// </summary>
[Route("api/[controller]", Name = "加班明细表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:overtimeitem", "加班明细表管理")]
public class TaktOvertimeItemsController : TaktControllerBase
{
    private readonly ITaktOvertimeItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktOvertimeItemsController(
        ITaktOvertimeItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取加班明细表(OvertimeItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:list", "查询加班明细表(OvertimeItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktOvertimeItemDto>>> GetOvertimeItemListAsync([FromQuery] TaktOvertimeItemQueryDto queryDto)
    {
        var result = await _service.GetOvertimeItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取加班明细表(OvertimeItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:query", "查询加班明细表(OvertimeItem)详情")]
    public async Task<ActionResult<TaktOvertimeItemDto>> GetOvertimeItemByIdAsync(long id)
    {
        var item = await _service.GetOvertimeItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取加班明细表(OvertimeItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:query", "查询加班明细表(OvertimeItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOvertimeItemOptionsAsync()
    {
        var result = await _service.GetOvertimeItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建加班明细表(OvertimeItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:create", "创建加班明细表(OvertimeItem)")]
    public async Task<ActionResult<TaktOvertimeItemDto>> CreateOvertimeItemAsync([FromBody] TaktOvertimeItemCreateDto dto)
    {
        var result = await _service.CreateOvertimeItemAsync(dto);
        return CreatedAtAction(nameof(GetOvertimeItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新加班明细表(OvertimeItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:update", "更新加班明细表(OvertimeItem)")]
    public async Task<ActionResult<TaktOvertimeItemDto>> UpdateOvertimeItemAsync(long id, [FromBody] TaktOvertimeItemUpdateDto dto)
    {
        var result = await _service.UpdateOvertimeItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除加班明细表(OvertimeItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:delete", "删除加班明细表(OvertimeItem)")]
    public async Task<ActionResult> DeleteOvertimeItemByIdAsync(long id)
    {
        await _service.DeleteOvertimeItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除加班明细表(OvertimeItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:delete", "批量删除加班明细表(OvertimeItem)")]
    public async Task<ActionResult> DeleteOvertimeItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteOvertimeItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取加班明细表(OvertimeItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:import", "获取加班明细表(OvertimeItem)导入模板")]
    public async Task<IActionResult> GetOvertimeItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetOvertimeItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入加班明细表(OvertimeItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:import", "导入加班明细表(OvertimeItem)")]
    public async Task<ActionResult<object>> ImportOvertimeItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportOvertimeItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出加班明细表(OvertimeItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:attendanceleave:overtimeitem:export", "导出加班明细表(OvertimeItem)")]
    public async Task<IActionResult> ExportOvertimeItemAsync([FromBody] TaktOvertimeItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportOvertimeItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
