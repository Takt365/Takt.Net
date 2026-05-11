// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：APS排程主表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程主表Dto
/// </summary>
public partial class TaktApsScheduleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleDto()
    {
        PlantCode = string.Empty;
        ScheduleCode = string.Empty;
        ScheduleName = string.Empty;
    }

    /// <summary>
    /// APS排程主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; } = 0;

    /// <summary>
    /// 工厂编码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 排程编码
    /// </summary>
    public string ScheduleCode { get; set; }
    /// <summary>
    /// 排程名称
    /// </summary>
    public string ScheduleName { get; set; }
    /// <summary>
    /// 排程类型
    /// </summary>
    public int ScheduleType { get; set; }
    /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime PlanDate { get; set; }
    /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }
    /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }
    /// <summary>
    /// 计划周期
    /// </summary>
    public int PlanCycle { get; set; }
    /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }
    /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }
    /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 排程策略
    /// </summary>
    public int ScheduleStrategy { get; set; }
    /// <summary>
    /// 排程算法
    /// </summary>
    public int ScheduleAlgorithm { get; set; }
    /// <summary>
    /// 优化目标
    /// </summary>
    public int OptimizationObjective { get; set; }
    /// <summary>
    /// 排程状态
    /// </summary>
    public int ScheduleStatus { get; set; }
    /// <summary>
    /// 计划员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PlannerId { get; set; }
    /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PublishUserId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }
    /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 排程明细列表（主子表关系）（外键在子表 TaktApsScheduleItemDto.ApsScheduleId）
    /// </summary>
    public List<TaktApsScheduleItemDto>? Items { get; set; }

    /// <summary>
    /// 变更日志列表（主子表关系）（外键在子表 TaktApsScheduleChangeLogDto.ApsScheduleId）
    /// </summary>
    public List<TaktApsScheduleChangeLogDto>? ChangeLogs { get; set; }
}

/// <summary>
/// APS排程主表查询DTO
/// </summary>
public partial class TaktApsScheduleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂编码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 排程编码
    /// </summary>
    public string? ScheduleCode { get; set; }
    /// <summary>
    /// 排程名称
    /// </summary>
    public string? ScheduleName { get; set; }
    /// <summary>
    /// 排程类型
    /// </summary>
    public int? ScheduleType { get; set; }
    /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime? PlanDate { get; set; }

    /// <summary>
    /// 计划日期开始时间
    /// </summary>
    public DateTime? PlanDateStart { get; set; }
    /// <summary>
    /// 计划日期结束时间
    /// </summary>
    public DateTime? PlanDateEnd { get; set; }
    /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime? PlanStartTime { get; set; }

    /// <summary>
    /// 计划开始时间开始时间
    /// </summary>
    public DateTime? PlanStartTimeStart { get; set; }
    /// <summary>
    /// 计划开始时间结束时间
    /// </summary>
    public DateTime? PlanStartTimeEnd { get; set; }
    /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime? PlanEndTime { get; set; }

    /// <summary>
    /// 计划结束时间开始时间
    /// </summary>
    public DateTime? PlanEndTimeStart { get; set; }
    /// <summary>
    /// 计划结束时间结束时间
    /// </summary>
    public DateTime? PlanEndTimeEnd { get; set; }
    /// <summary>
    /// 计划周期
    /// </summary>
    public int? PlanCycle { get; set; }
    /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }
    /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }
    /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 排程策略
    /// </summary>
    public int? ScheduleStrategy { get; set; }
    /// <summary>
    /// 排程算法
    /// </summary>
    public int? ScheduleAlgorithm { get; set; }
    /// <summary>
    /// 优化目标
    /// </summary>
    public int? OptimizationObjective { get; set; }
    /// <summary>
    /// 排程状态
    /// </summary>
    public int? ScheduleStatus { get; set; }
    /// <summary>
    /// 计划员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PlannerId { get; set; }
    /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 发布时间开始时间
    /// </summary>
    public DateTime? PublishTimeStart { get; set; }
    /// <summary>
    /// 发布时间结束时间
    /// </summary>
    public DateTime? PublishTimeEnd { get; set; }
    /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PublishUserId { get; set; }
    /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }
    /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建APS排程主表DTO
/// </summary>
public partial class TaktApsScheduleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleCreateDto()
    {
        PlantCode = string.Empty;
        ScheduleCode = string.Empty;
        ScheduleName = string.Empty;
    }

        /// <summary>
    /// 工厂编码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 排程编码
    /// </summary>
    public string ScheduleCode { get; set; }

        /// <summary>
    /// 排程名称
    /// </summary>
    public string ScheduleName { get; set; }

        /// <summary>
    /// 排程类型
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 计划周期
    /// </summary>
    public int PlanCycle { get; set; }

        /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }

        /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

        /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 排程策略
    /// </summary>
    public int ScheduleStrategy { get; set; }

        /// <summary>
    /// 排程算法
    /// </summary>
    public int ScheduleAlgorithm { get; set; }

        /// <summary>
    /// 优化目标
    /// </summary>
    public int OptimizationObjective { get; set; }

        /// <summary>
    /// 排程状态
    /// </summary>
    public int ScheduleStatus { get; set; }

        /// <summary>
    /// 计划员ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PlannerId { get; set; }

        /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PublishUserId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }

        /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 排程明细列表（主子表关系）（外键在子表 TaktApsScheduleItemCreateDto.ApsScheduleId）
    /// </summary>
    public List<TaktApsScheduleItemCreateDto>? Items { get; set; }


    /// <summary>
    /// 变更日志列表（主子表关系）（外键在子表 TaktApsScheduleChangeLogCreateDto.ApsScheduleId）
    /// </summary>
    public List<TaktApsScheduleChangeLogCreateDto>? ChangeLogs { get; set; }

}

