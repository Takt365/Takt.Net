// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查项目明细表控制器，提供CustomerSatisfactionSurveyItem管理的RESTful API接口
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
/// 客户满意度调查项目明细表控制器
/// </summary>
[Route("api/[controller]", Name = "客户满意度调查项目明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem", "客户满意度调查项目明细表管理")]
public class TaktCustomerSatisfactionSurveyItemsController : TaktControllerBase
{
    private readonly ITaktCustomerSatisfactionSurveyItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemsController(
        ITaktCustomerSatisfactionSurveyItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:list", "查询客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCustomerSatisfactionSurveyItemDto>>> GetCustomerSatisfactionSurveyItemListAsync([FromQuery] TaktCustomerSatisfactionSurveyItemQueryDto queryDto)
    {
        var result = await _service.GetCustomerSatisfactionSurveyItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:query", "查询客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)详情")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyItemDto>> GetCustomerSatisfactionSurveyItemByIdAsync(long id)
    {
        var item = await _service.GetCustomerSatisfactionSurveyItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:query", "查询客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCustomerSatisfactionSurveyItemOptionsAsync()
    {
        var result = await _service.GetCustomerSatisfactionSurveyItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:create", "创建客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyItemDto>> CreateCustomerSatisfactionSurveyItemAsync([FromBody] TaktCustomerSatisfactionSurveyItemCreateDto dto)
    {
        var result = await _service.CreateCustomerSatisfactionSurveyItemAsync(dto);
        return CreatedAtAction(nameof(GetCustomerSatisfactionSurveyItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:update", "更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyItemDto>> UpdateCustomerSatisfactionSurveyItemAsync(long id, [FromBody] TaktCustomerSatisfactionSurveyItemUpdateDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveyItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:delete", "删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<ActionResult> DeleteCustomerSatisfactionSurveyItemByIdAsync(long id)
    {
        await _service.DeleteCustomerSatisfactionSurveyItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:delete", "批量删除客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<ActionResult> DeleteCustomerSatisfactionSurveyItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCustomerSatisfactionSurveyItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)FollowUp
    /// </summary>
    [HttpPut("status-followup")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:update", "更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)FollowUp")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyItemDto>> UpdateCustomerSatisfactionSurveyItemFollowUpStatusAsync([FromBody] TaktCustomerSatisfactionSurveyItemFollowUpStatusDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveyItemFollowUpStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:update", "更新客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)排序")]
    public async Task<ActionResult<TaktCustomerSatisfactionSurveyItemDto>> UpdateCustomerSatisfactionSurveyItemSortAsync([FromBody] TaktCustomerSatisfactionSurveyItemSortDto dto)
    {
        var result = await _service.UpdateCustomerSatisfactionSurveyItemSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:import", "获取客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)导入模板")]
    public async Task<IActionResult> GetCustomerSatisfactionSurveyItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCustomerSatisfactionSurveyItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:import", "导入客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<ActionResult<object>> ImportCustomerSatisfactionSurveyItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCustomerSatisfactionSurveyItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:customersatisfactionsurveyitem:export", "导出客户满意度调查项目明细表(CustomerSatisfactionSurveyItem)")]
    public async Task<IActionResult> ExportCustomerSatisfactionSurveyItemAsync([FromBody] TaktCustomerSatisfactionSurveyItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCustomerSatisfactionSurveyItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
