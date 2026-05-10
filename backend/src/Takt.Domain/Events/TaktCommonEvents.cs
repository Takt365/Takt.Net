// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Events
// 文件名称：TaktCommonEvents.cs
// 创建时间：2026-04-27
// 创建人：Takt365
// 功能描述：通用事件定义（CRUD 和业务事件）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using MediatR;

namespace Takt.Domain.Events;

/// <summary>
/// 通用 CRUD 事件基类（实现 ITaktEvent 和 INotification）
/// </summary>
public abstract class TaktCrudEvent : ITaktEvent, INotification
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
    /// 模块名称（如：Leave、User、Role、Dept）
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 实体ID
    /// </summary>
    public long EntityId { get; set; }

    /// <summary>
    /// 实体类型名称（如：TaktLeave、TaktUser）
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// 操作人ID
    /// </summary>
    public long? OperatorId { get; set; }

    /// <summary>
    /// 扩展数据（可携带任意业务数据）
    /// </summary>
    public object? Data { get; set; }
}

/// <summary>
/// 创建事件
/// </summary>
public class TaktCrudCreatedEvent : TaktCrudEvent { }

/// <summary>
/// 更新事件
/// </summary>
public class TaktCrudUpdatedEvent : TaktCrudEvent { }

/// <summary>
/// 删除事件
/// </summary>
public class TaktCrudDeletedEvent : TaktCrudEvent { }

/// <summary>
/// 通用业务事件（用于业务流程触发）
/// </summary>
public class TaktBusinessEvent : ITaktEvent, INotification
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
    /// 模块名称（如：Leave、Approval、Workflow）
    /// </summary>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    /// 动作名称（如：Submit、Approve、Reject）
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// 实体ID
    /// </summary>
    public long EntityId { get; set; }

    /// <summary>
    /// 实体类型名称
    /// </summary>
    public string EntityType { get; set; } = string.Empty;

    /// <summary>
    /// 操作人ID
    /// </summary>
    public long? OperatorId { get; set; }

    /// <summary>
    /// 扩展数据（可携带任意业务数据）
    /// </summary>
    public object? Data { get; set; }
}
