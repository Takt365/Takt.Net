// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Enums
// 文件名称：TaktFlowOperationType.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程操作类型枚举
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Enums;

/// <summary>
/// 工作流流程操作类型
/// </summary>
public enum TaktFlowOperationType
{
    /// <summary>启动</summary>
    Start = 0,

    /// <summary>终止</summary>
    Terminate = 9,

    /// <summary>完成</summary>
    Complete = 10,

    /// <summary>撤回</summary>
    Recall = 6,

    /// <summary>节点指定/转办</summary>
    Designate = 8,

    /// <summary>撤销审批</summary>
    UndoVerification = 11,

    /// <summary>挂起</summary>
    Suspend = 12,

    /// <summary>恢复</summary>
    Resume = 13,

    /// <summary>退回</summary>
    Return = 14
}