/// <summary>
/// Takt更新APS排程主表DTO
/// </summary>
public partial class TaktApsScheduleUpdateDto : TaktApsScheduleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleUpdateDto()
    {
    }

        /// <summary>
    /// APS排程主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; } = 0;
}

/// <summary>
/// APS排程主表排程状态DTO
/// </summary>
public partial class TaktApsScheduleScheduleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleScheduleStatusDto()
    {
    }

        /// <summary>
    /// APS排程主表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; } = 0;

    /// <summary>
    /// 排程状态（0=禁用，1=启用）
    /// </summary>
    public int ScheduleStatus { get; set; }
}

/// <summary>
/// APS排程主表导入模板DTO
/// </summary>
public partial class TaktApsScheduleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleTemplateDto()
    {
        PlantCode = string.Empty;
        ScheduleCode = string.Empty;
        ScheduleName = string.Empty;
    }

        /// <summary>
    /// 工厂编码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 排程编码
    /// </summary>
    public string ScheduleCode { get; set; }

        /// <summary>
    /// 排程名称
    /// </summary>
    public string ScheduleName { get; set; }

        /// <summary>
    /// 排程类型
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 计划周期
    /// </summary>
    public int PlanCycle { get; set; }

        /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }

        /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

        /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 排程策略
    /// </summary>
    public int ScheduleStrategy { get; set; }

        /// <summary>
    /// 排程算法
    /// </summary>
    public int ScheduleAlgorithm { get; set; }

        /// <summary>
    /// 优化目标
    /// </summary>
    public int OptimizationObjective { get; set; }

        /// <summary>
    /// 排程状态
    /// </summary>
    public int ScheduleStatus { get; set; }

        /// <summary>
    /// 计划员ID
    /// </summary>
    public long? PlannerId { get; set; }

        /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long? PublishUserId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }

        /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// APS排程主表导入DTO
/// </summary>
public partial class TaktApsScheduleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleImportDto()
    {
        PlantCode = string.Empty;
        ScheduleCode = string.Empty;
        ScheduleName = string.Empty;
    }

        /// <summary>
    /// 工厂编码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 排程编码
    /// </summary>
    public string ScheduleCode { get; set; }

        /// <summary>
    /// 排程名称
    /// </summary>
    public string ScheduleName { get; set; }

        /// <summary>
    /// 排程类型
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 计划周期
    /// </summary>
    public int PlanCycle { get; set; }

        /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }

        /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

        /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 排程策略
    /// </summary>
    public int ScheduleStrategy { get; set; }

        /// <summary>
    /// 排程算法
    /// </summary>
    public int ScheduleAlgorithm { get; set; }

        /// <summary>
    /// 优化目标
    /// </summary>
    public int OptimizationObjective { get; set; }

        /// <summary>
    /// 排程状态
    /// </summary>
    public int ScheduleStatus { get; set; }

        /// <summary>
    /// 计划员ID
    /// </summary>
    public long? PlannerId { get; set; }

        /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long? PublishUserId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }

        /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// APS排程主表导出DTO
/// </summary>
public partial class TaktApsScheduleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        ScheduleCode = string.Empty;
        ScheduleName = string.Empty;
    }

        /// <summary>
    /// 工厂编码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 排程编码
    /// </summary>
    public string ScheduleCode { get; set; }

        /// <summary>
    /// 排程名称
    /// </summary>
    public string ScheduleName { get; set; }

        /// <summary>
    /// 排程类型
    /// </summary>
    public int ScheduleType { get; set; }

        /// <summary>
    /// 计划日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 计划周期
    /// </summary>
    public int PlanCycle { get; set; }

        /// <summary>
    /// 车间编码
    /// </summary>
    public string? WorkshopCode { get; set; }

        /// <summary>
    /// 车间名称
    /// </summary>
    public string? WorkshopName { get; set; }

        /// <summary>
    /// 生产线编码
    /// </summary>
    public string? ProductionLineCode { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 排程策略
    /// </summary>
    public int ScheduleStrategy { get; set; }

        /// <summary>
    /// 排程算法
    /// </summary>
    public int ScheduleAlgorithm { get; set; }

        /// <summary>
    /// 优化目标
    /// </summary>
    public int OptimizationObjective { get; set; }

        /// <summary>
    /// 排程状态
    /// </summary>
    public int ScheduleStatus { get; set; }

        /// <summary>
    /// 计划员ID
    /// </summary>
    public long? PlannerId { get; set; }

        /// <summary>
    /// 计划员姓名
    /// </summary>
    public string? PlannerName { get; set; }

        /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

        /// <summary>
    /// 发布人ID
    /// </summary>
    public long? PublishUserId { get; set; }

        /// <summary>
    /// 发布人姓名
    /// </summary>
    public string? PublishUserName { get; set; }

        /// <summary>
    /// 排程说明
    /// </summary>
    public string? ScheduleDescription { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}