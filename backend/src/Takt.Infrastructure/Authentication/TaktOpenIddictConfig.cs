// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Authentication
// 文件名称：TaktOpenIddictConfig.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：OpenIddict配置类，用于配置OIDC认证服务
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace Takt.Infrastructure.Authentication;

/// <summary>
/// OpenIddict配置类
/// </summary>
public static class TaktOpenIddictConfig
{
    /// <summary>
    /// 初始化OpenIddict应用和范围
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <returns>任务</returns>
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        // 创建默认作用域
        await CreateScopesAsync(scopeManager);

        // 创建默认应用
        await CreateApplicationsAsync(applicationManager);
    }

    /// <summary>
    /// 创建作用域
    /// </summary>
    private static async Task CreateScopesAsync(IOpenIddictScopeManager scopeManager)
    {
        var scopes = new[]
        {
            new { Id = "openid", Name = "openid", DisplayName = "OpenID Connect" },
            new { Id = "profile", Name = "profile", DisplayName = "Profile" },
            new { Id = "email", Name = "email", DisplayName = "Email" },
            new { Id = "roles", Name = "roles", DisplayName = "Roles" },
            new { Id = "api", Name = "api", DisplayName = "API Access" }
        };

        foreach (var scope in scopes)
        {
            var existingScope = await scopeManager.FindByNameAsync(scope.Name);
            if (existingScope == null)
            {
                await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = scope.Name,
                    DisplayName = scope.DisplayName
                });
            }
        }
    }

    /// <summary>
    /// 创建应用
    /// </summary>
    private static async Task CreateApplicationsAsync(IOpenIddictApplicationManager applicationManager)
    {
        // Web客户端应用
        var webClient = await applicationManager.FindByClientIdAsync("takt-web-client");
        if (webClient == null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "takt-web-client",
                DisplayName = "Takt Web Client",
                ClientType = OpenIddictConstants.ClientTypes.Public,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Revocation,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles
                }
            });
        }
    }
}
