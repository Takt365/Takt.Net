// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktPersonnelOperationRateDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：人员稼动率表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 人员稼动率表Dto
/// </summary>
public partial class TaktPersonnelOperationRateDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateDto()
    {
        PlantCode = string.Empty;
        ProductionLine = string.Empty;
    }

    /// <summary>
    /// 人员稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PersonnelOperationRateId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 时间类别
    /// </summary>
    public int TimeCategory { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }
    /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string ProductionLine { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }
    /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int PlannedDirectPersonnelCount { get; set; }
    /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int ActualDirectPersonnelCount { get; set; }
    /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int PlannedIndirectPersonnelCount { get; set; }
    /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int ActualIndirectPersonnelCount { get; set; }
    /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal PlannedWorkTime { get; set; }
    /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal ActualWorkTime { get; set; }
    /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal BreakTime { get; set; }
    /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal IdleTime { get; set; }
    /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal PersonnelOperationRate { get; set; }
    /// <summary>
    /// 计划产量
    /// </summary>
    public decimal PlannedOutput { get; set; }
    /// <summary>
    /// 实际产量
    /// </summary>
    public decimal ActualOutput { get; set; }
    /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal QualifiedQuantity { get; set; }
    /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal DefectiveQuantity { get; set; }
    /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal YieldRate { get; set; }
    /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal WorkEfficiency { get; set; }
    /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }
    /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }
    /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal OvertimeHours { get; set; }
    /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }
    /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 人员稼动率表查询DTO
/// </summary>
public partial class TaktPersonnelOperationRateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 人员稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PersonnelOperationRateId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 时间类别
    /// </summary>
    public int? TimeCategory { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }
    /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int? PlannedDirectPersonnelCount { get; set; }
    /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int? ActualDirectPersonnelCount { get; set; }
    /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int? PlannedIndirectPersonnelCount { get; set; }
    /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int? ActualIndirectPersonnelCount { get; set; }
    /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal? PlannedWorkTime { get; set; }
    /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal? ActualWorkTime { get; set; }
    /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal? BreakTime { get; set; }
    /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal? IdleTime { get; set; }
    /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal? PersonnelOperationRate { get; set; }
    /// <summary>
    /// 计划产量
    /// </summary>
    public decimal? PlannedOutput { get; set; }
    /// <summary>
    /// 实际产量
    /// </summary>
    public decimal? ActualOutput { get; set; }
    /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal? QualifiedQuantity { get; set; }
    /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal? DefectiveQuantity { get; set; }
    /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal? YieldRate { get; set; }
    /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal? WorkEfficiency { get; set; }
    /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }
    /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }
    /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal? OvertimeHours { get; set; }
    /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }
    /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
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
/// Takt创建人员稼动率表DTO
/// </summary>
public partial class TaktPersonnelOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateCreateDto()
    {
        PlantCode = string.Empty;
        ProductionLine = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 时间类别
    /// </summary>
    public int TimeCategory { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }

        /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int PlannedDirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int ActualDirectPersonnelCount { get; set; }

        /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int PlannedIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int ActualIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal PlannedWorkTime { get; set; }

        /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal ActualWorkTime { get; set; }

        /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal BreakTime { get; set; }

        /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal IdleTime { get; set; }

        /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal PersonnelOperationRate { get; set; }

        /// <summary>
    /// 计划产量
    /// </summary>
    public decimal PlannedOutput { get; set; }

        /// <summary>
    /// 实际产量
    /// </summary>
    public decimal ActualOutput { get; set; }

        /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal QualifiedQuantity { get; set; }

        /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal DefectiveQuantity { get; set; }

        /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal YieldRate { get; set; }

        /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal WorkEfficiency { get; set; }

        /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }

        /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }

        /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal OvertimeHours { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

        /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新人员稼动率表DTO
/// </summary>
public partial class TaktPersonnelOperationRateUpdateDto : TaktPersonnelOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateUpdateDto()
    {
    }

        /// <summary>
    /// 人员稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PersonnelOperationRateId { get; set; }
}

/// <summary>
/// 人员稼动率表状态DTO
/// </summary>
public partial class TaktPersonnelOperationRateStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateStatusDto()
    {
    }

        /// <summary>
    /// 人员稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PersonnelOperationRateId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 人员稼动率表导入模板DTO
