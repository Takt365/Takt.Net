// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNoticesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工程变更通知单表控制器，提供EcNotice管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 工程变更通知单表控制器
/// </summary>
[Route("api/[controller]", Name = "工程变更通知单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:engineeringchange:ecnotice", "工程变更通知单表管理")]
public class TaktEcNoticesController : TaktControllerBase
{
    private readonly ITaktEcNoticeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcNoticesController(
        ITaktEcNoticeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工程变更通知单表(EcNotice)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:list", "查询工程变更通知单表(EcNotice)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEcNoticeDto>>> GetEcNoticeListAsync([FromQuery] TaktEcNoticeQueryDto queryDto)
    {
        var result = await _service.GetEcNoticeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工程变更通知单表(EcNotice)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:query", "查询工程变更通知单表(EcNotice)详情")]
    public async Task<ActionResult<TaktEcNoticeDto>> GetEcNoticeByIdAsync(long id)
    {
        var item = await _service.GetEcNoticeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工程变更通知单表(EcNotice)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:query", "查询工程变更通知单表(EcNotice)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEcNoticeOptionsAsync()
    {
        var result = await _service.GetEcNoticeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工程变更通知单表(EcNotice)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:create", "创建工程变更通知单表(EcNotice)")]
    public async Task<ActionResult<TaktEcNoticeDto>> CreateEcNoticeAsync([FromBody] TaktEcNoticeCreateDto dto)
    {
        var result = await _service.CreateEcNoticeAsync(dto);
        return CreatedAtAction(nameof(GetEcNoticeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工程变更通知单表(EcNotice)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:update", "更新工程变更通知单表(EcNotice)")]
    public async Task<ActionResult<TaktEcNoticeDto>> UpdateEcNoticeAsync(long id, [FromBody] TaktEcNoticeUpdateDto dto)
    {
        var result = await _service.UpdateEcNoticeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工程变更通知单表(EcNotice)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:delete", "删除工程变更通知单表(EcNotice)")]
    public async Task<ActionResult> DeleteEcNoticeByIdAsync(long id)
    {
        await _service.DeleteEcNoticeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工程变更通知单表(EcNotice)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:delete", "批量删除工程变更通知单表(EcNotice)")]
    public async Task<ActionResult> DeleteEcNoticeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEcNoticeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工程变更通知单表(EcNotice)Notice
    /// </summary>
    [HttpPut("status-notice")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:update", "更新工程变更通知单表(EcNotice)Notice")]
    public async Task<ActionResult<TaktEcNoticeDto>> UpdateEcNoticeNoticeStatusAsync([FromBody] TaktEcNoticeNoticeStatusDto dto)
    {
        var result = await _service.UpdateEcNoticeNoticeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工程变更通知单表(EcNotice)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:import", "获取工程变更通知单表(EcNotice)导入模板")]
    public async Task<IActionResult> GetEcNoticeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEcNoticeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工程变更通知单表(EcNotice)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:import", "导入工程变更通知单表(EcNotice)")]
    public async Task<ActionResult<object>> ImportEcNoticeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEcNoticeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工程变更通知单表(EcNotice)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecnotice:export", "导出工程变更通知单表(EcNotice)")]
    public async Task<IActionResult> ExportEcNoticeAsync([FromBody] TaktEcNoticeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEcNoticeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
