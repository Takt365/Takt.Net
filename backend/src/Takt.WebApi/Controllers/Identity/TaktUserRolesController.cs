// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktUserRolesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：用户角色关联表控制器，提供UserRole管理的RESTful API接口
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
/// 用户角色关联表控制器
/// </summary>
[Route("api/[controller]", Name = "用户角色关联表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:userrole", "用户角色关联表管理")]
public class TaktUserRolesController : TaktControllerBase
{
    private readonly ITaktUserRoleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserRolesController(
        ITaktUserRoleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取用户角色关联表(UserRole)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:userrole:list", "查询用户角色关联表(UserRole)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktUserRoleDto>>> GetUserRoleListAsync([FromQuery] TaktUserRoleQueryDto queryDto)
    {
        var result = await _service.GetUserRoleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取用户角色关联表(UserRole)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:userrole:query", "查询用户角色关联表(UserRole)详情")]
    public async Task<ActionResult<TaktUserRoleDto>> GetUserRoleByIdAsync(long id)
    {
        var item = await _service.GetUserRoleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取用户角色关联表(UserRole)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:userrole:query", "查询用户角色关联表(UserRole)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetUserRoleOptionsAsync()
    {
        var result = await _service.GetUserRoleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建用户角色关联表(UserRole)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:userrole:create", "创建用户角色关联表(UserRole)")]
    public async Task<ActionResult<TaktUserRoleDto>> CreateUserRoleAsync([FromBody] TaktUserRoleCreateDto dto)
    {
        var result = await _service.CreateUserRoleAsync(dto);
        return CreatedAtAction(nameof(GetUserRoleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新用户角色关联表(UserRole)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:userrole:update", "更新用户角色关联表(UserRole)")]
    public async Task<ActionResult<TaktUserRoleDto>> UpdateUserRoleAsync(long id, [FromBody] TaktUserRoleUpdateDto dto)
    {
        var result = await _service.UpdateUserRoleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除用户角色关联表(UserRole)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:userrole:delete", "删除用户角色关联表(UserRole)")]
    public async Task<ActionResult> DeleteUserRoleByIdAsync(long id)
    {
        await _service.DeleteUserRoleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除用户角色关联表(UserRole)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:userrole:delete", "批量删除用户角色关联表(UserRole)")]
    public async Task<ActionResult> DeleteUserRoleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteUserRoleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取用户角色关联表(UserRole)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:userrole:import", "获取用户角色关联表(UserRole)导入模板")]
    public async Task<IActionResult> GetUserRoleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetUserRoleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入用户角色关联表(UserRole)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:userrole:import", "导入用户角色关联表(UserRole)")]
    public async Task<ActionResult<object>> ImportUserRoleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportUserRoleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出用户角色关联表(UserRole)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:userrole:export", "导出用户角色关联表(UserRole)")]
    public async Task<IActionResult> ExportUserRoleAsync([FromBody] TaktUserRoleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportUserRoleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
