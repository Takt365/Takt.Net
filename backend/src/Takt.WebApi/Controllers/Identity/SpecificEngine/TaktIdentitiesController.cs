// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity.SpecificEngine
// 文件名称：TaktIdentitiesController.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：身份专用控制器，提供菜单树等树形API
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity.SpecificEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity.SpecificEngine;

/// <summary>
/// 身份专用控制器
/// </summary>
[Route("api/[controller]", Name = "身份专用")]
[ApiModule("Identity", "身份认证")]
[TaktPermission("identity:identity:specific", "身份专用管理")]
public class TaktIdentitiesController : TaktControllerBase
{
    private readonly ITaktIdentityService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIdentitiesController(
        ITaktIdentityService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    #region 菜单树形选项

    /// <summary>
    /// 获取菜单树形选项（含按钮 MenuType=2），用于角色分配菜单等需勾选按钮权限的场景。
    /// </summary>
    /// <returns>菜单树形选项根节点集合（含按钮）</returns>
    [HttpGet("menu-tree-options-with-button")]
    [TaktPermission("identity:identity:menu:options:button", "菜单树形选项（含按钮）查询")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetMenuTreeOptionsWithButtonAsync()
    {
        var result = await _service.GetMenuTreeOptionsWithButtonAsync();
        return Ok(result);
    }

    #endregion

    #region 菜单目录树

    /// <summary>
    /// 获取模块名称用目录树（仅 MenuType=0），用于代码生成中的模块列表。
    /// </summary>
    /// <returns>目录级菜单树根节点集合</returns>
    [HttpGet("menu-directory-tree")]
    [TaktPermission("identity:identity:menu:directory", "菜单目录树查询")]
    public async Task<ActionResult<List<TaktMenuTreeDto>>> GetMenuDirectoryTreeAsync()
    {
        var result = await _service.GetMenuDirectoryTreeAsync();
        return Ok(result);
    }

    /// <summary>
    /// 获取当前用户可见的菜单树（按权限过滤）。
    /// </summary>
    /// <returns>当前用户菜单树根节点集合</returns>
    [HttpGet("current-user-menu-tree")]
    [TaktPermission("identity:identity:menu:current", "当前用户菜单树查询")]
    public async Task<ActionResult<List<TaktMenuTreeDto>>> GetCurrentUserMenuTreeAsync()
    {
        var result = await _service.GetCurrentUserMenuTreeAsync();
        return Ok(result);
    }

    #endregion

    #region 用户管理

    /// <summary>
    /// 重置密码（管理员操作）
    /// </summary>
    /// <param name="dto">重置密码DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("reset-password")]
    [TaktPermission("identity:identity:user:resetpwd", "重置用户密码")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] TaktUserResetPwdDto dto)
    {
        await _service.ResetPasswordAsync(dto);
        return Ok();
    }

    /// <summary>
    /// 修改密码（当前用户操作）
    /// </summary>
    /// <param name="dto">修改密码DTO</param>
    /// <returns>操作结果</returns>
    [HttpPost("change-password")]
    [TaktPermission("identity:identity:user:changepwd", "修改密码")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] TaktUserChangePwdDto dto)
    {
        await _service.ChangePasswordAsync(dto);
        return Ok();
    }

    /// <summary>
    /// 忘记密码（发送密码重置邮件）
    /// </summary>
    /// <param name="dto">忘记密码DTO</param>
    /// <returns>忘记密码结果</returns>
    [HttpPost("forgot-password")]
    [TaktPermission("identity:identity:user:forgotpwd", "忘记密码")]
    public async Task<ActionResult<TaktUserForgotPasswordResultDto>> ForgotPasswordAsync([FromBody] TaktUserForgotPasswordDto dto)
    {
        var result = await _service.ForgotPasswordAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 解锁用户
    /// </summary>
    /// <param name="dto">解锁用户DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPost("unlock-user")]
    [TaktPermission("identity:identity:user:unlock", "解锁用户")]
    public async Task<ActionResult<TaktUserDto>> UnlockUserAsync([FromBody] TaktUserUnlockDto dto)
    {
        var result = await _service.UnlockAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="dto">用户头像更新DTO</param>
    /// <returns>用户DTO</returns>
    [HttpPost("update-avatar")]
    [TaktPermission("identity:identity:user:avatar", "更新头像")]
    public async Task<ActionResult<TaktUserDto>> UpdateAvatarAsync([FromBody] TaktUserAvatarUpdateDto dto)
    {
        var result = await _service.UpdateAvatarAsync(dto);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户角色列表</returns>
    [HttpGet("user/{userId}/roles")]
    [TaktPermission("identity:identity:user:roles", "获取用户角色")]
    public async Task<ActionResult<List<TaktUserRoleDto>>> GetUserRolesAsync(long userId)
    {
        var result = await _service.GetUserRoleIdsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户部门列表</returns>
    [HttpGet("user/{userId}/depts")]
    [TaktPermission("identity:identity:user:depts", "获取用户部门")]
    public async Task<ActionResult<List<TaktUserDeptDto>>> GetUserDeptsAsync(long userId)
    {
        var result = await _service.GetUserDeptIdsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户岗位列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户岗位列表</returns>
    [HttpGet("user/{userId}/posts")]
    [TaktPermission("identity:identity:user:posts", "获取用户岗位")]
    public async Task<ActionResult<List<TaktUserPostDto>>> GetUserPostsAsync(long userId)
    {
        var result = await _service.GetUserPostIdsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    [HttpGet("user/{userId}/tenants")]
    [TaktPermission("identity:identity:user:tenants", "获取用户租户")]
    public async Task<ActionResult<List<TaktUserTenantDto>>> GetUserTenantsAsync(long userId)
    {
        var result = await _service.GetUserTenantIdsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 分配用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="dto">角色ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("user/{userId}/assign-roles")]
    [TaktPermission("identity:identity:user:assign:roles", "分配用户角色")]
    public async Task<ActionResult> AssignUserRolesAsync(long userId, [FromBody] long[] dto)
    {
        var result = await _service.AssignUserRolesAsync(userId, dto);
        return Ok(result);
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="dto">部门ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("user/{userId}/assign-depts")]
    [TaktPermission("identity:identity:user:assign:depts", "分配用户部门")]
    public async Task<ActionResult> AssignUserDeptsAsync(long userId, [FromBody] long[] dto)
    {
        var result = await _service.AssignUserDeptsAsync(userId, dto);
        return Ok(result);
    }

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="dto">岗位ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("user/{userId}/assign-posts")]
    [TaktPermission("identity:identity:user:assign:posts", "分配用户岗位")]
    public async Task<ActionResult> AssignUserPostsAsync(long userId, [FromBody] long[] dto)
    {
        var result = await _service.AssignUserPostsAsync(userId, dto);
        return Ok(result);
    }

    /// <summary>
    /// 分配用户租户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="dto">租户ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("user/{userId}/assign-tenants")]
    [TaktPermission("identity:identity:user:assign:tenants", "分配用户租户")]
    public async Task<ActionResult> AssignUserTenantsAsync(long userId, [FromBody] long[] dto)
    {
        var result = await _service.AssignUserTenantsAsync(userId, dto);
        return Ok(result);
    }

    #endregion

    #region 统计分析

    /// <summary>
    /// 获取用户总数统计
    /// </summary>
    /// <returns>用户总数</returns>
    [HttpGet("stats/user-count")]
    [TaktPermission("identity:identity:stats:usercount", "用户统计")]
    public async Task<ActionResult<long>> GetUserCountAsync()
    {
        var result = await _service.GetUserCountAsync();
        return Ok(result);
    }

    #endregion
}