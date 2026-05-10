// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktUsersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：用户信息表控制器，提供User管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 用户信息表控制器
/// </summary>
[Route("api/[controller]", Name = "用户信息表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:user", "用户信息表管理")]
public class TaktUsersController : TaktControllerBase
{
    private readonly ITaktUserService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUsersController(
        ITaktUserService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取用户信息表(User)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:user:list", "查询用户信息表(User)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserDto>>> GetUserListAsync([FromQuery] TaktUserQueryDto queryDto)
    {
        var result = await _service.GetUserListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取用户信息表(User)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:user:query", "查询用户信息表(User)详情")]
    public async Task<ActionResult<TaktUserDto>> GetUserByIdAsync(long id)
    {
        var item = await _service.GetUserByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取用户信息表(User)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:user:query", "查询用户信息表(User)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserOptionsAsync()
    {
        var result = await _service.GetUserOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建用户信息表(User)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:user:create", "创建用户信息表(User)")]
    public async Task<ActionResult<TaktUserDto>> CreateUserAsync([FromBody] TaktUserCreateDto dto)
    {
        var result = await _service.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUserByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新用户信息表(User)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:user:update", "更新用户信息表(User)")]
    public async Task<ActionResult<TaktUserDto>> UpdateUserAsync(long id, [FromBody] TaktUserUpdateDto dto)
    {
        var result = await _service.UpdateUserAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除用户信息表(User)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:user:delete", "删除用户信息表(User)")]
    public async Task<ActionResult> DeleteUserByIdAsync(long id)
    {
        await _service.DeleteUserByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除用户信息表(User)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:user:delete", "批量删除用户信息表(User)")]
    public async Task<ActionResult> DeleteUserBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteUserBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新用户信息表(User)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("identity:user:update", "更新用户信息表(User)状态")]
    public async Task<ActionResult<TaktUserDto>> UpdateUserStatusAsync([FromBody] TaktUserStatusDto dto)
    {
        var result = await _service.UpdateUserStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取用户信息表(User)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:user:import", "获取用户信息表(User)导入模板")]
    public async Task<IActionResult> GetUserTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetUserTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入用户信息表(User)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:user:import", "导入用户信息表(User)")]
    public async Task<ActionResult<object>> ImportUserAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportUserAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出用户信息表(User)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:user:export", "导出用户信息表(User)")]
    public async Task<IActionResult> ExportUserAsync([FromBody] TaktUserQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportUserAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
