// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Models
// 文件名称：TaktSelectOption.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt通用下拉选择框选项，用于前端下拉框、选择器等组件
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;

namespace Takt.Shared.Models;

/// <summary>
/// 下拉选择框选项
/// </summary>
public class TaktSelectOption
{
    /// <summary>
    /// 字典标签
    /// </summary>
    public string DictLabel { get; set; } = string.Empty;

    /// <summary>
    /// 字典键值
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public object DictValue { get; set; } = string.Empty;

    /// <summary>
    /// 扩展标签
    /// </summary>
    public string? ExtLabel { get; set; }

    /// <summary>
    /// 扩展键值
    /// </summary>
    public object? ExtValue { get; set; }

    /// <summary>
    /// 字典本地化键（用于多语言翻译）
    /// </summary>
    public string? DictL10nKey { get; set; }

    /// <summary>
    /// 字典类型编码（用于批量加载时前端分组，单个查询时通常为空）
    /// </summary>
    public string? DictTypeCode { get; set; }

    /// <summary>
    /// CSS类名
    /// </summary>
    public int? CssClass { get; set; }

    /// <summary>
    /// 列表类名
    /// </summary>
    public int? ListClass { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; } = 0;
}

/// <summary>
/// 树形下拉选择框选项（通用树形结构，适用于部门、会计科目、菜单等）
/// </summary>
public class TaktTreeSelectOption : TaktSelectOption
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTreeSelectOption()
    {
        Children = new List<TaktTreeSelectOption>();
    }

    /// <summary>
    /// 子节点列表
    /// </summary>
    public List<TaktTreeSelectOption> Children { get; set; }
}