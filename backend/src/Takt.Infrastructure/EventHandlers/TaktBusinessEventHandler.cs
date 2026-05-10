// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间:Takt.Infrastructure.EventHandlers
// 文件名称:TaktBusinessEventHandler.cs
// 创建时间:2026-05-10
// 创建人:Takt365
// 功能描述:通用业务事件处理器 - 处理业务流程触发的事件
// 
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using MediatR;
using Takt.Domain.Events;
using Takt.Shared.Helpers;

namespace Takt.Infrastructure.EventHandlers;

/// <summary>
/// 通用业务事件处理器
/// </summary>
public class TaktBusinessEventHandler : INotificationHandler<TaktBusinessEvent>
{
    /// <summary>
    /// 处理业务事件
    /// </summary>
    public Task Handle(TaktBusinessEvent notification, CancellationToken cancellationToken)
    {
        // 根据模块和动作路由到不同的处理逻辑
        var eventType = $"{notification.Module}:{notification.Action}";
        
        TaktLogger.Information(
            "[Business Event] {EventType} - 实体类型:{EntityType}, 实体ID:{EntityId}, 操作人:{OperatorId}",
            eventType,
            notification.EntityType,
            notification.EntityId,
            notification.OperatorId);

        // 根据事件类型分发处理
        switch (notification.Module)
        {
            case "SignalR":
                HandleSignalREvent(notification);
                break;
                
            case "Notification":
                HandleNotificationEvent(notification);
                break;
                
            case "User":
                HandleUserEvent(notification);
                break;
                
            case "Auth":
                HandleAuthEvent(notification);
                break;
                
            default:
                TaktLogger.Debug("[Business Event] 未定义处理逻辑的模块:{Module}, 动作:{Action}",
                    notification.Module, notification.Action);
                break;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// 处理 SignalR 相关事件
    /// </summary>
    private void HandleSignalREvent(TaktBusinessEvent notification)
    {
        switch (notification.Action)
        {
            case "UserConnected":
                TaktLogger.Information("[SignalR] 用户已连接 - UserName:{UserName}, ConnectionId:{ConnectionId}",
                    notification.Data?.GetType().GetProperty("UserName")?.GetValue(notification.Data),
                    notification.Data?.GetType().GetProperty("ConnectionId")?.GetValue(notification.Data));
                // TODO: 更新在线用户列表、推送上线通知等
                break;
                
            case "UserDisconnected":
                TaktLogger.Information("[SignalR] 用户已断开 - UserName:{UserName}",
                    notification.Data?.GetType().GetProperty("UserName")?.GetValue(notification.Data));
                // TODO: 更新在线状态、清理连接资源等
                break;
                
            default:
                TaktLogger.Debug("[SignalR] 未知动作:{Action}", notification.Action);
                break;
        }
    }

    /// <summary>
    /// 处理通知相关事件
    /// </summary>
    private void HandleNotificationEvent(TaktBusinessEvent notification)
    {
        switch (notification.Action)
        {
            case "MessageSent":
                TaktLogger.Information("[Notification] 消息已发送 - From:{FromUserName}, To:{ToUserName}",
                    notification.Data?.GetType().GetProperty("FromUserName")?.GetValue(notification.Data),
                    notification.Data?.GetType().GetProperty("ToUserName")?.GetValue(notification.Data));
                // TODO: 保存消息记录、推送通知等
                break;
                
            case "MessageRead":
                TaktLogger.Information("[Notification] 消息已读 - MessageId:{MessageId}",
                    notification.Data?.GetType().GetProperty("MessageId")?.GetValue(notification.Data));
                // TODO: 更新消息状态、通知发送者等
                break;
                
            default:
                TaktLogger.Debug("[Notification] 未知动作:{Action}", notification.Action);
                break;
        }
    }

    /// <summary>
    /// 处理用户相关事件
    /// </summary>
    private void HandleUserEvent(TaktBusinessEvent notification)
    {
        switch (notification.Action)
        {
            case "ProfileUpdated":
                TaktLogger.Information("[User] 用户资料已更新 - UserId:{UserId}",
                    notification.EntityId);
                // TODO: 清除用户缓存、同步资料等
                break;
                
            case "PasswordChanged":
                TaktLogger.Information("[User] 用户密码已更改 - UserId:{UserId}",
                    notification.EntityId);
                // TODO: 强制重新登录、发送安全通知等
                break;
                
            default:
                TaktLogger.Debug("[User] 未知动作:{Action}", notification.Action);
                break;
        }
    }

    /// <summary>
    /// 处理认证相关事件
    /// </summary>
    private void HandleAuthEvent(TaktBusinessEvent notification)
    {
        switch (notification.Action)
        {
            case "LoginSuccess":
                TaktLogger.Information("[Auth] 用户登录成功 - UserId:{UserId}",
                    notification.EntityId);
                // TODO: 记录登录日志、初始化用户会话等
                break;
                
            case "Logout":
                TaktLogger.Information("[Auth] 用户登出 - UserId:{UserId}",
                    notification.EntityId);
                // TODO: 清理会话、记录登出日志等
                break;
                
            case "TokenRefreshed":
                TaktLogger.Debug("[Auth] Token 已刷新 - UserId:{UserId}",
                    notification.EntityId);
                // TODO: 更新 Token 缓存等
                break;
                
            default:
                TaktLogger.Debug("[Auth] 未知动作:{Action}", notification.Action);
                break;
        }
    }
}
