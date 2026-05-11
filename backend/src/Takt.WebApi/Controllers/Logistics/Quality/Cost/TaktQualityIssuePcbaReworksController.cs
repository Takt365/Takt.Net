// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityIssuePcbaReworksController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题PCBA不良改修费用明细表控制器，提供QualityIssuePcbaRework管理的RESTful API接口
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
/// 质量问题PCBA不良改修费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "质量问题PCBA不良改修费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityissuepcbarework", "质量问题PCBA不良改修费用明细表管理")]
public class TaktQualityIssuePcbaReworksController : TaktControllerBase
{
    private readonly ITaktQualityIssuePcbaReworkService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuePcbaReworksController(
        ITaktQualityIssuePcbaReworkService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:list", "查询质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityIssuePcbaReworkDto>>> GetQualityIssuePcbaReworkListAsync([FromQuery] TaktQualityIssuePcbaReworkQueryDto queryDto)
    {
        var result = await _service.GetQualityIssuePcbaReworkListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:query", "查询质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)详情")]
    public async Task<ActionResult<TaktQualityIssuePcbaReworkDto>> GetQualityIssuePcbaReworkByIdAsync(long id)
    {
        var item = await _service.GetQualityIssuePcbaReworkByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:query", "查询质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityIssuePcbaReworkOptionsAsync()
    {
        var result = await _service.GetQualityIssuePcbaReworkOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:create", "创建质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<ActionResult<TaktQualityIssuePcbaReworkDto>> CreateQualityIssuePcbaReworkAsync([FromBody] TaktQualityIssuePcbaReworkCreateDto dto)
    {
        var result = await _service.CreateQualityIssuePcbaReworkAsync(dto);
        return CreatedAtAction(nameof(GetQualityIssuePcbaReworkByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:update", "更新质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<ActionResult<TaktQualityIssuePcbaReworkDto>> UpdateQualityIssuePcbaReworkAsync(long id, [FromBody] TaktQualityIssuePcbaReworkUpdateDto dto)
    {
        var result = await _service.UpdateQualityIssuePcbaReworkAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:delete", "删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<ActionResult> DeleteQualityIssuePcbaReworkByIdAsync(long id)
    {
        await _service.DeleteQualityIssuePcbaReworkByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:delete", "批量删除质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<ActionResult> DeleteQualityIssuePcbaReworkBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityIssuePcbaReworkBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:import", "获取质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)导入模板")]
    public async Task<IActionResult> GetQualityIssuePcbaReworkTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityIssuePcbaReworkTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:import", "导入质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<ActionResult<object>> ImportQualityIssuePcbaReworkAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityIssuePcbaReworkAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityissuepcbarework:export", "导出质量问题PCBA不良改修费用明细表(QualityIssuePcbaRework)")]
    public async Task<IActionResult> ExportQualityIssuePcbaReworkAsync([FromBody] TaktQualityIssuePcbaReworkQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityIssuePcbaReworkAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
