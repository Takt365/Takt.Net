// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Organization
// 文件名称：TaktUserPostsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位用户关联表控制器，提供UserPost管理的RESTful API接口
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
/// 岗位用户关联表控制器
/// </summary>
[Route("api/[controller]", Name = "岗位用户关联表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:userpost", "岗位用户关联表管理")]
public class TaktUserPostsController : TaktControllerBase
{
    private readonly ITaktUserPostService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserPostsController(
        ITaktUserPostService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取岗位用户关联表(UserPost)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:userpost:list", "查询岗位用户关联表(UserPost)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserPostDto>>> GetUserPostListAsync([FromQuery] TaktUserPostQueryDto queryDto)
    {
        var result = await _service.GetUserPostListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取岗位用户关联表(UserPost)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:userpost:query", "查询岗位用户关联表(UserPost)详情")]
    public async Task<ActionResult<TaktUserPostDto>> GetUserPostByIdAsync(long id)
    {
        var item = await _service.GetUserPostByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取岗位用户关联表(UserPost)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:userpost:query", "查询岗位用户关联表(UserPost)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserPostOptionsAsync()
    {
        var result = await _service.GetUserPostOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建岗位用户关联表(UserPost)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:organization:userpost:create", "创建岗位用户关联表(UserPost)")]
    public async Task<ActionResult<TaktUserPostDto>> CreateUserPostAsync([FromBody] TaktUserPostCreateDto dto)
    {
        var result = await _service.CreateUserPostAsync(dto);
        return CreatedAtAction(nameof(GetUserPostByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新岗位用户关联表(UserPost)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:userpost:update", "更新岗位用户关联表(UserPost)")]
    public async Task<ActionResult<TaktUserPostDto>> UpdateUserPostAsync(long id, [FromBody] TaktUserPostUpdateDto dto)
    {
        var result = await _service.UpdateUserPostAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除岗位用户关联表(UserPost)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:userpost:delete", "删除岗位用户关联表(UserPost)")]
    public async Task<ActionResult> DeleteUserPostByIdAsync(long id)
    {
        await _service.DeleteUserPostByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除岗位用户关联表(UserPost)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:organization:userpost:delete", "批量删除岗位用户关联表(UserPost)")]
    public async Task<ActionResult> DeleteUserPostBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteUserPostBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取岗位用户关联表(UserPost)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:userpost:import", "获取岗位用户关联表(UserPost)导入模板")]
    public async Task<IActionResult> GetUserPostTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetUserPostTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入岗位用户关联表(UserPost)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:userpost:import", "导入岗位用户关联表(UserPost)")]
    public async Task<ActionResult<object>> ImportUserPostAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportUserPostAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出岗位用户关联表(UserPost)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:userpost:export", "导出岗位用户关联表(UserPost)")]
    public async Task<IActionResult> ExportUserPostAsync([FromBody] TaktUserPostQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportUserPostAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
