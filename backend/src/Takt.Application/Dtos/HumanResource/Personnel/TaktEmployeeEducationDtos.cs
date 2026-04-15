// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationDtos.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工教育经历DTO，包含员工教育经历相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工教育经历DTO
/// </summary>
public class TaktEmployeeEducationDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationDto()
    {
        SchoolName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 员工教育经历ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 学历层次（0=其他，1=高中及以下，2=大专，3=本科，4=硕士，5=博士）
    /// </summary>
    public int EducationLevel { get; set; }

    /// <summary>
    /// 学校名称
    /// </summary>
    public string SchoolName { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    public string? MajorName { get; set; }

    /// <summary>
    /// 学位（0=无，1=学士，2=硕士，3=博士）
    /// </summary>
    public int DegreeLevel { get; set; }

    /// <summary>
    /// 入学日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否最高学历（0=否，1=是）
    /// </summary>
    public int IsHighest { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
}

/// <summary>
/// Takt员工教育经历查询DTO
/// </summary>
public class TaktEmployeeEducationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationQueryDto()
    {
    }

    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 学历层次（精确）
    /// </summary>
    public int? EducationLevel { get; set; }

    /// <summary>
    /// 是否最高学历（精确）
    /// </summary>
    public int? IsHighest { get; set; }

    /// <summary>
    /// 学校名称（模糊）
    /// </summary>
    public string? SchoolName { get; set; }
}

/// <summary>
/// Takt创建员工教育经历DTO
/// </summary>
public class TaktEmployeeEducationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationCreateDto()
    {
        SchoolName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 学历层次（0=其他，1=高中及以下，2=大专，3=本科，4=硕士，5=博士）
    /// </summary>
    public int EducationLevel { get; set; }

    /// <summary>
    /// 学校名称
    /// </summary>
    public string SchoolName { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    public string? MajorName { get; set; }

    /// <summary>
    /// 学位（0=无，1=学士，2=硕士，3=博士）
    /// </summary>
    public int DegreeLevel { get; set; }

    /// <summary>
    /// 入学日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否最高学历（0=否，1=是）
    /// </summary>
    public int IsHighest { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
}

/// <summary>
/// Takt更新员工教育经历DTO
/// </summary>
public class TaktEmployeeEducationUpdateDto : TaktEmployeeEducationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationUpdateDto()
    {
    }

    /// <summary>
    /// 员工教育经历ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }
}

/// <summary>
/// Takt员工教育经历导入模板DTO
/// </summary>
public class TaktEmployeeEducationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationTemplateDto()
    {
        SchoolName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 学历层次
    /// </summary>
    public int EducationLevel { get; set; }

    /// <summary>
    /// 学校名称
    /// </summary>
    public string SchoolName { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    public string? MajorName { get; set; }

    /// <summary>
    /// 学位
    /// </summary>
    public int DegreeLevel { get; set; }

    /// <summary>
    /// 入学日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否最高学历
    /// </summary>
    public int IsHighest { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
}

/// <summary>
/// Takt员工教育经历导入DTO
/// </summary>
public class TaktEmployeeEducationImportDto : TaktEmployeeEducationTemplateDto
{
}

/// <summary>
/// Takt员工教育经历导出DTO
/// </summary>
public class TaktEmployeeEducationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationExportDto()
    {
        SchoolName = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 员工教育经历ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 学历层次
    /// </summary>
    public int EducationLevel { get; set; }

    /// <summary>
    /// 学校名称
    /// </summary>
    public string SchoolName { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    public string? MajorName { get; set; }

    /// <summary>
    /// 学位
    /// </summary>
    public int DegreeLevel { get; set; }

    /// <summary>
    /// 入学日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 是否最高学历
    /// </summary>
    public int IsHighest { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
