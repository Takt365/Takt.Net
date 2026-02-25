// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.SignalR
// 文件名称：TaktConnectHub.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt连接Hub，用于管理在线用户连接
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Takt.Application.Dtos.Routine.SignalR;
using Takt.Application.Services.Routine.SignalR;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.SignalR;

/// <summary>
/// Takt连接Hub，用于管理在线用户连接
/// </summary>
[Authorize] // 保护 Hub，要求连接必须携带有效 Token
public class TaktConnectHub : Hub
{
    private readonly ITaktOnlineService _onlineService;
    private readonly ITaktRepository<TaktOnline> _onlineRepository;
    private readonly IConfiguration _configuration;
    private readonly ITaktUserContext? _userContext;
    private readonly ITaktTenantContext? _tenantContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineService">在线用户服务</param>
    /// <param name="onlineRepository">在线用户仓储</param>
    /// <param name="configuration">配置（用于读取 SingleDeviceLogin）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    public TaktConnectHub(
        ITaktOnlineService onlineService,
        ITaktRepository<TaktOnline> onlineRepository,
        IConfiguration configuration,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null)
    {
        _onlineService = onlineService;
        _onlineRepository = onlineRepository;
        _configuration = configuration;
        _userContext = userContext;
        _tenantContext = tenantContext;
    }

    /// <summary>
    /// 客户端连接时调用
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var httpContext = Context.GetHttpContext();
        
        // 调试信息：检查认证状态和 token
        var isAuthenticated = httpContext?.User?.Identity?.IsAuthenticated ?? false;
        var accessTokenFromQuery = httpContext?.Request.Query["access_token"].ToString();
        var authHeader = httpContext?.Request.Headers["Authorization"].ToString();
        
        TaktLogger.Debug("SignalR 连接调试信息: ConnectionId: {ConnectionId}, IsAuthenticated: {IsAuthenticated}, HasAccessTokenFromQuery: {HasAccessToken}, HasAuthHeader: {HasAuthHeader}", 
            connectionId ?? string.Empty, isAuthenticated, !string.IsNullOrEmpty(accessTokenFromQuery), !string.IsNullOrEmpty(authHeader));
        
        var userName = _userContext?.GetCurrentUserName() ?? "Anonymous";
        TaktLogger.Information("开始处理连接 Hub 连接请求，用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId ?? string.Empty);
        
        try
        {
            var userId = _userContext?.GetCurrentUserId();

            // 获取客户端信息
            var connectIp = httpContext?.Connection.RemoteIpAddress?.ToString();
            var userAgent = httpContext?.Request.Headers["User-Agent"].ToString();
            var connectTime = DateTime.Now;

            // 解析设备信息（简化版，实际可以使用 UserAgent 解析库）
            var deviceType = ParseDeviceType(userAgent);
            var browserType = ParseBrowserType(userAgent);
            var operatingSystem = ParseOperatingSystem(userAgent);

            // 按 SingleDeviceLogin 限制同一账号设备数：false=最多1台，true=最多3台。同设备复用 Login_ 记录，新设备则踢最旧会话再创建
            if (userId.HasValue)
            {
                var singleDeviceLogin = _configuration.GetValue<bool>("SingleDeviceLogin", false);
                var maxDevices = singleDeviceLogin ? 3 : 1;

                var onlines = (await _onlineRepository.FindAsync(o =>
                    o.IsDeleted == 0 && o.OnlineStatus == 0 && o.UserId == userId.Value))
                    .OrderBy(o => o.ConnectTime)
                    .ToList();

                // 1) 存在 Login_ 占位记录（登录后尚未连 Hub）：复用并只更新会话 ID，不新建
                var sameDevice = onlines.FirstOrDefault(o => o.ConnectionId.StartsWith("Login_", StringComparison.Ordinal));
                if (sameDevice != null)
                {
                    sameDevice.ConnectionId = connectionId;
                    sameDevice.ConnectIp = connectIp;
                    sameDevice.UserAgent = userAgent;
                    sameDevice.DeviceType = deviceType;
                    sameDevice.BrowserType = browserType;
                    sameDevice.OperatingSystem = operatingSystem;
                    sameDevice.ConnectTime = connectTime;
                    sameDevice.LastActiveTime = connectTime;
                    await _onlineRepository.UpdateAsync(sameDevice);
                    TaktLogger.Debug("已更新在线记录 ConnectionId（Login_ 占位），用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId ?? string.Empty);
                }
                else if (onlines.Count > 0)
                {
                    // 2) 无 Login_ 但已有在线记录：同 IP 视为同设备重连（健康检查/刷新后重连），只更新会话 ID；否则为新设备
                    var reconnectRecord = onlines.FirstOrDefault(o =>
                        string.Equals(o.ConnectIp, connectIp, StringComparison.OrdinalIgnoreCase));
                    if (reconnectRecord != null)
                    {
                        reconnectRecord.ConnectionId = connectionId;
                        reconnectRecord.ConnectIp = connectIp;
                        reconnectRecord.UserAgent = userAgent;
                        reconnectRecord.DeviceType = deviceType;
                        reconnectRecord.BrowserType = browserType;
                        reconnectRecord.OperatingSystem = operatingSystem;
                        reconnectRecord.ConnectTime = connectTime;
                        reconnectRecord.LastActiveTime = connectTime;
                        await _onlineRepository.UpdateAsync(reconnectRecord);
                        TaktLogger.Debug("已更新在线记录 ConnectionId（同设备重连），用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId ?? string.Empty);
                    }
                    else
                    {
                        // 新设备：按设备数限制踢人后创建
                        var toKick = onlines.Count >= maxDevices ? onlines.Count - maxDevices + 1 : 0;
                        for (var i = 0; i < toKick; i++)
                        {
                            onlines[i].OnlineStatus = 1;
                            onlines[i].DisconnectTime = connectTime;
                            await _onlineRepository.UpdateAsync(onlines[i]);
                        }
                        var createDto = new TaktOnlineCreateDto
                        {
                            ConnectionId = connectionId,
                            UserName = userName,
                            UserId = userId,
                            OnlineStatus = 0,
                            ConnectIp = connectIp,
                            ConnectLocation = null,
                            UserAgent = userAgent,
                            DeviceType = deviceType,
                            BrowserType = browserType,
                            OperatingSystem = operatingSystem,
                            ConnectTime = connectTime
                        };
                        await _onlineService.CreateAsync(createDto);
                    }
                }
                else
                {
                    // 3) 该用户无任何在线记录时才创建新记录
                    var createDto = new TaktOnlineCreateDto
                    {
                        ConnectionId = connectionId,
                        UserName = userName,
                        UserId = userId,
                        OnlineStatus = 0,
                        ConnectIp = connectIp,
                        ConnectLocation = null,
                        UserAgent = userAgent,
                        DeviceType = deviceType,
                        BrowserType = browserType,
                        OperatingSystem = operatingSystem,
                        ConnectTime = connectTime
                    };
                    await _onlineService.CreateAsync(createDto);
                }
            }
            else
            {
                var createDto = new TaktOnlineCreateDto
                {
                    ConnectionId = connectionId,
                    UserName = userName,
                    UserId = null,
                    OnlineStatus = 0,
                    ConnectIp = connectIp,
                    ConnectLocation = null,
                    UserAgent = userAgent,
                    DeviceType = deviceType,
                    BrowserType = browserType,
                    OperatingSystem = operatingSystem,
                    ConnectTime = connectTime
                };
                await _onlineService.CreateAsync(createDto);
            }

            // 加入用户组（按用户名分组）
            if (!string.IsNullOrEmpty(userName))
            {
                await Groups.AddToGroupAsync(connectionId, $"User_{userName}");
            }

            // 向当前客户端发送上线欢迎消息
            var realName = _userContext?.GetCurrentRealName();
            var welcomeMessage = $"欢迎 {realName ?? userName} 上线！连接成功，当前时间：{connectTime:yyyy-MM-dd HH:mm:ss}";
            
            await Clients.Caller.SendAsync("OnlineMessage", new
            {
                Message = welcomeMessage,
                MessageType = "Online",
                UserName = userName,
                UserId = userId,
                RealName = realName,
                ConnectTime = connectTime,
                ConnectIp = connectIp,
                DeviceType = deviceType
            });

            // 通知其他客户端有新用户上线
            await Clients.Others.SendAsync("UserConnected", new
            {
                UserName = userName,
                UserId = userId,
                ConnectTime = connectTime
            });

            TaktLogger.Information("连接 Hub 连接成功，用户: {UserName}, ConnectionId: {ConnectionId}, IP: {ConnectIp}, 设备: {DeviceType}", 
                userName, connectionId ?? string.Empty, connectIp ?? string.Empty, deviceType ?? string.Empty);

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "连接 Hub 连接失败，用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
                userName, connectionId, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 客户端断开连接时调用
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var userName = _userContext?.GetCurrentUserName() ?? "Anonymous";
        
        if (exception != null)
        {
            TaktLogger.Warning("连接 Hub 断开连接（异常），用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
                userName, connectionId, exception.Message);
        }
        else
        {
            TaktLogger.Information("连接 Hub 断开连接（正常），用户: {UserName}, ConnectionId: {ConnectionId}", 
                userName, connectionId);
        }
        
        try
        {
            // 查找在线用户记录（仓储工厂会自动根据 TaktOnline 实体类型切换到 Routine 数据库）
            var online = await _onlineRepository.GetAsync(o => o.ConnectionId == connectionId && o.IsDeleted == 0);
            if (online != null)
            {
                // 更新断开时间和连接时长
                var disconnectTime = DateTime.Now;
                var connectTime = online.ConnectTime;
                var connectionDuration = (int)(disconnectTime - connectTime).TotalSeconds;

                online.DisconnectTime = disconnectTime;
                online.ConnectionDuration = connectionDuration;
                online.OnlineStatus = 1; // 1=离线

                await _onlineRepository.UpdateAsync(online);
                TaktLogger.Information("连接 Hub 断开连接成功，用户: {UserName}, ConnectionId: {ConnectionId}, 连接时长: {Duration}秒", 
                    userName, connectionId, connectionDuration);
            }

            // 从用户组中移除
            if (!string.IsNullOrEmpty(userName))
            {
                await Groups.RemoveFromGroupAsync(connectionId, $"User_{userName}");
            }

            // 通知其他客户端用户已离线
            await Clients.Others.SendAsync("UserDisconnected", new
            {
                UserName = userName,
                DisconnectTime = DateTime.Now
            });

            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "连接 Hub 断开连接处理失败，用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
                userName, connectionId, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 心跳更新（客户端定期调用以保持连接活跃）
    /// </summary>
    public async Task Heartbeat()
    {
        try
        {
            var connectionId = Context.ConnectionId;
            
            // 仓储工厂会自动根据 TaktOnline 实体类型切换到 Routine 数据库
            await _onlineService.UpdateLastActiveTimeAsync(connectionId);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "处理心跳更新时发生错误");
        }
    }

    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    public async Task<List<object>> GetOnlineUsers()
    {
        try
        {
            // 直接使用仓储查询在线用户，避免复杂的查询表达式导致 SQL 转换错误
            var onlines = await _onlineRepository.FindAsync(o => o.IsDeleted == 0 && o.OnlineStatus == 0);
            
            return onlines.Select(u => new
            {
                UserName = u.UserName,
                UserId = u.UserId,
                ConnectTime = u.ConnectTime,
                LastActiveTime = u.LastActiveTime
            }).Cast<object>().ToList();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取在线用户列表时发生错误");
            return new List<object>();
        }
    }

    /// <summary>
    /// 解析设备类型
    /// </summary>
    private static string? ParseDeviceType(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLower();
        if (ua.Contains("mobile") || ua.Contains("android") || ua.Contains("iphone") || ua.Contains("ipad"))
            return "Mobile";
        if (ua.Contains("tablet"))
            return "Tablet";
        return "PC";
    }

    /// <summary>
    /// 解析浏览器类型
    /// </summary>
    private static string? ParseBrowserType(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLower();
        if (ua.Contains("chrome") && !ua.Contains("edg"))
            return "Chrome";
        if (ua.Contains("firefox"))
            return "Firefox";
        if (ua.Contains("safari") && !ua.Contains("chrome"))
            return "Safari";
        if (ua.Contains("edg"))
            return "Edge";
        if (ua.Contains("opera"))
            return "Opera";
        return "Unknown";
    }

    /// <summary>
    /// 解析操作系统
    /// </summary>
    private static string? ParseOperatingSystem(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        var ua = userAgent.ToLower();
        if (ua.Contains("windows"))
            return "Windows";
        if (ua.Contains("mac os") || ua.Contains("macos"))
            return "macOS";
        if (ua.Contains("linux"))
            return "Linux";
        if (ua.Contains("android"))
            return "Android";
        if (ua.Contains("ios") || ua.Contains("iphone") || ua.Contains("ipad"))
            return "iOS";
        return "Unknown";
    }
}
