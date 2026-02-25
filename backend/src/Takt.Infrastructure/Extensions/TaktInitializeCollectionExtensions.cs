// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktInitializeCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：初始化扩展方法，用于应用程序启动时的初始化逻辑
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using Takt.Application.Services.Captcha;
using Takt.Infrastructure.Authentication;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.Data.Seeds;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 初始化扩展方法
/// </summary>
public static class TaktInitializeCollectionExtensions
{
    /// <summary>
    /// 初始化应用程序数据库和数据
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <param name="configuration">配置</param>
    /// <returns>任务</returns>
    public static async Task InitializeApplicationAsync(this WebApplication app, IConfiguration configuration)
    {
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(string.Empty); // 使用空字符串作为类别名称，不显示类别

        // 1. 优先初始化OpenIddict数据库（仅 EF 迁移，官方方式）；未就绪时返回 false，不再执行应用/Scope 初始化，避免 4060 连串错误
        var openIddictDbReady = await app.InitializeOpenIddictDatabaseAsync();

        // 2. 仅当 OpenIddict 数据库已就绪时，初始化应用和范围（官方 IOpenIddictApplicationManager/IOpenIddictScopeManager）；否则跳过
        if (openIddictDbReady)
        {
            try
            {
                logger.LogInformation("开始初始化OpenIddict应用和范围...");
                await TaktOpenIddictConfig.InitializeAsync(app.Services);
                logger.LogInformation("OpenIddict应用和范围初始化完成");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OpenIddict应用和范围初始化失败: {Message}", ex.Message);
            }
        }
        else
        {
            logger.LogWarning("OpenIddict 数据库未就绪（无迁移或连接失败），已跳过应用和范围初始化。请先执行上述 dotnet ef 命令后再重启。");
        }

        // 3. 判断租户启用状态并输出配置状态
        var tenantSection = configuration.GetSection("Tenant");
        var useTenant = tenantSection.GetValue<bool>("Enabled", false);
        var defaultConfigId = tenantSection["DefaultConfigId"] ?? "0";
        if (!useTenant)
        {
            logger.LogInformation("多租户功能已禁用，多库按实体映射 0～5，不按租户过滤；主库 ConfigId: {DefaultConfigId}", defaultConfigId);
        }
        else
        {
            logger.LogInformation("多租户功能已启用，多库按实体映射 0～5 并按租户过滤；主库 ConfigId: {DefaultConfigId}", defaultConfigId);
        }

        // 输出其他配置状态
        var initSection = configuration.GetSection("Init");
        var initDb = initSection.GetValue<bool>("InitDb", false);
        var seedData = initSection.GetValue<bool>("SeedData", false);
        var singleDeviceLogin = configuration.GetValue<bool>("SingleDeviceLogin", false);
        var demoMode = configuration.GetValue<bool>("DemoMode", false);
        
        logger.LogInformation("数据库初始化: {InitDb}", initDb ? "启用" : "禁用");
        logger.LogInformation("种子数据初始化: {SeedData}", seedData ? "启用" : "禁用");
        logger.LogInformation("单设备登录配置 SingleDeviceLogin: {Value}（false=每账号1台，true=每账号3台）", singleDeviceLogin ? "true" : "false");
        logger.LogInformation("演示模式: {DemoMode}", demoMode ? "启用" : "禁用");

        // 4. 初始化数据表和种子数据
        if (initDb || seedData)
        {
            logger.LogInformation("开始数据库初始化...");

            var sqlSugarClient = app.Services.GetRequiredService<ISqlSugarClient>();
            var dbConfigsSection = configuration.GetSection("dbConfigs");

            // 初始化逻辑：无论 Tenant.Enabled 为何，都初始化 appsettings 里配置的所有库（建表/种子）。
            // 运行时：租户禁用与启用均为多库（按实体映射 0～5）；租户禁用时不按租户过滤，租户启用时按租户过滤。
            var configIds = new List<string>();
            
            if (dbConfigsSection.Exists())
            {
                // 初始化所有配置的数据库
                foreach (var dbConfig in dbConfigsSection.GetChildren())
                {
                    var configId = dbConfig["ConfigId"];
                    if (!string.IsNullOrEmpty(configId))
                    {
                        configIds.Add(configId);
                    }
                }

                // 如果没有配置，至少初始化主库
                if (configIds.Count == 0)
                {
                    configIds.Add(defaultConfigId);
                }
            }
            else
            {
                // 如果没有配置，只初始化默认数据库
                configIds.Add(defaultConfigId);
                logger.LogWarning("未找到 dbConfigs 配置，仅初始化默认数据库 ConfigId: {DefaultConfigId}", defaultConfigId);
            }

            if (useTenant)
            {
                logger.LogInformation("租户功能已启用，将初始化 {Count} 个数据库", configIds.Count);
            }
            else
            {
                logger.LogInformation("租户功能已禁用（但使用多库），将初始化 {Count} 个数据库", configIds.Count);
            }

            var tenant = sqlSugarClient.AsTenant();

            // 阶段一：先为所有 ConfigId 创建数据库（仅建库，不建表、不种子）
            logger.LogInformation("阶段一：创建所有数据库（如不存在）...");
            foreach (var configId in configIds)
            {
                try
                {
                    var db = tenant.GetConnectionScope(configId);
                    if (db != null)
                    {
                        db.DbMaintenance.CreateDatabase();
                        logger.LogInformation("数据库已就绪 ConfigId: {ConfigId}", configId);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "创建数据库失败 ConfigId: {ConfigId}", configId);
                    throw;
                }
            }
            logger.LogInformation("阶段一完成：所有数据库已创建");

            // 阶段二：为每个 ConfigId 初始化表结构
            if (initDb)
            {
                logger.LogInformation("阶段二：初始化所有表结构...");
                foreach (var configId in configIds)
                {
                    try
                    {
                        logger.LogInformation("正在初始化表结构 ConfigId: {ConfigId}", configId);
                        var db = tenant.GetConnectionScope(configId);
                        if (db != null)
                        {
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConfigId = configId;
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConnectionString = db.Ado.Connection.ConnectionString;
                            Takt.Infrastructure.Tenant.TaktTenantContext.DefaultConnectionString = db.Ado.Connection.ConnectionString;
                            var seedDataProviders = app.Services.GetServices<ITaktSeedData>();
                            var initializerLogger = loggerFactory.CreateLogger(string.Empty);
                            var initializer = new TaktDatabaseInitializer(db, seedDataProviders, app.Services, configId, initializerLogger);
                            await initializer.InitializeTablesAsync();
                            logger.LogInformation("表结构初始化完成 ConfigId: {ConfigId}", configId);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "表结构初始化失败 ConfigId: {ConfigId}", configId);
                        throw;
                    }
                }
                logger.LogInformation("阶段二完成：所有表结构已初始化");
            }

            // 阶段三：为每个 ConfigId 执行种子数据
            if (seedData)
            {
                logger.LogInformation("阶段三：执行种子数据...");
                foreach (var configId in configIds)
                {
                    try
                    {
                        logger.LogInformation("正在执行种子数据 ConfigId: {ConfigId}", configId);
                        var db = tenant.GetConnectionScope(configId);
                        if (db != null)
                        {
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConfigId = configId;
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConnectionString = db.Ado.Connection.ConnectionString;
                            Takt.Infrastructure.Tenant.TaktTenantContext.DefaultConnectionString = db.Ado.Connection.ConnectionString;
                            var seedDataProviders = app.Services.GetServices<ITaktSeedData>();
                            var initializerLogger = loggerFactory.CreateLogger(string.Empty);
                            var initializer = new TaktDatabaseInitializer(db, seedDataProviders, app.Services, configId, initializerLogger);
                            var (totalInsertCount, totalUpdateCount) = await initializer.SeedDataAsync();
                            logger.LogInformation("种子数据完成 ConfigId: {ConfigId}, 插入: {TotalInsertCount}, 更新: {TotalUpdateCount}",
                                configId, totalInsertCount, totalUpdateCount);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "种子数据执行失败 ConfigId: {ConfigId}", configId);
                        throw;
                    }
                }
                logger.LogInformation("阶段三完成：所有种子数据已执行");
            }

            logger.LogInformation("数据库初始化完成");
        }

        // 5. 验证码配置与初始化
        var captchaEnabled = configuration.GetValue<bool>("Captcha:Enabled", false);
        var captchaTypeRaw = configuration.GetValue<string>("Captcha:Type") ?? "Behavior";
        var captchaType = string.Equals(captchaTypeRaw, "Slider", StringComparison.OrdinalIgnoreCase) ? "Slider" : "Behavior";

        logger.LogInformation("验证码 - 启用: {Enabled}, 类型: {Type}, 当前启用的验证码类型: {Active}",
            captchaEnabled ? "是" : "否",
            captchaType,
            captchaEnabled ? captchaType : "未启用");

        if (captchaEnabled)
        {
            try
            {
                logger.LogInformation("开始初始化验证码服务（类型: {Type}）...", captchaType);
                _ = app.Services.GetRequiredService<ITaktCaptchaService>();
                logger.LogInformation("验证码服务初始化完成，类型: {Type}", captchaType);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "验证码服务初始化失败");
            }
        }
    }
}
