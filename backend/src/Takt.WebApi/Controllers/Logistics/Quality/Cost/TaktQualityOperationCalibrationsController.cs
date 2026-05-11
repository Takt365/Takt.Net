// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationCalibrationsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务设备校正费用明细表控制器，提供QualityOperationCalibration管理的RESTful API接口
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
/// 品质业务设备校正费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务设备校正费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationcalibration", "品质业务设备校正费用明细表管理")]
public class TaktQualityOperationCalibrationsController : TaktControllerBase
{
    private readonly ITaktQualityOperationCalibrationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationCalibrationsController(
        ITaktQualityOperationCalibrationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务设备校正费用明细表(QualityOperationCalibration)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:list", "查询品质业务设备校正费用明细表(QualityOperationCalibration)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationCalibrationDto>>> GetQualityOperationCalibrationListAsync([FromQuery] TaktQualityOperationCalibrationQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationCalibrationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:query", "查询品质业务设备校正费用明细表(QualityOperationCalibration)详情")]
    public async Task<ActionResult<TaktQualityOperationCalibrationDto>> GetQualityOperationCalibrationByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationCalibrationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务设备校正费用明细表(QualityOperationCalibration)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:query", "查询品质业务设备校正费用明细表(QualityOperationCalibration)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationCalibrationOptionsAsync()
    {
        var result = await _service.GetQualityOperationCalibrationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:create", "创建品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<ActionResult<TaktQualityOperationCalibrationDto>> CreateQualityOperationCalibrationAsync([FromBody] TaktQualityOperationCalibrationCreateDto dto)
    {
        var result = await _service.CreateQualityOperationCalibrationAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationCalibrationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:update", "更新品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<ActionResult<TaktQualityOperationCalibrationDto>> UpdateQualityOperationCalibrationAsync(long id, [FromBody] TaktQualityOperationCalibrationUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationCalibrationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:delete", "删除品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<ActionResult> DeleteQualityOperationCalibrationByIdAsync(long id)
    {
        await _service.DeleteQualityOperationCalibrationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:delete", "批量删除品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<ActionResult> DeleteQualityOperationCalibrationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationCalibrationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务设备校正费用明细表(QualityOperationCalibration)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:import", "获取品质业务设备校正费用明细表(QualityOperationCalibration)导入模板")]
    public async Task<IActionResult> GetQualityOperationCalibrationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationCalibrationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:import", "导入品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<ActionResult<object>> ImportQualityOperationCalibrationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationCalibrationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务设备校正费用明细表(QualityOperationCalibration)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationcalibration:export", "导出品质业务设备校正费用明细表(QualityOperationCalibration)")]
    public async Task<IActionResult> ExportQualityOperationCalibrationAsync([FromBody] TaktQualityOperationCalibrationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationCalibrationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
