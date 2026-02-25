// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktEntityDatabaseMapping.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：实体和种子数据到数据库的映射配置，根据业务领域自动分配
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Infrastructure.Data;

/// <summary>
/// 实体和种子数据到数据库的映射配置
/// </summary>
public static class TaktEntityDatabaseMapping
{
    /// <summary>
    /// 根据实体命名空间获取对应的 ConfigId
    /// </summary>
    /// <param name="entityNamespace">实体命名空间</param>
    /// <returns>ConfigId，如果未找到映射则返回 "0"（主库）</returns>
    public static string GetConfigIdByEntityNamespace(string entityNamespace)
    {
        // 映射规则：根据实体命名空间确定所属数据库（与 appsettings 中 dbConfigs 的 ConfigId 及 Database 对应）
        // ConfigId="0" - Takt_Identity_Dev: Identity, Tenant, Logging
        // ConfigId="1" - Takt_Accounting_Dev: Accounting
        // ConfigId="2" - Takt_HumanResource_Dev: HumanResource（含组织、岗位、人事等）
        // ConfigId="3" - Takt_Logistics_Dev: Logistics, Statistics
        // ConfigId="4" - Takt_Routine_Dev: Routine
        // ConfigId="5" - Takt_Building_Dev: Generator, Workflow

        if (string.IsNullOrEmpty(entityNamespace))
            return "0";

        // 转换为小写以便匹配
        var ns = entityNamespace.ToLowerInvariant();

        // 按 ConfigId 顺序检查（1→2→3→4→5，未匹配则 0）
        // ConfigId="1" - Accounting
        if (ns.Contains(".accounting"))
            return "1";

        // ConfigId="2" - HumanResource
        if (ns.Contains(".humanresource") || ns.Contains(".hrm"))
            return "2";

        // ConfigId="3" - Logistics, Statistics（含 Statistics.Logging 操作/审计日志）
        if (ns.Contains(".logistics") || ns.Contains(".statistics"))
            return "3";

        // ConfigId="4" - Routine（Takt_Routine_Dev）
        if (ns.Contains(".routine"))
            return "4";

        // ConfigId="5" - Generator, Workflow（Takt_Building_Dev）
        if (ns.Contains(".generator") || ns.Contains(".workflow") || ns.Contains(".code"))
            return "5";

        // 未匹配 -> ConfigId="0"（主库 Identity/Tenant/Logging）
        return "0";
    }

    /// <summary>
    /// 根据种子数据类名获取对应的 ConfigId
    /// </summary>
    /// <param name="seedDataClassName">种子数据类名</param>
    /// <returns>ConfigId，如果未找到映射则返回 "0"（主库）</returns>
    public static string GetConfigIdBySeedDataClassName(string seedDataClassName)
    {
        var configIds = GetConfigIdsForSeedDataClassName(seedDataClassName);
        return configIds.FirstOrDefault() ?? "0";
    }

    // Seeds/Data：ConfigId 处处不同，按业务库 0/1/2/3/4/5 映射（见下方规则）。
    // Seeds/Translation：统一 ConfigId=4（Routine 库 Takt_Routine_Dev，翻译表在此库）。
    // ConfigId 0: TenantSeedData, UserSeedData, MenuSeedData, RoleSeedData
    // ConfigId 0+2: RbacSeedData
    // ConfigId 1: CompanySeedData
    // ConfigId 2: DeptSeedData, PostSeedData, HolidaySeedData
    // ConfigId 3: PlantSeedData
    // ConfigId 4: DictData/DictType/Language 等 + 所有 *EntitiesSeedData、*I18nSeedData、*L10nSeedData（Routine 库）

    /// <summary>
    /// 根据种子数据类名获取对应的 ConfigId 列表（支持一个种子在多个库执行，如 TaktRbacSeedData）
    /// </summary>
    /// <param name="seedDataClassName">种子数据类名</param>
    /// <returns>ConfigId 列表</returns>
    public static IEnumerable<string> GetConfigIdsForSeedDataClassName(string seedDataClassName)
    {
        if (string.IsNullOrEmpty(seedDataClassName))
        {
            yield return "0";
            yield break;
        }

        var className = seedDataClassName.ToLowerInvariant();

        // 特殊：RBAC 需写 0 与 2 两个库
        if (className.Contains("rbacseeddata"))
        {
            yield return "0";
            yield return "2";
            yield break;
        }

        // 按 ConfigId 顺序 1→2→3→4→5，未匹配则 0
        // ConfigId="1" - TaktCompanySeedData（公司主数据）。*EntitiesSeedData 翻译种子 → 归 4
        if (className.Contains("accounting") || className.Contains("companyseeddata"))
        {
            yield return "1";
            yield break;
        }

        // ConfigId="2" - Dept/Post/Holiday 等（不含 *EntitiesSeedData）
        if (className.Contains("humanresource") || className.Contains("hrm")
            || className.Contains("deptseeddata") || className.Contains("postseeddata")
            || className.Contains("holidayseeddata"))
        {
            yield return "2";
            yield break;
        }

        // ConfigId="3" - TaktPlantSeedData（工厂主数据）。*EntitiesSeedData 翻译种子 → 归 4
        if (className.Contains("logistics") || className.Contains("statistics") || className.Contains("plantseeddata"))
        {
            yield return "3";
            yield break;
        }

        // ConfigId="4" - Routine 库：字典/语言/国家地区/编码规则 + 所有 *EntitiesSeedData、*I18nSeedData、*L10nSeedData
        if (className.Contains("routine")
            || className.Contains("dictdataseeddata") || className.Contains("dicttypeseeddata") || className.Contains("languageseeddata")
            || className.Contains("regioncountryseeddata")
            || className.Contains("numberingruleseeddata")
            || className.EndsWith("entitiesseeddata")
            || className.Contains("i18nseeddata") || className.Contains("l10nseeddata"))
        {
            yield return "4";
            yield break;
        }

        // ConfigId="5" - Generator/Workflow（Takt_Building_Dev）
        if (className.Contains("generator") || className.Contains("workflow") || className.Contains("code"))
        {
            yield return "5";
            yield break;
        }

        yield return "0";
    }
}
