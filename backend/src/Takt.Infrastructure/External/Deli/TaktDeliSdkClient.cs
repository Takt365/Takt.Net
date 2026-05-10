// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.Deli
// 文件名称：TaktDeliSdkClient.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：得力 e+ 云考勤对接客户端（综合签到 / 员工管理），协议依据官方《考勤数据对接协议》。
// 官方文档：https://doc.delicloud.com/v3/integration/oa.html
// 协议要点：域名 https://v2-api.delicloud.com ；POST JSON ；请求头 App-Key / App-Timestamp / App-Sig（sig=md5(Path+Timestamp+Key+Secret) 小写）；
//           综合签到类接口固定 POST /v2.0/cloudappapi，通过 Api-Module=CHECKIN 与 Api-Cmd 区分指令（如 checkin_query_init、checkin_query）；
//           员工同步 POST /v2.0/employee（签名 Path 为 /v2.0/employee）。设备 ConfigJson 须含 appKey、appSecret（可用设备 apiSecret 作 secret），以及 deliDefaultDepartmentExtId 等，详见类内 DeliConfig 说明。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Infrastructure.External.Deli;

/// <summary>
/// 得力 e+ 开放接口客户端。实现须与 <see href="https://doc.delicloud.com/v3/integration/oa.html"/> 保持一致；未配置 appKey/appSecret 时回退为旧版「直连设备 IP」占位请求（仅用于联调）。
/// </summary>
public class TaktDeliSdkClient
{
    private const string DeliCloudApiBase = "https://v2-api.delicloud.com";
    private const string PathCloudAppApi = "/v2.0/cloudappapi";
    private const string PathEmployee = "/v2.0/employee";
    private const string HeaderApiModule = "CHECKIN";
    private const string CmdCheckinInit = "checkin_query_init";
    private const string CmdCheckinQuery = "checkin_query";

    private const string DefaultAttendanceEndpoint = "/api/attendances/records";
    private const string DefaultUserSyncEndpoint = "/api/attendances/users/sync";

    /// <summary>
    /// 拉取得力云考勤原始记录。
    /// 注意：此方法为占位符。正式上线须使用得力官方 TCP SDK 或协议采集网关，而非 HTTP 请求。
    /// </summary>
    public async Task<TaktDeliAttendanceDownloadResult> DownloadAttendanceAsync(
        TaktAttendanceDevice device,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用得力官方 TCP SDK 实现
        throw new NotImplementedException(
            "得力考勤对接须使用官方 TCP 协议层 SDK（或厂商提供的等价专有协议），" +
            "当前 HTTP 实现仅为占位符，不可用于生产环境。" +
            "请参考官方文档：https://doc.delicloud.com/v3/integration/oa.html");
    }

    /// <summary>
    /// 同步员工至得力云。
    /// 注意：此方法为占位符。正式上线须使用得力官方 TCP SDK 实现员工同步。
    /// </summary>
    public async Task<int> SyncUsersAsync(
        TaktAttendanceDevice device,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken = default)
    {
        // TODO: 使用得力官方 TCP SDK 实现
        throw new NotImplementedException(
            "得力员工同步须使用官方 TCP 协议层 SDK，" +
            "当前 HTTP 实现仅为占位符，不可用于生产环境。");
    }

}

/// <summary>
/// 得力云拉取结果（占位符）。
/// </summary>
public sealed class TaktDeliAttendanceDownloadResult
{
    public IReadOnlyList<TaktAttendanceSourceIngestRowDto> Rows { get; init; } = Array.Empty<TaktAttendanceSourceIngestRowDto>();

    public string? UpdatedDeviceConfigJson { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();
}
