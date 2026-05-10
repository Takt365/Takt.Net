// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktAccountingTitleChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：会计科目变更记录表控制器，提供AccountingTitleChangeLog管理的RESTful API接口
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
/// 会计科目变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "会计科目变更记录表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:accountingtitlechangelog", "会计科目变更记录表管理")]
public class TaktAccountingTitleChangeLogsController : TaktControllerBase
{
    private readonly ITaktAccountingTitleChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAccountingTitleChangeLogsController(
        ITaktAccountingTitleChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取会计科目变更记录表(AccountingTitleChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:list", "查询会计科目变更记录表(AccountingTitleChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAccountingTitleChangeLogDto>>> GetAccountingTitleChangeLogListAsync([FromQuery] TaktAccountingTitleChangeLogQueryDto queryDto)
    {
        var result = await _service.GetAccountingTitleChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:query", "查询会计科目变更记录表(AccountingTitleChangeLog)详情")]
    public async Task<ActionResult<TaktAccountingTitleChangeLogDto>> GetAccountingTitleChangeLogByIdAsync(long id)
    {
        var item = await _service.GetAccountingTitleChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取会计科目变更记录表(AccountingTitleChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:query", "查询会计科目变更记录表(AccountingTitleChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAccountingTitleChangeLogOptionsAsync()
    {
        var result = await _service.GetAccountingTitleChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:accountingtitlechangelog:create", "创建会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<ActionResult<TaktAccountingTitleChangeLogDto>> CreateAccountingTitleChangeLogAsync([FromBody] TaktAccountingTitleChangeLogCreateDto dto)
    {
        var result = await _service.CreateAccountingTitleChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetAccountingTitleChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:update", "更新会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<ActionResult<TaktAccountingTitleChangeLogDto>> UpdateAccountingTitleChangeLogAsync(long id, [FromBody] TaktAccountingTitleChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateAccountingTitleChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:delete", "删除会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<ActionResult> DeleteAccountingTitleChangeLogByIdAsync(long id)
    {
        await _service.DeleteAccountingTitleChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:delete", "批量删除会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<ActionResult> DeleteAccountingTitleChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAccountingTitleChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取会计科目变更记录表(AccountingTitleChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:import", "获取会计科目变更记录表(AccountingTitleChangeLog)导入模板")]
    public async Task<IActionResult> GetAccountingTitleChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAccountingTitleChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:import", "导入会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<ActionResult<object>> ImportAccountingTitleChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportAccountingTitleChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出会计科目变更记录表(AccountingTitleChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:accountingtitlechangelog:export", "导出会计科目变更记录表(AccountingTitleChangeLog)")]
    public async Task<IActionResult> ExportAccountingTitleChangeLogAsync([FromBody] TaktAccountingTitleChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAccountingTitleChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
