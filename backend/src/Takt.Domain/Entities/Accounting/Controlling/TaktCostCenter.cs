// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktCostCenter.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本中心实体，定义成本中心领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// Takt成本中心实体
/// </summary>
[SugarTable("takt_accounting_controlling_cost_center", "成本中心表")]
[SugarIndex("ix_takt_accounting_controlling_cost_center_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_company_code_cost_center_code", nameof(CompanyCode), OrderByType.Asc, nameof(CostCenterCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_center_cost_center_status", nameof(CostCenterStatus), OrderByType.Asc)]
public class TaktCostCenter : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本中心编码（同一公司内唯一）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CostCenterCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本中心名称
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_name", ColumnDescription = "成本中心名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string CostCenterName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 成本中心类型（0=成本中心，1=利润中心，2=投资中心）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_type", ColumnDescription = "成本中心类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostCenterType { get; set; } = 0;

    /// <summary>
    /// 负责人（用户ID，序列化为string以避免Javascript精度问题）
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
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 成本中心状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_status", ColumnDescription = "成本中心状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostCenterStatus { get; set; } = 0;

    /// <summary>
    /// 工厂ID（关联 TaktPlant.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "plant_id", ColumnDescription = "工厂ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码（冗余，便于列表展示；来源于 TaktPlant.PlantCode）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }
}
