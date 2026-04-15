// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktUserDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户DTO，包含用户相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// Takt用户DTO
/// </summary>
public class TaktUserDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        UserEmail = string.Empty;
        UserPhone = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名（登录名，通过员工选项列表选择绑定）
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称（展示用，与员工档案独立）
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 关联员工ID（必填，通过员工选项列表选择）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string UserPhone { get; set; }

    /// <summary>
    /// 登录次数
    /// </summary>
    public int LoginCount { get; set; }

    /// <summary>
    /// 锁定原因
    /// </summary>
    public string? LockReason { get; set; }

    /// <summary>
    /// 锁定时间
    /// </summary>
    public DateTime? LockTime { get; set; }

    /// <summary>
    /// 锁定人（用户名）
    /// </summary>
    public string? LockBy { get; set; }

    /// <summary>
    /// 错误次数限制（登录错误次数上限，超过则锁定）
    /// </summary>
    public int ErrorLimit { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int UserStatus { get; set; }

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long>? RoleIds { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    public List<long>? DeptIds { get; set; }

    /// <summary>
    /// 岗位ID列表
    /// </summary>
    public List<long>? PostIds { get; set; }

    /// <summary>
    /// 租户ID列表
    /// </summary>
    public List<long>? TenantIds { get; set; }
}

/// <summary>
/// Takt用户查询DTO
/// </summary>
public class TaktUserQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、真实姓名、邮箱、手机号中模糊查询

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string? UserEmail { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string? UserPhone { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int? UserStatus { get; set; }
}

/// <summary>
/// Takt创建用户DTO
/// </summary>
public class TaktUserCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserCreateDto()
    {
        UserType = 0;
        NickName = string.Empty;
    }

    /// <summary>
    /// 关联员工ID（必填；通过员工选项列表选择，选中后可带出用户名/姓名等）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 用户名（登录名；服务端按关联员工编码覆盖为最终值）
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称（展示用；可为空）
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）。非管理员调用创建接口时服务端强制为 0；管理员/超级管理员可创建管理员或超级管理员账号，且关联员工编码须以数字 9 开头。
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; } = string.Empty;

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string UserPhone { get; set; } = string.Empty;

    /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定，默认值为1=启用）
    /// </summary>
    public int UserStatus { get; set; } = 1;

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long>? RoleIds { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    public List<long>? DeptIds { get; set; }

    /// <summary>
    /// 岗位ID列表
    /// </summary>
    public List<long>? PostIds { get; set; }

    /// <summary>
    /// 租户ID列表
    /// </summary>
    public List<long>? TenantIds { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新用户DTO
/// </summary>
public class TaktUserUpdateDto : TaktUserCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserUpdateDto()
    {
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }
}

/// <summary>
/// Takt用户状态DTO
/// </summary>
public class TaktUserStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserStatusDto()
    {
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int UserStatus { get; set; }
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
/// Takt重置密码DTO
/// </summary>
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
/// Takt修改密码DTO
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
/// Takt忘记密码DTO（发送密码重置邮件）
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
/// Takt忘记密码结果（Success 为 false 时 Code 表示原因：EmailNotFound=邮箱未注册，ProtectedUser=超级用户/来宾不允许找回密码）
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
/// Takt解锁用户DTO
/// </summary>
public class TaktUserUnlockDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserUnlockDto()
    {
        UserStatus = 1;
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定，解锁时设置为1）
    /// </summary>
    public int UserStatus { get; set; }
}

/// <summary>
/// Takt用户头像更新DTO
/// </summary>
public class TaktUserAvatarUpdateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserAvatarUpdateDto()
    {
        Avatar = null;
    }

    /// <summary>
    /// 用户ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }
}

/// <summary>
/// Takt用户导入模板DTO
/// </summary>
public class TaktUserTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserTemplateDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        RealName = string.Empty;
        LastName = string.Empty;
        FirstName = string.Empty;
        UserPhone = string.Empty;
        UserEmail = string.Empty;
        Avatar = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 姓
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 名
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string UserPhone { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int UserStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt用户导入DTO
/// </summary>
public class TaktUserImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserImportDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        RealName = string.Empty;
        LastName = string.Empty;
        FirstName = string.Empty;
        UserPhone = string.Empty;
        UserEmail = string.Empty;
        Avatar = string.Empty;
        Remark = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 姓
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 名
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string UserPhone { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int UserStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt用户导出DTO
/// </summary>
public class TaktUserExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserExportDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        RealName = string.Empty;
        LastName = string.Empty;
        FirstName = string.Empty;
        UserType = string.Empty;
        UserPhone = string.Empty;
        UserEmail = string.Empty;
        Gender = string.Empty;
        Avatar = string.Empty;
        DeptName = string.Empty;
        RoleNames = string.Empty;
        PostNames = string.Empty;
        UserStatus = 1;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 姓
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 名
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public string UserType { get; set; }

    /// <summary>
    /// 用户手机号
    /// </summary>
    public string UserPhone { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 角色名称列表（多个角色用逗号分隔）
    /// </summary>
    public string RoleNames { get; set; }

    /// <summary>
    /// 岗位名称列表（多个岗位用逗号分隔）
    /// </summary>
    public string PostNames { get; set; }

    /// <summary>
    /// 用户状态（1=启用，0=禁用，2=锁定）
    /// </summary>
    public int UserStatus { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}