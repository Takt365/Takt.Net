// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Code.Generator
// 文件名称：TaktCodeGeneratorSpecific.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：特殊DTO集合，包含业务特定的数据传输对象（如头像更新、密码重置、用户解锁等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Code.Generator;

/// <summary>
/// Takt代码生成表配置DTO扩展（用于包含字段列表）
/// </summary>
public partial class TaktGenTableDto
{
    /// <summary>
    /// 字段列表（非数据库字段，用于业务逻辑）
    /// </summary>
    public List<TaktGenTableColumnDto>? Columns { get; set; }
}

/// <summary>
/// Takt代码生成表创建DTO扩展（用于包含字段列表）
/// </summary>
public partial class TaktGenTableCreateDto
{
    /// <summary>
    /// 字段列表（非数据库字段，用于业务逻辑）
    /// </summary>
    public List<TaktGenTableColumnDto>? Columns { get; set; }
}

/// <summary>
/// Takt代码生成表更新DTO扩展（用于包含字段列表）
/// </summary>
public partial class TaktGenTableUpdateDto
{
    /// <summary>
    /// 字段列表（非数据库字段，用于业务逻辑）
    /// </summary>
    public List<TaktGenTableColumnDto>? Columns { get; set; }
}
