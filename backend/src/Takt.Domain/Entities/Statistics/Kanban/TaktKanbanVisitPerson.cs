// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Statistics.Kanban
// 文件名称：TaktKanbanVisitPerson.cs
// 功能描述：看板来访人员子表实体，来访人员（部门、职称、来访人员姓名）
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Statistics.Kanban;

/// <summary>
/// 看板来访人员子表实体（部门、职称、来访人员）
/// </summary>
[SugarTable("takt_statistics_kanban_visit_person", "看板来访人员表")]
[SugarIndex("ix_takt_statistics_kanban_visit_person_visit_id", nameof(VisitId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_person_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_person_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktKanbanVisitPerson : TaktEntityBase
{
    /// <summary>
    /// 来访记录ID（主表ID，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "visit_id", ColumnDescription = "来访记录ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long VisitId { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "department", ColumnDescription = "部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Department { get; set; }

    /// <summary>
    /// 职称
    /// </summary>
    [SugarColumn(ColumnName = "job_title", ColumnDescription = "职称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? JobTitle { get; set; }

    /// <summary>
    /// 来访人员姓名
    /// </summary>
    [SugarColumn(ColumnName = "person_name", ColumnDescription = "来访人员", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PersonName { get; set; } = string.Empty;
}
