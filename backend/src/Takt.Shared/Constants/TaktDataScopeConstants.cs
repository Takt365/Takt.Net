// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Constants
// 文件名称：TaktDataScopeConstants.cs
// 创建时间：2026-04-18
// 创建人：Takt365(Cursor AI)
// 功能描述：数据范围（DataScope）枚举值，与角色/部门实体字段一致
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Constants;

/// <summary>
/// 数据范围（与 <c>TaktRole.DataScope</c>、<c>TaktDept.DataScope</c> 语义一致）。
/// </summary>
public static class TaktDataScopeConstants
{
    /// <summary>全部数据</summary>
    public const int All = 0;

    /// <summary>本部门数据</summary>
    public const int DeptOnly = 1;

    /// <summary>本部门及以下数据</summary>
    public const int DeptAndChildren = 2;

    /// <summary>仅本人数据</summary>
    public const int SelfOnly = 3;

    /// <summary>自定义部门范围（使用 CustomScope）</summary>
    public const int Custom = 4;
}
