// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Generator
// 文件名称：TaktCodeGenWorkflowDtos.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成工作流 DTO（数据库表列表、生成结果等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Code.Generator;

/// <summary>
/// 数据库表信息（按 ConfigId 获取的物理表列表项）
/// </summary>
public class TaktDatabaseTableInfoDto
{
    /// <summary>表名</summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>表描述/注释</summary>
    public string? TableComment { get; set; }
}

/// <summary>
/// 代码生成结果：文件名（或相对路径） -> 生成后的内容
/// </summary>
public class TaktCodeGenResultDto
{
    /// <summary>生成的文件名或相对路径（如 Entity.cs、Dto.cs）</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>生成后的代码/文本内容</summary>
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// 从数据库导入表请求：ConfigId、表名，可选表配置覆盖
/// </summary>
public class TaktImportTableRequestDto
{
    /// <summary>数据库配置 ID</summary>
    public string ConfigId { get; set; } = string.Empty;

    /// <summary>要导入的数据表名</summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>表配置覆盖（可选，用于补充实体类名、业务名等）</summary>
    public TaktGenTableCreateDto? TableOverrides { get; set; }
}
