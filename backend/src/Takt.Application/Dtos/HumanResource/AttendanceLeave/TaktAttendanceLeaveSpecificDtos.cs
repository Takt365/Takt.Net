// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceLeaveSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：考勤排班DTO业务扩展字段
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.AttendanceLeave;

/// <summary>
/// Takt排班导入DTO扩展
/// </summary>
public partial class TaktShiftScheduleImportDto
{
    /// <summary>
    /// 班次编码（用于导入时查找班次）
    /// </summary>
    public string? ShiftCode { get; set; }    

}

/// <summary>
/// Takt排班导出DTO扩展
/// </summary>
public partial class TaktShiftScheduleExportDto
{
    /// <summary>
    /// 班次名称（业务展示字段）
    /// </summary>
    public string? ShiftName { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt排班DTO扩展
/// </summary>
public partial class TaktShiftScheduleDto
{
    /// <summary>
    /// 班次名称（业务展示字段）
    /// </summary>
    public string? ShiftName { get; set; }
}
