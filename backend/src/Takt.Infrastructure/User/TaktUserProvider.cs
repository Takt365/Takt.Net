// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.User
// 文件名称：TaktUserProvider.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户提供者，从 HTTP/Claims/SignalR Hub 解析当前用户；异步加载用户实体；GetCurrentUserName 含按主键读库的登录名兜底，与仓储审计、操作日志、差异日志一致
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Linq;
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
/// Takt 用户提供者：封装当前用户 Id、登录名、展示名及员工关联信息的解析；与 <see cref="TaktUserContext"/> 配合；<see cref="GetCurrentUserName"/> 与持久化审计、<c>TaktOperLog</c>/<c>TaktAopLog</c>、仓储 CreatedBy 写入一致。
/// </summary>
public class TaktUserProvider : ITaktUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TaktSqlSugarDbContext _dbContext;

    /// <summary>
    /// 安全获取当前 <see cref="HttpContext"/>。请求已结束或 Features 已释放时返回 null，避免后台任务访问 <see cref="IHttpContextAccessor.HttpContext"/> 抛异常。
    /// </summary>
    private HttpContext? TryGetHttpContext()
    {
        try
        {
            return _httpContextAccessor.HttpContext;
        }
        catch (ObjectDisposedException)
        {
            return null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    /// <summary>
    /// 当前用于解析用户的 Claims 主体：优先 HTTP 请求；SignalR Hub 内为 <see cref="TaktUserContext.HubInvocationPrincipal"/>。
    /// </summary>
    private ClaimsPrincipal? GetClaimsPrincipal()
    {
        var httpContext = TryGetHttpContext();
        var httpUser = httpContext?.User;

        // 已认证 HTTP 用户优先（常规 API）
        if (httpUser?.Identity?.IsAuthenticated == true)
            return httpUser;

        // SignalR：Hub 与 IHubFilter 写入 HubInvocationPrincipal；须优先于「未认证但非空的 HttpContext.User」，否则会误用匿名主体、读不到 name/sub
        var hubPrincipal = TaktUserContext.HubInvocationPrincipal;
        if (hubPrincipal?.Identity?.IsAuthenticated == true)
            return hubPrincipal;

        // 协商/个别宿主：User 可能已含 JWT Claims 但未标记 IsAuthenticated，仍优先于 Hub 空主体
        if (httpUser != null && httpUser.Claims.Any())
            return httpUser;

        return hubPrincipal;
    }

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
            await EnsureCurrentEmployeeLoadedAsync(TaktUserContext.CurrentUser).ConfigureAwait(false);
            return TaktUserContext.CurrentUser;
        }

        // 获取用户ID
        var userId = GetCurrentUserId();
        if (!userId.HasValue)
        {
            return null;
        }

        // 从数据库加载完整用户实体（直接使用数据库客户端，避免循环依赖）；无记录时不抛异常（与 SignalR/WebSocket 场景一致）
        var db = _dbContext.GetClient(typeof(TaktUser));
        var users = await db.Queryable<TaktUser>()
            .Where(u => u.Id == userId.Value && u.IsDeleted == 0)
            .Take(1)
            .ToListAsync();
        var user = users.FirstOrDefault();

        if (user != null)
        {
            // 缓存到上下文
            TaktUserContext.CurrentUser = user;
            await EnsureCurrentEmployeeLoadedAsync(user).ConfigureAwait(false);
        }

        return user;
    }

    /// <summary>
    /// 与 <c>TaktUserMiddleware</c> 一致：在已解析 <see cref="TaktUser"/> 后加载 <see cref="TaktEmployee"/>，供 SignalR 等不经该中间件的宿主使用 <see cref="TaktUserContext.GetCurrentUserDisplayName"/> / <see cref="GetCurrentRealName"/>。
    /// </summary>
    private async Task EnsureCurrentEmployeeLoadedAsync(TaktUser user)
    {
        if (user.EmployeeId <= 0)
        {
            TaktUserContext.CurrentEmployee = null;
            return;
        }

        var existing = TaktUserContext.CurrentEmployee;
        if (existing != null && existing.Id == user.EmployeeId)
            return;

        var empDb = _dbContext.GetClient(typeof(TaktEmployee));
        var rows = await empDb.Queryable<TaktEmployee>()
            .Where(e => e.Id == user.EmployeeId && e.IsDeleted == 0)
            .Take(1)
            .ToListAsync()
            .ConfigureAwait(false);
        TaktUserContext.CurrentEmployee = rows.FirstOrDefault();
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

        var principal = GetClaimsPrincipal();
        if (principal == null)
            return null;

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? principal.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            return null;

        // 与仓储审计 / 操作日志一致：主体上已有可解析 sub 即视为可解析用户主键（避免个别宿主上 IsAuthenticated 与 Claims 不同步）
        return userId;
    }

    /// <summary>
    /// 获取当前用户名（登录名语义，与 <c>CreatedBy</c>、操作日志 <c>user_name</c>、在线用户表一致）。
    /// </summary>
    /// <returns>用户名；未登录或无法解析时返回 null。</returns>
    /// <remarks>
    /// 无内存实体但 Claims 可解析用户主键时，优先按主键读 <see cref="TaktUser"/> 表取 <see cref="TaktUser.UserName"/>（不依赖 JWT 是否含 name），再退回 Claims 中的 name/email 等。
    /// </remarks>
    public string? GetCurrentUserName()
    {
        if (TaktUserContext.CurrentUser != null)
        {
            var u = TaktUserContext.CurrentUser;
            if (!string.IsNullOrWhiteSpace(u.UserName))
                return u.UserName.Trim();
            var display = TaktUserContext.GetCurrentUserDisplayName();
            return string.IsNullOrWhiteSpace(display) ? null : display.Trim();
        }

        var id = GetCurrentUserId();
        if (id.HasValue && id.Value > 0)
        {
            var fromDb = GetAuditDisplayNameFromDatabase(id.Value);
            if (!string.IsNullOrWhiteSpace(fromDb))
                return fromDb;
        }

        var principal = GetClaimsPrincipal();
        if (principal == null)
            return null;

        var sub = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? principal.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        if (string.IsNullOrEmpty(sub) || !long.TryParse(sub, out _))
        {
            if (principal.Identity?.IsAuthenticated != true)
                return null;
        }

        var userName = principal.FindFirst(ClaimTypes.Name)?.Value
            ?? principal.FindFirst(OpenIddictConstants.Claims.Name)?.Value
            ?? principal.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value
            ?? principal.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
            userName = principal.FindFirst(ClaimTypes.GivenName)?.Value;

        if (string.IsNullOrEmpty(userName))
        {
            userName = principal.FindFirst(ClaimTypes.Email)?.Value
                ?? principal.FindFirst(OpenIddictConstants.Claims.Email)?.Value
                ?? principal.FindFirst(ClaimTypes.Upn)?.Value;
        }

        return string.IsNullOrEmpty(userName) ? null : userName.Trim();
    }

    /// <summary>
    /// 按用户主键从 Identity 库同步读取用于审计/日志展示的字符串（与表字段 <c>user_name</c>、<c>created_by</c> 语义一致），不依赖 JWT 是否携带 Name 类 Claim。
    /// </summary>
    /// <param name="userId">用户主键，须大于 0。</param>
    /// <returns>优先 <see cref="TaktUser.UserName"/>，否则 <see cref="TaktUser.NickName"/>；无有效数据或未查到行时返回 null。</returns>
    /// <remarks>
    /// 吞掉异常并返回 null，避免差异日志/操作日志路径因读库失败影响主业务 SQL；调用方会继续使用 Claims 兜底。
    /// </remarks>
    private string? GetAuditDisplayNameFromDatabase(long userId)
    {
        try
        {
            // 与 GetCurrentUserAsync 相同：按实体所在库路由客户端，避免多库下连错库
            var db = _dbContext.GetClient(typeof(TaktUser));
            var rows = db.Queryable<TaktUser>()
                .Where(u => u.Id == userId && u.IsDeleted == 0)
                .Take(1)
                .ToList();
            var u = rows.FirstOrDefault();
            if (u == null)
                return null;
            if (!string.IsNullOrWhiteSpace(u.UserName))
                return u.UserName.Trim();
            if (!string.IsNullOrWhiteSpace(u.NickName))
                return u.NickName.Trim();
            return null;
        }
        catch
        {
            return null;
        }
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
        var principal = GetClaimsPrincipal();
        if (principal?.Identity?.IsAuthenticated == true)
        {
            var claim = principal.FindFirst(ClaimTypes.GivenName)?.Value;
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

        var principal = GetClaimsPrincipal();
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirst(ClaimTypes.Email)?.Value
            ?? principal.FindFirst(OpenIddictConstants.Claims.Email)?.Value;
    }

    /// <summary>
    /// 获取当前用户手机号
    /// </summary>
    /// <returns>用户手机号，如果未登录则返回null</returns>
    public string? GetCurrentUserPhone()
    {
        if (TaktUserContext.CurrentUser != null)
            return TaktUserContext.CurrentUser.UserPhone;

        var principal = GetClaimsPrincipal();
        if (principal?.Identity?.IsAuthenticated != true)
            return null;

        return principal.FindFirst(OpenIddictConstants.Claims.PhoneNumber)?.Value
            ?? principal.FindFirst("phone_number")?.Value;
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
               (GetClaimsPrincipal()?.Identity?.IsAuthenticated == true);
    }
}
