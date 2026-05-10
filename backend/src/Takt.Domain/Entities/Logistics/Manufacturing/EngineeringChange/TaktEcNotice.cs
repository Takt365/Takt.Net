// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNotice.cs
// 创建时间：2026-05-07
// 创建人：Takt365(Qoder AI)
// 功能描述：工程变更通知单（EC Notice），用于将设变（ECN）通知到相关部门和人员
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 工程变更通知单实体（EC Notice），用于将设变（ECN）通知到相关部门和人员，追踪通知状态和反馈
/// </summary>
[SugarTable("takt_logistics_manufacturing_ecn_notice", "工程变更通知单表")]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_notice_no", nameof(NoticeNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_ecn_id", nameof(EcnId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_notice_date", nameof(NoticeDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_notice_status", nameof(NoticeStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_ecn_notice_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEcNotice : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 通知单号（唯一，如：ECN-2026-0001）
    /// </summary>
    [SugarColumn(ColumnName = "notice_no", ColumnDescription = "通知单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string NoticeNo { get; set; } = string.Empty;

    /// <summary>
    /// 关联的设变主表ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "ecn_id", ColumnDescription = "设变ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EcnId { get; set; }

    /// <summary>
    /// 设变单号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "ecn_no", ColumnDescription = "设变单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EcnNo { get; set; } = string.Empty;

    /// <summary>
    /// 设变主题（冗余字段）
    /// </summary>
    [SugarColumn(ColumnName = "ecn_title", ColumnDescription = "设变主题", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? EcnTitle { get; set; }

    /// <summary>
    /// 通知日期
    /// </summary>
    [SugarColumn(ColumnName = "notice_date", ColumnDescription = "通知日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime NoticeDate { get; set; }

    /// <summary>
    /// 通知部门编码（多个部门用逗号分隔，如：Assy,PCBA,QC）
    /// </summary>
    [SugarColumn(ColumnName = "notice_dept_codes", ColumnDescription = "通知部门编码", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? NoticeDeptCodes { get; set; }

    /// <summary>
    /// 通知部门名称（多个部门用逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "notice_dept_names", ColumnDescription = "通知部门名称", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? NoticeDeptNames { get; set; }

    /// <summary>
    /// 通知人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "notifier_id", ColumnDescription = "通知人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? NotifierId { get; set; }

    /// <summary>
    /// 通知人姓名
    /// </summary>
    [SugarColumn(ColumnName = "notifier_name", ColumnDescription = "通知人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? NotifierName { get; set; }

    /// <summary>
    /// 通知方式（1=系统通知 2=邮件 3=纸质 4=会议）
    /// </summary>
    [SugarColumn(ColumnName = "notice_method", ColumnDescription = "通知方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int NoticeMethod { get; set; } = 1;

    /// <summary>
    /// 通知状态（0=待通知 1=已通知 2=已确认 3=已驳回 4=已过期）
    /// </summary>
    [SugarColumn(ColumnName = "notice_status", ColumnDescription = "通知状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int NoticeStatus { get; set; } = 0;

    /// <summary>
    /// 确认人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "confirmer_id", ColumnDescription = "确认人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ConfirmerId { get; set; }

    /// <summary>
    /// 确认人姓名
    /// </summary>
    [SugarColumn(ColumnName = "confirmer_name", ColumnDescription = "确认人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ConfirmerName { get; set; }

    /// <summary>
    /// 确认日期
    /// </summary>
    [SugarColumn(ColumnName = "confirm_date", ColumnDescription = "确认日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ConfirmDate { get; set; }

    /// <summary>
    /// 确认意见/反馈
    /// </summary>
    [SugarColumn(ColumnName = "confirm_comment", ColumnDescription = "确认意见", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ConfirmComment { get; set; }

    /// <summary>
    /// 要求反馈截止日期
    /// </summary>
    [SugarColumn(ColumnName = "require_feedback_date", ColumnDescription = "要求反馈截止日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? RequireFeedbackDate { get; set; }

    /// <summary>
    /// 流程实例ID（关联工作流，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; } = 0;

    /// <summary>
    /// 关联的设变主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(EcnId))]
    public TaktEc? Ecn { get; set; }
}
