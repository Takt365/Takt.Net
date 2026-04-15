// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.I18n
// 文件名称：TaktTranslationDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt翻译DTO，包含翻译相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.I18n;

/// <summary>
/// Takt翻译DTO
/// </summary>
public class TaktTranslationDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationDto()
    {
        ResourceKey = string.Empty;
        CultureCode = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 翻译ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TranslationId { get; set; }

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; }

    /// <summary>
    /// 语言ID（外键，关联 TaktLanguage.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    public string TranslationValue { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; }

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// Takt翻译查询DTO
/// </summary>
public class TaktTranslationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在资源键、翻译值中模糊查询

    /// <summary>
    /// 语言ID（外键，关联语言）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? LanguageId { get; set; }

    /// <summary>
    /// 资源键
    /// </summary>
    public string? ResourceKey { get; set; }

    /// <summary>
    /// 语言编码
    /// </summary>
    public string? CultureCode { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string? ResourceType { get; set; }

    /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }
}

/// <summary>
/// Takt创建翻译DTO
/// </summary>
public class TaktTranslationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationCreateDto()
    {
        ResourceKey = string.Empty;
        CultureCode = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = "Backend";
    }

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; }

    /// <summary>
    /// 语言ID（外键，关联 TaktLanguage.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    public string TranslationValue { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; }

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新翻译DTO
/// </summary>
public class TaktTranslationUpdateDto : TaktTranslationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationUpdateDto()
    {
    }

    /// <summary>
    /// 翻译ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TranslationId { get; set; }
}

/// <summary>
/// Takt翻译导入模板DTO
/// </summary>
public class TaktTranslationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationTemplateDto()
    {
        ResourceKey = string.Empty;
        CultureCode = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = "Backend";
    }

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; }

    /// <summary>
    /// 语言ID（外键，关联 TaktLanguage.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    public string TranslationValue { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; }

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt翻译导入DTO
/// </summary>
public class TaktTranslationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationImportDto()
    {
        ResourceKey = string.Empty;
        CultureCode = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = "Backend";
    }

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; }

    /// <summary>
    /// 语言ID（外键，关联 TaktLanguage.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    public string TranslationValue { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; }

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt翻译导出DTO
/// </summary>
public class TaktTranslationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationExportDto()
    {
        ResourceKey = string.Empty;
        CultureCode = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
        ResourceGroup = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    public string ResourceKey { get; set; }

    /// <summary>
    /// 语言ID（外键，关联 TaktLanguage.Id）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    public string TranslationValue { get; set; }

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    public string ResourceType { get; set; }

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    public string ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Takt翻译转置DTO（按资源键分组，各语言为列）
/// </summary>
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
    public int OrderNum { get; set; }

    /// <summary>
    /// 各语言翻译值，key 为 CultureCode（如 zh-CN、en-US），value 为 TranslationValue；键顺序与 CultureCodeOrder 一致，缺失语言为空串
    /// </summary>
    public Dictionary<string, string> Translations { get; set; } = new();
}

/// <summary>
/// Takt翻译转置接口结果（分页数据 + 语言列顺序，供表头与双行展示）
/// </summary>
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
