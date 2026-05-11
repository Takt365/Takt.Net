// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务主表控制器，提供QualityOperation管理的RESTful API接口
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
/// 品质业务主表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperation", "品质业务主表管理")]
public class TaktQualityOperationsController : TaktControllerBase
{
    private readonly ITaktQualityOperationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationsController(
        ITaktQualityOperationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperation:list", "查询品质业务主表(QualityOperation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationDto>>> GetQualityOperationListAsync([FromQuery] TaktQualityOperationQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务主表(QualityOperation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperation:query", "查询品质业务主表(QualityOperation)详情")]
    public async Task<ActionResult<TaktQualityOperationDto>> GetQualityOperationByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperation:query", "查询品质业务主表(QualityOperation)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationOptionsAsync()
    {
        var result = await _service.GetQualityOperationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务主表(QualityOperation)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperation:create", "创建品质业务主表(QualityOperation)")]
    public async Task<ActionResult<TaktQualityOperationDto>> CreateQualityOperationAsync([FromBody] TaktQualityOperationCreateDto dto)
    {
        var result = await _service.CreateQualityOperationAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务主表(QualityOperation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperation:update", "更新品质业务主表(QualityOperation)")]
    public async Task<ActionResult<TaktQualityOperationDto>> UpdateQualityOperationAsync(long id, [FromBody] TaktQualityOperationUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务主表(QualityOperation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperation:delete", "删除品质业务主表(QualityOperation)")]
    public async Task<ActionResult> DeleteQualityOperationByIdAsync(long id)
    {
        await _service.DeleteQualityOperationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务主表(QualityOperation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperation:delete", "批量删除品质业务主表(QualityOperation)")]
    public async Task<ActionResult> DeleteQualityOperationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务主表(QualityOperation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperation:import", "获取品质业务主表(QualityOperation)导入模板")]
    public async Task<IActionResult> GetQualityOperationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务主表(QualityOperation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperation:import", "导入品质业务主表(QualityOperation)")]
    public async Task<ActionResult<object>> ImportQualityOperationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务主表(QualityOperation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperation:export", "导出品质业务主表(QualityOperation)")]
    public async Task<IActionResult> ExportQualityOperationAsync([FromBody] TaktQualityOperationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
