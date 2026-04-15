// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktTenantContext.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt租户上下文接口，定义获取当前租户信息的方法
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Domain.Entities.Identity;

namespace Takt.Domain.Interfaces;

/// <summary>
/// Takt租户上下文接口
/// </summary>
public interface ITaktTenantContext
{
    /// <summary>
    /// 获取当前租户实体（完整数据）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    TaktTenant? GetCurrentTenant();

    /// <summary>
    /// 获取当前租户实体（完整数据，异步）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    Task<TaktTenant?> GetCurrentTenantAsync();

    /// <summary>
    /// 获取当前 ConfigId（数据库连接标识）
    /// </summary>
    /// <returns>ConfigId，如果未设置则返回null</returns>
    string? GetCurrentConfigId();

    /// <summary>
    /// 获取当前连接字符串
    /// </summary>
    /// <returns>连接字符串，如果未设置则返回null</returns>
    string? GetCurrentConnectionString();

    /// <summary>
    /// 获取默认连接字符串（用于判断是否使用单库模式）
    /// </summary>
    /// <returns>默认连接字符串，如果未设置则返回null</returns>
    string? GetDefaultConnectionString();

    /// <summary>
    /// 是否使用多库模式（当前连接字符串与默认不同）
    /// </summary>
    /// <returns>如果使用多库模式返回true，否则返回false</returns>
    bool IsMultiDatabaseMode();

    /// <summary>
    /// 获取连接字符串（异步）
    /// </summary>
    /// <param name="configId">ConfigId</param>
    /// <returns>连接字符串</returns>
    Task<string?> GetConnectionStringAsync(string configId);

    /// <summary>
    /// 获取当前租户的开始时间
    /// </summary>
    /// <returns>开始时间，如果未设置则返回null</returns>
    DateTime? GetCurrentStartTime();

    /// <summary>
    /// 获取当前租户的结束时间
    /// </summary>
    /// <returns>结束时间，如果未设置则返回null</returns>
    DateTime? GetCurrentEndTime();

    /// <summary>
    /// 获取当前租户的状态（0=启用，1=禁用）
    /// </summary>
    /// <returns>租户状态，如果未设置则返回null</returns>
    int? GetCurrentTenantStatus();
}
