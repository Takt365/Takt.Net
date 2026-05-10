// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktCache.cs
// 创建时间：2026-01-28
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt缓存服务接口，提供统一的缓存操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Interfaces;

/// <summary>
/// Takt缓存服务接口
/// </summary>
public interface ITaktCache
{
    /// <summary>
    /// 获取缓存值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值，如果不存在则返回默认值</returns>
    T? Get<T>(string key) where T : class;

    /// <summary>
    /// 异步获取缓存值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>缓存值，如果不存在则返回默认值</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// 设置缓存值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expirationMinutes">过期时间（分钟），如果为null则使用默认过期时间</param>
    /// <param name="slidingExpiration">是否使用滑动过期</param>
    void Set<T>(string key, T value, int? expirationMinutes = null, bool? slidingExpiration = null) where T : class;

    /// <summary>
    /// 异步设置缓存值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expirationMinutes">过期时间（分钟），如果为null则使用默认过期时间</param>
    /// <param name="slidingExpiration">是否使用滑动过期</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task SetAsync<T>(string key, T value, int? expirationMinutes = null, bool? slidingExpiration = null, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    void Remove(string key);

    /// <summary>
    /// 异步移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 检查缓存是否存在
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <returns>如果存在返回true，否则返回false</returns>
    bool Exists(string key);

    /// <summary>
    /// 异步检查缓存是否存在
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>如果存在返回true，否则返回false</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取或设置缓存值（如果不存在则通过工厂方法创建）
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="factory">值工厂方法</param>
    /// <param name="expirationMinutes">过期时间（分钟），如果为null则使用默认过期时间</param>
    /// <param name="slidingExpiration">是否使用滑动过期</param>
    /// <returns>缓存值，工厂返回 null 时可能为 null</returns>
    T? GetOrSet<T>(string key, Func<T> factory, int? expirationMinutes = null, bool? slidingExpiration = null) where T : class;

    /// <summary>
    /// 异步获取或设置缓存值（如果不存在则通过工厂方法创建）
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="factory">值工厂方法</param>
    /// <param name="expirationMinutes">过期时间（分钟），如果为null则使用默认过期时间</param>
    /// <param name="slidingExpiration">是否使用滑动过期</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>缓存值，工厂返回 null 时可能为 null</returns>
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, int? expirationMinutes = null, bool? slidingExpiration = null, CancellationToken cancellationToken = default) where T : class;
}
