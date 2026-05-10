// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktEmployeeBenefit.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：员工福利实体，记录员工享受的各项福利待遇
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 员工福利实体
/// </summary>
[SugarTable("takt_human_resource_employee_benefit", "员工福利表")]
[SugarIndex("ix_takt_human_resource_employee_benefit_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_employee_benefit_benefit_plan_id", nameof(BenefitPlanId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_employee_benefit_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_employee_benefit_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeBenefit : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利方案ID
    /// </summary>
    [SugarColumn(ColumnName = "benefit_plan_id", ColumnDescription = "福利方案ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long BenefitPlanId { get; set; }

    /// <summary>
    /// 福利类型(五险一金/补充医疗/商业保险/年假/体检/节日福利/餐补/交通补/通讯补/其他)
    /// </summary>
    [SugarColumn(ColumnName = "benefit_type", ColumnDescription = "福利类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BenefitType { get; set; } = string.Empty;

    /// <summary>
    /// 福利名称
    /// </summary>
    [SugarColumn(ColumnName = "benefit_name", ColumnDescription = "福利名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BenefitName { get; set; } = string.Empty;

    /// <summary>
    /// 福利金额
    /// </summary>
    [SugarColumn(ColumnName = "benefit_amount", ColumnDescription = "福利金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal BenefitAmount { get; set; } = 0;

    /// <summary>
    /// 发放方式(按月/按季/按年/一次性/实报实销)
    /// </summary>
    [SugarColumn(ColumnName = "distribution_method", ColumnDescription = "发放方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DistributionMethod { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 到期日期
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "到期日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// 福利说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "福利说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0=有效 1=已过期 2=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
