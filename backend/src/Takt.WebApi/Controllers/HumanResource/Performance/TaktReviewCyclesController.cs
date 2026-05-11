// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktReviewCyclesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：评审周期表控制器，提供ReviewCycle管理的RESTful API接口
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
/// 评审周期表控制器
/// </summary>
[Route("api/[controller]", Name = "评审周期表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:reviewcycle", "评审周期表管理")]
public class TaktReviewCyclesController : TaktControllerBase
{
    private readonly ITaktReviewCycleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCyclesController(
        ITaktReviewCycleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取评审周期表(ReviewCycle)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:reviewcycle:list", "查询评审周期表(ReviewCycle)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktReviewCycleDto>>> GetReviewCycleListAsync([FromQuery] TaktReviewCycleQueryDto queryDto)
    {
        var result = await _service.GetReviewCycleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取评审周期表(ReviewCycle)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:reviewcycle:query", "查询评审周期表(ReviewCycle)详情")]
    public async Task<ActionResult<TaktReviewCycleDto>> GetReviewCycleByIdAsync(long id)
    {
        var item = await _service.GetReviewCycleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取评审周期表(ReviewCycle)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:reviewcycle:query", "查询评审周期表(ReviewCycle)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetReviewCycleOptionsAsync()
    {
        var result = await _service.GetReviewCycleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建评审周期表(ReviewCycle)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:reviewcycle:create", "创建评审周期表(ReviewCycle)")]
    public async Task<ActionResult<TaktReviewCycleDto>> CreateReviewCycleAsync([FromBody] TaktReviewCycleCreateDto dto)
    {
        var result = await _service.CreateReviewCycleAsync(dto);
        return CreatedAtAction(nameof(GetReviewCycleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新评审周期表(ReviewCycle)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:reviewcycle:update", "更新评审周期表(ReviewCycle)")]
    public async Task<ActionResult<TaktReviewCycleDto>> UpdateReviewCycleAsync(long id, [FromBody] TaktReviewCycleUpdateDto dto)
    {
        var result = await _service.UpdateReviewCycleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除评审周期表(ReviewCycle)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:reviewcycle:delete", "删除评审周期表(ReviewCycle)")]
    public async Task<ActionResult> DeleteReviewCycleByIdAsync(long id)
    {
        await _service.DeleteReviewCycleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除评审周期表(ReviewCycle)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:reviewcycle:delete", "批量删除评审周期表(ReviewCycle)")]
    public async Task<ActionResult> DeleteReviewCycleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteReviewCycleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新评审周期表(ReviewCycle)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:reviewcycle:update", "更新评审周期表(ReviewCycle)状态")]
    public async Task<ActionResult<TaktReviewCycleDto>> UpdateReviewCycleStatusAsync([FromBody] TaktReviewCycleStatusDto dto)
    {
        var result = await _service.UpdateReviewCycleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取评审周期表(ReviewCycle)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:reviewcycle:import", "获取评审周期表(ReviewCycle)导入模板")]
    public async Task<IActionResult> GetReviewCycleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetReviewCycleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入评审周期表(ReviewCycle)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:reviewcycle:import", "导入评审周期表(ReviewCycle)")]
    public async Task<ActionResult<object>> ImportReviewCycleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportReviewCycleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出评审周期表(ReviewCycle)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:reviewcycle:export", "导出评审周期表(ReviewCycle)")]
    public async Task<IActionResult> ExportReviewCycleAsync([FromBody] TaktReviewCycleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportReviewCycleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
