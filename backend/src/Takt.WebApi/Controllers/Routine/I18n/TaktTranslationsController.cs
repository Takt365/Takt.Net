// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.I18n
// 文件名称：TaktTranslationsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt翻译控制器，提供翻译管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.I18n;
using Takt.Application.Services.Routine.I18n;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.I18n;

/// <summary>
/// Takt翻译控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "翻译")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:translation", "翻译管理")]
public class TaktTranslationsController : TaktControllerBase
{
    private readonly ITaktTranslationService _translationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationService">翻译服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktTranslationsController(
        ITaktTranslationService translationService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _translationService = translationService;
    }

    /// <summary>
    /// 获取翻译列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:translation:list", "查询翻译列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTranslationDto>>> GetListAsync([FromQuery] TaktTranslationQueryDto queryDto)
    {
        var result = await _translationService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 获取翻译列表（转置：按资源键分组，各语言为列，分页；含 CultureCodeOrder 供表头与双行展示）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果与语言列顺序</returns>
    [HttpGet("list/transposed")]
    [TaktPermission("routine:translation:list", "查询翻译列表")]
    public async Task<ActionResult<TaktTranslationTransposedResult>> GetListTransposedAsync([FromQuery] TaktTranslationQueryDto queryDto)
    {
        var result = await _translationService.GetListTransposedAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>翻译DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:translation:query", "查询翻译详情")]
    public async Task<ActionResult<TaktTranslationDto>> GetByIdAsync(long id)
    {
        var translation = await _translationService.GetByIdAsync(id);
        if (translation == null)
            return NotFound();
        return Ok(translation);
    }

    /// <summary>
    /// 获取翻译选项列表（用于下拉框等）
    /// </summary>
    /// <returns>翻译选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _translationService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建翻译
    /// </summary>
    /// <param name="dto">创建翻译DTO</param>
    /// <returns>翻译DTO</returns>
    [HttpPost]
    [TaktPermission("routine:translation:create", "创建翻译")]
    public async Task<ActionResult<TaktTranslationDto>> CreateAsync([FromBody] TaktTranslationCreateDto dto)
    {
        var translation = await _translationService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = translation.TranslationId }, translation);
    }

    /// <summary>
    /// 更新翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <param name="dto">更新翻译DTO</param>
    /// <returns>翻译DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:translation:update", "更新翻译")]
    public async Task<ActionResult<TaktTranslationDto>> UpdateAsync(long id, [FromBody] TaktTranslationUpdateDto dto)
    {
        var translation = await _translationService.UpdateAsync(id, dto);
        return Ok(translation);
    }

    /// <summary>
    /// 删除翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>无内容</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:translation:delete", "删除翻译")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _translationService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:translation:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _translationService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入翻译
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:translation:import", "导入翻译")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _translationService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出翻译
    /// </summary>
    /// <param name="query">翻译查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("routine:translation:export", "导出翻译")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktTranslationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _translationService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
