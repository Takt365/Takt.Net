// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Mail
// 文件名称：TaktMailRecipientsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件收件人表控制器，提供MailRecipient管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Application.Services.Routine.Business.Mail;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.Mail;

/// <summary>
/// 邮件收件人表控制器
/// </summary>
[Route("api/[controller]", Name = "邮件收件人表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:mail:mailrecipient", "邮件收件人表管理")]
public class TaktMailRecipientsController : TaktControllerBase
{
    private readonly ITaktMailRecipientService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientsController(
        ITaktMailRecipientService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:mail:mailrecipient:list", "查询邮件收件人表(MailRecipient)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMailRecipientDto>>> GetMailRecipientListAsync([FromQuery] TaktMailRecipientQueryDto queryDto)
    {
        var result = await _service.GetMailRecipientListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取邮件收件人表(MailRecipient)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:mail:mailrecipient:query", "查询邮件收件人表(MailRecipient)详情")]
    public async Task<ActionResult<TaktMailRecipientDto>> GetMailRecipientByIdAsync(long id)
    {
        var item = await _service.GetMailRecipientByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:mail:mailrecipient:query", "查询邮件收件人表(MailRecipient)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMailRecipientOptionsAsync()
    {
        var result = await _service.GetMailRecipientOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建邮件收件人表(MailRecipient)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:mail:mailrecipient:create", "创建邮件收件人表(MailRecipient)")]
    public async Task<ActionResult<TaktMailRecipientDto>> CreateMailRecipientAsync([FromBody] TaktMailRecipientCreateDto dto)
    {
        var result = await _service.CreateMailRecipientAsync(dto);
        return CreatedAtAction(nameof(GetMailRecipientByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新邮件收件人表(MailRecipient)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:mail:mailrecipient:update", "更新邮件收件人表(MailRecipient)")]
    public async Task<ActionResult<TaktMailRecipientDto>> UpdateMailRecipientAsync(long id, [FromBody] TaktMailRecipientUpdateDto dto)
    {
        var result = await _service.UpdateMailRecipientAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除邮件收件人表(MailRecipient)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:mail:mailrecipient:delete", "删除邮件收件人表(MailRecipient)")]
    public async Task<ActionResult> DeleteMailRecipientByIdAsync(long id)
    {
        await _service.DeleteMailRecipientByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除邮件收件人表(MailRecipient)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:mail:mailrecipient:delete", "批量删除邮件收件人表(MailRecipient)")]
    public async Task<ActionResult> DeleteMailRecipientBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMailRecipientBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新邮件收件人表(MailRecipient)Read
    /// </summary>
    [HttpPut("status-read")]
    [TaktPermission("routine:business:mail:mailrecipient:update", "更新邮件收件人表(MailRecipient)Read")]
    public async Task<ActionResult<TaktMailRecipientDto>> UpdateMailRecipientReadStatusAsync([FromBody] TaktMailRecipientReadStatusDto dto)
    {
        var result = await _service.UpdateMailRecipientReadStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取邮件收件人表(MailRecipient)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:mail:mailrecipient:import", "获取邮件收件人表(MailRecipient)导入模板")]
    public async Task<IActionResult> GetMailRecipientTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMailRecipientTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入邮件收件人表(MailRecipient)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:mail:mailrecipient:import", "导入邮件收件人表(MailRecipient)")]
    public async Task<ActionResult<object>> ImportMailRecipientAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportMailRecipientAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出邮件收件人表(MailRecipient)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:mail:mailrecipient:export", "导出邮件收件人表(MailRecipient)")]
    public async Task<IActionResult> ExportMailRecipientAsync([FromBody] TaktMailRecipientQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMailRecipientAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
