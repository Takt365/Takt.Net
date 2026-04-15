// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Generator
// 文件名称：TaktGenTableColumn.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt代码生成字段配置实体，参考主流代码生成器设计（RuoYi、MyBatis-Plus）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Generator;

/// <summary>
/// Takt代码生成字段配置实体
/// </summary>
[SugarTable("takt_generator_table_column", "代码生成字段配置表")]
[SugarIndex("ix_takt_generator_table_column_table_id", nameof(TableId), OrderByType.Asc)]
[SugarIndex("ix_takt_generator_table_column_database_column_name", nameof(DatabaseColumnName), OrderByType.Asc)]
[SugarIndex("ix_takt_generator_table_column_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_generator_table_column_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktGenTableColumn : TaktEntityBase
{
    /// <summary>
    /// 表ID（关联代码生成表配置，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "table_id", ColumnDescription = "表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TableId { get; set; }

    /// <summary>
    /// 数据库列名称（数据库字段名，使用下划线命名法snake_case，如：column_name、user_name）
    /// </summary>
    [SugarColumn(ColumnName = "database_column_name", ColumnDescription = "列名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string DatabaseColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 列描述（字段注释）
    /// </summary>
    [SugarColumn(ColumnName = "column_comment", ColumnDescription = "列描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ColumnComment { get; set; }

    /// <summary>
    /// 数据库数据类型（如：varchar、int、datetime、decimal等）
    /// </summary>
    [SugarColumn(ColumnName = "database_data_type", ColumnDescription = "数据类型", ColumnDataType = "nvarchar", Length = 100, IsNullable = false, DefaultValue = "nvarchar")]
   public string DatabaseDataType { get; set; } = "nvarchar";


    /// <summary>
    /// C#类型（对应C#数据类型，如：string、int、long、DateTime、decimal、bool、Guid等）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_data_type", ColumnDescription = "C#类型", ColumnDataType = "nvarchar", Length = 100, IsNullable = false, DefaultValue = "string")]
  public string CsharpDataType { get; set; } = "string";

    /// <summary>
    /// C#列名（C#属性名，首字母大写，帕斯卡命名法）
    /// </summary>
    [SugarColumn(ColumnName = "csharp_column_name", ColumnDescription = "C#列名", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
  public string CsharpColumnName { get; set; } = string.Empty;


  /// <summary>
  /// C#长度（字符串长度、数值类型的整数位数）
  /// </summary>
  [SugarColumn(ColumnName = "length", ColumnDescription = "数据长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int Length { get; set; } = 0;

  /// <summary>
  /// C#小数位数（decimal等数值类型的小数位数）
  /// </summary>
  [SugarColumn(ColumnName = "decimal_digits", ColumnDescription = "数据精度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int DecimalDigits { get; set; } = 0;
    /// <summary>
    /// 是否主键（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_pk", ColumnDescription = "是否主键", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPk { get; set; } = 0;

    /// <summary>
    /// 是否自增（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_increment", ColumnDescription = "是否自增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsIncrement { get; set; } = 0;

    /// <summary>
    /// 是否必填（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必填", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsRequired { get; set; } = 1;

    /// <summary>
    /// 是否为新增字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_create", ColumnDescription = "是否新增", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsCreate { get; set; } = 1;

    /// <summary>
    /// 是否更新字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_update", ColumnDescription = "是否更新", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsUpdate { get; set; } = 1;

    /// <summary>
    /// 是否查重字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_unique", ColumnDescription = "是否查重", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsUnique { get; set; } = 0;

    /// <summary>
    /// 是否列表字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_list", ColumnDescription = "是否列表", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsList { get; set; } = 1;

    /// <summary>
    /// 是否导出字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_export", ColumnDescription = "是否导出", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsExport { get; set; } = 1;

    /// <summary>
    /// 是否排序字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_sort", ColumnDescription = "是否排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSort { get; set; } = 0;
    /// <summary>
    /// 是否查询字段（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_query", ColumnDescription = "是否查询", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsQuery { get; set; } = 0;

    /// <summary>
    /// 查询方式（0=等于，1=不等于，2=大于，3=小于，4=模糊，5=范围）
    /// </summary>
    [SugarColumn(ColumnName = "query_type", ColumnDescription = "查询方式", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "LIKE")]
    public string QueryType { get; set; } = "LIKE";

    /// <summary>
    /// 显示类型（0=输入框，1=文本域，2=下拉框，3=复选框，4=单选框，5=日期控件，6=时间控件，7=图片上传，8=文件上传，9=滑块，10=开关，11=富文本编辑器）
    /// </summary>
    [SugarColumn(ColumnName = "html_type", ColumnDescription = "显示类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "input")]
    public string HtmlType { get; set; } = "input";

    /// <summary>
    /// 字典类型（关联数据字典）
    /// </summary>
    [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DictType { get; set; }

    /// <summary>
    /// 排序序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 所属表配置（主表，本表 TableId 关联 TaktGenTable.Id）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(TableId))]
    public TaktGenTable? Table { get; set; }
}
