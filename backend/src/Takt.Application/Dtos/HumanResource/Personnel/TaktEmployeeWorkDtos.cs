// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeWorkDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工工作履历表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工工作履历表Dto
/// </summary>
public partial class TaktEmployeeWorkDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkDto()
    {
        CompanyName = string.Empty;
    }

    /// <summary>
    /// 员工工作履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeWorkId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 单位名称
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }
    /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }
    /// <summary>
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }
    /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }
}

/// <summary>
/// 员工工作履历表查询DTO
/// </summary>
public partial class TaktEmployeeWorkQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工工作履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeWorkId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 单位名称
    /// </summary>
    public string? CompanyName { get; set; }
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }
    /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }
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
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }
    /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }

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
/// Takt创建员工工作履历表DTO
/// </summary>
public partial class TaktEmployeeWorkCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkCreateDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 单位名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }

        /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

        /// <summary>
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }

        /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }

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
/// Takt更新员工工作履历表DTO
/// </summary>
public partial class TaktEmployeeWorkUpdateDto : TaktEmployeeWorkCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkUpdateDto()
    {
    }

        /// <summary>
    /// 员工工作履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeWorkId { get; set; }
}

/// <summary>
/// 员工工作履历表导入模板DTO
/// </summary>
public partial class TaktEmployeeWorkTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkTemplateDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 单位名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }

        /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

        /// <summary>
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }

        /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }

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
/// 员工工作履历表导入DTO
/// </summary>
public partial class TaktEmployeeWorkImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkImportDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 单位名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }

        /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

        /// <summary>
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }

        /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }

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
/// 员工工作履历表导出DTO
/// </summary>
public partial class TaktEmployeeWorkExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 单位名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PositionName { get; set; }

        /// <summary>
    /// 工作内容
    /// </summary>
    public string? JobContent { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

        /// <summary>
    /// 证明人
    /// </summary>
    public string? WitnessName { get; set; }

        /// <summary>
    /// 证明人电话
    /// </summary>
    public string? WitnessPhone { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}