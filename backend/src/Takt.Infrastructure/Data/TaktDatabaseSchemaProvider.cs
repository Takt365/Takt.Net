// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data
// 文件名称：TaktDatabaseSchemaProvider.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：数据库元数据提供者实现：从 dbConfigs 获取所有数据库，根据 ConfigId 通过 SqlSugar 获取该库下的数据表列表。
// 实体行级 config_id 与分库 ConfigId 的统一规则见 TaktEntityDatabaseMapping.GetPersistenceConfigIdForEntityType 与 TaktRepository.EntityPersistenceConfigId。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Reflection;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using Takt.Domain.Entities;
using Takt.Domain.Interfaces;

namespace Takt.Infrastructure.Data;

/// <summary>
/// 数据库元数据提供者：获取所有数据库配置、根据 ConfigId 获取数据表/列列表、表注释、表是否存在、执行 DDL。
/// </summary>
public class TaktDatabaseSchemaProvider : ITaktDatabaseSchemaProvider
{
    /// <summary>
    /// 可选显隐的审计/通用列名（不区分大小写）。GetColumnsAsync(includeAuditColumns: false) 时仅过滤这些列。
    /// ExtFieldJson(ext_field_json)、Remark(remark) 必须显示，不在此集合中。
    /// </summary>
    private static readonly HashSet<string> AuditColumnNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "id", "config_id",
        "created_id", "created_by", "created_at", "updated_id", "updated_by", "updated_at",
        "is_deleted", "deleted_id", "deleted_by", "deleted_at",
        "flow_instance_id"
    };

    private readonly TaktSqlSugarDbContext _dbContext;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContext">SqlSugar 数据库上下文</param>
    /// <param name="configuration">配置（用于读取 dbConfigs）</param>
    public TaktDatabaseSchemaProvider(TaktSqlSugarDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<TaktDatabaseInfo>> GetDatabasesAsync()
    {
        var list = new List<TaktDatabaseInfo>();
        var dbConfigsSection = _configuration.GetSection("dbConfigs");
        if (!dbConfigsSection.Exists())
            return Task.FromResult<IReadOnlyList<TaktDatabaseInfo>>(list);

        foreach (var dbConfig in dbConfigsSection.GetChildren())
        {
            var configId = dbConfig["ConfigId"];
            if (string.IsNullOrWhiteSpace(configId))
                continue;
            var conn = dbConfig["Conn"];
            var dataBaseName = GetDatabaseNameFromConnectionString(conn) ?? configId;
            var displayName = dbConfig["Name"] ?? dbConfig["DisplayName"] ?? dataBaseName;
            list.Add(new TaktDatabaseInfo
            {
                ConfigId = configId,
                DisplayName = displayName,
                DataBaseName = dataBaseName
            });
        }

        return Task.FromResult<IReadOnlyList<TaktDatabaseInfo>>(list);
    }

    /// <inheritdoc />
    public string? ResolveDataBaseNameToConfigId(string dataBaseName)
    {
        if (string.IsNullOrWhiteSpace(dataBaseName))
            return null;
        var dbConfigsSection = _configuration.GetSection("dbConfigs");
        if (!dbConfigsSection.Exists())
            return null;
        var key = dataBaseName.Trim();
        foreach (var dbConfig in dbConfigsSection.GetChildren())
        {
            var configId = dbConfig["ConfigId"];
            if (string.IsNullOrWhiteSpace(configId))
                continue;
            var conn = dbConfig["Conn"];
            var name = GetDatabaseNameFromConnectionString(conn);
            if (!string.IsNullOrEmpty(name) && string.Equals(name, key, StringComparison.OrdinalIgnoreCase))
                return configId;
        }
        return null;
    }

    /// <summary>
    /// 从连接字符串中解析 Database 或 Initial Catalog 作为显示名（当配置未提供 Name/DisplayName 时使用）。
    /// </summary>
    private static string? GetDatabaseNameFromConnectionString(string? conn)
    {
        if (string.IsNullOrWhiteSpace(conn))
            return null;
        const StringComparison cmp = StringComparison.OrdinalIgnoreCase;
        var span = conn.AsSpan().Trim();
        while (span.Length > 0)
        {
            var semi = span.IndexOf(';');
            var segment = semi >= 0 ? span[..semi].Trim() : span;
            if (segment.StartsWith("Database=".AsSpan(), cmp) || segment.StartsWith("Initial Catalog=".AsSpan(), cmp))
            {
                var eq = segment.IndexOf('=');
                if (eq >= 0 && eq + 1 < segment.Length)
                {
                    var value = segment[(eq + 1)..].Trim();
                    return value.Length > 0 ? value.ToString() : null;
                }
            }
            span = semi >= 0 && semi + 1 < span.Length ? span[(semi + 1)..].Trim() : default;
        }
        return null;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesAsync(string configId)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));

        var db = _dbContext.GetClientByConfigId(configId);
        var tableInfoList = db.DbMaintenance.GetTableInfoList(false);
        var result = new List<TaktDatabaseTableInfo>(tableInfoList?.Count ?? 0);
        if (tableInfoList == null)
            return result;

        foreach (var t in tableInfoList)
        {
            var name = t.Name ?? string.Empty;
            var description = t.Description;
            result.Add(new TaktDatabaseTableInfo
            {
                TableName = name,
                TableComment = description
            });
        }

        return await Task.FromResult(result).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesWithRequiredColumnAsync(string configId, string requiredColumnName)
        => GetTablesFilteredAsync(configId, requiredColumnName?.Trim(), null);

    /// <inheritdoc />
    public async Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesFilteredAsync(string configId, string? requiredColumnName, string? excludedColumnName)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));

        var allTables = await GetTablesAsync(configId).ConfigureAwait(false);
        var hasRequired = !string.IsNullOrWhiteSpace(requiredColumnName);
        var hasExcluded = !string.IsNullOrWhiteSpace(excludedColumnName);
        if (!hasRequired && !hasExcluded)
            return allTables;

        var required = requiredColumnName?.Trim() ?? string.Empty;
        var excluded = excludedColumnName?.Trim() ?? string.Empty;
        var result = new List<TaktDatabaseTableInfo>();
        foreach (var t in allTables)
        {
            var tableName = t.TableName ?? string.Empty;
            if (string.IsNullOrEmpty(tableName)) continue;
            var columns = await GetColumnsAsync(configId, tableName, includeAuditColumns: true).ConfigureAwait(false);
            var columnNames = columns.Select(c => c.DbColumnName?.Trim() ?? string.Empty).ToList();
            var hasRequiredCol = !hasRequired || columnNames.Any(n => string.Equals(n, required, StringComparison.OrdinalIgnoreCase));
            var hasExcludedCol = hasExcluded && columnNames.Any(n => string.Equals(n, excluded, StringComparison.OrdinalIgnoreCase));
            if (hasRequiredCol && !hasExcludedCol)
                result.Add(new TaktDatabaseTableInfo { TableName = tableName, TableComment = t.TableComment });
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TaktDatabaseColumnInfo>> GetColumnsAsync(string configId, string tableName, bool includeAuditColumns = true)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("表名不能为空。", nameof(tableName));

        var db = _dbContext.GetClientByConfigId(configId);
        var columns = db.DbMaintenance.GetColumnInfosByTableName(tableName);
        var result = new List<TaktDatabaseColumnInfo>(columns?.Count ?? 0);
        if (columns == null)
            return result;

        foreach (var c in columns)
        {
            var dbColName = c.DbColumnName ?? string.Empty;
            if (!includeAuditColumns && AuditColumnNames.Contains(dbColName))
                continue;
            result.Add(new TaktDatabaseColumnInfo
            {
                DbColumnName = dbColName,
                ColumnDescription = c.ColumnDescription,
                DataType = c.DataType ?? "nvarchar",
                Length = c.Length,
                DecimalDigits = c.DecimalDigits,
                IsPrimarykey = c.IsPrimarykey,
                IsIdentity = c.IsIdentity,
                IsNullable = c.IsNullable
            });
        }

        return await Task.FromResult(result).ConfigureAwait(false);
    }

    /// <summary>
    /// 获取指定表的表注释/描述。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <returns>表注释，无则返回 null</returns>
    /// <remarks>从 GetTableInfoList 中按表名匹配取 Description。</remarks>
    public async Task<string?> GetTableCommentAsync(string configId, string tableName)
    {
        if (string.IsNullOrWhiteSpace(configId) || string.IsNullOrWhiteSpace(tableName))
            return null;

        var db = _dbContext.GetClientByConfigId(configId);
        var tableInfoList = db.DbMaintenance.GetTableInfoList(false);
        var t = tableInfoList?.FirstOrDefault(x => string.Equals(x.Name, tableName, StringComparison.OrdinalIgnoreCase));
        return await Task.FromResult(t?.Description).ConfigureAwait(false);
    }

    /// <summary>
    /// 判断指定表是否已存在。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <returns>存在返回 true</returns>
    /// <remarks>使用 DbMaintenance.IsAnyTable 判断表是否存在。</remarks>
    public async Task<bool> TableExistsAsync(string configId, string tableName)
    {
        if (string.IsNullOrWhiteSpace(configId) || string.IsNullOrWhiteSpace(tableName))
            return false;

        var db = _dbContext.GetClientByConfigId(configId);
        var exists = db.DbMaintenance.IsAnyTable(tableName);
        return await Task.FromResult(exists).ConfigureAwait(false);
    }

    /// <summary>
    /// 执行 DDL（如 CREATE TABLE）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="sql">DDL 语句</param>
    /// <remarks>使用 Ado.ExecuteCommandAsync 执行 DDL（如 CREATE TABLE）。</remarks>
    public async Task ExecuteDdlAsync(string configId, string sql)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentException("SQL 不能为空。", nameof(sql));

        var db = _dbContext.GetClientByConfigId(configId);
        await db.Ado.ExecuteCommandAsync(sql).ConfigureAwait(false);
    }

    /// <summary>
    /// 按实体类型初始化数据表（与 TaktTableInitializer 一致：SqlSugar CodeFirst.InitTables）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="entityTypeFullName">实体类型全名（如 Takt.Domain.Entities.Code.Generator.TaktGenTable）</param>
    /// <remarks>从已加载的 Takt.* 程序集中解析继承 TaktEntityBase 的类型，在指定库执行 CodeFirst.InitTables。</remarks>
    public async Task InitializeTableFromEntityTypeAsync(string configId, string entityTypeFullName)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));
        if (string.IsNullOrWhiteSpace(entityTypeFullName))
            throw new ArgumentException("实体类型全名不能为空。", nameof(entityTypeFullName));

        var entityType = ResolveEntityType(entityTypeFullName);
        if (entityType == null)
            throw new InvalidOperationException($"未找到实体类型：{entityTypeFullName}。请确认类型全名正确且实体已编译到当前运行的 Takt 程序集中。");

        var db = _dbContext.GetClientByConfigId(configId);
        db.CodeFirst.InitTables(entityType);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    /// <inheritdoc />
    /// <remarks>扫描已加载的 Takt.* 程序集中继承 TaktEntityBase 的类型，返回 FullName 列表（与 TaktTableInitializer 一致）。</remarks>
    public Task<IReadOnlyList<string>> GetAvailableEntityTypeFullNamesAsync()
    {
        var fullNames = GetAllEntityTypes().Select(t => t.FullName).Where(s => !string.IsNullOrEmpty(s)).Cast<string>().ToList();
        return Task.FromResult<IReadOnlyList<string>>(fullNames);
    }

    /// <summary>
    /// 从已加载的 Takt.* 程序集中解析继承 TaktEntityBase 的实体类型（与 TaktTableInitializer 一致）。
    /// </summary>
    private static Type? ResolveEntityType(string entityTypeFullName)
    {
        var candidates = GetAllEntityTypes();
        return candidates.FirstOrDefault(t =>
            string.Equals(t.FullName, entityTypeFullName, StringComparison.Ordinal) ||
            string.Equals(t.AssemblyQualifiedName, entityTypeFullName, StringComparison.Ordinal));
    }

    /// <summary>
    /// 获取所有继承 TaktEntityBase 的实体类型（与 TaktTableInitializer.GetAllEntityTypes 一致）。
    /// </summary>
    private static Type[] GetAllEntityTypes()
    {
        var entityBaseType = typeof(TaktEntityBase);
        var entityTypes = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName != null &&
                        (a.FullName.StartsWith("Takt.", StringComparison.OrdinalIgnoreCase) ||
                         a.GetName().Name?.StartsWith("Takt.", StringComparison.OrdinalIgnoreCase) == true))
            .ToList();

        foreach (var assembly in assemblies)
        {
            try
            {
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.IsSubclassOf(entityBaseType))
                    .ToList();
                entityTypes.AddRange(types);
            }
            catch (ReflectionTypeLoadException ex)
            {
                var loadedTypes = ex.Types?.Where(t => t != null).Cast<Type>()
                    .Where(t => t!.IsClass && !t.IsAbstract && !t.IsGenericType && t.IsSubclassOf(entityBaseType))
                    .ToList() ?? new List<Type>();
                entityTypes.AddRange(loadedTypes);
            }
            catch (Exception)
            {
                continue;
            }
        }

        return entityTypes.Distinct().ToArray();
    }
}
