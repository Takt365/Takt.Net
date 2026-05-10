// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachmentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：设变附件表控制器，提供EcAttachment管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Application.Services.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变附件表控制器
/// </summary>
[Route("api/[controller]", Name = "设变附件表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:engineeringchange:ecattachment", "设变附件表管理")]
public class TaktEcAttachmentsController : TaktControllerBase
{
    private readonly ITaktEcAttachmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcAttachmentsController(
        ITaktEcAttachmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:list", "查询设变附件表(EcAttachment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEcAttachmentDto>>> GetEcAttachmentListAsync([FromQuery] TaktEcAttachmentQueryDto queryDto)
    {
        var result = await _service.GetEcAttachmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取设变附件表(EcAttachment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:query", "查询设变附件表(EcAttachment)详情")]
    public async Task<ActionResult<TaktEcAttachmentDto>> GetEcAttachmentByIdAsync(long id)
    {
        var item = await _service.GetEcAttachmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:query", "查询设变附件表(EcAttachment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEcAttachmentOptionsAsync()
    {
        var result = await _service.GetEcAttachmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建设变附件表(EcAttachment)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:create", "创建设变附件表(EcAttachment)")]
    public async Task<ActionResult<TaktEcAttachmentDto>> CreateEcAttachmentAsync([FromBody] TaktEcAttachmentCreateDto dto)
    {
        var result = await _service.CreateEcAttachmentAsync(dto);
        return CreatedAtAction(nameof(GetEcAttachmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新设变附件表(EcAttachment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:update", "更新设变附件表(EcAttachment)")]
    public async Task<ActionResult<TaktEcAttachmentDto>> UpdateEcAttachmentAsync(long id, [FromBody] TaktEcAttachmentUpdateDto dto)
    {
        var result = await _service.UpdateEcAttachmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除设变附件表(EcAttachment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:delete", "删除设变附件表(EcAttachment)")]
    public async Task<ActionResult> DeleteEcAttachmentByIdAsync(long id)
    {
        await _service.DeleteEcAttachmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除设变附件表(EcAttachment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:delete", "批量删除设变附件表(EcAttachment)")]
    public async Task<ActionResult> DeleteEcAttachmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEcAttachmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新设变附件表(EcAttachment)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:update", "更新设变附件表(EcAttachment)排序")]
    public async Task<ActionResult<TaktEcAttachmentDto>> UpdateEcAttachmentSortAsync([FromBody] TaktEcAttachmentSortDto dto)
    {
        var result = await _service.UpdateEcAttachmentSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取设变附件表(EcAttachment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:import", "获取设变附件表(EcAttachment)导入模板")]
    public async Task<IActionResult> GetEcAttachmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEcAttachmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入设变附件表(EcAttachment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:import", "导入设变附件表(EcAttachment)")]
    public async Task<ActionResult<object>> ImportEcAttachmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEcAttachmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出设变附件表(EcAttachment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:engineeringchange:ecattachment:export", "导出设变附件表(EcAttachment)")]
    public async Task<IActionResult> ExportEcAttachmentAsync([FromBody] TaktEcAttachmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEcAttachmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
