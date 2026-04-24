// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Dict
// 文件名称：TaktDictType.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典类型实体，定义数据字典类型领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.Dict;

/// <summary>
/// Takt字典类型实体
/// </summary>
[SugarTable("takt_routine_dict_type", "字典类型表")]
[SugarIndex("ix_takt_routine_dict_type_dict_type_code", nameof(DictTypeCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_dict_type_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_type_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_type_dict_type_status", nameof(DictTypeStatus), OrderByType.Asc)]
public class TaktDictType : TaktEntityBase
{
    /// <summary>
    /// 字典类型编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type_code", ColumnDescription = "字典类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典类型名称
    /// </summary>
    [SugarColumn(ColumnName = "dict_type_name", ColumnDescription = "类型名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DictTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 数据源（0=系统表，1=SQL查询）
    /// </summary>
    [SugarColumn(ColumnName = "data_source", ColumnDescription = "数据源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DataSource { get; set; } = 0;

    /// <summary>
    /// 数据库配置ID（当数据源为SQL查询时，指定在哪个数据库连接上执行SQL脚本）
    /// </summary>
    [SugarColumn(ColumnName = "data_config_id", ColumnDescription = "数据库配置ID", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "0")]
    public string DataConfigId { get; set; } = "0";

    /// <summary>
    /// SQL脚本（当数据源为SQL查询时使用）
    /// </summary>
    [SugarColumn(ColumnName = "sql_script", ColumnDescription = "SQL脚本", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? SqlScript { get; set; }

    /// <summary>
    /// 是否内置（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_built_in", ColumnDescription = "是否内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBuiltIn { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 类型状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type_status", ColumnDescription = "类型状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DictTypeStatus { get; set; } = 0;

    /// <summary>
    /// 字典数据列表（外键：子表 TaktDictData.DictTypeId 关联本表 Id）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktDictData.DictTypeId))]
    public List<TaktDictData>? DictDataList { get; set; }
}
