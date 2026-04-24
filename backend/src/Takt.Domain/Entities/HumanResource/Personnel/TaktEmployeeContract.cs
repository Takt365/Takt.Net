// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource
// 文件名称：TaktEmployeeContract.cs
// 创建时间：2025-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工合同实体，定义员工合同领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险.
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// Takt员工合同实体
/// </summary>
[SugarTable("takt_humanresource_employee_contract", "员工合同表")]
[SugarIndex("ix_takt_humanresource_employee_contract_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_contract_contract_no", nameof(ContractNo), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_contract_contract_status", nameof(ContractStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_contract_end_date", nameof(EndDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_contract_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_contract_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeContract : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    [SugarColumn(ColumnName = "contract_no", ColumnDescription = "合同编号", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ContractNo { get; set; } = string.Empty;

    /// <summary>
    /// 合同类型（0=固定期限，1=无固定期限，2=项目制，3=实习/劳务）
    /// </summary>
    [SugarColumn(ColumnName = "contract_type", ColumnDescription = "合同类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ContractType { get; set; } = 0;

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
    /// 试用期结束日期
    /// </summary>
    [SugarColumn(ColumnName = "probation_end_date", ColumnDescription = "试用期结束日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ProbationEndDate { get; set; }

    /// <summary>
    /// 签订日期
    /// </summary>
    [SugarColumn(ColumnName = "sign_date", ColumnDescription = "签订日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? SignDate { get; set; }

    /// <summary>
    /// 合同状态（0=生效中，1=已到期，2=已终止）
    /// </summary>
    [SugarColumn(ColumnName = "contract_status", ColumnDescription = "合同状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ContractStatus { get; set; } = 0;

    /// <summary>
    /// 签约主体
    /// </summary>
    [SugarColumn(ColumnName = "sign_company", ColumnDescription = "签约主体", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? SignCompany { get; set; }
}
