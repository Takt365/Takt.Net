// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Sales
// 文件名称：TaktSalesPriceChangeLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：销售价格变更记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Sales;

/// <summary>
/// 销售价格变更记录表Dto
/// </summary>
public partial class TaktSalesPriceChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogDto()
    {
        ChangeUserName = string.Empty;
    }

    /// <summary>
    /// 销售价格变更记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceChangeLogId { get; set; }

    /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }
    /// <summary>
    /// 原价格
    /// </summary>
    public decimal OldPrice { get; set; }
    /// <summary>
    /// 新价格
    /// </summary>
    public decimal NewPrice { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }
    /// <summary>
    /// 变更人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChangeUserId { get; set; }
    /// <summary>
    /// 变更人姓名
    /// </summary>
    public string ChangeUserName { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
}

/// <summary>
/// 销售价格变更记录表查询DTO
/// </summary>
public partial class TaktSalesPriceChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 销售价格变更记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceChangeLogId { get; set; }

    /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PriceId { get; set; }
    /// <summary>
    /// 原价格
    /// </summary>
    public decimal? OldPrice { get; set; }
    /// <summary>
    /// 新价格
    /// </summary>
    public decimal? NewPrice { get; set; }
    /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime? ChangeTime { get; set; }

    /// <summary>
    /// 变更时间开始时间
    /// </summary>
    public DateTime? ChangeTimeStart { get; set; }
    /// <summary>
    /// 变更时间结束时间
    /// </summary>
    public DateTime? ChangeTimeEnd { get; set; }
    /// <summary>
    /// 变更人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ChangeUserId { get; set; }
    /// <summary>
    /// 变更人姓名
    /// </summary>
    public string? ChangeUserName { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt创建销售价格变更记录表DTO
/// </summary>
public partial class TaktSalesPriceChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogCreateDto()
    {
        ChangeUserName = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PriceId { get; set; }

        /// <summary>
    /// 原价格
    /// </summary>
    public decimal OldPrice { get; set; }

        /// <summary>
    /// 新价格
    /// </summary>
    public decimal NewPrice { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ChangeUserId { get; set; }

        /// <summary>
    /// 变更人姓名
    /// </summary>
    public string ChangeUserName { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt更新销售价格变更记录表DTO
/// </summary>
public partial class TaktSalesPriceChangeLogUpdateDto : TaktSalesPriceChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// 销售价格变更记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SalesPriceChangeLogId { get; set; }
}

/// <summary>
/// 销售价格变更记录表导入模板DTO
/// </summary>
public partial class TaktSalesPriceChangeLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogTemplateDto()
    {
        ChangeUserName = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 原价格
    /// </summary>
    public decimal OldPrice { get; set; }

        /// <summary>
    /// 新价格
    /// </summary>
    public decimal NewPrice { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人ID
    /// </summary>
    public long ChangeUserId { get; set; }

        /// <summary>
    /// 变更人姓名
    /// </summary>
    public string ChangeUserName { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 销售价格变更记录表导入DTO
/// </summary>
public partial class TaktSalesPriceChangeLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogImportDto()
    {
        ChangeUserName = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 原价格
    /// </summary>
    public decimal OldPrice { get; set; }

        /// <summary>
    /// 新价格
    /// </summary>
    public decimal NewPrice { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人ID
    /// </summary>
    public long ChangeUserId { get; set; }

        /// <summary>
    /// 变更人姓名
    /// </summary>
    public string ChangeUserName { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 销售价格变更记录表导出DTO
/// </summary>
public partial class TaktSalesPriceChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
        ChangeUserName = string.Empty;
    }

        /// <summary>
    /// 价格ID
    /// </summary>
    public long PriceId { get; set; }

        /// <summary>
    /// 原价格
    /// </summary>
    public decimal OldPrice { get; set; }

        /// <summary>
    /// 新价格
    /// </summary>
    public decimal NewPrice { get; set; }

        /// <summary>
    /// 变更时间
    /// </summary>
    public DateTime ChangeTime { get; set; }

        /// <summary>
    /// 变更人ID
    /// </summary>
    public long ChangeUserId { get; set; }

        /// <summary>
    /// 变更人姓名
    /// </summary>
    public string ChangeUserName { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}