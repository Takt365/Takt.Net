// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers
// 文件名称：TaktControllerBase.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt控制器基类，提供通用的控制器功能（日志、本地化、用户和租户上下文）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using System.Security.Claims;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Tenant;
using Takt.Infrastructure.User;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers;

/// <summary>
/// Takt控制器基类
/// </summary>
[ApiController]
[Authorize]
public abstract class TaktControllerBase : ControllerBase
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
    protected TaktControllerBase(
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
            var controllerTypeName = GetType().Name;
            var requestPath = HttpContext?.Request?.Path.Value ?? "unknown";
            var requestMethod = HttpContext?.Request?.Method ?? "unknown";
            
            // 获取用户上下文信息（RealName 来自员工档案）
            var currentUser = TaktUserContext.CurrentUser;
            var userInfo = currentUser != null
                ? $"UserId: {currentUser.Id}, UserName: {currentUser.UserName}, RealName: {_userContext?.GetCurrentRealName() ?? currentUser.UserName}, UserType: {currentUser.UserType}"
                : "User: 未登录";
            
            // 获取租户上下文信息
            var currentTenant = TaktTenantContext.CurrentTenant;
            var configId = TaktTenantContext.CurrentConfigId ?? "null";
            var tenantInfo = currentTenant != null
                ? $"TenantId: {currentTenant.Id}, TenantCode: {currentTenant.TenantCode}, TenantName: {currentTenant.TenantName}, ConfigId: {configId}"
                : $"ConfigId: {configId}";
            
            TaktLogger.Debug("控制器上下文信息: ControllerType: {ControllerType}, RequestPath: {RequestPath}, RequestMethod: {RequestMethod}, {UserInfo}, {TenantInfo}",
                controllerTypeName, requestPath, requestMethod, userInfo, tenantInfo);
        }
        catch
        {
            // 忽略日志输出异常，避免影响控制器初始化
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
    /// 处理异常并返回错误响应
    /// </summary>
    /// <param name="ex">异常</param>
    /// <returns>错误响应</returns>
    protected ActionResult HandleException(Exception ex)
    {
        // 记录日志
        TaktLogger.Error(ex, "控制器操作发生错误");

        if (ex is TaktLocalizedException lex)
        {
            var localized = GetLocalizedString(lex.MessageKey, lex.ResourceType, lex.Arguments);
            return BadRequest(new { message = localized });
        }

        // 根据异常类型返回不同的响应
        if (ex is TaktBusinessException businessEx)
        {
            var message = businessEx.Message;
            // 如果本地化器可用，尝试本地化错误消息
            if (_localizer != null && !string.IsNullOrEmpty(message))
            {
                // 尝试将错误消息作为本地化键查找
                var localizedMessage = _localizer.GetString(message, "Backend");
                if (localizedMessage != message)
                {
                    message = localizedMessage;
                }
            }
            return BadRequest(new { message });
        }

        // 默认返回500错误（仅翻译键，无中文兜底）
        var errorMessage = GetLocalizedString("validation.systemInternalError", "Frontend");
        return StatusCode(500, new { message = errorMessage });
    }

    #endregion

    #region 本地化方法

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">本地化键</param>
    /// <param name="resourceType">资源类型（Frontend=前端，Backend=后端），默认为Backend</param>
    /// <returns>本地化字符串；本地化器未注入时返回 key</returns>
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
    /// <returns>本地化字符串；本地化器未注入时返回 key</returns>
    protected string GetLocalizedString(string key, string resourceType, params object[] arguments)
    {
        return _localizer?.GetString(key, resourceType, arguments) ?? key;
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
    /// <returns>当前用户ID，如果未登录或无法解析则返回null</returns>
    protected long? GetCurrentUserId()
    {
        // 优先从用户上下文获取
        if (_userContext != null)
        {
            var userId = _userContext.GetCurrentUserId();
            if (userId.HasValue)
            {
                return userId;
            }
        }

        // 从 HTTP 上下文 Claims 中获取（向后兼容）
        var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier) 
            ?? User?.FindFirst("sub") 
            ?? User?.FindFirst(OpenIddictConstants.Claims.Subject);
        if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var parsedUserId))
        {
            return parsedUserId;
        }

        return null;
    }

    /// <summary>
    /// 获取当前用户名
    /// </summary>
    /// <returns>当前用户名，如果未登录则返回null</returns>
    protected string? GetCurrentUserName()
    {
        // 优先从用户上下文获取
        if (_userContext != null)
        {
            var userName = _userContext.GetCurrentUserName();
            if (!string.IsNullOrEmpty(userName))
            {
                return userName;
            }
        }

        // 从 HTTP 上下文获取（向后兼容）
        return User?.Identity?.Name
            ?? User?.FindFirst(ClaimTypes.Name)?.Value
            ?? User?.FindFirst(OpenIddictConstants.Claims.Name)?.Value
            ?? User?.FindFirst(OpenIddictConstants.Claims.PreferredUsername)?.Value;
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
    /// 获取当前用户昵称
    /// </summary>
    /// <returns>用户昵称，如果未登录则返回null</returns>
    protected string? GetCurrentNickName()
    {
        return _userContext?.GetCurrentNickName();
    }

    /// <summary>
    /// 获取当前用户头像
    /// </summary>
    /// <returns>用户头像，如果未登录则返回null</returns>
    protected string? GetCurrentAvatar()
    {
        return _userContext?.GetCurrentAvatar();
    }

    /// <summary>
    /// 是否已登录
    /// </summary>
    /// <returns>如果已登录返回true，否则返回false</returns>
    protected bool IsAuthenticated()
    {
        if (_userContext != null)
        {
            return _userContext.IsAuthenticated();
        }
        return User?.Identity?.IsAuthenticated == true;
    }

    /// <summary>
    /// 确保用户已登录，如果未登录则返回未授权响应
    /// </summary>
    /// <returns>如果未登录返回Unauthorized，否则返回null</returns>
    protected ActionResult? EnsureAuthenticated()
    {
        if (!IsAuthenticated())
        {
            var message = GetLocalizedString("validation.authNotLoggedIn", "Frontend");
            return Unauthorized(new { message });
        }
        return null;
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