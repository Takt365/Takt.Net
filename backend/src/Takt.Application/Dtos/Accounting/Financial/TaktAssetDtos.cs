// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Accounting.Financial
// 文件名称：TaktAssetDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：固定资产表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial;

/// <summary>
/// 固定资产表Dto
/// </summary>
public partial class TaktAssetDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetDto()
    {
        AssetCode = string.Empty;
        AssetName = string.Empty;
    }

    /// <summary>
    /// 固定资产表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; }
    /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; }
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
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }
    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }
}

/// <summary>
/// 固定资产表查询DTO
/// </summary>
public partial class TaktAssetQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 固定资产表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

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
    /// 资产类别名称
    /// </summary>
    public string? AssetCategoryName { get; set; }
    /// <summary>
    /// 资产类型
    /// </summary>
    public int? AssetType { get; set; }
    /// <summary>
    /// 资产原值
    /// </summary>
    public decimal? AssetOriginalValue { get; set; }
    /// <summary>
    /// 资产净值
    /// </summary>
    public decimal? AssetNetValue { get; set; }
    /// <summary>
    /// 累计折旧
    /// </summary>
    public decimal? AccumulatedDepreciation { get; set; }
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
    /// 购买日期开始时间
    /// </summary>
    public DateTime? PurchaseDateStart { get; set; }
    /// <summary>
    /// 购买日期结束时间
    /// </summary>
    public DateTime? PurchaseDateEnd { get; set; }
    /// <summary>
    /// 启用日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 启用日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 启用日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 报废日期
    /// </summary>
    public DateTime? ScrapDate { get; set; }

    /// <summary>
    /// 报废日期开始时间
    /// </summary>
    public DateTime? ScrapDateStart { get; set; }
    /// <summary>
    /// 报废日期结束时间
    /// </summary>
    public DateTime? ScrapDateEnd { get; set; }
    /// <summary>
    /// 处置日期
    /// </summary>
    public DateTime? DisposalDate { get; set; }

    /// <summary>
    /// 处置日期开始时间
    /// </summary>
    public DateTime? DisposalDateStart { get; set; }
    /// <summary>
    /// 处置日期结束时间
    /// </summary>
    public DateTime? DisposalDateEnd { get; set; }
    /// <summary>
    /// 预计使用年限（月）
    /// </summary>
    public int? ExpectedLifeMonths { get; set; }
    /// <summary>
    /// 折旧方法
    /// </summary>
    public int? DepreciationMethod { get; set; }
    /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal? MonthlyDepreciation { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 资产状态
    /// </summary>
    public int? AssetStatus { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建固定资产表DTO
/// </summary>
public partial class TaktAssetCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetCreateDto()
    {
        AssetCode = string.Empty;
        AssetName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; }

        /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; }

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
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

        /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新固定资产表DTO
/// </summary>
public partial class TaktAssetUpdateDto : TaktAssetCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetUpdateDto()
    {
    }

        /// <summary>
    /// 固定资产表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }
}

/// <summary>
/// 固定资产表资产状态DTO
/// </summary>
public partial class TaktAssetStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetStatusDto()
    {
    }

        /// <summary>
    /// 固定资产表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssetId { get; set; }

    /// <summary>
    /// 资产状态（0=禁用，1=启用）
    /// </summary>
    public int AssetStatus { get; set; }
}

/// <summary>
/// 固定资产表导入模板DTO
/// </summary>
public partial class TaktAssetTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetTemplateDto()
    {
        AssetCode = string.Empty;
        AssetName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; }

        /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; }

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
    /// 成本中心ID
    /// </summary>
    public long? CostCenterId { get; set; }

        /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 使用人ID
    /// </summary>
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
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

        /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 固定资产表导入DTO
/// </summary>
public partial class TaktAssetImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetImportDto()
    {
        AssetCode = string.Empty;
        AssetName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; }

        /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; }

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
    /// 成本中心ID
    /// </summary>
    public long? CostCenterId { get; set; }

        /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 使用人ID
    /// </summary>
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
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

        /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 固定资产表导出DTO
/// </summary>
public partial class TaktAssetExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssetExportDto()
    {
        CreatedAt = DateTime.Now;
        AssetCode = string.Empty;
        AssetName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }

        /// <summary>
    /// 资产编码
    /// </summary>
    public string AssetCode { get; set; }

        /// <summary>
    /// 资产名称
    /// </summary>
    public string AssetName { get; set; }

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
    /// 成本中心ID
    /// </summary>
    public long? CostCenterId { get; set; }

        /// <summary>
    /// 成本中心名称
    /// </summary>
    public string? CostCenterName { get; set; }

        /// <summary>
    /// 所属部门ID
    /// </summary>
    public long? DeptId { get; set; }

        /// <summary>
    /// 所属部门名称
    /// </summary>
    public string? DeptName { get; set; }

        /// <summary>
    /// 使用人ID
    /// </summary>
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
    /// 折旧方法
    /// </summary>
    public int DepreciationMethod { get; set; }

        /// <summary>
    /// 月折旧额
    /// </summary>
    public decimal MonthlyDepreciation { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 资产状态
    /// </summary>
    public int AssetStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}