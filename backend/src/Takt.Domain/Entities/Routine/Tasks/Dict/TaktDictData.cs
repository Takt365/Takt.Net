// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Dict
// 文件名称：TaktDictData.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt字典数据实体，定义数据字典数据领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================


using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.Dict;

/// <summary>
/// Takt字典数据实体
/// </summary>
[SugarTable("takt_routine_dict_data", "字典数据表")]
[SugarIndex("ix_takt_routine_dict_data_dict_type_id", nameof(DictTypeId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_data_dict_type_code_dict_label", nameof(DictTypeCode), OrderByType.Asc, nameof(DictLabel), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_dict_data_dict_type_code", nameof(DictTypeCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_data_dict_label", nameof(DictLabel), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_data_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_dict_data_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktDictData : TaktEntityBase
{
    /// <summary>
    /// 字典类型ID（外键，关联 TaktDictType.Id）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type_id", ColumnDescription = "字典类型ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典类型编码（冗余，便于查询与展示；业务键为 DictTypeId）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type_code", ColumnDescription = "字典类型编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DictTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// 字典标签（在同一个字典类型下唯一）
    /// </summary>
    [SugarColumn(ColumnName = "dict_label", ColumnDescription = "字典标签", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DictLabel { get; set; } = string.Empty;

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    [SugarColumn(ColumnName = "dict_l10n_key", ColumnDescription = "字典本地化键", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典标签（显示值）
    /// </summary>
    [SugarColumn(ColumnName = "dict_value", ColumnDescription = "字典标签", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string DictValue { get; set; } = string.Empty;

    /// <summary>
    /// CSS类名
    /// </summary>
    [SugarColumn(ColumnName = "css_class", ColumnDescription = "CSS类名", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CssClass { get; set; } = 0;

    /// <summary>
    /// 列表类名
    /// </summary>
    [SugarColumn(ColumnName = "list_class", ColumnDescription = "列表类名", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ListClass { get; set; } = 0;

    /// <summary>
    /// 扩展标签
    /// </summary>
    [SugarColumn(ColumnName = "ext_label", ColumnDescription = "扩展标签", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ExtLabel { get; set; }

    /// <summary>
    /// 扩展值
    /// </summary>
    [SugarColumn(ColumnName = "ext_value", ColumnDescription = "扩展值", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ExtValue { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
