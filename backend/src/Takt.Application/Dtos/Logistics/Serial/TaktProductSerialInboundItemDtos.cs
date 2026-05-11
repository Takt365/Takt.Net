// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Serial
// 文件名称：TaktProductSerialInboundItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：产品序列号入库明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Serial;

/// <summary>
/// 产品序列号入库明细表Dto
/// </summary>
public partial class TaktProductSerialInboundItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemDto()
    {
        InboundNo = string.Empty;
        InboundSerialNo = string.Empty;
    }

    /// <summary>
    /// 产品序列号入库明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialInboundItemId { get; set; } = 0;

    /// <summary>
    /// 入库ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InboundId { get; set; }
    /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 入库序列号
    /// </summary>
    public string InboundSerialNo { get; set; }
    /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime InboundTime { get; set; }

    /// <summary>
    /// 入库主表
    /// </summary>
    public TaktProductSerialInboundDto? Inbound { get; set; }
}

/// <summary>
/// 产品序列号入库明细表查询DTO
/// </summary>
public partial class TaktProductSerialInboundItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 入库ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InboundId { get; set; }
    /// <summary>
    /// 入库单号
    /// </summary>
    public string? InboundNo { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 入库序列号
    /// </summary>
    public string? InboundSerialNo { get; set; }
    /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime? InboundTime { get; set; }

    /// <summary>
    /// 入库时间开始时间
    /// </summary>
    public DateTime? InboundTimeStart { get; set; }
    /// <summary>
    /// 入库时间结束时间
    /// </summary>
    public DateTime? InboundTimeEnd { get; set; }

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
/// Takt创建产品序列号入库明细表DTO
/// </summary>
public partial class TaktProductSerialInboundItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemCreateDto()
    {
        InboundNo = string.Empty;
        InboundSerialNo = string.Empty;
    }

        /// <summary>
    /// 入库ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InboundId { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 入库序列号
    /// </summary>
    public string InboundSerialNo { get; set; }

        /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime InboundTime { get; set; }

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
/// Takt更新产品序列号入库明细表DTO
/// </summary>
public partial class TaktProductSerialInboundItemUpdateDto : TaktProductSerialInboundItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemUpdateDto()
    {
    }

        /// <summary>
    /// 产品序列号入库明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialInboundItemId { get; set; } = 0;
}

/// <summary>
/// 产品序列号入库明细表导入模板DTO
/// </summary>
public partial class TaktProductSerialInboundItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemTemplateDto()
    {
        InboundNo = string.Empty;
        InboundSerialNo = string.Empty;
    }

        /// <summary>
    /// 入库ID
    /// </summary>
    public long InboundId { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 入库序列号
    /// </summary>
    public string InboundSerialNo { get; set; }

        /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime InboundTime { get; set; }

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
/// 产品序列号入库明细表导入DTO
/// </summary>
public partial class TaktProductSerialInboundItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemImportDto()
    {
        InboundNo = string.Empty;
        InboundSerialNo = string.Empty;
    }

        /// <summary>
    /// 入库ID
    /// </summary>
    public long InboundId { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 入库序列号
    /// </summary>
    public string InboundSerialNo { get; set; }

        /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime InboundTime { get; set; }

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
/// 产品序列号入库明细表导出DTO
/// </summary>
public partial class TaktProductSerialInboundItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemExportDto()
    {
        CreatedAt = DateTime.Now;
        InboundNo = string.Empty;
        InboundSerialNo = string.Empty;
    }

        /// <summary>
    /// 入库ID
    /// </summary>
    public long InboundId { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 入库序列号
    /// </summary>
    public string InboundSerialNo { get; set; }

        /// <summary>
    /// 入库时间
    /// </summary>
    public DateTime InboundTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}