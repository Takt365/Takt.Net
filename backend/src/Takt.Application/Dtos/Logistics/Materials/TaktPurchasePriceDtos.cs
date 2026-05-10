// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchasePriceDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：采购价格表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购价格表Dto
/// </summary>
public partial class TaktPurchasePriceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceDto()
    {
        SupplierCode = string.Empty;
    }

    /// <summary>
    /// 采购价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }
    /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveStartDate { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }
    /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

    /// <summary>
    /// 物料价格明细列表（主子表关系，一个供应商价格可以有多个物料价格）（外键在子表 TaktPurchasePriceItemDto.PurchasePriceId）
    /// </summary>
    public List<TaktPurchasePriceItemDto>? Items { get; set; }

    /// <summary>
    /// 采购价格变更记录列表（外键在子表 ）（外键在子表 TaktPurchasePriceChangeLogDto.PurchasePriceId）
    /// </summary>
    public List<TaktPurchasePriceChangeLogDto>? ChangeLogs { get; set; }
}

/// <summary>
/// 采购价格表查询DTO
/// </summary>
public partial class TaktPurchasePriceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }
    /// <summary>
    /// 价格类型
    /// </summary>
    public int? PriceType { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveStartDate { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveStartDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveStartDateEnd { get; set; }
    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }

    /// <summary>
    /// 失效日期开始时间
    /// </summary>
    public DateTime? EffectiveEndDateStart { get; set; }
    /// <summary>
    /// 失效日期结束时间
    /// </summary>
    public DateTime? EffectiveEndDateEnd { get; set; }
    /// <summary>
    /// 价格状态
    /// </summary>
    public int? PriceStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
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
/// Takt创建采购价格表DTO
/// </summary>
public partial class TaktPurchasePriceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceCreateDto()
    {
        SupplierCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveStartDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 物料价格明细列表（主子表关系，一个供应商价格可以有多个物料价格）（外键在子表 TaktPurchasePriceItemCreateDto.PurchasePriceId）
    /// </summary>
    public List<TaktPurchasePriceItemCreateDto>? Items { get; set; }


    /// <summary>
    /// 采购价格变更记录列表（外键在子表 ）（外键在子表 TaktPurchasePriceChangeLogCreateDto.PurchasePriceId）
    /// </summary>
    public List<TaktPurchasePriceChangeLogCreateDto>? ChangeLogs { get; set; }

}

/// <summary>
/// Takt更新采购价格表DTO
/// </summary>
public partial class TaktPurchasePriceUpdateDto : TaktPurchasePriceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceUpdateDto()
    {
    }

        /// <summary>
    /// 采购价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceId { get; set; } = 0;
}

/// <summary>
/// 采购价格表价格状态DTO
/// </summary>
public partial class TaktPurchasePricePriceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePricePriceStatusDto()
    {
    }

        /// <summary>
    /// 采购价格表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchasePriceId { get; set; } = 0;

    /// <summary>
    /// 价格状态（0=禁用，1=启用）
    /// </summary>
    public int PriceStatus { get; set; }
}

/// <summary>
/// 采购价格表导入模板DTO
/// </summary>
public partial class TaktPurchasePriceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceTemplateDto()
    {
        SupplierCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveStartDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

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
/// 采购价格表导入DTO
/// </summary>
public partial class TaktPurchasePriceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceImportDto()
    {
        SupplierCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveStartDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

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
/// 采购价格表导出DTO
/// </summary>
public partial class TaktPurchasePriceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceExportDto()
    {
        CreatedAt = DateTime.Now;
        SupplierCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

        /// <summary>
    /// 价格类型
    /// </summary>
    public int PriceType { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveStartDate { get; set; }

        /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? EffectiveEndDate { get; set; }

        /// <summary>
    /// 价格状态
    /// </summary>
    public int PriceStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}