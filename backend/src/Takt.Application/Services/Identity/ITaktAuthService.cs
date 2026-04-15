// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktAuthService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt认证服务接口，定义用户认证和授权操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt认证服务接口
/// </summary>
public interface ITaktAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录请求DTO</param>
    /// <returns>登录响应DTO</returns>
    Task<TaktLoginResponseDto> LoginAsync(TaktLoginDto dto);

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="dto">刷新令牌请求DTO</param>
    /// <returns>登录响应DTO</returns>
    Task<TaktLoginResponseDto> RefreshTokenAsync(TaktRefreshTokenDto dto);

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>用户名（用于日志记录）</returns>
    Task<string> LogoutAsync(string refreshToken);

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns>用户信息DTO</returns>
    Task<TaktUserInfoDto> GetUserInfoAsync();

    /// <summary>
    /// 记录登录成功（登录日志和在线记录）
    /// </summary>
    /// <param name="user">用户实体</param>
    /// <param name="userName">用户名</param>
    /// <returns>任务</returns>
    Task RecordLoginSuccessAsync(TaktUser user, string userName);

    /// <summary>
    /// 记录登录失败（登录日志和失败次数累加）
    /// </summary>
    /// <param name="user">用户实体（如果用户存在）</param>
    /// <param name="userName">用户名</param>
    /// <param name="failureReason">失败原因</param>
    /// <returns>任务</returns>
    Task RecordLoginFailureAsync(TaktUser? user, string userName, string failureReason);

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>角色代码列表</returns>
    Task<List<string>> GetUserRolesAsync(long userId);

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>权限标识列表</returns>
    Task<List<string>> GetUserPermissionsAsync(long userId);

    /// <summary>
    /// 获取用户展示名（来自关联员工档案，无则返回用户名）
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>展示名</returns>
    Task<string> GetUserDisplayNameAsync(long userId);
}
