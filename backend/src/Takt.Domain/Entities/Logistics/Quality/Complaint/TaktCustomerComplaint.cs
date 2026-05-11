// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaint.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉主表实体，记录客户投诉基本信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 客诉主表实体
/// </summary>
[SugarTable("takt_logistics_quality_customer_complaint", "客诉主表")]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_complaint_unique", nameof(CompanyCode), OrderByType.Asc, nameof(CustomerComplaintCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_complaint_status", nameof(ComplaintStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_customer_id", nameof(CustomerId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerComplaint : TaktEntityBase
{
    /// <summary>
    /// 公司代码（4位字母数字组合）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 客诉单号（组合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "customer_complaint_code", ColumnDescription = "客诉单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerComplaintCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "customer_id", ColumnDescription = "客户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CustomerId { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerCode { get; set; }

    /// <summary>
    /// 投诉日期
    /// </summary>
    [SugarColumn(ColumnName = "complaint_date", ColumnDescription = "投诉日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ComplaintDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 投诉方式（0=电话，1=邮件，2=传真，3=现场，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_method", ColumnDescription = "投诉方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ComplaintMethod { get; set; } = 0;

    /// <summary>
    /// 投诉类型（0=质量，1=交期，2=服务，3=价格，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_type", ColumnDescription = "投诉类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ComplaintType { get; set; } = 0;

    /// <summary>
    /// 投诉等级（0=一般，1=重要，2=紧急，3=严重）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_level", ColumnDescription = "投诉等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ComplaintLevel { get; set; } = 0;

    /// <summary>
    /// 责任部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "responsible_dept_id", ColumnDescription = "责任部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ResponsibleDeptId { get; set; }

    /// <summary>
    /// 责任部门名称
    /// </summary>
    [SugarColumn(ColumnName = "responsible_dept_name", ColumnDescription = "责任部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ResponsibleDeptName { get; set; }

    /// <summary>
    /// 责任人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "responsible_person_id", ColumnDescription = "责任人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ResponsiblePersonId { get; set; }

    /// <summary>
    /// 责任人姓名
    /// </summary>
    [SugarColumn(ColumnName = "responsible_person_name", ColumnDescription = "责任人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ResponsiblePersonName { get; set; }

    /// <summary>
    /// 要求回复日期
    /// </summary>
    [SugarColumn(ColumnName = "required_reply_date", ColumnDescription = "要求回复日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RequiredReplyDate { get; set; }

    /// <summary>
    /// 实际回复日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_reply_date", ColumnDescription = "实际回复日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualReplyDate { get; set; }

    /// <summary>
    /// 客诉状态（0=待处理，1=处理中，2=已回复，3=已关闭，4=已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_status", ColumnDescription = "客诉状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ComplaintStatus { get; set; } = 0;

    /// <summary>
    /// 客诉描述
    /// </summary>
    [SugarColumn(ColumnName = "complaint_description", ColumnDescription = "客诉描述", ColumnDataType = "nvarchar", Length = 2000, IsNullable = false)]
    public string ComplaintDescription { get; set; } = string.Empty;

    /// <summary>
    /// 处理结果/回复内容
    /// </summary>
    [SugarColumn(ColumnName = "handling_result", ColumnDescription = "处理结果", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? HandlingResult { get; set; }

    /// <summary>
    /// 客户满意度（0=不满意，1=一般，2=满意，3=非常满意）
    /// </summary>
    [SugarColumn(ColumnName = "customer_satisfaction", ColumnDescription = "客户满意度", ColumnDataType = "int", IsNullable = true)]
    public int? CustomerSatisfaction { get; set; }

    /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 客诉明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktCustomerComplaintItem.ComplaintId))]
    public List<TaktCustomerComplaintItem>? Items { get; set; }
}
