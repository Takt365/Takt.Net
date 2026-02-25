// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Identity
// 文件名称：TaktAuthService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt认证服务实现，提供用户认证和授权功能
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using OpenIddict.Abstractions;
using System.Security.Claims;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Dtos.Identity;
using Takt.Application.Services;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.Entities.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt认证服务实现
/// </summary>
public class TaktAuthService : TaktServiceBase, ITaktAuthService
{
    private readonly ITaktRepository<TaktUser> _userRepository;
    private readonly ITaktRepository<TaktLoginLog> _loginLogRepository;
    private readonly ITaktRepository<TaktOnline> _onlineRepository;
    private readonly ITaktRepository<TaktRole> _roleRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktRoleMenu> _roleMenuRepository;
    private readonly ITaktRepository<TaktRolePermission> _rolePermissionRepository;
    private readonly ITaktRepository<TaktPermission> _permissionRepository;
    private readonly IOpenIddictTokenManager _tokenManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ITaktHolidayService _holidayService;
    private readonly ITaktLoginElsewhereNotifier? _loginElsewhereNotifier;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="loginLogRepository">登录日志仓储</param>
    /// <param name="onlineRepository">在线用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="roleMenuRepository">角色菜单关联仓储</param>
    /// <param name="rolePermissionRepository">角色权限关联仓储</param>
    /// <param name="permissionRepository">权限仓储</param>
    /// <param name="tokenManager">OpenIddict令牌管理器</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="configuration">配置</param>
    /// <param name="holidayService">假日服务（用于登录时获取当前假日）</param>
    /// <param name="loginElsewhereNotifier">在别处请求登录时的通知器（可选，用于向旧会话推送）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAuthService(
        ITaktRepository<TaktUser> userRepository,
        ITaktRepository<TaktLoginLog> loginLogRepository,
        ITaktRepository<TaktOnline> onlineRepository,
        ITaktRepository<TaktRole> roleRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktRoleMenu> roleMenuRepository,
        ITaktRepository<TaktRolePermission> rolePermissionRepository,
        ITaktRepository<TaktPermission> permissionRepository,
        IOpenIddictTokenManager tokenManager,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        ITaktHolidayService holidayService,
        ITaktLoginElsewhereNotifier? loginElsewhereNotifier = null,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _userRepository = userRepository;
        _loginLogRepository = loginLogRepository;
        _onlineRepository = onlineRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _roleMenuRepository = roleMenuRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _permissionRepository = permissionRepository;
        _tokenManager = tokenManager;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _holidayService = holidayService;
        _loginElsewhereNotifier = loginElsewhereNotifier;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录请求DTO</param>
    /// <returns>登录响应DTO</returns>
    /// <remarks>
    /// 注意：登录应该直接调用 /api/TaktAuth/connect/token 端点，此方法保留用于兼容性
    /// </remarks>
    public async Task<TaktLoginResponseDto> LoginAsync(TaktLoginDto dto)
    {
        // 验证用户名和密码
        var user = await _userRepository.GetAsync(u => u.UserName == dto.UserName && u.IsDeleted == 0);
        if (user == null)
        {
            throw new TaktBusinessException("用户名或密码错误");
        }

        // 验证密码
        if (!TaktEncryptHelper.VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new TaktBusinessException("用户名或密码错误");
        }

        // 检查用户状态（0=启用，1=禁用，3=锁定）
        if (user.UserStatus != 0)
        {
            throw new TaktBusinessException("用户已被禁用或锁定");
        }

        // 更新登录次数
        user.LoginCount++;
        user.UpdateTime = DateTime.Now;
        user.UpdateBy = user.UserName; // 设置更新人（自己更新自己的登录次数）
        await _userRepository.UpdateAsync(user);

        // 记录登录日志和在线记录
        await RecordLoginSuccessAsync(user, dto.UserName);

        // 注意：令牌应该通过 /api/TaktAuth/connect/token 端点生成
        // 此方法仅用于验证用户，实际登录应该调用 TaktAuthController.ExchangeAsync
        throw new NotImplementedException("登录应该直接调用 /api/TaktAuth/connect/token 端点，使用 TaktAuthController");
    }

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="dto">刷新令牌请求DTO</param>
    /// <returns>登录响应DTO</returns>
    /// <remarks>
    /// 注意：刷新令牌应该直接调用 /api/TaktAuth/connect/token 端点，此方法保留用于兼容性
    /// </remarks>
    public async Task<TaktLoginResponseDto> RefreshTokenAsync(TaktRefreshTokenDto dto)
    {
        // 注意：刷新令牌应该通过 /api/TaktAuth/connect/token 端点处理
        // 此方法仅用于兼容性，实际刷新应该调用 TaktAuthController.ExchangeAsync
        await Task.CompletedTask;
        throw new NotImplementedException("刷新令牌应该直接调用 /api/TaktAuth/connect/token 端点，使用 TaktAuthController");
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>用户名（用于日志记录）</returns>
    public async Task<string> LogoutAsync(string refreshToken)
    {
        // 先尝试从用户上下文获取用户名和用户ID
        var userName = _userContext?.GetCurrentUserName();
        var userId = _userContext?.GetCurrentUserId();

        // 如果无法从上下文获取，尝试从 refreshToken 中解析用户信息
        if ((string.IsNullOrEmpty(userName) || !userId.HasValue) && !string.IsNullOrEmpty(refreshToken))
        {
            try
            {
                var token = await _tokenManager.FindByReferenceIdAsync(refreshToken);
                if (token != null)
                {
                    // 从 token 的 Subject 中获取用户ID
                    var subject = await _tokenManager.GetSubjectAsync(token);
                    if (!string.IsNullOrEmpty(subject) && long.TryParse(subject, out var parsedUserId))
                    {
                        // 从数据库中查询用户信息
                        var user = await _userRepository.GetByIdAsync(parsedUserId);
                        if (user != null)
                        {
                            userName = user.UserName;
                            userId = user.Id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果无法从 token 中解析用户信息，记录警告但不影响退出登录流程
                TaktLogger.Warning(ex, "无法从 refreshToken 中解析用户信息: {ErrorMessage}", ex.Message);
            }
        }

        var logoutTime = DateTime.Now;

        // 1. 更新登录日志（记录退出时间和会话时长）
        if (!string.IsNullOrEmpty(userName))
        {
            try
            {
                // 查找该用户最近的登录日志（未退出的）
                var loginLogs = await _loginLogRepository.FindAsync(
                    log => log.UserName == userName &&
                           log.LogoutTime == null &&
                           log.LoginStatus == 0 &&
                           log.IsDeleted == 0);

                if (loginLogs.Count > 0)
                {
                    // 取最近的登录日志（按登录时间降序）
                    var latestLoginLog = loginLogs.OrderByDescending(log => log.LoginTime).First();
                    var sessionDuration = (int)(logoutTime - latestLoginLog.LoginTime).TotalSeconds;

                    latestLoginLog.LogoutTime = logoutTime;
                    latestLoginLog.SessionDuration = sessionDuration;
                    latestLoginLog.UpdateTime = logoutTime;
                    latestLoginLog.UpdateBy = userName; // 设置更新人

                    await _loginLogRepository.UpdateAsync(latestLoginLog);

                    TaktLogger.Information("退出登录日志已更新: UserName: {UserName}, LoginTime: {LoginTime}, LogoutTime: {LogoutTime}, SessionDuration: {SessionDuration}秒",
                        userName, latestLoginLog.LoginTime, logoutTime, sessionDuration);
                }
            }
            catch (Exception ex)
            {
                // 如果更新登录日志失败，记录警告但不影响退出登录流程
                TaktLogger.Warning(ex, "更新退出登录日志失败，用户: {UserName}, 错误: {ErrorMessage}", userName, ex.Message);
            }
        }

        // 2. 不在此处更新在线记录：单设备登录时，若被踢设备调用 logout，不应把新登录设备的在线记录置为离线。
        //    当前设备退出后，SignalR OnDisconnected 会将该连接对应的在线记录置为离线。

        // 3. 查找并删除刷新令牌
        var tokenToDelete = await _tokenManager.FindByReferenceIdAsync(refreshToken);
        if (tokenToDelete != null)
        {
            await _tokenManager.DeleteAsync(tokenToDelete);
        }

        // 返回用户名（如果无法获取，返回"未知用户"）
        return userName ?? "未知用户";
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns>用户信息DTO</returns>
    public async Task<TaktUserInfoDto> GetUserInfoAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated != true)
        {
            throw new TaktBusinessException("用户未登录");
        }

        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? httpContext.User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            throw new TaktBusinessException("用户ID无效");
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null || user.IsDeleted != 0)
        {
            throw new TaktBusinessException("用户不存在");
        }

        return await BuildUserInfoAsync(user);
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>角色代码列表</returns>
    public async Task<List<string>> GetUserRolesAsync(long userId)
    {
        // 如果用户是超级管理员（UserType = 2），返回所有角色
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new List<string>();
        }

        // 超级管理员拥有所有角色（这里可以根据实际需求返回特定角色）
        if (user.UserType == 2)
        {
            // 获取所有启用的角色
            var allRoles = await _roleRepository.FindAsync(r => r.RoleStatus == 0 && r.IsDeleted == 0);
            return allRoles.Select(r => r.RoleCode).Distinct().ToList();
        }

        // 获取用户的角色关联
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
        if (userRoles.Count == 0)
        {
            return new List<string>();
        }

        // 获取角色ID列表
        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();

        // 获取角色实体（只获取启用的角色）
        var roles = await _roleRepository.FindAsync(r => roleIds.Contains(r.Id) && r.RoleStatus == 0 && r.IsDeleted == 0);

        // 返回角色代码列表
        return roles.Select(r => r.RoleCode).Distinct().ToList();
    }

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>权限标识列表</returns>
    public async Task<List<string>> GetUserPermissionsAsync(long userId)
    {
        // 如果用户是超级管理员（UserType = 2），返回所有权限
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new List<string>();
        }

        // 超级管理员：返回所有启用的权限标识（来自 TaktPermission 表）
        if (user.UserType == 2)
        {
            var allPermissions = await _permissionRepository.FindAsync(p => p.PermissionStatus == 0 && p.IsDeleted == 0);
            return allPermissions.Select(p => p.PermissionCode).Distinct().ToList();
        }

        // 普通用户：通过角色权限关联表获取权限标识
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
        if (userRoles.Count == 0)
            return new List<string>();

        var roleIds = userRoles.Select(ur => ur.RoleId).Distinct().ToList();
        var rolePermissions = await _rolePermissionRepository.FindAsync(rp => roleIds.Contains(rp.RoleId) && rp.IsDeleted == 0);
        if (rolePermissions.Count == 0)
            return new List<string>();

        var permissionIds = rolePermissions.Select(rp => rp.PermissionId).Distinct().ToList();
        var permissions = await _permissionRepository.FindAsync(p => permissionIds.Contains(p.Id) && p.PermissionStatus == 0 && p.IsDeleted == 0);
        return permissions.Select(p => p.PermissionCode).Distinct().ToList();
    }

    /// <summary>
    /// 获取当前假日（用于登录 token 写入今日是否假期，供前端问候语/主题色）
    /// </summary>
    public async Task<TaktHolidayDto?> GetCurrentHolidayForLoginAsync(DateTime date, string? region)
    {
        TaktLogger.Information("获取当前假日: Date={Date:yyyy-MM-dd}, Region={Region}", date, region ?? "(null)");
        var result = await _holidayService.GetHolidayThemeAsync(date, region);
        if (result != null)
        {
            TaktLogger.Information("当前假日已命中: HolidayName={HolidayName}, HolidayGreeting={HolidayGreeting}, HolidayTheme={HolidayTheme}",
                result.HolidayName ?? "", result.HolidayGreeting ?? "", result.HolidayTheme ?? "");
        }
        else
        {
            TaktLogger.Information("当前日期未命中假日: Date={Date:yyyy-MM-dd}, Region={Region}", date, region ?? "(null)");
        }

        return result;
    }

    /// <summary>
    /// 记录登录成功（登录日志和在线记录）
    /// </summary>
    /// <param name="user">用户实体</param>
    /// <param name="userName">用户名</param>
    /// <returns>任务</returns>
    public async Task RecordLoginSuccessAsync(TaktUser user, string userName)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return;
        }

