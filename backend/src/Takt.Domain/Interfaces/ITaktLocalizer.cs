// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Interfaces
// 文件名称：ITaktLocalizer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt本地化器接口，定义本地化字符串获取方法
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Interfaces;

/// <summary>
/// Takt本地化器接口
/// </summary>
/// <remarks>
/// 仅从数据库按资源键与文化解析翻译；无记录时返回资源键本身，不使用 resx 或其它文案兜底。
/// 涉及数据库 I/O 时请使用 <see cref="GetStringAsync"/>；同步 <see cref="GetString"/> 仅读取内存缓存，未命中时返回资源键。
/// </remarks>
public interface ITaktLocalizer
{
    /// <summary>
    /// 获取本地化字符串（仅内存缓存，不访问数据库；未命中时返回资源键名）
    /// </summary>
    /// <param name="name">资源键名</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <returns>本地化字符串</returns>
    string GetString(string name, string resourceType = "Backend");

    /// <summary>
    /// 获取本地化字符串（仅内存缓存；带参数格式化）
    /// </summary>
    /// <param name="name">资源键名</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <param name="arguments">参数数组</param>
    /// <returns>本地化字符串</returns>
    string GetString(string name, string resourceType, params object[] arguments);

    /// <summary>
    /// 异步获取本地化字符串（可访问数据库并回填缓存）；<paramref name="arguments"/> 为空时不做格式化。
    /// </summary>
    Task<string> GetStringAsync(string name, string resourceType = "Backend", params object[]? arguments);

    /// <summary>
    /// 清除翻译缓存（当翻译更新时调用）
    /// </summary>
    /// <param name="cultureCode">语言编码，如果为null则清除所有语言的缓存</param>
    /// <param name="resourceType">资源类型，如果为null则清除所有类型的缓存</param>
    void ClearCache(string? cultureCode = null, string? resourceType = null);
}
