// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktServiceCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt服务注册扩展方法，统一注册所有应用服务和基础设施服务，充分利用Autofac
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Takt.Application.Services.Identity;
using Takt.Application.Services.Captcha;
using Takt.Application.Services.Code.Generator.CodeEngine;
using Takt.Application.Services.Routine.Tasks.NumberingRule.RuleEngine;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Cache;
using Takt.Infrastructure.Data;
using Takt.Infrastructure.Localization;
using Takt.Infrastructure.Repositories;
using Takt.Infrastructure.Security;
using Takt.Infrastructure.Tenant;
using Takt.Infrastructure.User;
using Takt.Infrastructure.External.Hikvision;
using Takt.Infrastructure.External.Deli;
using Takt.Infrastructure.External.ZKTeco;
using Takt.Application.Services.HumanResource.AttendanceLeave;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// Takt服务注册扩展方法
/// </summary>
public static class TaktServiceCollectionExtensions
{
    /// <summary>
    /// 添加所有Takt服务（统一注册入口）
    /// </summary>
    /// <remarks>
    /// 注意：当前项目使用 Autofac 作为 DI 容器，基础设施服务和应用服务在 Autofac 中注册。
    /// 此方法只注册必须在 IServiceCollection 中注册的框架服务（HttpContextAccessor、MemoryCache、本地化、OpenIddict、SignalR）。
    /// </remarks>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 注册HttpContextAccessor（多个服务需要）
        services.AddHttpContextAccessor();

        // 注册缓存服务（必须在本地化服务之前，因为本地化服务需要内存缓存）
        services.AddTaktCache(configuration);

        // 注册本地化服务（内部会检查内存缓存是否已注册）
        services.AddTaktLocalization();

        // 注册OpenIddict服务
        services.AddTaktOpenIddict(configuration);

        // 注册SignalR服务
        services.AddTaktSignalR();

        // 注册验证码配置选项
        services.Configure<Takt.Shared.Models.TaktCaptchaOptions>(configuration.GetSection("Captcha"));

        // 注册验证码初始化服务（如果验证码已启用）
        var captchaEnabled = configuration.GetValue<bool>("Captcha:Enabled", false);
        if (captchaEnabled)
        {
            services.AddHttpClient();
            services.AddHostedService<TaktCaptchaInitializer>();
        }

        // 注意：基础设施服务（ITaktLocalizer、ITaktUserContext、ITaktTenantContext）和应用服务
        // 在 Autofac 中注册（通过 TaktAutofacModule），不需要在此处注册。
        // Autofac 会提供 IServiceProvider 包装，SignalR Hub 等框架组件可以从 Autofac 容器解析依赖。

