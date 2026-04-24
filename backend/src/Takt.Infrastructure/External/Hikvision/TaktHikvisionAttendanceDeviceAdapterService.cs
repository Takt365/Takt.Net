// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.External.Hikvision
// 文件名称：TaktHikvisionAttendanceDeviceAdapterService.cs
// 创建时间：2026-04-15
// 创建人：Takt365(Cursor AI)
// 功能描述：海康设备适配器（用户同步、考勤下载并落库）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.External.Hikvision;

/// <summary>
/// 海康设备适配器
/// </summary>
public class TaktHikvisionAttendanceDeviceAdapterService : ITaktHikvisionAttendanceDeviceAdapter
{
    private readonly TaktHikvisionSdkClient _sdkClient;
    private readonly ITaktRepository<TaktAttendanceSource> _sourceRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sdkClient">海康 SDK 客户端</param>
    /// <param name="sourceRepository">考勤源记录仓储</param>
    public TaktHikvisionAttendanceDeviceAdapterService(
        TaktHikvisionSdkClient sdkClient,
        ITaktRepository<TaktAttendanceSource> sourceRepository)
    {
        _sdkClient = sdkClient;
        _sourceRepository = sourceRepository;
    }

    /// <inheritdoc />
    public async Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request)
    {
        var result = new TaktAttendancePullResultDto { Success = false, AcceptedCount = 0 };
        if (device == null)
        {
            result.Errors.Add("device is required.");
            return result;
        }

        try
        {
            var rows = await _sdkClient.DownloadAttendanceAsync(device, request);
            if (rows.Count == 0)
            {
                result.Success = true;
                return result;
            }

            var batchNo = $"hik-{DateTime.Now:yyyyMMddHHmmss}";
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
        catch (Exception ex)
        {
            result.Errors.Add(ex.Message);
            return result;
        }
    }

    /// <inheritdoc />
    public Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users)
    {
        return _sdkClient.SyncUsersAsync(device, users);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request)
    {
        // 海康通常使用拉取（Pull）模式；推送解析沿用默认采集入口，当前适配器不在此实现额外解析。
        return Task.FromResult<IReadOnlyList<TaktAttendanceSourceIngestRowDto>>(new List<TaktAttendanceSourceIngestRowDto>());
    }

    /// <inheritdoc />
    public Task<TaktAttendanceDeviceOnlineStatusDto> PingAsync(TaktAttendanceDevice device)
    {
        return Task.FromResult(new TaktAttendanceDeviceOnlineStatusDto
        {
            DeviceId = device.Id,
            IsOnline = device.DeviceStatus == 1,
            Message = "Hikvision adapter status."
        });
    }
}
