// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Materials
// 文件名称：TaktPurchaseRequestDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：采购申请表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Materials;

/// <summary>
/// 采购申请表Dto
/// </summary>
public partial class TaktPurchaseRequestDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestDto()
    {
        RequestCode = string.Empty;
        RequestUserName = string.Empty;
        DeptName = string.Empty;
    }

    /// <summary>
    /// 采购申请表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime RequestDate { get; set; }
    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }
    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RequestUserId { get; set; }
    /// <summary>
    /// 申请人姓名
    /// </summary>
    public string RequestUserName { get; set; }
    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }
    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string DeptName { get; set; }
    /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }
    /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }
    /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal ConvertedAmount { get; set; }
    /// <summary>
    /// 申请状态
    /// </summary>
    public int RequestStatus { get; set; }
    /// <summary>
    /// 审核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
    /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }
    /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }
    /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }
}

/// <summary>
/// 采购申请表查询DTO
/// </summary>
public partial class TaktPurchaseRequestQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 采购申请表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestId { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 申请编码
    /// </summary>
    public string? RequestCode { get; set; }
    /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime? RequestDate { get; set; }

    /// <summary>
    /// 申请日期开始时间
    /// </summary>
    public DateTime? RequestDateStart { get; set; }
    /// <summary>
    /// 申请日期结束时间
    /// </summary>
    public DateTime? RequestDateEnd { get; set; }
    /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 要求到货日期开始时间
    /// </summary>
    public DateTime? RequiredArrivalDateStart { get; set; }
    /// <summary>
    /// 要求到货日期结束时间
    /// </summary>
    public DateTime? RequiredArrivalDateEnd { get; set; }
    /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RequestUserId { get; set; }
    /// <summary>
    /// 申请人姓名
    /// </summary>
    public string? RequestUserName { get; set; }
    /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 申请部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal? TotalQuantity { get; set; }
    /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal? TotalAmount { get; set; }
    /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal? ConvertedQuantity { get; set; }
    /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal? ConvertedAmount { get; set; }
    /// <summary>
    /// 申请状态
    /// </summary>
    public int? RequestStatus { get; set; }
    /// <summary>
    /// 审核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
    /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }
    /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

    /// <summary>
    /// 审核时间开始时间
    /// </summary>
    public DateTime? ApproveTimeStart { get; set; }
    /// <summary>
    /// 审核时间结束时间
    /// </summary>
    public DateTime? ApproveTimeEnd { get; set; }
    /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }
    /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }

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
/// Takt创建采购申请表DTO
/// </summary>
public partial class TaktPurchaseRequestCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestCreateDto()
    {
        RequestCode = string.Empty;
        RequestUserName = string.Empty;
        DeptName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime RequestDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RequestUserId { get; set; }

        /// <summary>
    /// 申请人姓名
    /// </summary>
    public string RequestUserName { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal ConvertedAmount { get; set; }

        /// <summary>
    /// 申请状态
    /// </summary>
    public int RequestStatus { get; set; }

        /// <summary>
    /// 审核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }

        /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }

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
/// Takt更新采购申请表DTO
/// </summary>
public partial class TaktPurchaseRequestUpdateDto : TaktPurchaseRequestCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestUpdateDto()
    {
    }

        /// <summary>
    /// 采购申请表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestId { get; set; }
}

/// <summary>
/// 采购申请表申请状态DTO
/// </summary>
public partial class TaktPurchaseRequestStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestStatusDto()
    {
    }

        /// <summary>
    /// 采购申请表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PurchaseRequestId { get; set; }

    /// <summary>
    /// 申请状态（0=禁用，1=启用）
    /// </summary>
    public int RequestStatus { get; set; }
}

/// <summary>
/// 采购申请表导入模板DTO
/// </summary>
public partial class TaktPurchaseRequestTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestTemplateDto()
    {
        RequestCode = string.Empty;
        RequestUserName = string.Empty;
        DeptName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime RequestDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long RequestUserId { get; set; }

        /// <summary>
    /// 申请人姓名
    /// </summary>
    public string RequestUserName { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal ConvertedAmount { get; set; }

        /// <summary>
    /// 申请状态
    /// </summary>
    public int RequestStatus { get; set; }

        /// <summary>
    /// 审核人ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }

        /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }

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
/// 采购申请表导入DTO
/// </summary>
public partial class TaktPurchaseRequestImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestImportDto()
    {
        RequestCode = string.Empty;
        RequestUserName = string.Empty;
        DeptName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime RequestDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long RequestUserId { get; set; }

        /// <summary>
    /// 申请人姓名
    /// </summary>
    public string RequestUserName { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal ConvertedAmount { get; set; }

        /// <summary>
    /// 申请状态
    /// </summary>
    public int RequestStatus { get; set; }

        /// <summary>
    /// 审核人ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }

        /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }

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
/// 采购申请表导出DTO
/// </summary>
public partial class TaktPurchaseRequestExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestExportDto()
    {
        CreatedAt = DateTime.Now;
        RequestCode = string.Empty;
        RequestUserName = string.Empty;
        DeptName = string.Empty;
    }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }

        /// <summary>
    /// 申请编码
    /// </summary>
    public string RequestCode { get; set; }

        /// <summary>
    /// 申请日期
    /// </summary>
    public DateTime RequestDate { get; set; }

        /// <summary>
    /// 要求到货日期
    /// </summary>
    public DateTime? RequiredArrivalDate { get; set; }

        /// <summary>
    /// 申请人ID
    /// </summary>
    public long RequestUserId { get; set; }

        /// <summary>
    /// 申请人姓名
    /// </summary>
    public string RequestUserName { get; set; }

        /// <summary>
    /// 申请部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 申请部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 申请总数量
    /// </summary>
    public decimal TotalQuantity { get; set; }

        /// <summary>
    /// 申请总金额
    /// </summary>
    public decimal TotalAmount { get; set; }

        /// <summary>
    /// 已转订单数量
    /// </summary>
    public decimal ConvertedQuantity { get; set; }

        /// <summary>
    /// 已转订单金额
    /// </summary>
    public decimal ConvertedAmount { get; set; }

        /// <summary>
    /// 申请状态
    /// </summary>
    public int RequestStatus { get; set; }

        /// <summary>
    /// 审核人ID
    /// </summary>
    public long? ApproverId { get; set; }

        /// <summary>
    /// 审核人姓名
    /// </summary>
    public string? ApproverName { get; set; }

        /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? ApproveTime { get; set; }

        /// <summary>
    /// 审核意见
    /// </summary>
    public string? ApproveComment { get; set; }

        /// <summary>
    /// 申请原因
    /// </summary>
    public string? RequestReason { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}