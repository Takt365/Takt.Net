// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktRolesController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色控制器，提供角色管理的RESTful API接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 角色控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "角色管理")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:role", "角色管理")]
public class TaktRolesController : TaktControllerBase
{
    private readonly ITaktRoleService _roleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleService">角色服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktRolesController(
        ITaktRoleService roleService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 获取角色列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPermission("identity:role:list", "查询角色列表")]
    public async Task<ActionResult<TaktPagedResult<TaktRoleDto>>> GetListAsync([FromQuery] TaktRoleQueryDto queryDto)
    {
        var result = await _roleService.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>角色DTO</returns>
    [HttpGet("{id}")]
    [TaktPermission("identity:role:query", "查询角色详情")]
    public async Task<ActionResult<TaktRoleDto>> GetByIdAsync(long id)
    {
        var role = await _roleService.GetByIdAsync(id);
        if (role == null)
            return NotFound();
        return Ok(role);
    }

    /// <summary>
    /// 获取角色选项列表（用于下拉框等）
    /// </summary>
    /// <returns>角色选项列表</returns>
    [HttpGet("options")]
    [TaktPermission("identity:role:list", "查询角色选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetOptionsAsync()
    {
        var options = await _roleService.GetOptionsAsync();
        return Ok(options);
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="dto">创建角色DTO</param>
    /// <returns>角色DTO</returns>
    [HttpPost]
    [TaktPermission("identity:role:create", "创建角色")]
    public async Task<ActionResult<TaktRoleDto>> CreateAsync([FromBody] TaktRoleCreateDto dto)
    {
        var role = await _roleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = role.RoleId }, role);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="dto">更新角色DTO</param>
    /// <returns>角色DTO</returns>
    [HttpPut("{id}")]
    [TaktPermission("identity:role:update", "更新角色")]
    public async Task<ActionResult<TaktRoleDto>> UpdateAsync(long id, [FromBody] TaktRoleUpdateDto dto)
    {
        try
        {
            var role = await _roleService.UpdateAsync(id, dto);
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    [TaktPermission("identity:role:delete", "删除角色")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="dto">角色状态DTO</param>
    /// <returns>角色DTO</returns>
    [HttpPut("status")]
    [TaktPermission("identity:role:status", "更新角色状态")]
    public async Task<ActionResult<TaktRoleDto>> UpdateStatusAsync([FromBody] TaktRoleStatusDto dto)
    {
        try
        {
            var role = await _roleService.UpdateStatusAsync(dto);
            return Ok(role);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取角色菜单列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色菜单列表</returns>
    [HttpGet("{roleId}/menus")]
    [TaktPermission("identity:role:query", "查询角色菜单")]
    public async Task<ActionResult<List<TaktRoleMenuDto>>> GetRoleMenuIdsAsync(long roleId)
    {
        var menus = await _roleService.GetRoleMenuIdsAsync(roleId);
        return Ok(menus);
    }

    /// <summary>
    /// 获取角色权限列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色权限列表</returns>
    [HttpGet("{roleId}/permissions")]
    [TaktPermission("identity:role:query", "查询角色权限")]
    public async Task<ActionResult<List<TaktRolePermissionDto>>> GetRolePermissionIdsAsync(long roleId)
    {
        var permissions = await _roleService.GetRolePermissionIdsAsync(roleId);
        return Ok(permissions);
    }

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色部门列表</returns>
    [HttpGet("{roleId}/depts")]
    [TaktPermission("identity:role:query", "查询角色部门")]
    public async Task<ActionResult<List<TaktRoleDeptDto>>> GetRoleDeptIdsAsync(long roleId)
    {
        var depts = await _roleService.GetRoleDeptIdsAsync(roleId);
        return Ok(depts);
    }

    /// <summary>
    /// 分配角色菜单
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="menuIds">菜单ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{roleId}/menus")]
    [TaktPermission("identity:role:update", "分配角色菜单")]
    public async Task<IActionResult> AssignRoleMenusAsync(long roleId, [FromBody] long[] menuIds)
    {
        try
        {
            var result = await _roleService.AssignRoleMenusAsync(roleId, menuIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 分配角色权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="permissionIds">权限ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{roleId}/permissions")]
    [TaktPermission("identity:role:update", "分配角色权限")]
    public async Task<IActionResult> AssignRolePermissionsAsync(long roleId, [FromBody] long[] permissionIds)
    {
        try
        {
            var result = await _roleService.AssignRolePermissionsAsync(roleId, permissionIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 分配角色部门
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{roleId}/depts")]
    [TaktPermission("identity:role:update", "分配角色部门")]
    public async Task<IActionResult> AssignRoleDeptsAsync(long roleId, [FromBody] long[] deptIds)
    {
        try
        {
            var result = await _roleService.AssignRoleDeptsAsync(roleId, deptIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 分配角色用户
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{roleId}/users")]
    [TaktPermission("identity:role:update", "分配角色用户")]
    public async Task<IActionResult> AssignRoleUsersAsync(long roleId, [FromBody] long[] userIds)
    {
        try
        {
            var result = await _roleService.AssignRoleUsersAsync(roleId, userIds);
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPermission("identity:role:template", "获取导入模板")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _roleService.GetTemplateAsync(sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导入角色
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktPermission("identity:role:import", "导入角色")]
    public async Task<ActionResult<object>> ImportAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("请选择要导入的Excel文件");
            }

            if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
                !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("只支持Excel文件（.xlsx或.xls）");
            }

            using var stream = file.OpenReadStream();
            var (success, fail, errors) = await _roleService.ImportAsync(stream, sheetName);
            return Ok(new { success, fail, errors });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出角色
    /// </summary>
    /// <param name="query">角色查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    [HttpPost("export")]
    [TaktPermission("identity:role:export", "导出角色")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktRoleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _roleService.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}