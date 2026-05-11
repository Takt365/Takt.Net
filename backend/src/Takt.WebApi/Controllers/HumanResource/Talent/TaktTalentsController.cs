// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Talent
// 文件名称：TaktTalentsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：人才管理表控制器，提供Talent管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Talent;
using Takt.Application.Services.HumanResource.Talent;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Talent;

/// <summary>
/// 人才管理表控制器
/// </summary>
[Route("api/[controller]", Name = "人才管理表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:talent", "人才管理表管理")]
public class TaktTalentsController : TaktControllerBase
{
    private readonly ITaktTalentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentsController(
        ITaktTalentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取人才管理表(Talent)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:talent:list", "查询人才管理表(Talent)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTalentDto>>> GetTalentListAsync([FromQuery] TaktTalentQueryDto queryDto)
    {
        var result = await _service.GetTalentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取人才管理表(Talent)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:talent:query", "查询人才管理表(Talent)详情")]
    public async Task<ActionResult<TaktTalentDto>> GetTalentByIdAsync(long id)
    {
        var item = await _service.GetTalentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取人才管理表(Talent)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:talent:query", "查询人才管理表(Talent)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTalentOptionsAsync()
    {
        var result = await _service.GetTalentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建人才管理表(Talent)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:talent:create", "创建人才管理表(Talent)")]
    public async Task<ActionResult<TaktTalentDto>> CreateTalentAsync([FromBody] TaktTalentCreateDto dto)
    {
        var result = await _service.CreateTalentAsync(dto);
        return CreatedAtAction(nameof(GetTalentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新人才管理表(Talent)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:talent:update", "更新人才管理表(Talent)")]
    public async Task<ActionResult<TaktTalentDto>> UpdateTalentAsync(long id, [FromBody] TaktTalentUpdateDto dto)
    {
        var result = await _service.UpdateTalentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除人才管理表(Talent)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:talent:delete", "删除人才管理表(Talent)")]
    public async Task<ActionResult> DeleteTalentByIdAsync(long id)
    {
        await _service.DeleteTalentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除人才管理表(Talent)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:talent:delete", "批量删除人才管理表(Talent)")]
    public async Task<ActionResult> DeleteTalentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTalentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新人才管理表(Talent)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:talent:update", "更新人才管理表(Talent)状态")]
    public async Task<ActionResult<TaktTalentDto>> UpdateTalentStatusAsync([FromBody] TaktTalentStatusDto dto)
    {
        var result = await _service.UpdateTalentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取人才管理表(Talent)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:talent:import", "获取人才管理表(Talent)导入模板")]
    public async Task<IActionResult> GetTalentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTalentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入人才管理表(Talent)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:talent:import", "导入人才管理表(Talent)")]
    public async Task<ActionResult<object>> ImportTalentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTalentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出人才管理表(Talent)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:talent:export", "导出人才管理表(Talent)")]
    public async Task<IActionResult> ExportTalentAsync([FromBody] TaktTalentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTalentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
