// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质废弃主表控制器，提供QualityScrap管理的RESTful API接口
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
/// 品质废弃主表控制器
/// </summary>
[Route("api/[controller]", Name = "品质废弃主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityscrap", "品质废弃主表管理")]
public class TaktQualityScrapsController : TaktControllerBase
{
    private readonly ITaktQualityScrapService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapsController(
        ITaktQualityScrapService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityscrap:list", "查询品质废弃主表(QualityScrap)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityScrapDto>>> GetQualityScrapListAsync([FromQuery] TaktQualityScrapQueryDto queryDto)
    {
        var result = await _service.GetQualityScrapListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质废弃主表(QualityScrap)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrap:query", "查询品质废弃主表(QualityScrap)详情")]
    public async Task<ActionResult<TaktQualityScrapDto>> GetQualityScrapByIdAsync(long id)
    {
        var item = await _service.GetQualityScrapByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityscrap:query", "查询品质废弃主表(QualityScrap)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityScrapOptionsAsync()
    {
        var result = await _service.GetQualityScrapOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质废弃主表(QualityScrap)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityscrap:create", "创建品质废弃主表(QualityScrap)")]
    public async Task<ActionResult<TaktQualityScrapDto>> CreateQualityScrapAsync([FromBody] TaktQualityScrapCreateDto dto)
    {
        var result = await _service.CreateQualityScrapAsync(dto);
        return CreatedAtAction(nameof(GetQualityScrapByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质废弃主表(QualityScrap)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrap:update", "更新品质废弃主表(QualityScrap)")]
    public async Task<ActionResult<TaktQualityScrapDto>> UpdateQualityScrapAsync(long id, [FromBody] TaktQualityScrapUpdateDto dto)
    {
        var result = await _service.UpdateQualityScrapAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质废弃主表(QualityScrap)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrap:delete", "删除品质废弃主表(QualityScrap)")]
    public async Task<ActionResult> DeleteQualityScrapByIdAsync(long id)
    {
        await _service.DeleteQualityScrapByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质废弃主表(QualityScrap)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityscrap:delete", "批量删除品质废弃主表(QualityScrap)")]
    public async Task<ActionResult> DeleteQualityScrapBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityScrapBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质废弃主表(QualityScrap)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityscrap:import", "获取品质废弃主表(QualityScrap)导入模板")]
    public async Task<IActionResult> GetQualityScrapTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityScrapTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质废弃主表(QualityScrap)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityscrap:import", "导入品质废弃主表(QualityScrap)")]
    public async Task<ActionResult<object>> ImportQualityScrapAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityScrapAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质废弃主表(QualityScrap)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityscrap:export", "导出品质废弃主表(QualityScrap)")]
    public async Task<IActionResult> ExportQualityScrapAsync([FromBody] TaktQualityScrapQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityScrapAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
