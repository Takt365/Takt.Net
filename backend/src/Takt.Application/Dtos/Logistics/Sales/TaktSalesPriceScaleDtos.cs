// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Sales
// 文件名称：TaktSalesPriceScaleDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：销售价格阶梯表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Sales;

/// <summary>
/// 销售价格阶梯表Dto
/// </summary>
public partial class TaktSalesPriceScaleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 销售价格阶梯表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceScaleId { get; set; }

    /// <summary>
    /// 价格明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ItemId { get; set; }
    /// <summary>
    /// 起始数量
    /// </summary>
    public decimal StartQuantity { get; set; }
    /// <summary>
    /// 结束数量
    /// </summary>
    public decimal EndQuantity { get; set; }
    /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal ScalePrice { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 销售价格阶梯表查询DTO
/// </summary>
public partial class TaktSalesPriceScaleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceScaleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 销售价格阶梯表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceScaleId { get; set; }

    /// <summary>
    /// 价格明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ItemId { get; set; }
    /// <summary>
    /// 起始数量
    /// </summary>
    public decimal? StartQuantity { get; set; }
    /// <summary>
    /// 结束数量
    /// </summary>
    public decimal? EndQuantity { get; set; }
    /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal? ScalePrice { get; set; }
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
/// Takt创建销售价格阶梯表DTO
/// </summary>
public partial class TaktSalesPriceScaleCreateDto
{
        /// <summary>
    /// 价格明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ItemId { get; set; }

        /// <summary>
    /// 起始数量
    /// </summary>
    public decimal StartQuantity { get; set; }

        /// <summary>
    /// 结束数量
    /// </summary>
    public decimal EndQuantity { get; set; }

        /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal ScalePrice { get; set; }

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
/// Takt更新销售价格阶梯表DTO
/// </summary>
public partial class TaktSalesPriceScaleUpdateDto : TaktSalesPriceScaleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceScaleUpdateDto()
    {
    }

        /// <summary>
    /// 销售价格阶梯表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceScaleId { get; set; }
}

/// <summary>
/// 销售价格阶梯表导入模板DTO
/// </summary>
public partial class TaktSalesPriceScaleTemplateDto
{
        /// <summary>
    /// 价格明细ID
    /// </summary>
    public long ItemId { get; set; }

        /// <summary>
    /// 起始数量
    /// </summary>
    public decimal StartQuantity { get; set; }

        /// <summary>
    /// 结束数量
    /// </summary>
    public decimal EndQuantity { get; set; }

        /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal ScalePrice { get; set; }

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
/// 销售价格阶梯表导入DTO
/// </summary>
public partial class TaktSalesPriceScaleImportDto
{
        /// <summary>
    /// 价格明细ID
    /// </summary>
    public long ItemId { get; set; }

        /// <summary>
    /// 起始数量
    /// </summary>
    public decimal StartQuantity { get; set; }

        /// <summary>
    /// 结束数量
    /// </summary>
    public decimal EndQuantity { get; set; }

        /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal ScalePrice { get; set; }

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
/// 销售价格阶梯表导出DTO
/// </summary>
public partial class TaktSalesPriceScaleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceScaleExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 价格明细ID
    /// </summary>
    public long ItemId { get; set; }

        /// <summary>
    /// 起始数量
    /// </summary>
    public decimal StartQuantity { get; set; }

        /// <summary>
    /// 结束数量
    /// </summary>
    public decimal EndQuantity { get; set; }

        /// <summary>
    /// 阶梯价格
    /// </summary>
    public decimal ScalePrice { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}