// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCollectorService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备采集网关服务（第一版骨架实现）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Shared.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Concurrent;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设备采集网关服务（第一版骨架）
/// </summary>
public class TaktAttendanceCollectorService : TaktServiceBase, ITaktAttendanceCollector
{
    private static readonly ConcurrentDictionary<string, DateTime> NonceCache = new();
    private static readonly TimeSpan NonceTtl = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan TimestampSkew = TimeSpan.FromMinutes(5);

    private readonly ITaktRepository<TaktAttendanceDevice> _deviceRepository;
    private readonly ITaktRepository<TaktAttendanceSource> _sourceRepository;
    private readonly ITaktAttendanceDeviceAdapterFactory _adapterFactory;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktAttendanceCollectorService(
        ITaktRepository<TaktAttendanceDevice> deviceRepository,
        ITaktRepository<TaktAttendanceSource> sourceRepository,
        ITaktAttendanceDeviceAdapterFactory adapterFactory,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _deviceRepository = deviceRepository;
        _sourceRepository = sourceRepository;
        _adapterFactory = adapterFactory;
    }

    /// <inheritdoc />
    public Task<TaktAttendancePullResultDto> PullAttendanceRecordsAsync(TaktAttendancePullRequestDto request)
    {
        if (request == null || request.DeviceId <= 0)
        {
            return Task.FromResult(new TaktAttendancePullResultDto
            {
                Success = false,
                AcceptedCount = 0,
                Errors = new List<string> { "deviceId is required." }
            });
        }

        return PullInternalAsync(request);
    }

    private async Task<TaktAttendancePullResultDto> PullInternalAsync(TaktAttendancePullRequestDto request)
    {
        var device = await _deviceRepository.GetByIdAsync(request.DeviceId);
        if (device == null || device.IsDeleted != 0)
        {
            return new TaktAttendancePullResultDto
            {
                Success = false,
                AcceptedCount = 0,
                Errors = new List<string> { "Device not found." }
            };
        }

        var adapter = _adapterFactory.CreateAdapter(device);
        var result = await adapter.PullAsync(device, request);
        if (!string.IsNullOrWhiteSpace(result.UpdatedDeviceConfigJson))
            device.ConfigJson = result.UpdatedDeviceConfigJson.Trim();
        if (result.Success)
            device.LastPullAt = DateTime.Now;
        if (result.Success || !string.IsNullOrWhiteSpace(result.UpdatedDeviceConfigJson))
            await _deviceRepository.UpdateAsync(device);
        return result;
    }

    /// <inheritdoc />
    public async Task<TaktAttendancePushHandleResultDto> ReceiveAttendancePushAsync(TaktAttendancePushRequestDto request)
    {
        var result = new TaktAttendancePushHandleResultDto { Success = false, AcceptedCount = 0 };
        if (string.IsNullOrWhiteSpace(request.DeviceCode))
        {
            result.Errors.Add("deviceCode is required.");
            return result;
        }

        var deviceCode = request.DeviceCode.Trim();
        var device = await _deviceRepository.GetAsync(x => x.DeviceCode == deviceCode);
        if (device == null)
        {
            result.Errors.Add("Device not found.");
            return result;
        }

        if (device.IsPushEnabled != 1)
        {
            result.Errors.Add("Device push is disabled.");
            return result;
        }

        if (!ValidateSignature(device, request))
        {
            result.Errors.Add("Invalid signature.");
            return result;
        }

        var adapter = _adapterFactory.CreateAdapter(device);
        var rows = await adapter.ParsePushAsync(device, request);
        if (rows.Count == 0)
        {
            result.Success = true;
            return result;
        }

        var toInsert = new List<TaktAttendanceSource>();
        var batchNo = $"push-{DateTime.Now:yyyyMMddHHmmss}";
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
                RawPayloadJson = row.RawPayloadJson ?? request.RawPayloadJson
            });
        }

        if (toInsert.Count > 0)
        {
            await _sourceRepository.CreateRangeBulkAsync(toInsert);
            result.AcceptedCount = toInsert.Count;
            device.LastPushAt = DateTime.Now;
            await _deviceRepository.UpdateAsync(device);
        }
        result.Success = true;
        return result;
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceDeviceStatusDto> GetAttendanceDeviceStatusAsync(long deviceId)
    {
        var device = await _deviceRepository.GetByIdAsync(deviceId);
        if (device == null)
            throw new TaktBusinessException("validation.attendanceDeviceNotFound");
        var adapter = _adapterFactory.CreateAdapter(device);
        return await adapter.PingAsync(device);
    }

    /// <inheritdoc />
    public async Task<TaktAttendanceUserSyncResultDto> SyncDeviceUsersAsync(long deviceId, TaktAttendanceUserSyncRequestDto request)
    {
        var result = new TaktAttendanceUserSyncResultDto { Success = false, SyncedCount = 0 };
        if (deviceId <= 0)
        {
            result.Errors.Add("deviceId is required.");
            return result;
        }

        var users = request?.Users ?? new List<TaktAttendanceDeviceUserSyncItemDto>();
        if (users.Count == 0)
        {
            result.Success = true;
            return result;
        }

        var device = await _deviceRepository.GetByIdAsync(deviceId);
        if (device == null || device.IsDeleted != 0)
        {
            result.Errors.Add("Device not found.");
            return result;
        }

        var adapter = _adapterFactory.CreateAdapter(device);
        var syncedCount = await adapter.SyncUsersAsync(device, users);
        result.SyncedCount = syncedCount;
        result.Success = true;
        return result;
    }

    private static bool ValidateSignature(TaktAttendanceDevice device, TaktAttendancePushRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Signature)) return true;
        if (string.IsNullOrWhiteSpace(device.ApiSecret)) return false;
        if (!request.Timestamp.HasValue) return false;

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (Math.Abs(now - request.Timestamp.Value) > TimestampSkew.TotalSeconds) return false;

        var nonce = request.DeviceCode + ":" + request.Timestamp.Value + ":" + request.Signature.Trim();
        CleanupNonce();
        if (!NonceCache.TryAdd(nonce, DateTime.UtcNow)) return false;

        var payload = $"{request.DeviceCode}:{request.Timestamp.Value}:{request.RawPayloadJson}:{device.ApiSecret}";
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(payload));
        var expected = Convert.ToHexString(bytes);
        return string.Equals(expected, request.Signature.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    private static void CleanupNonce()
    {
        var expire = DateTime.UtcNow - NonceTtl;
        foreach (var pair in NonceCache)
        {
            if (pair.Value < expire)
                NonceCache.TryRemove(pair.Key, out _);
        }
    }
}
