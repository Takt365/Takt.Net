// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工教育履历表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工教育履历表Dto
/// </summary>
public partial class TaktEmployeeEducationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationDto()
    {
        SchoolName = string.Empty;
    }

    /// <summary>
    /// 员工教育履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
}

/// <summary>
/// 员工教育履历表查询DTO
/// </summary>
public partial class TaktEmployeeEducationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工教育履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 学历层次
    /// </summary>
    public int? EducationLevel { get; set; }
    /// <summary>
    /// 学校名称
    /// </summary>
    public string? SchoolName { get; set; }
    /// <summary>
    /// 专业
    /// </summary>
    public string? MajorName { get; set; }
    /// <summary>
    /// 学位
    /// </summary>
    public int? DegreeLevel { get; set; }
    /// <summary>
    /// 入学日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 入学日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 入学日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 毕业日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 毕业日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 毕业日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 是否最高学历
    /// </summary>
    public int? IsHighest { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

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
/// Takt创建员工教育履历表DTO
/// </summary>
public partial class TaktEmployeeEducationCreateDto
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新员工教育履历表DTO
/// </summary>
public partial class TaktEmployeeEducationUpdateDto : TaktEmployeeEducationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationUpdateDto()
    {
    }

        /// <summary>
    /// 员工教育履历表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeEducationId { get; set; }
}

/// <summary>
/// 员工教育履历表导入模板DTO
/// </summary>
public partial class TaktEmployeeEducationTemplateDto
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 员工教育履历表导入DTO
/// </summary>
public partial class TaktEmployeeEducationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationImportDto()
    {
        SchoolName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 员工教育履历表导出DTO
/// </summary>
public partial class TaktEmployeeEducationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeEducationExportDto()
    {
        CreatedAt = DateTime.Now;
        SchoolName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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