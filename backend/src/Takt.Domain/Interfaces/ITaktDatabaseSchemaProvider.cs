// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktDatabaseSchemaProvider.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：数据库元数据提供者接口：获取所有数据库（ConfigId 列表）、根据选中的数据库获取数据表列表，供 Application/Infrastructure 等层调用
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Interfaces;

/// <summary>
/// 数据库元数据提供者接口：获取所有数据库配置、根据 ConfigId 获取该库下的数据表/列，执行 DDL。
/// </summary>
public interface ITaktDatabaseSchemaProvider
{
    /// <summary>
    /// 获取所有数据库配置列表（对应 appsettings 中 dbConfigs 的每一项，用于“选中数据库”）
    /// </summary>
    /// <returns>数据库信息列表（ConfigId、显示名等）</returns>
    Task<IReadOnlyList<TaktDatabaseInfo>> GetDatabasesAsync();

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下的所有数据表列表。
    /// </summary>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <returns>该库下的表名与表描述列表</returns>
    Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesAsync(string configId);

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下“包含指定列名”的数据表列表（列名不区分大小写）。
    /// 用于流程表单等场景，仅展示带 flow_instance_id 等列的表。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="requiredColumnName">必须存在的列名（如 flow_instance_id）</param>
    /// <returns>满足条件的表名与表描述列表</returns>
    Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesWithRequiredColumnAsync(string configId, string requiredColumnName);

    /// <summary>
    /// 根据选中的数据库（ConfigId）获取该库下按“包含某列 / 不包含某列”过滤的数据表列表（列名不区分大小写）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="requiredColumnName">可选。必须存在的列名（如 flow_instance_id），不传则不按包含过滤</param>
    /// <param name="excludedColumnName">可选。必须不存在的列名，不传则不按排除过滤</param>
    /// <returns>满足条件的表名与表描述列表</returns>
    Task<IReadOnlyList<TaktDatabaseTableInfo>> GetTablesFilteredAsync(string configId, string? requiredColumnName, string? excludedColumnName);

    /// <summary>
    /// 获取指定表的列元数据（用于“有表”流程：从数据库导入表结构）。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <param name="includeAuditColumns">是否包含可选审计列（id、config_id、created_by、updated_at、is_deleted、flow_instance_id 等）；默认 true。ext_field_json、remark 始终返回</param>
    /// <returns>列信息列表</returns>
    Task<IReadOnlyList<TaktDatabaseColumnInfo>> GetColumnsAsync(string configId, string tableName, bool includeAuditColumns = true);

    /// <summary>
    /// 获取指定表的表注释/描述
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <returns>表注释，无则返回 null</returns>
    Task<string?> GetTableCommentAsync(string configId, string tableName);

    /// <summary>
    /// 判断指定表是否已存在
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">表名</param>
    /// <returns>存在返回 true</returns>
    Task<bool> TableExistsAsync(string configId, string tableName);

    /// <summary>
    /// 执行 DDL（如 CREATE TABLE）
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="sql">DDL 语句</param>
    Task ExecuteDdlAsync(string configId, string sql);

    /// <summary>
    /// 按实体类型初始化数据表（与项目内 TaktTableInitializer 一致：SqlSugar CodeFirst.InitTables）。
    /// 用于无表流程：代码生成后，用户手动指定实体类型全名，在指定库中创建该实体对应的表。
    /// </summary>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <param name="entityTypeFullName">实体类型全名（如 Takt.Domain.Entities.Generator.TaktGenTable）</param>
    /// <returns>任务</returns>
    Task InitializeTableFromEntityTypeAsync(string configId, string entityTypeFullName);

    /// <summary>
    /// 获取当前已加载、可用于“按实体初始化表”的实体类型全名列表（继承 TaktEntityBase 的 Takt.* 程序集类型）。
    /// 供无表流程中“手动选择实体”使用。
    /// </summary>
    /// <returns>实体类型全名列表（如 Takt.Domain.Entities.Generator.TaktGenTable）</returns>
    Task<IReadOnlyList<string>> GetAvailableEntityTypeFullNamesAsync();

    /// <summary>
    /// 根据数据库名（RelatedDataBaseName，从 appsettings dbConfigs 的 Conn 解析）解析为 ConfigId。
    /// 用于流程表单等：存储的是数据库名（如 Takt_Identity_Dev），按当前环境配置解析为 ConfigId 再调 GetTables/GetColumns。
    /// </summary>
    /// <param name="dataBaseName">数据库名（如 Takt_Identity_Dev）</param>
    /// <returns>对应的 ConfigId，未找到返回 null</returns>
    string? ResolveDataBaseNameToConfigId(string dataBaseName);
}

/// <summary>
/// 数据库信息（用于“获取所有数据库”的单项）
/// </summary>
public class TaktDatabaseInfo
{
    /// <summary>数据库配置 ID（与 dbConfigs 中 ConfigId 一致）</summary>
    public string ConfigId { get; set; } = string.Empty;

    /// <summary>显示名称（如“主库”“业务库”，可由配置或默认 ConfigId）</summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>数据库名（从 Conn 的 Database= 解析，用于流程表单 RelatedDataBaseName，开发/生产环境不同）</summary>
    public string DataBaseName { get; set; } = string.Empty;
}

/// <summary>
/// 数据表信息（用于“根据选中的数据库获取数据表”的单项）
/// </summary>
public class TaktDatabaseTableInfo
{
    /// <summary>表名</summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>表描述/注释</summary>
    public string? TableComment { get; set; }
}

/// <summary>
/// 数据库列信息（用于从物理表导入列元数据）
/// </summary>
public class TaktDatabaseColumnInfo
{
    /// <summary>数据库列名</summary>
    public string DbColumnName { get; set; } = string.Empty;

    /// <summary>列描述/注释</summary>
    public string? ColumnDescription { get; set; }

    /// <summary>数据库数据类型（如 nvarchar、int）</summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>长度（如 nvarchar 的 200）</summary>
    public int Length { get; set; }

    /// <summary>小数位数</summary>
    public int DecimalDigits { get; set; }

    /// <summary>是否主键</summary>
    public bool IsPrimarykey { get; set; }

    /// <summary>是否自增</summary>
    public bool IsIdentity { get; set; }

    /// <summary>是否可空</summary>
    public bool IsNullable { get; set; }
}
