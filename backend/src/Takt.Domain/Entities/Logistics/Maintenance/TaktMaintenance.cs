// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Maintenance
// 文件名称：TaktMaintenance.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt设备维护记录实体（TaktEquipment的子表），定义设备维护领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Maintenance;

/// <summary>
/// Takt设备维护记录实体（TaktEquipment的子表）
/// </summary>
[SugarTable("takt_logistics_maintenance_record", "设备维护记录表")]
[SugarIndex("ix_takt_logistics_maintenance_equipment_id", nameof(EquipmentId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_maintenance_maintenance_type", nameof(MaintenanceType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_maintenance_maintenance_date", nameof(MaintenanceDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_maintenance_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMaintenance : TaktEntityBase
{
    /// <summary>
    /// 设备ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_id", ColumnDescription = "设备ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EquipmentId { get; set; }

    /// <summary>
    /// 维护类型（0=定期保养，1=故障维修，2=大修，3=改造升级，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_type", ColumnDescription = "维护类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaintenanceType { get; set; } = 0;

    /// <summary>
    /// 维护单位
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_company", ColumnDescription = "维护单位", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaintenanceCompany { get; set; }

    /// <summary>
    /// 维护技师
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_technician", ColumnDescription = "维护技师", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? MaintenanceTechnician { get; set; }

    /// <summary>
    /// 维护日期
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_date", ColumnDescription = "维护日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime MaintenanceDate { get; set; }

    /// <summary>
    /// 维护开始时间
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_start_time", ColumnDescription = "维护开始时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? MaintenanceStartTime { get; set; }

    /// <summary>
    /// 维护结束时间
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_end_time", ColumnDescription = "维护结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? MaintenanceEndTime { get; set; }

    /// <summary>
    /// 维护内容描述
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_content", ColumnDescription = "维护内容", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MaintenanceContent { get; set; }

    /// <summary>
    /// 故障描述
    /// </summary>
    [SugarColumn(ColumnName = "fault_description", ColumnDescription = "故障描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? FaultDescription { get; set; }

    /// <summary>
    /// 处理方案
    /// </summary>
    [SugarColumn(ColumnName = "solution", ColumnDescription = "处理方案", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Solution { get; set; }

    /// <summary>
    /// 使用配件（JSON格式，存储使用的配件列表）
    /// </summary>
    [SugarColumn(ColumnName = "used_parts", ColumnDescription = "使用配件", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? UsedParts { get; set; }

    /// <summary>
    /// 维护费用（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_cost", ColumnDescription = "维护费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MaintenanceCost { get; set; } = 0;

    /// <summary>
    /// 维护结果（0=正常，1=待观察，2=需再次维修，3=已报废）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_result", ColumnDescription = "维护结果", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaintenanceResult { get; set; } = 0;

    /// <summary>
    /// 维护状态（0=待执行，1=执行中，2=已完成，3=已取消）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_status", ColumnDescription = "维护状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaintenanceStatus { get; set; } = 0;

    /// <summary>
    /// 下次维护日期
    /// </summary>
    [SugarColumn(ColumnName = "next_maintenance_date", ColumnDescription = "下次维护日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? NextMaintenanceDate { get; set; }

    /// <summary>
    /// 维护周期（天）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_cycle_days", ColumnDescription = "维护周期（天）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaintenanceCycleDays { get; set; } = 0;

    /// <summary>
    /// 维护文档（JSON格式，存储维护文档ID列表）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_documents", ColumnDescription = "维护文档", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MaintenanceDocuments { get; set; }

    /// <summary>
    /// 维护图片（JSON格式，存储维护图片URL列表）
    /// </summary>
    [SugarColumn(ColumnName = "maintenance_images", ColumnDescription = "维护图片", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MaintenanceImages { get; set; }

    /// <summary>
    /// 验收总结
    /// </summary>
    [SugarColumn(ColumnName = "accepted_summary", ColumnDescription = "验收总结", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? AcceptedSummary { get; set; }

    /// <summary>
    /// 验收人
    /// </summary>
    [SugarColumn(ColumnName = "accepted_by", ColumnDescription = "验收人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? AcceptedBy { get; set; }

    /// <summary>
    /// 验收时间
    /// </summary>
    [SugarColumn(ColumnName = "accepted_at", ColumnDescription = "验收时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? AcceptedAt { get; set; }
}
