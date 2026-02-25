// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Routine.I18n
// 文件名称：TaktLanguagesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言控制器，提供语言管理的RESTful API接口
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

namespace Takt.WebApi.Controllers.Routine.I18n;

/// <summary>
/// Takt语言控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "语言")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:language", "语言管理")]
public class TaktLanguagesController : TaktControllerBase
{
    private readonly ITaktLanguageService _languageService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="languageService">语言服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktLanguagesController(
        ITaktLanguageService languageService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _languageService = languageService;
    }

    /// <summary>
    /// 获取语言列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("routine:language:list", "查询语言列表")]
    public async Task<ActionResult<TaktPagedResult<TaktLanguageDto>>> GetListAsync([FromQuery] TaktLanguageQueryDto queryDto)
    {
        var result = await _languageService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>语言DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("routine:language:query", "查询语言详情")]
    public async Task<ActionResult<TaktLanguageDto>> GetByIdAsync(long id)
    {
        var language = await _languageService.GetByIdAsync(id);
        if (language == null)
            return NotFound();
        return Ok(language);
    }

    /// <summary>
    /// 获取语言选项列表（用于下拉框等）
    /// </summary>
    /// <returns>语言选项列表</returns>
    [HttpGet("options")]
    [AllowAnonymous]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _languageService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建语言
    /// </summary>
    /// <param name="dto">创建语言DTO</param>
    /// <returns>语言DTO</returns>
    [HttpPost]
    [TaktPermission("routine:language:create", "创建语言")]
    public async Task<ActionResult<TaktLanguageDto>> CreateAsync([FromBody] TaktLanguageCreateDto dto)
    {
        var language = await _languageService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = language.LanguageId }, language);
    }

    /// <summary>
    /// 更新语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <param name="dto">更新语言DTO</param>
    /// <returns>语言DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("routine:language:update", "更新语言")]
    public async Task<ActionResult<TaktLanguageDto>> UpdateAsync(long id, [FromBody] TaktLanguageUpdateDto dto)
    {
        var language = await _languageService.UpdateAsync(id, dto);
        return Ok(language);
    }

    /// <summary>
    /// 删除语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>无内容</returns>
    [HttpDelete("{id}")]
    [TaktPermission("routine:language:delete", "删除语言")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _languageService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新语言状态
    /// </summary>
    /// <param name="dto">语言状态DTO</param>
    /// <returns>语言DTO</returns>
    [HttpPut("status")]
    [TaktPermission("routine:language:status", "更新语言状态")]
    public async Task<ActionResult<TaktLanguageDto>> UpdateStatusAsync([FromBody] TaktLanguageStatusDto dto)
    {
        var language = await _languageService.UpdateStatusAsync(dto);
        return Ok(language);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("routine:language:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _languageService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入语言
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("routine:language:import", "导入语言")]
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
            var (success, fail, errors) = await _languageService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出语言
    /// </summary>
    /// <param name="query">语言查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("routine:language:export", "导出语言")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktLanguageQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _languageService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
