// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilyDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工家庭成员表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工家庭成员表Dto
/// </summary>
public partial class TaktEmployeeFamilyDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyDto()
    {
        MemberName = string.Empty;
    }

    /// <summary>
    /// 员工家庭成员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeFamilyId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 是否紧急联系人
    /// </summary>
    public int IsEmergencyContact { get; set; }
}

/// <summary>
/// 员工家庭成员表查询DTO
/// </summary>
public partial class TaktEmployeeFamilyQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工家庭成员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeFamilyId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 成员姓名
    /// </summary>
    public string? MemberName { get; set; }
    /// <summary>
    /// 关系类型
    /// </summary>
    public int? RelationType { get; set; }
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
    /// 出生日期开始时间
    /// </summary>
    public DateTime? BirthDateStart { get; set; }
    /// <summary>
    /// 出生日期结束时间
    /// </summary>
    public DateTime? BirthDateEnd { get; set; }
    /// <summary>
    /// 是否紧急联系人
    /// </summary>
    public int? IsEmergencyContact { get; set; }

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
/// Takt创建员工家庭成员表DTO
/// </summary>
public partial class TaktEmployeeFamilyCreateDto
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 是否紧急联系人
    /// </summary>
    public int IsEmergencyContact { get; set; }

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
/// Takt更新员工家庭成员表DTO
/// </summary>
public partial class TaktEmployeeFamilyUpdateDto : TaktEmployeeFamilyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyUpdateDto()
    {
    }

        /// <summary>
    /// 员工家庭成员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeFamilyId { get; set; }
}

/// <summary>
/// 员工家庭成员表导入模板DTO
/// </summary>
public partial class TaktEmployeeFamilyTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyTemplateDto()
    {
        MemberName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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
    /// 是否紧急联系人
    /// </summary>
    public int IsEmergencyContact { get; set; }

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
/// 员工家庭成员表导入DTO
/// </summary>
public partial class TaktEmployeeFamilyImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyImportDto()
    {
        MemberName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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
    /// 是否紧急联系人
    /// </summary>
    public int IsEmergencyContact { get; set; }

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
/// 员工家庭成员表导出DTO
/// </summary>
public partial class TaktEmployeeFamilyExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeFamilyExportDto()
    {
        CreatedAt = DateTime.Now;
        MemberName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
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
    /// 是否紧急联系人
    /// </summary>
    public int IsEmergencyContact { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}