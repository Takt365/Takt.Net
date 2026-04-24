// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.I18n
// 文件名称：TaktTranslation.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt翻译实体，定义多语言翻译领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.I18n;

/// <summary>
/// Takt翻译实体（主子表关系：主表）
/// </summary>
[SugarTable("takt_routine_i18n_translation", "翻译表")]
[SugarIndex("ix_takt_routine_i18n_translation_language_id", nameof(LanguageId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_translation_resource_key", nameof(ResourceKey), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_translation_culture_code", nameof(CultureCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_translation_resource_key_culture", nameof(ResourceKey), OrderByType.Asc, nameof(CultureCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_i18n_translation_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_translation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_i18n_translation_resource_type", nameof(ResourceType), OrderByType.Asc)]
public class TaktTranslation : TaktEntityBase
{
    /// <summary>
    /// 语言ID（外键，关联子表 TaktLanguage.Id）
    /// </summary>
    [SugarColumn(ColumnName = "language_id", ColumnDescription = "语言ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long LanguageId { get; set; }

    /// <summary>
    /// 语言编码（冗余，便于查询与展示；业务键为 LanguageId）
    /// </summary>
    [SugarColumn(ColumnName = "culture_code", ColumnDescription = "语言编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string CultureCode { get; set; } = string.Empty;

    /// <summary>
    /// 资源键（如：UserNotFound、OperationSuccess）
    /// </summary>
    [SugarColumn(ColumnName = "resource_key", ColumnDescription = "资源键", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ResourceKey { get; set; } = string.Empty;

    /// <summary>
    /// 翻译值（该语言下的文本内容）
    /// </summary>
    [SugarColumn(ColumnName = "translation_value", ColumnDescription = "翻译值", ColumnDataType = "nvarchar", Length = 2000, IsNullable = false)]
    public string TranslationValue { get; set; } = string.Empty;

    /// <summary>
    /// 资源类型（Frontend=前端，Backend=后端）
    /// </summary>
    [SugarColumn(ColumnName = "resource_type", ColumnDescription = "资源类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "Backend")]
    public string ResourceType { get; set; } = "Backend";

    /// <summary>
    /// 资源分组（如：Validation、Error、Success，用于进一步分类）
    /// </summary>
    [SugarColumn(ColumnName = "resource_group", ColumnDescription = "资源分组", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ResourceGroup { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 所属语言（主子表关系：主表本实体，子表 TaktLanguage）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(LanguageId))]
    public TaktLanguage? Language { get; set; }
}
