// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktPostsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位信息表控制器，提供Post管理的RESTful API接口
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
/// 岗位信息表控制器
/// </summary>
[Route("api/[controller]", Name = "岗位信息表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:post", "岗位信息表管理")]
public class TaktPostsController : TaktControllerBase
{
    private readonly ITaktPostService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostsController(
        ITaktPostService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取岗位信息表(Post)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:post:list", "查询岗位信息表(Post)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPostDto>>> GetPostListAsync([FromQuery] TaktPostQueryDto queryDto)
    {
        var result = await _service.GetPostListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取岗位信息表(Post)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:post:query", "查询岗位信息表(Post)详情")]
    public async Task<ActionResult<TaktPostDto>> GetPostByIdAsync(long id)
    {
        var item = await _service.GetPostByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取岗位信息表(Post)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:post:query", "查询岗位信息表(Post)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPostOptionsAsync()
    {
        var result = await _service.GetPostOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建岗位信息表(Post)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:post:create", "创建岗位信息表(Post)")]
    public async Task<ActionResult<TaktPostDto>> CreatePostAsync([FromBody] TaktPostCreateDto dto)
    {
        var result = await _service.CreatePostAsync(dto);
        return CreatedAtAction(nameof(GetPostByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新岗位信息表(Post)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:post:update", "更新岗位信息表(Post)")]
    public async Task<ActionResult<TaktPostDto>> UpdatePostAsync(long id, [FromBody] TaktPostUpdateDto dto)
    {
        var result = await _service.UpdatePostAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除岗位信息表(Post)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:post:delete", "删除岗位信息表(Post)")]
    public async Task<ActionResult> DeletePostByIdAsync(long id)
    {
        await _service.DeletePostByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除岗位信息表(Post)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:post:delete", "批量删除岗位信息表(Post)")]
    public async Task<ActionResult> DeletePostBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePostBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新岗位信息表(Post)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:organization:post:update", "更新岗位信息表(Post)状态")]
    public async Task<ActionResult<TaktPostDto>> UpdatePostStatusAsync([FromBody] TaktPostStatusDto dto)
    {
        var result = await _service.UpdatePostStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新岗位信息表(Post)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:organization:post:update", "更新岗位信息表(Post)排序")]
    public async Task<ActionResult<TaktPostDto>> UpdatePostSortAsync([FromBody] TaktPostSortDto dto)
    {
        var result = await _service.UpdatePostSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取岗位信息表(Post)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:post:import", "获取岗位信息表(Post)导入模板")]
    public async Task<IActionResult> GetPostTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPostTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入岗位信息表(Post)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:post:import", "导入岗位信息表(Post)")]
    public async Task<ActionResult<object>> ImportPostAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPostAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出岗位信息表(Post)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:post:export", "导出岗位信息表(Post)")]
    public async Task<IActionResult> ExportPostAsync([FromBody] TaktPostQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPostAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
