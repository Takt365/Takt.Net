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
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
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

        // 获取权限标识（从特性或路由数据中）
        var permission = GetRequiredPermission(context, endpoint);
        
        // 如果没有指定权限标识，直接通过（由授权策略处理）
        if (string.IsNullOrEmpty(permission))
        {
            await _next(context);
            return;
        }

        // 检查用户是否有权限
        var hasPermission = await CheckPermissionAsync(permission);
        
        if (!hasPermission)
        {
            _logger.LogWarning("用户 {UserId} 没有权限访问 {Permission}，路径: {Path}", 
                TaktUserContext.CurrentUserId, permission, context.Request.Path);
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
        // 由于特性标记为 Inherited = true，如果方法上有特性就返回方法的，否则返回控制器上的
        var permissionAttribute = endpoint.Metadata.GetMetadata<TaktPermissionAttribute>();
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
    /// 检查用户是否有指定权限
    /// </summary>
    /// <param name="permission">权限标识</param>
    /// <returns>是否有权限</returns>
    private async Task<bool> CheckPermissionAsync(string permission)
    {
        try
        {
            // 如果用户是超级管理员（UserType = 2），拥有所有权限
            var user = TaktUserContext.CurrentUser;
            if (user == null)
            {
                return false;
            }

            // 超级管理员拥有所有权限
            if (user.UserType == 2)
            {
                return true;
            }

            // TODO: 从数据库查询用户权限
            // 1. 获取用户的角色列表
            // 2. 获取角色关联的菜单列表
            // 3. 检查菜单的权限标识是否匹配
            // 这里先返回 true，等待权限服务实现后再完善
            _logger.LogDebug("检查权限: {Permission}, 用户: {UserId}", permission, user.Id);
            
            // 临时实现：返回 true（允许访问）
            // 后续需要实现完整的权限检查逻辑
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "检查权限时发生异常: {Permission}", permission);
            return false;
        }
    }
}
