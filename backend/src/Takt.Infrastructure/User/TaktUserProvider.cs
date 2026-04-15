// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.User
// 文件名称：TaktUserProvider.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户提供者，从HTTP上下文或Claims中获取当前用户信息，并从数据库加载完整用户实体
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;
using SqlSugar;
using System.Security.Claims;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Data;

namespace Takt.Infrastructure.User;

/// <summary>
/// Takt用户提供者
/// </summary>
public class TaktUserProvider : ITaktUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TaktSqlSugarDbContext _dbContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="dbContext">数据库上下文（用于直接查询数据库，避免循环依赖）</param>
    public TaktUserProvider(IHttpContextAccessor httpContextAccessor, TaktSqlSugarDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 获取当前用户实体（完整数据）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    public TaktUser? GetCurrentUser()
    {
        return TaktUserContext.CurrentUser;
    }

    /// <summary>
    /// 获取当前用户实体（完整数据，异步）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    public async Task<TaktUser?> GetCurrentUserAsync()
    {
        // 优先从上下文获取
        if (TaktUserContext.CurrentUser != null)
        {
            return TaktUserContext.CurrentUser;
        }

        // 获取用户ID
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return null;
        }

        // 从数据库加载完整用户实体（直接使用数据库客户端，避免循环依赖）
        var db = _dbContext.GetClient(typeof(TaktUser));
        var user = await db.Queryable<TaktUser>()
            .Where(u => u.Id == userId.Value && u.IsDeleted == 0)
            .FirstAsync();
        
        if (user != null)
        {
            // 缓存到上下文
            TaktUserContext.CurrentUser = user;
        }

        return user;
    }

    /// <summary>
    /// 获取当前用户ID
    /// </summary>
    /// <returns>用户ID，如果未登录则返回null</returns>
    public long? GetCurrentUserId()
    {
        // 优先从上下文获取
        if (TaktUserContext.CurrentUser != null)
        {
            return TaktUserContext.CurrentUser.Id;
        }

        // 从HTTP上下文Claims中获取
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            return null;
        }

        return userId;
    }

    /// <summary>
    /// 获取当前用户名
    /// </summary>
    /// <returns>用户名，如果未登录则返回null</returns>
    public string? GetCurrentUserName()
    {
        // 优先从上下文获取（AsyncLocal，在中间件中设置）
        if (TaktUserContext.CurrentUser != null)
        {
            return TaktUserContext.CurrentUser.UserName;
        }

        // 从HTTP上下文Claims中获取
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        // 尝试从多个 claim 获取用户名
        var userName = httpContext.User.FindFirst(ClaimTypes.Name)?.Value
            ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Name)?.Value
            ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
            ?? httpContext.User.Identity?.Name;

        // 无 Claims 用户名时依赖 TaktUserMiddleware 已写入的 CurrentUser（不在此同步访问数据库）
        if (string.IsNullOrEmpty(userName))
            return TaktUserContext.CurrentUser?.UserName;

        return userName;
    }

    /// <summary>
    /// 从员工档案获取当前用户对应的员工（UserId 关联）；无则返回 null。
    /// </summary>
    private static TaktEmployee? GetCurrentEmployee()
    {
        return TaktUserContext.CurrentEmployee;
    }

    /// <summary>
    /// 获取当前用户真实姓名（来自关联员工档案，无则返回用户名）
    /// </summary>
    /// <returns>用户真实姓名，如果未登录则返回null</returns>
    public string? GetCurrentRealName()
    {
        var employee = GetCurrentEmployee();
        if (employee != null && !string.IsNullOrWhiteSpace(employee.RealName))
            return employee.RealName.Trim();
        var user = TaktUserContext.CurrentUser;
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var claim = httpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
            if (!string.IsNullOrWhiteSpace(claim)) return claim;
        }
        return user?.UserName;
    }

    /// <summary>
    /// 获取当前用户昵称：优先用户表 <see cref="TaktUser.NickName"/>；否则关联员工档案姓名；再否则用户名。
    /// </summary>
    /// <returns>用户昵称，如果未登录则返回null</returns>
    public string? GetCurrentNickName()
    {
        var user = TaktUserContext.CurrentUser;
        if (user != null && !string.IsNullOrWhiteSpace(user.NickName))
            return user.NickName.Trim();
        var employee = GetCurrentEmployee();
        if (employee != null && !string.IsNullOrWhiteSpace(employee.RealName))
            return employee.RealName.Trim();
        return user?.UserName;
    }

    /// <summary>
    /// 获取当前用户头像（来自关联员工档案）
    /// </summary>
    /// <returns>用户头像，如果未登录则返回null</returns>
    public string? GetCurrentAvatar()
    {
        var employee = GetCurrentEmployee();
        return employee?.Avatar;
    }

    /// <summary>
    /// 获取当前用户性别（来自关联员工档案，0=未知，1=男，2=女）
    /// </summary>
    /// <returns>用户性别，如果未登录则返回null</returns>
    public int? GetCurrentGender()
    {
        var employee = GetCurrentEmployee();
        return employee?.Gender;
    }

    /// <summary>
    /// 获取当前用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    /// <returns>用户类型，如果未登录则返回null</returns>
    public int? GetCurrentUserType()
    {
        return TaktUserContext.CurrentUser?.UserType;
    }

    /// <summary>
    /// 获取当前用户邮箱
    /// </summary>
    /// <returns>用户邮箱，如果未登录则返回null</returns>
    public string? GetCurrentUserEmail()
    {
        // 优先从上下文获取
        if (TaktUserContext.CurrentUser != null)
        {
            return TaktUserContext.CurrentUser.UserEmail;
        }

        // 从HTTP上下文Claims中获取
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        return httpContext.User.FindFirst(ClaimTypes.Email)?.Value
            ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Email)?.Value;
    }

    /// <summary>
    /// 获取当前用户手机号
    /// </summary>
    /// <returns>用户手机号，如果未登录则返回null</returns>
    public string? GetCurrentUserPhone()
    {
        return TaktUserContext.CurrentUser?.UserPhone;
    }

    /// <summary>
    /// 获取当前用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    /// <returns>用户状态，如果未登录则返回null</returns>
    public int? GetCurrentUserStatus()
    {
        return TaktUserContext.CurrentUser?.UserStatus;
    }

    /// <summary>
    /// 是否已登录
    /// </summary>
    /// <returns>如果已登录返回true，否则返回false</returns>
    public bool IsAuthenticated()
    {
        return TaktUserContext.IsAuthenticated || 
               (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true);
    }
}
