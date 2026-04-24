// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseRequestItemDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：采购申请明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购申请明细表Dto
/// </summary>
public partial class TaktPurchaseRequestItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemDto()
    {
        RequestCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        RequestUnit = string.Empty;
    }

    /// <summary>
    /// 采购申请明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestItemId { get; set; }

    /// <summary>
    /// 申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RequestId { get; set; }
    /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }
    /// <summary>
    /// 物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaterialId { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 申请单位
    /// </summary>
    public string RequestUnit { get; set; }
    /// <summary>
    /// 申请数量
    /// </summary>
    public decimal RequestQuantity { get; set; }
    /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }
    /// <summary>
    /// 预计单价
    /// </summary>
    public decimal EstimatedUnitPrice { get; set; }
    /// <summary>
    /// 预计金额
    /// </summary>
    public decimal EstimatedAmount { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
}

/// <summary>
/// 采购申请明细表查询DTO
/// </summary>
public partial class TaktPurchaseRequestItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 采购申请明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestItemId { get; set; }

    /// <summary>
    /// 申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestId { get; set; }
    /// <summary>
    /// 申请编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MaterialId { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 物料名称
    /// </summary>
    public string? MaterialName { get; set; }
    /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }
    /// <summary>
    /// 申请单位
    /// </summary>
    public string? RequestUnit { get; set; }
    /// <summary>
    /// 申请数量
    /// </summary>
    public decimal? RequestQuantity { get; set; }
    /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal? ConvertedQuantity { get; set; }
    /// <summary>
    /// 预计单价
    /// </summary>
    public decimal? EstimatedUnitPrice { get; set; }
    /// <summary>
    /// 预计金额
    /// </summary>
    public decimal? EstimatedAmount { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }

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
/// Takt创建采购申请明细表DTO
/// </summary>
public partial class TaktPurchaseRequestItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemCreateDto()
    {
        RequestCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        RequestUnit = string.Empty;
    }

        /// <summary>
    /// 申请ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RequestId { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 物料ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MaterialId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 申请单位
    /// </summary>
    public string RequestUnit { get; set; }

        /// <summary>
    /// 申请数量
    /// </summary>
    public decimal RequestQuantity { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 预计单价
    /// </summary>
    public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
    /// 预计金额
    /// </summary>
    public decimal EstimatedAmount { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// Takt更新采购申请明细表DTO
/// </summary>
public partial class TaktPurchaseRequestItemUpdateDto : TaktPurchaseRequestItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemUpdateDto()
    {
    }

        /// <summary>
    /// 采购申请明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestItemId { get; set; }
}

/// <summary>
/// 采购申请明细表导入模板DTO
/// </summary>
public partial class TaktPurchaseRequestItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemTemplateDto()
    {
        RequestCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        RequestUnit = string.Empty;
    }

        /// <summary>
    /// 申请ID
    /// </summary>
    public long RequestId { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 物料ID
    /// </summary>
    public long MaterialId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 申请单位
    /// </summary>
    public string RequestUnit { get; set; }

        /// <summary>
    /// 申请数量
    /// </summary>
    public decimal RequestQuantity { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 预计单价
    /// </summary>
    public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
    /// 预计金额
    /// </summary>
    public decimal EstimatedAmount { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// 采购申请明细表导入DTO
/// </summary>
public partial class TaktPurchaseRequestItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemImportDto()
    {
        RequestCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        RequestUnit = string.Empty;
    }

        /// <summary>
    /// 申请ID
    /// </summary>
    public long RequestId { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 物料ID
    /// </summary>
    public long MaterialId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 申请单位
    /// </summary>
    public string RequestUnit { get; set; }

        /// <summary>
    /// 申请数量
    /// </summary>
    public decimal RequestQuantity { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 预计单价
    /// </summary>
    public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
    /// 预计金额
    /// </summary>
    public decimal EstimatedAmount { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

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
/// 采购申请明细表导出DTO
/// </summary>
public partial class TaktPurchaseRequestItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemExportDto()
    {
        CreatedAt = DateTime.Now;
        RequestCode = string.Empty;
        MaterialCode = string.Empty;
        MaterialName = string.Empty;
        RequestUnit = string.Empty;
    }

        /// <summary>
    /// 申请ID
    /// </summary>
    public long RequestId { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 物料ID
    /// </summary>
    public long MaterialId { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }

        /// <summary>
    /// 物料规格
    /// </summary>
    public string? MaterialSpecification { get; set; }

        /// <summary>
    /// 申请单位
    /// </summary>
    public string RequestUnit { get; set; }

        /// <summary>
    /// 申请数量
    /// </summary>
    public decimal RequestQuantity { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 预计单价
    /// </summary>
    public decimal EstimatedUnitPrice { get; set; }

        /// <summary>
    /// 预计金额
    /// </summary>
    public decimal EstimatedAmount { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}