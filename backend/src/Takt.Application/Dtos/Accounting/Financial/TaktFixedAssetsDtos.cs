// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktFixedAssetsDtos.cs
// 功能描述：Takt固定资产DTO，包含固定资产相关的数据传输对象（查询、创建、更新、状态、导入、导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// Takt固定资产DTO
/// </summary>
public class TaktFixedAssetsDto
{
    /// <summary>
    /// 固定资产ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FixedAssetsId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产编码（唯一索引）
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型（0=固定资产，1=无形资产，2=流动资产，3=长期投资）
    /// </summary>
    public int AssetType { get; set; }

    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal AssetOriginalValue { get; set; }

    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal AssetNetValue { get; set; }

    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal AccumulatedDepreciation { get; set; }

    /// <summary>
    /// 成本中心ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CostCenterId { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

    /// <summary>
    /// 所属部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 使用人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 使用人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 报废日期
    /// </summary>
    public DateTime? ScrapDate { get; set; }

    /// <summary>
    /// 处置日期
    /// </summary>
    public DateTime? DisposalDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int ExpectedLifeMonths { get; set; }

    /// <summary>
    /// 折旧方法（0=直线法，1=年数总和法，2=双倍余额递减法，3=工作量法）
    /// </summary>
    public int DepreciationMethod { get; set; }

    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 租户配置ID（ConfigId）
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID（与实体基类一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// Takt固定资产查询DTO
/// </summary>
public class TaktFixedAssetsQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 资产编码
    /// </summary>
    public string? AssetCode { get; set; }

    /// <summary>
    /// 资产名称
    /// </summary>
    public string? AssetName { get; set; }

    /// <summary>
    /// 资产类别ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类型（0=固定资产，1=无形资产，2=流动资产，3=长期投资）
    /// </summary>
    public int? AssetType { get; set; }

    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    public int? AssetStatus { get; set; }
}

/// <summary>
/// Takt创建固定资产DTO
/// </summary>
public class TaktFixedAssetsCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型（0=固定资产，1=无形资产，2=流动资产，3=长期投资）
    /// </summary>
    public int AssetType { get; set; }

    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal AssetOriginalValue { get; set; }

    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal AssetNetValue { get; set; }

    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal AccumulatedDepreciation { get; set; }

    /// <summary>
    /// 成本中心ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CostCenterId { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

    /// <summary>
    /// 所属部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 使用人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }

    /// <summary>
    /// 使用人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int ExpectedLifeMonths { get; set; }

    /// <summary>
    /// 折旧方法（0=直线法，1=年数总和法，2=双倍余额递减法，3=工作量法）
    /// </summary>
    public int DepreciationMethod { get; set; }

    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新固定资产DTO
/// </summary>
public class TaktFixedAssetsUpdateDto : TaktFixedAssetsCreateDto
{
    /// <summary>
    /// 固定资产ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FixedAssetsId { get; set; }
}

/// <summary>
/// Takt固定资产状态DTO
/// </summary>
public class TaktFixedAssetsStatusDto
{
    /// <summary>
    /// 固定资产ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FixedAssetsId { get; set; }

    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    public int AssetStatus { get; set; }
}

/// <summary>
/// Takt固定资产导入模板DTO
/// </summary>
public class TaktFixedAssetsTemplateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别ID
    /// </summary>
    public long AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型（0=固定资产，1=无形资产，2=流动资产，3=长期投资）
    /// </summary>
    public int AssetType { get; set; }

    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal AssetOriginalValue { get; set; }

    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal AssetNetValue { get; set; }

    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal AccumulatedDepreciation { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int ExpectedLifeMonths { get; set; }

    /// <summary>
    /// 折旧方法（0=直线法，1=年数总和法，2=双倍余额递减法，3=工作量法）
    /// </summary>
    public int DepreciationMethod { get; set; }

    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

    /// <summary>
    /// 资产状态（0=在用，1=闲置，2=维修中，3=报废，4=已处置）
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt固定资产导入DTO
/// </summary>
public class TaktFixedAssetsImportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别ID
    /// </summary>
    public long AssetCategoryId { get; set; }

    /// <summary>
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型
    /// </summary>
    public int AssetType { get; set; }

    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal AssetOriginalValue { get; set; }

    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal AssetNetValue { get; set; }

    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal AccumulatedDepreciation { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期（Excel 字符串）
    /// </summary>
    public string? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期（Excel 字符串）
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int ExpectedLifeMonths { get; set; }

    /// <summary>
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

    /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt固定资产导出DTO
/// </summary>
public class TaktFixedAssetsExportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; } = string.Empty;

    /// <summary>
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }

    /// <summary>
    /// 资产类型
    /// </summary>
    public int AssetType { get; set; }

    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal AssetOriginalValue { get; set; }

    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal AssetNetValue { get; set; }

    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal AccumulatedDepreciation { get; set; }

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

    /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 使用人姓名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 资产位置
    /// </summary>
    public string? AssetLocation { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int ExpectedLifeMonths { get; set; }

    /// <summary>
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

    /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
