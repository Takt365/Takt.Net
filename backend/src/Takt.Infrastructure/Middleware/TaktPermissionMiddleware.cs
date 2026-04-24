// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktPermissionMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt权限中间件，验证用户是否有访问权限
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Takt.Application.Services.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Infrastructure.User;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt权限中间件
/// </summary>
public class TaktPermissionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TaktPermissionMiddleware> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="logger">日志记录器</param>
    public TaktPermissionMiddleware(RequestDelegate next, ILogger<TaktPermissionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 如果已标记为允许匿名访问，直接通过
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
        {
            await _next(context);
            return;
        }

        // 如果端点不需要授权，直接通过
        if (endpoint?.Metadata?.GetMetadata<IAuthorizeData>() == null)
        {
            await _next(context);
            return;
        }

        // 如果用户未认证，直接通过（由认证中间件处理）
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            await _next(context);
            return;
        }

        // 已认证但显式跳过菜单权限串校验（仍须通过 [Authorize] 等）
        if (endpoint?.Metadata.OfType<TaktSkipPermissionAttribute>().Any() == true)
        {
            await _next(context);
            return;
        }

        // 获取权限标识（从特性或路由数据中）
        var permission = GetRequiredPermission(context, endpoint);
        
        // 如果没有指定权限标识，直接通过（由授权策略处理）
        if (string.IsNullOrEmpty(permission))
        {
            await _next(context);
            return;
        }

        // 检查用户是否有权限（已认证但 CurrentUser 偶发为空时，在 CheckPermissionAsync 内会按 ITaktUserContext 再加载）
        var (hasPermission, userIdForLog) = await CheckPermissionAsync(context, permission);

        if (!hasPermission)
        {
            _logger.LogWarning("用户 {UserId} 没有权限访问 {Permission}，路径: {Path}",
                userIdForLog?.ToString() ?? "(无用户实体)", permission, context.Request.Path);
            throw new UnauthorizedAccessException($"没有权限访问: {permission}");
        }

        await _next(context);
    }

    /// <summary>
    /// 获取所需的权限标识
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="endpoint">端点</param>
    /// <returns>权限标识</returns>
    private static string? GetRequiredPermission(HttpContext context, Endpoint? endpoint)
    {
        if (endpoint == null)
        {
            return null;
        }

        // 从端点元数据中获取权限标识（从 TaktPermissionAttribute 特性）
        // 同一类型在元数据中可能出现多次（控制器 + 方法）；取最后一个以匹配「方法覆盖控制器」的约定
        var permissionAttribute = endpoint.Metadata.OfType<TaktPermissionAttribute>().LastOrDefault();
        if (permissionAttribute != null)
        {
            return permissionAttribute.Permission;
        }

        // 从路由数据中获取权限标识（备用方式）
        if (context.Request.RouteValues.TryGetValue("permission", out var permissionValue) && 
            permissionValue is string permission)
        {
            return permission;
        }
        
        return null;
    }

    /// <summary>
    /// 检查用户是否有指定权限；返回是否通过及用于日志的用户 Id（与中间件内实际参与校验的实体一致）。
    /// </summary>
    private async Task<(bool HasPermission, long? UserIdForLog)> CheckPermissionAsync(HttpContext context, string permission)
    {
        try
        {
            var userContext = context.RequestServices.GetRequiredService<ITaktUserContext>();
            var user = TaktUserContext.CurrentUser;
            if (user == null && context.User?.Identity?.IsAuthenticated == true)
            {
                // 与 TaktUserMiddleware 一致：Claims 已认证时从库加载并写回 TaktUserContext（避免仅依赖 AsyncLocal 时序导致 CurrentUser 仍为空）
                user = await userContext.GetCurrentUserAsync().ConfigureAwait(true);
            }

            if (user == null)
                return (false, null);

            // 超级管理员拥有所有权限
            if (user.UserType == 2)
                return (true, user.Id);

            var permissionService = context.RequestServices.GetRequiredService<ITaktUserPermissionService>();
            var ok = await permissionService.HasMenuPermissionAsync(user.Id, permission, context.RequestAborted).ConfigureAwait(true);
            if (!ok)
                _logger.LogDebug("权限校验未通过: Permission={Permission}, UserId={UserId}", permission, user.Id);
            return (ok, user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "检查权限时发生异常: {Permission}", permission);
            return (false, null);
        }
    }
}
