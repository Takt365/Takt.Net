// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logistics.Material
// 文件名称：TaktPurchasePriceDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格DTO，包含采购价格相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// Takt采购价格DTO（主表）
/// </summary>
public class TaktPurchasePriceDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceDto()
    {
        SupplierCode = string.Empty;
        ConfigId = "0";
        Items = new List<TaktPurchasePriceItemDto>();
    }

    /// <summary>
    /// 价格ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; }

    /// <summary>
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（如果为空则表示永久有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 价格状态（0=草稿，1=已生效，2=已失效，3=已停用）
    /// </summary>
    public int PriceStatus { get; set; } = 0;

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 物料价格明细列表（主子表关系）
    /// </summary>
    public List<TaktPurchasePriceItemDto> Items { get; set; }
}

/// <summary>
/// Takt采购价格明细DTO（子表）
/// </summary>
public class TaktPurchasePriceItemDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = "个";
        Scales = new List<TaktPurchasePriceScaleDto>();
    }

    /// <summary>
    /// 明细ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ItemId { get; set; }

    /// <summary>
    /// 价格ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

    /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; }

    /// <summary>
    /// 采购价格（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal PurchasePrice { get; set; } = 0;

    /// <summary>
    /// 最小采购量（基本单位数量）
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 最大采购量（基本单位数量，0表示无限制）
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 价格阶梯列表
    /// </summary>
    public List<TaktPurchasePriceScaleDto> Scales { get; set; }
}

/// <summary>
/// Takt采购价格阶梯DTO（阶梯表）
/// </summary>
public class TaktPurchasePriceScaleDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceScaleDto()
    {
    }

    /// <summary>
    /// 阶梯ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ScaleId { get; set; }

    /// <summary>
    /// 价格明细ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ItemId { get; set; }

    /// <summary>
    /// 起始数量（基本单位数量，包含此数量）
    /// </summary>
    public decimal StartQuantity { get; set; } = 0;

    /// <summary>
    /// 结束数量（基本单位数量，包含此数量，0表示无上限）
    /// </summary>
    public decimal EndQuantity { get; set; } = 0;

    /// <summary>
    /// 阶梯价格（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal ScalePrice { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;
}

/// <summary>
/// Takt采购价格查询DTO
/// </summary>
public class TaktPurchasePriceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在供应商编码中模糊查询

    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    public int? PriceType { get; set; }

    /// <summary>
    /// 价格状态（0=草稿，1=已生效，2=已失效，3=已停用）
    /// </summary>
    public int? PriceStatus { get; set; }

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    public int? IsEnabled { get; set; }
}

/// <summary>
/// Takt创建采购价格DTO（主表）
/// </summary>
public class TaktPurchasePriceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceCreateDto()
    {
        SupplierCode = string.Empty;
        Items = new List<TaktPurchasePriceItemCreateDto>();
    }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

    /// <summary>
    /// 供应商编码
    /// </summary>
    public string SupplierCode { get; set; } = string.Empty;

    /// <summary>
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（如果为空则表示永久有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 物料价格明细列表（主子表关系）
    /// </summary>
    public List<TaktPurchasePriceItemCreateDto> Items { get; set; }
}

/// <summary>
/// Takt创建采购价格明细DTO（子表）
/// </summary>
public class TaktPurchasePriceItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemCreateDto()
    {
        MaterialCode = string.Empty;
        PurchaseUnit = "个";
        Scales = new List<TaktPurchasePriceScaleCreateDto>();
    }

    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 采购单位
    /// </summary>
    public string PurchaseUnit { get; set; } = "个";

    /// <summary>
    /// 采购价格（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal PurchasePrice { get; set; } = 0;

    /// <summary>
    /// 最小采购量（基本单位数量）
    /// </summary>
    public decimal MinPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 最大采购量（基本单位数量，0表示无限制）
    /// </summary>
    public decimal MaxPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 价格阶梯列表
    /// </summary>
    public List<TaktPurchasePriceScaleCreateDto> Scales { get; set; }
}

/// <summary>
/// Takt创建采购价格阶梯DTO（阶梯表）
/// </summary>
public class TaktPurchasePriceScaleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceScaleCreateDto()
    {
    }

    /// <summary>
    /// 起始数量（基本单位数量，包含此数量）
    /// </summary>
    public decimal StartQuantity { get; set; } = 0;

    /// <summary>
    /// 结束数量（基本单位数量，包含此数量，0表示无上限）
    /// </summary>
    public decimal EndQuantity { get; set; } = 0;

    /// <summary>
    /// 阶梯价格（精确到分，存储为整数，单位为分）
    /// </summary>
    public decimal ScalePrice { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int OrderNum { get; set; } = 0;
}

/// <summary>
/// Takt更新采购价格DTO
/// </summary>
public class TaktPurchasePriceUpdateDto : TaktPurchasePriceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceUpdateDto()
    {
    }

    /// <summary>
    /// 价格ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }
}

/// <summary>
/// Takt采购价格状态DTO
/// </summary>
public class TaktPurchasePriceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceStatusDto()
    {
    }

    /// <summary>
    /// 价格ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }

    /// <summary>
    /// 价格状态（0=草稿，1=已生效，2=已失效，3=已停用）
    /// </summary>
    public int PriceStatus { get; set; }
}

/// <summary>
/// Takt采购价格模板DTO
/// </summary>
public class TaktPurchasePriceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceTemplateDto()
    {
        SupplierCode = string.Empty;
        Remark = string.Empty;
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
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（如果为空则表示永久有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt采购价格导入DTO
/// </summary>
public class TaktPurchasePriceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceImportDto()
    {
        SupplierCode = string.Empty;
        Remark = string.Empty;
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
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（如果为空则表示永久有效）
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt采购价格导出DTO
/// </summary>
public class TaktPurchasePriceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceExportDto()
    {
        SupplierCode = string.Empty;
        PriceType = string.Empty;
        PriceStatus = string.Empty;
        IsEnabled = string.Empty;
        CreatedAt = DateTime.Now;
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
    public string PriceType { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 价格状态
    /// </summary>
    public string PriceStatus { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public string IsEnabled { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
