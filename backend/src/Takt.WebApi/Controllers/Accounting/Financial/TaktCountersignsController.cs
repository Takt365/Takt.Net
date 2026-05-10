// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial
// 文件名称：TaktCountersignsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：会签单表控制器，提供Countersign管理的RESTful API接口
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
/// 会签单表控制器
/// </summary>
[Route("api/[controller]", Name = "会签单表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:financial:countersign", "会签单表管理")]
public class TaktCountersignsController : TaktControllerBase
{
    private readonly ITaktCountersignService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCountersignsController(
        ITaktCountersignService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取会签单表(Countersign)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:financial:countersign:list", "查询会签单表(Countersign)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCountersignDto>>> GetCountersignListAsync([FromQuery] TaktCountersignQueryDto queryDto)
    {
        var result = await _service.GetCountersignListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取会签单表(Countersign)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:financial:countersign:query", "查询会签单表(Countersign)详情")]
    public async Task<ActionResult<TaktCountersignDto>> GetCountersignByIdAsync(long id)
    {
        var item = await _service.GetCountersignByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取会签单表(Countersign)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:financial:countersign:query", "查询会签单表(Countersign)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCountersignOptionsAsync()
    {
        var result = await _service.GetCountersignOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建会签单表(Countersign)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:financial:countersign:create", "创建会签单表(Countersign)")]
    public async Task<ActionResult<TaktCountersignDto>> CreateCountersignAsync([FromBody] TaktCountersignCreateDto dto)
    {
        var result = await _service.CreateCountersignAsync(dto);
        return CreatedAtAction(nameof(GetCountersignByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新会签单表(Countersign)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:financial:countersign:update", "更新会签单表(Countersign)")]
    public async Task<ActionResult<TaktCountersignDto>> UpdateCountersignAsync(long id, [FromBody] TaktCountersignUpdateDto dto)
    {
        var result = await _service.UpdateCountersignAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除会签单表(Countersign)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:financial:countersign:delete", "删除会签单表(Countersign)")]
    public async Task<ActionResult> DeleteCountersignByIdAsync(long id)
    {
        await _service.DeleteCountersignByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除会签单表(Countersign)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:financial:countersign:delete", "批量删除会签单表(Countersign)")]
    public async Task<ActionResult> DeleteCountersignBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCountersignBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新会签单表(Countersign)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:financial:countersign:update", "更新会签单表(Countersign)状态")]
    public async Task<ActionResult<TaktCountersignDto>> UpdateCountersignStatusAsync([FromBody] TaktCountersignStatusDto dto)
    {
        var result = await _service.UpdateCountersignStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取会签单表(Countersign)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:financial:countersign:import", "获取会签单表(Countersign)导入模板")]
    public async Task<IActionResult> GetCountersignTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCountersignTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入会签单表(Countersign)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:financial:countersign:import", "导入会签单表(Countersign)")]
    public async Task<ActionResult<object>> ImportCountersignAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCountersignAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出会签单表(Countersign)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:financial:countersign:export", "导出会签单表(Countersign)")]
    public async Task<IActionResult> ExportCountersignAsync([FromBody] TaktCountersignQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCountersignAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
