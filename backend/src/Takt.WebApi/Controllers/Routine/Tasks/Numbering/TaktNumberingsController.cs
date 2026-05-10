// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Numbering
// 文件名称：TaktNumberingsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：编码规则表控制器，提供Numbering管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Numbering;
using Takt.Application.Services.Routine.Tasks.Numbering;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Numbering;

/// <summary>
/// 编码规则表控制器
/// </summary>
[Route("api/[controller]", Name = "编码规则表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:numbering", "编码规则表管理")]
public class TaktNumberingsController : TaktControllerBase
{
    private readonly ITaktNumberingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNumberingsController(
        ITaktNumberingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取编码规则表(Numbering)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:numbering:list", "查询编码规则表(Numbering)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNumberingDto>>> GetNumberingListAsync([FromQuery] TaktNumberingQueryDto queryDto)
    {
        var result = await _service.GetNumberingListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取编码规则表(Numbering)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:numbering:query", "查询编码规则表(Numbering)详情")]
    public async Task<ActionResult<TaktNumberingDto>> GetNumberingByIdAsync(long id)
    {
        var item = await _service.GetNumberingByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取编码规则表(Numbering)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:numbering:query", "查询编码规则表(Numbering)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNumberingOptionsAsync()
    {
        var result = await _service.GetNumberingOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建编码规则表(Numbering)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:numbering:create", "创建编码规则表(Numbering)")]
    public async Task<ActionResult<TaktNumberingDto>> CreateNumberingAsync([FromBody] TaktNumberingCreateDto dto)
    {
        var result = await _service.CreateNumberingAsync(dto);
        return CreatedAtAction(nameof(GetNumberingByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新编码规则表(Numbering)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:numbering:update", "更新编码规则表(Numbering)")]
    public async Task<ActionResult<TaktNumberingDto>> UpdateNumberingAsync(long id, [FromBody] TaktNumberingUpdateDto dto)
    {
        var result = await _service.UpdateNumberingAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除编码规则表(Numbering)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:numbering:delete", "删除编码规则表(Numbering)")]
    public async Task<ActionResult> DeleteNumberingByIdAsync(long id)
    {
        await _service.DeleteNumberingByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除编码规则表(Numbering)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:numbering:delete", "批量删除编码规则表(Numbering)")]
    public async Task<ActionResult> DeleteNumberingBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNumberingBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新编码规则表(Numbering)Rule
    /// </summary>
    [HttpPut("status-rule")]
    [TaktPermission("routine:tasks:numbering:update", "更新编码规则表(Numbering)Rule")]
    public async Task<ActionResult<TaktNumberingDto>> UpdateNumberingRuleStatusAsync([FromBody] TaktNumberingRuleStatusDto dto)
    {
        var result = await _service.UpdateNumberingRuleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新编码规则表(Numbering)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:numbering:update", "更新编码规则表(Numbering)排序")]
    public async Task<ActionResult<TaktNumberingDto>> UpdateNumberingSortAsync([FromBody] TaktNumberingSortDto dto)
    {
        var result = await _service.UpdateNumberingSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取编码规则表(Numbering)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:numbering:import", "获取编码规则表(Numbering)导入模板")]
    public async Task<IActionResult> GetNumberingTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNumberingTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入编码规则表(Numbering)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:numbering:import", "导入编码规则表(Numbering)")]
    public async Task<ActionResult<object>> ImportNumberingAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNumberingAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出编码规则表(Numbering)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:numbering:export", "导出编码规则表(Numbering)")]
    public async Task<IActionResult> ExportNumberingAsync([FromBody] TaktNumberingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNumberingAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
