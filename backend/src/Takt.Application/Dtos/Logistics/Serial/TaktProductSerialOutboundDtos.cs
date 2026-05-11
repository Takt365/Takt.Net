// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Serial
// 文件名称：TaktProductSerialOutboundDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：产品序列号出库表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Serial;

/// <summary>
/// 产品序列号出库表Dto
/// </summary>
public partial class TaktProductSerialOutboundDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundDto()
    {
        PlantCode = string.Empty;
        OutboundNo = string.Empty;
        ShippingInvoiceNo = string.Empty;
        Destination = string.Empty;
        ShippingMethod = string.Empty;
        DestinationPort = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

    /// <summary>
    /// 产品序列号出库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialOutboundId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 出库单号
    /// </summary>
    public string OutboundNo { get; set; }
    /// <summary>
    /// 出货发票号
    /// </summary>
    public string ShippingInvoiceNo { get; set; }
    /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime OutboundDate { get; set; }
    /// <summary>
    /// 仕向地
    /// </summary>
    public string Destination { get; set; }
    /// <summary>
    /// 运输方式
    /// </summary>
    public string ShippingMethod { get; set; }
    /// <summary>
    /// 目的地港
    /// </summary>
    public string DestinationPort { get; set; }
    /// <summary>
    /// 出库类型
    /// </summary>
    public int OutboundType { get; set; }
    /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }
    /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }
    /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// 产品序列号出库明细列表（主子表关系）（外键在子表 TaktProductSerialOutboundItemDto.OutboundId）
    /// </summary>
    public List<TaktProductSerialOutboundItemDto>? Items { get; set; }
}

/// <summary>
/// 产品序列号出库表查询DTO
/// </summary>
public partial class TaktProductSerialOutboundQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundNo { get; set; }
    /// <summary>
    /// 出货发票号
    /// </summary>
    public string? ShippingInvoiceNo { get; set; }
    /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }

    /// <summary>
    /// 出库日期开始时间
    /// </summary>
    public DateTime? OutboundDateStart { get; set; }
    /// <summary>
    /// 出库日期结束时间
    /// </summary>
    public DateTime? OutboundDateEnd { get; set; }
    /// <summary>
    /// 仕向地
    /// </summary>
    public string? Destination { get; set; }
    /// <summary>
    /// 运输方式
    /// </summary>
    public string? ShippingMethod { get; set; }
    /// <summary>
    /// 目的地港
    /// </summary>
    public string? DestinationPort { get; set; }
    /// <summary>
    /// 出库类型
    /// </summary>
    public int? OutboundType { get; set; }
    /// <summary>
    /// 仓库编码
    /// </summary>
    public string? WarehouseCode { get; set; }
    /// <summary>
    /// 库位编码
    /// </summary>
    public string? LocationCode { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }
    /// <summary>
    /// 总数量
    /// </summary>
    public int? TotalQuantity { get; set; }

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
/// Takt创建产品序列号出库表DTO
/// </summary>
public partial class TaktProductSerialOutboundCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundCreateDto()
    {
        PlantCode = string.Empty;
        OutboundNo = string.Empty;
        ShippingInvoiceNo = string.Empty;
        Destination = string.Empty;
        ShippingMethod = string.Empty;
        DestinationPort = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string OutboundNo { get; set; }

        /// <summary>
    /// 出货发票号
    /// </summary>
    public string ShippingInvoiceNo { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime OutboundDate { get; set; }

        /// <summary>
    /// 仕向地
    /// </summary>
    public string Destination { get; set; }

        /// <summary>
    /// 运输方式
    /// </summary>
    public string ShippingMethod { get; set; }

        /// <summary>
    /// 目的地港
    /// </summary>
    public string DestinationPort { get; set; }

        /// <summary>
    /// 出库类型
    /// </summary>
    public int OutboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 产品序列号出库明细列表（主子表关系）（外键在子表 TaktProductSerialOutboundItemCreateDto.OutboundId）
    /// </summary>
    public List<TaktProductSerialOutboundItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新产品序列号出库表DTO
/// </summary>
public partial class TaktProductSerialOutboundUpdateDto : TaktProductSerialOutboundCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundUpdateDto()
    {
    }

        /// <summary>
    /// 产品序列号出库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialOutboundId { get; set; } = 0;
}

/// <summary>
/// 产品序列号出库表导入模板DTO
/// </summary>
public partial class TaktProductSerialOutboundTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundTemplateDto()
    {
        PlantCode = string.Empty;
        OutboundNo = string.Empty;
        ShippingInvoiceNo = string.Empty;
        Destination = string.Empty;
        ShippingMethod = string.Empty;
        DestinationPort = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string OutboundNo { get; set; }

        /// <summary>
    /// 出货发票号
    /// </summary>
    public string ShippingInvoiceNo { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime OutboundDate { get; set; }

        /// <summary>
    /// 仕向地
    /// </summary>
    public string Destination { get; set; }

        /// <summary>
    /// 运输方式
    /// </summary>
    public string ShippingMethod { get; set; }

        /// <summary>
    /// 目的地港
    /// </summary>
    public string DestinationPort { get; set; }

        /// <summary>
    /// 出库类型
    /// </summary>
    public int OutboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

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
/// 产品序列号出库表导入DTO
/// </summary>
public partial class TaktProductSerialOutboundImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundImportDto()
    {
        PlantCode = string.Empty;
        OutboundNo = string.Empty;
        ShippingInvoiceNo = string.Empty;
        Destination = string.Empty;
        ShippingMethod = string.Empty;
        DestinationPort = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string OutboundNo { get; set; }

        /// <summary>
    /// 出货发票号
    /// </summary>
    public string ShippingInvoiceNo { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime OutboundDate { get; set; }

        /// <summary>
    /// 仕向地
    /// </summary>
    public string Destination { get; set; }

        /// <summary>
    /// 运输方式
    /// </summary>
    public string ShippingMethod { get; set; }

        /// <summary>
    /// 目的地港
    /// </summary>
    public string DestinationPort { get; set; }

        /// <summary>
    /// 出库类型
    /// </summary>
    public int OutboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

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
/// 产品序列号出库表导出DTO
/// </summary>
public partial class TaktProductSerialOutboundExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        OutboundNo = string.Empty;
        ShippingInvoiceNo = string.Empty;
        Destination = string.Empty;
        ShippingMethod = string.Empty;
        DestinationPort = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string OutboundNo { get; set; }

        /// <summary>
    /// 出货发票号
    /// </summary>
    public string ShippingInvoiceNo { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime OutboundDate { get; set; }

        /// <summary>
    /// 仕向地
    /// </summary>
    public string Destination { get; set; }

        /// <summary>
    /// 运输方式
    /// </summary>
    public string ShippingMethod { get; set; }

        /// <summary>
    /// 目的地港
    /// </summary>
    public string DestinationPort { get; set; }

        /// <summary>
    /// 出库类型
    /// </summary>
    public int OutboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}