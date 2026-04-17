// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Organization
// 文件名称：TaktPostsController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt岗位控制器，提供岗位管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Services.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Organization;

/// <summary>
/// 岗位控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "岗位")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:organization:post:list", "岗位管理")]
public class TaktPostsController : TaktControllerBase
{
    private readonly ITaktPostService _postService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postService">岗位服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktPostsController(
        ITaktPostService postService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _postService = postService;
    }

    /// <summary>
    /// 获取岗位列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("humanresource:organization:post:list", "查询岗位列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPostDto>>> GetPostListAsync([FromQuery] TaktPostQueryDto queryDto)
    {
        var result = await _postService.GetPostListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>岗位DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:organization:post:detail", "查询岗位详情")]
    public async Task<ActionResult<TaktPostDto>> GetPostByIdAsync(long id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    /// <summary>
    /// 获取岗位选项列表（用于下拉框等）
    /// </summary>
    /// <returns>岗位选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("humanresource:organization:post:list", "查询岗位选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPostOptionsAsync()
    {
        var options = await _postService.GetPostOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建岗位
    /// </summary>
    /// <param name="dto">创建岗位DTO</param>
    /// <returns>岗位DTO</returns>
    [HttpPost]
    [TaktPermission("humanresource:organization:post:create", "创建岗位")]
    public async Task<ActionResult<TaktPostDto>> CreatePostAsync([FromBody] TaktPostCreateDto dto)
    {
        var post = await _postService.CreatePostAsync(dto);
        return CreatedAtAction(nameof(GetPostByIdAsync), new { id = post.PostId }, post);
    }

    /// <summary>
    /// 更新岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <param name="dto">更新岗位DTO</param>
    /// <returns>岗位DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:organization:post:update", "更新岗位")]
    public async Task<ActionResult<TaktPostDto>> UpdatePostAsync(long id, [FromBody] TaktPostUpdateDto dto)
    {
        try
        {
            var post = await _postService.UpdatePostAsync(id, dto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id">岗位ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:organization:post:delete", "删除岗位")]
    public async Task<IActionResult> DeletePostByIdAsync(long id)
    {
        await _postService.DeletePostByIdAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新岗位状态
    /// </summary>
    /// <param name="dto">岗位状态DTO</param>
    /// <returns>岗位DTO</returns>
    [HttpPut("status")]
    [TaktPermission("humanresource:organization:post:update", "更新岗位状态")]
    public async Task<ActionResult<TaktPostDto>> UpdatePostStatusAsync([FromBody] TaktPostStatusDto dto)
    {
        try
        {
            var post = await _postService.UpdatePostStatusAsync(dto);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取岗位用户列表
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <returns>岗位用户列表</returns>
    [HttpGet("{postId}/users")]
    [TaktPermission("humanresource:organization:post:query", "查询岗位用户")]
    public async Task<ActionResult<List<TaktUserPostDto>>> GetUserPostIdsAsync(long postId)
    {
        var users = await _postService.GetUserPostIdsAsync(postId);
        return Ok(users);
    }

    /// <summary>
    /// 分配岗位用户
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <param name="userIds">用户ID集合</param>
    /// <returns>操作结果</returns>
    [HttpPut("{postId}/users")]
    [TaktPermission("humanresource:organization:post:update", "分配岗位用户")]
    public async Task<IActionResult> AssignUserPostsAsync(long postId, [FromBody] long[] userIds)
    {
        try
        {
            var result = await _postService.AssignUserPostsAsync(postId, userIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("humanresource:organization:post:template", "获取导入模板")]
    public async Task<IActionResult> GetPostTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _postService.GetPostTemplateAsync(sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.ExcelContentType, resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导入岗位
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("humanresource:organization:post:import", "导入岗位")]
    public async Task<ActionResult<object>> ImportPostAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
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
            var (success, fail, errors) = await _postService.ImportPostAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 导出岗位
    /// </summary>
    /// <param name="query">岗位查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("humanresource:organization:post:export", "导出岗位")]
    public async Task<IActionResult> ExportPostAsync([FromBody] TaktPostQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _postService.ExportPostAsync(query, sheetName, fileName);
            return File(content, TaktExcelExportFileHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}