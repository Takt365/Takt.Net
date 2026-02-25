// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Vehicle
// 文件名称：TaktVehicle.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：车辆实体，定义车辆领域模型（车牌、品牌型号、类型、状态等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Vehicle;

/// <summary>
/// 车辆实体（车辆主数据/档案）
/// </summary>
/// <remarks>用于公车档案管理；车牌号唯一。日常 OA 用车流程使用 TaktVehicleApply（用车申请）走工作流审批，审批通过后分配本表车辆。</remarks>
[SugarTable("takt_routine_vehicle", "车辆表")]
[SugarIndex("ix_takt_routine_vehicle_plate_number", nameof(PlateNumber), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_vehicle_vehicle_status", nameof(VehicleStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_vehicle_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktVehicle : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码（关联工厂主数据 TaktPlant.PlantCode；冗余便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 车牌号（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "plate_number", ColumnDescription = "车牌号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string PlateNumber { get; set; } = string.Empty;

    /// <summary>
    /// 品牌型号（如：丰田凯美瑞、大众帕萨特）
    /// </summary>
    [SugarColumn(ColumnName = "brand_model", ColumnDescription = "品牌型号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? BrandModel { get; set; }

    /// <summary>
    /// 车辆类型（如：轿车、SUV、商务车；可关联字典）
    /// </summary>
    [SugarColumn(ColumnName = "vehicle_type", ColumnDescription = "车辆类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? VehicleType { get; set; }

    /// <summary>
    /// 车辆状态（0=可用，1=使用中，2=维修中，3=停用）
    /// </summary>
    [SugarColumn(ColumnName = "vehicle_status", ColumnDescription = "车辆状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int VehicleStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
