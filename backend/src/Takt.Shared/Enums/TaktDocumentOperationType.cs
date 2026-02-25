// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Enums
// 文件名称：TaktDocumentOperationType.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：文控文档历史操作类型枚举，与 OA 发文/收文及 ISO 全生命周期对应
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Enums;

/// <summary>
/// 文控文档历史操作类型（拟稿-审批-签发-归档-签收-变更-销毁等）
/// </summary>
public enum TaktDocumentOperationType
{
    /// <summary>拟稿</summary>
    Draft = 0,

    /// <summary>核稿</summary>
    Review = 1,

    /// <summary>会签</summary>
    Countersign = 2,

    /// <summary>签发</summary>
    Approve = 3,

    /// <summary>编号/盖章</summary>
    NumberAndSeal = 4,

    /// <summary>分发</summary>
    Distribute = 5,

    /// <summary>归档</summary>
    Archive = 6,

    /// <summary>签收</summary>
    Receipt = 7,

    /// <summary>变更/升版</summary>
    Change = 8,

    /// <summary>废止</summary>
    Obsolete = 9,

    /// <summary>销毁</summary>
    Destroy = 10,

    /// <summary>其他</summary>
    Other = 11
}
