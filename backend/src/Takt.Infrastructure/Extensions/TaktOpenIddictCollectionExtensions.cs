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
using Microsoft.Data.SqlClient;
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
        // 初始化时 EF 会执行 HasTablesAsync 等查询，库内对象多或网络慢时易超时，故将命令超时设为 120 秒（可在配置中覆盖）
        var openIddictCommandTimeoutSeconds = configuration.GetValue<int>("OpenIddict:CommandTimeoutSeconds", 120);
        services.AddDbContext<TaktOpenIddictDbContext>(options =>
        {
            options.UseSqlServer(openIddictConnectionString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(openIddictCommandTimeoutSeconds);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
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
    /// OpenIddict 官方初始化：有迁移用 MigrateAsync，无迁移用 EnsureCreatedAsync（建库+建表由 EF 一次完成，无需单独建库）；应用/Scope 用 Manager.CreateAsync。
    /// </summary>
    public static async Task<bool> InitializeOpenIddictDatabaseAsync(this WebApplication app)
    {
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(string.Empty);
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaktOpenIddictDbContext>();

        IReadOnlyList<string>? pending = null;
        IReadOnlyList<string>? applied = null;
        try
        {
            logger.LogInformation("[OpenIddict] 检查数据库连接与迁移状态...");
            pending = (await dbContext.Database.GetPendingMigrationsAsync()).ToList();
            applied = (await dbContext.Database.GetAppliedMigrationsAsync()).ToList();
            logger.LogInformation("[OpenIddict] 数据库已连接，迁移状态：待应用 {Pending} 个，已应用 {Applied} 个。", pending.Count, applied.Count);
        }
        catch (SqlException ex) when (ex.Number == 4060)
        {
            // 数据库不存在：不单独建库，直接交给 EnsureCreatedAsync 建库+建表
            logger.LogInformation("[OpenIddict] 数据库不存在（4060），使用 EnsureCreatedAsync 建库并建表...");
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                logger.LogInformation("[OpenIddict] EnsureCreatedAsync 完成，数据库与表已就绪。");
                return true;
            }
            catch (Exception exEnsure)
            {
                logger.LogError(exEnsure, "[OpenIddict] EnsureCreatedAsync 失败: {Message}", exEnsure.Message);
                return false;
            }
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("No migrations", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogInformation("[OpenIddict] 无迁移文件，使用 EnsureCreatedAsync 建库并建表...");
            try
            {
                await dbContext.Database.EnsureCreatedAsync();
                logger.LogInformation("[OpenIddict] EnsureCreatedAsync 完成，数据库与表已就绪。");
                return true;
            }
            catch (Exception exEnsure)
            {
                logger.LogError(exEnsure, "[OpenIddict] EnsureCreatedAsync 失败: {Message}", exEnsure.Message);
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[OpenIddict] 连接数据库或获取迁移状态失败: {Message}", ex.Message);
            return false;
        }

        if (pending.Count > 0)
        {
            var isCreate = applied.Count == 0;
            logger.LogInformation("[OpenIddict] {Action}：应用 {Count} 个迁移...", isCreate ? "建表" : "迁移更新", pending.Count);
            try
            {
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("[OpenIddict] {Action}完成。", isCreate ? "建表" : "迁移更新");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[OpenIddict] 应用迁移失败: {Message}", ex.Message);
                return false;
            }
        }

        if (applied.Count > 0)
        {
            logger.LogInformation("[OpenIddict] 数据库与表已存在，迁移已是最新（已应用 {Count} 个迁移）。", applied.Count);
            return true;
        }

        logger.LogInformation("[OpenIddict] 无迁移文件，使用 EnsureCreatedAsync 建库并建表...");
        try
        {
            await dbContext.Database.EnsureCreatedAsync();
            logger.LogInformation("[OpenIddict] EnsureCreatedAsync 完成，数据库与表已就绪。");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "[OpenIddict] EnsureCreatedAsync 失败: {Message}", ex.Message);
            return false;
        }
    }
}
