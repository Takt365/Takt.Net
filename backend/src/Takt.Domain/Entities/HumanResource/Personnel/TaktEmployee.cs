// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource.Personnel
// 文件名称：TaktEmployee.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工实体，为人事信息权威数据；关联由 TaktUser.EmployeeId 指向本表。紧急联系人在 TaktEmployeeFamily（IsEmergencyContact）维护，本表不存冗余字段。
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工实体（员工档案）
/// 人事相关信息（姓名、性别、联系方式、头像等）以此实体为准；登录账号在 TaktUser，由 TaktUser.EmployeeId 关联本表。
/// 紧急联系人不在本表维护，请在 <see cref="TaktEmployeeFamily"/> 中通过 <see cref="TaktEmployeeFamily.IsEmergencyContact"/> 标记家庭成员为紧急联系人。
/// </summary>
[SugarTable("takt_humanresource_employee", "员工表")]
[SugarIndex("ix_takt_humanresource_employee_employee_code", nameof(EmployeeCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_employee_id_card", nameof(IdCard), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_phone", nameof(Phone), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_email", nameof(Email), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_employee_status", nameof(EmployeeStatus), OrderByType.Asc)]
public class TaktEmployee : TaktEntityBase
{
    /// <summary>
    /// 员工编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "employee_code", ColumnDescription = "员工编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// 实名（身份证/户口本姓名，人事展示主字段）
    /// </summary>
    [SugarColumn(ColumnName = "real_name", ColumnDescription = "实名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 曾用名
    /// </summary>
    [SugarColumn(ColumnName = "former_name", ColumnDescription = "曾用名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FormerName { get; set; }

    /// <summary>
    /// 全名
    /// </summary>
    [SugarColumn(ColumnName = "full_name", ColumnDescription = "全名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FullName { get; set; }

    /// <summary>
    /// 本地化姓名（按地区/界面语言习惯展示的姓名，可与实名不同）
    /// </summary>
    [SugarColumn(ColumnName = "native_name", ColumnDescription = "本地化姓名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? NativeName { get; set; }

    /// <summary>
    /// 显示名
    /// </summary>
    [SugarColumn(ColumnName = "display_name", ColumnDescription = "显示名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Gender { get; set; } = 0;

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "birth_date", ColumnDescription = "出生日期", ColumnDataType = "date", IsNullable = false, DefaultValue = "1900-01-01")]
    public DateTime BirthDate { get; set; } = new DateTime(1900, 1, 1);

    /// <summary>
    /// 年龄
    /// </summary>
    [SugarColumn(ColumnName = "age", ColumnDescription = "年龄", ColumnDataType = "int", IsNullable = true)]
    public int? Age { get; set; }

    /// <summary>
    /// 身份证号（索引）
    /// </summary>
    [SugarColumn(ColumnName = "id_card", ColumnDescription = "身份证号", ColumnDataType = "nvarchar", Length = 18, IsNullable = false, DefaultValue = "")]
    public string IdCard { get; set; } = string.Empty;

    /// <summary>
    /// 手机号（索引）
    /// </summary>
    [SugarColumn(ColumnName = "phone", ColumnDescription = "手机号", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱（索引）
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Email { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [SugarColumn(ColumnName = "nationality", ColumnDescription = "民族", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string Nationality { get; set; } = string.Empty;

    /// <summary>
    /// 政治面貌（与字典 hr_political_status 一致：0=群众，1=共青团员，2=中共党员，3=中共预备党员，4=民革党员，5=民盟盟员，6=民建会员，7=民进会员，8=农工党党员，9=致公党党员，10=九三学社社员，11=台盟盟员，12=无党派民主人士）
    /// </summary>
    [SugarColumn(ColumnName = "political_status", ColumnDescription = "政治面貌", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PoliticalStatus { get; set; } = 0;

    /// <summary>
    /// 婚姻状况（0=未婚，1=已婚，2=离异，3=丧偶）
    /// </summary>
    [SugarColumn(ColumnName = "marital_status", ColumnDescription = "婚姻状况", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaritalStatus { get; set; } = 0;

    /// <summary>
    /// 籍贯
    /// </summary>
    [SugarColumn(ColumnName = "native_place", ColumnDescription = "籍贯", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? NativePlace { get; set; }

    /// <summary>
    /// 现居住地址
    /// </summary>
    [SugarColumn(ColumnName = "current_address", ColumnDescription = "现居住地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// 户籍地址
    /// </summary>
    [SugarColumn(ColumnName = "registered_address", ColumnDescription = "户籍地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? RegisteredAddress { get; set; }

    /// <summary>
    /// 员工状态（0=在职，1=离职，2=停薪留职，3=退休）
    /// </summary>
    [SugarColumn(ColumnName = "employee_status", ColumnDescription = "员工状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EmployeeStatus { get; set; } = 0;

    /// <summary>
    /// 员工代理规则列表（外键在子表 <see cref="TaktEmployeeDelegate.EmployeeId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEmployeeDelegate.EmployeeId))]
    public List<TaktEmployeeDelegate>? EmployeeDelegates { get; set; }
}
