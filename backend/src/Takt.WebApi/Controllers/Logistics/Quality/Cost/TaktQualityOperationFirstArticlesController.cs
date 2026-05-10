// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationFirstArticlesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务初期定期检定费用明细表控制器，提供QualityOperationFirstArticle管理的RESTful API接口
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
/// 品质业务初期定期检定费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务初期定期检定费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationfirstarticle", "品质业务初期定期检定费用明细表管理")]
public class TaktQualityOperationFirstArticlesController : TaktControllerBase
{
    private readonly ITaktQualityOperationFirstArticleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationFirstArticlesController(
        ITaktQualityOperationFirstArticleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务初期定期检定费用明细表(QualityOperationFirstArticle)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:list", "查询品质业务初期定期检定费用明细表(QualityOperationFirstArticle)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationFirstArticleDto>>> GetQualityOperationFirstArticleListAsync([FromQuery] TaktQualityOperationFirstArticleQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationFirstArticleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:query", "查询品质业务初期定期检定费用明细表(QualityOperationFirstArticle)详情")]
    public async Task<ActionResult<TaktQualityOperationFirstArticleDto>> GetQualityOperationFirstArticleByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationFirstArticleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务初期定期检定费用明细表(QualityOperationFirstArticle)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:query", "查询品质业务初期定期检定费用明细表(QualityOperationFirstArticle)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationFirstArticleOptionsAsync()
    {
        var result = await _service.GetQualityOperationFirstArticleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:create", "创建品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<ActionResult<TaktQualityOperationFirstArticleDto>> CreateQualityOperationFirstArticleAsync([FromBody] TaktQualityOperationFirstArticleCreateDto dto)
    {
        var result = await _service.CreateQualityOperationFirstArticleAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationFirstArticleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:update", "更新品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<ActionResult<TaktQualityOperationFirstArticleDto>> UpdateQualityOperationFirstArticleAsync(long id, [FromBody] TaktQualityOperationFirstArticleUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationFirstArticleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:delete", "删除品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<ActionResult> DeleteQualityOperationFirstArticleByIdAsync(long id)
    {
        await _service.DeleteQualityOperationFirstArticleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:delete", "批量删除品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<ActionResult> DeleteQualityOperationFirstArticleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationFirstArticleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务初期定期检定费用明细表(QualityOperationFirstArticle)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:import", "获取品质业务初期定期检定费用明细表(QualityOperationFirstArticle)导入模板")]
    public async Task<IActionResult> GetQualityOperationFirstArticleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationFirstArticleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:import", "导入品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<ActionResult<object>> ImportQualityOperationFirstArticleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationFirstArticleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务初期定期检定费用明细表(QualityOperationFirstArticle)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationfirstarticle:export", "导出品质业务初期定期检定费用明细表(QualityOperationFirstArticle)")]
    public async Task<IActionResult> ExportQualityOperationFirstArticleAsync([FromBody] TaktQualityOperationFirstArticleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationFirstArticleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
