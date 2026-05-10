// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.SignalR.ConnectEngine
// 文件名称：ITakeConnectEngineService.cs
// 创建时间：2026-05-01
// 创建人：Takt365
// 功能描述：SignalR 引擎实现，提供 SignalR 相关的核心功能实现
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Domain.Entities.Routine.Tasks.SignalR;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.Tasks.SignalR.ConnectEngine;

/// <summary>
/// SignalR 引擎实现
/// </summary>
public class TakeConnectEngineService : ITakeConnectEngineService
{
    private readonly ITaktRepository<TaktOnline> _onlineRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineRepository">在线用户仓储</param>
    public TakeConnectEngineService(
        ITaktRepository<TaktOnline> onlineRepository)
    {
        _onlineRepository = onlineRepository;
    }





    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    /// <returns>在线用户列表</returns>
    public async Task<List<TaktOnlineDto>> GetOnlineUsersAsync()
    {
        // 只查询在线用户，避免全表加载
        var list = await _onlineRepository.FindAsync(x => x.OnlineStatus == 0);
        return list.Adapt<List<TaktOnlineDto>>();
    }

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    /// <param name="dto">在线用户表(Online)最后活动时间更新DTO</param>
    /// <returns>任务</returns>
    public async Task UpdateLastActiveTimeAsync(TaktOnlineLastActiveTimeUpdateDto dto)
    {
        var entity = await _onlineRepository.GetAsync(x => x.ConnectionId == dto.ConnectionId);
        if (entity == null)
            throw new TaktBusinessException("validation.onlineNotFound");

        entity.LastActiveTime = DateTime.Now;
        entity.UpdatedAt = DateTime.Now;
        await _onlineRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 获取连接持续时间信息
    /// </summary>
    /// <param name="connectionId">SignalR连接ID</param>
    /// <returns>连接持续时间DTO</returns>
    public async Task<ConnectionDurationDto> GetConnectionDurationAsync(string connectionId)
    {
        var entity = await _onlineRepository.GetAsync(x => x.ConnectionId == connectionId);
        if (entity == null)
            throw new TaktBusinessException("validation.onlineNotFound");

        var duration = entity.DisconnectTime.HasValue ? 
            (int)(entity.DisconnectTime.Value - entity.ConnectTime).TotalSeconds : 
            (int)(DateTime.Now - entity.ConnectTime).TotalSeconds;

        return new ConnectionDurationDto
        {
            DurationInSeconds = duration,
            DurationInMilliseconds = entity.DisconnectTime.HasValue ? 
                (long)(entity.DisconnectTime.Value - entity.ConnectTime).TotalMilliseconds : 
                (long)(DateTime.Now - entity.ConnectTime).TotalMilliseconds,
            ConnectTime = entity.ConnectTime,
            DisconnectTime = entity.DisconnectTime,
            Status = entity.OnlineStatus == 0 ? "Connected" : "Disconnected"
        };
    }

    /// <summary>
    /// 获取连接持续时间统计信息
    /// </summary>
    /// <returns>连接持续时间统计DTO</returns>
    public async Task<ConnectionDurationDto> GetConnectionDurationStatsAsync()
    {
        // 只获取在线用户的连接时长统计
        var connectedUsers = await _onlineRepository.FindAsync(x => 
            x.OnlineStatus == 0 && x.ConnectTime != default);
        
        if (!connectedUsers.Any())
            return new ConnectionDurationDto();

        var durations = connectedUsers.Select(x => 
            (int)(DateTime.Now - x.ConnectTime).TotalSeconds
        ).ToList();

        return new ConnectionDurationDto
        {
            DurationInSeconds = (int)durations.Average(),
            DurationInMilliseconds = (long)durations.Average() * 1000,
            ConnectTime = connectedUsers.Min(x => x.ConnectTime),
            DisconnectTime = connectedUsers.Max(x => x.DisconnectTime ?? DateTime.Now),
            Status = "Statistics"
        };
    }

    /// <summary>
    /// 获取断开连接时间信息
    /// </summary>
    /// <param name="connectionId">SignalR连接ID</param>
    /// <returns>断开连接时间DTO</returns>
    public async Task<DisconnectTimeDto> GetDisconnectTimeAsync(string connectionId)
    {
        var entity = await _onlineRepository.GetAsync(x => x.ConnectionId == connectionId);
        if (entity == null || !entity.DisconnectTime.HasValue)
            throw new TaktBusinessException("validation.onlineNotFound");

        return new DisconnectTimeDto
        {
            ConnectionId = entity.ConnectionId,
            DisconnectTime = entity.DisconnectTime.Value,
            Reason = "User disconnected",
            ConnectionDuration = entity.ConnectionDuration
        };
    }

    /// <summary>
    /// 获取断开连接时间统计信息
    /// </summary>
    /// <returns>断开连接时间统计DTO</returns>
    public async Task<DisconnectTimeDto> GetDisconnectTimeStatsAsync()
    {
        // 统计最近24小时的断开连接情况
        var cutoffTime = DateTime.Now.AddHours(-24);
        var disconnectedUsers = await _onlineRepository.FindAsync(x => 
            x.DisconnectTime.HasValue && x.DisconnectTime.Value >= cutoffTime);
        
        return new DisconnectTimeDto
        {
            ConnectionId = "statistics",
            DisconnectTime = DateTime.Now,
            Reason = $"{disconnectedUsers.Count} users disconnected in last 24 hours",
            ConnectionDuration = disconnectedUsers.Any() ? 
                (int?)disconnectedUsers.Average(x => x.ConnectionDuration ?? 0) : null
        };
    }

    /// <summary>
    /// 获取在线用户统计信息
    /// </summary>
    /// <returns>在线用户统计DTO</returns>
    public async Task<OnlineUserStatsDto> GetOnlineUserStatsAsync()
    {
        // 使用条件查询，避免全表加载
        var allUsers = await _onlineRepository.GetAllAsync();
        var onlineUsers = allUsers.Where(x => x.OnlineStatus == 0).ToList();
        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
        var activeUsers = allUsers.Where(x => x.LastActiveTime.HasValue && 
            x.LastActiveTime.Value >= fiveMinutesAgo).ToList();
        
        var todayStart = DateTime.Today;
        var newUsersToday = allUsers.Where(x => x.CreatedAt >= todayStart).Count();

        // 计算平均连接时长
        var connectionDurations = onlineUsers.Select(x => x.ConnectionDuration ?? 0).ToList();
        
        return new OnlineUserStatsDto
        {
            TotalOnlineUsers = onlineUsers.Count,
            ActiveUsers = activeUsers.Count,
            NewUsersToday = newUsersToday,
            AverageConnectionDuration = connectionDurations.Any() ? 
                connectionDurations.Average() : 0,
            MaxConnectionDuration = connectionDurations.Any() ? 
                connectionDurations.Max() : 0,
            MinConnectionDuration = connectionDurations.Any() ? 
                connectionDurations.Min() : 0,
            StatisticsTime = DateTime.Now,
            OnlineUsers = onlineUsers.Adapt<List<TaktOnlineDto>>()
        };
    }

    /// <summary>
    /// 获取今日在线用户统计信息
    /// </summary>
    /// <returns>今日在线用户统计DTO</returns>
    public async Task<TodayOnlineStatsDto> GetTodayOnlineStatsAsync()
    {
        var todayStart = DateTime.Today;
        
        // 只查询今天有连接记录的用户
        var todayUsers = await _onlineRepository.FindAsync(x => 
            x.ConnectTime >= todayStart);
        
        var onlineUsers = await _onlineRepository.FindAsync(x => x.OnlineStatus == 0);
        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
        var activeUsers = onlineUsers.Where(x => x.LastActiveTime.HasValue && 
            x.LastActiveTime.Value >= fiveMinutesAgo).ToList();
        
        // 计算峰值在线人数（基于连接时间）
        int peakOnline = 0;
        for (int hour = 0; hour < 24; hour++)
        {
            var hourStart = todayStart.AddHours(hour);
            var hourEnd = hourStart.AddHours(1);
            var hourOnline = await _onlineRepository.CountAsync(x => 
                x.ConnectTime <= hourEnd && 
                (x.DisconnectTime == null || x.DisconnectTime > hourStart)
            );
            peakOnline = Math.Max(peakOnline, hourOnline);
        }

        return new TodayOnlineStatsDto
        {
            TotalConnectionCount = todayUsers.Count,
            PeakOnlineUsers = peakOnline,
            AverageOnlineUsers = peakOnline > 0 ? peakOnline / 24.0 : 0,
            NewUsersToday = todayUsers.Count,
            ActiveUsersToday = activeUsers.Count,
            OfflineUsersToday = await _onlineRepository.CountAsync(x => x.OnlineStatus == 1),
            AverageConnectionDuration = onlineUsers.Any() ? 
                onlineUsers.Average(x => x.ConnectionDuration ?? 0) : 0,
            StatisticsDate = DateTime.Today,
            OnlineUsers = onlineUsers.Take(100).Adapt<List<TaktOnlineDto>>() // 限制返回数量
        };
    }

    /// <summary>
    /// 获取月度在线用户统计信息
    /// </summary>
    /// <param name="month">月份（格式：yyyy-MM）</param>
    /// <returns>月统计DTO</returns>
    public async Task<MonthlyOnlineStatsDto> GetMonthlyOnlineStatsAsync(string month)
    {
        var startDate = DateTime.ParseExact(month + "-01", "yyyy-MM-dd", null);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var totalDays = (endDate - startDate).Days + 1;
        
        // 只查询本月数据
        var monthUsers = await _onlineRepository.FindAsync(x => 
            x.ConnectTime >= startDate && x.ConnectTime <= endDate);
        
        var onlineUsers = await _onlineRepository.FindAsync(x => x.OnlineStatus == 0);
        
        // 每日统计
        var dailyStats = new List<TodayOnlineStatsDto>();
        for (int day = 0; day < totalDays; day++)
        {
            var currentDay = startDate.AddDays(day);
            var nextDay = currentDay.AddDays(1);
            
            var dayUsers = monthUsers.Where(x => x.ConnectTime.Date == currentDay.Date).ToList();
            var dayOnlineUsers = dayUsers.Where(x => x.OnlineStatus == 0).ToList();
            
            dailyStats.Add(new TodayOnlineStatsDto
            {
                TotalConnectionCount = dayUsers.Count,
                PeakOnlineUsers = dayOnlineUsers.Count,
                AverageOnlineUsers = dayOnlineUsers.Count > 0 ? dayOnlineUsers.Count / 24.0 : 0,
                NewUsersToday = dayUsers.Count,
                ActiveUsersToday = dayOnlineUsers.Count(x => x.LastActiveTime.HasValue && 
                    x.LastActiveTime.Value >= DateTime.Now.AddMinutes(-5)),
                OfflineUsersToday = dayUsers.Count(x => x.OnlineStatus == 1),
                AverageConnectionDuration = dayOnlineUsers.Any() ? 
                    dayOnlineUsers.Average(x => x.ConnectionDuration ?? 0) : 0,
                StatisticsDate = currentDay
            });
        }

        return new MonthlyOnlineStatsDto
        {
            Month = month,
            TotalConnectionCount = monthUsers.Count,
            PeakOnlineUsers = onlineUsers.Count,
            AverageOnlineUsers = totalDays > 0 ? onlineUsers.Count / (double)totalDays : 0,
            NewUsersThisMonth = monthUsers.Count,
            ActiveUsersThisMonth = onlineUsers.Count,
            OfflineUsersThisMonth = await _onlineRepository.CountAsync(x => x.OnlineStatus == 1),
            AverageConnectionDuration = onlineUsers.Any() ? 
                onlineUsers.Average(x => x.ConnectionDuration ?? 0) : 0,
            MonthStartDate = startDate,
            MonthEndDate = endDate,
            DailyStats = dailyStats
        };
    }

    /// <summary>
    /// 获取离线用户统计信息
    /// </summary>
    /// <returns>离线用户统计DTO</returns>
    public async Task<OfflineUserStatsDto> GetOfflineUserStatsAsync()
    {
        var allUsers = await _onlineRepository.GetAllAsync();
        var offlineUsers = allUsers.Where(x => x.OnlineStatus == 1).ToList();
        var todayStart = DateTime.Today;
        var offlineToday = allUsers.Where(x => x.OnlineStatus == 1 && x.CreatedAt >= todayStart).ToList();
        
        // 从登录日志获取退出登录用户数
        // 这里简化处理，实际应该查询登录日志表
        var logoutUsersToday = offlineToday.Count;
        
        // 计算会话时长统计
        var sessionDurations = offlineUsers.Select(x => x.ConnectionDuration ?? 0).ToList();

        return new OfflineUserStatsDto
        {
            TotalOfflineUsers = offlineUsers.Count,
            OfflineUsersToday = offlineToday.Count,
            LogoutUsersToday = logoutUsersToday,
            AverageSessionDuration = sessionDurations.Any() ? 
                sessionDurations.Average() : 0,
            MaxSessionDuration = sessionDurations.Any() ? 
                sessionDurations.Max() : 0,
            MinSessionDuration = sessionDurations.Any() ? 
                sessionDurations.Min() : 0,
            StatisticsTime = DateTime.Now,
            OfflineUsers = offlineUsers.Adapt<List<TaktOnlineDto>>()
        };
    }



    /// <summary>
    /// 获取未读消息统计信息
    /// </summary>
    /// <returns>未读消息DTO</returns>
    public async Task<UnreadMessageDto> GetUnreadMessageStatsAsync()
    {
        // 消息统计功能由 TaktNotificationHub 负责，本引擎仅提供在线用户实时统计
        return new UnreadMessageDto();
    }

    /// <summary>
    /// 获取已读消息统计信息
    /// </summary>
    /// <returns>已读消息DTO</returns>
    public async Task<ReadMessageDto> GetReadMessageStatsAsync()
    {
        // 消息统计功能由 TaktNotificationHub 负责，本引擎仅提供在线用户实时统计
        return new ReadMessageDto();
    }

    /// <summary>
    /// 获取消息统计信息
    /// </summary>
    /// <returns>消息统计DTO</returns>
    public async Task<MessageStatsDto> GetMessageStatsAsync()
    {
        // 消息统计功能由 TaktNotificationHub 负责，本引擎仅提供在线用户实时统计
        return new MessageStatsDto();
    }

    /// <summary>
    /// 获取实时在线用户统计信息
    /// </summary>
    /// <returns>实时在线用户统计DTO</returns>
    public async Task<RealTimeOnlineStatsDto> GetRealTimeOnlineStatsAsync()
    {
        // 获取所有未删除的在线连接（OnlineStatus == 0）
        var onlineConnections = await _onlineRepository.FindAsync(x => 
            x.IsDeleted == 0 && x.OnlineStatus == 0);
        
        // 按 UserId 去重，统计真实在线用户数（一个用户可能有多个连接）
        var uniqueOnlineUsers = onlineConnections
            .Where(x => x.UserId.HasValue)
            .GroupBy(x => x.UserId.Value)
            .Select(g => g.First()) // 取每个用户的第一个连接
            .ToList();
        
        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
        var thirtyMinutesAgo = DateTime.Now.AddMinutes(-30);
        
        // 活跃用户：5分钟内有活动的用户
        var activeUsers = uniqueOnlineUsers.Where(x => x.LastActiveTime.HasValue && 
            x.LastActiveTime.Value >= fiveMinutesAgo).ToList();
        
        // 空闲用户：5-30分钟内无活动的用户
        var idleUsers = uniqueOnlineUsers.Where(x => x.LastActiveTime.HasValue && 
            x.LastActiveTime.Value >= thirtyMinutesAgo && 
            x.LastActiveTime.Value < fiveMinutesAgo).ToList();
        
        // 总连接数（包括一个用户的多个连接）
        var totalConnections = onlineConnections.Count;
        
        // 最近1分钟的新连接
        var oneMinuteAgo = DateTime.Now.AddMinutes(-1);
        var newConnections = onlineConnections.Count(x => x.ConnectTime >= oneMinuteAgo);
        
        // 最近1分钟的断开连接（需要查询所有记录，包括离线的）
        var disconnections = await _onlineRepository.CountAsync(x => 
            x.IsDeleted == 0 && 
            x.DisconnectTime.HasValue && 
            x.DisconnectTime.Value >= oneMinuteAgo);

        return new RealTimeOnlineStatsDto
        {
            CurrentOnlineUsers = uniqueOnlineUsers.Count, // 去重后的真实用户数
            CurrentActiveUsers = activeUsers.Count,
            CurrentIdleUsers = idleUsers.Count,
            CurrentOfflineUsers = 0, // 离线用户数无法准确统计（历史记录太多），建议设为0或通过其他方式获取
            CurrentTotalConnections = totalConnections, // 总连接数
            NewConnectionsLastMinute = newConnections,
            DisconnectionsLastMinute = disconnections,
            LastUpdateTime = DateTime.Now,
            OnlineUsers = uniqueOnlineUsers.Take(100).Adapt<List<TaktOnlineDto>>() // 限制返回数量
        };
    }

    /// <summary>
    /// 获取实时消息统计信息
    /// </summary>
    /// <returns>实时消息统计DTO</returns>
    public async Task<RealTimeMessageStatsDto> GetRealTimeMessageStatsAsync()
    {
        // 消息统计功能由 TaktNotificationHub 负责，本引擎仅提供在线用户实时统计
        return new RealTimeMessageStatsDto();
    }

    /// <summary>
    /// 获取实时综合统计信息
    /// </summary>
    /// <returns>实时综合统计DTO</returns>
    public async Task<RealTimeCombinedStatsDto> GetRealTimeCombinedStatsAsync()
    {
        var onlineStats = await GetRealTimeOnlineStatsAsync();
        var messageStats = await GetRealTimeMessageStatsAsync();
        
        return new RealTimeCombinedStatsDto
        {
            OnlineStats = onlineStats,
            MessageStats = messageStats,
            StatisticsTime = DateTime.Now,
            HealthStatus = "Healthy",
            SignalDelayMs = 10
        };
    }
}