// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Tasks.WordFilter
// 文件名称：TaktSensitiveWords.cs
// 创建时间：2026-04-21
// 创建人：Takt365(Cursor AI)
// 功能描述：敏感词实体
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.WordFilter;

/// <summary>
/// 敏感词实体
/// </summary>
[SugarTable("takt_routine_sensitive_word", "敏感词表")]
[SugarIndex("ix_takt_routine_sensitive_word_word_text", nameof(WordText), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_sensitive_word_word_category", nameof(WordCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_sensitive_word_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSensitiveWords : TaktEntityBase
{
    /// <summary>
    /// 敏感词文本
    /// </summary>
    [SugarColumn(ColumnName = "word_text", ColumnDescription = "敏感词文本", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string WordText { get; set; } = string.Empty;

    /// <summary>
    /// 词性类别（数据字典值，如：政治敏感、暴力恐怖、色情低俗等）
    /// </summary>
    [SugarColumn(ColumnName = "word_category", ColumnDescription = "词性类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WordCategory { get; set; } = 0;

    /// <summary>
    /// 过滤等级（1=低，2=中，3=高）
    /// </summary>
    [SugarColumn(ColumnName = "filter_level", ColumnDescription = "过滤等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int FilterLevel { get; set; } = 1;

    /// <summary>
    /// 替换文本（为空时使用*替换）
    /// </summary>
    [SugarColumn(ColumnName = "replace_text", ColumnDescription = "替换文本", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ReplaceText { get; set; }

    /// <summary>
    /// 状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;
}
