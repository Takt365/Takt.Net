// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Region
// 文件名称：TaktRegionCountry.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：国家地区实体（行政区划第1级），定义国家/地区领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Region;

/// <summary>
/// 国家地区实体（行政区划第1级）
/// </summary>
[SugarTable("takt_routine_region_country", "国家地区表")]
[SugarIndex("ix_takt_routine_region_country_code", nameof(RegionCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_region_country_alpha3", nameof(Alpha3Code), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_country_numeric", nameof(NumericCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_country_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_region_country_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRegionCountry : TaktEntityBase
{
    /// <summary>
    /// 国家地区代码 Alpha-2（两字母）。ISO 3166-1，最常用，用于互联网域名、物流等，如 CN（中国）、US（美国）。
    /// </summary>
    [SugarColumn(ColumnName = "region_code", ColumnDescription = "国家代码Alpha-2", ColumnDataType = "nvarchar", Length = 2, IsNullable = false)]
    public string RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// 国家地区代码 Alpha-3（三字母）。ISO 3166-1，用于金融、统计等，如 CHN（中国）、USA（美国）。
    /// </summary>
    [SugarColumn(ColumnName = "alpha3_code", ColumnDescription = "国家代码Alpha-3", ColumnDataType = "nvarchar", Length = 3, IsNullable = true)]
    public string? Alpha3Code { get; set; }

    /// <summary>
    /// 国家地区数字代码（三位数字）。ISO 3166-1 numeric，用于非拉丁字母系统，如 156（中国）、840（美国）。
    /// </summary>
    [SugarColumn(ColumnName = "numeric_code", ColumnDescription = "数字代码", ColumnDataType = "int", IsNullable = true)]
    public int? NumericCode { get; set; }

    /// <summary>
    /// 全称（官方全称，如中华人民共和国、United States of America）
    /// </summary>
    [SugarColumn(ColumnName = "full_name", ColumnDescription = "全称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? FullName { get; set; }

    /// <summary>
    /// 名称（国家/地区常用名称，如中国、美国）
    /// </summary>
    [SugarColumn(ColumnName = "region_name", ColumnDescription = "名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称（国际常用英文名，如 China、United States）
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 简称（可选）
    /// </summary>
    [SugarColumn(ColumnName = "short_name", ColumnDescription = "简称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ShortName { get; set; }

    /// <summary>
    /// 洲际（所属大洲，如亚洲、北美洲、欧洲、非洲、南美洲、大洋洲、南极洲）
    /// </summary>
    [SugarColumn(ColumnName = "continent", ColumnDescription = "洲际", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Continent { get; set; }

    /// <summary>
    /// 首都（国家或地区首府名称，如北京、华盛顿）
    /// </summary>
    [SugarColumn(ColumnName = "capital", ColumnDescription = "首都", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Capital { get; set; }

    /// <summary>
    /// 地区类型（0=主权国家，1=地区/属地，2=其他）
    /// </summary>
    [SugarColumn(ColumnName = "region_type", ColumnDescription = "地区类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RegionType { get; set; } = 0;

    /// <summary>
    /// 国别顶级域（ccTLD，如 .cn、.us、.hk）
    /// </summary>
    [SugarColumn(ColumnName = "country_domain", ColumnDescription = "国别顶级域", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? CountryDomain { get; set; }

    /// <summary>
    /// 电话区号（如 +86、+1）
    /// </summary>
    [SugarColumn(ColumnName = "area_code", ColumnDescription = "电话区号", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? AreaCode { get; set; }

    /// <summary>
    /// 时区（IANA 时区标识，如 Asia/Shanghai、America/New_York）
    /// </summary>
    [SugarColumn(ColumnName = "time_zone", ColumnDescription = "时区", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TimeZone { get; set; }

    /// <summary>
    /// 货币代码（ISO 4217，如 CNY、USD、EUR）
    /// </summary>
    [SugarColumn(ColumnName = "currency_code", ColumnDescription = "货币代码", ColumnDataType = "nvarchar", Length = 6, IsNullable = true)]
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// 语言代码（如 zh-CN、en-US、ja-JP）
    /// </summary>
    [SugarColumn(ColumnName = "language_code", ColumnDescription = "语言代码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? LanguageCode { get; set; }

    /// <summary>
    /// 层级（固定为 1=国家地区）
    /// </summary>
    [SugarColumn(ColumnName = "region_level", ColumnDescription = "层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int RegionLevel { get; set; } = 1;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
