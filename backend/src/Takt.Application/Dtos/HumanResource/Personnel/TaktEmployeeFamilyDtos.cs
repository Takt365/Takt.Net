// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilyDtos.cs
// 创建时间：2026-04-14
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工家庭成员DTO，包含员工家庭成员相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工家庭成员DTO
/// </summary>
public class TaktEmployeeFamilyDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyDto()
    {
        MemberName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 员工家庭成员ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeFamilyId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 成员姓名
    /// </summary>
    public string MemberName { get; set; }

    /// <summary>
    /// 关系类型
    /// </summary>
    public int RelationType { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    public string? WorkUnit { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 是否紧急联系人（0=否，1=是）
    /// </summary>
    public int IsEmergencyContact { get; set; }
}

/// <summary>
/// Takt员工家庭成员查询DTO
/// </summary>
public class TaktEmployeeFamilyQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 关系类型（精确）
    /// </summary>
    public int? RelationType { get; set; }

    /// <summary>
    /// 成员姓名（模糊）
    /// </summary>
    public string? MemberName { get; set; }
}

/// <summary>
/// Takt创建员工家庭成员DTO
/// </summary>
public class TaktEmployeeFamilyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyCreateDto()
    {
        MemberName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 成员姓名
    /// </summary>
    public string MemberName { get; set; }

    /// <summary>
    /// 关系类型
    /// </summary>
    public int RelationType { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 工作单位
    /// </summary>
    public string? WorkUnit { get; set; }

    /// <summary>
    /// 职务
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 是否紧急联系人（0=否，1=是）
    /// </summary>
    public int IsEmergencyContact { get; set; }
}

/// <summary>
/// Takt更新员工家庭成员DTO
/// </summary>
public class TaktEmployeeFamilyUpdateDto : TaktEmployeeFamilyCreateDto
{
    /// <summary>
    /// 员工家庭成员ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeFamilyId { get; set; }
}

/// <summary>
/// Takt员工家庭成员导入模板DTO
/// </summary>
public class TaktEmployeeFamilyTemplateDto : TaktEmployeeFamilyCreateDto
{
}

/// <summary>
/// Takt员工家庭成员导入DTO
/// </summary>
public class TaktEmployeeFamilyImportDto : TaktEmployeeFamilyTemplateDto
{
}

/// <summary>
/// Takt员工家庭成员导出DTO
/// </summary>
public class TaktEmployeeFamilyExportDto : TaktEmployeeFamilyDto
{
}
