// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeContractDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工合同表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工合同表Dto
/// </summary>
public partial class TaktEmployeeContractDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractDto()
    {
        ContractNo = string.Empty;
    }

    /// <summary>
    /// 员工合同表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeContractId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
/// 员工合同表查询DTO
/// </summary>
public partial class TaktEmployeeContractQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工合同表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeContractId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 合同编号
    /// </summary>
    public string? ContractNo { get; set; }
    /// <summary>
    /// 合同类型
    /// </summary>
    public int? ContractType { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 试用期结束日期
    /// </summary>
    public DateTime? ProbationEndDate { get; set; }

    /// <summary>
    /// 试用期结束日期开始时间
    /// </summary>
    public DateTime? ProbationEndDateStart { get; set; }
    /// <summary>
    /// 试用期结束日期结束时间
    /// </summary>
    public DateTime? ProbationEndDateEnd { get; set; }
    /// <summary>
    /// 签订日期
    /// </summary>
    public DateTime? SignDate { get; set; }

    /// <summary>
    /// 签订日期开始时间
    /// </summary>
    public DateTime? SignDateStart { get; set; }
    /// <summary>
    /// 签订日期结束时间
    /// </summary>
    public DateTime? SignDateEnd { get; set; }
    /// <summary>
    /// 合同状态
    /// </summary>
    public int? ContractStatus { get; set; }
    /// <summary>
    /// 签约主体
    /// </summary>
    public string? SignCompany { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建员工合同表DTO
/// </summary>
public partial class TaktEmployeeContractCreateDto
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新员工合同表DTO
/// </summary>
public partial class TaktEmployeeContractUpdateDto : TaktEmployeeContractCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractUpdateDto()
    {
    }

        /// <summary>
    /// 员工合同表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeContractId { get; set; }
}

/// <summary>
/// 员工合同表合同状态DTO
/// </summary>
public partial class TaktEmployeeContractStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractStatusDto()
    {
    }

        /// <summary>
    /// 员工合同表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeContractId { get; set; }

    /// <summary>
    /// 合同状态（0=禁用，1=启用）
    /// </summary>
    public int ContractStatus { get; set; }
}

/// <summary>
/// 员工合同表导入模板DTO
/// </summary>
public partial class TaktEmployeeContractTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractTemplateDto()
    {
        ContractNo = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 员工合同表导入DTO
/// </summary>
public partial class TaktEmployeeContractImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractImportDto()
    {
        ContractNo = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 员工合同表导出DTO
/// </summary>
public partial class TaktEmployeeContractExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeContractExportDto()
    {
        CreatedAt = DateTime.Now;
        ContractNo = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}