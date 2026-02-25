// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Generator.CodeEngine
// 文件名称：TaktCodeGenWorkflowService.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成工作流实现：有表/无表两条独立流程，均通过通用代码生成引擎（ITaktCodeEngine）生成后端/前端代码
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Code.Generator;
using Takt.Domain.Entities.Code.Generator;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Domain.Validation;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Code.Generator.CodeEngine;

/// <summary>
/// 代码生成工作流服务：有表（选库→选表→导入→生成代码）、无表（建表配置→建物理表→生成代码）两条独立实现，均使用通用代码生成引擎。
/// </summary>
public class TaktCodeGenWorkflowService : ITaktCodeGenWorkflowService
{
    private readonly ITaktDatabaseSchemaProvider _schemaProvider;
    private readonly ITaktRepository<TaktGenTable> _genTableRepository;
    private readonly ITaktRepository<TaktGenTableColumn> _genTableColumnRepository;
    private readonly ITaktCodeEngine _codeEngine;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schemaProvider">数据库元数据提供者（获取表、列、建表等）</param>
    /// <param name="genTableRepository">代码生成表配置仓储</param>
    /// <param name="genTableColumnRepository">代码生成字段配置仓储</param>
    /// <param name="codeEngine">通用代码生成引擎（Scriban 渲染）</param>
    public TaktCodeGenWorkflowService(
        ITaktDatabaseSchemaProvider schemaProvider,
        ITaktRepository<TaktGenTable> genTableRepository,
        ITaktRepository<TaktGenTableColumn> genTableColumnRepository,
        ITaktCodeEngine codeEngine)
    {
        _schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
        _genTableRepository = genTableRepository ?? throw new ArgumentNullException(nameof(genTableRepository));
        _genTableColumnRepository = genTableColumnRepository ?? throw new ArgumentNullException(nameof(genTableColumnRepository));
        _codeEngine = codeEngine ?? throw new ArgumentNullException(nameof(codeEngine));
    }

    /// <summary>
    /// 根据 ConfigId 获取该库下所有数据表，供选表（有表流程第 1 步）。
    /// </summary>
    /// <remarks>有表流程第 1 步：根据 ConfigId 获取该库下所有数据表，供选表。</remarks>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <returns>表名与表描述列表</returns>
    public async Task<List<TaktDatabaseTableInfoDto>> GetDatabaseTablesAsync(string configId)
    {
        var tables = await _schemaProvider.GetTablesAsync(configId).ConfigureAwait(false);
        return tables.Select(t => new TaktDatabaseTableInfoDto
        {
            TableName = t.TableName,
            TableComment = t.TableComment
        }).ToList();
    }

    /// <summary>
    /// 从数据库导入指定表及列元数据，写入 TaktGenTable、TaktGenTableColumn（有表流程第 2 步）。
    /// </summary>
    /// <remarks>有表流程第 2 步：从数据库导入指定表及列元数据，写入 TaktGenTable、TaktGenTableColumn。生成代码时走通用引擎。</remarks>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">要导入的数据表名</param>
    /// <param name="tableOverrides">表配置覆盖（可选，用于补充实体类名、业务名等）</param>
    /// <returns>导入后的表配置 DTO（含表 ID，可用于后续生成代码）</returns>
    public async Task<TaktGenTableDto> ImportTableFromDatabaseAsync(string configId, string tableName, TaktGenTableCreateDto? tableOverrides = null)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            throw new ArgumentException("表名不能为空。", nameof(tableName));

        // 重复导入直接提示，避免唯一索引冲突
        TaktLogger.Information("[CodeGenWorkflow] 开始从数据库导入表: ConfigId={ConfigId}, TableName={TableName}", configId, tableName);
        await TaktUniqueValidatorExtensions.ValidateUniqueAsync(
            _genTableRepository,
            t => t.TableName,
            tableName,
            null,
            $"表 {tableName} 已导入，请勿重复导入").ConfigureAwait(false);

        var columns = await _schemaProvider.GetColumnsAsync(configId, tableName).ConfigureAwait(false);
        if (columns == null || columns.Count == 0)
            throw new InvalidOperationException($"未找到表 {tableName} 的列信息。");

