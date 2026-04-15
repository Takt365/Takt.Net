// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Models
// 文件名称：TaktExcelOptions.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt Excel配置选项，用于配置Excel导入导出的相关设置
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Shared.Models;

/// <summary>
/// Takt Excel配置选项
/// </summary>
public class TaktExcelOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktExcelOptions()
    {
    }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 主题
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 类别
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keywords { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public string Application { get; set; } = string.Empty;

    /// <summary>
    /// 公司
    /// </summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>
    /// 管理者
    /// </summary>
    public string Manager { get; set; } = string.Empty;
}
