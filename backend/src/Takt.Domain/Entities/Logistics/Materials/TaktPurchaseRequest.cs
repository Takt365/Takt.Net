// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktPurchaseRequest.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购申请实体，定义采购申请领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购申请实体
/// </summary>
[SugarTable("takt_logistics_materials_purchase_request", "采购申请表")]
[SugarIndex("ix_takt_logistics_materials_purchase_request_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_request_code", nameof(RequestCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_request_date", nameof(RequestDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_request_status", nameof(RequestStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_request_user_id", nameof(RequestUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchaseRequest : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 申请编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "request_code", ColumnDescription = "申请编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RequestCode { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "request_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime RequestDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    [SugarColumn(ColumnName = "required_arrival_date", ColumnDescription = "要求到货日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 申请人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "request_user_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RequestUserId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    [SugarColumn(ColumnName = "request_user_name", ColumnDescription = "申请人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RequestUserName { get; set; } = string.Empty;

    /// <summary>
    /// 申请部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "申请部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 申请总数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "申请总数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 申请总金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "total_amount", ColumnDescription = "申请总金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// 已转订单数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "converted_quantity", ColumnDescription = "已转订单数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ConvertedQuantity { get; set; } = 0;

    /// <summary>
    /// 已转订单金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "converted_amount", ColumnDescription = "已转订单金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ConvertedAmount { get; set; } = 0;

    /// <summary>
    /// 申请状态（0=草稿，1=待审核，2=已审核，3=已转订单，4=已取消，5=已关闭）
    /// </summary>
    [SugarColumn(ColumnName = "request_status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RequestStatus { get; set; } = 0;

    /// <summary>
    /// 审核人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审核人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApproverId { get; set; }

    /// <summary>
    /// 审核人姓名
    /// </summary>
    [SugarColumn(ColumnName = "approver_name", ColumnDescription = "审核人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ApproverName { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    [SugarColumn(ColumnName = "approve_time", ColumnDescription = "审核时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApproveTime { get; set; }

    /// <summary>
    /// 审核意见
    /// </summary>
    [SugarColumn(ColumnName = "approve_comment", ColumnDescription = "审核意见", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ApproveComment { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    [SugarColumn(ColumnName = "request_reason", ColumnDescription = "申请原因", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? RequestReason { get; set; }
}
