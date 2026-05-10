// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Mail
// 文件名称：TaktMailAttachmentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件附件表控制器，提供MailAttachment管理的RESTful API接口
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
/// 邮件附件表控制器
/// </summary>
[Route("api/[controller]", Name = "邮件附件表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:mail:mailattachment", "邮件附件表管理")]
public class TaktMailAttachmentsController : TaktControllerBase
{
    private readonly ITaktMailAttachmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentsController(
        ITaktMailAttachmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:mail:mailattachment:list", "查询邮件附件表(MailAttachment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMailAttachmentDto>>> GetMailAttachmentListAsync([FromQuery] TaktMailAttachmentQueryDto queryDto)
    {
        var result = await _service.GetMailAttachmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取邮件附件表(MailAttachment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:mail:mailattachment:query", "查询邮件附件表(MailAttachment)详情")]
    public async Task<ActionResult<TaktMailAttachmentDto>> GetMailAttachmentByIdAsync(long id)
    {
        var item = await _service.GetMailAttachmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:mail:mailattachment:query", "查询邮件附件表(MailAttachment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMailAttachmentOptionsAsync()
    {
        var result = await _service.GetMailAttachmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建邮件附件表(MailAttachment)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:mail:mailattachment:create", "创建邮件附件表(MailAttachment)")]
    public async Task<ActionResult<TaktMailAttachmentDto>> CreateMailAttachmentAsync([FromBody] TaktMailAttachmentCreateDto dto)
    {
        var result = await _service.CreateMailAttachmentAsync(dto);
        return CreatedAtAction(nameof(GetMailAttachmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新邮件附件表(MailAttachment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:mail:mailattachment:update", "更新邮件附件表(MailAttachment)")]
    public async Task<ActionResult<TaktMailAttachmentDto>> UpdateMailAttachmentAsync(long id, [FromBody] TaktMailAttachmentUpdateDto dto)
    {
        var result = await _service.UpdateMailAttachmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除邮件附件表(MailAttachment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:mail:mailattachment:delete", "删除邮件附件表(MailAttachment)")]
    public async Task<ActionResult> DeleteMailAttachmentByIdAsync(long id)
    {
        await _service.DeleteMailAttachmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除邮件附件表(MailAttachment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:mail:mailattachment:delete", "批量删除邮件附件表(MailAttachment)")]
    public async Task<ActionResult> DeleteMailAttachmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMailAttachmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新邮件附件表(MailAttachment)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:mail:mailattachment:update", "更新邮件附件表(MailAttachment)排序")]
    public async Task<ActionResult<TaktMailAttachmentDto>> UpdateMailAttachmentSortAsync([FromBody] TaktMailAttachmentSortDto dto)
    {
        var result = await _service.UpdateMailAttachmentSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取邮件附件表(MailAttachment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:mail:mailattachment:import", "获取邮件附件表(MailAttachment)导入模板")]
    public async Task<IActionResult> GetMailAttachmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMailAttachmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入邮件附件表(MailAttachment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:mail:mailattachment:import", "导入邮件附件表(MailAttachment)")]
    public async Task<ActionResult<object>> ImportMailAttachmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportMailAttachmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出邮件附件表(MailAttachment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:mail:mailattachment:export", "导出邮件附件表(MailAttachment)")]
    public async Task<IActionResult> ExportMailAttachmentAsync([FromBody] TaktMailAttachmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMailAttachmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
