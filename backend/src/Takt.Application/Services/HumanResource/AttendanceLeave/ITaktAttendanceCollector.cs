// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceCollector.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备采集网关接口（第一版骨架）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备采集网关接口
/// </summary>
public interface ITaktAttendanceCollector
{
    /// <summary>
    /// 从设备侧拉取考勤原始记录
    /// </summary>
    /// <param name="request">拉取请求</param>
    /// <returns>拉取处理结果</returns>
    Task<TaktAttendancePullResultDto> PullAttendanceRecordsAsync(TaktAttendancePullRequestDto request);

    /// <summary>
    /// 接收设备主动推送的考勤原始记录
    /// </summary>
    /// <param name="request">推送请求</param>
    /// <returns>处理结果</returns>
    Task<TaktAttendancePushHandleResultDto> ReceiveAttendancePushAsync(TaktAttendancePushRequestDto request);

    /// <summary>
    /// 查询设备联机状态
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>设备状态</returns>
    Task<TaktAttendanceDeviceStatusDto> GetAttendanceDeviceStatusAsync(long deviceId);

    /// <summary>
    /// 同步设备用户信息
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="request">同步请求</param>
    /// <returns>同步结果</returns>
    Task<TaktAttendanceUserSyncResultDto> SyncDeviceUsersAsync(long deviceId, TaktAttendanceUserSyncRequestDto request);
}
