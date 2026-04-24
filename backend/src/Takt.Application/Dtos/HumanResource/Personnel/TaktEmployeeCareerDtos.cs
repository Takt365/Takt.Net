// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：员工职业表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Personnel;

/// <summary>
/// 员工职业表Dto
/// </summary>
public partial class TaktEmployeeCareerDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerDto()
    {
        DeptName = string.Empty;
    }

    /// <summary>
    /// 员工职业表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeCareerId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }
    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 工作性质
    /// </summary>
    public int WorkNature { get; set; }
    /// <summary>
    /// 用工形式
    /// </summary>
    public int EmploymentType { get; set; }
    /// <summary>
    /// 是否主职
    /// </summary>
    public int IsPrimary { get; set; }
    /// <summary>
    /// 直接上级员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DirectManagerId { get; set; }
    /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }
}

/// <summary>
/// 员工职业表查询DTO
/// </summary>
public partial class TaktEmployeeCareerQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工职业表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeCareerId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeptId { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }
    /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 入职日期开始时间
    /// </summary>
    public DateTime? JoinDateStart { get; set; }
    /// <summary>
    /// 入职日期结束时间
    /// </summary>
    public DateTime? JoinDateEnd { get; set; }
    /// <summary>
    /// 转正日期
    /// </summary>
    public DateTime? RegularizationDate { get; set; }

    /// <summary>
    /// 转正日期开始时间
    /// </summary>
    public DateTime? RegularizationDateStart { get; set; }
    /// <summary>
    /// 转正日期结束时间
    /// </summary>
    public DateTime? RegularizationDateEnd { get; set; }
    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 离职日期开始时间
    /// </summary>
    public DateTime? LeaveDateStart { get; set; }
    /// <summary>
    /// 离职日期结束时间
    /// </summary>
    public DateTime? LeaveDateEnd { get; set; }
    /// <summary>
    /// 工作年限
    /// </summary>
    public decimal? WorkYears { get; set; }
    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }
    /// <summary>
    /// 工作性质
    /// </summary>
    public int? WorkNature { get; set; }
    /// <summary>
    /// 用工形式
    /// </summary>
    public int? EmploymentType { get; set; }
    /// <summary>
    /// 是否主职
    /// </summary>
    public int? IsPrimary { get; set; }
    /// <summary>
    /// 直接上级员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DirectManagerId { get; set; }
    /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }

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
/// Takt创建员工职业表DTO
/// </summary>
public partial class TaktEmployeeCareerCreateDto
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 岗位ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
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
    /// 工作性质
    /// </summary>
    public int WorkNature { get; set; }

        /// <summary>
    /// 用工形式
    /// </summary>
    public int EmploymentType { get; set; }

        /// <summary>
    /// 是否主职
    /// </summary>
    public int IsPrimary { get; set; }

        /// <summary>
    /// 直接上级员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DirectManagerId { get; set; }

        /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }

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
/// Takt更新员工职业表DTO
/// </summary>
public partial class TaktEmployeeCareerUpdateDto : TaktEmployeeCareerCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerUpdateDto()
    {
    }

        /// <summary>
    /// 员工职业表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeCareerId { get; set; }
}

/// <summary>
/// 员工职业表导入模板DTO
/// </summary>
public partial class TaktEmployeeCareerTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerTemplateDto()
    {
        DeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 岗位ID
    /// </summary>
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
    /// 工作性质
    /// </summary>
    public int WorkNature { get; set; }

        /// <summary>
    /// 用工形式
    /// </summary>
    public int EmploymentType { get; set; }

        /// <summary>
    /// 是否主职
    /// </summary>
    public int IsPrimary { get; set; }

        /// <summary>
    /// 直接上级员工ID
    /// </summary>
    public long? DirectManagerId { get; set; }

        /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }

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
/// 员工职业表导入DTO
/// </summary>
public partial class TaktEmployeeCareerImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerImportDto()
    {
        DeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 岗位ID
    /// </summary>
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
    /// 工作性质
    /// </summary>
    public int WorkNature { get; set; }

        /// <summary>
    /// 用工形式
    /// </summary>
    public int EmploymentType { get; set; }

        /// <summary>
    /// 是否主职
    /// </summary>
    public int IsPrimary { get; set; }

        /// <summary>
    /// 直接上级员工ID
    /// </summary>
    public long? DirectManagerId { get; set; }

        /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }

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
/// 员工职业表导出DTO
/// </summary>
public partial class TaktEmployeeCareerExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEmployeeCareerExportDto()
    {
        CreatedAt = DateTime.Now;
        DeptName = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

        /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

        /// <summary>
    /// 岗位ID
    /// </summary>
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
    /// 工作性质
    /// </summary>
    public int WorkNature { get; set; }

        /// <summary>
    /// 用工形式
    /// </summary>
    public int EmploymentType { get; set; }

        /// <summary>
    /// 是否主职
    /// </summary>
    public int IsPrimary { get; set; }

        /// <summary>
    /// 直接上级员工ID
    /// </summary>
    public long? DirectManagerId { get; set; }

        /// <summary>
    /// 直接上级姓名
    /// </summary>
    public string? DirectManagerName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}