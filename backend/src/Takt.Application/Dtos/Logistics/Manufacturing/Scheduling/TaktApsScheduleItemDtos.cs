// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleItemDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：APS排程明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;

/// <summary>
/// APS排程明细表Dto
/// </summary>
public partial class TaktApsScheduleItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemDto()
    {
        WorkOrderCode = string.Empty;
        ProductCode = string.Empty;
        ProductName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

    /// <summary>
    /// APS排程明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleItemId { get; set; } = 0;

    /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 生产工单编码
    /// </summary>
    public string WorkOrderCode { get; set; }
    /// <summary>
    /// 生产工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WorkOrderId { get; set; }
    /// <summary>
    /// 产品编码
    /// </summary>
    public string ProductCode { get; set; }
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }
    /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }
    /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }
    /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }
    /// <summary>
    /// 工序序号
    /// </summary>
    public int ProcessSequence { get; set; }
    /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal ProcessStandardST { get; set; }
    /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int ProcessStandardSTUnit { get; set; }
    /// <summary>
    /// 额外时间
    /// </summary>
    public decimal ExtraMinutes { get; set; }
    /// <summary>
    /// 计划数量
    /// </summary>
    public decimal PlanQuantity { get; set; }
    /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }
    /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }
    /// <summary>
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }
    /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }
    /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }
    /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }
    /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }
    /// <summary>
    /// 工序状态
    /// </summary>
    public int ProcessStatus { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// APS排程主表（主表）
    /// </summary>
    public TaktApsScheduleDto? Schedule { get; set; }
}

/// <summary>
/// APS排程明细表查询DTO
/// </summary>
public partial class TaktApsScheduleItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApsScheduleId { get; set; }
    /// <summary>
    /// 项号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 生产工单编码
    /// </summary>
    public string? WorkOrderCode { get; set; }
    /// <summary>
    /// 生产工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? WorkOrderId { get; set; }
    /// <summary>
    /// 产品编码
    /// </summary>
    public string? ProductCode { get; set; }
    /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }
    /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }
    /// <summary>
    /// 工序编码
    /// </summary>
    public string? ProcessCode { get; set; }
    /// <summary>
    /// 工序名称
    /// </summary>
    public string? ProcessName { get; set; }
    /// <summary>
    /// 工序序号
    /// </summary>
    public int? ProcessSequence { get; set; }
    /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal? ProcessStandardST { get; set; }
    /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int? ProcessStandardSTUnit { get; set; }
    /// <summary>
    /// 额外时间
    /// </summary>
    public decimal? ExtraMinutes { get; set; }
    /// <summary>
    /// 计划数量
    /// </summary>
    public decimal? PlanQuantity { get; set; }
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
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

    /// <summary>
    /// 实际开始时间开始时间
    /// </summary>
    public DateTime? ActualStartTimeStart { get; set; }
    /// <summary>
    /// 实际开始时间结束时间
    /// </summary>
    public DateTime? ActualStartTimeEnd { get; set; }
    /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

    /// <summary>
    /// 实际结束时间开始时间
    /// </summary>
    public DateTime? ActualEndTimeStart { get; set; }
    /// <summary>
    /// 实际结束时间结束时间
    /// </summary>
    public DateTime? ActualEndTimeEnd { get; set; }
    /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }
    /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }
    /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }
    /// <summary>
    /// 工序状态
    /// </summary>
    public int? ProcessStatus { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int? Priority { get; set; }

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
/// Takt创建APS排程明细表DTO
/// </summary>
public partial class TaktApsScheduleItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemCreateDto()
    {
        WorkOrderCode = string.Empty;
        ProductCode = string.Empty;
        ProductName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// APS排程ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 生产工单编码
    /// </summary>
    public string WorkOrderCode { get; set; }

        /// <summary>
    /// 生产工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long WorkOrderId { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }

        /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }

        /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 工序序号
    /// </summary>
    public int ProcessSequence { get; set; }

        /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal ProcessStandardST { get; set; }

        /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int ProcessStandardSTUnit { get; set; }

        /// <summary>
    /// 额外时间
    /// </summary>
    public decimal ExtraMinutes { get; set; }

        /// <summary>
    /// 计划数量
    /// </summary>
    public decimal PlanQuantity { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

        /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }

        /// <summary>
    /// 工序状态
    /// </summary>
    public int ProcessStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

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
/// Takt更新APS排程明细表DTO
/// </summary>
public partial class TaktApsScheduleItemUpdateDto : TaktApsScheduleItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemUpdateDto()
    {
    }

        /// <summary>
    /// APS排程明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleItemId { get; set; } = 0;
}