/// </summary>
public partial class TaktPersonnelOperationRateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateTemplateDto()
    {
        PlantCode = string.Empty;
        ProductionLine = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 时间类别
    /// </summary>
    public int TimeCategory { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }

        /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int PlannedDirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int ActualDirectPersonnelCount { get; set; }

        /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int PlannedIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int ActualIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal PlannedWorkTime { get; set; }

        /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal ActualWorkTime { get; set; }

        /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal BreakTime { get; set; }

        /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal IdleTime { get; set; }

        /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal PersonnelOperationRate { get; set; }

        /// <summary>
    /// 计划产量
    /// </summary>
    public decimal PlannedOutput { get; set; }

        /// <summary>
    /// 实际产量
    /// </summary>
    public decimal ActualOutput { get; set; }

        /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal QualifiedQuantity { get; set; }

        /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal DefectiveQuantity { get; set; }

        /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal YieldRate { get; set; }

        /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal WorkEfficiency { get; set; }

        /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }

        /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }

        /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal OvertimeHours { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

        /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 人员稼动率表导入DTO
/// </summary>
public partial class TaktPersonnelOperationRateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateImportDto()
    {
        PlantCode = string.Empty;
        ProductionLine = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 时间类别
    /// </summary>
    public int TimeCategory { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }

        /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int PlannedDirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int ActualDirectPersonnelCount { get; set; }

        /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int PlannedIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int ActualIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal PlannedWorkTime { get; set; }

        /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal ActualWorkTime { get; set; }

        /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal BreakTime { get; set; }

        /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal IdleTime { get; set; }

        /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal PersonnelOperationRate { get; set; }

        /// <summary>
    /// 计划产量
    /// </summary>
    public decimal PlannedOutput { get; set; }

        /// <summary>
    /// 实际产量
    /// </summary>
    public decimal ActualOutput { get; set; }

        /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal QualifiedQuantity { get; set; }

        /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal DefectiveQuantity { get; set; }

        /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal YieldRate { get; set; }

        /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal WorkEfficiency { get; set; }

        /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }

        /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }

        /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal OvertimeHours { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

        /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 人员稼动率表导出DTO
/// </summary>
public partial class TaktPersonnelOperationRateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPersonnelOperationRateExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        ProductionLine = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 时间类别
    /// </summary>
    public int TimeCategory { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 周数
    /// </summary>
    public int? WeekNumber { get; set; }

        /// <summary>
    /// 月份
    /// </summary>
    public int? MonthNumber { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProductionLine { get; set; }

        /// <summary>
    /// 生产线名称
    /// </summary>
    public string? ProductionLineName { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 计划直接人员数量
    /// </summary>
    public int PlannedDirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际直接人员数量
    /// </summary>
    public int ActualDirectPersonnelCount { get; set; }

        /// <summary>
    /// 计划间接人员数量
    /// </summary>
    public int PlannedIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 实际间接人员数量
    /// </summary>
    public int ActualIndirectPersonnelCount { get; set; }

        /// <summary>
    /// 出勤时间(分钟)
    /// </summary>
    public decimal PlannedWorkTime { get; set; }

        /// <summary>
    /// 在岗作业时间(分钟)
    /// </summary>
    public decimal ActualWorkTime { get; set; }

        /// <summary>
    /// 休息时间(分钟)
    /// </summary>
    public decimal BreakTime { get; set; }

        /// <summary>
    /// 空闲时间(分钟)
    /// </summary>
    public decimal IdleTime { get; set; }

        /// <summary>
    /// 人员稼动率(%)
    /// </summary>
    public decimal PersonnelOperationRate { get; set; }

        /// <summary>
    /// 计划产量
    /// </summary>
    public decimal PlannedOutput { get; set; }

        /// <summary>
    /// 实际产量
    /// </summary>
    public decimal ActualOutput { get; set; }

        /// <summary>
    /// 合格品数量
    /// </summary>
    public decimal QualifiedQuantity { get; set; }

        /// <summary>
    /// 不良品数量
    /// </summary>
    public decimal DefectiveQuantity { get; set; }

        /// <summary>
    /// 良品率(%)
    /// </summary>
    public decimal YieldRate { get; set; }

        /// <summary>
    /// 工作效率(%)
    /// </summary>
    public decimal WorkEfficiency { get; set; }

        /// <summary>
    /// 空闲原因类型
    /// </summary>
    public int? IdleReasonType { get; set; }

        /// <summary>
    /// 空闲原因描述
    /// </summary>
    public string? IdleReason { get; set; }

        /// <summary>
    /// 加班时间(分钟)
    /// </summary>
    public decimal OvertimeHours { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

        /// <summary>
    /// 主管
    /// </summary>
    public string? Supervisor { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}