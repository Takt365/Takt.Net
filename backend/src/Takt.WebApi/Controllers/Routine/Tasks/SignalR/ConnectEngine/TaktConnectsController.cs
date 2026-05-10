// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.SignalR.SignalREngine
// 文件名称：TaktSingnalREngineController.cs
// 创建时间：2026-05-02
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR引擎控制器，提供SignalR引擎功能的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Application.Services.Routine.Tasks.SignalR.ConnectEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;

namespace Takt.WebApi.Controllers.Routine.Tasks.SignalR.ConnectEngine;

/// <summary>
/// SignalR引擎控制器
/// </summary>
[Route("api/[controller]", Name = "SignalR引擎")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:signalr:engine", "SignalR引擎管理")]
public class TaktConnectsController : TaktControllerBase
{
    private readonly ITakeConnectEngineService _engine;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktConnectsController(
        ITakeConnectEngineService engine,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _engine = engine;
    }


    /// <summary>
    /// 获取实时在线用户统计信息
    /// </summary>
    [HttpGet("realtime-online-stats")]
    [TaktPermission("routine:tasks:signalr:engine:realtime-online-stats", "获取实时在线用户统计信息")]
    public async Task<ActionResult<RealTimeOnlineStatsDto>> GetRealTimeOnlineStatsAsync()
    {
        var result = await _engine.GetRealTimeOnlineStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取今日在线用户统计信息
    /// </summary>
    [HttpGet("today-online-stats")]
    [TaktPermission("routine:tasks:signalr:engine:today-online-stats", "获取今日在线用户统计信息")]
    public async Task<ActionResult<TodayOnlineStatsDto>> GetTodayOnlineStatsAsync()
    {
        var result = await _engine.GetTodayOnlineStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取月度在线用户统计信息
    /// </summary>
    [HttpGet("monthly-online-stats/{month}")]
    [TaktPermission("routine:tasks:signalr:engine:monthly-online-stats", "获取月度在线用户统计信息")]
    public async Task<ActionResult<MonthlyOnlineStatsDto>> GetMonthlyOnlineStatsAsync([FromRoute] string month)
    {
        var result = await _engine.GetMonthlyOnlineStatsAsync(month);
        return Ok(result);
    }


    /// <summary>
    /// 获取实时消息统计信息
    /// </summary>
    [HttpGet("realtime-message-stats")]
    [TaktPermission("routine:tasks:signalr:engine:realtime-message-stats", "获取实时消息统计信息")]
    public async Task<ActionResult<RealTimeMessageStatsDto>> GetRealTimeMessageStatsAsync()
    {
        var result = await _engine.GetRealTimeMessageStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取未读消息统计信息
    /// </summary>
    [HttpGet("unread-message-stats")]
    [TaktPermission("routine:tasks:signalr:engine:unread-message-stats", "获取未读消息统计信息")]
    public async Task<ActionResult<UnreadMessageDto>> GetUnreadMessageStatsAsync()
    {
        var result = await _engine.GetUnreadMessageStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取已读消息统计信息
    /// </summary>
    [HttpGet("read-message-stats")]
    [TaktPermission("routine:tasks:signalr:engine:read-message-stats", "获取已读消息统计信息")]
    public async Task<ActionResult<ReadMessageDto>> GetReadMessageStatsAsync()
    {
        var result = await _engine.GetReadMessageStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取消息统计信息
    /// </summary>
    [HttpGet("message-stats")]
    [TaktPermission("routine:tasks:signalr:engine:message-stats", "获取消息统计信息")]
    public async Task<ActionResult<MessageStatsDto>> GetMessageStatsAsync()
    {
        var result = await _engine.GetMessageStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取连接持续时间信息
    /// </summary>
    [HttpGet("connection-duration/{connectionId}")]
    [TaktPermission("routine:tasks:signalr:engine:connection-duration", "获取连接持续时间信息")]
    public async Task<ActionResult<ConnectionDurationDto>> GetConnectionDurationAsync([FromRoute] string connectionId)
    {
        var result = await _engine.GetConnectionDurationAsync(connectionId);
        return Ok(result);
    }


    /// <summary>
    /// 获取连接持续时间统计信息
    /// </summary>
    [HttpGet("connection-duration-stats")]
    [TaktPermission("routine:tasks:signalr:engine:connection-duration-stats", "获取连接持续时间统计信息")]
    public async Task<ActionResult<ConnectionDurationDto>> GetConnectionDurationStatsAsync()
    {
        var result = await _engine.GetConnectionDurationStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取断开连接时间信息
    /// </summary>
    [HttpGet("disconnect-time/{connectionId}")]
    [TaktPermission("routine:tasks:signalr:engine:disconnect-time", "获取断开连接时间信息")]
    public async Task<ActionResult<DisconnectTimeDto>> GetDisconnectTimeAsync([FromRoute] string connectionId)
    {
        var result = await _engine.GetDisconnectTimeAsync(connectionId);
        return Ok(result);
    }


    /// <summary>
    /// 获取断开连接时间统计信息
    /// </summary>
    [HttpGet("disconnect-time-stats")]
    [TaktPermission("routine:tasks:signalr:engine:disconnect-time-stats", "获取断开连接时间统计信息")]
    public async Task<ActionResult<DisconnectTimeDto>> GetDisconnectTimeStatsAsync()
    {
        var result = await _engine.GetDisconnectTimeStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取在线用户统计信息
    /// </summary>
    [HttpGet("online-user-stats")]
    [TaktPermission("routine:tasks:signalr:engine:online-user-stats", "获取在线用户统计信息")]
    public async Task<ActionResult<OnlineUserStatsDto>> GetOnlineUserStatsAsync()
    {
        var result = await _engine.GetOnlineUserStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取离线用户统计信息
    /// </summary>
    [HttpGet("offline-user-stats")]
    [TaktPermission("routine:tasks:signalr:engine:offline-user-stats", "获取离线用户统计信息")]
    public async Task<ActionResult<OfflineUserStatsDto>> GetOfflineUserStatsAsync()
    {
        var result = await _engine.GetOfflineUserStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取实时综合统计信息
    /// </summary>
    [HttpGet("realtime-combined-stats")]
    [TaktPermission("routine:tasks:signalr:engine:realtime-combined-stats", "获取实时综合统计信息")]
    public async Task<ActionResult<RealTimeCombinedStatsDto>> GetRealTimeCombinedStatsAsync()
    {
        var result = await _engine.GetRealTimeCombinedStatsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    [HttpPost("update-last-active-time")]
    [TaktPermission("routine:tasks:signalr:engine:update-last-active-time", "更新用户最后活动时间")]
    public async Task<ActionResult> UpdateLastActiveTimeAsync([FromBody] TaktOnlineLastActiveTimeUpdateDto dto)
    {
        await _engine.UpdateLastActiveTimeAsync(dto);
        return Ok();
    }
}