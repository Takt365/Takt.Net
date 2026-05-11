// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Mail
// 文件名称：TaktMailsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：邮件表控制器，提供Mail管理的RESTful API接口
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
/// 邮件表控制器
/// </summary>
[Route("api/[controller]", Name = "邮件表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:mail", "邮件表管理")]
public class TaktMailsController : TaktControllerBase
{
    private readonly ITaktMailService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailsController(
        ITaktMailService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取邮件表(Mail)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:mail:list", "查询邮件表(Mail)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMailDto>>> GetMailListAsync([FromQuery] TaktMailQueryDto queryDto)
    {
        var result = await _service.GetMailListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取邮件表(Mail)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:mail:query", "查询邮件表(Mail)详情")]
    public async Task<ActionResult<TaktMailDto>> GetMailByIdAsync(long id)
    {
        var item = await _service.GetMailByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取邮件表(Mail)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:mail:query", "查询邮件表(Mail)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMailOptionsAsync()
    {
        var result = await _service.GetMailOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建邮件表(Mail)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:mail:create", "创建邮件表(Mail)")]
    public async Task<ActionResult<TaktMailDto>> CreateMailAsync([FromBody] TaktMailCreateDto dto)
    {
        var result = await _service.CreateMailAsync(dto);
        return CreatedAtAction(nameof(GetMailByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新邮件表(Mail)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:mail:update", "更新邮件表(Mail)")]
    public async Task<ActionResult<TaktMailDto>> UpdateMailAsync(long id, [FromBody] TaktMailUpdateDto dto)
    {
        var result = await _service.UpdateMailAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除邮件表(Mail)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:mail:delete", "删除邮件表(Mail)")]
    public async Task<ActionResult> DeleteMailByIdAsync(long id)
    {
        await _service.DeleteMailByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除邮件表(Mail)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:mail:delete", "批量删除邮件表(Mail)")]
    public async Task<ActionResult> DeleteMailBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMailBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新邮件表(Mail)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:mail:update", "更新邮件表(Mail)状态")]
    public async Task<ActionResult<TaktMailDto>> UpdateMailStatusAsync([FromBody] TaktMailStatusDto dto)
    {
        var result = await _service.UpdateMailStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取邮件表(Mail)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:mail:import", "获取邮件表(Mail)导入模板")]
    public async Task<IActionResult> GetMailTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMailTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入邮件表(Mail)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:mail:import", "导入邮件表(Mail)")]
    public async Task<ActionResult<object>> ImportMailAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportMailAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出邮件表(Mail)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:mail:export", "导出邮件表(Mail)")]
    public async Task<IActionResult> ExportMailAsync([FromBody] TaktMailQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMailAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
