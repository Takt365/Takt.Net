// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktNamingHelper.cs
// 功能描述：通用命名约定：实体类名与 Takt 前缀、Excel 导入导出 sheet/file 默认值、entity.xxx._self 资源键推导等。
// ========================================

namespace Takt.Shared.Helpers;

/// <summary>
/// 通用命名辅助（如 Excel 模板/导入/导出：fileName 默认为领域实体类名；sheetName 默认为去掉 <c>Takt</c> 后的英文业务名；与种子 <c>entity.xxx._self</c> 规则对齐）。
/// </summary>
public static class TaktNamingHelper
{
    /// <summary>
    /// 去掉 <c>Takt</c> 前缀后的类型后缀（如 <c>TaktUser</c> → <c>User</c>），常用作默认工作表业务英文名。
    /// </summary>
    public static string DefaultSheetNameEnglish(string entityTypeName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entityTypeName);
        return entityTypeName.StartsWith("Takt", StringComparison.Ordinal) && entityTypeName.Length > 4
            ? entityTypeName[4..]
            : entityTypeName;
    }

    /// <summary>
    /// 与实体翻译种子一致的 <c>entity.xxx._self</c> 键（<c>xxx</c> 为 <see cref="DefaultSheetNameEnglish"/> 结果的全小写）。
    /// </summary>
    public static string EntitySelfResourceKey(string entityTypeName)
    {
        var tail = DefaultSheetNameEnglish(entityTypeName);
        return $"entity.{tail.ToLowerInvariant()}._self";
    }

    /// <summary>
    /// 与本地化 <c>entity.xxx._self</c> 拼接构成 Excel 导入模板默认文件基名的翻译键（各语种文案为后缀片段，如「导入模板」）。
    /// </summary>
    public const string EntityTemplateNameResourceKey = "entity.template.name";

    /// <summary>
    /// 解析 Excel 模板/导入/导出使用的工作表名与文件基名（不含扩展名；时间戳与扩展名由 Excel 帮助类拼接）。
    /// </summary>
    /// <param name="sheetName">调用方传入的工作表名；空则使用 <paramref name="defaultSheetNameEnglishOverride"/> 或实体默认英文名。</param>
    /// <param name="fileName">调用方传入的文件基名；空则使用 <paramref name="entityTypeName"/>。</param>
    /// <param name="entityTypeName">领域实体类名，如 <c>TaktUser</c>。</param>
    /// <param name="defaultSheetNameEnglishOverride">非标准 CRUD 时可指定默认工作表英文名（如 <c>FlowTodo</c>）。</param>
    public static (string Sheet, string FileBase) ResolveExcelImportExport(
        string? sheetName,
        string? fileName,
        string entityTypeName,
        string? defaultSheetNameEnglishOverride = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entityTypeName);
        var defaultSheet = string.IsNullOrWhiteSpace(defaultSheetNameEnglishOverride)
            ? DefaultSheetNameEnglish(entityTypeName)
            : defaultSheetNameEnglishOverride.Trim();
        var sheet = string.IsNullOrWhiteSpace(sheetName) ? defaultSheet : sheetName.Trim();
        var file = string.IsNullOrWhiteSpace(fileName) ? entityTypeName : fileName.Trim();
        return (sheet, file);
    }
}
