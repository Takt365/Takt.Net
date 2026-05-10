// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity.SpecificEngine
// 文件名称：TaktIdentityService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：身份认证树形服务，提供菜单树等树形结构的业务操作
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
namespace Takt.Application.Services.Identity.SpecificEngine;

/// <summary>
/// 身份认证专用服务
/// </summary>
public class TaktIdentityService : TaktServiceBase, ITaktIdentityService
{
    private readonly ITaktRepository<TaktMenu> _menuRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktUserDept> _userDeptRepository;
    private readonly ITaktRepository<TaktUserPost> _userPostRepository;
    private readonly ITaktRepository<TaktUserTenant> _userTenantRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuRepository">菜单仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="userRoleRepository">用户角色仓储</param>
    /// <param name="userDeptRepository">用户部门仓储</param>
    /// <param name="userPostRepository">用户岗位仓储</param>
    /// <param name="userTenantRepository">用户租户仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktIdentityService(
        ITaktRepository<TaktMenu> menuRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktUserDept> userDeptRepository,
        ITaktRepository<TaktUserPost> userPostRepository,
        ITaktRepository<TaktUserTenant> userTenantRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _menuRepository = menuRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _userDeptRepository = userDeptRepository;
        _userPostRepository = userPostRepository;
        _userTenantRepository = userTenantRepository;
    }

    /// <summary>
    /// 获取菜单树形选项（含按钮 MenuType=2），用于角色分配菜单等需勾选按钮权限的场景。
    /// </summary>
    /// <returns>菜单树形选项根节点集合（含按钮）</returns>
    public async Task<List<TaktTreeSelectOption>> GetMenuTreeOptionsWithButtonAsync()
    {
        // 查询所有启用的菜单（包括目录、菜单、按钮）
        var all = await _menuRepository.FindAsync(x => x.MenuStatus == 1);
        return BuildTreeOptionsWithButton(all, 0);
    }

    /// <summary>
    /// 构建含按钮的树形选项列表（递归）
    /// </summary>
    private List<TaktTreeSelectOption> BuildTreeOptionsWithButton(List<TaktMenu> all, long parentId)
    {
        var result = new List<TaktTreeSelectOption>();
        var children = all.Where(x => x.ParentId == parentId).OrderBy(x => x.SortOrder);

        foreach (var item in children)
        {
            var option = new TaktTreeSelectOption
            {
                DictValue = item.Id.ToString(),
                DictLabel = item.MenuName,
                SortOrder = item.SortOrder
            };
            var childOptions = BuildTreeOptionsWithButton(all, item.Id);
            if (childOptions.Count > 0)
            {
                option.Children = childOptions;
            }
            result.Add(option);
        }

        return result;
    }

    /// <summary>
    /// 获取模块名称用目录树（仅 MenuType=0），用于代码生成中的模块列表。
    /// </summary>
    /// <returns>目录级菜单树根节点集合</returns>
    public async Task<List<TaktMenuTreeDto>> GetMenuDirectoryTreeAsync()
    {
        // 查询所有启用的目录（MenuType=0）
        var allDirectories = await _menuRepository.FindAsync(x => x.MenuStatus == 1 && x.MenuType == 0);
        var result = new List<TaktMenuTreeDto>();

        foreach (var directory in allDirectories.OrderBy(x => x.SortOrder))
        {
            var treeDto = directory.Adapt<TaktMenuTreeDto>();
            // 递归加载子节点
            treeDto.Children = await GetDirectoryChildrenAsync(directory.Id, true);
            result.Add(treeDto);
        }

        return result;
    }

    /// <summary>
    /// 获取目录的子节点（递归）
    /// </summary>
    private async Task<List<TaktMenuTreeDto>> GetDirectoryChildrenAsync(long parentId, bool includeDisabled)
    {
        Expression<Func<TaktMenu, bool>> predicate = includeDisabled
            ? x => x.ParentId == parentId
            : x => x.ParentId == parentId && x.MenuStatus == 1;

        var children = await _menuRepository.FindAsync(predicate);
        var result = new List<TaktMenuTreeDto>();

        foreach (var child in children.OrderBy(x => x.SortOrder))
        {
            var treeDto = child.Adapt<TaktMenuTreeDto>();
            // 递归加载子节点
            treeDto.Children = await GetDirectoryChildrenAsync(child.Id, includeDisabled);
            result.Add(treeDto);
        }

        return result;
    }

    /// <summary>
    /// 获取当前用户可见的菜单树（按权限过滤）。
    /// </summary>
    /// <returns>当前用户菜单树根节点集合</returns>
    public async Task<List<TaktMenuTreeDto>> GetCurrentUserMenuTreeAsync()
    {
        // 获取当前用户ID
        var userId = _userContext?.GetCurrentUserId() ?? 0;
        
        // TODO: 根据用户权限获取菜单（需要关联用户角色和菜单权限表）
        // 目前先返回所有启用的菜单树，后续接入权限系统
        var allMenus = await _menuRepository.FindAsync(x => x.MenuStatus == 1 && x.IsVisible == 1);
        var result = new List<TaktMenuTreeDto>();

        // 只返回根节点（ParentId=0）的菜单
        var rootMenus = allMenus.Where(x => x.ParentId == 0).OrderBy(x => x.SortOrder);

        foreach (var root in rootMenus)
        {
            var treeDto = root.Adapt<TaktMenuTreeDto>();
            // 递归加载子节点
            treeDto.Children = await GetUserMenuChildrenAsync(root.Id, allMenus.ToList());
            result.Add(treeDto);
        }

        return result;
    }

    /// <summary>
    /// 获取用户可见的菜单子节点（递归，按权限过滤）
    /// </summary>
    private async Task<List<TaktMenuTreeDto>> GetUserMenuChildrenAsync(long parentId, List<TaktMenu> allMenus)
    {
        var result = new List<TaktMenuTreeDto>();
        var children = allMenus.Where(x => x.ParentId == parentId && x.MenuStatus == 1 && x.IsVisible == 1).OrderBy(x => x.SortOrder);

        foreach (var child in children)
        {
            var treeDto = child.Adapt<TaktMenuTreeDto>();
            // 递归加载子节点
            treeDto.Children = await GetUserMenuChildrenAsync(child.Id, allMenus);
            result.Add(treeDto);
        }

        return result;
    }

    // ==================== 用户管理 ====================

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="dto">重置密码DTO</param>
    /// <returns>任务</returns>
    public async Task ResetPasswordAsync(TaktUserResetPwdDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new TaktBusinessException("用户不存在");
        }

        // 加密新密码
        user.PasswordHash = TaktEncryptHelper.HashPassword(dto.NewPassword);
        await _userRepository.UpdateAsync(user);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">修改密码DTO</param>
    /// <returns>任务</returns>
    public async Task ChangePasswordAsync(TaktUserChangePwdDto dto)
    {
        var userId = _userContext?.GetCurrentUserId() ?? 0;
        if (userId == 0)
        {
            throw new TaktBusinessException("用户未登录");
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new TaktBusinessException("用户不存在");
        }

        // 验证旧密码
        if (!TaktEncryptHelper.VerifyPassword(dto.OldPassword, user.PasswordHash))
        {
            throw new TaktBusinessException("旧密码不正确");
        }

        // 更新为新密码
        user.PasswordHash = TaktEncryptHelper.HashPassword(dto.NewPassword);
        await _userRepository.UpdateAsync(user);
    }

    /// <summary>
    /// 忘记密码（发送密码重置邮件）
    /// </summary>
    /// <param name="dto">忘记密码DTO</param>
    /// <returns>结果，Success 为 false 时 Code 为 EmailNotFound 或 ProtectedUser</returns>
    public async Task<TaktUserForgotPasswordResultDto> ForgotPasswordAsync(TaktUserForgotPasswordDto dto)
    {
        var user = await _userRepository.FindAsync(x => x.UserEmail == dto.UserEmail);
        if (user == null || user.Count == 0)
        {
            return new TaktUserForgotPasswordResultDto
            {
                Success = false,
                Code = "EmailNotFound"
            };
        }

        var foundUser = user.First();
        // 保护用户不允许忘记密码（例如超级管理员）
        if (foundUser.UserType >= 2)
        {
            return new TaktUserForgotPasswordResultDto
            {
                Success = false,
                Code = "ProtectedUser"
            };
        }

        // TODO: 发送密码重置邮件
        // 目前返回成功，后续需要集成邮件服务

        return new TaktUserForgotPasswordResultDto
        {
            Success = true
        };
    }

    /// <summary>
    /// 解锁用户
    /// </summary>
    /// <param name="dto">解锁用户DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UnlockAsync(TaktUserUnlockDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new TaktBusinessException("用户不存在");
        }

        // 解锁用户
        user.UserStatus = 1; // 启用状态
        user.LockReason = null;
        user.LockTime = null;
        user.LockBy = null;
        user.ErrorCount = 0;

        await _userRepository.UpdateAsync(user);
        return user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="dto">用户头像更新DTO</param>
    /// <returns>用户DTO</returns>
    public async Task<TaktUserDto> UpdateAvatarAsync(TaktUserAvatarUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new TaktBusinessException("用户不存在");
        }

        // 更新头像（头像存储在关联的员工档案中）
        if (user.Employee != null)
        {
            user.Employee.Avatar = dto.AvatarUrl;
            await _userRepository.UpdateAsync(user);
        }
        else
        {
            throw new TaktBusinessException("用户未关联员工档案");
        }

        return user.Adapt<TaktUserDto>();
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户角色列表</returns>
    public async Task<List<TaktUserRoleDto>> GetUserRoleIdsAsync(long userId)
    {
        var userRoles = await _userRoleRepository.FindAsync(x => x.UserId == userId);
        return userRoles.Adapt<List<TaktUserRoleDto>>();
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户部门列表</returns>
    public async Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long userId)
    {
        var userDepts = await _userDeptRepository.FindAsync(x => x.UserId == userId);
        return userDepts.Adapt<List<TaktUserDeptDto>>();
    }

    /// <summary>
    /// 获取用户岗位列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户岗位列表</returns>
    public async Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long userId)
    {
        var userPosts = await _userPostRepository.FindAsync(x => x.UserId == userId);
        return userPosts.Adapt<List<TaktUserPostDto>>();
    }

    /// <summary>
    /// 获取用户租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    public async Task<List<TaktUserTenantDto>> GetUserTenantIdsAsync(long userId)
    {
        var userTenants = await _userTenantRepository.FindAsync(x => x.UserId == userId);
        return userTenants.Adapt<List<TaktUserTenantDto>>();
    }

    /// <summary>
    /// 分配用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserRolesAsync(long userId, long[] roleIds)
    {
        // 删除用户的旧角色
        var oldRoles = await _userRoleRepository.FindAsync(x => x.UserId == userId);
        if (oldRoles.Count > 0)
        {
            await _userRoleRepository.DeleteAsync(oldRoles.Select(x => x.Id).ToList());
        }

        // 添加新角色
        if (roleIds != null && roleIds.Length > 0)
        {
            var newRoles = roleIds.Select(roleId => new TaktUserRole
            {
                UserId = userId,
                RoleId = roleId
            }).ToList();

            await _userRoleRepository.CreateRangeAsync(newRoles);
        }

        return true;
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserDeptsAsync(long userId, long[] deptIds)
    {
        // 删除用户的旧部门
        var oldDepts = await _userDeptRepository.FindAsync(x => x.UserId == userId);
        if (oldDepts.Count > 0)
        {
            await _userDeptRepository.DeleteAsync(oldDepts.Select(x => x.Id).ToList());
        }

        // 添加新部门
        if (deptIds != null && deptIds.Length > 0)
        {
            var newDepts = deptIds.Select(deptId => new TaktUserDept
            {
                UserId = userId,
                DeptId = deptId
            }).ToList();

            await _userDeptRepository.CreateRangeAsync(newDepts);
        }

        return true;
    }

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserPostsAsync(long userId, long[] postIds)
    {
        // 删除用户的旧岗位
        var oldPosts = await _userPostRepository.FindAsync(x => x.UserId == userId);
        if (oldPosts.Count > 0)
        {
            await _userPostRepository.DeleteAsync(oldPosts.Select(x => x.Id).ToList());
        }

        // 添加新岗位
        if (postIds != null && postIds.Length > 0)
        {
            var newPosts = postIds.Select(postId => new TaktUserPost
            {
                UserId = userId,
                PostId = postId
            }).ToList();

            await _userPostRepository.CreateRangeAsync(newPosts);
        }

        return true;
    }

    /// <summary>
    /// 分配用户租户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AssignUserTenantsAsync(long userId, long[] tenantIds)
    {
        // 删除用户的旧租户
        var oldTenants = await _userTenantRepository.FindAsync(x => x.UserId == userId);
        if (oldTenants.Count > 0)
        {
            await _userTenantRepository.DeleteAsync(oldTenants.Select(x => x.Id).ToList());
        }

        // 添加新租户
        if (tenantIds != null && tenantIds.Length > 0)
        {
            var newTenants = tenantIds.Select(tenantId => new TaktUserTenant
            {
                UserId = userId,
                TenantId = tenantId
            }).ToList();

            await _userTenantRepository.CreateRangeAsync(newTenants);
        }

        return true;
    }

    #region 统计分析

    /// <summary>
    /// 统计用户总数
    /// </summary>
    /// <returns>用户总数</returns>
    public async Task<long> GetUserCountAsync()
    {
        return await _userRepository.CountAsync(x => x.IsDeleted == 0);
    }

    #endregion
}
