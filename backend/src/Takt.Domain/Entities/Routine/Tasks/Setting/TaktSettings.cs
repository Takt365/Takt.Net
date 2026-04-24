// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Setting
// 文件名称：TaktSettings.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt系统设置实体，定义系统配置设置领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.Setting;

/// <summary>
/// Takt系统设置实体
/// </summary>
[SugarTable("takt_routine_setting", "系统设置表")]
[SugarIndex("ix_takt_routine_setting_setting_key", nameof(SettingKey), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_setting_setting_group", nameof(SettingGroup), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_setting_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_setting_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_setting_setting_status", nameof(SettingStatus), OrderByType.Asc)]
public class TaktSettings : TaktEntityBase
{
    /// <summary>
    /// 设置键（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "setting_key", ColumnDescription = "设置键", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string SettingKey { get; set; } = string.Empty;

    /// <summary>
    /// 设置值
    /// </summary>
    [SugarColumn(ColumnName = "setting_value", ColumnDescription = "设置值", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? SettingValue { get; set; }

    /// <summary>
    /// 设置名称（描述）
    /// </summary>
    [SugarColumn(ColumnName = "setting_name", ColumnDescription = "设置名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? SettingName { get; set; }

    /// <summary>
    /// 设置分组（backend=后端，frontend=前端）
    /// </summary>
    [SugarColumn(ColumnName = "setting_group", ColumnDescription = "设置分组", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SettingGroup { get; set; }

    /// <summary>
    /// 是否内置（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_built_in", ColumnDescription = "是否内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBuiltIn { get; set; } = 0;

    /// <summary>
    /// 是否加密（1=是，0=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_encrypted", ColumnDescription = "是否加密", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsEncrypted { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 设置状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "setting_status", ColumnDescription = "设置状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SettingStatus { get; set; } = 0;
}
