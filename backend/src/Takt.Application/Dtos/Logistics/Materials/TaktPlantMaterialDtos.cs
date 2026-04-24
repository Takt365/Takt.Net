// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPlantMaterialDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工厂物料表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 工厂物料表Dto
/// </summary>
public partial class TaktPlantMaterialDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        BaseUnit = string.Empty;
    }

    /// <summary>
    /// 工厂物料表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantMaterialId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }
    /// <summary>
    /// 品目阶层ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MaterialHierarchyId { get; set; }
    /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }
    /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }
    /// <summary>
    /// 物料类型
    /// </summary>
    public int MaterialType { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }
    /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }
    /// <summary>
    /// 基本单位
    /// </summary>
    public string BaseUnit { get; set; }
    /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }
    /// <summary>
    /// 采购类型
    /// </summary>
    public int PurchaseType { get; set; }
    /// <summary>
    /// 特殊采购
    /// </summary>
    public int SpecialProcurement { get; set; }
    /// <summary>
    /// 是否散装
    /// </summary>
    public int IsBulk { get; set; }
    /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }
    /// <summary>
    /// 舍入值
    /// </summary>
    public decimal RoundingValue { get; set; }
    /// <summary>
    /// 计划交货时间
    /// </summary>
    public int PlannedDeliveryTimeDays { get; set; }
    /// <summary>
    /// 自制生产天数
    /// </summary>
    public int InHouseProductionDays { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }
    /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }
    /// <summary>
    /// 币种代码
    /// </summary>
    public string CurrencyCode { get; set; }
    /// <summary>
    /// 价格控制
    /// </summary>
    public int PriceControl { get; set; }
    /// <summary>
    /// 价格单位
    /// </summary>
    public decimal PriceUnit { get; set; }
    /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }
    /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }
    /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }
    /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal LatestPurchasePrice { get; set; }
    /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }
    /// <summary>
    /// 安全库存
    /// </summary>
    public decimal SafetyStock { get; set; }
    /// <summary>
    /// 最大库存
    /// </summary>
    public decimal MaxStock { get; set; }
    /// <summary>
    /// 最小库存
    /// </summary>
    public decimal MinStock { get; set; }
    /// <summary>
    /// 当前库存
    /// </summary>
    public decimal CurrentStock { get; set; }
    /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }
    /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }
    /// <summary>
    /// 是否检验
    /// </summary>
    public int InspectionRequired { get; set; }
    /// <summary>
    /// 是否批次管理
    /// </summary>
    public int IsBatch { get; set; }
    /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int IsExpiry { get; set; }
    /// <summary>
    /// 保质期天数
    /// </summary>
    public int ExpiryDays { get; set; }
    /// <summary>
    /// 物料状态
    /// </summary>
    public int MaterialStatus { get; set; }
    /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }
    /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }
    /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }
    /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }
}

