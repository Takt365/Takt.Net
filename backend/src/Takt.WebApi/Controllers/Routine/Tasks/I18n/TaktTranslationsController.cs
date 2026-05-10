// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.I18n
// 文件名称：TaktTranslationsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：翻译表控制器，提供Translation管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Application.Services.Routine.Tasks.I18n;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.I18n;

/// <summary>
/// 翻译表控制器
/// </summary>
[Route("api/[controller]", Name = "翻译表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:i18n:translation", "翻译表管理")]
public class TaktTranslationsController : TaktControllerBase
{
    private readonly ITaktTranslationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationsController(
        ITaktTranslationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取翻译表(Translation)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:i18n:translation:list", "查询翻译表(Translation)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTranslationDto>>> GetTranslationListAsync([FromQuery] TaktTranslationQueryDto queryDto)
    {
        var result = await _service.GetTranslationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取翻译表(Translation)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:i18n:translation:query", "查询翻译表(Translation)详情")]
    public async Task<ActionResult<TaktTranslationDto>> GetTranslationByIdAsync(long id)
    {
        var item = await _service.GetTranslationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取翻译表(Translation)选项列表
    /// </summary>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTranslationOptionsAsync()
    {
        var result = await _service.GetTranslationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建翻译表(Translation)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:i18n:translation:create", "创建翻译表(Translation)")]
    public async Task<ActionResult<TaktTranslationDto>> CreateTranslationAsync([FromBody] TaktTranslationCreateDto dto)
    {
        var result = await _service.CreateTranslationAsync(dto);
        return CreatedAtAction(nameof(GetTranslationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新翻译表(Translation)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:i18n:translation:update", "更新翻译表(Translation)")]
    public async Task<ActionResult<TaktTranslationDto>> UpdateTranslationAsync(long id, [FromBody] TaktTranslationUpdateDto dto)
    {
        var result = await _service.UpdateTranslationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除翻译表(Translation)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:i18n:translation:delete", "删除翻译表(Translation)")]
    public async Task<ActionResult> DeleteTranslationByIdAsync(long id)
    {
        await _service.DeleteTranslationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除翻译表(Translation)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:i18n:translation:delete", "批量删除翻译表(Translation)")]
    public async Task<ActionResult> DeleteTranslationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTranslationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新翻译表(Translation)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:i18n:translation:update", "更新翻译表(Translation)排序")]
    public async Task<ActionResult<TaktTranslationDto>> UpdateTranslationSortAsync([FromBody] TaktTranslationSortDto dto)
    {
        var result = await _service.UpdateTranslationSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取翻译表(Translation)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:i18n:translation:import", "获取翻译表(Translation)导入模板")]
    public async Task<IActionResult> GetTranslationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTranslationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入翻译表(Translation)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:i18n:translation:import", "导入翻译表(Translation)")]
    public async Task<ActionResult<object>> ImportTranslationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTranslationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出翻译表(Translation)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:i18n:translation:export", "导出翻译表(Translation)")]
    public async Task<IActionResult> ExportTranslationAsync([FromBody] TaktTranslationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTranslationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
