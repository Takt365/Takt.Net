// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAccountingTitlesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：会计科目表控制器，提供AccountingTitle管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Financial;

/// <summary>
/// 会计科目表控制器
/// </summary>
[Route("api/[controller]", Name = "会计科目表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:accountingtitle", "会计科目表管理")]
public class TaktAccountingTitlesController : TaktControllerBase
{
    private readonly ITaktAccountingTitleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitlesController(
        ITaktAccountingTitleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:accountingtitle:list", "查询会计科目表(AccountingTitle)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAccountingTitleDto>>> GetAccountingTitleListAsync([FromQuery] TaktAccountingTitleQueryDto queryDto)
    {
        var result = await _service.GetAccountingTitleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取会计科目表(AccountingTitle)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:accountingtitle:query", "查询会计科目表(AccountingTitle)详情")]
    public async Task<ActionResult<TaktAccountingTitleDto>> GetAccountingTitleByIdAsync(long id)
    {
        var item = await _service.GetAccountingTitleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:accountingtitle:query", "查询会计科目表(AccountingTitle)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAccountingTitleOptionsAsync()
    {
        var result = await _service.GetAccountingTitleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:financial:accountingtitle:query", "查询会计科目表(AccountingTitle)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetAccountingTitleTreeOptionsAsync()
    {
        var result = await _service.GetAccountingTitleTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("accounting:financial:accountingtitle:query", "查询会计科目表(AccountingTitle)树形")]
    public async Task<ActionResult<List<TaktAccountingTitleTreeDto>>> GetAccountingTitleTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetAccountingTitleTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("accounting:financial:accountingtitle:query", "查询会计科目表(AccountingTitle)子节点")]
    public async Task<ActionResult<List<TaktAccountingTitleDto>>> GetAccountingTitleChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetAccountingTitleChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建会计科目表(AccountingTitle)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:accountingtitle:create", "创建会计科目表(AccountingTitle)")]
    public async Task<ActionResult<TaktAccountingTitleDto>> CreateAccountingTitleAsync([FromBody] TaktAccountingTitleCreateDto dto)
    {
        var result = await _service.CreateAccountingTitleAsync(dto);
        return CreatedAtAction(nameof(GetAccountingTitleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新会计科目表(AccountingTitle)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:accountingtitle:update", "更新会计科目表(AccountingTitle)")]
    public async Task<ActionResult<TaktAccountingTitleDto>> UpdateAccountingTitleAsync(long id, [FromBody] TaktAccountingTitleUpdateDto dto)
    {
        var result = await _service.UpdateAccountingTitleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除会计科目表(AccountingTitle)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:accountingtitle:delete", "删除会计科目表(AccountingTitle)")]
    public async Task<ActionResult> DeleteAccountingTitleByIdAsync(long id)
    {
        await _service.DeleteAccountingTitleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除会计科目表(AccountingTitle)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:accountingtitle:delete", "批量删除会计科目表(AccountingTitle)")]
    public async Task<ActionResult> DeleteAccountingTitleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAccountingTitleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新会计科目表(AccountingTitle)Title
    /// </summary>
    [HttpPut("status-title")]
    [TaktPermission("accounting:financial:accountingtitle:update", "更新会计科目表(AccountingTitle)Title")]
    public async Task<ActionResult<TaktAccountingTitleDto>> UpdateAccountingTitleTitleStatusAsync([FromBody] TaktAccountingTitleTitleStatusDto dto)
    {
        var result = await _service.UpdateAccountingTitleTitleStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新会计科目表(AccountingTitle)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("accounting:financial:accountingtitle:update", "更新会计科目表(AccountingTitle)排序")]
    public async Task<ActionResult<TaktAccountingTitleDto>> UpdateAccountingTitleSortAsync([FromBody] TaktAccountingTitleSortDto dto)
    {
        var result = await _service.UpdateAccountingTitleSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取会计科目表(AccountingTitle)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:accountingtitle:import", "获取会计科目表(AccountingTitle)导入模板")]
    public async Task<IActionResult> GetAccountingTitleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAccountingTitleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入会计科目表(AccountingTitle)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:accountingtitle:import", "导入会计科目表(AccountingTitle)")]
    public async Task<ActionResult<object>> ImportAccountingTitleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAccountingTitleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出会计科目表(AccountingTitle)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:accountingtitle:export", "导出会计科目表(AccountingTitle)")]
    public async Task<IActionResult> ExportAccountingTitleAsync([FromBody] TaktAccountingTitleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAccountingTitleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
