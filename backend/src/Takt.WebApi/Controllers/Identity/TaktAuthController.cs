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
using System.Linq;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Attributes;
using Takt.Infrastructure.User;
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
public class TaktAuthController : TaktControllerBase
{
    private readonly ITaktAuthService _authService;
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly IOpenIddictTokenManager _tokenManager;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authService">认证服务</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="applicationManager">OpenIddict应用管理器</param>
    /// <param name="scopeManager">OpenIddict作用域管理器</param>
    /// <param name="tokenManager">OpenIddict令牌管理器</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAuthController(
        ITaktAuthService authService,
        ITaktRepository<TaktUser> userRepository,
        IOpenIddictApplicationManager applicationManager,
        IOpenIddictScopeManager scopeManager,
        IOpenIddictTokenManager tokenManager,
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
    }

    /// <summary>
    /// 令牌端点 - 处理OAuth2/OIDC令牌请求
    /// </summary>
    /// <returns>令牌响应</returns>
    [HttpPost("connect/token")]
    [AllowAnonymous]
    [ApiModule("Identity", "身份认证")]
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
                throw new InvalidOperationException(GetLocalizedString("validation.authOpenIddictRequestMissing", "Frontend"));
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
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authUsernamePasswordRequired", "Frontend")
                    }));
            }

            var user = await _userRepository.GetAsync(u => u.UserName == request.Username && u.IsDeleted == 0);
            if (user == null)
            {
                // 记录登录失败日志（用户不存在）
                await _authService.RecordLoginFailureAsync(null, request.Username, "validation.authLogUserNotFound");
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authInvalidCredentials", "Frontend")
                    }));
            }

            // 检查用户状态（1=启用，0=禁用，2=锁定）
            if (user.UserStatus != 1)
            {
                var statusMessage = user.UserStatus == 0
                    ? "validation.authLogUserDisabled"
                    : "validation.authLogUserLocked";
                await _authService.RecordLoginFailureAsync(user, request.Username, statusMessage);
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authUserDisabledOrLocked", "Frontend")
                    }));
            }

            // 验证密码
            if (!TaktEncryptHelper.VerifyPassword(request.Password!, user.PasswordHash))
            {
                // 记录登录失败日志（密码错误）
                await _authService.RecordLoginFailureAsync(user, request.Username, "validation.authLogWrongPassword");
                
                // 重新查询用户以获取最新的失败次数和状态
                user = await _userRepository.GetByIdAsync(user.Id);
                if (user != null && user.UserStatus == 2)
                {
                    // 账户已被锁定
                    return Forbid(
                        authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authAccountLockedWithReason", "Frontend", user.LockReason ?? string.Empty)
                        }));
                }
                
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authInvalidCredentials", "Frontend")
                    }));
            }

            // 与刷新令牌一致：以库表 TaktUser 为源构建主体，保证 access_token 中含登录名（OpenIddict 默认不把未标注目的地的声明写入 JWT）
            claimsPrincipal = await CreateClaimsPrincipalForValidatedUserAsync(user);

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

            // Password 流程在 SignIn 之前 HttpContext.User 尚未为最终用户主体，仓储 Resolve 依赖 TaktUserContext.CurrentUser。
            var previousCurrentUser = TaktUserContext.CurrentUser;
            try
            {
                TaktUserContext.CurrentUser = user;
                user.LoginCount++;
                user.UpdatedAt = DateTime.Now;
                await _userRepository.UpdateAsync(user);

                // 记录登录日志和在线记录
                await _authService.RecordLoginSuccessAsync(user, user.UserName);
            }
            finally
            {
                TaktUserContext.CurrentUser = previousCurrentUser;
            }

            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture?.UICulture?.Name
                ?? CultureInfo.CurrentUICulture.Name
                ?? "zh-CN";
            var loginTime = DateTime.Now;
            TaktLogger.Information("登录成功，用户: {UserName}，语言: {Culture}，时间: {LoginTime:yyyy-MM-dd HH:mm:ss}",
                user.UserName, requestCulture, loginTime);
        }
        // 处理刷新令牌流程
        else if (request.IsRefreshTokenGrantType())
        {
            // 从当前认证结果中获取 Claims Principal（仅用于解析 subject；新 access_token 的主体须与密码登录同源，否则旧令牌未含 name 时刷新后仍无登录名）
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            var refreshPrincipal = result?.Principal ?? throw new InvalidOperationException(GetLocalizedString("validation.authRefreshPrincipalMissing", "Frontend"));

            var subject = refreshPrincipal.GetClaim(OpenIddictConstants.Claims.Subject);
            if (string.IsNullOrEmpty(subject) || !long.TryParse(subject, out var userId))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authRefreshSubjectInvalid", "Frontend")
                    }));
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.IsDeleted != 0 || user.UserStatus != 1)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authRefreshUserInvalid", "Frontend")
                    }));
            }

            claimsPrincipal = await CreateClaimsPrincipalForValidatedUserAsync(user);

            var requestScopes = request.GetScopes();
            if (requestScopes.Any())
            {
                claimsPrincipal.SetScopes(requestScopes);
            }
            else
            {
                var inheritedScopes = refreshPrincipal.GetScopes();
                if (inheritedScopes.Any())
                {
                    claimsPrincipal.SetScopes(inheritedScopes);
                }
                else
                {
                    claimsPrincipal.SetScopes(new[]
                    {
                        OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.Email,
                        OpenIddictConstants.Scopes.Roles,
                        OpenIddictConstants.Scopes.OfflineAccess
                    });
                }
            }

            var refreshResources = new List<string>();
            await foreach (var resource in _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()))
            {
                refreshResources.Add(resource);
            }

            claimsPrincipal.SetResources(refreshResources);
        }
        else
        {
            return Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.UnsupportedGrantType,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = GetLocalizedString("validation.authUnsupportedGrantType", "Frontend")
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
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<TaktLoginResponseDto>>> LoginAsync([FromBody] TaktLoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(TaktApiResult<TaktLoginResponseDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(TaktApiResult<TaktLoginResponseDto>.Fail(GetLocalizedExceptionMessage(ex)));
        }
    }

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="dto">刷新令牌请求DTO</param>
    /// <returns>登录响应DTO</returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<TaktLoginResponseDto>>> RefreshTokenAsync([FromBody] TaktRefreshTokenDto dto)
    {
        try
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(TaktApiResult<TaktLoginResponseDto>.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(TaktApiResult<TaktLoginResponseDto>.Fail(GetLocalizedExceptionMessage(ex)));
        }
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>操作结果</returns>
    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<object>>> LogoutAsync([FromBody] string refreshToken)
    {
        try
        {
            // 调用 Service 层处理退出登录（Service 层负责从 refreshToken 解析用户信息）
            var userName = await _authService.LogoutAsync(refreshToken);
            TaktLogger.Information("退出登录成功，用户: {UserName}", userName);
            return Ok(TaktApiResult<object>.Ok(null, GetLocalizedString("common.auth.logoutSuccess", "Frontend")));
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "退出登录失败，错误: {ErrorMessage}", ex.Message);
            return BadRequest(TaktApiResult<object>.Fail(GetLocalizedExceptionMessage(ex)));
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
            return BadRequest(TaktApiResult<TaktUserInfoDto>.Fail(GetLocalizedExceptionMessage(ex)));
        }
    }

    /// <summary>
    /// 按已校验的 <see cref="TaktUser"/>（前端以 <see cref="TaktUser.UserName"/> 登录）构建签发 access/refresh 使用的主体，
    /// 并调用 OpenIddict <c>SetDestinations</c>，否则访问令牌 JWT 中默认仅含 <c>sub</c>，资源服务器与 SignalR 无法从 Claims 读取登录名。
    /// </summary>
    private async Task<ClaimsPrincipal> CreateClaimsPrincipalForValidatedUserAsync(TaktUser user)
    {
        var identity = new ClaimsIdentity(
            authenticationType: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
            nameType: OpenIddictConstants.Claims.Name,
            roleType: OpenIddictConstants.Claims.Role);

        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id.ToString()));
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, user.UserName));
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.PreferredUsername, user.UserName));
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        var displayName = await _authService.GetUserDisplayNameAsync(user.Id);
        identity.AddClaim(new Claim(ClaimTypes.GivenName, displayName));

        if (!string.IsNullOrEmpty(user.UserEmail))
        {
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Email, user.UserEmail));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.UserEmail));
        }

        // 超级管理员：GetUserRolesAsync 会返回库中全部启用角色代码，逐条写入令牌与旧版「全量 permission」一样会把 JWE 撑到数百 KB，
        // 导致 Authorization 头过大、OpenIddict 验签与 GET userinfo 超时。令牌内仅写入占位角色；真实角色列表由 GetUserInfoAsync 返回。
        if (user.UserType == 2)
        {
            const string superAdminRoleMarker = "takt:super_admin";
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, superAdminRoleMarker));
            identity.AddClaim(new Claim(ClaimTypes.Role, superAdminRoleMarker));
        }
        else
        {
            var roles = await _authService.GetUserRolesAsync(user.Id);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, role));
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        // 不把每条菜单 permission 写入 access_token（权限由 TaktPermissionMiddleware 与 GetUserInfoAsync 提供）。

        var principal = new ClaimsPrincipal(identity);
        ApplyTokenClaimDestinations(principal);
        return principal;
    }

    /// <summary>
    /// 将声明标注写入 access_token / identity_token 的目的地（OpenIddict 要求显式标注）。
    /// </summary>
    private static void ApplyTokenClaimDestinations(ClaimsPrincipal principal)
    {
        principal.SetDestinations(claim => claim.Type switch
        {
            OpenIddictConstants.Claims.Subject or OpenIddictConstants.Claims.Name or OpenIddictConstants.Claims.PreferredUsername
            or OpenIddictConstants.Claims.Email or OpenIddictConstants.Claims.Role
            or ClaimTypes.NameIdentifier or ClaimTypes.Name or ClaimTypes.GivenName or ClaimTypes.Email or ClaimTypes.Role
                => [OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken],
            _ => [OpenIddictConstants.Destinations.AccessToken]
        });
    }
}
