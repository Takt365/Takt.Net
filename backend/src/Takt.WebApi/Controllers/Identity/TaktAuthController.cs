// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktAuthController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt认证控制器，提供OIDC/OAuth2认证端点
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Application.Services.Identity;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// 认证控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]", Name = "身份认证")]
[ApiModule("Identity", "身份认证")]
[AllowAnonymous]
public class TaktAuthController : TaktControllerBase
{
    private readonly ITaktAuthService _authService;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly IOpenIddictTokenManager _tokenManager;
    private readonly ITaktHolidayService _holidayService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authService">认证服务</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="applicationManager">OpenIddict应用管理器</param>
    /// <param name="scopeManager">OpenIddict作用域管理器</param>
    /// <param name="tokenManager">OpenIddict令牌管理器</param>
    /// <param name="holidayService">假日服务（用于登录时返回今日是否假期）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAuthController(
        ITaktAuthService authService,
        ITaktRepository<TaktUser> userRepository,
        IOpenIddictApplicationManager applicationManager,
        IOpenIddictScopeManager scopeManager,
        IOpenIddictTokenManager tokenManager,
        ITaktHolidayService holidayService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _authService = authService;
        _userRepository = userRepository;
        _applicationManager = applicationManager;
        _scopeManager = scopeManager;
        _tokenManager = tokenManager;
        _holidayService = holidayService;
    }

    /// <summary>
    /// 令牌端点 - 处理OAuth2/OIDC令牌请求
    /// </summary>
    /// <returns>令牌响应</returns>
    [HttpPost("connect/token")]
    [ApiModule("Connect", "OAuth2/OIDC连接")]
    [Produces("application/json")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> ExchangeAsync()
    {
        try
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            if (request == null)
            {
                TaktLogger.Error("无法获取OpenID Connect请求");
                throw new InvalidOperationException("无法获取OpenID Connect请求");
            }

        ClaimsPrincipal claimsPrincipal;

        // 处理密码流程（用户名密码登录）
        if (request.IsPasswordGrantType())
        {
            TaktLogger.Information("开始处理登录请求，用户名: {UserName}", request.Username ?? "未知");
            
            // 验证用户名和密码
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                TaktLogger.Warning("登录失败：用户名或密码为空，用户名: {UserName}", request.Username ?? "未知");
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户名和密码不能为空"
                    }));
            }

            var user = await _userRepository.GetAsync(u => u.UserName == request.Username && u.IsDeleted == 0);
            if (user == null)
            {
                // 记录登录失败日志（用户不存在）
                await _authService.RecordLoginFailureAsync(null, request.Username, "用户不存在");
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户名或密码错误"
                    }));
            }

            // 检查用户状态（0=启用，1=禁用，3=锁定）
            if (user.UserStatus != 0)
            {
                var statusMessage = user.UserStatus == 1 ? "用户已被禁用" : "用户已被锁定";
                await _authService.RecordLoginFailureAsync(user, request.Username, statusMessage);
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户已被禁用或锁定"
                    }));
            }

            // 验证密码
            if (!TaktEncryptHelper.VerifyPassword(request.Password!, user.PasswordHash))
            {
                // 记录登录失败日志（密码错误）
                await _authService.RecordLoginFailureAsync(user, request.Username, "密码错误");
                
                // 重新查询用户以获取最新的失败次数和状态
                user = await _userRepository.GetByIdAsync(user.Id);
                if (user != null && user.UserStatus == 3)
                {
                    // 账户已被锁定
                    return Forbid(
                        authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = $"账户已被锁定，原因：{user.LockReason}"
                        }));
                }
                
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户名或密码错误"
                    }));
            }

            // 设备数限制：先尝试记录登录（不强制则可能返回已在别处登录）
            var forceLoginParam = HttpContext.Request.HasFormContentType ? HttpContext.Request.Form["force_login"].ToString() : null;
            var forceLogin = string.Equals(forceLoginParam, "true", StringComparison.OrdinalIgnoreCase)
                || string.Equals(forceLoginParam, "1", StringComparison.OrdinalIgnoreCase);
            var tryRecordResult = await _authService.TryRecordLoginSuccessAsync(user, user.UserName!, forceLogin);
            if (!tryRecordResult.Success)
            {
                return Ok(new
                {
                    error = "already_logged_in_elsewhere",
                    error_description = "当前用户已在其他位置登录，需要发送通知吗？",
                    existing_sessions = tryRecordResult.ExistingSessions
                });
            }

            // 创建Claims Principal
            var identity = new ClaimsIdentity(
                authenticationType: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                nameType: OpenIddictConstants.Claims.Name,
                roleType: OpenIddictConstants.Claims.Role);

            // 添加标准声明
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id.ToString()));
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, user.UserName));
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.PreferredUsername, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.RealName));

            if (!string.IsNullOrEmpty(user.UserEmail))
            {
                identity.AddClaim(new Claim(OpenIddictConstants.Claims.Email, user.UserEmail));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.UserEmail));
            }

            // 添加角色和权限声明
            var roles = await _authService.GetUserRolesAsync(user.Id);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, role));
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            // 添加权限声明（使用自定义 Claim 类型）
            var permissions = await _authService.GetUserPermissionsAsync(user.Id);
            foreach (var permission in permissions)
            {
                identity.AddClaim(new Claim("permission", permission));
            }

            // 根据当前请求语言和日期查询今日是否假期，写入 token 供前端展示（由认证服务统一获取并输出调试日志）
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture?.UICulture?.Name
                ?? CultureInfo.CurrentUICulture.Name
                ?? "zh-CN";
            var region = MapCultureToCountryCode(requestCulture);
            TaktLogger.Information("登录颁发 token 前获取当前假日: RequestCulture={RequestCulture}, Region={Region}", requestCulture, region);
            var holidayToday = await _authService.GetCurrentHolidayForLoginAsync(DateTime.Now, region);
            identity.AddClaim(new Claim("holiday_today", holidayToday != null ? "1" : "0"));
            if (holidayToday != null)
            {
                identity.AddClaim(new Claim("holiday_name", (holidayToday.HolidayName ?? string.Empty).Trim()));
                identity.AddClaim(new Claim("holiday_greeting", (holidayToday.HolidayGreeting ?? string.Empty).Trim()));
                identity.AddClaim(new Claim("holiday_theme", (holidayToday.HolidayTheme ?? string.Empty).Trim()));
            }

            claimsPrincipal = new ClaimsPrincipal(identity);

            // 设置作用域
            claimsPrincipal.SetScopes(new[]
            {
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Roles,
                OpenIddictConstants.Scopes.OfflineAccess // 允许刷新令牌
            });

            // 设置资源（可选）
            var resources = new List<string>();
            await foreach (var resource in _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()))
            {
                resources.Add(resource);
            }
            claimsPrincipal.SetResources(resources);

            // 更新登录次数（登录日志与在线记录已由 TryRecordLoginSuccessAsync 写入）
            user.LoginCount++;
            user.UpdateTime = DateTime.Now;
            await _userRepository.UpdateAsync(user);

            TaktLogger.Information("登录成功，用户: {UserName}，语言: {Culture}，时间: {LoginTime:yyyy-MM-dd HH:mm:ss}",
                user.UserName, requestCulture, DateTime.Now);
        }
        // 处理刷新令牌流程
        else if (request.IsRefreshTokenGrantType())
        {
            // 从当前认证结果中获取Claims Principal
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            claimsPrincipal = result?.Principal ?? throw new InvalidOperationException("无法获取认证主体");

            // 获取用户ID
            var subject = claimsPrincipal.GetClaim(OpenIddictConstants.Claims.Subject);
            if (string.IsNullOrEmpty(subject) || !long.TryParse(subject, out var userId))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户信息无效"
                    }));
            }

            // 验证用户状态（0=启用，1=禁用，3=锁定）
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.IsDeleted != 0 || user.UserStatus != 0)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "用户不存在或已被禁用"
                    }));
            }
        }
        else
        {
            return Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.UnsupportedGrantType,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "不支持的授权类型"
                }));
        }

            // 返回SignIn结果，OpenIddict会自动生成令牌
            try
            {
                var result = SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                var userName = claimsPrincipal.GetClaim(OpenIddictConstants.Claims.Name) ?? "未知";
                TaktLogger.Information("令牌生成成功，用户名: {UserName}", userName);
                return result;
            }
            catch (Exception ex)
            {
                var userName = claimsPrincipal.GetClaim(OpenIddictConstants.Claims.Name) ?? "未知";
                TaktLogger.Error(ex, "令牌生成失败，用户名: {UserName}", userName);
                throw;
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "登录处理异常，错误: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录请求DTO</param>
    /// <returns>登录响应DTO</returns>
    [HttpPost("login")]
    public async Task<ActionResult<TaktApiResult<TaktLoginResponseDto>>> LoginAsync([FromBody] TaktLoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(TaktApiResult<TaktLoginResponseDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(TaktApiResult<TaktLoginResponseDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="dto">刷新令牌请求DTO</param>
    /// <returns>登录响应DTO</returns>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<TaktApiResult<TaktLoginResponseDto>>> RefreshTokenAsync([FromBody] TaktRefreshTokenDto dto)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(TaktApiResult<TaktLoginResponseDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(TaktApiResult<TaktLoginResponseDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>操作结果</returns>
    [HttpPost("logout")]
    public async Task<ActionResult<TaktApiResult<object>>> LogoutAsync([FromBody] string refreshToken)
    {
        try
        {
            // 调用 Service 层处理退出登录（Service 层负责从 refreshToken 解析用户信息）
            var userName = await _authService.LogoutAsync(refreshToken);
            TaktLogger.Information("退出登录成功，用户: {UserName}", userName);
            return Ok(TaktApiResult<object>.Ok(null, "登出成功"));
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "退出登录失败，错误: {ErrorMessage}", ex.Message);
            return BadRequest(TaktApiResult<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns>用户信息DTO</returns>
    [HttpGet("userinfo")]
    [Authorize]
    [TaktPermission("identity:auth:userinfo", "获取当前用户信息")]
    public async Task<ActionResult<TaktApiResult<TaktUserInfoDto>>> GetUserInfoAsync()
    {
        try
        {
            var result = await _authService.GetUserInfoAsync();
            return Ok(TaktApiResult<TaktUserInfoDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(TaktApiResult<TaktUserInfoDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// 将请求文化（如 zh-CN、en-US）映射为假日表使用的国别码（CN、JP、US）
    /// </summary>
    private static string MapCultureToCountryCode(string? culture)
    {
        if (string.IsNullOrWhiteSpace(culture))
            return "CN";
        var c = culture.Trim();
        if (c.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
            return "CN";
        if (c.StartsWith("ja", StringComparison.OrdinalIgnoreCase))
            return "JP";
        if (c.StartsWith("en", StringComparison.OrdinalIgnoreCase))
            return "US";
        return "CN";
    }
}
