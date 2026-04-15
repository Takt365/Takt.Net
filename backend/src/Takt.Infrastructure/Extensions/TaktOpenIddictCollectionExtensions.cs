// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktOpenIddictCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：OpenIddict扩展方法，用于配置OpenIddict服务
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation;
using Takt.Infrastructure.Data;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// OpenIddict扩展方法
/// </summary>
public static class TaktOpenIddictCollectionExtensions
{
    /// <summary>
    /// 添加OpenIddict服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>OpenIddict构建器</returns>
    public static OpenIddictBuilder AddTaktOpenIddict(this IServiceCollection services, IConfiguration configuration)
    {
        // 添加HttpContextAccessor（OpenIddict需要）
        services.AddHttpContextAccessor();

        // 获取OpenIddict专用数据库连接字符串
        var openIddictConnectionString = configuration["OpenIddictDb"] 
            ?? throw new InvalidOperationException("未找到OpenIddict数据库连接字符串，请在appsettings.json中配置OpenIddictDb");

        // 获取认证配置（Token过期时间等）
        var accessTokenLifetimeHours = configuration.GetValue<int>("Authentication:AccessTokenLifetimeHours", 2);
        var refreshTokenLifetimeDays = configuration.GetValue<int>("Authentication:RefreshTokenLifetimeDays", 7);
        var refreshTokenReuseLeewayMinutes = configuration.GetValue<int>("Authentication:RefreshTokenReuseLeewayMinutes", 5);

        // 注册OpenIddict数据库上下文（使用专门的数据库）
        services.AddDbContext<TaktOpenIddictDbContext>(options =>
        {
            options.UseSqlServer(openIddictConnectionString);
            // 使用OpenIddict的EF Core集成
            options.UseOpenIddict();
        });

        // 添加OpenIddict服务器
        var builder = services.AddOpenIddict()
            // 注册OpenIddict核心组件（使用Entity Framework Core数据库存储）
            .AddCore(options =>
            {
                // 使用Entity Framework Core存储
                options.UseEntityFrameworkCore()
                    .UseDbContext<TaktOpenIddictDbContext>();
            })
            // 注册OpenIddict服务器组件
            .AddServer(options =>
            {
                // 启用令牌端点
                options.SetTokenEndpointUris("/api/TaktAuth/connect/token");

                // 允许密码流程和刷新令牌流程
                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow();

                // 配置访问令牌过期时间（默认2小时，可在appsettings.json中配置）
                // 注意：前端会在过期前5分钟自动刷新，所以实际用户体验是无感知的
                options.SetAccessTokenLifetime(TimeSpan.FromHours(accessTokenLifetimeHours));

                // 配置刷新令牌过期时间（默认7天，可在appsettings.json中配置）
                // 刷新令牌用于获取新的访问令牌，过期时间应该比访问令牌长得多
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(refreshTokenLifetimeDays));

                // 配置刷新令牌重用策略
                // 允许时钟偏差（默认5分钟，可在appsettings.json中配置）
                // 这可以处理客户端和服务器之间的时间差
                options.SetRefreshTokenReuseLeeway(TimeSpan.FromMinutes(refreshTokenReuseLeewayMinutes));

                // 注册签名和加密凭据（开发环境使用临时密钥，生产环境应使用持久化密钥）
                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                // 注册ASP.NET Core主机并配置选项
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough();
            })
            // 注册OpenIddict验证组件
            .AddValidation(options =>
            {
                // 导入本地服务器配置
                options.UseLocalServer();
                // 注册ASP.NET Core主机
                // 注意：UseAspNetCore() 默认支持从查询参数提取 access_token（通过 ExtractAccessTokenFromQueryString 处理器）
                // 这意味着 SignalR WebSocket 可以通过查询参数传递 token，OpenIddict 会自动提取并验证
                options.UseAspNetCore();
            });

