// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Tasks.SignalR
// 文件名称：TaktSignalRDtos.cs
// 创建时间：2026-05-01
// 创建人：Takt365
// 功能描述：SignalR 相关的特定 DTO 类定义
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.SignalR;

/// <summary>
    /// Takt消息已读DTO
    /// </summary>
public partial class TaktMessageReadDto
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MessageId { get; set; }
}

/// <summary>
    /// Takt在线用户导出DTO扩展（用于包含业务字段）
    /// </summary>
public partial class TaktOnlineExportDto
{
    /// <summary>
    /// 在线状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? OnlineStatusString { get; set; }
}

/// <summary>
    /// Takt消息导出DTO扩展（用于包含业务字段）
    /// </summary>
public partial class TaktMessageExportDto
{
    /// <summary>
    /// 阅读状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? ReadStatusString { get; set; }
}

/// <summary>
    /// 在线用户表最后活动时间更新DTO
    /// </summary>
public class TaktOnlineLastActiveTimeUpdateDto
{
    /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;
}

/// <summary>
    /// 实时在线用户统计DTO
    /// </summary>
public partial class RealTimeOnlineStatsDto
{
    /// <summary>
    /// 当前在线用户总数
    /// </summary>
    public int CurrentOnlineUsers { get; set; }

    /// <summary>
    /// 当前活跃用户数（最后活动时间在5分钟内）
    /// </summary>
    public int CurrentActiveUsers { get; set; }

    /// <summary>
    /// 当前空闲用户数（最后活动时间在5-30分钟内）
    /// </summary>
    public int CurrentIdleUsers { get; set; }

    /// <summary>
    /// 当前离线用户数
    /// </summary>
    public int CurrentOfflineUsers { get; set; }

    /// <summary>
    /// 当前连接总数
    /// </summary>
    public int CurrentTotalConnections { get; set; }

    /// <summary>
    /// 最近1分钟新增连接数
    /// </summary>
    public int NewConnectionsLastMinute { get; set; }

    /// <summary>
    /// 最近1分钟断开连接数
    /// </summary>
    public int DisconnectionsLastMinute { get; set; }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// 在线用户列表（可选）
    /// </summary>
    public List<TaktOnlineDto>? OnlineUsers { get; set; }
}

/// <summary>
    /// 今日用户在线统计DTO
    /// </summary>
public partial class TodayOnlineStatsDto
{
    /// <summary>
    /// 今日总连接数
    /// </summary>
    public int TotalConnectionCount { get; set; }

    /// <summary>
    /// 今日最高在线人数
    /// </summary>
    public int PeakOnlineUsers { get; set; }

    /// <summary>
    /// 今日平均在线人数
    /// </summary>
    public double AverageOnlineUsers { get; set; }

    /// <summary>
    /// 今日新增用户数
    /// </summary>
    public int NewUsersToday { get; set; }

    /// <summary>
    /// 今日活跃用户数（最后活动时间在24小时内）
    /// </summary>
    public int ActiveUsersToday { get; set; }

    /// <summary>
    /// 今日离线用户数
    /// </summary>
    public int OfflineUsersToday { get; set; }

    /// <summary>
    /// 今日平均连接时长（秒）
    /// </summary>
    public double AverageConnectionDuration { get; set; }

    /// <summary>
    /// 统计日期
    /// </summary>
    public DateTime StatisticsDate { get; set; }

    /// <summary>
    /// 在线用户列表（可选）
    /// </summary>
    public List<TaktOnlineDto>? OnlineUsers { get; set; }
}

/// <summary>
    /// 月统计DTO
    /// </summary>
public partial class MonthlyOnlineStatsDto
{
    /// <summary>
    /// 月份（格式：yyyy-MM）
    /// </summary>
    public string Month { get; set; } = string.Empty;

    /// <summary>
    /// 月总连接数
    /// </summary>
    public int TotalConnectionCount { get; set; }

    /// <summary>
    /// 月最高在线人数
    /// </summary>
    public int PeakOnlineUsers { get; set; }

    /// <summary>
    /// 月平均在线人数
    /// </summary>
    public double AverageOnlineUsers { get; set; }

    /// <summary>
    /// 月新增用户数
    /// </summary>
    public int NewUsersThisMonth { get; set; }

    /// <summary>
    /// 月活跃用户数
    /// </summary>
    public int ActiveUsersThisMonth { get; set; }

    /// <summary>
    /// 月离线用户数
    /// </summary>
    public int OfflineUsersThisMonth { get; set; }

