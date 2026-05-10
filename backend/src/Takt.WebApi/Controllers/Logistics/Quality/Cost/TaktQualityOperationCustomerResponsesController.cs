// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationCustomerResponsesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务顾客品质要求对应费用明细表控制器，提供QualityOperationCustomerResponse管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Cost;

/// <summary>
/// 品质业务顾客品质要求对应费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务顾客品质要求对应费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse", "品质业务顾客品质要求对应费用明细表管理")]
public class TaktQualityOperationCustomerResponsesController : TaktControllerBase
{
    private readonly ITaktQualityOperationCustomerResponseService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCustomerResponsesController(
        ITaktQualityOperationCustomerResponseService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:list", "查询品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationCustomerResponseDto>>> GetQualityOperationCustomerResponseListAsync([FromQuery] TaktQualityOperationCustomerResponseQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationCustomerResponseListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:query", "查询品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)详情")]
    public async Task<ActionResult<TaktQualityOperationCustomerResponseDto>> GetQualityOperationCustomerResponseByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationCustomerResponseByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:query", "查询品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationCustomerResponseOptionsAsync()
    {
        var result = await _service.GetQualityOperationCustomerResponseOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:create", "创建品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<ActionResult<TaktQualityOperationCustomerResponseDto>> CreateQualityOperationCustomerResponseAsync([FromBody] TaktQualityOperationCustomerResponseCreateDto dto)
    {
        var result = await _service.CreateQualityOperationCustomerResponseAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationCustomerResponseByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:update", "更新品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<ActionResult<TaktQualityOperationCustomerResponseDto>> UpdateQualityOperationCustomerResponseAsync(long id, [FromBody] TaktQualityOperationCustomerResponseUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationCustomerResponseAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:delete", "删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<ActionResult> DeleteQualityOperationCustomerResponseByIdAsync(long id)
    {
        await _service.DeleteQualityOperationCustomerResponseByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:delete", "批量删除品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<ActionResult> DeleteQualityOperationCustomerResponseBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationCustomerResponseBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:import", "获取品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)导入模板")]
    public async Task<IActionResult> GetQualityOperationCustomerResponseTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationCustomerResponseTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:import", "导入品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<ActionResult<object>> ImportQualityOperationCustomerResponseAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationCustomerResponseAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationcustomerresponse:export", "导出品质业务顾客品质要求对应费用明细表(QualityOperationCustomerResponse)")]
    public async Task<IActionResult> ExportQualityOperationCustomerResponseAsync([FromBody] TaktQualityOperationCustomerResponseQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationCustomerResponseAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
