// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Business.Visiting
// 文件名称：TaktVisit.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：参访公司主表实体，来访公司（公司名称、参访起止时间）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.Visiting;

/// <summary>
/// 参访公司主表实体（来访公司：公司名称，参访起止时间）
/// </summary>
[SugarTable("takt_routine_business_visiting", "参访公司表")]
[SugarIndex("ix_takt_routine_business_visiting_company_name", nameof(CompanyName), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_visit_start", nameof(VisitStartTime), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_visit_end", nameof(VisitEndTime), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktVisit : TaktEntityBase
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
    [Navigate(NavigateType.OneToMany, nameof(TaktVisitPerson.VisitId))]
    public List<TaktVisitPerson>? Persons { get; set; }
}