    /// <summary>
    /// 月平均连接时长（秒）
    /// </summary>
    public double AverageConnectionDuration { get; set; }

    /// <summary>
    /// 统计月份开始日期
    /// </summary>
    public DateTime MonthStartDate { get; set; }

    /// <summary>
    /// 统计月份结束日期
    /// </summary>
    public DateTime MonthEndDate { get; set; }

    /// <summary>
    /// 每日统计详情
    /// </summary>
    public List<TodayOnlineStatsDto>? DailyStats { get; set; }
}

/// <summary>
    /// 实时消息统计DTO
    /// </summary>
public partial class RealTimeMessageStatsDto
{
    /// <summary>
    /// 当前未读消息总数
    /// </summary>
    public int CurrentUnreadMessages { get; set; }

    /// <summary>
    /// 当前在线用户发送的消息数
    /// </summary>
    public int MessagesSentByOnlineUsers { get; set; }

    /// <summary>
    /// 当前在线用户接收的消息数
    /// </summary>
    public int MessagesReceivedByOnlineUsers { get; set; }

    /// <summary>
    /// 最近1分钟发送消息数
    /// </summary>
    public int MessagesSentLastMinute { get; set; }

    /// <summary>
    /// 最近1分钟接收消息数
    /// </summary>
    public int MessagesReceivedLastMinute { get; set; }

    /// <summary>
    /// 最近1分钟系统通知数
    /// </summary>
    public int SystemNotificationsLastMinute { get; set; }

    /// <summary>
    /// 最近1分钟群组消息数
    /// </summary>
    public int GroupMessagesLastMinute { get; set; }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime LastUpdateTime { get; set; }

    /// <summary>
    /// 未读消息列表（可选）
    /// </summary>
    public List<TaktMessageDto>? UnreadMessages { get; set; }
}

/// <summary>
    /// 未读消息DTO
    /// </summary>
public partial class UnreadMessageDto
{
    /// <summary>
    /// 未读消息总数
    /// </summary>
    public int TotalUnreadCount { get; set; }

    /// <summary>
    /// 个人未读消息数
    /// </summary>
    public int PersonalUnreadCount { get; set; }

    /// <summary>
    /// 群组未读消息数
    /// </summary>
    public int GroupUnreadCount { get; set; }

    /// <summary>
    /// 系统通知未读数
    /// </summary>
    public int SystemNotificationUnreadCount { get; set; }

    /// <summary>
    /// 最后一条未读消息时间
    /// </summary>
    public DateTime? LastUnreadTime { get; set; }

    /// <summary>
    /// 未读消息列表（可选）
    /// </summary>
    public List<TaktMessageDto>? UnreadMessages { get; set; }
}

/// <summary>
    /// 已读消息DTO
    /// </summary>
public partial class ReadMessageDto
{
    /// <summary>
    /// 已读消息总数
    /// </summary>
    public int TotalReadCount { get; set; }

    /// <summary>
    /// 个人已读消息数
    /// </summary>
    public int PersonalReadCount { get; set; }

    /// <summary>
    /// 群组已读消息数
    /// </summary>
    public int GroupReadCount { get; set; }

    /// <summary>
    /// 系统通知已读数
    /// </summary>
    public int SystemNotificationReadCount { get; set; }

    /// <summary>
    /// 最后一条已读消息时间
    /// </summary>
    public DateTime? LastReadTime { get; set; }

    /// <summary>
    /// 已读消息列表（可选）
    /// </summary>
    public List<TaktMessageDto>? ReadMessages { get; set; }
}

/// <summary>
    /// 消息统计DTO
    /// </summary>
public partial class MessageStatsDto
{
    /// <summary>
    /// 总消息数
    /// </summary>
    public int TotalMessageCount { get; set; }

    /// <summary>
    /// 今日发送消息数
    /// </summary>
    public int TodaySendMessageCount { get; set; }

    /// <summary>
    /// 今日接收消息数
    /// </summary>
    public int TodayReceiveMessageCount { get; set; }

    /// <summary>
    /// 今日未读消息数
    /// </summary>
    public int TodayUnreadMessageCount { get; set; }

    /// <summary>
    /// 平均消息响应时间（毫秒）
    /// </summary>
    public double AverageResponseTime { get; set; }

    /// <summary>
    /// 消息发送成功率
    /// </summary>
    public double SendMessageSuccessRate { get; set; }

