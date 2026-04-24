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

        // 1. 优先初始化OpenIddict数据库
        await app.InitializeOpenIddictDatabaseAsync();

        // 2. 初始化OpenIddict应用和范围
        try
        {
            logger.LogInformation("开始初始化OpenIddict应用和范围...");
            await TaktOpenIddictConfig.InitializeAsync(app.Services);
            logger.LogInformation("OpenIddict应用和范围初始化完成");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "OpenIddict应用和范围初始化失败: {Message}", ex.Message);
            // 不抛出异常，允许应用程序继续启动，但记录错误
            // 注意：这可能导致认证功能不可用
        }

        // 3. 判断租户启用状态并输出配置状态
        var tenantSection = configuration.GetSection("Tenant");
        var useTenant = tenantSection.GetValue<bool>("Enabled", false);
        var defaultConfigId = tenantSection["DefaultConfigId"] ?? "0";
        if (!useTenant)
        {
            logger.LogInformation("多租户功能已禁用，使用主库配置 ConfigId: {DefaultConfigId}", defaultConfigId);
        }
        else
        {
            logger.LogInformation("多租户功能已启用，主库 ConfigId: {DefaultConfigId}", defaultConfigId);
        }

        // 输出其他配置状态
        var initSection = configuration.GetSection("Init");
        var initDb = initSection.GetValue<bool>("InitDb", false);
        var seedData = initSection.GetValue<bool>("SeedData", false);
        var singleLogin = configuration.GetValue<bool>("SingleLogin", false);
        var demoMode = configuration.GetValue<bool>("DemoMode", false);
        
        logger.LogInformation("数据库初始化: {InitDb}", initDb ? "启用" : "禁用");
        logger.LogInformation("种子数据初始化: {SeedData}", seedData ? "启用" : "禁用");
        logger.LogInformation("单点登录: {SingleLogin}", singleLogin ? "启用" : "禁用");
        logger.LogInformation("演示模式: {DemoMode}", demoMode ? "启用" : "禁用");

        // 4. 初始化数据表和种子数据
        if (initDb || seedData)
        {
            logger.LogInformation("开始数据库初始化...");

            var sqlSugarClient = app.Services.GetRequiredService<ISqlSugarClient>();
            var dbConfigsSection = configuration.GetSection("dbConfigs");

            // 初始化逻辑：
            // 无论 Tenant.Enabled 是 true 还是 false，都需要初始化所有配置的数据库
            // Tenant.Enabled = false：不启用租户管理，但使用多库，只有一条租户记录 ConfigId="0"，不需要过滤
            // Tenant.Enabled = true：启用租户管理，多租户多库，需要过滤
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

                // 初始化顺序与 ConfigId 一致：0=Identity, 1=HumanResource, 2=Routine, 3=Building, 4=Accounting, 5=Logistics
                var configIdOrder = new[] { "0", "1", "2", "3", "4", "5" };
                configIds.Sort((a, b) =>
                {
                    var ia = Array.IndexOf(configIdOrder, a);
                    var ib = Array.IndexOf(configIdOrder, b);
                    if (ia < 0) ia = int.MaxValue;
                    if (ib < 0) ib = int.MaxValue;
                    return ia.CompareTo(ib);
                });
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

            // 步骤1：先初始化所有数据表（所有 ConfigId 的表结构创建完成后再执行种子）
            if (initDb)
            {
                logger.LogInformation("步骤1：开始初始化所有数据表...");
                foreach (var configId in configIds)
                {
                    try
                    {
                        logger.LogInformation("正在初始化数据表 ConfigId: {ConfigId}", configId);
                        var tenant = sqlSugarClient.AsTenant();
                        var db = tenant.GetConnectionScope(configId);
                        if (db != null)
                        {
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConfigId = configId;
                            Takt.Infrastructure.Tenant.TaktTenantContext.CurrentConnectionString = db.Ado.Connection.ConnectionString;
                            Takt.Infrastructure.Tenant.TaktTenantContext.DefaultConnectionString = db.Ado.Connection.ConnectionString;
                            var initializerLogger = loggerFactory.CreateLogger(string.Empty);
                            var initializer = new TaktDatabaseInitializer(db, app.Services.GetServices<ITaktSeedData>(), app.Services, configId, initializerLogger);
                            await initializer.InitializeTablesAsync();
                            logger.LogInformation("数据表初始化完成 ConfigId: {ConfigId}", configId);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "数据表初始化失败 ConfigId: {ConfigId}", configId);
                        throw;
                    }
                }
                logger.LogInformation("步骤1：所有数据表初始化完成");
            }

            // 步骤2：再初始化所有种子数据（RBAC 在 ConfigId 1 执行且 Order=999，将 ConfigId 1 放在最后遍历以确保 RBAC 种子最后执行）
            if (seedData)
            {
                logger.LogInformation("步骤2：开始初始化所有种子数据...");
                var seedConfigIdOrder = new[] { "0", "2", "3", "4", "5", "1" };
                var seedConfigIds = configIds
                    .OrderBy(c => { var i = Array.IndexOf(seedConfigIdOrder, c); return i < 0 ? int.MaxValue : i; })
                    .ToList();
                foreach (var configId in seedConfigIds)
                {
                    try
                    {
                        logger.LogInformation("正在初始化种子数据 ConfigId: {ConfigId}", configId);
                        var tenant = sqlSugarClient.AsTenant();
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
                            logger.LogInformation("种子数据初始化完成 ConfigId: {ConfigId}, 总计 - 插入: {TotalInsertCount}, 更新: {TotalUpdateCount}",
                                configId, totalInsertCount, totalUpdateCount);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "种子数据初始化失败 ConfigId: {ConfigId}", configId);
                        throw;
                    }
                }
                logger.LogInformation("步骤2：所有种子数据初始化完成");
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
