// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Bom
// 文件名称：TaktPackagingDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：物料包装信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom;

/// <summary>
/// 物料包装信息表Dto
/// </summary>
public partial class TaktPackagingDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingDto()
    {
        MaterialCode = string.Empty;
        WeightUnit = string.Empty;
        VolumeUnit = string.Empty;
        PackagingType = string.Empty;
        PackingUnit = string.Empty;
    }

    /// <summary>
    /// 物料包装信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PackagingId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }
    /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }
    /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }
    /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }
    /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }
    /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }
    /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }
    /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }
    /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }
    /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }
    /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }
    /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }
    /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }
    /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }
    /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }
    /// <summary>
    /// 包装类型
    /// </summary>
    public string PackagingType { get; set; }
    /// <summary>
    /// 包装单位
    /// </summary>
    public string PackingUnit { get; set; }
    /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }
    /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }
    /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 物料包装信息表查询DTO
/// </summary>
public partial class TaktPackagingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 物料包装信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PackagingId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }
    /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }
    /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }
    /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }
    /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }
    /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }
    /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }
    /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }
    /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }
    /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }
    /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }
    /// <summary>
    /// 重量单位
    /// </summary>
    public string? WeightUnit { get; set; }
    /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }
    /// <summary>
    /// 体积单位
    /// </summary>
    public string? VolumeUnit { get; set; }
    /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }
    /// <summary>
    /// 包装类型
    /// </summary>
    public string? PackagingType { get; set; }
    /// <summary>
    /// 包装单位
    /// </summary>
    public string? PackingUnit { get; set; }
    /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }
    /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }
    /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建物料包装信息表DTO
/// </summary>
public partial class TaktPackagingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingCreateDto()
    {
        MaterialCode = string.Empty;
        WeightUnit = string.Empty;
        VolumeUnit = string.Empty;
        PackagingType = string.Empty;
        PackingUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }

        /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }

        /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }

        /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }

        /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }

        /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }

        /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }

        /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }

        /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }

        /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }

        /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }

        /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

        /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }

        /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }

        /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }

        /// <summary>
    /// 包装类型
    /// </summary>
    public string PackagingType { get; set; }

        /// <summary>
    /// 包装单位
    /// </summary>
    public string PackingUnit { get; set; }

        /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }

        /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }

        /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新物料包装信息表DTO
/// </summary>
public partial class TaktPackagingUpdateDto : TaktPackagingCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingUpdateDto()
    {
    }

        /// <summary>
    /// 物料包装信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PackagingId { get; set; }
}

/// <summary>
/// 物料包装信息表导入模板DTO
/// </summary>
public partial class TaktPackagingTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingTemplateDto()
    {
        MaterialCode = string.Empty;
        WeightUnit = string.Empty;
        VolumeUnit = string.Empty;
        PackagingType = string.Empty;
        PackingUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }

        /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }

        /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }

        /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }

        /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }

        /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }

        /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }

        /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }

        /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }

        /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }

        /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }

        /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

        /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }

        /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }

        /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }

        /// <summary>
    /// 包装类型
    /// </summary>
    public string PackagingType { get; set; }

        /// <summary>
    /// 包装单位
    /// </summary>
    public string PackingUnit { get; set; }

        /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }

        /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }

        /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 物料包装信息表导入DTO
/// </summary>
public partial class TaktPackagingImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingImportDto()
    {
        MaterialCode = string.Empty;
        WeightUnit = string.Empty;
        VolumeUnit = string.Empty;
        PackagingType = string.Empty;
        PackingUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }

        /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }

        /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }

        /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }

        /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }

        /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }

        /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }

        /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }

        /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }

        /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }

        /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }

        /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

        /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }

        /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }

        /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }

        /// <summary>
    /// 包装类型
    /// </summary>
    public string PackagingType { get; set; }

        /// <summary>
    /// 包装单位
    /// </summary>
    public string PackingUnit { get; set; }

        /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }

        /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }

        /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 物料包装信息表导出DTO
/// </summary>
public partial class TaktPackagingExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPackagingExportDto()
    {
        CreatedAt = DateTime.Now;
        MaterialCode = string.Empty;
        WeightUnit = string.Empty;
        VolumeUnit = string.Empty;
        PackagingType = string.Empty;
        PackingUnit = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 海关商品编码
    /// </summary>
    public string? HsCode { get; set; }

        /// <summary>
    /// 商品名称
    /// </summary>
    public string? HsName { get; set; }

        /// <summary>
    /// 附加编码
    /// </summary>
    public string? AdditionalCode { get; set; }

        /// <summary>
    /// 原产国/地区编码
    /// </summary>
    public string? OriginCountryRegionCode { get; set; }

        /// <summary>
    /// 原产国/地区名称
    /// </summary>
    public string? OriginCountryRegionName { get; set; }

        /// <summary>
    /// 目的国/地区编码
    /// </summary>
    public string? DestinationCountryRegionCode { get; set; }

        /// <summary>
    /// 目的国/地区名称
    /// </summary>
    public string? DestinationCountryRegionName { get; set; }

        /// <summary>
    /// 监管条件代码
    /// </summary>
    public string? RegulatoryConditionCode { get; set; }

        /// <summary>
    /// 税率/协定税率标识
    /// </summary>
    public string? TariffRateType { get; set; }

        /// <summary>
    /// 毛重
    /// </summary>
    public decimal? GrossWeight { get; set; }

        /// <summary>
    /// 净重
    /// </summary>
    public decimal? NetWeight { get; set; }

        /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

        /// <summary>
    /// 业务量
    /// </summary>
    public decimal? BusinessVolume { get; set; }

        /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }

        /// <summary>
    /// 大小/量纲
    /// </summary>
    public string? SizeDimension { get; set; }

        /// <summary>
    /// 包装类型
    /// </summary>
    public string PackagingType { get; set; }

        /// <summary>
    /// 包装单位
    /// </summary>
    public string PackingUnit { get; set; }

        /// <summary>
    /// 每包装数量
    /// </summary>
    public decimal? QuantityPerPacking { get; set; }

        /// <summary>
    /// 包装规格
    /// </summary>
    public string? PackagingSpec { get; set; }

        /// <summary>
    /// 包装描述
    /// </summary>
    public string? PackagingDescription { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}