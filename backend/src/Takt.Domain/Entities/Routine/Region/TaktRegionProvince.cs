// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Region
// 文件名称：TaktRegionProvince.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：州省实体（行政区划第2级），定义省/州/自治区等领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Region;

/// <summary>
/// 州省实体（行政区划第2级）
/// </summary>
[SugarTable("takt_routine_region_province", "州省表")]
[SugarIndex("ix_takt_routine_region_province_country_id", nameof(CountryId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_province_code", nameof(RegionCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_province_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_province_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRegionProvince : TaktEntityBase
{
    /// <summary>
    /// 所属国家地区ID（关联国家地区表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "country_id", ColumnDescription = "所属国家地区ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CountryId { get; set; }

    /// <summary>
    /// 行政区划代码（省/州级代码，如国标）
    /// </summary>
    [SugarColumn(ColumnName = "region_code", ColumnDescription = "行政区划代码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 名称（省/州/自治区名称）
    /// </summary>
    [SugarColumn(ColumnName = "region_name", ColumnDescription = "名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 简称（可选）
    /// </summary>
    [SugarColumn(ColumnName = "short_name", ColumnDescription = "简称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ShortName { get; set; }

    /// <summary>
    /// 层级（固定为 2=州省）
    /// </summary>
    [SugarColumn(ColumnName = "region_level", ColumnDescription = "层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int RegionLevel { get; set; } = 2;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
