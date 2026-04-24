// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktIdentitySpecific.cs
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

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt菜单排序DTO
/// </summary>
public class TaktMenuOrderNumDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuOrderNumDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// Takt菜单缓存DTO
/// </summary>
public class TaktMenuIsCacheDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMenuIsCacheDto()
    {
    }

    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 是否缓存（1=是，0=否）
    /// </summary>
    public int IsCache { get; set; }
}

/// <summary>
/// Takt菜单可见性DTO
/// </summary>
public class TaktMenuVisibleDto
{
    /// <summary>
    /// 菜单ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MenuId { get; set; }

    /// <summary>
    /// 是否可见（1=是，0=否）
    /// </summary>
    public int IsVisible { get; set; }
}

/// <summary>
/// Takt角色分配菜单DTO
/// </summary>
public class TaktRoleAssignMenusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleAssignMenusDto()
    {
        MenuIds = new List<long>();
    }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> MenuIds { get; set; }
}

/// <summary>
/// Takt角色分配部门DTO
/// </summary>
public class TaktRoleAssignDeptsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRoleAssignDeptsDto()
    {
        DeptIds = new List<long>();
    }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> DeptIds { get; set; }
}

/// <summary>
/// Takt用户分配角色DTO
/// </summary>
public class TaktUserAssignRolesDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserAssignRolesDto()
    {
        RoleIds = new List<long>();
    }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> RoleIds { get; set; }
}

/// <summary>
/// Takt用户分配部门DTO
/// </summary>
public class TaktUserAssignDeptsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserAssignDeptsDto()
    {
        DeptIds = new List<long>();
    }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> DeptIds { get; set; }
}

/// <summary>
/// Takt用户分配岗位DTO
/// </summary>
public class TaktUserAssignPostsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserAssignPostsDto()
    {
        PostIds = new List<long>();
    }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> PostIds { get; set; }
}

/// <summary>
/// Takt用户分配租户DTO
/// </summary>
public class TaktUserAssignTenantsDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserAssignTenantsDto()
    {
        TenantIds = new List<long>();
    }

    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 租户ID列表
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public List<long> TenantIds { get; set; }
}
/// <summary>
/// Takt用户重置密码DTO
/// </summary>
/// 
public class TaktUserResetPwdDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserResetPwdDto()
    {
        NewPassword = string.Empty;
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    public string NewPassword { get; set; }
}
/// <summary>
/// Takt用户修改密码DTO
/// </summary>
public class TaktUserChangePwdDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserChangePwdDto()
    {
        OldPassword = string.Empty;
        NewPassword = string.Empty;
    }

    /// <summary>
    /// 旧密码
    /// </summary>
    public string OldPassword { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    public string NewPassword { get; set; }
}
/// <summary>
/// Takt用户忘记密码DTO
/// </summary>
public class TaktUserForgotPasswordDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserForgotPasswordDto()
    {
        UserEmail = string.Empty;
    }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }
}
/// <summary>
/// Takt用户忘记密码结果DTO
/// </summary>

public class TaktUserForgotPasswordResultDto
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 失败时的原因代码：EmailNotFound、ProtectedUser
    /// </summary>
    public string? Code { get; set; }
}

/// <summary>
/// Takt用户解锁DTO
/// </summary>
public partial class TaktUserUnlockDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }
}

/// <summary>
/// Takt用户头像更新DTO
/// </summary>
public partial class TaktUserAvatarUpdateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 头像URL
    /// </summary>
    public string AvatarUrl { get; set; } = string.Empty;
}

// ========================================
// 用户、角色、租户业务扩展字段
// ========================================

/// <summary>
/// Takt用户DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserDto
{
    /// <summary>
    /// 角色ID列表（非数据库字段）
    /// </summary>
    public List<long>? RoleIds { get; set; }
    
    /// <summary>
    /// 部门ID列表（非数据库字段）
    /// </summary>
    public List<long>? DeptIds { get; set; }
    
    /// <summary>
    /// 岗位ID列表（非数据库字段）
    /// </summary>
    public List<long>? PostIds { get; set; }
    
    /// <summary>
    /// 租户ID列表（非数据库字段）
    /// </summary>
    public List<long>? TenantIds { get; set; }
}

/// <summary>
/// Takt用户创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserCreateDto
{
    /// <summary>
    /// 角色ID列表（非数据库字段）
    /// </summary>
    public List<long>? RoleIds { get; set; }
    
    /// <summary>
    /// 部门ID列表（非数据库字段）
    /// </summary>
    public List<long>? DeptIds { get; set; }
    
    /// <summary>
    /// 岗位ID列表（非数据库字段）
    /// </summary>
    public List<long>? PostIds { get; set; }
    
    /// <summary>
    /// 租户ID列表（非数据库字段）
    /// </summary>
    public List<long>? TenantIds { get; set; }
}

