// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.ZKTeco
// 文件名称：TaktZKTecoAttendanceDeviceAdapterService.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：中控设备适配器（用户同步、考勤下载并落库）。
// 上线注意：中控不适用 HTTP 推送；ParsePush 固定空实现。现场应以 TCP SDK 采集或由采集服务写入考勤源数据；本适配器内 HTTP 客户端为占位，与厂商机端协议不一致时须替换实现。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.External.ZKTeco;

/// <summary>
/// 中控设备适配器。考勤数据应以 TCP SDK 等方式采集；不支持、不实现基于 HTTP 的设备推送解析。
/// </summary>
public class TaktZKTecoAttendanceDeviceAdapterService : ITaktZKTecoAttendanceDeviceAdapter
{
    private readonly TaktZKTecoSdkClient _sdkClient;
    private readonly ITaktRepository<TaktAttendanceSource> _sourceRepository;

    public TaktZKTecoAttendanceDeviceAdapterService(
        TaktZKTecoSdkClient sdkClient,
        ITaktRepository<TaktAttendanceSource> sourceRepository)
    {
        _sdkClient = sdkClient;
        _sourceRepository = sourceRepository;
    }

    public async Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request)
    {
        var result = new TaktAttendancePullResultDto { Success = false, AcceptedCount = 0 };
        var rows = await _sdkClient.DownloadAttendanceAsync(device, request);
        if (rows.Count == 0)
        {
            result.Success = true;
            return result;
        }

        var batchNo = $"zkteco-{DateTime.Now:yyyyMMddHHmmss}";
        var toInsert = new List<TaktAttendanceSource>(rows.Count);
        foreach (var row in rows)
        {
            var key = string.IsNullOrWhiteSpace(row.ExternalRecordKey) ? null : row.ExternalRecordKey.Trim();
            if (!string.IsNullOrWhiteSpace(key))
            {
                var duplicated = await _sourceRepository.GetAsync(x => x.DeviceId == device.Id && x.ExternalRecordKey == key);
                if (duplicated != null) continue;
            }
            toInsert.Add(new TaktAttendanceSource
            {
                DeviceId = device.Id,
                EmployeeId = row.EmployeeId,
                EnrollNumber = row.EnrollNumber ?? string.Empty,
                RawPunchTime = row.RawPunchTime,
                VerifyMode = row.VerifyMode,
                ExternalRecordKey = key,
                DownloadBatchNo = batchNo,
                RawPayloadJson = row.RawPayloadJson
            });
        }
        if (toInsert.Count > 0)
        {
            await _sourceRepository.CreateRangeBulkAsync(toInsert);
            result.AcceptedCount = toInsert.Count;
        }
        result.Success = true;
        return result;
    }

    public Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users)
        => _sdkClient.SyncUsersAsync(device, users);

    /// <summary>
    /// 中控设备不提供标准 HTTP Push 对接路径；此处恒返回空列表。考勤应由 TCP SDK/采集进程写入 <see cref="TaktAttendanceSource"/> 或走其他入库通道。
    /// </summary>
    public Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request)
        => Task.FromResult<IReadOnlyList<TaktAttendanceSourceIngestRowDto>>(new List<TaktAttendanceSourceIngestRowDto>());

    public Task<TaktAttendanceDeviceStatusDto> PingAsync(TaktAttendanceDevice device)
        => Task.FromResult(new TaktAttendanceDeviceStatusDto
        {
            DeviceId = device.Id,
            IsOnline = device.DeviceStatus == 1,
            Message = "中控须 TCP SDK 对接；HTTP 推送不适用。"
        });
}
