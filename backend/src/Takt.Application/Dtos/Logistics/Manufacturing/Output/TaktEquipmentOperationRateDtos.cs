// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRateDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：机器稼动率表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 机器稼动率表Dto
/// </summary>
public partial class TaktEquipmentOperationRateDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateDto()
    {
        PlantCode = string.Empty;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
    }

    /// <summary>
    /// 机器稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentOperationRateId { get; set; }

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
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }
    /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal PlannedRuntime { get; set; }
    /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal ActualRuntime { get; set; }
    /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal Downtime { get; set; }
    /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal EquipmentOperationRate { get; set; }
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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }
    /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }
    /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }
    /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }
    /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 机器稼动率表查询DTO
/// </summary>
public partial class TaktEquipmentOperationRateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 机器稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentOperationRateId { get; set; }

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
    /// 设备编码
    /// </summary>
    public string? EquipmentCode { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string? EquipmentName { get; set; }
    /// <summary>
    /// 设备类型
    /// </summary>
    public int? EquipmentType { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal? PlannedRuntime { get; set; }
    /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal? ActualRuntime { get; set; }
    /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal? Downtime { get; set; }
    /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal? EquipmentOperationRate { get; set; }
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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }
    /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }
    /// <summary>
    /// 设备状态
    /// </summary>
    public int? EquipmentStatus { get; set; }
    /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }
    /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }
    /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }
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
/// Takt创建机器稼动率表DTO
/// </summary>
public partial class TaktEquipmentOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateCreateDto()
    {
        PlantCode = string.Empty;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
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
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal PlannedRuntime { get; set; }

        /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal ActualRuntime { get; set; }

        /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal Downtime { get; set; }

        /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal EquipmentOperationRate { get; set; }

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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }

        /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

        /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }

        /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

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
/// Takt更新机器稼动率表DTO
/// </summary>
public partial class TaktEquipmentOperationRateUpdateDto : TaktEquipmentOperationRateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateUpdateDto()
    {
    }

        /// <summary>
    /// 机器稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentOperationRateId { get; set; }
}

/// <summary>
/// 机器稼动率表设备状态DTO
/// </summary>
public partial class TaktEquipmentOperationRateEquipmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateEquipmentStatusDto()
    {
    }

        /// <summary>
    /// 机器稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentOperationRateId { get; set; }

    /// <summary>
    /// 设备状态（0=禁用，1=启用）
    /// </summary>
    public int EquipmentStatus { get; set; }
}

/// <summary>
/// 机器稼动率表状态DTO
/// </summary>
public partial class TaktEquipmentOperationRateStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateStatusDto()
    {
    }

        /// <summary>
    /// 机器稼动率表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EquipmentOperationRateId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 机器稼动率表导入模板DTO
/// </summary>
public partial class TaktEquipmentOperationRateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateTemplateDto()
    {
        PlantCode = string.Empty;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
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
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal PlannedRuntime { get; set; }

        /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal ActualRuntime { get; set; }

        /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal Downtime { get; set; }

        /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal EquipmentOperationRate { get; set; }

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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }

        /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

        /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }

        /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

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
/// 机器稼动率表导入DTO
/// </summary>
public partial class TaktEquipmentOperationRateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateImportDto()
    {
        PlantCode = string.Empty;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
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
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal PlannedRuntime { get; set; }

        /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal ActualRuntime { get; set; }

        /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal Downtime { get; set; }

        /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal EquipmentOperationRate { get; set; }

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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }

        /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

        /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }

        /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

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
/// 机器稼动率表导出DTO
/// </summary>
public partial class TaktEquipmentOperationRateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRateExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        EquipmentCode = string.Empty;
        EquipmentName = string.Empty;
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
    /// 设备编码
    /// </summary>
    public string EquipmentCode { get; set; }

        /// <summary>
    /// 设备名称
    /// </summary>
    public string EquipmentName { get; set; }

        /// <summary>
    /// 设备类型
    /// </summary>
    public int EquipmentType { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProductionLine { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 负荷时间(分钟)
    /// </summary>
    public decimal PlannedRuntime { get; set; }

        /// <summary>
    /// 稼动时间(分钟)
    /// </summary>
    public decimal ActualRuntime { get; set; }

        /// <summary>
    /// 停线损失时间(分钟)
    /// </summary>
    public decimal Downtime { get; set; }

        /// <summary>
    /// 时间稼动率(%)
    /// </summary>
    public decimal EquipmentOperationRate { get; set; }

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
    /// 停机原因类型
    /// </summary>
    public int? DowntimeReasonType { get; set; }

        /// <summary>
    /// 停机原因描述
    /// </summary>
    public string? DowntimeReason { get; set; }

        /// <summary>
    /// 设备状态
    /// </summary>
    public int EquipmentStatus { get; set; }

        /// <summary>
    /// 设备操作员
    /// </summary>
    public string? EquipmentOperator { get; set; }

        /// <summary>
    /// 设备维护员
    /// </summary>
    public string? EquipmentMaintainer { get; set; }

        /// <summary>
    /// 班组长
    /// </summary>
    public string? TeamLeader { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}