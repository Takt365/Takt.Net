// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityIssuesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质问题应对主表控制器，提供QualityIssue管理的RESTful API接口
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
/// 品质问题应对主表控制器
/// </summary>
[Route("api/[controller]", Name = "品质问题应对主表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityissue", "品质问题应对主表管理")]
public class TaktQualityIssuesController : TaktControllerBase
{
    private readonly ITaktQualityIssueService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssuesController(
        ITaktQualityIssueService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityissue:list", "查询品质问题应对主表(QualityIssue)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityIssueDto>>> GetQualityIssueListAsync([FromQuery] TaktQualityIssueQueryDto queryDto)
    {
        var result = await _service.GetQualityIssueListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissue:query", "查询品质问题应对主表(QualityIssue)详情")]
    public async Task<ActionResult<TaktQualityIssueDto>> GetQualityIssueByIdAsync(long id)
    {
        var item = await _service.GetQualityIssueByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityissue:query", "查询品质问题应对主表(QualityIssue)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityIssueOptionsAsync()
    {
        var result = await _service.GetQualityIssueOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityissue:create", "创建品质问题应对主表(QualityIssue)")]
    public async Task<ActionResult<TaktQualityIssueDto>> CreateQualityIssueAsync([FromBody] TaktQualityIssueCreateDto dto)
    {
        var result = await _service.CreateQualityIssueAsync(dto);
        return CreatedAtAction(nameof(GetQualityIssueByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissue:update", "更新品质问题应对主表(QualityIssue)")]
    public async Task<ActionResult<TaktQualityIssueDto>> UpdateQualityIssueAsync(long id, [FromBody] TaktQualityIssueUpdateDto dto)
    {
        var result = await _service.UpdateQualityIssueAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissue:delete", "删除品质问题应对主表(QualityIssue)")]
    public async Task<ActionResult> DeleteQualityIssueByIdAsync(long id)
    {
        await _service.DeleteQualityIssueByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityissue:delete", "批量删除品质问题应对主表(QualityIssue)")]
    public async Task<ActionResult> DeleteQualityIssueBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityIssueBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质问题应对主表(QualityIssue)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityissue:import", "获取品质问题应对主表(QualityIssue)导入模板")]
    public async Task<IActionResult> GetQualityIssueTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityIssueTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityissue:import", "导入品质问题应对主表(QualityIssue)")]
    public async Task<ActionResult<object>> ImportQualityIssueAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityIssueAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质问题应对主表(QualityIssue)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityissue:export", "导出品质问题应对主表(QualityIssue)")]
    public async Task<IActionResult> ExportQualityIssueAsync([FromBody] TaktQualityIssueQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityIssueAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
