// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Organization
// 文件名称：TaktHumanResourceOrganizationSpecific.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：特殊DTO集合，包含业务特定的数据传输对象（如头像更新、密码重置、用户解锁等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.HumanResource.Organization;

public class TaktDeptDelegateItemDto
{
    /// <summary>
    /// 行主键（更新/详情返回；新建可不传）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? Id { get; set; }

    /// <summary>
    /// 代理模式（字典类型 hr_delegate_mode；取值与 <see cref="Takt.Shared.Constants.TaktDelegateMode"/> 一致）
    /// </summary>
    public int DelegateMode { get; set; }

    /// <summary>
    /// 直接代理：被代理人员工 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }

    /// <summary>
    /// 部门规则：被引用部门 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }

    /// <summary>
    /// 岗位规则：被引用岗位 Id
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegatePostId { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

public class TaktDeptAssignUsersDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDeptAssignUsersDto()
    {
        UserIds = new List<long>();
    }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public List<long> UserIds { get; set; }
}

public class TaktPostAssignUsersDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPostAssignUsersDto()
    {
        UserIds = new List<long>();
    }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public List<long> UserIds { get; set; }
}

/// <summary>
/// Takt部门DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDeptDto
{
    /// <summary>
    /// 部门负责人姓名（非数据库字段，用于展示）
    /// </summary>
    public string? DeptHead { get; set; }
    
    /// <summary>
    /// 成本中心名称（非数据库字段，用于展示）
    /// </summary>
    public string? DeptCostCenterName { get; set; }
    
    /// <summary>
    /// 用户ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public List<long>? UserIds { get; set; }
    
    /// <summary>
    /// 角色ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public List<long>? RoleIds { get; set; }
    
    /// <summary>
    /// 委托代理列表（非数据库字段）
    /// </summary>
    public List<TaktDeptDelegateItemDto>? Delegates { get; set; }
}

/// <summary>
/// Takt部门创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDeptCreateDto
{
    /// <summary>
    /// 委托代理列表（非数据库字段）
    /// </summary>
    public List<TaktDeptDelegateItemDto>? Delegates { get; set; }
    
    /// <summary>
    /// 用户ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public List<long>? UserIds { get; set; }
    
    /// <summary>
    /// 角色ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public List<long>? RoleIds { get; set; }
}

/// <summary>
/// Takt部门更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDeptUpdateDto
{
    /// <summary>
    /// 委托代理列表（非数据库字段）
    /// </summary>
    public new List<TaktDeptDelegateItemDto>? Delegates { get; set; }
    
    /// <summary>
    /// 用户ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public new List<long>? UserIds { get; set; }
    
    /// <summary>
    /// 角色ID列表（非数据库字段，用于批量分配）
    /// </summary>
    public new List<long>? RoleIds { get; set; }
}

/// <summary>
/// Takt部门导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDeptExportDto
{
    /// <summary>
    /// 部门负责人姓名（非数据库字段，用于展示）
    /// </summary>
    public string? DeptHead { get; set; }
    
    /// <summary>
    /// 部门类型字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? DeptTypeString { get; set; }
    
    /// <summary>
    /// 数据范围字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? DataScopeString { get; set; }
}

/// <summary>
/// Takt用户部门关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktUserDeptDto
{
    /// <summary>
    /// 用户姓名（非数据库字段，用于展示）
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 真实姓名（非数据库字段，用于展示）
    /// </summary>
    public string? RealName { get; set; }
    
    /// <summary>
    /// 部门名称（非数据库字段，用于展示）
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 部门编码（非数据库字段，用于展示）
    /// </summary>
    public string? DeptCode { get; set; }
}

/// <summary>
/// Takt角色部门关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktRoleDeptDto
{
    /// <summary>
    /// 角色名称（非数据库字段，用于展示）
    /// </summary>
    public string? RoleName { get; set; }
    
    /// <summary>
    /// 角色编码（非数据库字段，用于展示）
    /// </summary>
    public string? RoleCode { get; set; }
    
    /// <summary>
    /// 部门名称（非数据库字段，用于展示）
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 部门编码（非数据库字段，用于展示）
    /// </summary>
    public string? DeptCode { get; set; }
}

/// <summary>
/// Takt岗位DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPostDto
{
    /// <summary>
    /// 用户ID列表（非数据库字段，用于岗位用户关联）
    /// </summary>
    public List<long>? UserIds { get; set; }
}

/// <summary>
/// Takt岗位创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPostCreateDto
{
    /// <summary>
    /// 用户ID列表（非数据库字段，用于创建时关联用户）
    /// </summary>
    public List<long>? UserIds { get; set; }
}

/// <summary>
/// Takt岗位更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktPostUpdateDto
{
    // UserIds 已从 TaktPostCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt用户岗位关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktUserPostDto
{
    /// <summary>
    /// 岗位名称（非数据库字段，用于展示）
    /// </summary>
    public string? PostName { get; set; }
    
    /// <summary>
    /// 岗位编码（非数据库字段，用于展示）
    /// </summary>
    public string? PostCode { get; set; }
    
    /// <summary>
    /// 用户名（非数据库字段，用于展示）
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 真实姓名（非数据库字段，用于展示）
    /// </summary>
    public string? RealName { get; set; }
}

/// <summary>
/// Takt员工部门关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktEmployeeDeptDto
{
    /// <summary>
    /// 员工姓名（非数据库字段，用于展示）
    /// </summary>
    public string? EmployeeName { get; set; }
    
    /// <summary>
    /// 员工编码（非数据库字段，用于展示）
    /// </summary>
    public string? EmployeeCode { get; set; }
    
    /// <summary>
    /// 部门名称（非数据库字段，用于展示）
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 部门编码（非数据库字段，用于展示）
    /// </summary>
    public string? DeptCode { get; set; }
}

/// <summary>
/// Takt员工岗位关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktEmployeePostDto
{
    /// <summary>
    /// 员工姓名（非数据库字段，用于展示）
    /// </summary>
    public string? EmployeeName { get; set; }
    
    /// <summary>
    /// 员工编码（非数据库字段，用于展示）
    /// </summary>
    public string? EmployeeCode { get; set; }
    
    /// <summary>
    /// 岗位名称（非数据库字段，用于展示）
    /// </summary>
    public string? PostName { get; set; }
    
    /// <summary>
    /// 岗位编码（非数据库字段，用于展示）
    /// </summary>
    public string? PostCode { get; set; }
}

/// <summary>
/// Takt岗位导出DTO扩展（用于包含业务展示字段）
/// </summary>
public partial class TaktPostExportDto
{
    /// <summary>
    /// 数据权限文本（非数据库字段，用于展示）
    /// </summary>
    public string? DataScopeString { get; set; }
}

