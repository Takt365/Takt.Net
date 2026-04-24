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

using Microsoft.Extensions.Configuration;

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
        // 映射规则：ConfigId 与库名一一对应
        // ConfigId="0" - Takt_Identity_Dev; "1" - Takt_HumanResource_Dev; "2" - Takt_Routine_Dev;
        // ConfigId="3" - Takt_Building_Dev; "4" - Takt_Accounting_Dev; "5" - Takt_Logistics_Dev

        if (string.IsNullOrEmpty(entityNamespace))
            return "0";

        var ns = entityNamespace.ToLowerInvariant();

        if (ns.Contains(".humanresource") || ns.Contains(".hrm"))
            return "1";
        if (ns.Contains(".routine"))
            return "2";
        if (ns.Contains(".generator") || ns.Contains(".workflow") || ns.Contains(".code"))
            return "3";
        if (ns.Contains(".accounting"))
            return "4";
        if (ns.Contains(".logistics") || ns.Contains(".statistics"))
            return "5";

        return "0";
    }

    /// <summary>
    /// 获取实体类型在持久化时应写入的 ConfigId（与 <see cref="TaktSqlSugarDbContext.GetClient(System.Type?)"/> 路由、以及 appsettings 中 dbConfigs 的 ConfigId 一致）。
    /// </summary>
    /// <param name="entityType">实体 CLR 类型</param>
    /// <param name="configuration">可选；提供时将在 dbConfigs 中校验并统一大小写/空白，与配置文件中的 ConfigId 字符串完全一致。</param>
    /// <returns>分库 ConfigId 字符串（如 "0"～"5"）</returns>
    public static string GetPersistenceConfigIdForEntityType(Type entityType, IConfiguration? configuration = null)
    {
        if (entityType == null)
            return "0";
        var mapped = GetConfigIdByEntityNamespace(entityType.Namespace ?? string.Empty);
        return AlignConfigIdWithDbConfigs(configuration, mapped);
    }

    /// <summary>
    /// 将映射得到的 ConfigId 与 appsettings 中 dbConfigs[*].ConfigId 对齐（存在则返回配置中的原文字符串；不存在则仍返回映射值）。
    /// </summary>
    public static string AlignConfigIdWithDbConfigs(IConfiguration? configuration, string? mappedConfigId)
    {
        var id = string.IsNullOrWhiteSpace(mappedConfigId) ? "0" : mappedConfigId.Trim();
        if (configuration == null)
            return id;

        var section = configuration.GetSection("dbConfigs");
        if (!section.Exists())
            return id;

        foreach (var child in section.GetChildren())
        {
            var cid = child["ConfigId"]?.Trim();
            if (!string.IsNullOrEmpty(cid) && string.Equals(cid, id, StringComparison.OrdinalIgnoreCase))
                return cid;
        }

        return id;
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

        // RBAC：仅在 ConfigId 1（HR 库）执行一次，依赖用户/角色/部门/岗位已就绪
        if (className.Contains("rbacseeddata"))
        {
            yield return "1";
            yield break;
        }

        // 员工、用户、员工家庭成员（紧急联系人）种子：在 ConfigId 1 执行（员工写 HR 库，用户写 Identity 库；须先于 RBAC）
        if (className.EndsWith("employeeseeddata") || className.EndsWith("userseeddata") || className.Contains("employeefamilyseeddata"))
        {
            yield return "1";
            yield break;
        }

        // I18n/Routine 库种子 -> ConfigId="2"
        if (className.EndsWith("i18nseeddata") || className.EndsWith("entitiesseeddata"))
        {
            yield return "2";
            yield break;
        }
        if (className.Contains("numberingruleseeddata"))
        {
            yield return "2";
            yield break;
        }
        if (className.Contains("routine")
            || className.Contains("dictdataseeddata") || className.Contains("dicttypeseeddata") || className.Contains("languageseeddata"))
        {
            yield return "2";
            yield break;
        }

        // Accounting -> ConfigId="4"
        if (className.Contains("accounting"))
        {
            yield return "4";
            yield break;
        }

        // HumanResource -> ConfigId="1"
        if (className.Contains("humanresource") || className.Contains("hrm")
            || className.Contains("deptseeddata") || className.Contains("postseeddata"))
        {
            yield return "1";
            yield break;
        }

        // Logistics、Statistics -> ConfigId="5"
        if (className.Contains("logistics") || className.Contains("statistics"))
        {
            yield return "5";
            yield break;
        }

        // Generator/Workflow -> ConfigId="3"
        if (className.Contains("generator") || className.Contains("workflow") || className.Contains("code")
            || className.Contains("flowformseeddata") || className.Contains("flowschemeseeddata"))
        {
            yield return "3";
            yield break;
        }

        yield return "0";
    }
}
