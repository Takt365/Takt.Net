// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeWork.cs
// 创建时间：2025-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工工作经历实体，定义员工履历领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工工作经历实体
/// </summary>
[SugarTable("takt_humanresource_employee_work", "员工工作履历表")]
[SugarIndex("ix_takt_humanresource_employee_work_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_work_start_date", nameof(StartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_work_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_work_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeWork : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 单位名称
    /// </summary>
    [SugarColumn(ColumnName = "company_name", ColumnDescription = "单位名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "position_name", ColumnDescription = "岗位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PositionName { get; set; }

    /// <summary>
    /// 工作内容
    /// </summary>
    [SugarColumn(ColumnName = "job_content", ColumnDescription = "工作内容", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? JobContent { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 证明人
    /// </summary>
    [SugarColumn(ColumnName = "witness_name", ColumnDescription = "证明人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? WitnessName { get; set; }

    /// <summary>
    /// 证明人电话
    /// </summary>
    [SugarColumn(ColumnName = "witness_phone", ColumnDescription = "证明人电话", ColumnDataType = "nvarchar", Length = 30, IsNullable = true)]
    public string? WitnessPhone { get; set; }
}
