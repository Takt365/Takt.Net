// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeContractDtos.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工合同DTO，包含员工合同相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工合同DTO
/// </summary>
public class TaktEmployeeContractDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractDto()
    {
        ContractNo = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 员工合同ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeContractId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    public string ContractNo { get; set; }

    /// <summary>
    /// 合同类型
    /// </summary>
    public int ContractType { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 试用期结束日期
    /// </summary>
    public DateTime? ProbationEndDate { get; set; }

    /// <summary>
    /// 签订日期
    /// </summary>
    public DateTime? SignDate { get; set; }

    /// <summary>
    /// 合同状态
    /// </summary>
    public int ContractStatus { get; set; }

    /// <summary>
    /// 签约主体
    /// </summary>
    public string? SignCompany { get; set; }
}

/// <summary>
/// Takt员工合同查询DTO
/// </summary>
public class TaktEmployeeContractQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 合同状态（精确）
    /// </summary>
    public int? ContractStatus { get; set; }

    /// <summary>
    /// 合同编号（模糊）
    /// </summary>
    public string? ContractNo { get; set; }
}

/// <summary>
/// Takt创建员工合同DTO
/// </summary>
public class TaktEmployeeContractCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractCreateDto()
    {
        ContractNo = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    public string ContractNo { get; set; }

    /// <summary>
    /// 合同类型
    /// </summary>
    public int ContractType { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 试用期结束日期
    /// </summary>
    public DateTime? ProbationEndDate { get; set; }

    /// <summary>
    /// 签订日期
    /// </summary>
    public DateTime? SignDate { get; set; }

    /// <summary>
    /// 合同状态
    /// </summary>
    public int ContractStatus { get; set; }

    /// <summary>
    /// 签约主体
    /// </summary>
    public string? SignCompany { get; set; }
}

/// <summary>
/// Takt更新员工合同DTO
/// </summary>
public class TaktEmployeeContractUpdateDto : TaktEmployeeContractCreateDto
{
    /// <summary>
    /// 员工合同ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeContractId { get; set; }
}

/// <summary>
/// Takt员工合同导入模板DTO
/// </summary>
public class TaktEmployeeContractTemplateDto : TaktEmployeeContractCreateDto
{
}

/// <summary>
/// Takt员工合同导入DTO
/// </summary>
public class TaktEmployeeContractImportDto : TaktEmployeeContractTemplateDto
{
}

/// <summary>
/// Takt员工合同导出DTO
/// </summary>
public class TaktEmployeeContractExportDto : TaktEmployeeContractDto
{
}
