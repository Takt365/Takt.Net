// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Models
// 文件名称：TaktCacheOptions.cs
// 创建时间：2026-01-28
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt缓存配置选项，用于配置缓存相关设置
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Models;

/// <summary>
/// Takt缓存配置选项
/// </summary>
public class TaktCacheOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCacheOptions()
    {
        Memory = new TaktCacheMemoryOptions();
        Redis = new TaktCacheRedisOptions();
    }

    /// <summary>
    /// 缓存提供者：Memory（内存缓存）或 Redis（Redis缓存）
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// 默认过期时间（分钟）
    /// </summary>
    public int DefaultExpirationMinutes { get; set; }

    /// <summary>
    /// 是否启用滑动过期（每次访问时重置过期时间）
    /// </summary>
    public bool EnableSlidingExpiration { get; set; }

    /// <summary>
    /// 是否启用多级缓存（Memory + Redis）
    /// </summary>
    public bool EnableMultiLevelCache { get; set; }

    /// <summary>
    /// 内存缓存配置
    /// </summary>
    public TaktCacheMemoryOptions Memory { get; set; } = new();

    /// <summary>
    /// Redis缓存配置
    /// </summary>
    public TaktCacheRedisOptions Redis { get; set; } = new();
}

/// <summary>
/// 内存缓存配置选项
/// </summary>
public class TaktCacheMemoryOptions
{
    /// <summary>
    /// 压缩百分比（0.0-1.0），当缓存大小超过限制时，清理此百分比的数据
    /// </summary>
    public double CompactionPercentage { get; set; }

    /// <summary>
    /// 压缩阈值（字节），当缓存大小超过此值时触发压缩
    /// </summary>
    public long CompactionThreshold { get; set; }

    /// <summary>
    /// 过期扫描频率（秒），定期扫描并清理过期项
    /// </summary>
    public int ExpirationScanFrequency { get; set; }

    /// <summary>
    /// 大小限制（字节），缓存总大小限制
    /// </summary>
    public long SizeLimit { get; set; }
}

/// <summary>
/// Redis缓存配置选项
/// </summary>
public class TaktCacheRedisOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCacheRedisOptions()
    {
        ConnectionString = string.Empty;
        InstanceName = string.Empty;
        Password = string.Empty;
    }

    /// <summary>
    /// 是否启用Redis缓存
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Redis连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 实例名称前缀，用于区分不同应用的缓存键
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// 默认数据库编号
    /// </summary>
    public int DefaultDatabase { get; set; }

    /// <summary>
    /// 连接超时时间（毫秒）
    /// </summary>
    public int ConnectTimeout { get; set; }

    /// <summary>
    /// 同步超时时间（毫秒）
    /// </summary>
    public int SyncTimeout { get; set; }

    /// <summary>
    /// 是否允许管理员操作
    /// </summary>
    public bool AllowAdmin { get; set; }

    /// <summary>
    /// 是否启用SSL
    /// </summary>
    public bool Ssl { get; set; }

    /// <summary>
    /// Redis密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用压缩
    /// </summary>
    public bool EnableCompression { get; set; }

    /// <summary>
    /// 压缩阈值（字节），超过此大小的值才会压缩
    /// </summary>
    public int CompressionThreshold { get; set; }
}
