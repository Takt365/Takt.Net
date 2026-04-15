// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillDtos.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工业务技能DTO，包含员工业务技能相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工业务技能DTO
/// </summary>
public class TaktEmployeeSkillDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillDto()
    {
        SkillName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 员工技能ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeSkillId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

    /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

    /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

    /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}

/// <summary>
/// Takt员工业务技能查询DTO
/// </summary>
public class TaktEmployeeSkillQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 技能名称（模糊）
    /// </summary>
    public string? SkillName { get; set; }

    /// <summary>
    /// 技能等级（精确）
    /// </summary>
    public int? SkillLevel { get; set; }
}

/// <summary>
/// Takt创建员工业务技能DTO
/// </summary>
public class TaktEmployeeSkillCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeSkillCreateDto()
    {
        SkillName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel { get; set; }

    /// <summary>
    /// 证书名称
    /// </summary>
    public string? CertificateName { get; set; }

    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }

    /// <summary>
    /// 获得日期
    /// </summary>
    public DateTime? ObtainedDate { get; set; }

    /// <summary>
    /// 到期日期
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
}

/// <summary>
/// Takt更新员工业务技能DTO
/// </summary>
public class TaktEmployeeSkillUpdateDto : TaktEmployeeSkillCreateDto
{
    /// <summary>
    /// 员工技能ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeSkillId { get; set; }
}

/// <summary>
/// Takt员工业务技能导入模板DTO
/// </summary>
public class TaktEmployeeSkillTemplateDto : TaktEmployeeSkillCreateDto
{
}

/// <summary>
/// Takt员工业务技能导入DTO
/// </summary>
public class TaktEmployeeSkillImportDto : TaktEmployeeSkillTemplateDto
{
}

/// <summary>
/// Takt员工业务技能导出DTO
/// </summary>
public class TaktEmployeeSkillExportDto : TaktEmployeeSkillDto
{
}
