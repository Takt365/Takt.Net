// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave.SpecificEngine
// 文件名称：TaktAttendanceLeavesController.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤请假专用控制器，提供假日主题色查询等RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave.SpecificEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave.SpecificEngine;

/// <summary>
/// 考勤请假专用控制器
/// </summary>
[Route("api/[controller]", Name = "考勤请假专用")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:attendanceleave:specific", "考勤请假专用管理")]
[ApiController]
public class TaktAttendanceLeavesController : TaktControllerBase
{
    private readonly ITaktAttendanceLeaveService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAttendanceLeavesController(
        ITaktAttendanceLeaveService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取假日主题色（根据日期和区域获取假日信息）
    /// </summary>
    /// <param name="date">检查日期（默认为当前日期，格式：yyyy-MM-dd）</param>
    /// <param name="region">区域代码（默认CN，如：CN、US、JP）</param>
    /// <returns>假日DTO对象，包含主题色信息</returns>
    [HttpGet("holiday-theme")]
    [AllowAnonymous]
    //[TaktPermission("humanresource:attendanceleave:holiday:theme", "假日主题色查询")]
    public async Task<ActionResult<TaktHolidayDto>> GetHolidayThemeAsync(
        [FromQuery] string? date = null,
        [FromQuery] string? region = null)
    {
        var result = await _service.GetHolidayThemeAsync(date, region);
        if (result == null) return NotFound("未找到指定日期的假日信息");
        return Ok(result);
    }
}