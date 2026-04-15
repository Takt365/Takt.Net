// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceIngestController.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备采集/推送入口控制器（第一版骨架）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;

namespace Takt.WebApi.Controllers.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备数据采集入口控制器
/// </summary>
[Route("api/[controller]", Name = "考勤采集入口")]
[ApiModule("HumanResource.AttendanceLeave", "考勤请假")]
public class TaktAttendanceIngestController : TaktControllerBase
{
    private readonly ITaktAttendanceCollector _collector;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="collector">采集网关服务</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceIngestController(
        ITaktAttendanceCollector collector,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _collector = collector;
    }

    /// <summary>
    /// 设备主动推送入口（第一版骨架）
    /// </summary>
    /// <param name="request">推送请求</param>
    /// <returns>处理结果</returns>
    [HttpPost("push")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktAttendancePushHandleResultDto>> ReceivePushAsync([FromBody] TaktAttendancePushRequestDto request)
    {
        return Ok(await _collector.ReceiveAttendancePushAsync(request));
    }

    /// <summary>
    /// 设备拉取入口（第一版骨架）
    /// </summary>
    /// <param name="request">拉取请求</param>
    /// <returns>处理结果</returns>
    [HttpPost("pull")]
    [TaktPermission("humanresource:attendanceleave:attendancesource:create", "拉取设备考勤数据")]
    public async Task<ActionResult<TaktAttendancePullResultDto>> PullAsync([FromBody] TaktAttendancePullRequestDto request)
    {
        return Ok(await _collector.PullAttendanceRecordsAsync(request));
    }

    /// <summary>
    /// 获取设备联机状态（第一版骨架）
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>设备状态</returns>
    [HttpGet("devices/{deviceId}/status")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:list", "查询设备在线状态")]
    public async Task<ActionResult<TaktAttendanceDeviceStatusDto>> GetDeviceStatusAsync(long deviceId)
    {
        return Ok(await _collector.GetAttendanceDeviceStatusAsync(deviceId));
    }

    /// <summary>
    /// 同步设备用户信息（如海康 employeeNo、cardNo）
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="request">同步请求</param>
    /// <returns>同步结果</returns>
    [HttpPost("devices/{deviceId}/sync-users")]
    [TaktPermission("humanresource:attendanceleave:attendancedevice:update", "同步设备用户信息")]
    public async Task<ActionResult<TaktAttendanceUserSyncResultDto>> SyncDeviceUsersAsync(
        long deviceId,
        [FromBody] TaktAttendanceUserSyncRequestDto request)
    {
        return Ok(await _collector.SyncDeviceUsersAsync(deviceId, request));
    }
}
