// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.HelpDesk
// 文件名称：TaktKnowledge.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库实体，服务台知识库领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// Takt知识库实体
/// </summary>
[SugarTable("takt_routine_help_desk_knowledge", "知识库表")]
[SugarIndex("ix_takt_routine_help_desk_knowledge_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_category_code", nameof(CategoryCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_knowledge_status", nameof(KnowledgeStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_is_published", nameof(IsPublished), OrderByType.Asc)]
public class TaktKnowledge : TaktEntityBase
{
    /// <summary>
    /// 知识标题
    /// </summary>
    [SugarColumn(ColumnName = "title", ColumnDescription = "知识标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 知识内容（富文本/HTML）
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "知识内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 知识摘要（简短描述，列表/搜索展示）
    /// </summary>
    [SugarColumn(ColumnName = "summary", ColumnDescription = "知识摘要", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Summary { get; set; }

    /// <summary>
    /// 分类编码（如 faq/guide 等）
    /// </summary>
    [SugarColumn(ColumnName = "category_code", ColumnDescription = "分类编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 标签（逗号分隔或JSON数组存储）
    /// </summary>
    [SugarColumn(ColumnName = "tags", ColumnDescription = "标签", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Tags { get; set; }

    /// <summary>
    /// 知识状态（0=草稿，1=已发布，2=已下架）
    /// </summary>
    [SugarColumn(ColumnName = "knowledge_status", ColumnDescription = "知识状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int KnowledgeStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 浏览次数
    /// </summary>
    [SugarColumn(ColumnName = "view_count", ColumnDescription = "浏览次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ViewCount { get; set; } = 0;

    /// <summary>
    /// 有用评价数（用户点“有帮助”次数）
    /// </summary>
    [SugarColumn(ColumnName = "helpful_count", ColumnDescription = "有用评价数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HelpfulCount { get; set; } = 0;

    /// <summary>
    /// 无帮助评价数（用户点“无帮助”次数）
    /// </summary>
    [SugarColumn(ColumnName = "unhelpful_count", ColumnDescription = "无帮助评价数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UnhelpfulCount { get; set; } = 0;

    /// <summary>
    /// 是否已发布（0=否，1=是；与 KnowledgeStatus=1 语义一致，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "is_published", ColumnDescription = "是否已发布", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPublished { get; set; } = 0;

    /// <summary>
    /// 版本号（每次修订可自增，用于订版本）
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDescription = "版本号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Version { get; set; } = 1;

    /// <summary>
    /// 发布时间（首次发布或重新发布时间）
    /// </summary>
    [SugarColumn(ColumnName = "published_at", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// 最后修订时间（订版本相关，可与 UpdatedAt 同步或独立维护）
    /// </summary>
    [SugarColumn(ColumnName = "revised_at", ColumnDescription = "最后修订时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RevisedAt { get; set; }
}
