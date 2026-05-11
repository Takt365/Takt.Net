// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：APS排程明细表控制器，提供ApsScheduleItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Application.Services.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程明细表控制器
/// </summary>
[Route("api/[controller]", Name = "APS排程明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem", "APS排程明细表管理")]
public class TaktApsScheduleItemsController : TaktControllerBase
{
    private readonly ITaktApsScheduleItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemsController(
        ITaktApsScheduleItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取APS排程明细表(ApsScheduleItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:list", "查询APS排程明细表(ApsScheduleItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktApsScheduleItemDto>>> GetApsScheduleItemListAsync([FromQuery] TaktApsScheduleItemQueryDto queryDto)
    {
        var result = await _service.GetApsScheduleItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:query", "查询APS排程明细表(ApsScheduleItem)详情")]
    public async Task<ActionResult<TaktApsScheduleItemDto>> GetApsScheduleItemByIdAsync(long id)
    {
        var item = await _service.GetApsScheduleItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取APS排程明细表(ApsScheduleItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:query", "查询APS排程明细表(ApsScheduleItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetApsScheduleItemOptionsAsync()
    {
        var result = await _service.GetApsScheduleItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:create", "创建APS排程明细表(ApsScheduleItem)")]
    public async Task<ActionResult<TaktApsScheduleItemDto>> CreateApsScheduleItemAsync([FromBody] TaktApsScheduleItemCreateDto dto)
    {
        var result = await _service.CreateApsScheduleItemAsync(dto);
        return CreatedAtAction(nameof(GetApsScheduleItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:update", "更新APS排程明细表(ApsScheduleItem)")]
    public async Task<ActionResult<TaktApsScheduleItemDto>> UpdateApsScheduleItemAsync(long id, [FromBody] TaktApsScheduleItemUpdateDto dto)
    {
        var result = await _service.UpdateApsScheduleItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:delete", "删除APS排程明细表(ApsScheduleItem)")]
    public async Task<ActionResult> DeleteApsScheduleItemByIdAsync(long id)
    {
        await _service.DeleteApsScheduleItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:delete", "批量删除APS排程明细表(ApsScheduleItem)")]
    public async Task<ActionResult> DeleteApsScheduleItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteApsScheduleItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新APS排程明细表(ApsScheduleItem)Process
    /// </summary>
    [HttpPut("status-process")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:update", "更新APS排程明细表(ApsScheduleItem)Process")]
    public async Task<ActionResult<TaktApsScheduleItemDto>> UpdateApsScheduleItemProcessStatusAsync([FromBody] TaktApsScheduleItemProcessStatusDto dto)
    {
        var result = await _service.UpdateApsScheduleItemProcessStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取APS排程明细表(ApsScheduleItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:import", "获取APS排程明细表(ApsScheduleItem)导入模板")]
    public async Task<IActionResult> GetApsScheduleItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetApsScheduleItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:import", "导入APS排程明细表(ApsScheduleItem)")]
    public async Task<ActionResult<object>> ImportApsScheduleItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportApsScheduleItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出APS排程明细表(ApsScheduleItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:scheduling:apsscheduleitem:export", "导出APS排程明细表(ApsScheduleItem)")]
    public async Task<IActionResult> ExportApsScheduleItemAsync([FromBody] TaktApsScheduleItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportApsScheduleItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
