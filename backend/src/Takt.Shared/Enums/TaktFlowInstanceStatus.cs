// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Enums
// 文件名称：TaktFlowInstanceStatus.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程实例状态枚举
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Enums;

/// <summary>
/// 工作流流程实例状态
/// </summary>
public enum TaktFlowInstanceStatus
{
    /// <summary>运行中</summary>
    Running = 0,

    /// <summary>已完成</summary>
    Completed = 1,

    /// <summary>已终止</summary>
    Terminated = 2,

    /// <summary>已挂起</summary>
    Suspended = 3,

    /// <summary>已撤回</summary>
    Recalled = 4
}
