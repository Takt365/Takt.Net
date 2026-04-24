// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktUserDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：用户信息表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 用户信息表Dto
/// </summary>
public partial class TaktUserDto : TaktDtosEntityBase
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
        PasswordHash = string.Empty;
    }

    /// <summary>
    /// 用户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
    /// <summary>
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }
    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }
    /// <summary>
    /// 用户手机
    /// </summary>
    public string UserPhone { get; set; }
    /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; }
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
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }
    /// <summary>
    /// 登录失败次数
    /// </summary>
    public int ErrorCount { get; set; }
    /// <summary>
    /// 错误次数限制
    /// </summary>
    public int ErrorLimit { get; set; }
    /// <summary>
    /// 用户状态
    /// </summary>
    public int UserStatus { get; set; }
    /// <summary>
    /// 关联员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
}

/// <summary>
/// 用户信息表查询DTO
/// </summary>
public partial class TaktUserQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 用户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }
    /// <summary>
    /// 用户类型
    /// </summary>
    public int? UserType { get; set; }
    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string? UserEmail { get; set; }
    /// <summary>
    /// 用户手机
    /// </summary>
    public string? UserPhone { get; set; }
    /// <summary>
    /// 密码哈希
    /// </summary>
    public string? PasswordHash { get; set; }
    /// <summary>
    /// 登录次数
    /// </summary>
    public int? LoginCount { get; set; }
    /// <summary>
    /// 锁定原因
    /// </summary>
    public string? LockReason { get; set; }
    /// <summary>
    /// 锁定时间
    /// </summary>
    public DateTime? LockTime { get; set; }

    /// <summary>
    /// 锁定时间开始时间
    /// </summary>
    public DateTime? LockTimeStart { get; set; }
    /// <summary>
    /// 锁定时间结束时间
    /// </summary>
    public DateTime? LockTimeEnd { get; set; }
    /// <summary>
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }
    /// <summary>
    /// 登录失败次数
    /// </summary>
    public int? ErrorCount { get; set; }
    /// <summary>
    /// 错误次数限制
    /// </summary>
    public int? ErrorLimit { get; set; }
    /// <summary>
    /// 用户状态
    /// </summary>
    public int? UserStatus { get; set; }
    /// <summary>
    /// 关联员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }

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
/// Takt创建用户信息表DTO
/// </summary>
public partial class TaktUserCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserCreateDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        UserEmail = string.Empty;
        UserPhone = string.Empty;
        PasswordHash = string.Empty;
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
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

        /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

        /// <summary>
    /// 用户手机
    /// </summary>
    public string UserPhone { get; set; }

        /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; }

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
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }

        /// <summary>
    /// 登录失败次数
    /// </summary>
    public int ErrorCount { get; set; }

        /// <summary>
    /// 错误次数限制
    /// </summary>
    public int ErrorLimit { get; set; }

        /// <summary>
    /// 用户状态
    /// </summary>
    public int UserStatus { get; set; }

        /// <summary>
    /// 关联员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

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
/// Takt更新用户信息表DTO
/// </summary>
public partial class TaktUserUpdateDto : TaktUserCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserUpdateDto()
    {
    }

        /// <summary>
    /// 用户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }
}

/// <summary>
/// 用户信息表用户状态DTO
/// </summary>
public partial class TaktUserStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserStatusDto()
    {
    }

        /// <summary>
    /// 用户信息表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户状态（0=禁用，1=启用）
    /// </summary>
    public int UserStatus { get; set; }
}

/// <summary>
/// 用户信息表导入模板DTO
/// </summary>
public partial class TaktUserTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserTemplateDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        UserEmail = string.Empty;
        UserPhone = string.Empty;
        PasswordHash = string.Empty;
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
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

        /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

        /// <summary>
    /// 用户手机
    /// </summary>
    public string UserPhone { get; set; }

        /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; }

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
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }

        /// <summary>
    /// 登录失败次数
    /// </summary>
    public int ErrorCount { get; set; }

        /// <summary>
    /// 错误次数限制
    /// </summary>
    public int ErrorLimit { get; set; }

        /// <summary>
    /// 用户状态
    /// </summary>
    public int UserStatus { get; set; }

        /// <summary>
    /// 关联员工ID
    /// </summary>
    public long EmployeeId { get; set; }

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
/// 用户信息表导入DTO
/// </summary>
public partial class TaktUserImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserImportDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        UserEmail = string.Empty;
        UserPhone = string.Empty;
        PasswordHash = string.Empty;
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
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

        /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

        /// <summary>
    /// 用户手机
    /// </summary>
    public string UserPhone { get; set; }

        /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; }

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
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }

        /// <summary>
    /// 登录失败次数
    /// </summary>
    public int ErrorCount { get; set; }

        /// <summary>
    /// 错误次数限制
    /// </summary>
    public int ErrorLimit { get; set; }

        /// <summary>
    /// 用户状态
    /// </summary>
    public int UserStatus { get; set; }

        /// <summary>
    /// 关联员工ID
    /// </summary>
    public long EmployeeId { get; set; }

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
/// 用户信息表导出DTO
/// </summary>
public partial class TaktUserExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
        NickName = string.Empty;
        UserEmail = string.Empty;
        UserPhone = string.Empty;
        PasswordHash = string.Empty;
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
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

        /// <summary>
    /// 用户邮箱
    /// </summary>
    public string UserEmail { get; set; }

        /// <summary>
    /// 用户手机
    /// </summary>
    public string UserPhone { get; set; }

        /// <summary>
    /// 密码哈希
    /// </summary>
    public string PasswordHash { get; set; }

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
    /// 锁定人
    /// </summary>
    public string? LockBy { get; set; }

        /// <summary>
    /// 登录失败次数
    /// </summary>
    public int ErrorCount { get; set; }

        /// <summary>
    /// 错误次数限制
    /// </summary>
    public int ErrorLimit { get; set; }

        /// <summary>
    /// 用户状态
    /// </summary>
    public int UserStatus { get; set; }

        /// <summary>
    /// 关联员工ID
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}