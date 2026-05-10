// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Events
// 文件名称：ITaktEventBus.cs
// 创建时间：2026-04-27
// 创建人：Takt365
// 功能描述：事件总线接口和领域事件基类定义（DDD 领域层）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Domain.Events;

/// <summary>
/// 事件总线接口（定义在 Domain 层，实现在 Infrastructure 层）
/// </summary>
/// <remarks>
/// <para>设计原则：依赖倒置 - Application 层依赖此接口，Infrastructure 层实现此接口</para>
/// <para>Application 层通过此接口发布/订阅事件，不关心具体实现（MediatR、RabbitMQ 等）</para>
/// </remarks>
public interface ITaktEventBus
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : ITaktEvent;

    /// <summary>
    /// 批量发布事件
    /// </summary>
    /// <typeparam name="TEvent">事件类型</typeparam>
    /// <param name="events">事件列表</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task PublishBatchAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default) where TEvent : ITaktEvent;
}

/// <summary>
/// 领域事件接口（所有领域事件必须实现此接口）
/// </summary>
public interface ITaktEvent
{
    /// <summary>
    /// 事件发生时间（UTC）
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// 事件ID（用于追踪和日志）
    /// </summary>
    string EventId { get; }
}

/// <summary>
/// 纯领域事件基类（不依赖任何框架）
/// </summary>
public abstract class TaktDomainEvent : ITaktEvent
{
    /// <summary>
    /// 事件发生时间（UTC）
    /// </summary>
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 事件ID（用于追踪和日志）
    /// </summary>
    public string EventId { get; protected set; } = Guid.NewGuid().ToString("N");
}

/// <summary>
/// 集成事件基类（用于跨服务/跨模块通信）
/// </summary>
public abstract class TaktIntegrationEvent : ITaktEvent
{
    /// <summary>
    /// 事件发生时间（UTC）
    /// </summary>
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// 事件ID（用于追踪和日志）
    /// </summary>
    public string EventId { get; protected set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    /// 事件版本号（用于事件演进和兼容性）
    /// </summary>
    public string Version { get; protected set; } = "1.0";
}
