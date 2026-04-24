// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.Output
// 文件名称：TaktProductionOrderDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：生产工单表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Output;

/// <summary>
/// 生产工单表Dto
/// </summary>
public partial class TaktProductionOrderDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderDto()
    {
        PlantCode = string.Empty;
        ProdOrderType = string.Empty;
        ProdOrderCode = string.Empty;
        MaterialCode = string.Empty;
        UnitOfMeasure = string.Empty;
    }

    /// <summary>
    /// 生产工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionOrderId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 生产工单类型
    /// </summary>
    public string ProdOrderType { get; set; }
    /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }
    /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }
    /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal ProducedQty { get; set; }
    /// <summary>
    /// 计量单位
    /// </summary>
    public string UnitOfMeasure { get; set; }
    /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }
    /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }
    /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 生产工单表查询DTO
/// </summary>
public partial class TaktProductionOrderQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 生产工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionOrderId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 生产工单类型
    /// </summary>
    public string? ProdOrderType { get; set; }
    /// <summary>
    /// 生产工单号
    /// </summary>
    public string? ProdOrderCode { get; set; }
    /// <summary>
    /// 物料编码
    /// </summary>
    public string? MaterialCode { get; set; }
    /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal? ProdOrderQty { get; set; }
    /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal? ProducedQty { get; set; }
    /// <summary>
    /// 计量单位
    /// </summary>
    public string? UnitOfMeasure { get; set; }
    /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

    /// <summary>
    /// 实际开始日期开始时间
    /// </summary>
    public DateTime? ActualStartDateStart { get; set; }
    /// <summary>
    /// 实际开始日期结束时间
    /// </summary>
    public DateTime? ActualStartDateEnd { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

    /// <summary>
    /// 实际完成日期开始时间
    /// </summary>
    public DateTime? ActualEndDateStart { get; set; }
    /// <summary>
    /// 实际完成日期结束时间
    /// </summary>
    public DateTime? ActualEndDateEnd { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int? Priority { get; set; }
    /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }
    /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }
    /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }
    /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }
    /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }
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
/// Takt创建生产工单表DTO
/// </summary>
public partial class TaktProductionOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderCreateDto()
    {
        PlantCode = string.Empty;
        ProdOrderType = string.Empty;
        ProdOrderCode = string.Empty;
        MaterialCode = string.Empty;
        UnitOfMeasure = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产工单类型
    /// </summary>
    public string ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal ProducedQty { get; set; }

        /// <summary>
    /// 计量单位
    /// </summary>
    public string UnitOfMeasure { get; set; }

        /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }

        /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }

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
/// Takt更新生产工单表DTO
/// </summary>
public partial class TaktProductionOrderUpdateDto : TaktProductionOrderCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderUpdateDto()
    {
    }

        /// <summary>
    /// 生产工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionOrderId { get; set; }
}

/// <summary>
/// 生产工单表状态DTO
/// </summary>
public partial class TaktProductionOrderStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderStatusDto()
    {
    }

        /// <summary>
    /// 生产工单表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ProductionOrderId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 生产工单表导入模板DTO
/// </summary>
public partial class TaktProductionOrderTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderTemplateDto()
    {
        PlantCode = string.Empty;
        ProdOrderType = string.Empty;
        ProdOrderCode = string.Empty;
        MaterialCode = string.Empty;
        UnitOfMeasure = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产工单类型
    /// </summary>
    public string ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal ProducedQty { get; set; }

        /// <summary>
    /// 计量单位
    /// </summary>
    public string UnitOfMeasure { get; set; }

        /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }

        /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }

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
/// 生产工单表导入DTO
/// </summary>
public partial class TaktProductionOrderImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderImportDto()
    {
        PlantCode = string.Empty;
        ProdOrderType = string.Empty;
        ProdOrderCode = string.Empty;
        MaterialCode = string.Empty;
        UnitOfMeasure = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产工单类型
    /// </summary>
    public string ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal ProducedQty { get; set; }

        /// <summary>
    /// 计量单位
    /// </summary>
    public string UnitOfMeasure { get; set; }

        /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }

        /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }

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
/// 生产工单表导出DTO
/// </summary>
public partial class TaktProductionOrderExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrderExportDto()
    {
        CreatedAt = DateTime.Now;
        PlantCode = string.Empty;
        ProdOrderType = string.Empty;
        ProdOrderCode = string.Empty;
        MaterialCode = string.Empty;
        UnitOfMeasure = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 生产工单类型
    /// </summary>
    public string ProdOrderType { get; set; }

        /// <summary>
    /// 生产工单号
    /// </summary>
    public string ProdOrderCode { get; set; }

        /// <summary>
    /// 物料编码
    /// </summary>
    public string MaterialCode { get; set; }

        /// <summary>
    /// 生产工单数量
    /// </summary>
    public decimal ProdOrderQty { get; set; }

        /// <summary>
    /// 已生产数量
    /// </summary>
    public decimal ProducedQty { get; set; }

        /// <summary>
    /// 计量单位
    /// </summary>
    public string UnitOfMeasure { get; set; }

        /// <summary>
    /// 实际开始日期
    /// </summary>
    public DateTime? ActualStartDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualEndDate { get; set; }

        /// <summary>
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 工作中心
    /// </summary>
    public string? WorkCenter { get; set; }

        /// <summary>
    /// 生产线
    /// </summary>
    public string? ProdLine { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProdBatch { get; set; }

        /// <summary>
    /// 序列号
    /// </summary>
    public string? SerialNo { get; set; }

        /// <summary>
    /// 工艺路线编码
    /// </summary>
    public string? RoutingCode { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}