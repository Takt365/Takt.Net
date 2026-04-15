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
using Takt.Infrastructure.Data.Seeds.SeedData;
using Takt.Infrastructure.Data.Seeds.SeedI18nData;

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
        // 注册所有种子数据提供者
        builder.RegisterType<TaktTenantSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        // 员工种子 Order=1，家庭成员（紧急联系人）Order=2，用户种子 Order=3，以便用户种子能按 EmployeeCode 查到员工并正确设置 EmployeeId
        builder.RegisterType<TaktEmployeeSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeFamilySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMenuSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDeptSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktPostSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktHolidaySeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRbacSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictTypeSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictDataSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLanguageSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktNumberingRuleSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMenuI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDeptI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktGreetingsI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktQuotesI18nSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktValidationSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMenuEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktPostEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktHolidayEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleMenuEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserRoleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktTenantEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserTenantEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktRoleDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktUserPostEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktPostDelegateEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDeptDelegateEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeePostEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeDeptEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeDelegateEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktShiftScheduleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktWorkShiftEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceSettingEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceDeviceEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktOvertimeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceCorrectionEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceExceptionEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceResultEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendanceSourceEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAttendancePunchEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLeaveEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktGenTableEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktGenTableColumnEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictDataEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktDictTypeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLanguageEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktTranslationEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktSettingsEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktMessageEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktOnlineEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAopLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktLoginLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktOperLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktQuartzLogEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFileEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowSchemeSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowFormSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowSchemeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowFormEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktFlowInstanceEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeAttachmentEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeCareerEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeEducationEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeWorkEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeFamilyEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeContractEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeSkillEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktEmployeeTransferEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktProductionTeamEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCostCenterEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCostElementEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktProfitCenterEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktStandardWageRateEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCompanyEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAccountingTitleEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktAssetEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();
        builder.RegisterType<TaktCountersignFormEntitiesSeedData>().As<ITaktSeedData>().InstancePerLifetimeScope();

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
        // 注册所有种子数据提供者
        services.AddScoped<ITaktSeedData, TaktTenantSeedData>();
        // 员工种子 Order=1，家庭成员（紧急联系人）Order=2，用户种子 Order=3
        services.AddScoped<ITaktSeedData, TaktEmployeeSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeFamilySeedData>();
        services.AddScoped<ITaktSeedData, TaktUserSeedData>();
        services.AddScoped<ITaktSeedData, TaktMenuSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleSeedData>();
        services.AddScoped<ITaktSeedData, TaktDeptSeedData>();
        services.AddScoped<ITaktSeedData, TaktPostSeedData>();
        services.AddScoped<ITaktSeedData, TaktHolidaySeedData>();
        services.AddScoped<ITaktSeedData, TaktRbacSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictTypeSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictDataSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktLanguageSeedData>();
        services.AddScoped<ITaktSeedData, TaktMenuI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktDeptI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktGreetingsI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktQuotesI18nSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktValidationSeedData>();
        services.AddScoped<ITaktSeedData, TaktMenuEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktPostEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktHolidayEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleMenuEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserRoleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktTenantEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserTenantEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktRoleDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktUserPostEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktPostDelegateEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDeptDelegateEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeePostEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeDeptEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeDelegateEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktShiftScheduleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktWorkShiftEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceSettingEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceDeviceEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktOvertimeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceCorrectionEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceExceptionEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceResultEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendanceSourceEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAttendancePunchEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktLeaveEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktGenTableEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktGenTableColumnEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictDataEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktDictTypeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktLanguageEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktTranslationEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktSettingsEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktMessageEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktOnlineEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAopLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktLoginLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktOperLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktQuartzLogEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFileEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowSchemeSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowFormSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowSchemeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowFormEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktFlowInstanceEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeAttachmentEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeCareerEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeEducationEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeWorkEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeFamilyEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeContractEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeSkillEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktEmployeeTransferEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktProductionTeamEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCostCenterEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCostElementEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktProfitCenterEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktStandardWageRateEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCompanyEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAccountingTitleEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktAssetEntitiesSeedData>();
        services.AddScoped<ITaktSeedData, TaktCountersignFormEntitiesSeedData>();

        return services;
    }
}
