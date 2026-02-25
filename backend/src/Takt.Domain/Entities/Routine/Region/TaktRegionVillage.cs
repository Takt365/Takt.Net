// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Region
// 文件名称：TaktRegionVillage.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：村社区实体（行政区划第6级），定义村/社区/居委会等领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Region;

/// <summary>
/// 村社区实体（行政区划第6级）
/// </summary>
[SugarTable("takt_routine_region_village", "村社区表")]
[SugarIndex("ix_takt_routine_region_village_township_id", nameof(TownshipId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_village_code", nameof(RegionCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_village_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_village_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRegionVillage : TaktEntityBase
{
    /// <summary>
    /// 所属乡镇ID（关联乡镇表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "township_id", ColumnDescription = "所属乡镇ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TownshipId { get; set; }

    /// <summary>
    /// 行政区划代码（村/社区代码，如国标）
    /// </summary>
    [SugarColumn(ColumnName = "region_code", ColumnDescription = "行政区划代码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 名称（村/社区/居委会名称）
    /// </summary>
    [SugarColumn(ColumnName = "region_name", ColumnDescription = "名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 简称（可选）
    /// </summary>
    [SugarColumn(ColumnName = "short_name", ColumnDescription = "简称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ShortName { get; set; }

    /// <summary>
    /// 层级（固定为 6=村社区）
    /// </summary>
    [SugarColumn(ColumnName = "region_level", ColumnDescription = "层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "6")]
    public int RegionLevel { get; set; } = 6;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
