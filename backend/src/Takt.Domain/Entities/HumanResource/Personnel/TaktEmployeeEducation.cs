// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeEducation.cs
// 创建时间：2025-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工教育经历实体，定义员工教育背景领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工教育经历实体
/// </summary>
[SugarTable("takt_humanresource_employee_education", "员工教育经历表")]
[SugarIndex("ix_takt_humanresource_employee_education_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_education_is_highest", nameof(IsHighest), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_education_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_education_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeEducation : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 学历层次（0=其他，1=高中及以下，2=大专，3=本科，4=硕士，5=博士）
    /// </summary>
    [SugarColumn(ColumnName = "education_level", ColumnDescription = "学历层次", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EducationLevel { get; set; } = 0;

    /// <summary>
    /// 学校名称
    /// </summary>
    [SugarColumn(ColumnName = "school_name", ColumnDescription = "学校名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SchoolName { get; set; } = string.Empty;

    /// <summary>
    /// 专业
    /// </summary>
    [SugarColumn(ColumnName = "major_name", ColumnDescription = "专业", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? MajorName { get; set; }

    /// <summary>
    /// 学位（0=无，1=学士，2=硕士，3=博士）
    /// </summary>
    [SugarColumn(ColumnName = "degree_level", ColumnDescription = "学位", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DegreeLevel { get; set; } = 0;

    /// <summary>
    /// 入学日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "入学日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "毕业日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否最高学历（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_highest", ColumnDescription = "是否最高学历", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsHighest { get; set; } = 0;

    /// <summary>
    /// 证书编号
    /// </summary>
    [SugarColumn(ColumnName = "certificate_no", ColumnDescription = "证书编号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CertificateNo { get; set; }
}
