// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularysController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：敏感词表控制器，提供Vocabulary管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.Vocabulary;
using Takt.Application.Services.Routine.Tasks.Vocabulary;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Tasks.Vocabulary;

/// <summary>
/// 敏感词表控制器
/// </summary>
[Route("api/[controller]", Name = "敏感词表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:vocabulary", "敏感词表管理")]
public class TaktVocabularysController : TaktControllerBase
{
    private readonly ITaktVocabularyService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVocabularysController(
        ITaktVocabularyService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取敏感词表(Vocabulary)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:tasks:vocabulary:list", "查询敏感词表(Vocabulary)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktVocabularyDto>>> GetVocabularyListAsync([FromQuery] TaktVocabularyQueryDto queryDto)
    {
        var result = await _service.GetVocabularyListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取敏感词表(Vocabulary)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:tasks:vocabulary:query", "查询敏感词表(Vocabulary)详情")]
    public async Task<ActionResult<TaktVocabularyDto>> GetVocabularyByIdAsync(long id)
    {
        var item = await _service.GetVocabularyByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取敏感词表(Vocabulary)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:tasks:vocabulary:query", "查询敏感词表(Vocabulary)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetVocabularyOptionsAsync()
    {
        var result = await _service.GetVocabularyOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建敏感词表(Vocabulary)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:tasks:vocabulary:create", "创建敏感词表(Vocabulary)")]
    public async Task<ActionResult<TaktVocabularyDto>> CreateVocabularyAsync([FromBody] TaktVocabularyCreateDto dto)
    {
        var result = await _service.CreateVocabularyAsync(dto);
        return CreatedAtAction(nameof(GetVocabularyByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新敏感词表(Vocabulary)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:tasks:vocabulary:update", "更新敏感词表(Vocabulary)")]
    public async Task<ActionResult<TaktVocabularyDto>> UpdateVocabularyAsync(long id, [FromBody] TaktVocabularyUpdateDto dto)
    {
        var result = await _service.UpdateVocabularyAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除敏感词表(Vocabulary)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:tasks:vocabulary:delete", "删除敏感词表(Vocabulary)")]
    public async Task<ActionResult> DeleteVocabularyByIdAsync(long id)
    {
        await _service.DeleteVocabularyByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除敏感词表(Vocabulary)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:tasks:vocabulary:delete", "批量删除敏感词表(Vocabulary)")]
    public async Task<ActionResult> DeleteVocabularyBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteVocabularyBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新敏感词表(Vocabulary)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:tasks:vocabulary:update", "更新敏感词表(Vocabulary)状态")]
    public async Task<ActionResult<TaktVocabularyDto>> UpdateVocabularyStatusAsync([FromBody] TaktVocabularyStatusDto dto)
    {
        var result = await _service.UpdateVocabularyStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取敏感词表(Vocabulary)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:tasks:vocabulary:import", "获取敏感词表(Vocabulary)导入模板")]
    public async Task<IActionResult> GetVocabularyTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetVocabularyTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入敏感词表(Vocabulary)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:tasks:vocabulary:import", "导入敏感词表(Vocabulary)")]
    public async Task<ActionResult<object>> ImportVocabularyAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportVocabularyAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出敏感词表(Vocabulary)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:tasks:vocabulary:export", "导出敏感词表(Vocabulary)")]
    public async Task<IActionResult> ExportVocabularyAsync([FromBody] TaktVocabularyQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportVocabularyAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
