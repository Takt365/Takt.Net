// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceDeviceAdapter.cs
// 创建时间：2026-04-29
// 创建人：Qoder AI
// 功能描述：考勤设备适配器接口定义，支持不同厂商设备的统一抽象
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备适配器基础接口
/// </summary>
public interface ITaktAttendanceDeviceAdapter
{
    /// <summary>
    /// 从设备拉取考勤数据并入库
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="request">拉取请求</param>
    /// <returns>拉取结果</returns>
    Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request);

    /// <summary>
    /// 同步设备用户信息
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="users">用户同步项列表</param>
    /// <returns>成功同步数量</returns>
    Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users);

    /// <summary>
    /// 解析设备主动推送的数据
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="request">推送请求</param>
    /// <returns>解析后的考勤源记录</returns>
    Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request);

    /// <summary>
    /// 检测设备在线状态
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <returns>设备状态</returns>
    Task<TaktAttendanceDeviceOnlineStatusDto> PingAsync(TaktAttendanceDevice device);
}

/// <summary>
/// 得力设备适配器接口
/// </summary>
public interface ITaktDeliAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}

/// <summary>
/// 海康设备适配器接口
/// </summary>
public interface ITaktHikvisionAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}

/// <summary>
/// 中控设备适配器接口
/// </summary>
public interface ITaktZKTecoAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}