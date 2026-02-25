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
    /// 添加所有默认的种子数据提供者（用于Autofac容器注册）。按 ConfigId 分大组，按业务领域分小组。
    /// </summary>
    /// <param name="builder">Autofac容器构建器</param>
    /// <returns>容器构建器</returns>
    public static ContainerBuilder AddTaktSeeds(this ContainerBuilder builder)
    {
        // ==================== ConfigId=0（主库 Identity） ====================
        // 身份认证
        builder.RegisterType<TaktMenuSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRbacSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 租户与用户
        builder.RegisterType<TaktTenantSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

        // ==================== ConfigId=1（会计核算 Accounting） ====================
        // 会计核算
        builder.RegisterType<TaktCompanySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

        // ==================== ConfigId=2（人力资源 HumanResource） ====================
        // 人力资源
        builder.RegisterType<TaktDeptSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktPostSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktHolidaySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

        // ==================== ConfigId=3（后勤管理 Logistics） ====================
        // 后勤管理
        builder.RegisterType<TaktPlantSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

        // ==================== ConfigId=4（日常事务 Routine） ====================
        // 身份认证（实体翻译）
        builder.RegisterType<TaktMenuL10nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMenuEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleMenuEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserRoleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserPostEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 租户与用户（实体翻译）
        builder.RegisterType<TaktDeptI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktGreetingsI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktQuotesI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktPostEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktTenantEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserTenantEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 会计核算（实体翻译）
        builder.RegisterType<TaktCompanyEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktBankEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFixedAssetsEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAccountTitleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCostCenterEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCostElementEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktProfitCenterEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktWageRateEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFiscalYearEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktBudgetEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktExpenseEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 后勤管理（实体翻译）
        builder.RegisterType<TaktPlantEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 日常事务
        builder.RegisterType<TaktDictTypeSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictDataSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktNumberingRuleSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLanguageSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRegionCountrySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRegionProvinceSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRegionCitySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRegionCountySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictDataEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictTypeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLanguageEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktTranslationEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktSettingsEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMessageEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktHolidayEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAnnouncementEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEventEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCustomerReceptionEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktNumberingRuleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFileEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDocumentEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 代码生成（实体翻译，对应 Domain.Entities.Generator）
        builder.RegisterType<TaktGenTableEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktGenTableColumnEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 工作流（实体翻译 + 流程方案数据，对应 Domain.Entities.Workflow）
        builder.RegisterType<TaktFlowSchemeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowFormEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowExecutionEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowOperationEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowInstanceEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktWorkflowSchemeSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 统计看板（实体翻译）
        builder.RegisterType<TaktOnlineEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAopLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLoginLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktOperLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktQuartzLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

        // ==================== ConfigId=5（Generator/Workflow，暂无种子预留） ====================

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
    /// 添加所有默认的种子数据提供者（用于Microsoft.Extensions.DependencyInjection）。按 ConfigId 分大组，按业务领域分小组。
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSeeds(this IServiceCollection services)
    {
        // ==================== ConfigId=0（主库 Identity） ====================
        // 身份认证
        services.AddScoped<ITaktSeedData, TaktMenuSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleSeedData>();
        services.AddScoped<ITaktSeedData, TaktRbacSeedData>();
        services.AddScoped<ITaktSeedData, TaktTenantSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserSeedData>();

        // ==================== ConfigId=1（会计核算 Accounting） ====================
        // 会计核算
        services.AddScoped<ITaktSeedData, TaktCompanySeedData>();

        // ==================== ConfigId=2（人力资源 HumanResource） ====================
        // 人力资源
        services.AddScoped<ITaktSeedData, TaktDeptSeedData>();
        services.AddScoped<ITaktSeedData, TaktPostSeedData>();
        services.AddScoped<ITaktSeedData, TaktHolidaySeedData>();

        // ==================== ConfigId=3（后勤管理 Logistics） ====================
        // 后勤管理
        services.AddScoped<ITaktSeedData, TaktPlantSeedData>();

        // ==================== ConfigId=4（日常事务 Routine） ====================
        // 身份认证（实体翻译）
        services.AddScoped<ITaktSeedData, TaktMenuL10nSeedData>();
        services.AddScoped<ITaktSeedData, TaktMenuEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleMenuEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserRoleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserPostEntitiesSeedData>();
        // 租户与用户（实体翻译）
        services.AddScoped<ITaktSeedData, TaktDeptI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktGreetingsI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktQuotesI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktPostEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktTenantEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserTenantEntitiesSeedData>();
        // 会计核算（实体翻译）
        services.AddScoped<ITaktSeedData, TaktCompanyEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktBankEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFixedAssetsEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAccountTitleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCostCenterEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCostElementEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktProfitCenterEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktWageRateEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFiscalYearEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktBudgetEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktExpenseEntitiesSeedData>();
        // 后勤管理（实体翻译）
        services.AddScoped<ITaktSeedData, TaktPlantEntitiesSeedData>();
        // 日常事务
        services.AddScoped<ITaktSeedData, TaktDictTypeSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictDataSeedData>();
        services.AddScoped<ITaktSeedData, TaktNumberingRuleSeedData>();
        services.AddScoped<ITaktSeedData, TaktLanguageSeedData>();
        services.AddScoped<ITaktSeedData, TaktRegionCountrySeedData>();
        services.AddScoped<ITaktSeedData, TaktRegionProvinceSeedData>();
        services.AddScoped<ITaktSeedData, TaktRegionCitySeedData>();
        services.AddScoped<ITaktSeedData, TaktRegionCountySeedData>();
        services.AddScoped<ITaktSeedData, TaktDictDataEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictTypeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktLanguageEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktTranslationEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktSettingsEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktMessageEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktHolidayEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAnnouncementEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEventEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCustomerReceptionEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktNumberingRuleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFileEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDocumentEntitiesSeedData>();
        // 代码生成（实体翻译，对应 Domain.Entities.Generator）
        services.AddScoped<ITaktSeedData, TaktGenTableEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktGenTableColumnEntitiesSeedData>();
        // 工作流（实体翻译 + 流程方案数据，对应 Domain.Entities.Workflow）
        services.AddScoped<ITaktSeedData, TaktFlowSchemeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowFormEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowExecutionEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowOperationEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowInstanceEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktWorkflowSchemeSeedData>();
        // 统计看板（实体翻译）
        services.AddScoped<ITaktSeedData, TaktOnlineEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAopLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktLoginLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktOperLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktQuartzLogEntitiesSeedData>();

        // ==================== ConfigId=5（Generator/Workflow，暂无种子预留） ====================

        return services;
    }
}
