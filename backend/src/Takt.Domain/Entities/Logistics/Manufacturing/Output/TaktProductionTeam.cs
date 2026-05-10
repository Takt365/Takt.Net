#nullable enable
// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeam.cs
// 创建时间：2026-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：生产班组实体，用于替代 prod_team_category 字典管理生产班组主数据
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// 生产班组实体（生产线班组主数据）
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_production_team", "生产班组表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_team_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_team_team_code", nameof(TeamCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_team_team_unique", nameof(PlantCode), OrderByType.Asc, nameof(TeamCode), OrderByType.Asc, nameof(TeamName), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_team_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_team_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductionTeam : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 班组编码（唯一标识，例如：1、1SMT1、1SMT2、2自插A 等）
    /// </summary>
    [SugarColumn(ColumnName = "team_code", ColumnDescription = "班组编码", ColumnDataType = "nvarchar", Length = 32, IsNullable = false)]
    public string TeamCode { get; set; } = string.Empty;

    /// <summary>
    /// 班组名称（显示名称，如：SMT一班、手插二班等）
    /// </summary>
    [SugarColumn(ColumnName = "team_name", ColumnDescription = "班组名称", ColumnDataType = "nvarchar", Length = 64, IsNullable = false)]
    public string TeamName { get; set; } = string.Empty;

    /// <summary>
    /// 班组分类编码（M=组立，P=PCBA，S=SMT，Q=质检，O=其他）
    /// </summary>
    [SugarColumn(ColumnName = "team_category", ColumnDescription = "班组分类编码", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? TeamCategory { get; set; }

    /// <summary>
    /// 班组分类名称（如：组立、PCBA、SMT、质检等）
    /// </summary>
    [SugarColumn(ColumnName = "team_category_name", ColumnDescription = "班组分类名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TeamCategoryName { get; set; }

    /// <summary>
    /// 生产线代码（如：SMT1、ASSY1 等，与 TeamCode 区分，TeamCode 可包含班组信息）
    /// </summary>
    [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线代码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ProductionLine { get; set; }

    /// <summary>
    /// 班组长员工Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    [SugarColumn(ColumnName = "team_leader_id", ColumnDescription = "班组长员工Id", ColumnDataType = "bigint", IsNullable = true)]
    public long? TeamLeaderId { get; set; }

    /// <summary>
    /// 班组长姓名
    /// </summary>
    [SugarColumn(ColumnName = "team_leader_name", ColumnDescription = "班组长姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TeamLeaderName { get; set; }

    /// <summary>
    /// 班次（1=早班，2=中班，3=晚班）
    /// </summary>
    [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = true)]
    public int? ShiftNo { get; set; }

    /// <summary>
    /// 启用状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "启用状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;

}

