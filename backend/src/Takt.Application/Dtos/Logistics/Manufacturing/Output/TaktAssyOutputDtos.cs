// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：组立日报表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报表Dto
/// </summary>
public partial class TaktAssyOutputDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputDto()
    {
        PlantCode = string.Empty;
        ProdCategory = string.Empty;
        ProdLine = string.Empty;
        ProdOrderCode = string.Empty;
        ModelCode = string.Empty;
        MaterialCode = string.Empty;
    }

    /// <summary>
    /// 组立日报表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 生产类别
    /// </summary>
    public string ProdCategory { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProdDate { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string ProdLine { get; set; }
    /// <summary>
    /// 直接人员
    /// </summary>
    public int DirectLabor { get; set; }
    /// <summary>
    /// 间接人员
    /// </summary>
    public int IndirectLabor { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }
    /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }
    /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string ModelCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 订单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }
    /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal StdMinutes { get; set; }
    /// <summary>
    /// 标准产能
    /// </summary>
    public decimal StdCapacity { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 组立日报明细ID列表
    /// </summary>
    public List<long>? AssyOutputDetailIds { get; set; }
}

/// <summary>
/// 组立日报表查询DTO
/// </summary>
public partial class TaktAssyOutputQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 组立日报表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 生产类别
    /// </summary>
    public string? ProdCategory { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProdDate { get; set; }

    /// <summary>
    /// 生产日期开始时间
    /// </summary>
    public DateTime? ProdDateStart { get; set; }
    /// <summary>
    /// 生产日期结束时间
    /// </summary>
    public DateTime? ProdDateEnd { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 直接人员
    /// </summary>
    public int? DirectLabor { get; set; }
    /// <summary>
    /// 间接人员
    /// </summary>
    public int? IndirectLabor { get; set; }
    /// <summary>
    /// 班次
    /// </summary>
    public int? ShiftNo { get; set; }
    /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }
    /// <summary>
    /// 生产工单号
    /// </summary>
    public string? ProdOrderCode { get; set; }
    /// <summary>
    /// 机种
    /// </summary>
    public string? ModelCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }
    /// <summary>
    /// 订单数量
    /// </summary>
    public decimal? ProdOrderQty { get; set; }
    /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal? StdMinutes { get; set; }
    /// <summary>
    /// 标准产能
    /// </summary>
    public decimal? StdCapacity { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建组立日报表DTO
/// </summary>
public partial class TaktAssyOutputCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputCreateDto()
    {
        PlantCode = string.Empty;
        ProdCategory = string.Empty;
        ProdLine = string.Empty;
        ProdOrderCode = string.Empty;
        ModelCode = string.Empty;
        MaterialCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string ProdCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProdDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProdLine { get; set; }

        /// <summary>
    /// 直接人员
    /// </summary>
    public int DirectLabor { get; set; }

        /// <summary>
    /// 间接人员
    /// </summary>
    public int IndirectLabor { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string ModelCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 订单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal StdMinutes { get; set; }

        /// <summary>
    /// 标准产能
    /// </summary>
    public decimal StdCapacity { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新组立日报表DTO
/// </summary>
public partial class TaktAssyOutputUpdateDto : TaktAssyOutputCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputUpdateDto()
    {
    }

        /// <summary>
    /// 组立日报表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }
}

/// <summary>
/// 组立日报表状态DTO
/// </summary>
public partial class TaktAssyOutputStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputStatusDto()
    {
    }

        /// <summary>
    /// 组立日报表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssyOutputId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 组立日报表导入模板DTO
/// </summary>
public partial class TaktAssyOutputTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputTemplateDto()
    {
        PlantCode = string.Empty;
        ProdCategory = string.Empty;
        ProdLine = string.Empty;
        ProdOrderCode = string.Empty;
        ModelCode = string.Empty;
        MaterialCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string ProdCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProdDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProdLine { get; set; }

        /// <summary>
    /// 直接人员
    /// </summary>
    public int DirectLabor { get; set; }

        /// <summary>
    /// 间接人员
    /// </summary>
    public int IndirectLabor { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string ModelCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 订单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal StdMinutes { get; set; }

        /// <summary>
    /// 标准产能
    /// </summary>
    public decimal StdCapacity { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 组立日报表导入DTO
/// </summary>
public partial class TaktAssyOutputImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputImportDto()
    {
        PlantCode = string.Empty;
        ProdCategory = string.Empty;
        ProdLine = string.Empty;
        ProdOrderCode = string.Empty;
        ModelCode = string.Empty;
        MaterialCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string ProdCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProdDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProdLine { get; set; }

        /// <summary>
    /// 直接人员
    /// </summary>
    public int DirectLabor { get; set; }

        /// <summary>
    /// 间接人员
    /// </summary>
    public int IndirectLabor { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string ModelCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 订单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal StdMinutes { get; set; }

        /// <summary>
    /// 标准产能
    /// </summary>
    public decimal StdCapacity { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 组立日报表导出DTO
/// </summary>
public partial class TaktAssyOutputExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAssyOutputExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        ProdCategory = string.Empty;
        ProdLine = string.Empty;
        ProdOrderCode = string.Empty;
        ModelCode = string.Empty;
        MaterialCode = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产类别
    /// </summary>
    public string ProdCategory { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime ProdDate { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string ProdLine { get; set; }

        /// <summary>
    /// 直接人员
    /// </summary>
    public int DirectLabor { get; set; }

        /// <summary>
    /// 间接人员
    /// </summary>
    public int IndirectLabor { get; set; }

        /// <summary>
    /// 班次
    /// </summary>
    public int ShiftNo { get; set; }

        /// <summary>
    /// 生产订单类型
    /// </summary>
    public string? ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 机种
    /// </summary>
    public string ModelCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 批次
    /// </summary>
    public string? BatchNo { get; set; }

        /// <summary>
    /// 订单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 标准工时(分钟)
    /// </summary>
    public decimal StdMinutes { get; set; }

        /// <summary>
    /// 标准产能
    /// </summary>
    public decimal StdCapacity { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}