// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉明细表控制器，提供CustomerComplaintItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Application.Services.Logistics.Quality.Complaint;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Complaint;

/// <summary>
/// 客诉明细表控制器
/// </summary>
[Route("api/[controller]", Name = "客诉明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:customercomplaintitem", "客诉明细表管理")]
public class TaktCustomerComplaintItemsController : TaktControllerBase
{
    private readonly ITaktCustomerComplaintItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerComplaintItemsController(
        ITaktCustomerComplaintItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取客诉明细表(CustomerComplaintItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:list", "查询客诉明细表(CustomerComplaintItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerComplaintItemDto>>> GetCustomerComplaintItemListAsync([FromQuery] TaktCustomerComplaintItemQueryDto queryDto)
    {
        var result = await _service.GetCustomerComplaintItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:query", "查询客诉明细表(CustomerComplaintItem)详情")]
    public async Task<ActionResult<TaktCustomerComplaintItemDto>> GetCustomerComplaintItemByIdAsync(long id)
    {
        var item = await _service.GetCustomerComplaintItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取客诉明细表(CustomerComplaintItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:query", "查询客诉明细表(CustomerComplaintItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCustomerComplaintItemOptionsAsync()
    {
        var result = await _service.GetCustomerComplaintItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:create", "创建客诉明细表(CustomerComplaintItem)")]
    public async Task<ActionResult<TaktCustomerComplaintItemDto>> CreateCustomerComplaintItemAsync([FromBody] TaktCustomerComplaintItemCreateDto dto)
    {
        var result = await _service.CreateCustomerComplaintItemAsync(dto);
        return CreatedAtAction(nameof(GetCustomerComplaintItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:update", "更新客诉明细表(CustomerComplaintItem)")]
    public async Task<ActionResult<TaktCustomerComplaintItemDto>> UpdateCustomerComplaintItemAsync(long id, [FromBody] TaktCustomerComplaintItemUpdateDto dto)
    {
        var result = await _service.UpdateCustomerComplaintItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:delete", "删除客诉明细表(CustomerComplaintItem)")]
    public async Task<ActionResult> DeleteCustomerComplaintItemByIdAsync(long id)
    {
        await _service.DeleteCustomerComplaintItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:delete", "批量删除客诉明细表(CustomerComplaintItem)")]
    public async Task<ActionResult> DeleteCustomerComplaintItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCustomerComplaintItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新客诉明细表(CustomerComplaintItem)Improvement
    /// </summary>
    [HttpPut("status-improvement")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:update", "更新客诉明细表(CustomerComplaintItem)Improvement")]
    public async Task<ActionResult<TaktCustomerComplaintItemDto>> UpdateCustomerComplaintItemImprovementStatusAsync([FromBody] TaktCustomerComplaintItemImprovementStatusDto dto)
    {
        var result = await _service.UpdateCustomerComplaintItemImprovementStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新客诉明细表(CustomerComplaintItem)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:update", "更新客诉明细表(CustomerComplaintItem)排序")]
    public async Task<ActionResult<TaktCustomerComplaintItemDto>> UpdateCustomerComplaintItemSortAsync([FromBody] TaktCustomerComplaintItemSortDto dto)
    {
        var result = await _service.UpdateCustomerComplaintItemSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取客诉明细表(CustomerComplaintItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:import", "获取客诉明细表(CustomerComplaintItem)导入模板")]
    public async Task<IActionResult> GetCustomerComplaintItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCustomerComplaintItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:import", "导入客诉明细表(CustomerComplaintItem)")]
    public async Task<ActionResult<object>> ImportCustomerComplaintItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCustomerComplaintItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出客诉明细表(CustomerComplaintItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:customercomplaintitem:export", "导出客诉明细表(CustomerComplaintItem)")]
    public async Task<IActionResult> ExportCustomerComplaintItemAsync([FromBody] TaktCustomerComplaintItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCustomerComplaintItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
