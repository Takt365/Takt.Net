// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Constants
// 文件名称：TaktWorkflowProcessKeys.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程键常量，与 TaktFlowScheme.ProcessKey 及业务回调匹配
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Constants;

/// <summary>
/// 工作流流程键常量（对应 TaktFlowScheme.ProcessKey，用于种子数据与实例业务回调）
/// </summary>
public static class TaktWorkflowProcessKeys
{
    /// <summary>
    /// 公告发布审批流程键
    /// </summary>
    public const string ProcessKeyAnnouncement = "Announcement";

    /// <summary>
    /// 文控文档审批流程键
    /// </summary>
    public const string ProcessKeyDocument = "Document";
}
