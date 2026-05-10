// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktRoleMenusController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：角色菜单关联表控制器，提供RoleMenu管理的RESTful API接口
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
/// 角色菜单关联表控制器
/// </summary>
[Route("api/[controller]", Name = "角色菜单关联表")]
[ApiModule("System", "系统管理")]
[TaktPermission("identity:rolemenu", "角色菜单关联表管理")]
public class TaktRoleMenusController : TaktControllerBase
{
    private readonly ITaktRoleMenuService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleMenusController(
        ITaktRoleMenuService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取角色菜单关联表(RoleMenu)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("identity:rolemenu:list", "查询角色菜单关联表(RoleMenu)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktRoleMenuDto>>> GetRoleMenuListAsync([FromQuery] TaktRoleMenuQueryDto queryDto)
    {
        var result = await _service.GetRoleMenuListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("identity:rolemenu:query", "查询角色菜单关联表(RoleMenu)详情")]
    public async Task<ActionResult<TaktRoleMenuDto>> GetRoleMenuByIdAsync(long id)
    {
        var item = await _service.GetRoleMenuByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取角色菜单关联表(RoleMenu)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("identity:rolemenu:query", "查询角色菜单关联表(RoleMenu)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetRoleMenuOptionsAsync()
    {
        var result = await _service.GetRoleMenuOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpPost]
    [TaktPermission("identity:rolemenu:create", "创建角色菜单关联表(RoleMenu)")]
    public async Task<ActionResult<TaktRoleMenuDto>> CreateRoleMenuAsync([FromBody] TaktRoleMenuCreateDto dto)
    {
        var result = await _service.CreateRoleMenuAsync(dto);
        return CreatedAtAction(nameof(GetRoleMenuByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("identity:rolemenu:update", "更新角色菜单关联表(RoleMenu)")]
    public async Task<ActionResult<TaktRoleMenuDto>> UpdateRoleMenuAsync(long id, [FromBody] TaktRoleMenuUpdateDto dto)
    {
        var result = await _service.UpdateRoleMenuAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("identity:rolemenu:delete", "删除角色菜单关联表(RoleMenu)")]
    public async Task<ActionResult> DeleteRoleMenuByIdAsync(long id)
    {
        await _service.DeleteRoleMenuByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("identity:rolemenu:delete", "批量删除角色菜单关联表(RoleMenu)")]
    public async Task<ActionResult> DeleteRoleMenuBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteRoleMenuBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取角色菜单关联表(RoleMenu)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("identity:rolemenu:import", "获取角色菜单关联表(RoleMenu)导入模板")]
    public async Task<IActionResult> GetRoleMenuTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetRoleMenuTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("identity:rolemenu:import", "导入角色菜单关联表(RoleMenu)")]
    public async Task<ActionResult<object>> ImportRoleMenuAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportRoleMenuAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出角色菜单关联表(RoleMenu)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("identity:rolemenu:export", "导出角色菜单关联表(RoleMenu)")]
    public async Task<IActionResult> ExportRoleMenuAsync([FromBody] TaktRoleMenuQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportRoleMenuAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
