// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Code.Generator.CodeEngine
// 文件名称：ITaktCodeGenWorkflowService.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成工作流服务接口，支持有表/无表两条流程，均通过通用代码生成引擎生成后端/前端代码
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Code.Generator;

namespace Takt.Application.Services.Code.Generator.CodeEngine;

/// <summary>
/// 代码生成工作流服务接口
/// </summary>
/// <remarks>
/// 两大流程：
/// 1. 数据表存在：根据 ConfigId 获取所有数据表 → 选中表 → 导入为 TaktGenTable + TaktGenTableColumn → 根据模板生成后端、前端代码。
/// 2. 数据表不存在：创建 TaktGenTable + TaktGenTableColumn → 生成代码（得到实体文件）→ 手动指定实体类型，按实体初始化数据表（与项目 TaktTableInitializer 一致：CodeFirst.InitTables）。
/// </remarks>
public interface ITaktCodeGenWorkflowService
{
    /// <summary>
    /// 根据 ConfigId 获取该数据库下所有数据表列表（用于“数据表存在”流程：选表）
    /// </summary>
    /// <param name="configId">数据库配置 ID（对应 dbConfigs 中的 ConfigId）</param>
    /// <returns>表名与表描述列表</returns>
    Task<List<TaktDatabaseTableInfoDto>> GetDatabaseTablesAsync(string configId);

    /// <summary>
    /// 从数据库导入指定表：读取表及列元数据，写入 TaktGenTable、TaktGenTableColumn（用于“数据表存在”流程：导入）
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="tableName">要导入的数据表名</param>
    /// <param name="tableOverrides">表配置覆盖（可选，用于补充实体类名、业务名等）</param>
    /// <returns>导入后的表配置 DTO（含表 ID，可用于后续生成代码）</returns>
    Task<TaktGenTableDto> ImportTableFromDatabaseAsync(string configId, string tableName, TaktGenTableCreateDto? tableOverrides = null);

    /// <summary>
    /// 按实体类型初始化数据表（无表流程：代码生成后，手动指定实体类型全名）。与项目内 TaktTableInitializer 一致，使用 SqlSugar CodeFirst.InitTables。
    /// </summary>
    /// <param name="configId">数据库配置 ID</param>
    /// <param name="entityTypeFullName">实体类型全名（如 Takt.Domain.Entities.Code.Generator.TaktGenTable，对应生成的实体文件中的类）</param>
    /// <returns>任务</returns>
    Task InitializeTableFromEntityTypeAsync(string configId, string entityTypeFullName);

    /// <summary>
    /// 获取可用于“按实体初始化表”的实体类型全名列表（当前已加载的 TaktEntityBase 子类）。供无表流程中“手动选择实体”使用。
    /// </summary>
    /// <returns>实体类型全名列表</returns>
    Task<IReadOnlyList<string>> GetAvailableEntityTypeFullNamesAsync();

    /// <summary>
    /// 根据表配置与模板映射生成代码：使用 TaktGenTemplateContext + Scriban 渲染，返回文件名与内容（后端、前端）
    /// </summary>
    /// <param name="tableId">代码生成表配置 ID</param>
    /// <param name="templates">模板键（如 "Entity.cs"）→ Scriban 模板内容</param>
    /// <param name="sqlCreateBy">生成 SQL 时写入 create_by 的当前登录用户名（如 admin、user01），未传则用表配置 GenAuthor 或 "admin"</param>
    /// <returns>生成结果：文件名 → 生成后的内容</returns>
    Task<List<TaktCodeGenResultDto>> GenerateCodeAsync(long tableId, IReadOnlyDictionary<string, string> templates, string? sqlCreateBy = null);

    /// <summary>
    /// 根据表配置与模板映射渲染预览文件（目标相对路径 + 内容 + 是否已存在），仅用于模板正确性校验，不执行落盘生成。
    /// </summary>
    /// <param name="tableId">代码生成表配置 ID</param>
    /// <param name="templates">模板键（如 "Backend/Crud/Csharp/Entity.cs"）→ Scriban 模板内容</param>
    /// <param name="resolveTargetRelativePath">根据模板键解析目标相对路径的函数</param>
    /// <param name="targetBasePath">目标根路径（可空；为空时不检查是否已存在）</param>
    /// <param name="sqlCreateBy">生成 SQL 时写入 create_by 的当前登录用户名（可空）</param>
    /// <returns>预览渲染结果（成功文件 + 校验问题）</returns>
    Task<TaktCodeGenPreviewResultDto> GeneratePreviewFilesAsync(
        long tableId,
        IReadOnlyDictionary<string, string> templates,
        Func<TaktGenTableDto, string, string?> resolveTargetRelativePath,
        string? targetBasePath = null,
        string? sqlCreateBy = null);
}
