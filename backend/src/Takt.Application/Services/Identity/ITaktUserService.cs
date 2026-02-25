// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktUserService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户应用服务接口，定义用户管理的业务操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Application.Dtos.Identity;
using Takt.Shared.Models;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt用户应用服务接口
/// </summary>
public interface ITaktUserService
{
    /// <summary>
    /// 获取用户列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktUserDto>> GetListAsync(TaktUserQueryDto queryDto);

    /// <summary>
    /// 根据ID获取用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取用户选项列表（用于下拉框等）
    /// </summary>
    /// <returns>用户选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto">创建用户DTO</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto> CreateAsync(TaktUserCreateDto dto);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="dto">更新用户DTO（仅包含UserId）</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto> UpdateAsync(long id, TaktUserUpdateDto dto);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>任务</returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除用户
    /// </summary>
    /// <param name="ids">用户ID列表</param>
    /// <returns>任务</returns>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="dto">用户状态DTO</param>
    /// <returns>用户DTO</returns>
    Task<TaktUserDto> UpdateStatusAsync(TaktUserStatusDto dto);

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

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入用户
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出用户
    /// </summary>
    /// <param name="query">用户查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktUserQueryDto query, string? sheetName, string? fileName);
}