/// <summary>
/// 工厂物料表查询DTO
/// </summary>
public partial class TaktPlantMaterialQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂物料表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantMaterialId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }
    /// <summary>
    /// 品目阶层ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MaterialHierarchyId { get; set; }
    /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }
    /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }
    /// <summary>
    /// 物料类型
    /// </summary>
    public int? MaterialType { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }
    /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }
    /// <summary>
    /// 基本单位
    /// </summary>
    public string? BaseUnit { get; set; }
    /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }
    /// <summary>
    /// 采购类型
    /// </summary>
    public int? PurchaseType { get; set; }
    /// <summary>
    /// 特殊采购
    /// </summary>
    public int? SpecialProcurement { get; set; }
    /// <summary>
    /// 是否散装
    /// </summary>
    public int? IsBulk { get; set; }
    /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal? MinOrderQuantity { get; set; }
    /// <summary>
    /// 舍入值
    /// </summary>
    public decimal? RoundingValue { get; set; }
    /// <summary>
    /// 计划交货时间
    /// </summary>
    public int? PlannedDeliveryTimeDays { get; set; }
    /// <summary>
    /// 自制生产天数
    /// </summary>
    public int? InHouseProductionDays { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }
    /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }
    /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }
    /// <summary>
    /// 币种代码
    /// </summary>
    public string? CurrencyCode { get; set; }
    /// <summary>
    /// 价格控制
    /// </summary>
    public int? PriceControl { get; set; }
    /// <summary>
    /// 价格单位
    /// </summary>
    public decimal? PriceUnit { get; set; }
    /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }
    /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }
    /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }
    /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal? LatestPurchasePrice { get; set; }
    /// <summary>
    /// 销售价格
    /// </summary>
    public decimal? SalesPrice { get; set; }
    /// <summary>
    /// 安全库存
    /// </summary>
    public decimal? SafetyStock { get; set; }
    /// <summary>
    /// 最大库存
    /// </summary>
    public decimal? MaxStock { get; set; }
    /// <summary>
    /// 最小库存
    /// </summary>
    public decimal? MinStock { get; set; }
    /// <summary>
    /// 当前库存
    /// </summary>
    public decimal? CurrentStock { get; set; }
    /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }
    /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }
    /// <summary>
    /// 是否检验
    /// </summary>
    public int? InspectionRequired { get; set; }
    /// <summary>
    /// 是否批次管理
    /// </summary>
    public int? IsBatch { get; set; }
    /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int? IsExpiry { get; set; }
    /// <summary>
    /// 保质期天数
    /// </summary>
    public int? ExpiryDays { get; set; }
    /// <summary>
    /// 物料状态
    /// </summary>
    public int? MaterialStatus { get; set; }
    /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }
    /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }
    /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }
    /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }

    /// <summary>
    /// 停产日期开始时间
    /// </summary>
    public DateTime? EndOfLifeDateStart { get; set; }
    /// <summary>
    /// 停产日期结束时间
    /// </summary>
    public DateTime? EndOfLifeDateEnd { get; set; }

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
/// Takt创建工厂物料表DTO
/// </summary>
public partial class TaktPlantMaterialCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialCreateDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        BaseUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }

        /// <summary>
    /// 品目阶层ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MaterialHierarchyId { get; set; }

        /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }

        /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }

        /// <summary>
    /// 物料类型
    /// </summary>
    public int MaterialType { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }

        /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }

        /// <summary>
    /// 基本单位
    /// </summary>
    public string BaseUnit { get; set; }

        /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 采购类型
    /// </summary>
    public int PurchaseType { get; set; }

        /// <summary>
    /// 特殊采购
    /// </summary>
    public int SpecialProcurement { get; set; }

        /// <summary>
    /// 是否散装
    /// </summary>
    public int IsBulk { get; set; }

        /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 舍入值
    /// </summary>
    public decimal RoundingValue { get; set; }

        /// <summary>
    /// 计划交货时间
    /// </summary>
    public int PlannedDeliveryTimeDays { get; set; }

        /// <summary>
    /// 自制生产天数
    /// </summary>
    public int InHouseProductionDays { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }

        /// <summary>
    /// 币种代码
    /// </summary>
    public string CurrencyCode { get; set; }

        /// <summary>
    /// 价格控制
    /// </summary>
    public int PriceControl { get; set; }

        /// <summary>
    /// 价格单位
    /// </summary>
    public decimal PriceUnit { get; set; }

        /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }

        /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }

        /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }

        /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal LatestPurchasePrice { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 安全库存
    /// </summary>
    public decimal SafetyStock { get; set; }

        /// <summary>
    /// 最大库存
    /// </summary>
    public decimal MaxStock { get; set; }

        /// <summary>
    /// 最小库存
    /// </summary>
    public decimal MinStock { get; set; }

        /// <summary>
    /// 当前库存
    /// </summary>
    public decimal CurrentStock { get; set; }

        /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }

        /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }

        /// <summary>
    /// 是否检验
    /// </summary>
    public int InspectionRequired { get; set; }

        /// <summary>
    /// 是否批次管理
    /// </summary>
    public int IsBatch { get; set; }

        /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int IsExpiry { get; set; }

        /// <summary>
    /// 保质期天数
    /// </summary>
    public int ExpiryDays { get; set; }

        /// <summary>
    /// 物料状态
    /// </summary>
    public int MaterialStatus { get; set; }

        /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }

        /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }

        /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }

        /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }

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
/// Takt更新工厂物料表DTO
/// </summary>
public partial class TaktPlantMaterialUpdateDto : TaktPlantMaterialCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialUpdateDto()
    {
    }

        /// <summary>
    /// 工厂物料表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantMaterialId { get; set; }
}

/// <summary>
/// 工厂物料表物料状态DTO
/// </summary>
public partial class TaktPlantMaterialStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialStatusDto()
    {
    }

        /// <summary>
    /// 工厂物料表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PlantMaterialId { get; set; }

    /// <summary>
    /// 物料状态（0=禁用，1=启用）
    /// </summary>
    public int MaterialStatus { get; set; }
}

