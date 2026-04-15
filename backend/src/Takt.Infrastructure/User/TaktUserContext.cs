// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.User
// 文件名称：TaktUserContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户上下文，存储当前请求的用户信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.User;

/// <summary>
/// Takt用户上下文
/// </summary>
public class TaktUserContext
{
    private static readonly AsyncLocal<TaktUser?> _currentUser = new();
    private static readonly AsyncLocal<TaktEmployee?> _currentEmployee = new();

    /// <summary>
    /// 当前用户关联的员工档案（由 <c>TaktUserMiddleware</c> 异步加载；仅内存上下文，非 Claims）
    /// </summary>
    public static TaktEmployee? CurrentEmployee
    {
        get => _currentEmployee.Value;
        set => _currentEmployee.Value = value;
    }

    /// <summary>
    /// 当前用户实体（完整数据）
    /// </summary>
    public static TaktUser? CurrentUser
    {
        get => _currentUser.Value;
        set
        {
            var oldUser = _currentUser.Value;
            _currentUser.Value = value;

            if (value == null || oldUser == null || oldUser.Id != value.Id)
                _currentEmployee.Value = null;
            
            // 输出日志：用户上下文变更（RealName 来自员工档案，此处仅记录 UserName）
            if (value != null && (oldUser == null || oldUser.Id != value.Id))
            {
                TaktLogger.Information("用户上下文已设置: UserId: {UserId}, UserName: {UserName}, UserType: {UserType}", 
                    value.Id, value.UserName, value.UserType);
            }
            else if (value == null && oldUser != null)
            {
                TaktLogger.Information("用户上下文已清除: UserId: {UserId}, UserName: {UserName}", 
                    oldUser.Id, oldUser.UserName);
            }
        }
    }

    /// <summary>
    /// 当前用户ID
    /// </summary>
    public static long? CurrentUserId => CurrentUser?.Id;

    /// <summary>
    /// 当前用户名
    /// </summary>
    public static string? CurrentUserName => CurrentUser?.UserName;

    /// <summary>
    /// 当前用户真实姓名（仅登录用户表无此字段，请通过 ITaktUserContext.GetCurrentRealName() 从员工档案获取）
    /// </summary>
    public static string? CurrentRealName => null;

    /// <summary>
    /// 当前用户昵称（静态占位恒为 null；请通过注入的 <c>ITaktUserContext.GetCurrentNickName()</c>，优先用户表昵称再回退员工档案）
    /// </summary>
    public static string? CurrentNickName => null;

    /// <summary>
    /// 当前用户头像（仅登录用户表无此字段，请通过 ITaktUserContext.GetCurrentAvatar() 从员工档案获取）
    /// </summary>
    public static string? CurrentAvatar => null;

    /// <summary>
    /// 当前用户性别（仅登录用户表无此字段，请通过 ITaktUserContext.GetCurrentGender() 从员工档案获取）
    /// </summary>
    public static int? CurrentGender => null;

    /// <summary>
    /// 当前用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    public static int? CurrentUserType => CurrentUser?.UserType;

    /// <summary>
    /// 当前用户邮箱
    /// </summary>
    public static string? CurrentUserEmail => CurrentUser?.UserEmail;

    /// <summary>
    /// 当前用户手机号
    /// </summary>
    public static string? CurrentUserPhone => CurrentUser?.UserPhone;

    /// <summary>
    /// 当前用户状态（0=启用，1=禁用，3=锁定）
    /// </summary>
    public static int? CurrentUserStatus => CurrentUser?.UserStatus;

    /// <summary>
    /// 是否已登录
    /// </summary>
    public static bool IsAuthenticated => CurrentUser != null && CurrentUser.Id > 0;
}
