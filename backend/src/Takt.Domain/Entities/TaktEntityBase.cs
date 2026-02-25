// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities
// 文件名称：TaktEntityBase.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt实体基类，包含通用字段（ID、租户ID、扩展字段JSON、备注、创建人/创建人ID/创建时间、更新人/更新人ID/更新时间、软删除标记、删除人/删除人ID/删除时间）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities;

/// <summary>
/// Takt实体基类
/// </summary>
public abstract class TaktEntityBase
{
    /// <summary>
    /// 主键ID（雪花ID，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDescription = "主键ID", ColumnDataType = "bigint", IsPrimaryKey = true, IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long Id { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换）
    /// </summary>
    [SugarColumn(ColumnName = "config_id", ColumnDescription = "租户配置ID", ColumnDataType = "nvarchar", Length = 2, IsNullable = false, DefaultValue = "0")]
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    [SugarColumn(ColumnName = "ext_field_json", ColumnDescription = "扩展字段JSON", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [SugarColumn(ColumnName = "create_id", ColumnDescription = "创建人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新人ID
    /// </summary>
    [SugarColumn(ColumnName = "update_id", ColumnDescription = "更新人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=是（未删除），1=否（已删除））
    /// </summary>
    [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除人ID
    /// </summary>
    [SugarColumn(ColumnName = "deleted_id", ColumnDescription = "删除人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "deleted_by", ColumnDescription = "删除人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnName = "deleted_time", ColumnDescription = "删除时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DeletedTime { get; set; }
}
