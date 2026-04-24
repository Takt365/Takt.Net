// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktAsset.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt资产实体，定义资产领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// Takt资产实体
/// </summary>
[SugarTable("takt_accounting_financial_asset", "固定资产表")]
[SugarIndex("ix_takt_accounting_financial_asset_asset_code", nameof(AssetCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_asset_asset_category_id", nameof(AssetCategoryId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_asset_cost_center_id", nameof(CostCenterId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_asset_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_asset_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_asset_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_asset_asset_status", nameof(AssetStatus), OrderByType.Asc)]
public class TaktAsset : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 资产编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "asset_code", ColumnDescription = "资产编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    [SugarColumn(ColumnName = "asset_name", ColumnDescription = "资产名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "asset_category_id", ColumnDescription = "资产类别ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类别名称
    /// </summary>
    [SugarColumn(ColumnName = "asset_category_name", ColumnDescription = "资产类别名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型（0=固定资产，1=无形资产，2=流动资产，3=长期投资）
    /// </summary>
    [SugarColumn(ColumnName = "asset_type", ColumnDescription = "资产类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AssetType { get; set; } = 0;

    /// <summary>
    /// 资产原值（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "asset_original_value", ColumnDescription = "资产原值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssetOriginalValue { get; set; } = 0;

    /// <summary>
    /// 资产净值（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "asset_net_value", ColumnDescription = "资产净值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssetNetValue { get; set; } = 0;

    /// <summary>
    /// 累计折旧（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "accumulated_depreciation", ColumnDescription = "累计折旧", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AccumulatedDepreciation { get; set; } = 0;

    /// <summary>
    /// 成本中心ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_id", ColumnDescription = "成本中心ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? CostCenterId { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_name", ColumnDescription = "成本中心名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CostCenterName { get; set; }

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
    /// 使用人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "使用人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 使用人姓名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "使用人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? UserName { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    [SugarColumn(ColumnName = "asset_location", ColumnDescription = "资产位置", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    [SugarColumn(ColumnName = "purchase_date", ColumnDescription = "购买日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "启用日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 报废日期
    /// </summary>
    [SugarColumn(ColumnName = "scrap_date", ColumnDescription = "报废日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ScrapDate { get; set; }

    /// <summary>
    /// 处置日期
    /// </summary>
    [SugarColumn(ColumnName = "disposal_date", ColumnDescription = "处置日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DisposalDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    [SugarColumn(ColumnName = "expected_life_months", ColumnDescription = "预计使用年限（月）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExpectedLifeMonths { get; set; } = 0;

    /// <summary>
    /// 折旧方法（0=直线法，1=年数总和法，2=双倍余额递减法，3=工作量法）
    /// </summary>
    [SugarColumn(ColumnName = "depreciation_method", ColumnDescription = "折旧方法", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DepreciationMethod { get; set; } = 0;

    /// <summary>
    /// 月折旧额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "monthly_depreciation", ColumnDescription = "月折旧额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MonthlyDepreciation { get; set; } = 0;

    /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }
    
    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    [SugarColumn(ColumnName = "asset_status", ColumnDescription = "资产状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AssetStatus { get; set; } = 0;
}
