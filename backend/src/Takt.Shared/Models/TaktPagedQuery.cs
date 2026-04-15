// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Models
// 文件名称：TaktPagedQuery.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt通用分页查询基类，所有查询DTO应继承此类
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Models;

/// <summary>
/// Takt通用分页查询基类
/// </summary>
public class TaktPagedQuery
{
    /// <summary>
    /// 当前页码（从1开始）
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 关键词（用于模糊查询，在多个字段中搜索）
    /// </summary>
    public string? KeyWords { get; set; }
}