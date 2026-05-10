// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Business.Visiting
// 文件名称：TaktVisitPerson.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：参访人员子表实体，参访人员（部门、职称、参访人员姓名）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.Visiting;

/// <summary>
/// 参访人员子表实体（部门、职称、参访人员）
/// </summary>
[SugarTable("takt_routine_business_visiting_person", "参访人员表")]
[SugarIndex("ix_takt_routine_business_visiting_person_visit_id", nameof(VisitId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_person_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_business_visiting_person_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktVisitPerson : TaktEntityBase
{
    /// <summary>
    /// 参访记录ID（主表ID，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "visit_id", ColumnDescription = "参访记录ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long VisitId { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "department", ColumnDescription = "部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// 职称
    /// </summary>
    [SugarColumn(ColumnName = "job_title", ColumnDescription = "职称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 参访人员姓名
    /// </summary>
    [SugarColumn(ColumnName = "person_name", ColumnDescription = "参访人员", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PersonName { get; set; } = string.Empty;

    /// <summary>
    /// 参访记录（主表）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(VisitId))]
    public TaktVisit? Visit { get; set; }
}
