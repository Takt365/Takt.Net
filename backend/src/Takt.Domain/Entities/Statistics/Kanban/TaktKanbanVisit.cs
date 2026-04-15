// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Statistics.Kanban
// 文件名称：TaktKanbanVisit.cs
// 功能描述：看板来访公司主表实体，来访公司（公司名称、参访起止时间）
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Statistics.Kanban;

/// <summary>
/// 看板来访公司主表实体（来访公司：公司名称，参访起止时间）
/// </summary>
[SugarTable("takt_statistics_kanban_visit", "看板来访公司表")]
[SugarIndex("ix_takt_statistics_kanban_visit_company_name", nameof(CompanyName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_visit_start", nameof(VisitStartTime), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_visit_end", nameof(VisitEndTime), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_visit_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktKanbanVisit : TaktEntityBase
{
    /// <summary>
    /// 公司名称（来访公司）
    /// </summary>
    [SugarColumn(ColumnName = "company_name", ColumnDescription = "公司名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// 参访开始时间
    /// </summary>
    [SugarColumn(ColumnName = "visit_start_time", ColumnDescription = "参访开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime VisitStartTime { get; set; }

    /// <summary>
    /// 参访结束时间
    /// </summary>
    [SugarColumn(ColumnName = "visit_end_time", ColumnDescription = "参访结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime VisitEndTime { get; set; }

    /// <summary>
    /// 来访人员列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktKanbanVisitPerson.VisitId))]
    public List<TaktKanbanVisitPerson>? Persons { get; set; }
}