/// <summary>
/// Takt用户更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserUpdateDto
{
    // RoleIds, DeptIds, PostIds, TenantIds 已从 TaktUserCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt用户解锁DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserUnlockDto
{
    /// <summary>
    /// 用户状态（非数据库字段）
    /// </summary>
    public int? UserStatus { get; set; }
}

/// <summary>
/// Takt用户头像更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserAvatarUpdateDto
{
    /// <summary>
    /// 头像（非数据库字段）
    /// </summary>
    public string? Avatar { get; set; }
}

/// <summary>
/// Takt用户导出DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktUserExportDto
{
    /// <summary>
    /// 真实姓名（非数据库字段，用于展示）
    /// </summary>
    public string? RealName { get; set; }
    
    /// <summary>
    /// 性别（非数据库字段，用于展示）
    /// </summary>
    public int? Gender { get; set; }
    
    /// <summary>
    /// 头像（非数据库字段，用于展示）
    /// </summary>
    public string? Avatar { get; set; }
    
    /// <summary>
    /// 部门名称（非数据库字段，用于展示）
    /// </summary>
    public string? DeptName { get; set; }
    
    /// <summary>
    /// 角色名称列表（非数据库字段，用于展示）
    /// </summary>
    public string? RoleNames { get; set; }
    
    /// <summary>
    /// 岗位名称列表（非数据库字段，用于展示）
    /// </summary>
    public string? PostNames { get; set; }
}

/// <summary>
/// Takt用户角色关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktUserRoleDto
{
    /// <summary>
    /// 用户名（非数据库字段，用于展示）
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 真实姓名（非数据库字段，用于展示）
    /// </summary>
    public string? RealName { get; set; }
    
    /// <summary>
    /// 角色名称（非数据库字段，用于展示）
    /// </summary>
    public string? RoleName { get; set; }
    
    /// <summary>
    /// 角色编码（非数据库字段，用于展示）
    /// </summary>
    public string? RoleCode { get; set; }
}



/// <summary>
/// Takt用户租户关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktUserTenantDto
{
    /// <summary>
    /// 用户名（非数据库字段，用于展示）
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// 真实姓名（非数据库字段，用于展示）
    /// </summary>
    public string? RealName { get; set; }
    
    /// <summary>
    /// 租户名称（非数据库字段，用于展示）
    /// </summary>
    public string? TenantName { get; set; }
    
    /// <summary>
    /// 租户编码（非数据库字段，用于展示）
    /// </summary>
    public string? TenantCode { get; set; }
    
    /// <summary>
    /// 租户配置ID（非数据库字段，用于展示）
    /// </summary>
    public string? TenantConfigId { get; set; }
    
    /// <summary>
    /// 租户状态（非数据库字段，用于展示）
    /// </summary>
    public int? TenantStatus { get; set; }
    
    /// <summary>
    /// 订阅开始时间（非数据库字段，用于展示）
    /// </summary>
    public DateTime? SubscriptionStartTime { get; set; }
    
    /// <summary>
    /// 订阅结束时间（非数据库字段，用于展示）
    /// </summary>
    public DateTime? SubscriptionEndTime { get; set; }
}

/// <summary>
/// Takt角色DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktRoleDto
{
    /// <summary>
    /// 菜单ID列表（非数据库字段）
    /// </summary>
    public List<long>? MenuIds { get; set; }
    
    /// <summary>
    /// 用户ID列表（非数据库字段）
    /// </summary>
    public List<long>? UserIds { get; set; }
    
    /// <summary>
    /// 部门ID列表（非数据库字段）
    /// </summary>
    public List<long>? DeptIds { get; set; }
}

/// <summary>
/// Takt角色创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktRoleCreateDto
{
    /// <summary>
    /// 菜单ID列表（非数据库字段）
    /// </summary>
    public List<long>? MenuIds { get; set; }
    
    /// <summary>
    /// 用户ID列表（非数据库字段）
    /// </summary>
    public List<long>? UserIds { get; set; }
    
    /// <summary>
    /// 部门ID列表（非数据库字段）
    /// </summary>
    public List<long>? DeptIds { get; set; }
}

/// <summary>
/// Takt角色更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktRoleUpdateDto
{
    // MenuIds, UserIds, DeptIds 已从 TaktRoleCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt角色菜单关联DTO扩展（用于包含展示字段）
/// </summary>
public partial class TaktRoleMenuDto
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
    /// 菜单名称（非数据库字段，用于展示）
    /// </summary>
    public string? MenuName { get; set; }
    
    /// <summary>
    /// 菜单编码（非数据库字段，用于展示）
    /// </summary>
    public string? MenuCode { get; set; }
    
    /// <summary>
    /// 路由路径（非数据库字段，用于展示）
    /// </summary>
    public string? Path { get; set; }
    
    /// <summary>
    /// 菜单类型（非数据库字段，用于展示）
    /// </summary>
    public int? MenuType { get; set; }
}

/// <summary>
/// Takt租户导入DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktTenantImportDto
{
    /// <summary>
    /// 配置ID（非数据库字段）
    /// </summary>
    public string? ConfigId { get; set; }
}

/// <summary>
/// Takt租户查询DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktTenantQueryDto
{
    /// <summary>
    /// 配置ID（非数据库字段）
    /// </summary>
    public string? ConfigId { get; set; }
}

/// <summary>
/// Takt菜单导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktMenuExportDto
{
    /// <summary>
    /// 菜单类型字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? MenuTypeString { get; set; }
}

/// <summary>
/// Takt角色导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktRoleExportDto
{
    /// <summary>
    /// 数据范围字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? DataScopeString { get; set; }
}

/// <summary>
/// Takt用户导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktUserExportDto
{
    /// <summary>
    /// 用户类型字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? UserTypeString { get; set; }
    
    /// <summary>
    /// 性别字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? GenderString { get; set; }
}