        return services;
    }

    /// <summary>
    /// 配置Autofac容器，注册所有Takt服务
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    /// <param name="configuration">配置</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktServices(this ContainerBuilder builder, IConfiguration configuration)
    {
        // 注册基础设施服务
        RegisterInfrastructureServices(builder);

        // 注册应用服务
        RegisterApplicationServices(builder);

        // 注意：SignalR Hub 由 SignalR 框架自动管理，不需要在 Autofac 中注册
        // SignalR 会从 IServiceProvider 中解析 Hub 的依赖

        return builder;
    }

    /// <summary>
    /// 注册基础设施服务
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    private static void RegisterInfrastructureServices(ContainerBuilder builder)
    {
        // 注册本地化器（实现 Domain 层的 ITaktLocalizer）
        builder.RegisterType<TaktLocalizer>()
            .As<ITaktLocalizer>()
            .InstancePerLifetimeScope();

        // 注册缓存服务（实现 Domain 层的 ITaktCache）
        builder.Register(c =>
        {
            var memoryCache = c.ResolveOptional<IMemoryCache>();
            var distributedCache = c.ResolveOptional<IDistributedCache>();
            var options = c.Resolve<IOptions<Takt.Shared.Models.TaktCacheOptions>>();
            var loggerFactory = c.ResolveOptional<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger<TaktCache>();
            return new TaktCache(memoryCache, distributedCache, options, logger);
        }).As<ITaktCache>().InstancePerLifetimeScope();

        // 注册用户提供者（实现 Domain 层的 ITaktUserContext）
        // 使用 TaktSqlSugarDbContext 直接查询数据库，不依赖 ITaktRepository<TaktUser>，避免循环依赖
        builder.RegisterType<TaktUserProvider>()
            .As<ITaktUserContext>()
            .InstancePerLifetimeScope();

        // 注册租户提供者（实现 Domain 层的 ITaktTenantContext）
        // 使用 TaktSqlSugarDbContext 直接查询数据库，不依赖 ITaktRepository<TaktTenant>，避免循环依赖
        builder.RegisterType<TaktTenantProvider>()
            .As<ITaktTenantContext>()
            .InstancePerLifetimeScope();

        // 注册海康 SDK 及适配器（设备工厂按 DeviceType/Manufacturer 路由）
        builder.RegisterType<TaktHikvisionSdkClient>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<TaktHikvisionAttendanceDeviceAdapterService>()
            .As<ITaktHikvisionAttendanceDeviceAdapter>()
            .InstancePerLifetimeScope();
        builder.RegisterType<TaktDeliSdkClient>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<TaktDeliAttendanceDeviceAdapterService>()
            .As<ITaktDeliAttendanceDeviceAdapter>()
            .InstancePerLifetimeScope();
        builder.RegisterType<TaktZKTecoSdkClient>()
            .AsSelf()
            .SingleInstance();
        builder.RegisterType<TaktZKTecoAttendanceDeviceAdapterService>()
            .As<ITaktZKTecoAttendanceDeviceAdapter>()
            .InstancePerLifetimeScope();

        // 注册通用仓储（开放泛型）
        // 注意：TaktRepository<TaktUser> 和 TaktRepository<TaktTenant> 已经在上面单独注册
        // Autofac 会优先使用特定注册，不会使用这个泛型注册来创建它们
        // 其他实体类型的仓储使用此泛型注册（审计用户仅通过 ITaktUserContext / TaktUserProvider 解析）
        builder.RegisterGeneric(typeof(TaktRepository<>))
            .As(typeof(ITaktRepository<>))
            .InstancePerLifetimeScope();

        // 注册数据库元数据提供者（实现 Domain 层 ITaktDatabaseSchemaProvider，Infrastructure 实现）
        builder.RegisterType<TaktDatabaseSchemaProvider>()
            .As<ITaktDatabaseSchemaProvider>()
            .InstancePerLifetimeScope();
    }

    /// <summary>
    /// 注册应用服务（所有以Service结尾的类）
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    private static void RegisterApplicationServices(ContainerBuilder builder)
    {
        // 注册验证码服务（根据配置选择实现）
        RegisterCaptchaService(builder);

        // 注册代码生成核心引擎（类名不以 Service 结尾，需显式注册）
        builder.RegisterType<TaktCodeEngine>()
            .As<ITaktCodeEngine>()
            .InstancePerLifetimeScope();

        // 注册编码规则生成引擎（类名不以 Service 结尾，需显式注册）
        builder.RegisterType<TaktNumberingRuleEngine>()
            .As<ITaktNumberingRuleEngine>()
            .InstancePerLifetimeScope();

        // 注册所有应用服务（自动扫描Takt.Application程序集中以Service结尾的类）
        // 排除验证码服务，因为它们已经手动注册
        builder.RegisterAssemblyTypes(typeof(TaktUserService).Assembly)
            .Where(t => t.Name.EndsWith("Service") && 
                       !t.IsAbstract && 
                       !t.IsInterface &&
                       !t.Name.Contains("Captcha")) // 排除验证码服务
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    /// <summary>
    /// 注册验证码服务（同时注册两个实现，验证时根据验证码ID自动选择）
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    private static void RegisterCaptchaService(ContainerBuilder builder)
    {
        // 注册 Slider 服务（作为命名服务，使用单例模式以保持验证码数据）
        builder.Register(c =>
        {
            var configuration = c.Resolve<IConfiguration>();
            var loggerFactory = c.Resolve<ILoggerFactory>();
            var captchaOptions = c.Resolve<IOptions<Takt.Shared.Models.TaktCaptchaOptions>>();
            var webHostEnvironment = c.ResolveOptional<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();
            return new TaktCaptchaSliderService(configuration, captchaOptions, webHostEnvironment);
        }).Named<ITaktCaptchaService>("Slider").SingleInstance();

        // 注册 Behavior 服务（作为命名服务，使用单例模式以保持验证码数据）
        builder.Register(c =>
        {
            var configuration = c.Resolve<IConfiguration>();
            var loggerFactory = c.Resolve<ILoggerFactory>();
            var captchaOptions = c.Resolve<IOptions<Takt.Shared.Models.TaktCaptchaOptions>>();
            return new TaktCaptchaBehaviorService(configuration, captchaOptions);
        }).Named<ITaktCaptchaService>("Behavior").SingleInstance();

        // 注册默认服务（根据配置选择，用于生成验证码，使用单例模式）
        builder.Register(c =>
        {
            var configuration = c.Resolve<IConfiguration>();
            var captchaType = configuration.GetValue<string>("Captcha:Type", "Behavior") ?? "Behavior";
            var resolver = c.Resolve<IComponentContext>();
            
            return resolver.ResolveNamed<ITaktCaptchaService>(captchaType);
        }).As<ITaktCaptchaService>().SingleInstance();
    }

    /// <summary>
    /// 注册指定的应用服务
    /// </summary>
    /// <typeparam name="TService">服务接口类型</typeparam>
    /// <typeparam name="TImplementation">服务实现类型</typeparam>
    /// <param name="builder">Autofac容器构建器</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktService<TService, TImplementation>(this ContainerBuilder builder)
        where TService : class
        where TImplementation : class, TService
    {
        builder.RegisterType<TImplementation>()
            .As<TService>()
            .InstancePerLifetimeScope();
        return builder;
    }

}
