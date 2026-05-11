// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueMeetingsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：质量问题会议调查试验费用明细表控制器，提供QualityIssueMeeting管理的RESTful API接口
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
/// 质量问题会议调查试验费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "质量问题会议调查试验费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityissuemeeting", "质量问题会议调查试验费用明细表管理")]
public class TaktQualityIssueMeetingsController : TaktControllerBase
{
    private readonly ITaktQualityIssueMeetingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityIssueMeetingsController(
        ITaktQualityIssueMeetingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:list", "查询质量问题会议调查试验费用明细表(QualityIssueMeeting)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityIssueMeetingDto>>> GetQualityIssueMeetingListAsync([FromQuery] TaktQualityIssueMeetingQueryDto queryDto)
    {
        var result = await _service.GetQualityIssueMeetingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:query", "查询质量问题会议调查试验费用明细表(QualityIssueMeeting)详情")]
    public async Task<ActionResult<TaktQualityIssueMeetingDto>> GetQualityIssueMeetingByIdAsync(long id)
    {
        var item = await _service.GetQualityIssueMeetingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:query", "查询质量问题会议调查试验费用明细表(QualityIssueMeeting)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityIssueMeetingOptionsAsync()
    {
        var result = await _service.GetQualityIssueMeetingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:create", "创建质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<ActionResult<TaktQualityIssueMeetingDto>> CreateQualityIssueMeetingAsync([FromBody] TaktQualityIssueMeetingCreateDto dto)
    {
        var result = await _service.CreateQualityIssueMeetingAsync(dto);
        return CreatedAtAction(nameof(GetQualityIssueMeetingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:update", "更新质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<ActionResult<TaktQualityIssueMeetingDto>> UpdateQualityIssueMeetingAsync(long id, [FromBody] TaktQualityIssueMeetingUpdateDto dto)
    {
        var result = await _service.UpdateQualityIssueMeetingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:delete", "删除质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<ActionResult> DeleteQualityIssueMeetingByIdAsync(long id)
    {
        await _service.DeleteQualityIssueMeetingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:delete", "批量删除质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<ActionResult> DeleteQualityIssueMeetingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityIssueMeetingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取质量问题会议调查试验费用明细表(QualityIssueMeeting)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:import", "获取质量问题会议调查试验费用明细表(QualityIssueMeeting)导入模板")]
    public async Task<IActionResult> GetQualityIssueMeetingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityIssueMeetingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:import", "导入质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<ActionResult<object>> ImportQualityIssueMeetingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityIssueMeetingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出质量问题会议调查试验费用明细表(QualityIssueMeeting)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityissuemeeting:export", "导出质量问题会议调查试验费用明细表(QualityIssueMeeting)")]
    public async Task<IActionResult> ExportQualityIssueMeetingAsync([FromBody] TaktQualityIssueMeetingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityIssueMeetingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
