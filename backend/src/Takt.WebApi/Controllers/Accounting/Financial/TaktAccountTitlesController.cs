// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAccountTitlesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 科目（AccountTitle）控制器，提供会计科目管理的 RESTful API
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 会计科目（AccountTitle）控制器
/// </summary>
[Route("api/[controller]", Name = "会计科目")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:title", "会计科目管理")]
public class TaktAccountTitlesController : TaktControllerBase
{
    private readonly ITaktAccountTitleService _titleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountTitlesController(
        ITaktAccountTitleService titleService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _titleService = titleService;
    }

    /// <summary>
    /// 获取会计科目列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:title:list", "查询会计科目列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAccountTitleDto>>> GetListAsync([FromQuery] TaktAccountTitleQueryDto queryDto)
    {
        var result = await _titleService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取会计科目
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:title:query", "查询会计科目详情")]
    public async Task<ActionResult<TaktAccountTitleDto>> GetByIdAsync(long id)
    {
        var title = await _titleService.GetByIdAsync(id);
        if (title == null)
            return NotFound();
        return Ok(title);
    }

    /// <summary>
    /// 获取会计科目树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:title:list", "查询会计科目树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetTreeOptionsAsync()
    {
        var options = await _titleService.GetTreeOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 获取会计科目树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("accounting:title:list", "查询会计科目树形列表")]
    public async Task<ActionResult<List<TaktAccountTitleTreeDto>>> GetTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _titleService.GetTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 获取会计科目子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("accounting:title:list", "查询会计科目子节点列表")]
    public async Task<ActionResult<List<TaktAccountTitleDto>>> GetChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _titleService.GetChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }

    /// <summary>
    /// 创建会计科目
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:title:create", "创建会计科目")]
    public async Task<ActionResult<TaktAccountTitleDto>> CreateAsync([FromBody] TaktAccountTitleCreateDto dto)
    {
        try
        {
            var title = await _titleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = title.TitleId }, title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新会计科目
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:title:update", "更新会计科目")]
    public async Task<ActionResult<TaktAccountTitleDto>> UpdateAsync(long id, [FromBody] TaktAccountTitleUpdateDto dto)
    {
        try
        {
            var title = await _titleService.UpdateAsync(id, dto);
            return Ok(title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除会计科目
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:title:delete", "删除会计科目")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _titleService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新会计科目状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:title:status", "更新会计科目状态")]
    public async Task<ActionResult<TaktAccountTitleDto>> UpdateStatusAsync([FromBody] TaktAccountTitleStatusDto dto)
    {
        try
        {
            var title = await _titleService.UpdateStatusAsync(dto);
            return Ok(title);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:title:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _titleService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导入会计科目
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:title:import", "导入会计科目")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("请选择要导入的Excel文件");
            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _titleService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出会计科目
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:title:export", "导出会计科目")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktAccountTitleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _titleService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
