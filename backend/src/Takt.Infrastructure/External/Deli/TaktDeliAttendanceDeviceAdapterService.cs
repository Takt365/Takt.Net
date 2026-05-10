// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.Deli
// 文件名称：TaktDeliAttendanceDeviceAdapterService.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：得力设备适配器（用户同步、考勤下载并落库）。
// 协议依据：https://doc.delicloud.com/v3/integration/oa.html
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.External.Deli;

/// <summary>
/// 得力设备适配器。对接行为须与得力云《考勤数据对接协议》一致（见 https://doc.delicloud.com/v3/integration/oa.html ）。
/// </summary>
public class TaktDeliAttendanceDeviceAdapterService : ITaktDeliAttendanceDeviceAdapter
{
    private readonly TaktDeliSdkClient _sdkClient;
    private readonly ITaktRepository<TaktAttendanceSource> _sourceRepository;

    public TaktDeliAttendanceDeviceAdapterService(
        TaktDeliSdkClient sdkClient,
        ITaktRepository<TaktAttendanceSource> sourceRepository)
    {
        _sdkClient = sdkClient;
        _sourceRepository = sourceRepository;
    }

    public async Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request)
    {
        var result = new TaktAttendancePullResultDto { Success = false, AcceptedCount = 0 };
        var dl = await _sdkClient.DownloadAttendanceAsync(device, request);
        if (dl.Errors.Count > 0)
            result.Errors.AddRange(dl.Errors);
        if (!string.IsNullOrWhiteSpace(dl.UpdatedDeviceConfigJson))
            result.UpdatedDeviceConfigJson = dl.UpdatedDeviceConfigJson.Trim();
        result.Success = dl.Errors.Count == 0;
        if (dl.Rows.Count == 0)
            return result;

        var batchNo = $"deli-{DateTime.Now:yyyyMMddHHmmss}";
        var toInsert = new List<TaktAttendanceSource>(dl.Rows.Count);
        foreach (var row in dl.Rows)
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
        return result;
    }

    public Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users)
        => _sdkClient.SyncUsersAsync(device, users);

    public Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request)
        => Task.FromResult<IReadOnlyList<TaktAttendanceSourceIngestRowDto>>(new List<TaktAttendanceSourceIngestRowDto>());

    public Task<TaktAttendanceDeviceOnlineStatusDto> PingAsync(TaktAttendanceDevice device)
        => Task.FromResult(new TaktAttendanceDeviceOnlineStatusDto
        {
            DeviceId = device.Id,
            IsOnline = device.DeviceStatus == 1,
            Message = "得力云对接参见 https://doc.delicloud.com/v3/integration/oa.html"
        });
}
