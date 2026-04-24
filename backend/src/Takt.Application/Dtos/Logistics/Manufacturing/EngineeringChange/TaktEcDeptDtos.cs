// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDeptDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：设变部门表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// 设变部门表Dto
/// </summary>
public partial class TaktEcDeptDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptDto()
    {
        DeptCode = string.Empty;
    }

    /// <summary>
    /// 设变部门表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDeptId { get; set; }

    /// <summary>
    /// 设变明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnDetailId { get; set; }
    /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }
    /// <summary>
    /// 是否实施
    /// </summary>
    public int IsImplemented { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }
    /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }
    /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }
    /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }
    /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }
    /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }
    /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }
    /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }
    /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }
    /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }
    /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }
    /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }
    /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }
    /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }
    /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }
    /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }
    /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }
    /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }
    /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }
    /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int IsSopUpdated { get; set; }
}

/// <summary>
/// 设变部门表查询DTO
/// </summary>
public partial class TaktEcDeptQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 设变部门表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDeptId { get; set; }

    /// <summary>
    /// 设变明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EcnDetailId { get; set; }
    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DeptCode { get; set; }
    /// <summary>
    /// 是否实施
    /// </summary>
    public int? IsImplemented { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
    /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }

    /// <summary>
    /// 预计生产日期开始时间
    /// </summary>
    public DateTime? ScheduledProductionDateStart { get; set; }
    /// <summary>
    /// 预计生产日期结束时间
    /// </summary>
    public DateTime? ScheduledProductionDateEnd { get; set; }
    /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }
    /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }
    /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }
    /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }
    /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }

    /// <summary>
    /// 采购订单发行日期开始时间
    /// </summary>
    public DateTime? PurchaseOrderIssueDateStart { get; set; }
    /// <summary>
    /// 采购订单发行日期结束时间
    /// </summary>
    public DateTime? PurchaseOrderIssueDateEnd { get; set; }
    /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }
    /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }
    /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }
    /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

    /// <summary>
    /// 检验日期开始时间
    /// </summary>
    public DateTime? InspectionDateStart { get; set; }
    /// <summary>
    /// 检验日期结束时间
    /// </summary>
    public DateTime? InspectionDateEnd { get; set; }
    /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }
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
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

    /// <summary>
    /// 生产日期开始时间
    /// </summary>
    public DateTime? ProductionDateStart { get; set; }
    /// <summary>
    /// 生产日期结束时间
    /// </summary>
    public DateTime? ProductionDateEnd { get; set; }
    /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }
    /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }
    /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }
    /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }

    /// <summary>
    /// 实施日期开始时间
    /// </summary>
    public DateTime? ImplementationDateStart { get; set; }
    /// <summary>
    /// 实施日期结束时间
    /// </summary>
    public DateTime? ImplementationDateEnd { get; set; }
    /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }
    /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }
    /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int? IsSopUpdated { get; set; }

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
/// Takt创建设变部门表DTO
/// </summary>
public partial class TaktEcDeptCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptCreateDto()
    {
        DeptCode = string.Empty;
    }

        /// <summary>
    /// 设变明细ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcnDetailId { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

        /// <summary>
    /// 是否实施
    /// </summary>
    public int IsImplemented { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }

        /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }

        /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }

        /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }

        /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }

        /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }

        /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }

        /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }

        /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

        /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }

        /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }

        /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }

        /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }

        /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }

        /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int IsSopUpdated { get; set; }

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
/// Takt更新设变部门表DTO
/// </summary>
public partial class TaktEcDeptUpdateDto : TaktEcDeptCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptUpdateDto()
    {
    }

        /// <summary>
    /// 设变部门表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EcDeptId { get; set; }
}

/// <summary>
/// 设变部门表导入模板DTO
/// </summary>
public partial class TaktEcDeptTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptTemplateDto()
    {
        DeptCode = string.Empty;
    }

        /// <summary>
    /// 设变明细ID
    /// </summary>
    public long EcnDetailId { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

        /// <summary>
    /// 是否实施
    /// </summary>
    public int IsImplemented { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }

        /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }

        /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }

        /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }

        /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }

        /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }

        /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }

        /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }

        /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

        /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }

        /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }

        /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }

        /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }

        /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }

        /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int IsSopUpdated { get; set; }

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
/// 设变部门表导入DTO
/// </summary>
public partial class TaktEcDeptImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptImportDto()
    {
        DeptCode = string.Empty;
    }

        /// <summary>
    /// 设变明细ID
    /// </summary>
    public long EcnDetailId { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

        /// <summary>
    /// 是否实施
    /// </summary>
    public int IsImplemented { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }

        /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }

        /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }

        /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }

        /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }

        /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }

        /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }

        /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }

        /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

        /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }

        /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }

        /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }

        /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }

        /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }

        /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int IsSopUpdated { get; set; }

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
/// 设变部门表导出DTO
/// </summary>
public partial class TaktEcDeptExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEcDeptExportDto()
    {
        CreatedAt = DateTime.Now;
        DeptCode = string.Empty;
    }

        /// <summary>
    /// 设变明细ID
    /// </summary>
    public long EcnDetailId { get; set; }

        /// <summary>
    /// 部门编码
    /// </summary>
    public string DeptCode { get; set; }

        /// <summary>
    /// 是否实施
    /// </summary>
    public int IsImplemented { get; set; }

        /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }

        /// <summary>
    /// 预计生产日期
    /// </summary>
    public DateTime? ScheduledProductionDate { get; set; }

        /// <summary>
    /// 预定批次
    /// </summary>
    public string? ScheduledBatch { get; set; }

        /// <summary>
    /// Po残
    /// </summary>
    public string? PoRemainder { get; set; }

        /// <summary>
    /// 结余
    /// </summary>
    public string? Balance { get; set; }

        /// <summary>
    /// 旧品处理
    /// </summary>
    public string? OldProductHandling { get; set; }

        /// <summary>
    /// 采购订单发行日期
    /// </summary>
    public DateTime? PurchaseOrderIssueDate { get; set; }

        /// <summary>
    /// 供应商
    /// </summary>
    public string? Supplier { get; set; }

        /// <summary>
    /// 采购订单号码
    /// </summary>
    public string? PurchaseOrderNo { get; set; }

        /// <summary>
    /// 受检单号
    /// </summary>
    public string? IqcOrderNo { get; set; }

        /// <summary>
    /// 检验日期
    /// </summary>
    public DateTime? InspectionDate { get; set; }

        /// <summary>
    /// 出库批次
    /// </summary>
    public string? OutboundBatch { get; set; }

        /// <summary>
    /// 出库日期
    /// </summary>
    public DateTime? OutboundDate { get; set; }

        /// <summary>
    /// 生产日期
    /// </summary>
    public DateTime? ProductionDate { get; set; }

        /// <summary>
    /// 生产批次
    /// </summary>
    public string? ProductionBatch { get; set; }

        /// <summary>
    /// 出库单号
    /// </summary>
    public string? OutboundOrderNo { get; set; }

        /// <summary>
    /// 生产班组
    /// </summary>
    public string? ProductionTeam { get; set; }

        /// <summary>
    /// 实施日期
    /// </summary>
    public DateTime? ImplementationDate { get; set; }

        /// <summary>
    /// 检验批次
    /// </summary>
    public string? InspectionBatch { get; set; }

        /// <summary>
    /// 抽样号码
    /// </summary>
    public string? SamplingNo { get; set; }

        /// <summary>
    /// 是否更新SOP
    /// </summary>
    public int IsSopUpdated { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}