// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Shared.Enums
// 文件名称：TaktDocumentLifecycleStage.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：文控文档生命周期阶段枚举，与 ISO 文件全生命周期管理对应
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Enums;

/// <summary>
/// 文控文档生命周期阶段（ISO 视角：创建→审批→发布→使用→变更→归档→销毁）
/// </summary>
public enum TaktDocumentLifecycleStage
{
    /// <summary>创建（起草/上传）</summary>
    Create = 0,

    /// <summary>审批（核稿/会签/签发）</summary>
    Approval = 1,

    /// <summary>发布（生效、分发）</summary>
    Publish = 2,

    /// <summary>使用（查阅、借阅）</summary>
    InUse = 3,

    /// <summary>变更（修订、升版）</summary>
    Change = 4,

    /// <summary>归档（封存、备份）</summary>
    Archive = 5,

    /// <summary>销毁（到期经审批销毁）</summary>
    Destroy = 6
}
