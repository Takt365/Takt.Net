// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Sales
// 文件名称：TaktSalesPriceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：销售价格表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Sales;

/// <summary>
/// 销售价格表Dto
/// </summary>
public partial class TaktSalesPriceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 销售价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }
    /// <summary>
    /// Effective from
    /// </summary>
    public DateTime EffectiveFrom { get; set; }
    /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }
    /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

    /// <summary>
    /// 物料价格明细列表（主子表关系，一个客户价格可以有多个物料价格）
    /// </summary>
    public List<long>? ItemIds { get; set; }
}

/// <summary>
/// 销售价格表查询DTO
/// </summary>
public partial class TaktSalesPriceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 销售价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 价格类型
    /// </summary>
    public int? PriceType { get; set; }
    /// <summary>
    /// Effective from
    /// </summary>
    public DateTime? EffectiveFrom { get; set; }

    /// <summary>
    /// Effective from开始时间
    /// </summary>
    public DateTime? EffectiveFromStart { get; set; }
    /// <summary>
    /// Effective from结束时间
    /// </summary>
    public DateTime? EffectiveFromEnd { get; set; }
    /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Effective to开始时间
    /// </summary>
    public DateTime? EffectiveToStart { get; set; }
    /// <summary>
    /// Effective to结束时间
    /// </summary>
    public DateTime? EffectiveToEnd { get; set; }
    /// <summary>
    /// 价格状态
    /// </summary>
    public int? PriceStatus { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    public int? IsEnabled { get; set; }

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
/// Takt创建销售价格表DTO
/// </summary>
public partial class TaktSalesPriceCreateDto
{
        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// Effective from
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

        /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

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
/// Takt更新销售价格表DTO
/// </summary>
public partial class TaktSalesPriceUpdateDto : TaktSalesPriceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceUpdateDto()
    {
    }

        /// <summary>
    /// 销售价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceId { get; set; }
}

/// <summary>
/// 销售价格表价格状态DTO
/// </summary>
public partial class TaktSalesPriceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceStatusDto()
    {
    }

        /// <summary>
    /// 销售价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceId { get; set; }

    /// <summary>
    /// 价格状态（0=禁用，1=启用）
    /// </summary>
    public int PriceStatus { get; set; }
}

/// <summary>
/// 销售价格表导入模板DTO
/// </summary>
public partial class TaktSalesPriceTemplateDto
{
        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// Effective from
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

        /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

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
/// 销售价格表导入DTO
/// </summary>
public partial class TaktSalesPriceImportDto
{
        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// Effective from
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

        /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

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
/// 销售价格表导出DTO
/// </summary>
public partial class TaktSalesPriceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// Effective from
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

        /// <summary>
    /// Effective to
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

        /// <summary>
    /// 是否启用
    /// </summary>
    public int IsEnabled { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}