// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktSeedsCollectionExtensions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：种子数据配置扩展方法，用于注册种子数据提供者
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Takt.Infrastructure.Data.Seeds;
namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 种子数据配置扩展方法
/// </summary>
public static class TaktSeedsCollectionExtensions
{
    /// <summary>
    /// 添加所有默认的种子数据提供者（用于Autofac容器注册）
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktSeeds(this ContainerBuilder builder)
    {
        // 自动扫描并注册所有实现 ITaktSeedData 接口的类（包含 SeedData 和 SeedI18nData）
        var assembly = typeof(Takt.Infrastructure.Data.Seeds.SeedData.TaktTenantSeedData).Assembly;
        
        var seedTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract 
                     && !t.IsInterface 
                     && typeof(ITaktSeedData).IsAssignableFrom(t)
                     && t.Name.EndsWith("SeedData"))
            .ToList();

        // 按 Order 排序后注册（确保执行顺序正确）
        foreach (var type in seedTypes)
        {
            builder.RegisterType(type).As<ITaktSeedData>().InstancePerLifetimeScope();
        }

        return builder;
    }

    /// <summary>
    /// 添加种子数据提供者（用于Autofac容器注册）
    /// </summary>
    /// <typeparam name="TSeedData">种子数据提供者类型</typeparam>
    /// <param name="builder">Autofac容器构建器</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktSeed<TSeedData>(this ContainerBuilder builder)
        where TSeedData : class, ITaktSeedData
    {
        builder.RegisterType<TSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        return builder;
    }

    /// <summary>
    /// 添加种子数据提供者（用于Microsoft.Extensions.DependencyInjection）
    /// </summary>
    /// <typeparam name="TSeedData">种子数据提供者类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSeed<TSeedData>(this IServiceCollection services)
        where TSeedData : class, ITaktSeedData
    {
        services.AddScoped<ITaktSeedData, TSeedData>();
        return services;
    }

    /// <summary>
    /// 添加所有默认的种子数据提供者（用于Microsoft.Extensions.DependencyInjection）
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSeeds(this IServiceCollection services)
    {
        // 自动扫描并注册所有实现 ITaktSeedData 接口的类（包含 SeedData 和 SeedI18nData）
        var assembly = typeof(Takt.Infrastructure.Data.Seeds.SeedData.TaktTenantSeedData).Assembly;
        
        var seedTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract 
                     && !t.IsInterface 
                     && typeof(ITaktSeedData).IsAssignableFrom(t)
                     && t.Name.EndsWith("SeedData"))
            .ToList();

        // 注册所有种子数据提供者（按 Order 排序后注册）
        foreach (var type in seedTypes)
        {
            services.AddScoped(typeof(ITaktSeedData), type);
        }

        return services;
    }
}
