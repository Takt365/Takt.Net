// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Business.HelpDesk
// 文件名称：TaktTicketEvaluation.cs
// 功能描述：工单服务评价实体，关联工单，工单关闭后用户对服务进行评价。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// 工单服务评价。一个工单可对应一条评价（关闭后由提交人或客户评价）。
/// </summary>
[SugarTable("takt_routine_help_desk_ticket_evaluation", "工单服务评价表")]
[SugarIndex("ix_takt_routine_help_desk_ticket_evaluation_ticket_id", nameof(TicketId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_help_desk_ticket_evaluation_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_evaluation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTicketEvaluation : TaktEntityBase
{
    /// <summary>
    /// 工单ID（关联 TaktTicket.Id，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "ticket_id", ColumnDescription = "工单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TicketId { get; set; }

    /// <summary>
    /// 综合评分（如 1-5 星或 1-10 分，具体由前端约定）
    /// </summary>
    [SugarColumn(ColumnName = "score", ColumnDescription = "综合评分", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Score { get; set; } = 0;

    /// <summary>
    /// 评价内容/评语
    /// </summary>
    [SugarColumn(ColumnName = "comment", ColumnDescription = "评价内容", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Comment { get; set; }

    /// <summary>
    /// 评价人ID（提交人或其他评价者，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "evaluator_id", ColumnDescription = "评价人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EvaluatorId { get; set; }

    /// <summary>
    /// 评价人姓名
    /// </summary>
    [SugarColumn(ColumnName = "evaluator_name", ColumnDescription = "评价人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EvaluatorName { get; set; }

    /// <summary>
    /// 评价时间
    /// </summary>
    [SugarColumn(ColumnName = "evaluated_at", ColumnDescription = "评价时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EvaluatedAt { get; set; } = DateTime.Now;
}