        var tableComment = await _schemaProvider.GetTableCommentAsync(configId, tableName).ConfigureAwait(false);
        var (namePrefix, genModuleName, genBusinessNameRaw) = ParseTableNameParts(tableName);
        // 实体类名与项目约定一致：Takt + 业务名帕斯卡，如 TaktCompany、TaktStandardOperationTime
        var businessPascal = ToPascalCase(genBusinessNameRaw);
        var entityClassName = tableOverrides?.EntityClassName ?? (string.IsNullOrEmpty(businessPascal) ? ToPascalCase(tableName) : "Takt" + businessPascal);

        var databases = await _schemaProvider.GetDatabasesAsync().ConfigureAwait(false);
        var dbInfo = databases?.FirstOrDefault(d => string.Equals(d.ConfigId, configId, StringComparison.OrdinalIgnoreCase));
        var dataSource = dbInfo != null ? $"{dbInfo.DisplayName}:{dbInfo.ConfigId}" : $":{configId}";

        var table = new TaktGenTable
        {
            ConfigId = configId,
            DataSource = dataSource,
            TableName = tableName,
            TableComment = tableComment ?? tableOverrides?.TableComment,
            InDatabase = 0,
            GenTemplate = tableOverrides?.GenTemplate ?? "crud",
            EntityClassName = entityClassName,
            GenBusinessName = tableOverrides?.GenBusinessName ?? ToPascalCase(genBusinessNameRaw),
            GenFunctionName = tableOverrides?.GenFunctionName,
            GenAuthor = tableOverrides?.GenAuthor ?? "Takt365",
            GenMethod = tableOverrides?.GenMethod ?? 0,
            GenPath = tableOverrides?.GenPath ?? "/",
            PermsPrefix = tableOverrides?.PermsPrefix ?? string.Empty,
            SortType = tableOverrides?.SortType ?? "asc",
            SortField = tableOverrides?.SortField ?? string.Empty,
            NamePrefix = tableOverrides?.NamePrefix ?? namePrefix,
            GenModuleName = tableOverrides?.GenModuleName ?? genModuleName,
            EntityNamespace = tableOverrides?.EntityNamespace ?? BuildNamespace(namePrefix, genModuleName, "Domain.Entities"),
            DtoNamespace = tableOverrides?.DtoNamespace ?? BuildNamespace(namePrefix, genModuleName, "Application.Dtos"),
            DtoClassName = tableOverrides?.DtoClassName ?? entityClassName + "Dto",
            ServiceNamespace = tableOverrides?.ServiceNamespace ?? BuildNamespace(namePrefix, genModuleName, "Application.Services"),
            IServiceClassName = tableOverrides?.IServiceClassName ?? "I" + entityClassName + "Service",
            ServiceClassName = tableOverrides?.ServiceClassName ?? entityClassName + "Service",
            ControllerNamespace = tableOverrides?.ControllerNamespace ?? BuildNamespace(namePrefix, genModuleName, "WebApi.Controllers"),
            ControllerClassName = tableOverrides?.ControllerClassName ?? TaktGenTableTemplateModel.ToPluralControllerName(entityClassName),
            RepositoryInterfaceNamespace = tableOverrides?.RepositoryInterfaceNamespace ?? "Takt.Domain.Repositories",
            IRepositoryClassName = tableOverrides?.IRepositoryClassName ?? "I" + entityClassName + "Repository",
            RepositoryNamespace = tableOverrides?.RepositoryNamespace ?? BuildNamespace(namePrefix, genModuleName, "Infrastructure.Repositories"),
            RepositoryClassName = tableOverrides?.RepositoryClassName ?? entityClassName + "Repository"
        };
        if (tableOverrides != null)
        {
            if (tableOverrides.SubTableName != null) table.SubTableName = tableOverrides.SubTableName;
            if (tableOverrides.SubTableFkName != null) table.SubTableFkName = tableOverrides.SubTableFkName;
            if (tableOverrides.TreeCode != null) table.TreeCode = tableOverrides.TreeCode;
            if (tableOverrides.TreeParentCode != null) table.TreeParentCode = tableOverrides.TreeParentCode;
            if (tableOverrides.TreeName != null) table.TreeName = tableOverrides.TreeName;
            if (tableOverrides.GenFunctionName != null) table.GenFunctionName = tableOverrides.GenFunctionName;
            if (tableOverrides.GenFunction != null) table.GenFunction = tableOverrides.GenFunction;
            table.IsRepository = tableOverrides.IsRepository;
            table.ParentMenuId = tableOverrides.ParentMenuId;
            table.IsGenMenu = tableOverrides.IsGenMenu;
            table.IsGenTranslation = tableOverrides.IsGenTranslation;
            table.FrontTemplate = tableOverrides.FrontTemplate;
            table.FrontStyle = tableOverrides.FrontStyle;
            table.BtnStyle = tableOverrides.BtnStyle;
            table.IsGenCode = tableOverrides.IsGenCode;
            table.IsUseTabs = tableOverrides.IsUseTabs;
            table.TabsFieldCount = tableOverrides.TabsFieldCount;
            if (tableOverrides.Options != null) table.Options = tableOverrides.Options;
        }

