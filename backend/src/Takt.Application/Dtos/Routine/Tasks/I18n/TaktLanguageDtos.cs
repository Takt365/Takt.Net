// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.I18n
// 文件名称：TaktLanguageDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：语言表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.I18n;

/// <summary>
/// 语言表Dto
/// </summary>
public partial class TaktLanguageDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageDto()
    {
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
    }

    /// <summary>
    /// 语言表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string LanguageName { get; set; }
    /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }
    /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; }
    /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }
    /// <summary>
    /// 是否默认语言
    /// </summary>
    public int IsDefault { get; set; }
    /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int IsRtl { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 语言状态
    /// </summary>
    public int LanguageStatus { get; set; }
}

/// <summary>
/// 语言表查询DTO
/// </summary>
public partial class TaktLanguageQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 语言表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string? LanguageName { get; set; }
    /// <summary>
    /// 语言编码
    /// </summary>
    public string? CultureCode { get; set; }
    /// <summary>
    /// 本地化名称
    /// </summary>
    public string? NativeName { get; set; }
    /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }
    /// <summary>
    /// 是否默认语言
    /// </summary>
    public int? IsDefault { get; set; }
    /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int? IsRtl { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }
    /// <summary>
    /// 语言状态
    /// </summary>
    public int? LanguageStatus { get; set; }

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
/// Takt创建语言表DTO
/// </summary>
public partial class TaktLanguageCreateDto
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
    /// 语言名称
    /// </summary>
    public string LanguageName { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; }

        /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }

        /// <summary>
    /// 是否默认语言
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int IsRtl { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 语言状态
    /// </summary>
    public int LanguageStatus { get; set; }

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
/// Takt更新语言表DTO
/// </summary>
public partial class TaktLanguageUpdateDto : TaktLanguageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageUpdateDto()
    {
    }

        /// <summary>
    /// 语言表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }
}

/// <summary>
/// 语言表语言状态DTO
/// </summary>
public partial class TaktLanguageStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageStatusDto()
    {
    }

        /// <summary>
    /// 语言表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言状态（0=禁用，1=启用）
    /// </summary>
    public int LanguageStatus { get; set; }
}

/// <summary>
/// 语言表导入模板DTO
/// </summary>
public partial class TaktLanguageTemplateDto
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
    /// 语言名称
    /// </summary>
    public string LanguageName { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; }

        /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }

        /// <summary>
    /// 是否默认语言
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int IsRtl { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 语言状态
    /// </summary>
    public int LanguageStatus { get; set; }

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
/// 语言表导入DTO
/// </summary>
public partial class TaktLanguageImportDto
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
    /// 语言名称
    /// </summary>
    public string LanguageName { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; }

        /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }

        /// <summary>
    /// 是否默认语言
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int IsRtl { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 语言状态
    /// </summary>
    public int LanguageStatus { get; set; }

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
/// 语言表导出DTO
/// </summary>
public partial class TaktLanguageExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLanguageExportDto()
    {
        CreatedAt = DateTime.Now;
        LanguageName = string.Empty;
        CultureCode = string.Empty;
        NativeName = string.Empty;
    }

        /// <summary>
    /// 语言名称
    /// </summary>
    public string LanguageName { get; set; }

        /// <summary>
    /// 语言编码
    /// </summary>
    public string CultureCode { get; set; }

        /// <summary>
    /// 本地化名称
    /// </summary>
    public string NativeName { get; set; }

        /// <summary>
    /// 语言图标
    /// </summary>
    public string? LanguageIcon { get; set; }

        /// <summary>
    /// 是否默认语言
    /// </summary>
    public int IsDefault { get; set; }

        /// <summary>
    /// 是否启用RTL
    /// </summary>
    public int IsRtl { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 语言状态
    /// </summary>
    public int LanguageStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}