// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Localization
// 文件名称：TaktLocalizer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 本地化器实现。仅从表 TaktTranslation 按当前请求文化与资源类型解析译文；无记录或译文为空时返回资源键本身，不使用 resx、其它语言或硬编码兜底。
//           同步 GetString 仅读 IMemoryCache，不访问数据库；需读库或保证译文完整请使用 GetStringAsync。当前文化优先，未命中时可回退 en-US。缓存项带过期时间。
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Takt.Domain.Entities.Routine.Tasks.I18n;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.Localization;

/// <summary>
/// <see cref="ITaktLocalizer"/> 的实现：从 <see cref="TaktTranslation"/> 表解析译文，配合 <see cref="IMemoryCache"/> 做短期缓存。
/// 同步方法仅命中缓存；异步方法在缓存未命中时访问仓储并回填缓存。当前文化与 en-US 回退策略见私有方法实现。
/// </summary>
public class TaktLocalizer : ITaktLocalizer
{
    /// <summary>
    /// 资源类型常量：邮件模板（与种子 ResourceType=Email 一致，译文须入库）。
    /// </summary>
    public const string ResourceTypeEmail = "Email";

    private readonly ITaktRepository<TaktTranslation> _translationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _memoryCache;
    private const string CacheKeyPrefix = "TaktTranslation_";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);
    private const string FallbackCultureCode = "en-US";

    /// <summary>
    /// 初始化本地化器。
    /// </summary>
    /// <param name="translationRepository">翻译实体仓储，用于按键、文化、资源类型查询单条记录。</param>
    /// <param name="httpContextAccessor">HTTP 上下文访问器，用于读取 <see cref="Microsoft.AspNetCore.Localization.IRequestCultureFeature"/>。</param>
    /// <param name="memoryCache">内存缓存，用于缓存已解析的译文字符串。</param>
    public TaktLocalizer(
        ITaktRepository<TaktTranslation> translationRepository,
        IHttpContextAccessor httpContextAccessor,
        IMemoryCache memoryCache)
    {
        _translationRepository = translationRepository;
        _httpContextAccessor = httpContextAccessor;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// 获取本地化字符串（仅读内存缓存，不访问数据库；缓存未命中时返回 <paramref name="name"/>）。
    /// </summary>
    /// <param name="name">资源键名。</param>
    /// <param name="resourceType">资源类型，如 Frontend、Backend、Email。</param>
    /// <returns>已缓存的译文；无缓存则返回 <paramref name="name"/>。</returns>
    public string GetString(string name, string resourceType = "Backend")
    {
        var translation = GetTranslationFromCacheOnly(name, resourceType);
        return string.IsNullOrEmpty(translation) ? name : translation;
    }

    /// <summary>
    /// 获取本地化字符串并格式化（仅读内存缓存；缓存未命中时返回 <paramref name="name"/>，不进行格式化）。
    /// </summary>
    /// <param name="name">资源键名。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <param name="arguments">string.Format 占位符参数。</param>
    /// <returns>格式化后的译文；缓存无数据或格式化失败时返回 <paramref name="name"/>。</returns>
    public string GetString(string name, string resourceType, params object[] arguments)
    {
        var translation = GetTranslationFromCacheOnly(name, resourceType);
        if (string.IsNullOrEmpty(translation))
            return name;

        try
        {
            return string.Format(translation, arguments);
        }
        catch
        {
            return name;
        }
    }

    /// <summary>
    /// 异步获取本地化字符串：缓存优先，未命中则查询数据库并写入缓存后再返回。
    /// </summary>
    /// <param name="name">资源键名。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <param name="arguments">可选；非空且长度大于 0 时对译文执行 string.Format。</param>
    /// <returns>译文或资源键（无记录时）。</returns>
    public async Task<string> GetStringAsync(string name, string resourceType = "Backend", params object[]? arguments)
    {
        var translation = await GetTranslationWithDatabaseAsync(name, resourceType).ConfigureAwait(false);
        if (string.IsNullOrEmpty(translation))
            return name;
        if (arguments == null || arguments.Length == 0)
            return translation;
        try
        {
            return string.Format(translation, arguments);
        }
        catch
        {
            return name;
        }
    }

    /// <summary>
    /// 清除翻译缓存的预留入口；当前实现为空操作，翻译变更后如需失效缓存可在此接入 IMemoryCache 移除逻辑。
    /// </summary>
    /// <param name="cultureCode">语言编码；null 表示全部语言（待实现）。</param>
    /// <param name="resourceType">资源类型；null 表示全部类型（待实现）。</param>
    public void ClearCache(string? cultureCode = null, string? resourceType = null)
    {
    }

    /// <summary>
    /// 按当前解析出的文化与回退文化，仅从 <see cref="IMemoryCache"/> 读取译文，不访问数据库。
    /// </summary>
    /// <param name="resourceKey">资源键。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <returns>缓存中的非空译文；否则 null。</returns>
    private string? GetTranslationFromCacheOnly(string resourceKey, string resourceType)
    {
        try
        {
            var cultureCode = GetCurrentCultureCode();
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = GetDefaultCultureFromOperatingSystem();
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = FallbackCultureCode;

            var translation = TryGetCachedTranslation(resourceKey, resourceType, cultureCode);
            if (!string.IsNullOrEmpty(translation))
                return translation;

            if (!string.Equals(cultureCode, FallbackCultureCode, StringComparison.OrdinalIgnoreCase))
                return TryGetCachedTranslation(resourceKey, resourceType, FallbackCultureCode);

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 先尝试缓存；未命中则按当前文化、必要时按 en-US 异步查询仓储并写入缓存。
    /// </summary>
    /// <param name="resourceKey">资源键。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <returns>译文；不存在或异常时 null。</returns>
    private async Task<string?> GetTranslationWithDatabaseAsync(string resourceKey, string resourceType)
    {
        try
        {
            var cultureCode = GetCurrentCultureCode();
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = GetDefaultCultureFromOperatingSystem();
            if (string.IsNullOrEmpty(cultureCode))
                cultureCode = FallbackCultureCode;

            var translation = await GetTranslationByCultureAsync(resourceKey, resourceType, cultureCode).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(translation))
                return translation;

            if (!string.Equals(cultureCode, FallbackCultureCode, StringComparison.OrdinalIgnoreCase))
                return await GetTranslationByCultureAsync(resourceKey, resourceType, FallbackCultureCode).ConfigureAwait(false);

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 解析当前 UI 文化：优先 HTTP 请求的 <c>IRequestCultureFeature</c>，否则 <see cref="CultureInfo.CurrentUICulture"/>，再否则操作系统默认。
    /// </summary>
    /// <returns>文化名称（如 zh-CN）；可能为空字符串。</returns>
    private string GetCurrentCultureCode()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var requestCulture = httpContext.Features.Get<Microsoft.AspNetCore.Localization.IRequestCultureFeature>();
            if (requestCulture?.RequestCulture?.UICulture != null)
                return requestCulture.RequestCulture.UICulture.Name;
        }

        var currentCulture = CultureInfo.CurrentUICulture.Name;
        if (!string.IsNullOrEmpty(currentCulture))
            return currentCulture;

        return GetDefaultCultureFromOperatingSystem();
    }

    /// <summary>
    /// 通过 <see cref="TaktServHelper.GetOperatingSystemLanguage"/> 与 <see cref="CultureInfo.InstalledUICulture"/> 得到默认文化名。
    /// </summary>
    /// <returns>文化名称；失败时返回 <see cref="FallbackCultureCode"/>。</returns>
    private static string GetDefaultCultureFromOperatingSystem()
    {
        try
        {
            var osLanguage = TaktServHelper.GetOperatingSystemLanguage();
            if (!string.IsNullOrEmpty(osLanguage.CurrentUICulture))
                return osLanguage.CurrentUICulture;
            if (!string.IsNullOrEmpty(osLanguage.CurrentCulture))
                return osLanguage.CurrentCulture;
            if (!string.IsNullOrEmpty(osLanguage.SystemDefaultLanguage))
                return osLanguage.SystemDefaultLanguage;
        }
        catch
        {
        }

        try
        {
            return CultureInfo.InstalledUICulture.Name;
        }
        catch
        {
            return FallbackCultureCode;
        }
    }

    /// <summary>
    /// 根据文化、资源类型与键构造缓存键并尝试读取。
    /// </summary>
    /// <param name="resourceKey">资源键。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <param name="cultureCode">文化编码。</param>
    /// <returns>缓存中的非空字符串；未命中或为空则 null。</returns>
    private string? TryGetCachedTranslation(string resourceKey, string resourceType, string cultureCode)
    {
        var cacheKey = $"{CacheKeyPrefix}{cultureCode}_{resourceType}_{resourceKey}";
        return _memoryCache.TryGetValue(cacheKey, out string? cachedTranslation) && !string.IsNullOrEmpty(cachedTranslation)
            ? cachedTranslation
            : null;
    }

    /// <summary>
    /// 在指定文化下获取译文：先读缓存，未命中则 <c>await</c> 仓储单条查询，成功则写入缓存。
    /// </summary>
    /// <param name="resourceKey">资源键。</param>
    /// <param name="resourceType">资源类型。</param>
    /// <param name="cultureCode">文化编码。</param>
    /// <returns>译文；无记录或仓储异常时 null。</returns>
    private async Task<string?> GetTranslationByCultureAsync(string resourceKey, string resourceType, string cultureCode)
    {
        var cached = TryGetCachedTranslation(resourceKey, resourceType, cultureCode);
        if (!string.IsNullOrEmpty(cached))
            return cached;

        TaktTranslation? translation;
        try
        {
            translation = await _translationRepository.GetAsync(t =>
                t.ResourceKey == resourceKey &&
                t.CultureCode == cultureCode &&
                t.ResourceType == resourceType &&
                t.IsDeleted == 0).ConfigureAwait(false);
        }
        catch
        {
            return null;
        }

        if (translation == null || string.IsNullOrEmpty(translation.TranslationValue))
            return null;

        var cacheKey = $"{CacheKeyPrefix}{cultureCode}_{resourceType}_{resourceKey}";
        _memoryCache.Set(cacheKey, translation.TranslationValue, CacheExpiration);
        return translation.TranslationValue;
    }
}
