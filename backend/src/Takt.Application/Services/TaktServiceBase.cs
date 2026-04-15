// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services
// 文件名称：TaktServiceBase.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt服务基类，提供通用的服务功能（日志、本地化、用户和租户上下文）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.IO;
using System.Threading.Tasks;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;

namespace Takt.Application.Services;

/// <summary>
/// Takt服务基类
/// </summary>
public abstract class TaktServiceBase
{
    protected readonly ITaktUserContext? _userContext;
    protected readonly ITaktTenantContext? _tenantContext;
    protected readonly ITaktLocalizer? _localizer;

    /// <summary>
    /// 构造函数（可选依赖注入）
    /// </summary>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    protected TaktServiceBase(
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
    {
        _userContext = userContext;
        _tenantContext = tenantContext;
        _localizer = localizer;

        // 输出用户和租户上下文信息日志
        LogContextInfo();
    }

    /// <summary>
    /// 记录用户和租户上下文信息
    /// </summary>
    private void LogContextInfo()
    {
        try
        {
            var serviceTypeName = GetType().Name;

            // 通过注入的接口获取用户上下文信息
            var currentUser = _userContext?.GetCurrentUser();
            var userInfo = currentUser != null
                ? $"UserId: {currentUser.Id}, UserName: {currentUser.UserName}, RealName: {_userContext?.GetCurrentRealName() ?? currentUser.UserName}, UserType: {currentUser.UserType}"
                : "User: 未登录";

            // 通过注入的接口获取租户上下文信息
            var currentTenant = _tenantContext?.GetCurrentTenant();
            var configId = _tenantContext?.GetCurrentConfigId() ?? "null";
            var tenantInfo = currentTenant != null
                ? $"TenantId: {currentTenant.Id}, TenantCode: {currentTenant.TenantCode}, TenantName: {currentTenant.TenantName}, ConfigId: {configId}"
                : $"ConfigId: {configId}";

            TaktLogger.Debug("服务上下文信息: ServiceType: {ServiceType}, {UserInfo}, {TenantInfo}",
                serviceTypeName, userInfo, tenantInfo);
        }
        catch
        {
            // 忽略日志输出异常，避免影响服务初始化
        }
    }