        // 登录日志与在线记录由 TryRecordLoginSuccessAsync(forceLogin: true) 内统一写入
        var result = await TryRecordLoginSuccessAsync(user, userName, forceLogin: true);
        if (!result.Success)
        {
            throw new InvalidOperationException("RecordLoginSuccessAsync 应使用 forceLogin 或未达设备上限");
        }
    }

    /// <inheritdoc />
    public async Task<RecordLoginSuccessResult> TryRecordLoginSuccessAsync(TaktUser user, string userName, bool forceLogin)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return new RecordLoginSuccessResult { Success = true };
        }

        var clientIp = GetClientIpAddress(httpContext);
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        var deviceType = ParseDeviceType(userAgent);
        var browserType = ParseBrowserType(userAgent);
        var operatingSystem = ParseOperatingSystem(userAgent);
        var loginTime = DateTime.Now;
        var configId = _tenantContext?.GetCurrentConfigId() ?? "0";

        var singleDeviceLogin = _configuration.GetValue<bool>("SingleDeviceLogin", false);
        var maxDevices = singleDeviceLogin ? 3 : 1;

        var onlines = (await _onlineRepository.FindAsync(o =>
            o.UserId == user.Id && o.OnlineStatus == 0 && o.IsDeleted == 0))
            .OrderBy(o => o.ConnectTime)
            .ToList();

        if (onlines.Count >= maxDevices && !forceLogin)
        {
            var loginLogReject = new TaktLoginLog
            {
                UserName = userName,
                LoginIp = clientIp,
                LoginLocation = null,
                LoginType = "Password",
                UserAgent = userAgent,
                LoginStatus = 1,
                LoginMsg = "已达设备上限，未强制登录",
                LoginTime = loginTime,
                ConfigId = configId,
                CreateBy = userName,
                CreateTime = loginTime
            };
            FillIpLocationInfo(loginLogReject.LoginIp,
                location => loginLogReject.LoginLocation = location,
                country => loginLogReject.LoginCountry = country,
                province => loginLogReject.LoginProvince = province,
                city => loginLogReject.LoginCity = city,
                isp => loginLogReject.LoginIsp = isp);
            await _loginLogRepository.CreateAsync(loginLogReject);

            var existingSessions = onlines.Select(o => new TaktExistingSessionDto
            {
                ConnectLocation = o.ConnectLocation,
                ConnectIp = o.ConnectIp,
                DeviceType = o.DeviceType,
                BrowserType = o.BrowserType,
                ConnectTime = o.ConnectTime
            }).ToList();
            return new RecordLoginSuccessResult { Success = false, ExistingSessions = existingSessions };
        }

        var loginLogSuccess = new TaktLoginLog
        {
            UserName = userName,
            LoginIp = clientIp,
            LoginLocation = null,
            LoginType = "Password",
            UserAgent = userAgent,
            LoginStatus = 0,
            LoginMsg = "登录成功",
            LoginTime = loginTime,
            ConfigId = configId,
            CreateBy = userName,
            CreateTime = loginTime
        };
        FillIpLocationInfo(loginLogSuccess.LoginIp,
            location => loginLogSuccess.LoginLocation = location,
            country => loginLogSuccess.LoginCountry = country,
            province => loginLogSuccess.LoginProvince = province,
            city => loginLogSuccess.LoginCity = city,
            isp => loginLogSuccess.LoginIsp = isp);
        await _loginLogRepository.CreateAsync(loginLogSuccess);

        var toKick = onlines.Count >= maxDevices ? onlines.Count - maxDevices + 1 : 0;
        var requestLocation = loginLogSuccess.LoginLocation ?? clientIp ?? "未知位置";
        var toNotifyConnectionIds = onlines.Take(toKick)
            .Where(o => !string.IsNullOrEmpty(o.ConnectionId) && !o.ConnectionId.StartsWith("Login_", StringComparison.Ordinal))
            .Select(o => o.ConnectionId)
            .ToList();

        for (var i = 0; i < toKick; i++)
        {
            onlines[i].OnlineStatus = 1;
            onlines[i].DisconnectTime = loginTime;
            await _onlineRepository.UpdateAsync(onlines[i]);
        }

        if (_loginElsewhereNotifier != null && toNotifyConnectionIds.Count > 0)
        {
            await _loginElsewhereNotifier.NotifyLoginRequestElsewhereAsync(toNotifyConnectionIds, requestLocation);
        }

        var online = new TaktOnline
        {
            ConnectionId = $"Login_{user.Id}_{loginTime:yyyyMMddHHmmss}",
            UserName = userName,
            UserId = user.Id,
            OnlineStatus = 0,
            ConnectIp = clientIp,
            ConnectLocation = null,
            UserAgent = userAgent,
            DeviceType = deviceType,
            BrowserType = browserType,
            OperatingSystem = operatingSystem,
            ConnectTime = loginTime,
            LastActiveTime = loginTime,
            ConfigId = configId,
            CreateBy = userName,
            CreateTime = loginTime
        };
        FillIpLocationInfo(online.ConnectIp,
            location => online.ConnectLocation = location,
            country => online.ConnectCountry = country,
            province => online.ConnectProvince = province,
            city => online.ConnectCity = city,
            isp => online.ConnectIsp = isp);
        await _onlineRepository.CreateAsync(online);
        return new RecordLoginSuccessResult { Success = true };
    }

    /// <inheritdoc />
    public async Task<int> RevokeRefreshTokensByUserIdAsync(long userId, CancellationToken cancellationToken = default)
    {
        var subject = userId.ToString();
        var count = 0;
        await foreach (var token in _tokenManager.FindBySubjectAsync(subject, cancellationToken))
        {
            await _tokenManager.DeleteAsync(token, cancellationToken);
            count++;
        }
        return count;
    }

    /// <summary>
    /// 记录登录失败（登录日志和失败次数累加）
    /// </summary>
    /// <param name="user">用户实体（如果用户存在）</param>
    /// <param name="userName">用户名</param>
    /// <param name="failureReason">失败原因</param>
    /// <returns>任务</returns>
    public async Task RecordLoginFailureAsync(TaktUser? user, string userName, string failureReason)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return;
        }

        // 获取客户端IP地址
        var clientIp = GetClientIpAddress(httpContext);

        // 获取User-Agent
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();

        var loginTime = DateTime.Now;
        var configId = _tenantContext?.GetCurrentConfigId() ?? "0";

        // 1. 记录登录失败日志（TaktLoginLog 属于 Identity 数据库，ConfigId="0"）
        var loginLog = new TaktLoginLog
        {
            UserName = userName,
            LoginIp = clientIp,
            LoginLocation = null, // 将通过IP定位填充
            LoginType = "Password", // 账号密码登录
            UserAgent = userAgent,
            LoginStatus = 1, // 1=失败
            LoginMsg = failureReason,
            LoginTime = loginTime,
            ConfigId = configId,
            CreateBy = "Takt365", // 系统记录
            CreateTime = loginTime
        };
        
        // 填充IP定位信息
        FillIpLocationInfo(loginLog.LoginIp,
            location => loginLog.LoginLocation = location,
            country => loginLog.LoginCountry = country,
            province => loginLog.LoginProvince = province,
            city => loginLog.LoginCity = city,
            isp => loginLog.LoginIsp = isp);
        
        await _loginLogRepository.CreateAsync(loginLog);

        // 2. 如果用户存在，增加失败次数并检查是否需要锁定
        if (user != null)
        {
            // 从配置中读取账户锁定配置
            var accountLockSection = _configuration.GetSection("AccountLock");
            var lockEnabled = accountLockSection.GetValue<bool>("Enabled", true);
            var errorLimit = accountLockSection.GetValue<int>("ErrorLimit", 5);
            var lockReasonTemplate = accountLockSection.GetValue<string>("LockReason", "连续登录失败{ErrorCount}次，达到错误次数限制（{ErrorLimit}次）");

            // 如果锁定功能已启用，增加失败次数并检查是否需要锁定
            if (lockEnabled)
            {
                user.ErrorCount++;
                var shouldLock = user.ErrorCount >= errorLimit;

                if (shouldLock)
                {
                    // 锁定账户（系统自动触发）
                    user.UserStatus = 3; // 3=锁定
                    user.LockTime = loginTime;
                    user.LockBy = "Takt365";
                    user.LockReason = lockReasonTemplate
                        .Replace("{ErrorCount}", user.ErrorCount.ToString())
                        .Replace("{ErrorLimit}", errorLimit.ToString());
                    
                    TaktLogger.Warning("账户已自动锁定：用户名: {UserName}, 用户ID: {UserId}, 失败次数: {ErrorCount}, 错误限制: {ErrorLimit}", 
                        userName, user.Id, user.ErrorCount, errorLimit);
                }
                else
                {
                    TaktLogger.Warning("登录失败：用户名: {UserName}, 用户ID: {UserId}, 失败次数: {ErrorCount}/{ErrorLimit}, 原因: {FailureReason}", 
                        userName, user.Id, user.ErrorCount, errorLimit, failureReason);
                }
            }
            else
            {
                // 锁定功能已禁用，只记录失败日志，不增加失败次数
                TaktLogger.Warning("登录失败：用户名: {UserName}, 用户ID: {UserId}, 原因: {FailureReason}（账户锁定功能已禁用）", 
                    userName, user.Id, failureReason);
            }

            user.UpdateTime = loginTime;
            await _userRepository.UpdateAsync(user);
        }
        else
        {
            // 用户不存在，只记录日志
            TaktLogger.Warning("登录失败：用户不存在，用户名: {UserName}, 原因: {FailureReason}", userName, failureReason);
        }
    }

    /// <summary>
    /// 填充IP定位信息
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <param name="setLocation">设置地点回调</param>
    /// <param name="setCountry">设置国家回调</param>
    /// <param name="setProvince">设置省份回调</param>
    /// <param name="setCity">设置城市回调</param>
    /// <param name="setIsp">设置ISP回调</param>
    private static void FillIpLocationInfo(string? ip,
        Action<string?> setLocation,
        Action<string?> setCountry,
        Action<string?> setProvince,
        Action<string?> setCity,
        Action<string?> setIsp)
    {
        if (string.IsNullOrWhiteSpace(ip))
        {
            return;
        }

        try
        {
            // 检查IP定位功能是否已初始化
            if (!TaktLocationHelper.IsInitialized())
            {
                return;
            }

            var location = TaktLocationHelper.Search(ip);
            if (location != null)
            {
                setLocation(location.FormattedAddress);
                setCountry(location.Country);
                setProvince(location.Province);
                setCity(location.City);
                setIsp(location.Isp);
            }
        }
        catch (Exception ex)
        {
            // IP定位失败不影响主流程，只记录日志
            TaktLogger.Debug(ex, "[TaktAuthService] IP定位失败: {Ip}", ip);
        }
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private string? GetClientIpAddress(HttpContext httpContext)
    {
        // 优先从 X-Forwarded-For 头获取（适用于反向代理场景）
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            // X-Forwarded-For 可能包含多个IP，取第一个
            var ips = forwardedFor.Split(',');
            if (ips.Length > 0)
            {
                return ips[0].Trim();
            }
        }

        // 从 X-Real-IP 头获取
        var realIp = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        // 从连接信息获取
        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// 解析设备类型
    /// </summary>
    private string? ParseDeviceType(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLowerInvariant();
        if (ua.Contains("mobile") || ua.Contains("android") || ua.Contains("iphone") || ua.Contains("ipod"))
            return "Mobile";
        if (ua.Contains("tablet") || ua.Contains("ipad"))
            return "Tablet";
        return "PC";
    }

    /// <summary>
    /// 解析浏览器类型
    /// </summary>
    private string? ParseBrowserType(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLowerInvariant();
        if (ua.Contains("chrome") && !ua.Contains("edg"))
            return "Chrome";
        if (ua.Contains("firefox"))
            return "Firefox";
        if (ua.Contains("safari") && !ua.Contains("chrome"))
            return "Safari";
        if (ua.Contains("edg"))
            return "Edge";
        if (ua.Contains("opera") || ua.Contains("opr"))
            return "Opera";
        if (ua.Contains("msie") || ua.Contains("trident"))
            return "IE";
        return "Unknown";
    }

    /// <summary>
    /// 解析操作系统
    /// </summary>
    private string? ParseOperatingSystem(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLowerInvariant();
        if (ua.Contains("windows"))
            return "Windows";
        if (ua.Contains("mac os") || ua.Contains("macos"))
            return "macOS";
        if (ua.Contains("linux"))
            return "Linux";
        if (ua.Contains("android"))
            return "Android";
        if (ua.Contains("ios") || ua.Contains("iphone") || ua.Contains("ipad") || ua.Contains("ipod"))
            return "iOS";
        return "Unknown";
    }

    /// <summary>
    /// 从当前请求获取国别码（用于查询假日）
    /// </summary>
    private static string GetCountryCodeFromRequest(IHttpContextAccessor httpContextAccessor)
    {
        var culture = httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture?.UICulture?.Name
            ?? CultureInfo.CurrentUICulture.Name
            ?? "zh-CN";
        if (string.IsNullOrWhiteSpace(culture)) return "CN";
        var c = culture.Trim();
        if (c.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return "CN";
        if (c.StartsWith("ja", StringComparison.OrdinalIgnoreCase)) return "JP";
        if (c.StartsWith("en", StringComparison.OrdinalIgnoreCase)) return "US";
        return "CN";
    }

    /// <summary>
    /// 构建用户信息
    /// </summary>
    private async Task<TaktUserInfoDto> BuildUserInfoAsync(TaktUser user)
    {
        // 获取用户角色和权限
        var roles = await GetUserRolesAsync(user.Id);
        var permissions = await GetUserPermissionsAsync(user.Id);

        var region = GetCountryCodeFromRequest(_httpContextAccessor);
        var holiday = await GetCurrentHolidayForLoginAsync(DateTime.Now, region);

        var userInfo = new TaktUserInfoDto
        {
            UserId = user.Id.ToString(),
            UserName = user.UserName,
            RealName = user.RealName,
            EnglishName = user.EnglishName ?? string.Empty,
            NickName = user.NickName ?? string.Empty,
            Avatar = user.Avatar ?? string.Empty,
            Roles = roles,
            Permissions = permissions,
            HolidayToday = holiday != null,
            HolidayName = holiday?.HolidayName,
            HolidayGreeting = holiday?.HolidayGreeting,
            HolidayQuote = holiday?.HolidayQuote,
            HolidayTheme = holiday?.HolidayTheme?.Trim()
        };

        return userInfo;
    }
}