    /// <summary>
    /// 消息读取率
    /// </summary>
    public double MessageReadRate { get; set; }

    /// <summary>
    /// 统计时间
    /// </summary>
    public DateTime StatisticsTime { get; set; }

    /// <summary>
    /// 消息类型统计
    /// </summary>
    public Dictionary<string, int>? MessageTypeStats { get; set; }
}

/// <summary>
    /// 连接持续时间DTO
    /// </summary>
public partial class ConnectionDurationDto
{
    /// <summary>
    /// 连接持续时间（秒）
    /// </summary>
    public int DurationInSeconds { get; set; }

    /// <summary>
    /// 连接持续时间（毫秒）
    /// </summary>
    public long DurationInMilliseconds { get; set; }

    /// <summary>
    /// 连接开始时间
    /// </summary>
    public DateTime ConnectTime { get; set; }

    /// <summary>
    /// 连接结束时间
    /// </summary>
    public DateTime? DisconnectTime { get; set; }

    /// <summary>
    /// 连接状态
    /// </summary>
    public string Status { get; set; } = "Connected";
}

/// <summary>
    /// 断开连接时间DTO
    /// </summary>
public partial class DisconnectTimeDto
{
    /// <summary>
    /// SignalR连接ID
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// 断开连接时间
    /// </summary>
    public DateTime DisconnectTime { get; set; }

    /// <summary>
    /// 断开原因
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// 连接持续时间
    /// </summary>
    public int? ConnectionDuration { get; set; }
}

/// <summary>
    /// 在线用户统计DTO
    /// </summary>
public partial class OnlineUserStatsDto
{
    /// <summary>
    /// 总在线用户数
    /// </summary>
    public int TotalOnlineUsers { get; set; }

    /// <summary>
    /// 当前活跃用户数
    /// </summary>
    public int ActiveUsers { get; set; }

    /// <summary>
    /// 今日新增用户数
    /// </summary>
    public int NewUsersToday { get; set; }

    /// <summary>
    /// 平均连接时长（秒）
    /// </summary>
    public double AverageConnectionDuration { get; set; }

    /// <summary>
    /// 最长连接时长（秒）
    /// </summary>
    public int MaxConnectionDuration { get; set; }

    /// <summary>
    /// 最短连接时长（秒）
    /// </summary>
    public int MinConnectionDuration { get; set; }

    /// <summary>
    /// 统计时间
    /// </summary>
    public DateTime StatisticsTime { get; set; }

    /// <summary>
    /// 在线用户列表（可选）
    /// </summary>
    public List<TaktOnlineDto>? OnlineUsers { get; set; }
}

/// <summary>
    /// 离线用户统计DTO
    /// </summary>
public partial class OfflineUserStatsDto
{
    /// <summary>
    /// 总离线用户数
    /// </summary>
    public int TotalOfflineUsers { get; set; }

    /// <summary>
    /// 今日离线用户数
    /// </summary>
    public int OfflineUsersToday { get; set; }

    /// <summary>
    /// 今日退出登录用户数
    /// </summary>
    public int LogoutUsersToday { get; set; }

    /// <summary>
    /// 平均会话时长（秒）
    /// </summary>
    public double AverageSessionDuration { get; set; }

    /// <summary>
    /// 最长会话时长（秒）
    /// </summary>
    public int MaxSessionDuration { get; set; }

    /// <summary>
    /// 最短会话时长（秒）
    /// </summary>
    public int MinSessionDuration { get; set; }

    /// <summary>
    /// 统计时间
    /// </summary>
    public DateTime StatisticsTime { get; set; }

    /// <summary>
    /// 离线用户列表（可选）
    /// </summary>
    public List<TaktOnlineDto>? OfflineUsers { get; set; }
}

/// <summary>
    /// 实时综合统计DTO
    /// </summary>
public partial class RealTimeCombinedStatsDto
{
    /// <summary>
    /// 实时在线统计
    /// </summary>
    public RealTimeOnlineStatsDto? OnlineStats { get; set; }

    /// <summary>
    /// 实时消息统计
    /// </summary>
    public RealTimeMessageStatsDto? MessageStats { get; set; }

    /// <summary>
    /// 综合统计时间
    /// </summary>
    public DateTime StatisticsTime { get; set; }

    /// <summary>
    /// 系统健康状态
    /// </summary>
    public string HealthStatus { get; set; } = "Healthy";

    /// <summary>
    /// 信号延迟（毫秒）
    /// </summary>
    public int SignalDelayMs { get; set; }
}