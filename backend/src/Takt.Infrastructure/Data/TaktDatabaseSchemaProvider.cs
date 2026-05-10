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
using Takt.Domain.Entities;
using Takt.Domain.Entities.Code.Generator;
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

    /// <summary>
    /// 获取所有数据库配置列表（对应 appsettings 中 dbConfigs 的每一项，用于"选中数据库"）。
    /// </summary>
    /// <returns>数据库信息列表（ConfigId、显示名、数据库名）</returns>
    /// <remarks>
    /// 从 IConfiguration 的 "dbConfigs" 节读取所有数据库配置。
    /// 数据库名从连接字符串的 Database= 或 Initial Catalog= 解析。
    /// 显示名优先使用配置的 Name/DisplayName，否则使用数据库名。
    /// </remarks>
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

    /// <summary>
    /// 根据数据库名（RelatedDataBaseName，从 appsettings dbConfigs 的 Conn 解析）解析为 ConfigId。
    /// </summary>
    /// <param name="dataBaseName">数据库名（如 Takt_Identity_Dev）</param>
    /// <returns>对应的 ConfigId，未找到返回 null</returns>
    /// <remarks>
    /// 用于流程表单等场景：前端存储的是数据库名（如 Takt_Identity_Dev），
    /// 需要按当前环境配置解析为 ConfigId，再调用 GetTables/GetColumns 等方法。
    /// 开发/生产环境的数据库名可能不同，但 ConfigId 保持一致。
    /// </remarks>
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

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下的所有数据表列表。
    /// </summary>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <returns>该库下的表名与表描述列表</returns>
    /// <remarks>
    /// 使用 SqlSugar 的 DbMaintenance.GetTableInfoList(false) 获取物理表信息。
    /// 返回所有用户表，不包含系统表。
    /// </remarks>
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

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下"尚未生成控制器"的数据表列表。
    /// </summary>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <returns>未生成控制器的表名与表描述列表（已排除 TaktGenTable 中存在的表）</returns>
    /// <remarks>
    /// 用于代码生成器：获取指定数据库中"尚未生成代码"的表列表。
    /// 实现逻辑：
    /// 1. 获取该数据库下所有物理表（调用 GetTablesAsync）
    /// 2. 扫描所有已存在的 Controller 文件，提取对应的表名
    /// 3. 查询 TaktGenTable，获取已导入的表名
    /// 4. 排除已有控制器或已导入的表（两者满足其一即排除）
    /// 5. 返回剩余的表列表
    /// 
    /// 判断条件：
    /// - 有 Controller → 不显示（代码已生成）
    /// - 有 TaktGenTable 记录 → 不显示（已导入）
    /// - 两者都没有 → 才显示（可以导入生成）
    /// </remarks>
    public async Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesWithGeneratorAsync(string configId)
    {
        if (string.IsNullOrWhiteSpace(configId))
            throw new ArgumentException("ConfigId 不能为空。", nameof(configId));
    
        // 步骤 1：获取该数据库下所有物理表
        var allTables = await GetTablesAsync(configId).ConfigureAwait(false);
        if (allTables.Count == 0)
            return allTables;
    
        // 步骤 2：扫描所有已存在的 Controller，提取对应的表名
        var tablesWithController = GetExistingControllerTableNames();
        var controllerSet = new HashSet<string>(tablesWithController, StringComparer.OrdinalIgnoreCase);
    
        // 步骤 3：查询 TaktGenTable，获取已导入的表名
        var importedTableNames = await GetImportedTableNamesAsync(configId).ConfigureAwait(false);
        var importedSet = new HashSet<string>(importedTableNames, StringComparer.OrdinalIgnoreCase);
    
        // 步骤 4：排除已有控制器或已导入的表，返回剩余表列表
        var result = allTables
            .Where(t => !controllerSet.Contains(t.TableName) && !importedSet.Contains(t.TableName))
            .ToList();
    
        return result;
    }

    /// <summary>
    /// 查询指定数据库中已导入到 TaktGenTable 的表名列表。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <returns>已导入的表名列表</returns>
    private async Task<List<string>> GetImportedTableNamesAsync(string configId)
    {
        try
        {
            var targetDb = _dbContext.GetClientByConfigId(configId);
            return await targetDb.Queryable<TaktGenTable>()
                .Where(g => g.ConfigId == configId && g.IsDeleted == 0)
                .Select(g => g.TableName)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        catch
        {
            // 表不存在或查询失败，返回空列表
            return new List<string>();
        }
    }

    /// <summary>
    /// 扫描所有已存在的 Controller 文件，提取对应的数据库表名。
    /// </summary>
    /// <returns>已有控制器的表名列表（蛇形命名，如 takt_plant）</returns>
    private static List<string> GetExistingControllerTableNames()
    {
        var tableNames = new List<string>();
        
        try
        {
            // 获取当前应用程序域中所有加载的程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var assembly in assemblies)
            {
                // 只扫描 Takt.WebApi 程序集
                if (!assembly.FullName.StartsWith("Takt.WebApi"))
                    continue;

                // 获取所有继承自 TaktControllerBase 的类
                var controllerTypes = assembly.GetTypes()
                    .Where(t => t.IsClass 
                             && !t.IsAbstract 
                             && t.Name.EndsWith("Controller")
                             && t.BaseType != null && t.BaseType.Name == "TaktControllerBase")
                    .ToList();

                foreach (var controllerType in controllerTypes)
                {
                    // 从 Controller 名提取实体名：TaktPlantController -> TaktPlant
                    var controllerName = controllerType.Name;
                    var entityName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
                    
                    // 从实体名转换为表名：TaktPlant -> takt_plant
                    var tableName = ToSnakeCase(entityName);
                    tableNames.Add(tableName);
                }
            }
        }
        catch
        {
            // 如果扫描失败，返回空列表（不排除任何表）
        }

        return tableNames;
    }

    /// <summary>
    /// 将帕斯卡命名转换为蛇形命名。
    /// 例如：TaktPlant -> takt_plant，TaktStandardOperationTime -> takt_standard_operation_time
    /// </summary>
    /// <param name="pascalCase">帕斯卡命名</param>
    /// <returns>蛇形命名</returns>
    private static string ToSnakeCase(string pascalCase)
    {
        if (string.IsNullOrEmpty(pascalCase))
            return pascalCase;

        var result = new System.Text.StringBuilder();
        result.Append(char.ToLowerInvariant(pascalCase[0]));

        for (int i = 1; i < pascalCase.Length; i++)
        {
            if (char.IsUpper(pascalCase[i]))
            {
                result.Append('_');
                result.Append(char.ToLowerInvariant(pascalCase[i]));
            }
            else
            {
                result.Append(pascalCase[i]);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下"包含指定列名"的数据表列表（列名不区分大小写）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="requiredColumnName">必须存在的列名（如 flow_instance_id）</param>
    /// <returns>满足条件的表名与表描述列表</returns>
    /// <remarks>
    /// 用于流程表单等场景，仅展示带特定列（如 flow_instance_id）的表。
    /// 内部调用 GetTablesFilteredAsync 实现。
    /// </remarks>
    public Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesWithRequiredColumnAsync(string configId, string requiredColumnName)
        => GetTablesFilteredAsync(configId, requiredColumnName?.Trim(), null);

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下按"包含某列 / 不包含某列"过滤的数据表列表（列名不区分大小写）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="requiredColumnName">可选。必须存在的列名（如 flow_instance_id），不传则不按包含过滤</param>
    /// <param name="excludedColumnName">可选。必须不存在的列名，不传则不按排除过滤</param>
    /// <returns>满足条件的表名与表描述列表</returns>
    /// <remarks>
    /// 过滤逻辑：
    /// 1. 如果 requiredColumnName 不为空，表必须包含该列
    /// 2. 如果 excludedColumnName 不为空，表必须不包含该列
    /// 3. 两个条件同时满足时才返回该表
    /// 
    /// 注意：此方法需要逐表查询列信息，性能较低，适合表数量较少的场景。
    /// </remarks>
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

    /// <summary>
    /// 获取指定表的列元数据（用于"有表"流程：从数据库导入表结构）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <param name="includeAuditColumns">是否包含可选审计列（id、config_id、created_by、updated_at、is_deleted、flow_instance_id 等）；默认 true。ext_field_json、remark 始终返回</param>
    /// <returns>列信息列表</returns>
    /// <remarks>
    /// 使用 SqlSugar 的 DbMaintenance.GetColumnInfosByTableName 获取列信息。
    /// 当 includeAuditColumns=false 时，过滤掉 AuditColumnNames 集合中的审计列，
    /// 但 ext_field_json 和 remark 始终返回（不在 AuditColumnNames 中）。
    /// </remarks>
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
    /// <returns>任务</returns>
    /// <remarks>
    /// 用于无表流程：代码生成后，用户手动指定实体类型全名，在指定库中创建该实体对应的表。
    /// 实现步骤：
    /// 1. 从已加载的 Takt.* 程序集中解析继承 TaktEntityBase 的类型
    /// 2. 在指定数据库执行 SqlSugar 的 CodeFirst.InitTables(entityType)
    /// 3. 自动创建表结构（包含列、索引、注释等）
    /// 
    /// 如果实体类型不存在或不在 Takt.* 程序集中，抛出 InvalidOperationException。
    /// </remarks>
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

    /// <summary>
    /// 获取当前已加载、可用于"按实体初始化表"的实体类型全名列表（继承 TaktEntityBase 的 Takt.* 程序集类型）。
    /// </summary>
    /// <returns>实体类型全名列表（如 Takt.Domain.Entities.Code.Generator.TaktGenTable）</returns>
    /// <remarks>
    /// 供无表流程中"手动选择实体"使用。
    /// 扫描所有已加载的 Takt.* 程序集，查找继承 TaktEntityBase 的非抽象、非泛型类。
    /// 与 TaktTableInitializer.GetAllEntityTypes 逻辑一致。
    /// </remarks>
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