/// <summary>
/// 工厂物料表导入模板DTO
/// </summary>
public partial class TaktPlantMaterialTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialTemplateDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        BaseUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }

        /// <summary>
    /// 品目阶层ID
    /// </summary>
    public long? MaterialHierarchyId { get; set; }

        /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }

        /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }

        /// <summary>
    /// 物料类型
    /// </summary>
    public int MaterialType { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }

        /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }

        /// <summary>
    /// 基本单位
    /// </summary>
    public string BaseUnit { get; set; }

        /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 采购类型
    /// </summary>
    public int PurchaseType { get; set; }

        /// <summary>
    /// 特殊采购
    /// </summary>
    public int SpecialProcurement { get; set; }

        /// <summary>
    /// 是否散装
    /// </summary>
    public int IsBulk { get; set; }

        /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 舍入值
    /// </summary>
    public decimal RoundingValue { get; set; }

        /// <summary>
    /// 计划交货时间
    /// </summary>
    public int PlannedDeliveryTimeDays { get; set; }

        /// <summary>
    /// 自制生产天数
    /// </summary>
    public int InHouseProductionDays { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }

        /// <summary>
    /// 币种代码
    /// </summary>
    public string CurrencyCode { get; set; }

        /// <summary>
    /// 价格控制
    /// </summary>
    public int PriceControl { get; set; }

        /// <summary>
    /// 价格单位
    /// </summary>
    public decimal PriceUnit { get; set; }

        /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }

        /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }

        /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }

        /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal LatestPurchasePrice { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 安全库存
    /// </summary>
    public decimal SafetyStock { get; set; }

        /// <summary>
    /// 最大库存
    /// </summary>
    public decimal MaxStock { get; set; }

        /// <summary>
    /// 最小库存
    /// </summary>
    public decimal MinStock { get; set; }

        /// <summary>
    /// 当前库存
    /// </summary>
    public decimal CurrentStock { get; set; }

        /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }

        /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }

        /// <summary>
    /// 是否检验
    /// </summary>
    public int InspectionRequired { get; set; }

        /// <summary>
    /// 是否批次管理
    /// </summary>
    public int IsBatch { get; set; }

        /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int IsExpiry { get; set; }

        /// <summary>
    /// 保质期天数
    /// </summary>
    public int ExpiryDays { get; set; }

        /// <summary>
    /// 物料状态
    /// </summary>
    public int MaterialStatus { get; set; }

        /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }

        /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }

        /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }

        /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }

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
/// 工厂物料表导入DTO
/// </summary>
public partial class TaktPlantMaterialImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialImportDto()
    {
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        BaseUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }

        /// <summary>
    /// 品目阶层ID
    /// </summary>
    public long? MaterialHierarchyId { get; set; }

        /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }

        /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }

        /// <summary>
    /// 物料类型
    /// </summary>
    public int MaterialType { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }

        /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }

        /// <summary>
    /// 基本单位
    /// </summary>
    public string BaseUnit { get; set; }

        /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 采购类型
    /// </summary>
    public int PurchaseType { get; set; }

        /// <summary>
    /// 特殊采购
    /// </summary>
    public int SpecialProcurement { get; set; }

        /// <summary>
    /// 是否散装
    /// </summary>
    public int IsBulk { get; set; }

        /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 舍入值
    /// </summary>
    public decimal RoundingValue { get; set; }

        /// <summary>
    /// 计划交货时间
    /// </summary>
    public int PlannedDeliveryTimeDays { get; set; }

        /// <summary>
    /// 自制生产天数
    /// </summary>
    public int InHouseProductionDays { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }

        /// <summary>
    /// 币种代码
    /// </summary>
    public string CurrencyCode { get; set; }

        /// <summary>
    /// 价格控制
    /// </summary>
    public int PriceControl { get; set; }

        /// <summary>
    /// 价格单位
    /// </summary>
    public decimal PriceUnit { get; set; }

        /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }

        /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }

        /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }

        /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal LatestPurchasePrice { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 安全库存
    /// </summary>
    public decimal SafetyStock { get; set; }

        /// <summary>
    /// 最大库存
    /// </summary>
    public decimal MaxStock { get; set; }

        /// <summary>
    /// 最小库存
    /// </summary>
    public decimal MinStock { get; set; }

        /// <summary>
    /// 当前库存
    /// </summary>
    public decimal CurrentStock { get; set; }

        /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }

        /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }

        /// <summary>
    /// 是否检验
    /// </summary>
    public int InspectionRequired { get; set; }

        /// <summary>
    /// 是否批次管理
    /// </summary>
    public int IsBatch { get; set; }

        /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int IsExpiry { get; set; }

        /// <summary>
    /// 保质期天数
    /// </summary>
    public int ExpiryDays { get; set; }

        /// <summary>
    /// 物料状态
    /// </summary>
    public int MaterialStatus { get; set; }

        /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }

        /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }

        /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }

        /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }

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
/// 工厂物料表导出DTO
/// </summary>
public partial class TaktPlantMaterialExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        BaseUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 行业领域
    /// </summary>
    public string? IndustrySector { get; set; }

        /// <summary>
    /// 品目阶层ID
    /// </summary>
    public long? MaterialHierarchyId { get; set; }

        /// <summary>
    /// 品目阶层名称
    /// </summary>
    public string? MaterialHierarchyName { get; set; }

        /// <summary>
    /// 品目组代码
    /// </summary>
    public string? MaterialGroupCode { get; set; }

        /// <summary>
    /// 物料类型
    /// </summary>
    public int MaterialType { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 物料型号
    /// </summary>
    public string? MaterialModel { get; set; }

        /// <summary>
    /// 物料品牌
    /// </summary>
    public string? MaterialBrand { get; set; }

        /// <summary>
    /// 基本单位
    /// </summary>
    public string BaseUnit { get; set; }

        /// <summary>
    /// 采购组
    /// </summary>
    public string? PurchaseGroup { get; set; }

        /// <summary>
    /// 采购类型
    /// </summary>
    public int PurchaseType { get; set; }

        /// <summary>
    /// 特殊采购
    /// </summary>
    public int SpecialProcurement { get; set; }

        /// <summary>
    /// 是否散装
    /// </summary>
    public int IsBulk { get; set; }

        /// <summary>
    /// 最小起订量
    /// </summary>
    public decimal MinOrderQuantity { get; set; }

        /// <summary>
    /// 舍入值
    /// </summary>
    public decimal RoundingValue { get; set; }

        /// <summary>
    /// 计划交货时间
    /// </summary>
    public int PlannedDeliveryTimeDays { get; set; }

        /// <summary>
    /// 自制生产天数
    /// </summary>
    public int InHouseProductionDays { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }

        /// <summary>
    /// 制造商
    /// </summary>
    public string? Manufacturer { get; set; }

        /// <summary>
    /// 制造商零件编号
    /// </summary>
    public string? ManufacturerPartNumber { get; set; }

        /// <summary>
    /// 币种代码
    /// </summary>
    public string CurrencyCode { get; set; }

        /// <summary>
    /// 价格控制
    /// </summary>
    public int PriceControl { get; set; }

        /// <summary>
    /// 价格单位
    /// </summary>
    public decimal PriceUnit { get; set; }

        /// <summary>
    /// 评估类别代码
    /// </summary>
    public string? ValuationCategory { get; set; }

        /// <summary>
    /// 差异码
    /// </summary>
    public string? DifferenceCode { get; set; }

        /// <summary>
    /// 利润中心
    /// </summary>
    public string? ProfitCenter { get; set; }

        /// <summary>
    /// 最新采购价
    /// </summary>
    public decimal LatestPurchasePrice { get; set; }

        /// <summary>
    /// 销售价格
    /// </summary>
    public decimal SalesPrice { get; set; }

        /// <summary>
    /// 安全库存
    /// </summary>
    public decimal SafetyStock { get; set; }

        /// <summary>
    /// 最大库存
    /// </summary>
    public decimal MaxStock { get; set; }

        /// <summary>
    /// 最小库存
    /// </summary>
    public decimal MinStock { get; set; }

        /// <summary>
    /// 当前库存
    /// </summary>
    public decimal CurrentStock { get; set; }

        /// <summary>
    /// 生产地点
    /// </summary>
    public string? ProductionLocation { get; set; }

        /// <summary>
    /// 采购地点
    /// </summary>
    public string? PurchasingLocation { get; set; }

        /// <summary>
    /// 是否检验
    /// </summary>
    public int InspectionRequired { get; set; }

        /// <summary>
    /// 是否批次管理
    /// </summary>
    public int IsBatch { get; set; }

        /// <summary>
    /// 是否保质期管理
    /// </summary>
    public int IsExpiry { get; set; }

        /// <summary>
    /// 保质期天数
    /// </summary>
    public int ExpiryDays { get; set; }

        /// <summary>
    /// 物料状态
    /// </summary>
    public int MaterialStatus { get; set; }

        /// <summary>
    /// 物料属性
    /// </summary>
    public string? MaterialAttributes { get; set; }

        /// <summary>
    /// 物料描述
    /// </summary>
    public string? MaterialDescription { get; set; }

        /// <summary>
    /// 停产状态
    /// </summary>
    public string? IsEndOfLife { get; set; }

        /// <summary>
    /// 停产日期
    /// </summary>
    public DateTime? EndOfLifeDate { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}