// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationIncomingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务来料检验费用明细表控制器，提供QualityOperationIncoming管理的RESTful API接口
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
/// 品质业务来料检验费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务来料检验费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationincoming", "品质业务来料检验费用明细表管理")]
public class TaktQualityOperationIncomingsController : TaktControllerBase
{
    private readonly ITaktQualityOperationIncomingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationIncomingsController(
        ITaktQualityOperationIncomingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务来料检验费用明细表(QualityOperationIncoming)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:list", "查询品质业务来料检验费用明细表(QualityOperationIncoming)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationIncomingDto>>> GetQualityOperationIncomingListAsync([FromQuery] TaktQualityOperationIncomingQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationIncomingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:query", "查询品质业务来料检验费用明细表(QualityOperationIncoming)详情")]
    public async Task<ActionResult<TaktQualityOperationIncomingDto>> GetQualityOperationIncomingByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationIncomingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务来料检验费用明细表(QualityOperationIncoming)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:query", "查询品质业务来料检验费用明细表(QualityOperationIncoming)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationIncomingOptionsAsync()
    {
        var result = await _service.GetQualityOperationIncomingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:create", "创建品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<ActionResult<TaktQualityOperationIncomingDto>> CreateQualityOperationIncomingAsync([FromBody] TaktQualityOperationIncomingCreateDto dto)
    {
        var result = await _service.CreateQualityOperationIncomingAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationIncomingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:update", "更新品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<ActionResult<TaktQualityOperationIncomingDto>> UpdateQualityOperationIncomingAsync(long id, [FromBody] TaktQualityOperationIncomingUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationIncomingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:delete", "删除品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<ActionResult> DeleteQualityOperationIncomingByIdAsync(long id)
    {
        await _service.DeleteQualityOperationIncomingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:delete", "批量删除品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<ActionResult> DeleteQualityOperationIncomingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationIncomingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务来料检验费用明细表(QualityOperationIncoming)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:import", "获取品质业务来料检验费用明细表(QualityOperationIncoming)导入模板")]
    public async Task<IActionResult> GetQualityOperationIncomingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationIncomingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:import", "导入品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<ActionResult<object>> ImportQualityOperationIncomingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationIncomingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务来料检验费用明细表(QualityOperationIncoming)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationincoming:export", "导出品质业务来料检验费用明细表(QualityOperationIncoming)")]
    public async Task<IActionResult> ExportQualityOperationIncomingAsync([FromBody] TaktQualityOperationIncomingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationIncomingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
