// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.SignalR
// 文件名称：TaktNotificationHub.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt通知Hub，用于管理在线消息通知
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.SignalR;

/// <summary>
/// Takt通知Hub，用于管理在线消息通知
/// </summary>
[Authorize] // 保护 Hub，要求连接必须携带有效 Token
public class TaktNotificationHub : Hub
{
    private readonly ITaktOnlineService _onlineService;
    private readonly ITaktUserContext? _userContext;
    private readonly ITaktTenantContext? _tenantContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineService">在线用户服务（用于查找接收者的连接）</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    public TaktNotificationHub(
        ITaktOnlineService onlineService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null)
    {
        _onlineService = onlineService;
        _userContext = userContext;
        _tenantContext = tenantContext;
    }

    /// <summary>
    /// 客户端连接时调用
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var userName = _userContext?.GetCurrentUserName() ?? "Anonymous";
        TaktLogger.Information("开始处理通知 Hub 连接请求，用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId);
        
        try
        {
            if (!string.IsNullOrEmpty(userName) && userName != "Anonymous")
            {
                // 加入用户组（按用户名分组，用于接收个人消息）
                await Groups.AddToGroupAsync(connectionId, $"User_{userName}");
                
                // 加入通知组（所有用户都在此组中，用于接收广播消息）
                await Groups.AddToGroupAsync(connectionId, "Notifications");

                // 向当前客户端发送上线欢迎消息
                var userId = _userContext?.GetCurrentUserId();
                var realName = _userContext?.GetCurrentRealName();
                var connectTime = DateTime.Now;
                var welcomeMessage = $"欢迎 {realName ?? userName} 上线！通知服务连接成功，当前时间：{connectTime:yyyy-MM-dd HH:mm:ss}";
                
                await Clients.Caller.SendAsync("OnlineMessage", new
                {
                    Message = welcomeMessage,
                    MessageType = "Online",
                    UserName = userName,
                    UserId = userId,
                    RealName = realName,
                    ConnectTime = connectTime
                });

                TaktLogger.Information("通知 Hub 连接成功，用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId);
            }
            else
            {
                TaktLogger.Warning("通知 Hub 连接：用户名为空，ConnectionId: {ConnectionId}", connectionId);
            }

            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "通知 Hub 连接失败，用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
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
            TaktLogger.Warning("通知 Hub 断开连接（异常），用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
                userName, connectionId, exception.Message);
        }
        else
        {
            TaktLogger.Information("通知 Hub 断开连接（正常），用户: {UserName}, ConnectionId: {ConnectionId}", 
                userName, connectionId);
        }
        
        try
        {
            if (!string.IsNullOrEmpty(userName) && userName != "Anonymous")
            {
                await Groups.RemoveFromGroupAsync(connectionId, $"User_{userName}");
                await Groups.RemoveFromGroupAsync(connectionId, "Notifications");

                TaktLogger.Information("通知 Hub 断开连接成功，用户: {UserName}, ConnectionId: {ConnectionId}", userName, connectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "通知 Hub 断开连接处理失败，用户: {UserName}, ConnectionId: {ConnectionId}, 错误: {ErrorMessage}", 
                userName, connectionId, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 发送消息给指定用户
    /// </summary>
    /// <param name="toUserName">接收者用户名</param>
    /// <param name="messageTitle">消息标题</param>
    /// <param name="messageContent">消息内容</param>
    /// <param name="messageType">消息类型（如：Text、Image、File、System）</param>
    /// <param name="messageGroup">消息分组（如：Chat、Notification、Alert）</param>
    /// <param name="messageExtData">消息扩展数据（JSON格式）</param>
    public async Task SendMessage(
        string toUserName,
        string messageContent,
        string? messageTitle = null,
        string messageType = "Text",
        string? messageGroup = null,
        string? messageExtData = null)
    {
        try
        {
            var fromUserName = _userContext?.GetCurrentUserName() ?? "Takt365";
            var fromUserId = _userContext?.GetCurrentUserId();
            var sendTime = DateTime.Now;

            // 创建消息DTO（这里需要消息服务，暂时先发送实时通知）
            var message = new
            {
                FromUserName = fromUserName,
                FromUserId = fromUserId,
                ToUserName = toUserName,
                ToUserId = (long?)null, // 可以通过用户名查询用户ID
                MessageTitle = messageTitle,
                MessageContent = messageContent,
                MessageType = messageType,
                MessageGroup = messageGroup ?? "Chat",
                ReadStatus = 0, // 0=未读
                SendTime = sendTime,
                MessageExtData = messageExtData
            };

            // 发送给指定用户（通过用户组）
            await Clients.Group($"User_{toUserName}").SendAsync("ReceiveMessage", message);

            // 如果发送者也在线，也通知发送者消息已发送
            if (!string.IsNullOrEmpty(fromUserName))
            {
                await Clients.Caller.SendAsync("MessageSent", new
                {
                    ToUserName = toUserName,
                    SendTime = sendTime
                });
            }

            TaktLogger.Information("用户 {FromUserName} 向 {ToUserName} 发送了消息", fromUserName, toUserName);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "发送消息时发生错误");
            await Clients.Caller.SendAsync("Error", new { Message = "发送消息失败" });
        }
    }

    /// <summary>
    /// 发送广播消息给所有在线用户
    /// </summary>
    /// <param name="messageTitle">消息标题</param>
    /// <param name="messageContent">消息内容</param>
    /// <param name="messageType">消息类型</param>
    /// <param name="messageGroup">消息分组</param>
    public async Task BroadcastMessage(
        string messageContent,
        string? messageTitle = null,
        string messageType = "Takt365",
        string messageGroup = "Notification")
    {
        try
        {
            var fromUserName = _userContext?.GetCurrentUserName() ?? "Takt365";
            var sendTime = DateTime.Now;

            var message = new
            {
                FromUserName = fromUserName,
                MessageTitle = messageTitle,
                MessageContent = messageContent,
                MessageType = messageType,
                MessageGroup = messageGroup,
                SendTime = sendTime
            };

            // 发送给所有在通知组的用户
            await Clients.Group("Notifications").SendAsync("ReceiveBroadcast", message);

            TaktLogger.Information("用户 {FromUserName} 发送了广播消息", fromUserName);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "发送广播消息时发生错误");
            await Clients.Caller.SendAsync("Error", new { Message = "发送广播消息失败" });
        }
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="messageId">消息ID</param>
    public async Task MarkAsRead(long messageId)
    {
        try
        {
            var userName = _userContext?.GetCurrentUserName();
            if (string.IsNullOrEmpty(userName))
            {
                await Clients.Caller.SendAsync("Error", new { Message = "未登录" });
                return;
            }

            // 这里需要消息服务来更新消息状态
            // await _messageService.MarkAsReadAsync(messageId, userName);

            await Clients.Caller.SendAsync("MessageRead", new
            {
                MessageId = messageId,
                ReadTime = DateTime.Now
            });

            TaktLogger.Information("用户 {UserName} 标记消息 {MessageId} 为已读", userName, messageId);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "标记消息为已读时发生错误");
            await Clients.Caller.SendAsync("Error", new { Message = "标记消息为已读失败" });
        }
    }

    /// <summary>
    /// 获取未读消息数量
    /// </summary>
    public Task<int> GetUnreadCount()
    {
        try
        {
            var userName = _userContext?.GetCurrentUserName();
            if (string.IsNullOrEmpty(userName))
            {
                return Task.FromResult(0);
            }

            // 这里需要消息服务来查询未读消息数量
            // var count = await _messageService.GetUnreadCountAsync(userName);
            // return count;

            return Task.FromResult(0);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "获取未读消息数量时发生错误");
            return Task.FromResult(0);
        }
    }
}
