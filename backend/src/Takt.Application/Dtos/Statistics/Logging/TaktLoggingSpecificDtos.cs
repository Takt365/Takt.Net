// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktLoggingSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：日志相关业务特定DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// Takt AOP日志创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktAopLogCreateDto
{
    /// <summary>
    /// 审计用户ID（非数据库字段）
    /// </summary>
    public long? AuditUserId { get; set; }
    
    /// <summary>
    /// 审计用户显示名称（非数据库字段）
    /// </summary>
    public string? AuditUserDisplayName { get; set; }
}

/// <summary>
/// Takt操作日志创建DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktOperLogCreateDto
{
    /// <summary>
    /// 审计用户ID（非数据库字段）
    /// </summary>
    public long? AuditUserId { get; set; }
    
    /// <summary>
    /// 审计用户显示名称（非数据库字段）
    /// </summary>
    public string? AuditUserDisplayName { get; set; }
}

/// <summary>
/// Takt任务日志导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktQuartzLogExportDto
{
    /// <summary>
    /// 执行状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? ExecuteStatusString { get; set; }
}

/// <summary>
/// Takt登录日志导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktLoginLogExportDto
{
    /// <summary>
    /// 登录状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? LoginStatusString { get; set; }
}

/// <summary>
/// Takt操作日志导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktOperLogExportDto
{
    /// <summary>
    /// 操作状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? OperStatusString { get; set; }
}
