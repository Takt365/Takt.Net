// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.HelpDesk
// 文件名称：TaktKnowledgeChangeLog.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt知识库变更日志实体，完整记录知识条目的创建/修改/删除历史
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// Takt知识库变更日志实体
/// </summary>
[SugarTable("takt_routine_help_desk_knowledge_change_log", "知识库变更日志表")]
[SugarIndex("ix_takt_routine_help_desk_knowledge_change_log_knowledge_id", nameof(KnowledgeId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_change_log_created_at", nameof(CreatedAt), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_change_log_change_type", nameof(ChangeType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_change_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_knowledge_change_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktKnowledgeChangeLog : TaktEntityBase
{
    /// <summary>
    /// 知识ID（关联知识库表，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "knowledge_id", ColumnDescription = "知识ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long KnowledgeId { get; set; }

    /// <summary>
    /// 知识标题（冗余，便于日志列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "knowledge_title", ColumnDescription = "知识标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? KnowledgeTitle { get; set; }

    /// <summary>
    /// 变更类型（0=创建，1=更新，2=删除）
    /// </summary>
    [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ChangeType { get; set; } = 1;

    /// <summary>
    /// 修改内容摘要（简短描述本次变更，如“修改标题与摘要”“发布知识”“下架”等，便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "change_summary", ColumnDescription = "修改内容摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ChangeSummary { get; set; }

    /// <summary>
    /// 变更字段列表（JSON数组格式，记录同一时间点修改的所有字段及其旧值、新值）
    /// 格式：[{"field":"FieldName","description":"字段描述","oldValue":"旧值","newValue":"新值"}]
    /// </summary>
    [SugarColumn(ColumnName = "change_fields", ColumnDescription = "变更字段列表", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? ChangeFields { get; set; }

    /// <summary>
    /// 变更原因或备注（变更时间、变更人由基类 CreatedAt/CreatedById/CreatedBy 表示）
    /// </summary>
    [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 变更时知识版本号（与 TaktKnowledge.Version 对应）
    /// </summary>
    [SugarColumn(ColumnName = "version_at_change", ColumnDescription = "变更时版本号", ColumnDataType = "int", IsNullable = true)]
    public int? VersionAtChange { get; set; }

    /// <summary>
    /// 知识库（主表）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(KnowledgeId))]
    public TaktKnowledge? Knowledge { get; set; }
}
