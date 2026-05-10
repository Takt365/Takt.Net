// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave.SpecificEngine
// 文件名称：ITaktAttendanceLeaveService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤休假公共应用服务接口，提供Holiday主题色等公共接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Shared.Models;

namespace Takt.Application.Services.HumanResource.AttendanceLeave.SpecificEngine;

/// <summary>
/// 考勤休假专用服务接口
/// </summary>
public interface ITaktAttendanceLeaveService
{
    /// <summary>
    /// 获取指定日期的假日主题色（用于前端根据假日动态显示主色调，支持未登录访问）
    /// </summary>
    /// <param name="date">日期（可选，格式：yyyy-MM-dd）</param>
    /// <param name="region">区域（可选，默认zh-CN）</param>
    /// <returns>假日主题DTO，无假日时返回null</returns>
    Task<TaktHolidayDto?> GetHolidayThemeAsync(string? date, string? region);
}