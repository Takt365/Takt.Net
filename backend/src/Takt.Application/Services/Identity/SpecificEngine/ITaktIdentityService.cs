// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Identity.SpecificEngine
// 文件名称：ITaktIdentityService.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：身份认证树形服务接口，定义菜单树等树形结构的业务操作
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity.SpecificEngine;

/// <summary>
/// 身份认证专用服务接口
/// </summary>
public interface ITaktIdentityService
{
    // ==================== 菜单树（不含与TaktMenuService重复的方法） ====================

    /// <summary>
    /// 获取菜单树形选项（含按钮 MenuType=2），用于角色分配菜单等需勾选按钮权限的场景。
    /// </summary>
    /// <returns>菜单树形选项根节点集合（含按钮）</returns>
    Task<List<TaktTreeSelectOption>> GetMenuTreeOptionsWithButtonAsync();

    /// <summary>
    /// 获取模块名称用目录树（仅 MenuType=0），用于代码生成中的模块列表。
    /// </summary>
    /// <returns>目录级菜单树根节点集合</returns>
    Task<List<TaktMenuTreeDto>> GetMenuDirectoryTreeAsync();

    /// <summary>
    /// 获取当前用户可见的菜单树（按权限过滤）。
    /// </summary>
    /// <returns>当前用户菜单树根节点集合</returns>
    Task<List<TaktMenuTreeDto>> GetCurrentUserMenuTreeAsync();

    // ==================== 用户管理 ====================

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="dto">重置密码DTO</param>
    /// <returns>任务</returns>
    Task ResetPasswordAsync(TaktUserResetPwdDto dto);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">修改密码DTO</param>
    /// <returns>任务</returns>
    Task ChangePasswordAsync(TaktUserChangePwdDto dto);

    /// <summary>
    /// 忘记密码（发送密码重置邮件）
    /// </summary>
    /// <param name="dto">忘记密码DTO</param>
    /// <returns>结果，Success 为 false 时 Code 为 EmailNotFound 或 ProtectedUser</returns>
    Task<TaktUserForgotPasswordResultDto> ForgotPasswordAsync(TaktUserForgotPasswordDto dto);

    /// <summary>
    /// 解锁用户
    /// </summary>
    /// <param name="dto">解锁用户DTO</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto> UnlockAsync(TaktUserUnlockDto dto);

    /// <summary>
    /// 更新头像
    /// </summary>
    /// <param name="dto">用户头像更新DTO</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto> UpdateAvatarAsync(TaktUserAvatarUpdateDto dto);

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户角色列表</returns>
    Task<List<TaktUserRoleDto>> GetUserRoleIdsAsync(long userId);

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户部门列表</returns>
    Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long userId);

    /// <summary>
    /// 获取用户岗位列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户岗位列表</returns>
    Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long userId);

    /// <summary>
    /// 获取用户租户列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户租户列表</returns>
    Task<List<TaktUserTenantDto>> GetUserTenantIdsAsync(long userId);

    /// <summary>
    /// 分配用户角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignUserRolesAsync(long userId, long[] roleIds);

    /// <summary>
    /// 分配用户部门
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deptIds">部门ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignUserDeptsAsync(long userId, long[] deptIds);

    /// <summary>
    /// 分配用户岗位
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignUserPostsAsync(long userId, long[] postIds);

    /// <summary>
    /// 分配用户租户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="tenantIds">租户ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignUserTenantsAsync(long userId, long[] tenantIds);

    #region 统计分析

    /// <summary>
    /// 统计用户总数
    /// </summary>
    /// <returns>用户总数</returns>
    Task<long> GetUserCountAsync();

    #endregion
}