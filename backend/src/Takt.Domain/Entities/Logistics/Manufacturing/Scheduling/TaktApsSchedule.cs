// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsSchedule.cs
// 功能描述：APS排程主表，高级计划与排程
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程主表（高级计划与排程）
/// </summary>
[SugarTable("takt_logistics_manufacturing_scheduling_aps_schedule", "APS排程主表")]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_schedule_code", nameof(ScheduleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_schedule_type", nameof(ScheduleType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_schedule_status", nameof(ScheduleStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_plan_date", nameof(PlanDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_scheduling_aps_schedule_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktApsSchedule : TaktEntityBase
{
    /// <summary>
    /// 排程编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_code", ColumnDescription = "排程编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ScheduleCode { get; set; } = string.Empty;

    /// <summary>
    /// 排程名称
    /// </summary>
    [SugarColumn(ColumnName = "schedule_name", ColumnDescription = "排程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ScheduleName { get; set; } = string.Empty;

    /// <summary>
    /// 排程类型（0=主生产计划，1=车间作业计划，2=紧急插单，3=计划调整）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_type", ColumnDescription = "排程类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ScheduleType { get; set; } = 0;

    /// <summary>
    /// 计划日期
    /// </summary>
    [SugarColumn(ColumnName = "plan_date", ColumnDescription = "计划日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PlanDate { get; set; }

    /// <summary>
    /// 计划开始时间
    /// </summary>
    [SugarColumn(ColumnName = "plan_start_time", ColumnDescription = "计划开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PlanStartTime { get; set; }

    /// <summary>
    /// 计划结束时间
    /// </summary>
    [SugarColumn(ColumnName = "plan_end_time", ColumnDescription = "计划结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PlanEndTime { get; set; }

    /// <summary>
    /// 计划周期（0=日计划，1=周计划，2=月计划）
    /// </summary>
    [SugarColumn(ColumnName = "plan_cycle", ColumnDescription = "计划周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PlanCycle { get; set; } = 0;

    /// <summary>
    /// 工厂编码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 工厂名称
    /// </summary>
    [SugarColumn(ColumnName = "plant_name", ColumnDescription = "工厂名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? PlantName { get; set; }

    /// <summary>
    /// 车间编码
    /// </summary>
    [SugarColumn(ColumnName = "workshop_code", ColumnDescription = "车间编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? WorkshopCode { get; set; }

    /// <summary>
    /// 车间名称
    /// </summary>
    [SugarColumn(ColumnName = "workshop_name", ColumnDescription = "车间名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? WorkshopName { get; set; }

    /// <summary>
    /// 生产线编码
    /// </summary>
    [SugarColumn(ColumnName = "production_line_code", ColumnDescription = "生产线编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ProductionLineCode { get; set; }

    /// <summary>
    /// 生产线名称
    /// </summary>
    [SugarColumn(ColumnName = "production_line_name", ColumnDescription = "生产线名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ProductionLineName { get; set; }

    /// <summary>
    /// 排程策略（0=按订单排程，1=按库存排程，2=混合排程）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_strategy", ColumnDescription = "排程策略", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ScheduleStrategy { get; set; } = 0;

    /// <summary>
    /// 排程算法（0=正向排程，1=逆向排程，2=双向排程）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_algorithm", ColumnDescription = "排程算法", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ScheduleAlgorithm { get; set; } = 0;

    /// <summary>
    /// 优化目标（0=交期优先，1=产能优先，2=成本优先，3=均衡生产）
    /// </summary>
    [SugarColumn(ColumnName = "optimization_objective", ColumnDescription = "优化目标", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OptimizationObjective { get; set; } = 0;

    /// <summary>
    /// 排程状态（0=草稿，1=计算中，2=已计算，3=已发布，4=执行中，5=已完成，6=已取消）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_status", ColumnDescription = "排程状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ScheduleStatus { get; set; } = 0;

    /// <summary>
    /// 计划员ID
    /// </summary>
    [SugarColumn(ColumnName = "planner_id", ColumnDescription = "计划员ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PlannerId { get; set; }

    /// <summary>
    /// 计划员姓名
    /// </summary>
    [SugarColumn(ColumnName = "planner_name", ColumnDescription = "计划员姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlannerName { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    [SugarColumn(ColumnName = "publish_time", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 发布人ID
    /// </summary>
    [SugarColumn(ColumnName = "publish_user_id", ColumnDescription = "发布人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PublishUserId { get; set; }

    /// <summary>
    /// 发布人姓名
    /// </summary>
    [SugarColumn(ColumnName = "publish_user_name", ColumnDescription = "发布人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PublishUserName { get; set; }

    /// <summary>
    /// 排程说明
    /// </summary>
    [SugarColumn(ColumnName = "schedule_description", ColumnDescription = "排程说明", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 排程明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktApsScheduleItem.ApsScheduleId))]
    public List<TaktApsScheduleItem>? Items { get; set; }

    /// <summary>
    /// 变更日志列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktApsScheduleChangeLog.ApsScheduleId))]
    public List<TaktApsScheduleChangeLog>? ChangeLogs { get; set; }
}
