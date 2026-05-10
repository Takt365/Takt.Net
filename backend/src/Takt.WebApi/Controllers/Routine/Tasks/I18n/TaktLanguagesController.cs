// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.I18n
// 文件名称：TaktLanguagesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：语言表控制器，提供Language管理的RESTful API接口
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
/// 语言表控制器
/// </summary>
[Route("api/[controller]", Name = "语言表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:i18n:language", "语言表管理")]
public class TaktLanguagesController : TaktControllerBase
{
    private readonly ITaktLanguageService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguagesController(
        ITaktLanguageService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取语言表(Language)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:i18n:language:list", "查询语言表(Language)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktLanguageDto>>> GetLanguageListAsync([FromQuery] TaktLanguageQueryDto queryDto)
    {
        var result = await _service.GetLanguageListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取语言表(Language)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:i18n:language:query", "查询语言表(Language)详情")]
    public async Task<ActionResult<TaktLanguageDto>> GetLanguageByIdAsync(long id)
    {
        var item = await _service.GetLanguageByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取语言表(Language)选项列表
    /// </summary>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetLanguageOptionsAsync()
    {
        var result = await _service.GetLanguageOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建语言表(Language)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:i18n:language:create", "创建语言表(Language)")]
    public async Task<ActionResult<TaktLanguageDto>> CreateLanguageAsync([FromBody] TaktLanguageCreateDto dto)
    {
        var result = await _service.CreateLanguageAsync(dto);
        return CreatedAtAction(nameof(GetLanguageByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新语言表(Language)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:i18n:language:update", "更新语言表(Language)")]
    public async Task<ActionResult<TaktLanguageDto>> UpdateLanguageAsync(long id, [FromBody] TaktLanguageUpdateDto dto)
    {
        var result = await _service.UpdateLanguageAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除语言表(Language)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:i18n:language:delete", "删除语言表(Language)")]
    public async Task<ActionResult> DeleteLanguageByIdAsync(long id)
    {
        await _service.DeleteLanguageByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除语言表(Language)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:i18n:language:delete", "批量删除语言表(Language)")]
    public async Task<ActionResult> DeleteLanguageBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteLanguageBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新语言表(Language)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:i18n:language:update", "更新语言表(Language)状态")]
    public async Task<ActionResult<TaktLanguageDto>> UpdateLanguageStatusAsync([FromBody] TaktLanguageStatusDto dto)
    {
        var result = await _service.UpdateLanguageStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新语言表(Language)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:tasks:i18n:language:update", "更新语言表(Language)排序")]
    public async Task<ActionResult<TaktLanguageDto>> UpdateLanguageSortAsync([FromBody] TaktLanguageSortDto dto)
    {
        var result = await _service.UpdateLanguageSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取语言表(Language)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:i18n:language:import", "获取语言表(Language)导入模板")]
    public async Task<IActionResult> GetLanguageTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetLanguageTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入语言表(Language)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:i18n:language:import", "导入语言表(Language)")]
    public async Task<ActionResult<object>> ImportLanguageAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportLanguageAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出语言表(Language)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:i18n:language:export", "导出语言表(Language)")]
    public async Task<IActionResult> ExportLanguageAsync([FromBody] TaktLanguageQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportLanguageAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
