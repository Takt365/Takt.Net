// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Business.HelpDesk
// 文件名称：TaktTicketCategoryAssign.cs
// 功能描述：工单分类默认处理人配置，接收工单时根据类型（CategoryCode）系统自动分配该处理人。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// 工单分类默认处理人。按分类编码配置默认处理人，创建/接收工单时根据 CategoryCode 自动写入工单的 AssigneeId/AssigneeName。
/// </summary>
[SugarTable("takt_routine_help_desk_ticket_category_assign", "工单分类默认处理人表")]
[SugarIndex("ix_takt_routine_help_desk_ticket_category_assign_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_category_assign_category_code", nameof(CategoryCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_category_assign_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTicketCategoryAssign : TaktEntityBase
{
    /// <summary>
    /// 分类编码（与 TaktTicket.CategoryCode 对应，如 incident、request）
    /// </summary>
    [SugarColumn(ColumnName = "category_code", ColumnDescription = "分类编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CategoryCode { get; set; } = string.Empty;

    /// <summary>
    /// 默认处理人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "assignee_id", ColumnDescription = "默认处理人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AssigneeId { get; set; }

    /// <summary>
    /// 默认处理人姓名
    /// </summary>
    [SugarColumn(ColumnName = "assignee_name", ColumnDescription = "默认处理人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? AssigneeName { get; set; }

    /// <summary>
    /// 排序号（同分类多规则时按此排序，取第一条）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;
}