    #region 日志方法

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="ex">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    protected void LogError(Exception ex, string messageTemplate, params object[]? propertyValues)
    {
        TaktLogger.Error(ex, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录信息日志
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    protected void LogInformation(string messageTemplate, params object[]? propertyValues)
    {
        TaktLogger.Information(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录警告日志
    /// </summary>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    protected void LogWarning(string messageTemplate, params object[]? propertyValues)
    {
        TaktLogger.Warning(messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    /// <summary>
    /// 记录警告日志
    /// </summary>
    /// <param name="exception">异常</param>
    /// <param name="messageTemplate">消息模板（支持结构化参数，如：{PropertyName}）</param>
    /// <param name="propertyValues">属性值</param>
    protected void LogWarning(Exception exception, string messageTemplate, params object[]? propertyValues)
    {
        TaktLogger.Warning(exception, messageTemplate, propertyValues ?? Array.Empty<object>());
    }

    #endregion

    #region 异常处理

    /// <summary>
    /// 抛出业务异常
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <remarks>
    /// 注意：业务异常由全局异常处理中间件统一记录日志，此处不重复记录
    /// </remarks>
    protected void ThrowBusinessException(string message)
    {
        throw new TaktBusinessException(message);
    }

    /// <summary>
    /// 抛出业务异常（本地化）
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <param name="arguments">参数数组</param>
    /// <remarks>
    /// 注意：业务异常由全局异常处理中间件统一记录日志，此处不重复记录
    /// </remarks>
    protected void ThrowBusinessExceptionLocalized(string key, string resourceType = "Backend", params object[] arguments)
    {
        var message = GetLocalizedString(key, resourceType, arguments);
        throw new TaktBusinessException(message);
    }

    /// <summary>
    /// 验证实体是否存在，如果不存在则抛出异常
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="errorMessage">错误消息</param>
    protected T EnsureEntityExists<T>(T? entity, string errorMessage) where T : class
    {
        if (entity == null)
        {
            ThrowBusinessException(errorMessage);
        }
        return entity!;
    }

    /// <summary>
    /// 验证实体是否存在，如果不存在则抛出异常（本地化）
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <param name="arguments">参数数组</param>
    protected T EnsureEntityExistsLocalized<T>(T? entity, string key, string resourceType = "Backend", params object[] arguments) where T : class
    {
        if (entity == null)
        {
            ThrowBusinessExceptionLocalized(key, resourceType, arguments);
        }
        return entity!;
    }

    #endregion

    #region 本地化方法

    /// <summary>
    /// 获取本地化字符串（仅内存缓存，见 <see cref="ITaktLocalizer.GetString(string, string)"/>）
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <returns>本地化字符串；本地化器未注入时返回 key（与 ITaktLocalizer 无兜底一致）</returns>
    protected string GetLocalizedString(string key, string resourceType = "Backend")
    {
        return _localizer?.GetString(key, resourceType) ?? key;
    }

    /// <summary>
    /// 获取本地化字符串（仅内存缓存；带参数）
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <param name="arguments">参数数组</param>
    /// <returns>本地化字符串；本地化器未注入时返回 key，不使用 string.Format(key,…) 兜底</returns>
    protected string GetLocalizedString(string key, string resourceType, params object[] arguments)
    {
        return _localizer?.GetString(key, resourceType, arguments) ?? key;
    }

    /// <summary>
    /// 异步获取本地化字符串（可访问数据库翻译表）；<paramref name="arguments"/> 为空时不做格式化。
    /// </summary>
    protected async Task<string> GetLocalizedStringAsync(string key, string resourceType = "Backend", params object[] arguments)
    {
        if (_localizer == null)
            return key;
        if (arguments == null || arguments.Length == 0)
            return await _localizer.GetStringAsync(key, resourceType).ConfigureAwait(false);
        return await _localizer.GetStringAsync(key, resourceType, arguments).ConfigureAwait(false);
    }

    /// <summary>
    /// 将持久化的 Frontend 校验键（如登录日志 LoginMsg）解析为当前语言文案；非 validation.* 则原样返回（兼容历史明文）。
    /// </summary>
    protected string LocalizeStoredFrontendMessage(string? stored)
    {
        if (string.IsNullOrEmpty(stored))
            return stored ?? string.Empty;
        if (stored.StartsWith("validation.", StringComparison.Ordinal))
            return GetLocalizedString(stored, "Frontend");
        return stored;
    }

    /// <summary>
    /// Excel 工作表默认英文名（导入/导出与模板下载共用）。
    /// </summary>
    protected string ResolveExcelSheetName(string? sheetName, string entityTypeName, string? defaultSheetNameEnglishOverride = null)
    {
        var (sheet, _) = TaktNamingHelper.ResolveExcelImportExport(sheetName, "_", entityTypeName, defaultSheetNameEnglishOverride);
        return sheet;
    }

    /// <summary>
    /// 前端 <c>naming.ts</c> 等与 <see cref="TaktNamingHelper"/> 对齐时常传入实体类名（如 <c>TaktUser</c>）或去 Takt 后的英文名（<c>User</c>），
    /// 与「未自定义下载文件名」语义相同，应使用本地化 <c>entity.xxx._self</c>，而非字面使用 <c>TaktUser</c>。
    /// </summary>
    protected static bool ShouldUseLocalizedExcelFileBase(string? fileName, string entityTypeName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return true;
        var t = fileName.Trim();
        if (string.Equals(t, entityTypeName, StringComparison.OrdinalIgnoreCase))
            return true;
        var english = TaktNamingHelper.DefaultSheetNameEnglish(entityTypeName);
        return string.Equals(t, english, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 导出：工作表为英文业务名（可覆盖）；文件基名为本地化 <c>entity.xxx._self</c>（除非 <paramref name="fileName"/> 为真实自定义名称）。
    /// </summary>
    protected async Task<(string Sheet, string FileBase)> ResolveExcelExportNamesAsync(
        string? sheetName,
        string? fileName,
        string entityTypeName,
        string? defaultSheetNameEnglishOverride = null)
    {
        var sheet = ResolveExcelSheetName(sheetName, entityTypeName, defaultSheetNameEnglishOverride);
        var fileBase = ShouldUseLocalizedExcelFileBase(fileName, entityTypeName)
            ? await GetLocalizedStringAsync(TaktNamingHelper.EntitySelfResourceKey(entityTypeName), "Frontend").ConfigureAwait(false)
            : fileName!.Trim();
        return (sheet, SanitizeExcelFileBase(fileBase));
    }

    /// <summary>
    /// 导入模板下载：工作表规则同 <see cref="ResolveExcelExportNamesAsync"/>；文件基名为本地化 <c>entity.xxx._self</c> 与 <see cref="TaktNamingHelper.EntityTemplateNameResourceKey"/> 拼接（如中文「用户表」+「模板」）。
    /// </summary>
    protected async Task<(string Sheet, string FileBase)> ResolveExcelImportTemplateNamesAsync(
        string? sheetName,
        string? fileName,
        string entityTypeName,
        string? defaultSheetNameEnglishOverride = null)
    {
        var sheet = ResolveExcelSheetName(sheetName, entityTypeName, defaultSheetNameEnglishOverride);
        var fileBase = ShouldUseLocalizedExcelFileBase(fileName, entityTypeName)
            ? await GetLocalizedStringAsync(TaktNamingHelper.EntitySelfResourceKey(entityTypeName), "Frontend").ConfigureAwait(false)
              + await GetLocalizedStringAsync(TaktNamingHelper.EntityTemplateNameResourceKey, "Frontend").ConfigureAwait(false)
            : fileName!.Trim();
        return (sheet, SanitizeExcelFileBase(fileBase));
    }

    private static string SanitizeExcelFileBase(string fileBase)
    {
        if (string.IsNullOrEmpty(fileBase))
            return fileBase;
        foreach (var c in Path.GetInvalidFileNameChars())
            fileBase = fileBase.Replace(c, '_');
        return fileBase.Trim();
    }

    /// <summary>
    /// 将异常转换为本地化文案；若非本地化异常则返回原始消息。
    /// </summary>
    protected string GetLocalizedExceptionMessage(Exception ex)
    {
        if (ex is TaktLocalizedException lex)
            return GetLocalizedString(lex.MessageKey, lex.ResourceType, lex.Arguments);
        if (ex is TaktBusinessException bex)
        {
            var backendText = GetLocalizedString(bex.Message, "Backend");
            if (!string.Equals(backendText, bex.Message, StringComparison.Ordinal))
                return backendText;
            var frontendText = GetLocalizedString(bex.Message, "Frontend");
            if (!string.Equals(frontendText, bex.Message, StringComparison.Ordinal))
                return frontendText;
            return bex.Message;
        }
        return ex.Message;
    }

    /// <summary>
    /// 向 Excel 导入结果的错误列表追加一条已本地化文案（ResourceType：Frontend，键一般为 validation.import*）。
    /// </summary>
    /// <param name="errors">错误列表</param>
    /// <param name="messageKey">翻译键</param>
    /// <param name="arguments">格式化参数（可选）</param>
    protected void AddImportError(List<string> errors, string messageKey, params object[]? arguments)
    {
        if (arguments == null || arguments.Length == 0)
            errors.Add(GetLocalizedString(messageKey, "Frontend"));
        else
            errors.Add(GetLocalizedString(messageKey, "Frontend", arguments));
    }

    #endregion

    #region 用户上下文方法

    /// <summary>
    /// 获取当前用户实体（完整数据）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    protected TaktUser? GetCurrentUser()
    {
        return _userContext?.GetCurrentUser();
    }

    /// <summary>
    /// 获取当前用户实体（完整数据，异步）
    /// </summary>
    /// <returns>用户实体，如果未登录则返回null</returns>
    protected async Task<TaktUser?> GetCurrentUserAsync()
    {
        if (_userContext != null)
        {
            return await _userContext.GetCurrentUserAsync();
        }
        return null;
    }

    /// <summary>
    /// 获取当前用户ID
    /// </summary>
    /// <returns>用户ID，如果未登录则返回null</returns>
    protected long? GetCurrentUserId()
    {
        return _userContext?.GetCurrentUserId();
    }

    /// <summary>
    /// 获取当前用户名
    /// </summary>
    /// <returns>用户名，如果未登录则返回null</returns>
    protected string? GetCurrentUserName()
    {
        return _userContext?.GetCurrentUserName();
    }

    /// <summary>
    /// 获取当前用户真实姓名
    /// </summary>
    /// <returns>用户真实姓名，如果未登录则返回null</returns>
    protected string? GetCurrentRealName()
    {
        return _userContext?.GetCurrentRealName();
    }

    /// <summary>
    /// 是否已登录
    /// </summary>
    /// <returns>如果已登录返回true，否则返回false</returns>
    protected bool IsAuthenticated()
    {
        return _userContext?.IsAuthenticated() ?? false;
    }

    /// <summary>
    /// 确保用户已登录，如果未登录则抛出异常
    /// </summary>
    /// <exception cref="TaktBusinessException">如果用户未登录</exception>
    protected void EnsureAuthenticated()
    {
        if (!IsAuthenticated())
        {
            ThrowBusinessException("validation.authNotLoggedIn");
        }
    }

    /// <summary>
    /// 确保用户已登录，如果未登录则抛出异常（本地化）
    /// </summary>
    /// <exception cref="TaktBusinessException">如果用户未登录</exception>
    protected void EnsureAuthenticatedLocalized()
    {
        if (!IsAuthenticated())
        {
            ThrowBusinessExceptionLocalized("validation.authNotLoggedIn", "Frontend");
        }
    }

    #endregion

    #region 租户上下文方法

    /// <summary>
    /// 获取当前租户实体（完整数据）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    protected TaktTenant? GetCurrentTenant()
    {
        return _tenantContext?.GetCurrentTenant();
    }

    /// <summary>
    /// 获取当前租户实体（完整数据，异步）
    /// </summary>
    /// <returns>租户实体，如果未设置则返回null</returns>
    protected async Task<TaktTenant?> GetCurrentTenantAsync()
    {
        if (_tenantContext != null)
        {
            return await _tenantContext.GetCurrentTenantAsync();
        }
        return null;
    }

    /// <summary>
    /// 获取当前 ConfigId（数据库连接标识）
    /// </summary>
    /// <returns>ConfigId，如果未设置则返回null</returns>
    protected string? GetCurrentConfigId()
    {
        return _tenantContext?.GetCurrentConfigId();
    }

    /// <summary>
    /// 获取当前连接字符串
    /// </summary>
    /// <returns>连接字符串，如果未设置则返回null</returns>
    protected string? GetCurrentConnectionString()
    {
        return _tenantContext?.GetCurrentConnectionString();
    }

    /// <summary>
    /// 是否使用多库模式（当前连接字符串与默认不同）
    /// </summary>
    /// <returns>如果使用多库模式返回true，否则返回false</returns>
    protected bool IsMultiDatabaseMode()
    {
        return _tenantContext?.IsMultiDatabaseMode() ?? false;
    }

    #endregion
}