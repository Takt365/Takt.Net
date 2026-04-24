// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.SignalR
// 文件名称：TaktSignalRSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：SignalR消息相关业务特定DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.SignalR;

/// <summary>
/// Takt消息已读DTO
/// </summary>
public class TaktMessageReadDto
{
    /// <summary>
    /// 消息ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MessageId { get; set; }
}

/// <summary>
/// Takt在线用户导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktOnlineExportDto
{
    /// <summary>
    /// 在线状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? OnlineStatusString { get; set; }
}

/// <summary>
/// Takt消息导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktMessageExportDto
{
    /// <summary>
    /// 阅读状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? ReadStatusString { get; set; }
}
