// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.I18n
// 文件名称：TaktLanguage.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt语言实体，定义支持的语言领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.I18n;

/// <summary>
/// Takt语言实体（主子表关系：子表）
/// </summary>
[SugarTable("takt_routine_i18n_language", "语言表")]
[SugarIndex("ix_takt_routine_i18n_language_culture_code", nameof(CultureCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_i18n_language_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_language_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_language_language_status", nameof(LanguageStatus), OrderByType.Asc)]
public class TaktLanguage : TaktEntityBase
{
    /// <summary>
    /// 语言名称（中文名称，如：简体中文）
    /// </summary>
    [SugarColumn(ColumnName = "language_name", ColumnDescription = "语言名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string LanguageName { get; set; } = string.Empty;

    /// <summary>
    /// 语言编码（ISO 639-1/639-2，如：zh-CN、en-US）
    /// </summary>
    [SugarColumn(ColumnName = "culture_code", ColumnDescription = "语言编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string CultureCode { get; set; } = string.Empty;

    /// <summary>
    /// 本地化名称（该语言下的名称，如：简体中文、English）
    /// </summary>
    [SugarColumn(ColumnName = "native_name", ColumnDescription = "本地化名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string NativeName { get; set; } = string.Empty;

    /// <summary>
    /// 语言图标（国旗图标或语言图标URL）
    /// </summary>
    [SugarColumn(ColumnName = "language_icon", ColumnDescription = "语言图标", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LanguageIcon { get; set; }
    /// <summary>
    /// 是否默认语言（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_default", ColumnDescription = "是否默认语言", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsDefault { get; set; } = 1;

    /// <summary>
    /// 是否启用RTL（从右到左，1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_rtl", ColumnDescription = "是否启用RTL", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsRtl { get; set; } = 1;
    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 语言状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "language_status", ColumnDescription = "语言状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LanguageStatus { get; set; } = 0;
}
