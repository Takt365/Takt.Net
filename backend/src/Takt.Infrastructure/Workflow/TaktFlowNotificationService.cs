// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Workflow
// 文件名称：TaktFlowNotificationService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流通知服务默认实现（空操作）；可替换为对接 NotificationConfig、站内信/SignalR/邮件的实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Services.Workflow;

namespace Takt.Infrastructure.Workflow;

/// <summary>
/// 工作流通知服务默认实现（空操作）
/// </summary>
public class TaktFlowNotificationService : ITaktFlowNotificationService
{
    /// <inheritdoc />
    public Task NotifyInstanceStartedAsync(long instanceId, long schemeId) => Task.CompletedTask;

    /// <inheritdoc />
    public Task NotifyInstanceEndedAsync(long instanceId, long schemeId, bool completed) => Task.CompletedTask;

    /// <inheritdoc />
    public Task NotifyNodeReachedAsync(long instanceId, long schemeId, string nodeId, string nodeName) => Task.CompletedTask;

    /// <inheritdoc />
    public Task NotifyTimeoutAsync(long instanceId, long schemeId, bool passed) => Task.CompletedTask;
}
