// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤原始记录DTO业务扩展字段
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// Takt考勤原始记录导入DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceImportDto
{
    /// <summary>
    /// 设备编码（用于导入时查找设备）
    /// </summary>
    public string? DeviceCode { get; set; }
}

/// <summary>
/// Takt考勤原始记录导出DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceExportDto
{
    /// <summary>
    /// 设备编码（业务展示字段）
    /// </summary>
    public string? DeviceCode { get; set; }
}

/// <summary>
/// Takt考勤原始记录DTO扩展（用于包含设备编码等业务字段）
/// </summary>
public partial class TaktAttendanceSourceDto
{
    /// <summary>
    /// 设备编码（业务展示字段）
    /// </summary>
    public string? DeviceCode { get; set; }
}