        table = await _genTableRepository.CreateAsync(table).ConfigureAwait(false);
        var tableId = table.Id;

        int orderNum = 0;
        foreach (var col in columns)
        {
            var dbColName = col.DbColumnName ?? string.Empty;
            if (IsTaktEntityBaseColumn(dbColName))
                continue;
            var csharpColName = ToPascalCase(dbColName);
            var dbType = col.DataType ?? "nvarchar";
            var csharpType = MapDbTypeToCsharp(dbType);
            var colEntity = new TaktGenTableColumn
            {
                TableId = tableId,
                ConfigId = configId,
                DatabaseColumnName = dbColName,
                ColumnComment = col.ColumnDescription,
                DatabaseDataType = dbType,
                CsharpDataType = csharpType,
                CsharpColumnName = csharpColName,
                Length = col.Length,
                DecimalDigits = col.DecimalDigits,
                IsPk = col.IsPrimarykey ? 0 : 1,
                IsIncrement = col.IsIdentity ? 0 : 1,
                IsRequired = col.IsNullable ? 1 : 0,
                IsCreate = 0,
                IsUpdate = 0,
                IsList = 0,
                IsExport = 1,
                IsSort = 1,
                IsQuery = 1,
                QueryType = "LIKE",
                HtmlType = "input",
                OrderNum = orderNum++
            };
            await _genTableColumnRepository.CreateAsync(colEntity).ConfigureAwait(false);
        }

