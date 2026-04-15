// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktUser.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户实体，仅记录登录/身份相关信息；人事信息（姓名、头像等）以 TaktEmployee 为准。
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;
using Takt.Domain.Entities.HumanResource.Personnel;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt用户实体（登录用户）
/// 登录用户必须关联人事档案：本表 EmployeeId 必填，指向 TaktEmployee。人事档案中员工不一定具备登录资格。
/// 用户名 UserName 通过员工选项列表选择绑定（新建/编辑用户时从 TaktEmployee 选项选择后带出或约束）。
/// 姓名、性别、头像等人事信息以 TaktEmployee 为准。
/// </summary>
[SugarTable("takt_identity_user", "用户表")]
[SugarIndex("ix_takt_identity_user_user_name", nameof(UserName), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_user_user_email", nameof(UserEmail), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_user_phone", nameof(UserPhone), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_user_status", nameof(UserStatus), OrderByType.Asc)]
public class TaktUser : TaktEntityBase
{
    /// <summary>
    /// 用户名（唯一索引，登录用；通过员工选项列表选择绑定人事后由员工编码/姓名带出或手动填写）
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称（展示用，与员工档案姓名独立；可为空）
    /// </summary>
    [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false, DefaultValue = "")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserType { get; set; } = 0;

    /// <summary>
    /// 用户邮箱（索引，登录/找回密码用）
    /// </summary>
    [SugarColumn(ColumnName = "user_email", ColumnDescription = "用户邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string UserEmail { get; set; } = string.Empty;

    /// <summary>
    /// 用户手机号（索引，登录/验证用）
    /// </summary>
    [SugarColumn(ColumnName = "user_phone", ColumnDescription = "用户手机", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
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
    /// 用户状态（索引，1=启用，0=禁用，2=锁定）
    /// </summary>
    [SugarColumn(ColumnName = "user_status", ColumnDescription = "用户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int UserStatus { get; set; } = 1;

    /// <summary>
    /// 关联员工ID（外键指向 TaktEmployee.Id，必填；登录用户必须对应一名员工，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "关联员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    // ----- 导航属性（统一放在实体末尾，便于维护） -----

    /// <summary>
    /// 关联的员工档案（导航属性）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public TaktEmployee? Employee { get; set; }
}