// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.I18n
// 文件名称：TaktTranslationDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：翻译表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.I18n;

/// <summary>
/// 翻译表Dto
/// </summary>
public partial class TaktTranslationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationDto()
    {
        CultureCode = string.Empty;
        ResourceKey = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
    }

    /// <summary>
    /// 翻译表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TranslationId { get; set; }

    /// <summary>
    /// 语言ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }
    /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }
    /// <summary>
    /// 资源键
    /// </summary>
    public string ResourceKey { get; set; }
    /// <summary>
    /// 翻译值
    /// </summary>
    public string TranslationValue { get; set; }
    /// <summary>
    /// 资源类型
    /// </summary>
    public string ResourceType { get; set; }
    /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 翻译表查询DTO
/// </summary>
public partial class TaktTranslationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 翻译表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TranslationId { get; set; }

    /// <summary>
    /// 语言ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? LanguageId { get; set; }
    /// <summary>
    /// 语言编码
    /// </summary>
    public string? CultureCode { get; set; }
    /// <summary>
    /// 资源键
    /// </summary>
    public string? ResourceKey { get; set; }
    /// <summary>
    /// 翻译值
    /// </summary>
    public string? TranslationValue { get; set; }
    /// <summary>
    /// 资源类型
    /// </summary>
    public string? ResourceType { get; set; }
    /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建翻译表DTO
/// </summary>
public partial class TaktTranslationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationCreateDto()
    {
        CultureCode = string.Empty;
        ResourceKey = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
    }

        /// <summary>
    /// 语言ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 资源键
    /// </summary>
    public string ResourceKey { get; set; }

        /// <summary>
    /// 翻译值
    /// </summary>
    public string TranslationValue { get; set; }

        /// <summary>
    /// 资源类型
    /// </summary>
    public string ResourceType { get; set; }

        /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新翻译表DTO
/// </summary>
public partial class TaktTranslationUpdateDto : TaktTranslationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationUpdateDto()
    {
    }

        /// <summary>
    /// 翻译表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TranslationId { get; set; }
}

/// <summary>
/// 翻译表导入模板DTO
/// </summary>
public partial class TaktTranslationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationTemplateDto()
    {
        CultureCode = string.Empty;
        ResourceKey = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
    }

        /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 资源键
    /// </summary>
    public string ResourceKey { get; set; }

        /// <summary>
    /// 翻译值
    /// </summary>
    public string TranslationValue { get; set; }

        /// <summary>
    /// 资源类型
    /// </summary>
    public string ResourceType { get; set; }

        /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 翻译表导入DTO
/// </summary>
public partial class TaktTranslationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationImportDto()
    {
        CultureCode = string.Empty;
        ResourceKey = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
    }

        /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 资源键
    /// </summary>
    public string ResourceKey { get; set; }

        /// <summary>
    /// 翻译值
    /// </summary>
    public string TranslationValue { get; set; }

        /// <summary>
    /// 资源类型
    /// </summary>
    public string ResourceType { get; set; }

        /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 翻译表导出DTO
/// </summary>
public partial class TaktTranslationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTranslationExportDto()
    {
        CreatedAt = DateTime.Now;
        CultureCode = string.Empty;
        ResourceKey = string.Empty;
        TranslationValue = string.Empty;
        ResourceType = string.Empty;
    }

        /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 资源键
    /// </summary>
    public string ResourceKey { get; set; }

        /// <summary>
    /// 翻译值
    /// </summary>
    public string TranslationValue { get; set; }

        /// <summary>
    /// 资源类型
    /// </summary>
    public string ResourceType { get; set; }

        /// <summary>
    /// 资源分组
    /// </summary>
    public string? ResourceGroup { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}