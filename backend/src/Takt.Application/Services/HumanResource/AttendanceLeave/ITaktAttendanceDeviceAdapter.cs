// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：ITaktAttendanceDeviceAdapter.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：设备适配器与工厂接口（第一版骨架）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 单设备适配器接口
/// </summary>
public interface ITaktAttendanceDeviceAdapter
{
    /// <summary>
    /// 根据拉取请求读取设备数据
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="request">拉取请求</param>
    /// <returns>标准化结果</returns>
    Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request);

    /// <summary>
    /// 同步设备用户信息（如海康 employeeNo / cardNo）
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="users">用户同步项</param>
    /// <returns>成功同步数量</returns>
    Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users);

    /// <summary>
    /// 解析设备主动推送数据
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <param name="request">推送请求</param>
    /// <returns>处理结果</returns>
    Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request);

    /// <summary>
    /// 检测设备状态
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <returns>设备状态</returns>
    Task<TaktAttendanceDeviceOnlineStatusDto> PingAsync(TaktAttendanceDevice device);
}

/// <summary>
/// 海康设备适配器接口（用于工厂按设备类型路由）
/// </summary>
public interface ITaktHikvisionAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}

/// <summary>
/// 得力设备适配器接口（用于工厂按设备品牌路由）。对接须遵循得力云《考勤数据对接协议》：https://doc.delicloud.com/v3/integration/oa.html
/// </summary>
public interface ITaktDeliAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}

/// <summary>
/// 中控设备适配器接口（用于工厂按设备品牌路由）。现场对接以厂商 TCP/专有协议 SDK 为准；中控不适用 HTTP 推送。
/// </summary>
public interface ITaktZKTecoAttendanceDeviceAdapter : ITaktAttendanceDeviceAdapter
{
}

/// <summary>
/// 设备适配器工厂接口
/// </summary>
public interface ITaktAttendanceDeviceAdapterFactory
{
    /// <summary>
    /// 根据设备主数据创建对应适配器
    /// </summary>
    /// <param name="device">设备主数据</param>
    /// <returns>设备适配器</returns>
    ITaktAttendanceDeviceAdapter CreateAdapter(TaktAttendanceDevice device);
}
