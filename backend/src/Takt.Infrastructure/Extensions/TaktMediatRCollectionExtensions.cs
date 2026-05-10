// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Extensions
// 文件名称：TaktMediatRCollectionExtensions.cs
// 创建时间：2026-04-27
// 创建人：Takt365
// 功能描述：MediatR 事件总线注册扩展方法
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Reflection;
using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Events;
using Takt.Infrastructure.EventBus.Behaviors;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// MediatR 事件总线注册扩展方法
/// </summary>
public static class TaktMediatRCollectionExtensions
{
    /// <summary>
    /// 添加 MediatR 事件总线服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="assemblies">要扫描的程序集（用于自动发现事件处理器）</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktMediatR(
        this IServiceCollection services, 
        params Assembly[] assemblies)
    {
        // 如果没有传入程序集，默认扫描 Domain 和 Application 程序集
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = new[]
            {
                typeof(TaktDomainEvent).Assembly, // Takt.Domain
                Assembly.Load("Takt.Application") // Takt.Application
            };
        }

        // 注册 MediatR 服务
        services.AddMediatR(cfg =>
        {
            // 注册行为管道（可用于日志、事务、异常处理等）
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(EventLoggingBehavior<,>));
            
            // 扫描指定程序集中的所有 INotificationHandler
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        return services;
    }

    /// <summary>
    /// 在 Autofac 中注册 MediatR 事件处理器
    /// </summary>
    /// <param name="builder">Autofac 容器构建器</param>
    /// <param name="assemblies">要扫描的程序集</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktMediatRHandlers(
        this ContainerBuilder builder,
        params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = new[]
            {
                typeof(TaktDomainEvent).Assembly,
                Assembly.Load("Takt.Application")
            };
        }

        // 自动注册所有 INotificationHandler 实现
        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(MediatR.INotificationHandler<>))
            .InstancePerLifetimeScope();

        return builder;
    }
}