        TaktLogger.Information("[CodeGenWorkflow] 导入表完成: TableName={TableName}, TableId={TableId}, 列数={ColumnCount}", tableName, tableId, orderNum);
        return table.Adapt<TaktGenTableDto>();
    }

    /// <summary>
    /// 按实体类型初始化数据表（无表流程：代码生成后，手动指定实体类型全名）。
    /// </summary>
    /// <remarks>无表流程：代码生成后，手动指定实体类型全名，在指定库中按实体初始化表（与 TaktTableInitializer 一致：CodeFirst.InitTables）。</remarks>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="entityTypeFullName">实体类型全名（如 Takt.Domain.Entities.Generator.TaktGenTable）</param>
    /// <returns>任务</returns>
    public async Task InitializeTableFromEntityTypeAsync(string configId, string entityTypeFullName)
    {
        TaktLogger.Information("[CodeGenWorkflow] 按实体初始化数据表: ConfigId={ConfigId}, EntityType={EntityType}", configId, entityTypeFullName);
        await _schemaProvider.InitializeTableFromEntityTypeAsync(configId, entityTypeFullName).ConfigureAwait(false);
        TaktLogger.Information("[CodeGenWorkflow] 按实体初始化数据表完成: {EntityType}", entityTypeFullName);
    }

    /// <summary>
    /// 获取可用于按实体初始化表的实体类型全名列表（当前已加载的 TaktEntityBase 子类）。
    /// </summary>
    /// <returns>实体类型全名列表（当前已加载的 TaktEntityBase 子类）</returns>
    public async Task<IReadOnlyList<string>> GetAvailableEntityTypeFullNamesAsync()
    {
        return await _schemaProvider.GetAvailableEntityTypeFullNamesAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// 根据表配置与模板生成后端/前端代码（有表流程第 3 步、无表流程第 2 步）。
    /// </summary>
    /// <remarks>有表流程第 3 步、无表流程第 2 步：统一使用通用代码生成引擎（ITaktCodeEngine + TaktGenTemplateContext）根据模板生成后端/前端代码。</remarks>
    /// <param name="tableId">代码生成表配置 ID</param>
    /// <param name="templates">模板键（如 Entity.cs）→ Scriban 模板内容</param>
    /// <param name="sqlCreateBy">SQL 创建人（可选，用于菜单/翻译脚本中的 CreateBy 字段，默认使用 GenAuthor）</param>
    /// <returns>生成结果：文件名与内容列表</returns>
    public async Task<List<TaktCodeGenResultDto>> GenerateCodeAsync(long tableId, IReadOnlyDictionary<string, string> templates, string? sqlCreateBy = null)
    {
        if (templates == null || templates.Count == 0)
            return new List<TaktCodeGenResultDto>();

        TaktLogger.Information("[CodeGenWorkflow] 开始生成代码: TableId={TableId}, 模板数={TemplateCount}", tableId, templates.Count);
        var table = await _genTableRepository.GetByIdAsync(tableId).ConfigureAwait(false);
        if (table == null)
        {
            TaktLogger.Warning("[CodeGenWorkflow] 未找到表配置: TableId={TableId}", tableId);
            throw new InvalidOperationException($"未找到表配置 TableId={tableId}。");
        }

        var columns = await _genTableColumnRepository.FindAsync(c => c.TableId == tableId).ConfigureAwait(false);
        var context = TaktGenTemplateContext.From(table, columns);
        context.Table.SqlCreateBy = !string.IsNullOrWhiteSpace(sqlCreateBy) ? sqlCreateBy.Trim() : (table.GenAuthor ?? "admin");

        // 菜单/翻译 SQL 的雪花 ID：直接调用 SnowFlakeSingle.Instance.NextId()（见 SqlSugar 文档 1.3 手动调用雪花ID）
        if (context.Table.IsGenMenu == 0)
            context.Table.SqlMenuId = SnowFlakeSingle.Instance.NextId();
        if (context.Table.IsGenTranslation == 0)
            context.Table.SqlTranslationRows = BuildSqlTranslationRows(context);

        TaktLogger.Information("[CodeGenWorkflow] 表配置已加载: TableName={TableName}, 列数={ColumnCount}", table.TableName, columns.Count);

        var results = new List<TaktCodeGenResultDto>(templates.Count);
        foreach (var kv in templates)
        {
            var fileName = kv.Key;
            // InDatabase=0 表示是库表，实体已存在，不生成实体文件，避免重复
            if (table.InDatabase == 0 && IsBackendEntityTemplateKey(fileName))
            {
                TaktLogger.Information("[CodeGenWorkflow] 跳过实体模板（InDatabase=0 库表）: {FileName}", fileName);
                continue;
            }
            TaktLogger.Information("[CodeGenWorkflow] 渲染模板: {FileName}", fileName);
            var content = await _codeEngine.RenderAsync(kv.Value, context, fileName).ConfigureAwait(false);
            results.Add(new TaktCodeGenResultDto { FileName = fileName, Content = content });
        }
        TaktLogger.Information("[CodeGenWorkflow] 代码生成完成: TableId={TableId}, 生成文件数={ResultCount}", tableId, results.Count);
        return results;
    }

    /// <summary>按 menu_and_translation.sql 模板顺序构建翻译行（每行 Id 用 SnowFlakeSingle.Instance.NextId() 生成）。翻译键与种子一致：菜单标题用 menu.xxx（同 menu_l10n_key），字段用 xxx.entities.fieldname（全小写）。</summary>
    private static List<TaktSqlTranslationRowItem> BuildSqlTranslationRows(TaktGenTemplateContext context)
    {
        var rows = new List<TaktSqlTranslationRowItem>();
        var cultureCodes = new[] { "ar-SA", "en-US", "es-ES", "fr-FR", "ja-JP", "ko-KR", "ru-RU", "zh-CN", "zh-TW" };
        var t = context.Table;
        var moduleDot = (t.GenModuleName ?? "").ToLowerInvariant().Replace("_", ".");
        var entityLower = (t.EntityNameCamel ?? "").ToLowerInvariant();
        var menuL10nKey = "menu." + moduleDot + "." + entityLower;
        var pageKeyPrefix = string.IsNullOrEmpty(moduleDot) ? entityLower : (moduleDot + "." + entityLower);
        var comment = t.Comment ?? "";

        foreach (var culture in cultureCodes)
        {
            var titleValue = culture == "zh-CN" ? comment + "管理" : comment;
            rows.Add(new TaktSqlTranslationRowItem
            {
                Id = SnowFlakeSingle.Instance.NextId(),
                Culture = culture,
                ResourceKey = menuL10nKey,
                TranslationValue = titleValue,
                ResourceGroup = "menu",
                OrderNum = 0
            });
        }
        foreach (var col in context.Columns.Where(c => c.IsList == 0))
        {
            var colKey = pageKeyPrefix + ".entities." + (col.TsColumnName ?? "").ToLowerInvariant();
            foreach (var culture in cultureCodes)
            {
                rows.Add(new TaktSqlTranslationRowItem
                {
                    Id = SnowFlakeSingle.Instance.NextId(),
                    Culture = culture,
                    ResourceKey = colKey,
                    TranslationValue = col.Comment ?? "",
                    ResourceGroup = "page",
                    OrderNum = col.OrderNum
                });
            }
        }
        foreach (var col in context.Columns.Where(c => c.IsQuery == 0 && c.IsList != 0))
        {
            var colKey = pageKeyPrefix + ".entities." + (col.TsColumnName ?? "").ToLowerInvariant();
            foreach (var culture in cultureCodes)
            {
                rows.Add(new TaktSqlTranslationRowItem
                {
                    Id = SnowFlakeSingle.Instance.NextId(),
                    Culture = culture,
                    ResourceKey = colKey,
                    TranslationValue = col.Comment ?? "",
                    ResourceGroup = "page",
                    OrderNum = col.OrderNum
                });
            }
        }
        return rows;
    }

    /// <summary>判断模板键是否为后端实体模板（Backend/Crud/Csharp/Entity.cs）。</summary>
    private static bool IsBackendEntityTemplateKey(string? templateKey)
    {
        if (string.IsNullOrWhiteSpace(templateKey)) return false;
        var key = templateKey.Replace('\\', '/').Trim();
        return key.EndsWith("Backend/Crud/Csharp/Entity.cs", StringComparison.OrdinalIgnoreCase)
            || key.Equals("Entity.cs", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>将 snake_case 模块名转为命名空间后缀（如 accounting_financial → Accounting.Financial）。</summary>
    private static string ToNamespaceSuffix(string? moduleSnakeCase)
    {
        if (string.IsNullOrWhiteSpace(moduleSnakeCase)) return string.Empty;
        var parts = moduleSnakeCase.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join(".", parts.Select(p => p.Length > 0 ? char.ToUpperInvariant(p[0]) + p.Substring(1).ToLowerInvariant() : p));
    }

    /// <summary>拼接命名空间：前缀.中间段.模块后缀，如 Takt + accounting_financial + Domain.Entities → Takt.Domain.Entities.Accounting.Financial。</summary>
    private static string BuildNamespace(string namePrefix, string genModuleName, string middleSegment)
    {
        var suffix = ToNamespaceSuffix(genModuleName);
        var prefix = string.IsNullOrEmpty(namePrefix) ? "Takt" : namePrefix;
        return string.IsNullOrEmpty(suffix) ? $"{prefix}.{middleSegment}" : $"{prefix}.{middleSegment}.{suffix}";
    }

    /// <summary>将下划线/空格/横线命名字符串转为帕斯卡命名（如 user_name → UserName）。</summary>
    /// <param name="snakeCase">下划线、空格或横线分隔的字符串</param>
    /// <returns>帕斯卡命名字符串</returns>
    private static string ToPascalCase(string snakeCase)
    {
        if (string.IsNullOrWhiteSpace(snakeCase)) return snakeCase;
        var parts = snakeCase.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(parts.Select(p => p.Length > 0 ? char.ToUpperInvariant(p[0]) + p.Substring(1).ToLowerInvariant() : p));
    }

    /// <summary>
    /// 根据数据表名拆分为：命名空间前缀、模块名称、业务名。
    /// 约定：表名按下划线分段，第一段为前缀，最后一段为业务名，中间为模块名；模块名需包含“一级/二级”路径以便与菜单一致（如 日常事务/公告通知→routine_announcement，人力资源/人事管理→humanresource_personnel）。
    /// 例：takt_routine_announcement → 前缀=takt，模块=routine_announcement（Routine/Announcement），业务=announcement；
    /// takt_humanresource_personnel_employee → 前缀=takt，模块=humanresource_personnel（人力资源/人事管理），业务=employee；
    /// takt_accounting_financial_company → 前缀=takt，模块=accounting_financial，业务=company。
    /// </summary>
    /// <param name="tableName">数据表名（下划线分隔）</param>
    /// <returns>(NamePrefix 帕斯卡, GenModuleName 下划线, GenBusinessName 原始 snake 段)</returns>
    private static (string NamePrefix, string GenModuleName, string GenBusinessNameRaw) ParseTableNameParts(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            return ("Takt", string.Empty, string.Empty);
        var parts = tableName.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return ("Takt", string.Empty, string.Empty);
        var first = parts[0];
        var namePrefix = first.Length > 0 ? char.ToUpperInvariant(first[0]) + first.Substring(1).ToLowerInvariant() : "Takt";
        if (parts.Length == 1)
            return (namePrefix, string.Empty, first.ToLowerInvariant());
        if (parts.Length == 2)
            return (namePrefix, parts[1].ToLowerInvariant(), parts[1].ToLowerInvariant());
        // 3 段（prefix + 一级模块 + 业务）：模块名取“一级_业务”，与菜单路径一致（如 routine_announcement → 日常事务/公告通知，业务名=Announcement）
        if (parts.Length == 3)
        {
            return (namePrefix, parts[1].ToLowerInvariant() + "_" + parts[2].ToLowerInvariant(), parts[2].ToLowerInvariant());
        }
        // 业务名：最后一段若为“复合业务”常见后缀（rate/time/date/code/name/type 等），取最后 3 段；否则取最后 1 段。至少保留 1 段给模块。
        var lastSegment = parts[^1].ToLowerInvariant();
        var multiSegmentSuffixes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "rate", "rates", "time", "date", "code", "name", "type", "control" };
        var businessSegmentCount = 1;
        if (parts.Length >= 5 && multiSegmentSuffixes.Contains(lastSegment))
            businessSegmentCount = 3;
        var moduleCount = parts.Length - 1 - businessSegmentCount;
        if (moduleCount < 1)
        {
            businessSegmentCount = 1;
            moduleCount = parts.Length - 2;
        }
        var genModuleName = moduleCount > 0
            ? string.Join("_", parts.Skip(1).Take(moduleCount).Select(p => p.ToLowerInvariant()))
            : string.Empty;
        var genBusinessNameRaw = string.Join("_", parts.Skip(1 + moduleCount).Take(businessSegmentCount).Select(p => p.ToLowerInvariant()));
        return (namePrefix, genModuleName, genBusinessNameRaw);
    }

    /// <summary>TaktEntityBase 实体基类在数据库中的列名（与 TaktEntityBase.cs 中 SugarColumn.ColumnName 一致），导入时排除这些列。</summary>
    private static readonly HashSet<string> TaktEntityBaseColumnNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "id", "config_id", "ext_field_json", "remark", "create_by", "create_time",
        "update_by", "update_time", "is_deleted", "deleted_by", "deleted_time"
    };

    /// <summary>判断数据库列名是否属于 TaktEntityBase 基类字段，若是则导入时排除。</summary>
    private static bool IsTaktEntityBaseColumn(string? dbColumnName)
    {
        return !string.IsNullOrWhiteSpace(dbColumnName) && TaktEntityBaseColumnNames.Contains(dbColumnName.Trim());
    }

    /// <summary>将数据库数据类型映射为 C# 类型（如 nvarchar→string、bigint→long、bit→bool）。</summary>
    /// <param name="dbType">数据库类型名（如 nvarchar、bigint、datetime）</param>
    /// <returns>C# 类型名（如 string、long、DateTime）</returns>
    private static string MapDbTypeToCsharp(string dbType)
    {
        if (string.IsNullOrWhiteSpace(dbType)) return "string";
        var t = dbType.ToLowerInvariant();
        if (t.Contains("int") && !t.Contains("bigint")) return "int";
        if (t.Contains("bigint")) return "long";
        if (t.Contains("decimal") || t.Contains("numeric")) return "decimal";
        if (t.Contains("float") || t.Contains("double")) return "double";
        if (t.Contains("date") || t.Contains("time")) return "DateTime";
        if (t.Contains("bit") || t.Contains("bool")) return "bool";
        if (t.Contains("uniqueidentifier") || t.Contains("guid")) return "Guid";
        return "string";
    }
}
