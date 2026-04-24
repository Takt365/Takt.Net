// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Maintenance
// 文件名称：TaktEquipment.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂设备实体，定义工厂设备领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Maintenance;

/// <summary>
/// Takt工厂设备实体
/// </summary>
[SugarTable("takt_logistics_maintenance_equipment", "工厂设备表")]
[SugarIndex("ix_takt_logistics_maintenance_equipment_equipment_code", nameof(EquipmentCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_maintenance_equipment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_maintenance_equipment_equipment_status", nameof(EquipmentStatus), OrderByType.Asc)]
public class TaktEquipment : TaktEntityBase
{
    /// <summary>
    /// 设备编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string EquipmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string EquipmentName { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型（0=生产设备，1=检测设备，2=辅助设备，3=办公设备，4=其他设备）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_type", ColumnDescription = "设备类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EquipmentType { get; set; } = 0;

    /// <summary>
    /// 设备型号
    /// </summary>
    [SugarColumn(ColumnName = "equipment_model", ColumnDescription = "设备型号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EquipmentModel { get; set; }

    /// <summary>
    /// 设备规格
    /// </summary>
    [SugarColumn(ColumnName = "equipment_specification", ColumnDescription = "设备规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? EquipmentSpecification { get; set; }

    /// <summary>
    /// 设备品牌
    /// </summary>
    [SugarColumn(ColumnName = "equipment_brand", ColumnDescription = "设备品牌", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EquipmentBrand { get; set; }

    /// <summary>
    /// 制造商
    /// </summary>
    [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "制造商", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// 经销商
    /// </summary>
    [SugarColumn(ColumnName = "dealer_by", ColumnDescription = "经销商", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? DealerBy { get; set; }

    /// <summary>
    /// 序列号/出厂编号
    /// </summary>
    [SugarColumn(ColumnName = "serial_number", ColumnDescription = "序列号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// 所属车间
    /// </summary>
    [SugarColumn(ColumnName = "workshop_by", ColumnDescription = "所属车间", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? WorkshopBy { get; set; }

    /// <summary>
    /// 所属产线
    /// </summary>
    [SugarColumn(ColumnName = "production_line_by", ColumnDescription = "所属产线", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ProductionLineBy { get; set; }

    /// <summary>
    /// 所属工位
    /// </summary>
    [SugarColumn(ColumnName = "workstation_by", ColumnDescription = "所属工位", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? WorkstationBy { get; set; }

    /// <summary>
    /// 所属部门
    /// </summary>
    [SugarColumn(ColumnName = "dept_by", ColumnDescription = "所属部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptBy { get; set; }

    /// <summary>
    /// 设备位置（详细位置描述）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_location", ColumnDescription = "设备位置", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? EquipmentLocation { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "responsible_user_by", ColumnDescription = "负责人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ResponsibleUserBy { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    [SugarColumn(ColumnName = "operator_by", ColumnDescription = "操作人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OperatorBy { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    [SugarColumn(ColumnName = "purchase_date", ColumnDescription = "购买日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 安装日期
    /// </summary>
    [SugarColumn(ColumnName = "installation_date", ColumnDescription = "安装日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? InstallationDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "启用日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 保修开始日期
    /// </summary>
    [SugarColumn(ColumnName = "warranty_start_date", ColumnDescription = "保修开始日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? WarrantyStartDate { get; set; }

    /// <summary>
    /// 保修结束日期
    /// </summary>
    [SugarColumn(ColumnName = "warranty_end_date", ColumnDescription = "保修结束日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? WarrantyEndDate { get; set; }

    /// <summary>
    /// 设备原值（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_original_value", ColumnDescription = "设备原值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal EquipmentOriginalValue { get; set; } = 0;

    /// <summary>
    /// 设备技术参数（JSON格式，存储设备技术参数配置）
    /// </summary>
    [SugarColumn(ColumnName = "technical_parameters", ColumnDescription = "设备技术参数", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? TechnicalParameters { get; set; }

    /// <summary>
    /// 设备图片（JSON格式，存储设备图片URL列表）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_images", ColumnDescription = "设备图片", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? EquipmentImages { get; set; }

    /// <summary>
    /// 设备文档（JSON格式，存储设备文档ID列表）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_documents", ColumnDescription = "设备文档", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? EquipmentDocuments { get; set; }

    /// <summary>
    /// 是否关键设备（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_critical", ColumnDescription = "是否关键设备", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCritical { get; set; } = 0;

    /// <summary>
    /// 保修状态（0=无保修，1=保修期内，2=保修期外，3=延保中）
    /// </summary>
    [SugarColumn(ColumnName = "warranty_status", ColumnDescription = "保修状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WarrantyStatus { get; set; } = 0;

    /// <summary>
    /// 设备状态（0=运行中，1=停机，2=维修中，3=故障，4=待报废，5=已报废）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EquipmentStatus { get; set; } = 0;

    /// <summary>
    /// 维护记录列表（外键：子表 TaktMaintenance.EquipmentId 关联本表 Id）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktMaintenance.EquipmentId))]
    public List<TaktMaintenance>? MaintenanceRecords { get; set; }
}