/// <summary>
/// APS排程明细表工序状态DTO
/// </summary>
public partial class TaktApsScheduleItemProcessStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemProcessStatusDto()
    {
    }

        /// <summary>
    /// APS排程明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApsScheduleItemId { get; set; } = 0;

    /// <summary>
    /// 工序状态（0=禁用，1=启用）
    /// </summary>
    public int ProcessStatus { get; set; }
}

/// <summary>
/// APS排程明细表导入模板DTO
/// </summary>
public partial class TaktApsScheduleItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemTemplateDto()
    {
        WorkOrderCode = string.Empty;
        ProductCode = string.Empty;
        ProductName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 生产工单编码
    /// </summary>
    public string WorkOrderCode { get; set; }

        /// <summary>
    /// 生产工单ID
    /// </summary>
    public long WorkOrderId { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }

        /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }

        /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 工序序号
    /// </summary>
    public int ProcessSequence { get; set; }

        /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal ProcessStandardST { get; set; }

        /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int ProcessStandardSTUnit { get; set; }

        /// <summary>
    /// 额外时间
    /// </summary>
    public decimal ExtraMinutes { get; set; }

        /// <summary>
    /// 计划数量
    /// </summary>
    public decimal PlanQuantity { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

        /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }

        /// <summary>
    /// 工序状态
    /// </summary>
    public int ProcessStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

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
/// APS排程明细表导入DTO
/// </summary>
public partial class TaktApsScheduleItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemImportDto()
    {
        WorkOrderCode = string.Empty;
        ProductCode = string.Empty;
        ProductName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 生产工单编码
    /// </summary>
    public string WorkOrderCode { get; set; }

        /// <summary>
    /// 生产工单ID
    /// </summary>
    public long WorkOrderId { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }

        /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }

        /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 工序序号
    /// </summary>
    public int ProcessSequence { get; set; }

        /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal ProcessStandardST { get; set; }

        /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int ProcessStandardSTUnit { get; set; }

        /// <summary>
    /// 额外时间
    /// </summary>
    public decimal ExtraMinutes { get; set; }

        /// <summary>
    /// 计划数量
    /// </summary>
    public decimal PlanQuantity { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

        /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }

        /// <summary>
    /// 工序状态
    /// </summary>
    public int ProcessStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

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
/// APS排程明细表导出DTO
/// </summary>
public partial class TaktApsScheduleItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktApsScheduleItemExportDto()
    {
        CreatedAt = DateTime.Now;
        WorkOrderCode = string.Empty;
        ProductCode = string.Empty;
        ProductName = string.Empty;
        ProcessCode = string.Empty;
        ProcessName = string.Empty;
    }

        /// <summary>
    /// APS排程ID
    /// </summary>
    public long ApsScheduleId { get; set; }

        /// <summary>
    /// 项号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 生产工单编码
    /// </summary>
    public string WorkOrderCode { get; set; }

        /// <summary>
    /// 生产工单ID
    /// </summary>
    public long WorkOrderId { get; set; }

        /// <summary>
    /// 产品编码
    /// </summary>
    public string ProductCode { get; set; }

        /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }

        /// <summary>
    /// 工作中心编码
    /// </summary>
    public string? WorkCenterCode { get; set; }

        /// <summary>
    /// 工作中心名称
    /// </summary>
    public string? WorkCenterName { get; set; }

        /// <summary>
    /// 工序编码
    /// </summary>
    public string ProcessCode { get; set; }

        /// <summary>
    /// 工序名称
    /// </summary>
    public string ProcessName { get; set; }

        /// <summary>
    /// 工序序号
    /// </summary>
    public int ProcessSequence { get; set; }

        /// <summary>
    /// 工序标准ST值
    /// </summary>
    public decimal ProcessStandardST { get; set; }

        /// <summary>
    /// 工序标准ST单位
    /// </summary>
    public int ProcessStandardSTUnit { get; set; }

        /// <summary>
    /// 额外时间
    /// </summary>
    public decimal ExtraMinutes { get; set; }

        /// <summary>
    /// 计划数量
    /// </summary>
    public decimal PlanQuantity { get; set; }

        /// <summary>
    /// 计划开始时间
    /// </summary>
    public DateTime PlanStartTime { get; set; }

        /// <summary>
    /// 计划结束时间
    /// </summary>
    public DateTime PlanEndTime { get; set; }

        /// <summary>
    /// 实际开始时间
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

        /// <summary>
    /// 实际结束时间
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

        /// <summary>
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }

        /// <summary>
    /// 班组编码
    /// </summary>
    public string? TeamCode { get; set; }

        /// <summary>
    /// 班组名称
    /// </summary>
    public string? TeamName { get; set; }

        /// <summary>
    /// 工序状态
    /// </summary>
    public int ProcessStatus { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}