        return builder;
    }

    /// <summary>
    /// 初始化OpenIddict数据库
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>任务</returns>
    public static async Task InitializeOpenIddictDatabaseAsync(this WebApplication app)
    {
        // 参考: https://documentation.openiddict.com/guides/getting-started/creating-your-own-server-instance
        // 官方文档建议: Before running the application, make sure the database is updated 
        // with OpenIddict tables by running Add-Migration and Update-Database.
        // 这里使用 MigrateAsync() 自动应用迁移，如果没有迁移则使用 EnsureCreatedAsync() 作为开发环境的回退
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(string.Empty); // 使用空字符串作为类别名称，不显示类别
        
        try
        {
            logger.LogInformation("开始初始化OpenIddict数据库...");
            using var openIddictScope = app.Services.CreateScope();
            var openIddictDbContext = openIddictScope.ServiceProvider.GetRequiredService<TaktOpenIddictDbContext>();
            
            try
            {
                // 优先使用迁移方式（官方推荐）
                var pendingMigrations = await openIddictDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    logger.LogInformation("检测到待应用的迁移，正在应用迁移...");
                    await openIddictDbContext.Database.MigrateAsync();
                    logger.LogInformation("OpenIddict数据库迁移完成");
                }
                else
                {
                    var appliedMigrations = await openIddictDbContext.Database.GetAppliedMigrationsAsync();
                    if (appliedMigrations.Any())
                    {
                        logger.LogInformation("OpenIddict数据库迁移已是最新版本");
                    }
                    else
                    {
                        // 没有迁移，使用 EnsureCreatedAsync 作为开发环境的回退方案
                        // 生产环境应该创建迁移: dotnet ef migrations add InitialOpenIddict --project Takt.Infrastructure
                        logger.LogWarning("未找到OpenIddict迁移，使用EnsureCreatedAsync作为回退方案");
                        logger.LogWarning("建议运行以下命令创建迁移: dotnet ef migrations add InitialOpenIddict --project Takt.Infrastructure");
                        
                        await openIddictDbContext.Database.EnsureCreatedAsync();
                        
                        // 验证表是否真的被创建了
                        var connection = openIddictDbContext.Database.GetDbConnection();
                        await connection.OpenAsync();
                        try
                        {
                            using var command = connection.CreateCommand();
                            command.CommandText = @"
                                SELECT COUNT(*) 
                                FROM INFORMATION_SCHEMA.TABLES 
                                WHERE TABLE_NAME = 'OpenIddictScopes'";
                            var result = await command.ExecuteScalarAsync();
                            var tableExists = result != null && Convert.ToInt32(result) > 0;
                            
                            if (!tableExists)
                            {
                                // 表不存在，删除数据库后重新创建
                                logger.LogWarning("检测到表不存在，正在重新创建数据库...");
                                await connection.CloseAsync();
                                await openIddictDbContext.Database.EnsureDeletedAsync();
                                await openIddictDbContext.Database.EnsureCreatedAsync();
                                
                                // 再次验证表是否被创建
                                await connection.OpenAsync();
                                using var verifyCommand = connection.CreateCommand();
                                verifyCommand.CommandText = @"
                                    SELECT COUNT(*) 
                                    FROM INFORMATION_SCHEMA.TABLES 
                                    WHERE TABLE_NAME = 'OpenIddictScopes'";
                                var verifyResult = await verifyCommand.ExecuteScalarAsync();
                                var verifyTableExists = verifyResult != null && Convert.ToInt32(verifyResult) > 0;
                                
                                if (verifyTableExists)
                                {
                                    logger.LogInformation("OpenIddict数据库和表已重新创建");
                                }
                                else
                                {
                                    logger.LogError("OpenIddict表创建失败，请手动创建迁移");
                                    throw new InvalidOperationException("OpenIddict表创建失败");
                                }
                            }
                            else
                            {
                                logger.LogInformation("OpenIddict数据库和表已存在");
                            }
                        }
                        finally
                        {
                            if (connection.State == System.Data.ConnectionState.Open)
                            {
                                await connection.CloseAsync();
                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No migrations"))
            {
                // 如果没有迁移配置，使用 EnsureCreatedAsync 作为开发环境的回退
                logger.LogWarning("无法获取迁移信息，使用EnsureCreatedAsync作为回退方案");
                await openIddictDbContext.Database.EnsureCreatedAsync();
                logger.LogInformation("OpenIddict数据库和表已创建（使用EnsureCreatedAsync）");
            }
            
            logger.LogInformation("OpenIddict数据库初始化完成");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "OpenIddict数据库初始化失败: {Message}", ex.Message);
            logger.LogWarning("应用程序将继续启动，但OpenIddict功能可能不可用");
            logger.LogWarning("如果表不存在，请运行: dotnet ef migrations add InitialOpenIddict --project Takt.Infrastructure");
            logger.LogWarning("然后运行: dotnet ef database update --project Takt.Infrastructure");
        }
    }
}
