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
                ? $"UserId: {currentUser.Id}, UserName: {currentUser.UserName}, RealName: {currentUser.RealName}, UserType: {currentUser.UserType}"
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
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <returns>本地化字符串，如果本地化器未注入则返回key</returns>
    protected string GetLocalizedString(string key, string resourceType = "Backend")
    {
        return _localizer?.GetString(key, resourceType) ?? key;
    }

    /// <summary>
    /// 获取本地化字符串（带参数）
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <param name="arguments">参数数组</param>
    /// <returns>本地化字符串，如果本地化器未注入则返回格式化后的key</returns>
    protected string GetLocalizedString(string key, string resourceType, params object[] arguments)
    {
        return _localizer?.GetString(key, resourceType, arguments) ?? string.Format(key, arguments);
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
            ThrowBusinessException("用户未登录");
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
            ThrowBusinessExceptionLocalized("UserNotAuthenticated");
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