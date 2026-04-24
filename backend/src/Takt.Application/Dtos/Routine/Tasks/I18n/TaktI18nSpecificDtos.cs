// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.I18n
// 文件名称：TaktI18nSpecific.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：特殊DTO集合，包含业务特定的数据传输对象（如头像更新、密码重置、用户解锁等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.I18n;

public class TaktTranslationTransposedDto
{
    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; } = string.Empty;

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; } = string.Empty;

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 各语言翻译值，key 为 CultureCode（如 zh-CN、en-US），value 为 TranslationValue；键顺序与 CultureCodeOrder 一致，缺失语言为空串
    /// </summary>
    public Dictionary<string, string> Translations { get; set; } = new();
}

public class TaktTranslationTransposedResult
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public TaktPagedResult<TaktTranslationTransposedDto> Paged { get; set; } = null!;

    /// <summary>
    /// 语言列顺序（表头从左到右），如 ar-SA、en-US、zh-CN、zh-TW 等
    /// </summary>
    public IReadOnlyList<string> CultureCodeOrder { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Takt语言创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktLanguageCreateDto
{
    /// <summary>
    /// 翻译列表（非数据库字段）
    /// </summary>
    public List<TaktTranslationDto>? TranslationList { get; set; }
}

/// <summary>
/// Takt语言更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktLanguageUpdateDto
{
    // TranslationList 已从 TaktLanguageCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt语言DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktLanguageDto
{
    /// <summary>
    /// 翻译列表（非数据库字段）
    /// </summary>
    public List<TaktTranslationDto>? TranslationList { get; set; }
}

/// <summary>
/// Takt语言导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktLanguageExportDto
{
    /// <summary>
    /// 语言状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? LanguageStatusString { get; set; }
    
    /// <summary>
    /// 是否默认字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? IsDefaultString { get; set; }
    
    /// <summary>
    /// 是否RTL字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? IsRtlString { get; set; }
}