// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueAssyReworksController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题组装不良改修费用明细表控制器，提供QualityIssueAssyRework管理的RESTful API接口
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
/// 质量问题组装不良改修费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "质量问题组装不良改修费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityissueassyrework", "质量问题组装不良改修费用明细表管理")]
public class TaktQualityIssueAssyReworksController : TaktControllerBase
{
    private readonly ITaktQualityIssueAssyReworkService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueAssyReworksController(
        ITaktQualityIssueAssyReworkService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:list", "查询质量问题组装不良改修费用明细表(QualityIssueAssyRework)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityIssueAssyReworkDto>>> GetQualityIssueAssyReworkListAsync([FromQuery] TaktQualityIssueAssyReworkQueryDto queryDto)
    {
        var result = await _service.GetQualityIssueAssyReworkListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:query", "查询质量问题组装不良改修费用明细表(QualityIssueAssyRework)详情")]
    public async Task<ActionResult<TaktQualityIssueAssyReworkDto>> GetQualityIssueAssyReworkByIdAsync(long id)
    {
        var item = await _service.GetQualityIssueAssyReworkByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:query", "查询质量问题组装不良改修费用明细表(QualityIssueAssyRework)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityIssueAssyReworkOptionsAsync()
    {
        var result = await _service.GetQualityIssueAssyReworkOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:create", "创建质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<ActionResult<TaktQualityIssueAssyReworkDto>> CreateQualityIssueAssyReworkAsync([FromBody] TaktQualityIssueAssyReworkCreateDto dto)
    {
        var result = await _service.CreateQualityIssueAssyReworkAsync(dto);
        return CreatedAtAction(nameof(GetQualityIssueAssyReworkByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:update", "更新质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<ActionResult<TaktQualityIssueAssyReworkDto>> UpdateQualityIssueAssyReworkAsync(long id, [FromBody] TaktQualityIssueAssyReworkUpdateDto dto)
    {
        var result = await _service.UpdateQualityIssueAssyReworkAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:delete", "删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<ActionResult> DeleteQualityIssueAssyReworkByIdAsync(long id)
    {
        await _service.DeleteQualityIssueAssyReworkByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:delete", "批量删除质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<ActionResult> DeleteQualityIssueAssyReworkBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityIssueAssyReworkBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:import", "获取质量问题组装不良改修费用明细表(QualityIssueAssyRework)导入模板")]
    public async Task<IActionResult> GetQualityIssueAssyReworkTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityIssueAssyReworkTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:import", "导入质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<ActionResult<object>> ImportQualityIssueAssyReworkAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityIssueAssyReworkAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出质量问题组装不良改修费用明细表(QualityIssueAssyRework)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityissueassyrework:export", "导出质量问题组装不良改修费用明细表(QualityIssueAssyRework)")]
    public async Task<IActionResult> ExportQualityIssueAssyReworkAsync([FromBody] TaktQualityIssueAssyReworkQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityIssueAssyReworkAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
