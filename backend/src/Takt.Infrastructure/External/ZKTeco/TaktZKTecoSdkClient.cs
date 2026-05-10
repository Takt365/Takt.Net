// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.ZKTeco
// 文件名称：TaktZKTecoSdkClient.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：中控对接占位客户端（当前以 HTTP 形态示例实现 Pull/Sync）。
// 上线注意：中控考勤/门禁在厂商标准集成路径下须使用 **TCP 协议层 SDK**（或厂商提供的等价专有协议）与设备通信；设备端**不提供**可作为标准对接方式的 **HTTP Push**，机内数据亦通常无法直接按本类所示 REST 路径访问。下列 HTTP 代码仅用于联调或与**已自建协议采集网关**对接；正式上线须以 TCP SDK 实现拉取/下发，或由网关将设备协议转译为系统 DTO。若仅改 ConfigJson 仍无法替代 TCP 协议实现。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Infrastructure.External.ZKTeco;

/// <summary>
/// 中控对接占位客户端：标准场景应接入厂商 TCP SDK；本类 HTTP 调用不代表中控机原生 REST 能力，亦与 HTTP 推送无关。
/// </summary>
public class TaktZKTecoSdkClient
{
    private const string DefaultAttendanceEndpoint = "/api/iclock/transactions";
    private const string DefaultUserSyncEndpoint = "/api/iclocks/users";

    /// <summary>
    /// 下载中控考勤数据。
    /// 注意：此方法为占位符。正式上线须使用中控官方 TCP SDK，而非 HTTP 请求。
    /// </summary>
    public async Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> DownloadAttendanceAsync(
        TaktAttendanceDevice device,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用中控官方 TCP SDK 实现
        throw new NotImplementedException(
            "中控考勤/门禁对接须使用厂商 TCP 协议层 SDK（或厂商提供的等价专有协议），" +
            "设备端不提供 HTTP Push 能力，当前 HTTP 实现仅为占位符，不可用于生产环境。");
    }

    /// <summary>
    /// 同步员工至中控设备。
    /// 注意：此方法为占位符。正式上线须使用中控官方 TCP SDK 实现员工同步。
    /// </summary>
    public async Task<int> SyncUsersAsync(
        TaktAttendanceDevice device,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用中控官方 TCP SDK 实现
        throw new NotImplementedException(
            "中控员工同步须使用官方 TCP 协议层 SDK，" +
            "当前 HTTP 实现仅为占位符，不可用于生产环境。");
    }

}
