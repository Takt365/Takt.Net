// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktProfitCenter.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt利润中心实体，定义利润中心领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// Takt利润中心实体
/// </summary>
[SugarTable("takt_accounting_controlling_profit_center", "利润中心表")]
[SugarIndex("ix_takt_accounting_controlling_profit_center_profit_center_code", nameof(ProfitCenterCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_profit_center_status", nameof(ProfitCenterStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_valid", nameof(ValidFrom), OrderByType.Asc, nameof(ValidTo), OrderByType.Asc)]
public class TaktProfitCenter : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 利润中心编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_code", ColumnDescription = "利润中心编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ProfitCenterCode { get; set; } = string.Empty;

    /// <summary>
    /// 利润中心名称
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_name", ColumnDescription = "利润中心名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ProfitCenterName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 负责人ID（用户ID，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "manager_id", ColumnDescription = "负责人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ManagerId { get; set; }

    /// <summary>
    /// 负责人姓名
    /// </summary>
    [SugarColumn(ColumnName = "manager_name", ColumnDescription = "负责人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ManagerName { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "所属部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "所属部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 利润中心层级（从1开始）
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_level", ColumnDescription = "利润中心层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ProfitCenterLevel { get; set; } = 1;

    /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }

    /// <summary>
    /// 利润中心状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_status", ColumnDescription = "利润中心状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ProfitCenterStatus { get; set; } = 0;

    /// <summary>
    /// 生效日期（含当日，默认当前日期）
    /// </summary>
    [SugarColumn(ColumnName = "valid_from", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ValidFrom { get; set; } = DateTime.Today;

    /// <summary>
    /// 失效日期（含当日，默认 9999-12-31 表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "valid_to", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ValidTo { get; set; } = new DateTime(9999, 12, 31);

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;
}
