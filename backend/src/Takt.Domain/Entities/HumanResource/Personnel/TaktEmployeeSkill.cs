// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeSkill.cs
// 创建时间：2025-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工业务技能实体，定义员工技能资质领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工业务技能实体
/// </summary>
[SugarTable("takt_humanresource_employee_skill", "员工业务技能表")]
[SugarIndex("ix_takt_humanresource_employee_skill_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_skill_skill_name", nameof(SkillName), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_skill_skill_level", nameof(SkillLevel), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_skill_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_skill_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeSkill : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 技能名称
    /// </summary>
    [SugarColumn(ColumnName = "skill_name", ColumnDescription = "技能名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string SkillName { get; set; } = string.Empty;

    /// <summary>
    /// 技能等级（0=其他，1=初级，2=中级，3=高级，4=专家）
    /// </summary>
    [SugarColumn(ColumnName = "skill_level", ColumnDescription = "技能等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SkillLevel { get; set; } = 0;

    /// <summary>
    /// 证书名称
    /// </summary>
    [SugarColumn(ColumnName = "certificate_name", ColumnDescription = "证书名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CertificateName { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    [SugarColumn(ColumnName = "certificate_no", ColumnDescription = "证书编号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? CertificateNo { get; set; }

    /// <summary>
    /// 获得日期
    /// </summary>
    [SugarColumn(ColumnName = "obtained_date", ColumnDescription = "获得日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ObtainedDate { get; set; }

    /// <summary>
    /// 到期日期
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "到期日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ExpiryDate { get; set; }
}
