// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktPerformanceReviewsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效评审表控制器，提供PerformanceReview管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Application.Services.HumanResource.Performance;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Performance;

/// <summary>
/// 绩效评审表控制器
/// </summary>
[Route("api/[controller]", Name = "绩效评审表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:performancereview", "绩效评审表管理")]
public class TaktPerformanceReviewsController : TaktControllerBase
{
    private readonly ITaktPerformanceReviewService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewsController(
        ITaktPerformanceReviewService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取绩效评审表(PerformanceReview)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:performancereview:list", "查询绩效评审表(PerformanceReview)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPerformanceReviewDto>>> GetPerformanceReviewListAsync([FromQuery] TaktPerformanceReviewQueryDto queryDto)
    {
        var result = await _service.GetPerformanceReviewListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取绩效评审表(PerformanceReview)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:performancereview:query", "查询绩效评审表(PerformanceReview)详情")]
    public async Task<ActionResult<TaktPerformanceReviewDto>> GetPerformanceReviewByIdAsync(long id)
    {
        var item = await _service.GetPerformanceReviewByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取绩效评审表(PerformanceReview)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:performancereview:query", "查询绩效评审表(PerformanceReview)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPerformanceReviewOptionsAsync()
    {
        var result = await _service.GetPerformanceReviewOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建绩效评审表(PerformanceReview)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:performancereview:create", "创建绩效评审表(PerformanceReview)")]
    public async Task<ActionResult<TaktPerformanceReviewDto>> CreatePerformanceReviewAsync([FromBody] TaktPerformanceReviewCreateDto dto)
    {
        var result = await _service.CreatePerformanceReviewAsync(dto);
        return CreatedAtAction(nameof(GetPerformanceReviewByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新绩效评审表(PerformanceReview)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:performancereview:update", "更新绩效评审表(PerformanceReview)")]
    public async Task<ActionResult<TaktPerformanceReviewDto>> UpdatePerformanceReviewAsync(long id, [FromBody] TaktPerformanceReviewUpdateDto dto)
    {
        var result = await _service.UpdatePerformanceReviewAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除绩效评审表(PerformanceReview)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:performancereview:delete", "删除绩效评审表(PerformanceReview)")]
    public async Task<ActionResult> DeletePerformanceReviewByIdAsync(long id)
    {
        await _service.DeletePerformanceReviewByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除绩效评审表(PerformanceReview)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:performancereview:delete", "批量删除绩效评审表(PerformanceReview)")]
    public async Task<ActionResult> DeletePerformanceReviewBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePerformanceReviewBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新绩效评审表(PerformanceReview)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:performancereview:update", "更新绩效评审表(PerformanceReview)状态")]
    public async Task<ActionResult<TaktPerformanceReviewDto>> UpdatePerformanceReviewStatusAsync([FromBody] TaktPerformanceReviewStatusDto dto)
    {
        var result = await _service.UpdatePerformanceReviewStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取绩效评审表(PerformanceReview)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:performancereview:import", "获取绩效评审表(PerformanceReview)导入模板")]
    public async Task<IActionResult> GetPerformanceReviewTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPerformanceReviewTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入绩效评审表(PerformanceReview)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:performancereview:import", "导入绩效评审表(PerformanceReview)")]
    public async Task<ActionResult<object>> ImportPerformanceReviewAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPerformanceReviewAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出绩效评审表(PerformanceReview)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:performancereview:export", "导出绩效评审表(PerformanceReview)")]
    public async Task<IActionResult> ExportPerformanceReviewAsync([FromBody] TaktPerformanceReviewQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPerformanceReviewAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
