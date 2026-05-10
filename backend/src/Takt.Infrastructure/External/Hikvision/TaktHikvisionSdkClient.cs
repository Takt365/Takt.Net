// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.Hikvision
// 文件名称：TaktHikvisionSdkClient.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：海康威视 ISAPI SDK 轻量客户端（用户同步、考勤数据下载）。
// 上线注意：本类默认 ISAPI 路径与 JSON 字段解析为常见形态；不同门禁/考勤机型、固件版本与 ISAPI 能力存在差异，系统上线前须按现场设备型号与官方文档核对并调整（也可仅通过设备 ConfigJson 覆盖 endpoint、鉴权与分页参数，无需改代码）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Infrastructure.External.Hikvision;

/// <summary>
/// 海康设备 SDK 客户端（基于 ISAPI HTTP）。默认路径与解析逻辑需按现场机型与固件对齐；上线前请按设备型号与 ISAPI 文档调整，或通过 <see cref="TaktAttendanceDevice.ConfigJson"/> 覆盖。
/// </summary>
public class TaktHikvisionSdkClient
{
    private const string DefaultAttendanceSearchEndpoint = "/ISAPI/AccessControl/AcsEvent?format=json";
    private const string DefaultUserSyncEndpoint = "/ISAPI/AccessControl/UserInfo/Record?format=json";

    /// <summary>
    /// 下载考勤事件。
    /// 注意：此方法为占位符。正式上线须使用海康官方 SDK，而非 ISAPI HTTP 接口。
    /// </summary>
    /// <param name="device">设备</param>
    /// <param name="request">拉取请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>标准化行</returns>
    public async Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> DownloadAttendanceAsync(
        TaktAttendanceDevice device,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用海康官方 SDK 实现
        throw new NotImplementedException(
            "海康威视门禁/考勤对接须使用官方 SDK，" +
            "不同机型、固件版本与 ISAPI 能力存在差异，当前 HTTP 实现仅为占位符，不可用于生产环境。");
    }

    /// <summary>
    /// 同步用户信息到海康设备。
    /// 注意：此方法为占位符。正式上线须使用海康官方 SDK 实现用户同步。
    /// </summary>
    /// <param name="device">设备</param>
    /// <param name="users">用户</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>成功数量</returns>
    public async Task<int> SyncUsersAsync(
        TaktAttendanceDevice device,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用海康官方 SDK 实现
        throw new NotImplementedException(
            "海康威视用户同步须使用官方 SDK，" +
            "当前 HTTP 实现仅为占位符，不可用于生产环境。");
    }

}
