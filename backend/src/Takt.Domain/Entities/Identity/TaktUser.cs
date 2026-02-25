// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktUser.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户实体，定义用户领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt用户实体
/// </summary>
[SugarTable("takt_identity_user", "用户表")]
[SugarIndex("ix_takt_identity_user_user_name", nameof(UserName), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_user_user_email", nameof(UserEmail), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_user_phone", nameof(UserPhone), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_user_status", nameof(UserStatus), OrderByType.Asc)]
public class TaktUser : TaktEntityBase
{
    /// <summary>
    /// 用户名（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "real_name", ColumnDescription = "真实姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 全名
    /// </summary>
    [SugarColumn(ColumnName = "full_name", ColumnDescription = "全名", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Gender { get; set; } = 0;

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserType { get; set; } = 0;

    /// <summary>
    /// 用户邮箱（索引）
    /// </summary>
    [SugarColumn(ColumnName = "user_email", ColumnDescription = "用户邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string UserEmail { get; set; } = string.Empty;

    /// <summary>
    /// 用户手机号（索引）
    /// </summary>
    [SugarColumn(ColumnName = "user_phone", ColumnDescription = "用户手机号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string UserPhone { get; set; } = string.Empty;

    /// <summary>
    /// 密码哈希
    /// </summary>
    [SugarColumn(ColumnName = "password_hash", ColumnDescription = "密码哈希", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// 登录次数
    /// </summary>
    [SugarColumn(ColumnName = "login_count", ColumnDescription = "登录次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LoginCount { get; set; } = 0;

    /// <summary>
    /// 锁定原因
    /// </summary>
    [SugarColumn(ColumnName = "lock_reason", ColumnDescription = "锁定原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LockReason { get; set; }

    /// <summary>
    /// 锁定时间
    /// </summary>
    [SugarColumn(ColumnName = "lock_time", ColumnDescription = "锁定时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LockTime { get; set; }

    /// <summary>
    /// 锁定人（用户名）
    /// </summary>
    [SugarColumn(ColumnName = "lock_by", ColumnDescription = "锁定人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? LockBy { get; set; }

    /// <summary>
    /// 登录失败次数（连续登录失败次数，登录成功时重置为0）
    /// </summary>
    [SugarColumn(ColumnName = "error_count", ColumnDescription = "登录失败次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ErrorCount { get; set; } = 0;

    /// <summary>
    /// 错误次数限制（登录错误次数上限，超过则锁定）
    /// </summary>
    [SugarColumn(ColumnName = "error_limit", ColumnDescription = "错误次数限制", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int ErrorLimit { get; set; } = 5;

    /// <summary>
    /// 用户状态（索引，0=启用，1=禁用，3=锁定）
    /// </summary>
    [SugarColumn(ColumnName = "user_status", ColumnDescription = "用户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserStatus { get; set; } = 0;

}