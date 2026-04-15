// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeCareer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工职业信息实体，定义员工职业信息领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工职业信息实体
/// </summary>
[SugarTable("takt_humanresource_employee_career", "员工职业信息表")]
[SugarIndex("ix_takt_humanresource_employee_career_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_career_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_career_post_id", nameof(PostId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_career_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_career_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_career_is_primary", nameof(IsPrimary), OrderByType.Asc)]
public class TaktEmployeeCareer : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "post_name", ColumnDescription = "岗位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    [SugarColumn(ColumnName = "job_level", ColumnDescription = "职级", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    [SugarColumn(ColumnName = "job_title", ColumnDescription = "职位", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    [SugarColumn(ColumnName = "join_date", ColumnDescription = "入职日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    [SugarColumn(ColumnName = "regularization_date", ColumnDescription = "转正日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    [SugarColumn(ColumnName = "leave_date", ColumnDescription = "离职日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 工作年限
    /// </summary>
    [SugarColumn(ColumnName = "work_years", ColumnDescription = "工作年限", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = true)]
    public decimal? WorkYears { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    [SugarColumn(ColumnName = "work_location", ColumnDescription = "工作地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "work_nature", ColumnDescription = "工作性质", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WorkNature { get; set; } = 0;

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    [SugarColumn(ColumnName = "employment_type", ColumnDescription = "用工形式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EmploymentType { get; set; } = 0;

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_primary", ColumnDescription = "是否主职", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPrimary { get; set; } = 0;

    /// <summary>
    /// 直接上级员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "direct_manager_id", ColumnDescription = "直接上级员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DirectManagerId { get; set; }

    /// <summary>
    /// 直接上级姓名
    /// </summary>
    [SugarColumn(ColumnName = "direct_manager_name", ColumnDescription = "直接上级姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DirectManagerName { get; set; }
}
