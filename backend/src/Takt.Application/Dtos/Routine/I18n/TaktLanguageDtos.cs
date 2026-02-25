// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.I18n
// 文件名称：TaktLanguageDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言DTO，包含语言相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.I18n;

/// <summary>
/// Takt语言DTO
/// </summary>
public class TaktLanguageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 语言ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    public string LanguageName { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    public string NativeName { get; set; }

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    public string? LanguageIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    public int LanguageStatus { get; set; }

    /// <summary>
    /// 是否默认语言（0=是，1=否）
    /// </summary>
    public int IsDefault { get; set; }

    /// <summary>
    /// 是否启用RTL（从右到左，0=是，1=否）
    /// </summary>
    public int IsRtl { get; set; }

    /// <summary>
    /// 翻译列表（主子表关系）
    /// </summary>
    public List<TaktTranslationDto>? TranslationList { get; set; }

    // ----- 审计字段（与 TaktEntityBase 一致，统一放在最后） -----

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt语言查询DTO
/// </summary>
public class TaktLanguageQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在语言名称、语言编码、本地化名称中模糊查询

    /// <summary>
    /// 语言编码
    /// </summary>
    public string? CultureCode { get; set; }

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    public int? LanguageStatus { get; set; }

    /// <summary>
    /// 是否默认语言（0=是，1=否）
    /// </summary>
    public int? IsDefault { get; set; }
}

/// <summary>
/// Takt创建语言DTO
/// </summary>
public class TaktLanguageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageCreateDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
    }

    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    public string LanguageName { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    public string NativeName { get; set; }

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    public string? LanguageIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    public int LanguageStatus { get; set; } = 0;

    /// <summary>
    /// 是否默认语言（0=是，1=否）
    /// </summary>
    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 是否启用RTL（从右到左，0=是，1=否）
    /// </summary>
    public int IsRtl { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 翻译列表（主子表关系）
    /// </summary>
    public List<TaktTranslationCreateDto>? TranslationList { get; set; }
}

/// <summary>
/// Takt更新语言DTO
/// </summary>
public class TaktLanguageUpdateDto : TaktLanguageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageUpdateDto()
    {
    }

    /// <summary>
    /// 语言ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }
}

/// <summary>
/// Takt语言状态DTO
/// </summary>
public class TaktLanguageStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageStatusDto()
    {
    }

    /// <summary>
    /// 语言ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言状态（0=禁用，1=启用）
    /// </summary>
    public byte LanguageStatus { get; set; }
}

/// <summary>
/// Takt语言导入模板DTO
/// </summary>
public class TaktLanguageTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageTemplateDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
    }

    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    public string LanguageName { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    public string NativeName { get; set; }

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    public string? LanguageIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    public int LanguageStatus { get; set; } = 0;

    /// <summary>
    /// 是否默认语言（0=是，1=否）
    /// </summary>
    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 是否启用RTL（从右到左，0=是，1=否）
    /// </summary>
    public int IsRtl { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt语言导入DTO
/// </summary>
public class TaktLanguageImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageImportDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
    }

    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    public string LanguageName { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    public string NativeName { get; set; }

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    public string? LanguageIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    public int LanguageStatus { get; set; } = 0;

    /// <summary>
    /// 是否默认语言（0=是，1=否）
    /// </summary>
    public int IsDefault { get; set; } = 0;

    /// <summary>
    /// 是否启用RTL（从右到左，0=是，1=否）
    /// </summary>
    public int IsRtl { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt语言导出DTO
/// </summary>
public class TaktLanguageExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageExportDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
        LanguageIcon = string.Empty;
        LanguageStatus = string.Empty;
        IsDefault = string.Empty;
        IsRtl = string.Empty;
        CreateTime = DateTime.Now;
    }

    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    public string LanguageName { get; set; }

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    public string CultureCode { get; set; }

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    public string NativeName { get; set; }

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    public string LanguageIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 语言状态
    /// </summary>
    public string LanguageStatus { get; set; }

    /// <summary>
    /// 是否默认语言
    /// </summary>
    public string IsDefault { get; set; }

    /// <summary>
    /// 是否启用RTL
    /// </summary>
    public string IsRtl { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
