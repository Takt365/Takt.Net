// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowNotificationService.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流通知服务接口（节点到达、流程结束、超时等）；具体实现可对接站内信/SignalR/邮件，受方案 NotificationConfig 控制
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流通知服务接口
/// </summary>
/// <remarks>实现类可解析 TaktFlowScheme.NotificationConfig，按配置发送站内信/SignalR/邮件等；默认实现为空操作。</remarks>
public interface ITaktFlowNotificationService
{
    /// <summary>
    /// 流程实例已发起（进入首节点）
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="schemeId">方案ID</param>
    Task NotifyInstanceStartedAsync(long instanceId, long schemeId);

    /// <summary>
    /// 流程实例已结束（通过或终止）
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="schemeId">方案ID</param>
    /// <param name="completed">true=通过完成，false=驳回终止</param>
    Task NotifyInstanceEndedAsync(long instanceId, long schemeId, bool completed);

    /// <summary>
    /// 流程进入某节点（审批节点到达，可通知处理人）
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="schemeId">方案ID</param>
    /// <param name="nodeId">节点ID</param>
    /// <param name="nodeName">节点名称</param>
    Task NotifyNodeReachedAsync(long instanceId, long schemeId, string nodeId, string nodeName);

    /// <summary>
    /// 流程超时自动推进
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="schemeId">方案ID</param>
    /// <param name="passed">true=自动通过，false=自动驳回</param>
    Task NotifyTimeoutAsync(long instanceId, long schemeId, bool passed);
}
