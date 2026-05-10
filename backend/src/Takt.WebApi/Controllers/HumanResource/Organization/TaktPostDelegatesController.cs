// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktPostDelegatesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位代理表控制器，提供PostDelegate管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 岗位代理表控制器
/// </summary>
[Route("api/[controller]", Name = "岗位代理表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:postdelegate", "岗位代理表管理")]
public class TaktPostDelegatesController : TaktControllerBase
{
    private readonly ITaktPostDelegateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostDelegatesController(
        ITaktPostDelegateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取岗位代理表(PostDelegate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:postdelegate:list", "查询岗位代理表(PostDelegate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPostDelegateDto>>> GetPostDelegateListAsync([FromQuery] TaktPostDelegateQueryDto queryDto)
    {
        var result = await _service.GetPostDelegateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取岗位代理表(PostDelegate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:postdelegate:query", "查询岗位代理表(PostDelegate)详情")]
    public async Task<ActionResult<TaktPostDelegateDto>> GetPostDelegateByIdAsync(long id)
    {
        var item = await _service.GetPostDelegateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取岗位代理表(PostDelegate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:postdelegate:query", "查询岗位代理表(PostDelegate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPostDelegateOptionsAsync()
    {
        var result = await _service.GetPostDelegateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建岗位代理表(PostDelegate)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:postdelegate:create", "创建岗位代理表(PostDelegate)")]
    public async Task<ActionResult<TaktPostDelegateDto>> CreatePostDelegateAsync([FromBody] TaktPostDelegateCreateDto dto)
    {
        var result = await _service.CreatePostDelegateAsync(dto);
        return CreatedAtAction(nameof(GetPostDelegateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新岗位代理表(PostDelegate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:postdelegate:update", "更新岗位代理表(PostDelegate)")]
    public async Task<ActionResult<TaktPostDelegateDto>> UpdatePostDelegateAsync(long id, [FromBody] TaktPostDelegateUpdateDto dto)
    {
        var result = await _service.UpdatePostDelegateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除岗位代理表(PostDelegate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:postdelegate:delete", "删除岗位代理表(PostDelegate)")]
    public async Task<ActionResult> DeletePostDelegateByIdAsync(long id)
    {
        await _service.DeletePostDelegateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除岗位代理表(PostDelegate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:postdelegate:delete", "批量删除岗位代理表(PostDelegate)")]
    public async Task<ActionResult> DeletePostDelegateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePostDelegateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新岗位代理表(PostDelegate)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:organization:postdelegate:update", "更新岗位代理表(PostDelegate)排序")]
    public async Task<ActionResult<TaktPostDelegateDto>> UpdatePostDelegateSortAsync([FromBody] TaktPostDelegateSortDto dto)
    {
        var result = await _service.UpdatePostDelegateSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取岗位代理表(PostDelegate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:postdelegate:import", "获取岗位代理表(PostDelegate)导入模板")]
    public async Task<IActionResult> GetPostDelegateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPostDelegateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入岗位代理表(PostDelegate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:postdelegate:import", "导入岗位代理表(PostDelegate)")]
    public async Task<ActionResult<object>> ImportPostDelegateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPostDelegateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出岗位代理表(PostDelegate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:postdelegate:export", "导出岗位代理表(PostDelegate)")]
    public async Task<IActionResult> ExportPostDelegateAsync([FromBody] TaktPostDelegateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPostDelegateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
