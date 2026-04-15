// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeWorkDtos.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工工作经历DTO，包含员工工作经历相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工工作经历DTO
/// </summary>
public class TaktEmployeeWorkDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeWorkDto()
    {
        CompanyName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 员工工作经历ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeWorkId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
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
/// Takt员工工作经历查询DTO
/// </summary>
public class TaktEmployeeWorkQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 单位名称（模糊）
    /// </summary>
    public string? CompanyName { get; set; }
}

/// <summary>
/// Takt创建员工工作经历DTO
/// </summary>
public class TaktEmployeeWorkCreateDto
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
    [JsonConverter(typeof(ValueToStringConverter))]
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
/// Takt更新员工工作经历DTO
/// </summary>
public class TaktEmployeeWorkUpdateDto : TaktEmployeeWorkCreateDto
{
    /// <summary>
    /// 员工工作经历ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeWorkId { get; set; }
}

/// <summary>
/// Takt员工工作经历导入模板DTO
/// </summary>
public class TaktEmployeeWorkTemplateDto : TaktEmployeeWorkCreateDto
{
}

/// <summary>
/// Takt员工工作经历导入DTO
/// </summary>
public class TaktEmployeeWorkImportDto : TaktEmployeeWorkTemplateDto
{
}

/// <summary>
/// Takt员工工作经历导出DTO
/// </summary>
public class TaktEmployeeWorkExportDto : TaktEmployeeWorkDto
{
}
