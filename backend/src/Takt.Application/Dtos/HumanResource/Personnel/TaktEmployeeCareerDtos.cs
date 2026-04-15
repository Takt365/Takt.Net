// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工职业信息DTO，包含员工职业信息相关的数据传输对象（查询、创建、更新、导入导出）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// Takt员工职业信息DTO
/// </summary>
public class TaktEmployeeCareerDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerDto()
    {
        DeptName = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 职业记录ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CareerId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 工作年限
    /// </summary>
    public decimal? WorkYears { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    public int WorkNature { get; set; }

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    public int EmploymentType { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    public int IsPrimary { get; set; }

    /// <summary>
    /// 直接上级员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DirectManagerId { get; set; }

    /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }
}

/// <summary>
/// Takt员工职业信息查询DTO
/// </summary>
public class TaktEmployeeCareerQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerQueryDto()
    {
    }

    /// <summary>
    /// 员工ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 部门ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 岗位ID（精确）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是；null 表示全部）
    /// </summary>
    public int? IsPrimary { get; set; }
}

/// <summary>
/// Takt创建员工职业信息DTO
/// </summary>
public class TaktEmployeeCareerCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerCreateDto()
    {
        DeptName = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 工作年限
    /// </summary>
    public decimal? WorkYears { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    public int WorkNature { get; set; }

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    public int EmploymentType { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    public int IsPrimary { get; set; }

    /// <summary>
    /// 直接上级员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DirectManagerId { get; set; }

    /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }
}

/// <summary>
/// Takt更新员工职业信息DTO
/// </summary>
public class TaktEmployeeCareerUpdateDto : TaktEmployeeCareerCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerUpdateDto()
    {
    }

    /// <summary>
    /// 职业记录ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CareerId { get; set; }
}

/// <summary>
/// Takt员工职业信息导入模板DTO
/// </summary>
public class TaktEmployeeCareerTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerTemplateDto()
    {
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    public int WorkNature { get; set; }

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    public int EmploymentType { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    public int IsPrimary { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt员工职业信息导入DTO
/// </summary>
public class TaktEmployeeCareerImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerImportDto()
    {
        DeptName = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PostId { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    public int WorkNature { get; set; }

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    public int EmploymentType { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    public int IsPrimary { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt员工职业信息导出DTO
/// </summary>
public class TaktEmployeeCareerExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerExportDto()
    {
        DeptName = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 职业记录ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CareerId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    public string? JobLevel { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 工作性质（0=全职，1=兼职，2=实习，3=外包，4=其他）
    /// </summary>
    public int WorkNature { get; set; }

    /// <summary>
    /// 用工形式（0=正式，1=合同，2=派遣，3=其他）
    /// </summary>
    public int EmploymentType { get; set; }

    /// <summary>
    /// 是否主职（0=否，1=是）
    /// </summary>
    public int IsPrimary { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
