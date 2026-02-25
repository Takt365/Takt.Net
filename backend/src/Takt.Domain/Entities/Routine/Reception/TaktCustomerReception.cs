// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Reception
// 文件名称：TaktCustomerReception.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：客户接待实体，定义客户接待领域模型（来访客户、来访时间、接待人、接待部门等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Reception;

/// <summary>
/// 客户接待实体
/// </summary>
/// <remarks>用于日常事务中的客户来访接待登记，接待单编码可由单据编码规则生成。</remarks>
[SugarTable("takt_routine_customer_reception", "客户接待表")]
[SugarIndex("ix_takt_routine_customer_reception_reception_code", nameof(ReceptionCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_customer_reception_visit_time", nameof(VisitTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_customer_reception_reception_status", nameof(ReceptionStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_customer_reception_reception_user_id", nameof(ReceptionUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_customer_reception_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_customer_reception_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_customer_reception_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerReception : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码（关联工厂主数据 TaktPlant.PlantCode；冗余便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 接待单编码（唯一索引，可由单据编码规则生成）
    /// </summary>
    [SugarColumn(ColumnName = "reception_code", ColumnDescription = "接待单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ReceptionCode { get; set; } = string.Empty;

    /// <summary>
    /// 来访客户/单位名称
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "来访客户名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 来访单位（公司名称，可为空表示个人来访）
    /// </summary>
    [SugarColumn(ColumnName = "customer_company", ColumnDescription = "来访单位", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CustomerCompany { get; set; }

    /// <summary>
    /// 来访时间
    /// </summary>
    [SugarColumn(ColumnName = "visit_time", ColumnDescription = "来访时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime VisitTime { get; set; }

    /// <summary>
    /// 来访事由/目的
    /// </summary>
    [SugarColumn(ColumnName = "visit_purpose", ColumnDescription = "来访事由", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? VisitPurpose { get; set; }

    /// <summary>
    /// 接待部门ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "reception_dept_id", ColumnDescription = "接待部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceptionDeptId { get; set; }

    /// <summary>
    /// 接待部门名称
    /// </summary>
    [SugarColumn(ColumnName = "reception_dept_name", ColumnDescription = "接待部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ReceptionDeptName { get; set; }

    /// <summary>
    /// 接待人ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "reception_user_id", ColumnDescription = "接待人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceptionUserId { get; set; }

    /// <summary>
    /// 接待人姓名
    /// </summary>
    [SugarColumn(ColumnName = "reception_user_name", ColumnDescription = "接待人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ReceptionUserName { get; set; }

    /// <summary>
    /// 接待状态（0=待接待，1=接待中，2=已结束，3=已取消）
    /// </summary>
    [SugarColumn(ColumnName = "reception_status", ColumnDescription = "接待状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReceptionStatus { get; set; } = 0;

    /// <summary>
    /// 联系人/电话（来访方联系方式）
    /// </summary>
    [SugarColumn(ColumnName = "contact_info", ColumnDescription = "联系人/电话", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ContactInfo { get; set; }

    /// <summary>
    /// 来访人数
    /// </summary>
    [SugarColumn(ColumnName = "visitor_count", ColumnDescription = "来访人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int VisitorCount { get; set; } = 1;
}
