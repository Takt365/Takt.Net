// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Serial
// 文件名称：TaktProductSerialInboundDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：产品序列号入库表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Serial;

/// <summary>
/// 产品序列号入库表Dto
/// </summary>
public partial class TaktProductSerialInboundDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundDto()
    {
        PlantCode = string.Empty;
        InboundNo = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

    /// <summary>
    /// 产品序列号入库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialInboundId { get; set; } = 0;

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }
    /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime InboundDate { get; set; }
    /// <summary>
    /// 入库类型
    /// </summary>
    public int InboundType { get; set; }
    /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }
    /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }
    /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

    /// <summary>
    /// 产品序列号入库明细列表(主子表关系)（外键在子表 TaktProductSerialInboundItemDto.InboundId）
    /// </summary>
    public List<TaktProductSerialInboundItemDto>? Items { get; set; }
}

/// <summary>
/// 产品序列号入库表查询DTO
/// </summary>
public partial class TaktProductSerialInboundQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 入库单号
    /// </summary>
    public string? InboundNo { get; set; }
    /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime? InboundDate { get; set; }

    /// <summary>
    /// 入库日期开始时间
    /// </summary>
    public DateTime? InboundDateStart { get; set; }
    /// <summary>
    /// 入库日期结束时间
    /// </summary>
    public DateTime? InboundDateEnd { get; set; }
    /// <summary>
    /// 入库类型
    /// </summary>
    public int? InboundType { get; set; }
    /// <summary>
    /// 仓库编码
    /// </summary>
    public string? WarehouseCode { get; set; }
    /// <summary>
    /// 库位编码
    /// </summary>
    public string? LocationCode { get; set; }
    /// <summary>
    /// 总数量
    /// </summary>
    public int? TotalQuantity { get; set; }
    /// <summary>
    /// 关联公司
    /// </summary>
    public string? RelatedCompany { get; set; }

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
/// Takt创建产品序列号入库表DTO
/// </summary>
public partial class TaktProductSerialInboundCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundCreateDto()
    {
        PlantCode = string.Empty;
        InboundNo = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime InboundDate { get; set; }

        /// <summary>
    /// 入库类型
    /// </summary>
    public int InboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }


    /// <summary>
    /// 产品序列号入库明细列表(主子表关系)（外键在子表 TaktProductSerialInboundItemCreateDto.InboundId）
    /// </summary>
    public List<TaktProductSerialInboundItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新产品序列号入库表DTO
/// </summary>
public partial class TaktProductSerialInboundUpdateDto : TaktProductSerialInboundCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundUpdateDto()
    {
    }

        /// <summary>
    /// 产品序列号入库表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductSerialInboundId { get; set; } = 0;
}

/// <summary>
/// 产品序列号入库表导入模板DTO
/// </summary>
public partial class TaktProductSerialInboundTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundTemplateDto()
    {
        PlantCode = string.Empty;
        InboundNo = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime InboundDate { get; set; }

        /// <summary>
    /// 入库类型
    /// </summary>
    public int InboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

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
/// 产品序列号入库表导入DTO
/// </summary>
public partial class TaktProductSerialInboundImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundImportDto()
    {
        PlantCode = string.Empty;
        InboundNo = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime InboundDate { get; set; }

        /// <summary>
    /// 入库类型
    /// </summary>
    public int InboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

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
/// 产品序列号入库表导出DTO
/// </summary>
public partial class TaktProductSerialInboundExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        InboundNo = string.Empty;
        WarehouseCode = string.Empty;
        LocationCode = string.Empty;
        RelatedCompany = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 入库单号
    /// </summary>
    public string InboundNo { get; set; }

        /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime InboundDate { get; set; }

        /// <summary>
    /// 入库类型
    /// </summary>
    public int InboundType { get; set; }

        /// <summary>
    /// 仓库编码
    /// </summary>
    public string WarehouseCode { get; set; }

        /// <summary>
    /// 库位编码
    /// </summary>
    public string LocationCode { get; set; }

        /// <summary>
    /// 总数量
    /// </summary>
    public int TotalQuantity { get; set; }

        /// <summary>
    /// 关联公司
    /// </summary>
    public string RelatedCompany { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}