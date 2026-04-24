// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities
// 文件名称：TaktEntityBase.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt实体基类，包含通用字段（ID、租户ID、扩展字段JSON、备注、CreatedById/CreatedBy/CreatedAt、UpdatedById/UpdatedBy/UpdatedAt、软删除、DeletedById/DeletedBy/DeletedAt）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Constants;

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
    /// 分库配置 ID（与 appsettings 中 dbConfigs[*].ConfigId 一致，标识本行数据所属物理库/分片；与 SqlSugar 多库路由及仓储写入逻辑一致，不等同于仅表示「当前登录租户」的默认连接）。
    /// </summary>
    [SugarColumn(ColumnName = "config_id", ColumnDescription = "分库ConfigId（与dbConfigs一致）", ColumnDataType = "nvarchar", Length = 2, IsNullable = false, DefaultValue = "0")]
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
    /// 创建人ID（非空，序列化为string以避免Javascript精度问题；无当前用户时仓储填 999）
    /// </summary>
    [SugarColumn(ColumnName = "created_id", ColumnDescription = "创建人ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CreatedById { get; set; }

    /// <summary>
    /// 创建人（用户名，非空；种子等无当前用户时仓储填 <see cref="TaktAppConstants.InitUserName"/>）
    /// </summary>
    [SugarColumn(ColumnName = "created_by", ColumnDescription = "创建人", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间（非空）
    /// </summary>
    [SugarColumn(ColumnName = "created_at", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "updated_id", ColumnDescription = "更新人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? UpdatedById { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "updated_by", ColumnDescription = "更新人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnName = "updated_at", ColumnDescription = "更新时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "deleted_id", ColumnDescription = "删除人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeletedById { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "deleted_by", ColumnDescription = "删除人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnName = "deleted_at", ColumnDescription = "删除时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DeletedAt { get; set; }
}
