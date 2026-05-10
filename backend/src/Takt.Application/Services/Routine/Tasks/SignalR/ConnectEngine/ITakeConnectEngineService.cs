// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.SignalR.ConnectEngine
// 文件名称：ITakeConnectEngineService.cs
// 创建时间：2026-05-01
// 创建人：Takt365
// 功能描述：SignalR 引擎接口，提供 SignalR 相关的核心功能
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;

namespace Takt.Application.Services.Routine.Tasks.SignalR.ConnectEngine;

/// <summary>
/// SignalR 引擎接口
/// </summary>
public interface ITakeConnectEngineService
{
    /// <summary>
    /// 获取实时在线用户统计信息
    /// </summary>
    /// <returns>实时在线用户统计DTO</returns>
    Task<RealTimeOnlineStatsDto> GetRealTimeOnlineStatsAsync();

    /// <summary>
    /// 获取今日在线用户统计信息
    /// </summary>
    /// <returns>今日在线用户统计DTO</returns>
    Task<TodayOnlineStatsDto> GetTodayOnlineStatsAsync();

    /// <summary>
    /// 获取月度在线用户统计信息
    /// </summary>
    /// <param name="month">月份（格式：yyyy-MM）</param>
    /// <returns>月统计DTO</returns>
    Task<MonthlyOnlineStatsDto> GetMonthlyOnlineStatsAsync(string month);

    /// <summary>
    /// 获取实时消息统计信息
    /// </summary>
    /// <returns>实时消息统计DTO</returns>
    Task<RealTimeMessageStatsDto> GetRealTimeMessageStatsAsync();

    /// <summary>
    /// 获取未读消息统计信息
    /// </summary>
    /// <returns>未读消息DTO</returns>
    Task<UnreadMessageDto> GetUnreadMessageStatsAsync();

    /// <summary>
    /// 获取已读消息统计信息
    /// </summary>
    /// <returns>已读消息DTO</returns>
    Task<ReadMessageDto> GetReadMessageStatsAsync();

    /// <summary>
    /// 获取消息统计信息
    /// </summary>
    /// <returns>消息统计DTO</returns>
    Task<MessageStatsDto> GetMessageStatsAsync();

    /// <summary>
    /// 获取连接持续时间信息
    /// </summary>
    /// <param name="connectionId">SignalR连接ID</param>
    /// <returns>连接持续时间DTO</returns>
    Task<ConnectionDurationDto> GetConnectionDurationAsync(string connectionId);

    /// <summary>
    /// 获取连接持续时间统计信息
    /// </summary>
    /// <returns>连接持续时间统计DTO</returns>
    Task<ConnectionDurationDto> GetConnectionDurationStatsAsync();

    /// <summary>
    /// 获取断开连接时间信息
    /// </summary>
    /// <param name="connectionId">SignalR连接ID</param>
    /// <returns>断开连接时间DTO</returns>
    Task<DisconnectTimeDto> GetDisconnectTimeAsync(string connectionId);

    /// <summary>
    /// 获取断开连接时间统计信息
    /// </summary>
    /// <returns>断开连接时间统计DTO</returns>
    Task<DisconnectTimeDto> GetDisconnectTimeStatsAsync();

    /// <summary>
    /// 获取在线用户统计信息
    /// </summary>
    /// <returns>在线用户统计DTO</returns>
    Task<OnlineUserStatsDto> GetOnlineUserStatsAsync();

    /// <summary>
    /// 获取离线用户统计信息
    /// </summary>
    /// <returns>离线用户统计DTO</returns>
    Task<OfflineUserStatsDto> GetOfflineUserStatsAsync();

    /// <summary>
    /// 获取实时综合统计信息
    /// </summary>
    /// <returns>实时综合统计DTO</returns>
    Task<RealTimeCombinedStatsDto> GetRealTimeCombinedStatsAsync();

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    /// <param name="dto">在线用户表(Online)最后活动时间更新DTO</param>
    /// <returns>任务</returns>
    Task UpdateLastActiveTimeAsync(TaktOnlineLastActiveTimeUpdateDto dto);
}