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
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR;
using Takt.Domain.Entities.Routine.Tasks.SignalR;
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
    private readonly ITaktUserContext? _userContext;
    private readonly ITaktTenantContext? _tenantContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineService">在线用户服务</param>
    /// <param name="onlineRepository">在线用户仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    public TaktConnectHub(
        ITaktOnlineService onlineService,
        ITaktRepository<TaktOnline> onlineRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null)
    {
        _onlineService = onlineService;
        _onlineRepository = onlineRepository;
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

            // 创建在线用户记录（仓储工厂会自动根据 TaktOnline 实体类型切换到 Routine 数据库）
            var createDto = new TaktOnlineCreateDto
            {
                ConnectionId = connectionId ?? string.Empty,
                UserName = userName,
                UserId = userId,
                OnlineStatus = 0, // 0=在线
                ConnectIp = connectIp,
                ConnectLocation = null, // 可以通过IP地址解析地理位置
                UserAgent = userAgent,
                DeviceType = deviceType,
                BrowserType = browserType,
                OperatingSystem = operatingSystem,
                ConnectTime = connectTime
            };

            await _onlineService.CreateAsync(createDto);

            // 加入用户组（按用户名分组）
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(connectionId))
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
                userName ?? string.Empty, connectionId ?? string.Empty, ex.Message ?? string.Empty);
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
