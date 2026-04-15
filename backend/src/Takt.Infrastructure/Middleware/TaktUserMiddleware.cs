// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Middleware
// 文件名称：TaktUserMiddleware.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户中间件，从HTTP请求中提取用户信息并从数据库加载完整用户实体设置到上下文
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.User;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// Takt用户中间件
/// </summary>
public class TaktUserMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">下一个中间件</param>
    public TaktUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="userContext">用户上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context, ITaktUserContext userContext, TaktSqlSugarDbContext dbContext)
    {
        // 如果用户已认证，从数据库加载完整用户实体并设置到上下文
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            var userId = userContext.GetCurrentUserId();
            
            // 从数据库加载完整用户实体
            var user = await userContext.GetCurrentUserAsync();
            if (user != null)
            {
                if (user.EmployeeId > 0)
                {
                    var empDb = dbContext.GetClient(typeof(TaktEmployee));
                    var emp = await empDb.Queryable<TaktEmployee>()
                        .Where(e => e.Id == user.EmployeeId && e.IsDeleted == 0)
                        .FirstAsync();
                    TaktUserContext.CurrentEmployee = emp;
                }
                else
                    TaktUserContext.CurrentEmployee = null;

                // 用户实体已经缓存到上下文（在 GetCurrentUserAsync 中）
                // 输出日志：用户登录成功
                var realName = userContext.GetCurrentRealName() ?? user.UserName;
                TaktLogger.Information("TaktUserMiddleware: 用户登录成功，路径: {Path}, UserId: {UserId}, UserName: {UserName}, RealName: {RealName}, UserType: {UserType}", 
                    path, user.Id, user.UserName, realName, user.UserType);
            }
            else
            {
                TaktUserContext.CurrentEmployee = null;
                // 诊断：如果认证成功但无法加载用户，记录警告
                TaktLogger.Warning("TaktUserMiddleware: 用户已认证但无法加载用户实体，路径: {Path}, UserId: {UserId}", path, userId?.ToString() ?? string.Empty);
            }
        }
        else
            TaktUserContext.CurrentEmployee = null;

        await _next(context);
    }
}
