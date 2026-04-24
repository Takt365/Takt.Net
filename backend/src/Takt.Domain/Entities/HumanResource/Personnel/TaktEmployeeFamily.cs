// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeFamily.cs
// 创建时间：2025-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工家庭成员实体，定义家庭关系领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工家庭成员实体；紧急联系人通过 <see cref="IsEmergencyContact"/> 标记（姓名、电话、关系类型见成员字段），与员工主表分离以避免重复存储。
/// </summary>
[SugarTable("takt_humanresource_employee_family", "员工家庭成员表")]
[SugarIndex("ix_takt_humanresource_employee_family_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_family_relation_type", nameof(RelationType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_family_is_emergency_contact", nameof(IsEmergencyContact), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_family_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_family_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeFamily : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 成员姓名
    /// </summary>
    [SugarColumn(ColumnName = "member_name", ColumnDescription = "成员姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MemberName { get; set; } = string.Empty;

    /// <summary>
    /// 关系类型（0=其他，1=配偶，2=父亲，3=母亲，4=子女）
    /// </summary>
    [SugarColumn(ColumnName = "relation_type", ColumnDescription = "关系类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RelationType { get; set; } = 0;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "phone_number", ColumnDescription = "联系电话", ColumnDataType = "nvarchar", Length = 30, IsNullable = true)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    [SugarColumn(ColumnName = "work_unit", ColumnDescription = "工作单位", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? WorkUnit { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    [SugarColumn(ColumnName = "job_title", ColumnDescription = "职务", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? JobTitle { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "birth_date", ColumnDescription = "出生日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 是否紧急联系人（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_emergency_contact", ColumnDescription = "是否紧急联系人", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsEmergencyContact { get; set; } = 0;
}
