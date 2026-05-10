// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.EventBus
// 文件名称：TaktEventBus.cs
// 创建时间：2026-04-27
// 创建人：Takt365
// 功能描述：事件总线基础设施实现（基于 MediatR）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using MediatR;
using Takt.Domain.Events;

namespace Takt.Infrastructure.EventBus;

/// <summary>
/// 事件总线实现（基于 MediatR）
/// </summary>
public class TaktEventBus : ITaktEventBus
{
    private readonly IMediator _mediator;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="mediator">MediatR 中介者</param>
    public TaktEventBus(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// 发布单个事件
    /// </summary>
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : ITaktEvent
    {
        if (@event == null) throw new ArgumentNullException(nameof(@event));
        await _mediator.Publish(@event, cancellationToken);
    }

    /// <summary>
    /// 批量发布事件
    /// </summary>
    public async Task PublishBatchAsync<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellationToken = default) where TEvent : ITaktEvent
    {
        if (events == null) throw new ArgumentNullException(nameof(events));
        
        foreach (var evt in events)
        {
            await _mediator.Publish(evt, cancellationToken);
        }
    }
}
