// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktUserContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户上下文接口，定义获取当前用户信息的方法
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.Identity;

namespace Takt.Domain.Interfaces;

/// <summary>
/// Takt用户上下文接口
/// </summary>
public interface ITaktUserContext
{
    /// <summary>
    /// 获取当前用户实体（完整数据）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    TaktUser? GetCurrentUser();

    /// <summary>
    /// 获取当前用户实体（完整数据，异步）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    Task<TaktUser?> GetCurrentUserAsync();

    /// <summary>
    /// 获取当前用户ID
    /// </summary>
    /// <returns>用户ID，如果未登录则返回null</returns>
    long? GetCurrentUserId();

    /// <summary>
    /// 获取当前用户名
    /// </summary>
    /// <returns>用户名，如果未登录则返回null</returns>
    string? GetCurrentUserName();

    /// <summary>
    /// 获取当前用户真实姓名
    /// </summary>
    /// <returns>用户真实姓名，如果未登录则返回null</returns>
    string? GetCurrentRealName();

    /// <summary>
    /// 获取当前用户昵称（优先 <see cref="TaktUser.NickName"/>；为空则回退员工档案姓名，再回退登录名）
    /// </summary>
    /// <returns>用户昵称，如果未登录则返回null</returns>
    string? GetCurrentNickName();

    /// <summary>
    /// 获取当前用户头像
    /// </summary>
    /// <returns>用户头像，如果未登录则返回null</returns>
    string? GetCurrentAvatar();

    /// <summary>
    /// 获取当前用户性别（0=未知，1=男，2=女）
    /// </summary>
    /// <returns>用户性别，如果未登录则返回null</returns>
    int? GetCurrentGender();

    /// <summary>
    /// 获取当前用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    /// <returns>用户类型，如果未登录则返回null</returns>
    int? GetCurrentUserType();

    /// <summary>
    /// 获取当前用户邮箱
    /// </summary>
    /// <returns>用户邮箱，如果未登录则返回null</returns>
    string? GetCurrentUserEmail();

    /// <summary>
    /// 获取当前用户手机号
    /// </summary>
    /// <returns>用户手机号，如果未登录则返回null</returns>
    string? GetCurrentUserPhone();

    /// <summary>
    /// 获取当前用户状态（0=启用，1=禁用，3=锁定）
    /// </summary>
    /// <returns>用户状态，如果未登录则返回null</returns>
    int? GetCurrentUserStatus();

    /// <summary>
    /// 是否已登录
    /// </summary>
    /// <returns>如果已登录返回true，否则返回false</returns>
    bool IsAuthenticated();

}
