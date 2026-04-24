// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Dict
// 文件名称：TaktDictSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：字典DTO业务扩展字段
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Routine.Tasks.Dict;

/// <summary>
/// Takt字典类型创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDictTypeCreateDto
{
    /// <summary>
    /// 字典数据列表（非数据库字段）
    /// </summary>
    public List<TaktDictDataDto>? DictDataList { get; set; }
}

/// <summary>
/// Takt字典类型更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDictTypeUpdateDto
{
    // DictDataList 已从 TaktDictTypeCreateDto 继承，无需重复定义
}

/// <summary>
/// Takt字典类型导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDictTypeExportDto
{
    /// <summary>
    /// 数据源字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? DataSourceString { get; set; }
}

/// <summary>
/// Takt字典类型DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktDictTypeDto
{
    /// <summary>
    /// 字典数据列表（非数据库字段，用于包含完整字典数据）
    /// </summary>
    public List<TaktDictDataDto>? DictDataListObjects { get; set; }
}
