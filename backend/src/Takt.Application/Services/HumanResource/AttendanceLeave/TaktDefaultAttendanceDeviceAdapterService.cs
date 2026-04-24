// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.HumanResource.AttendanceLeave
// 文件名称：TaktDefaultAttendanceDeviceAdapterService.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：默认设备适配器（第一版，解析统一 JSON 推送）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Text.Json;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Application.Services.HumanResource.AttendanceLeave;

/// <summary>
/// 默认设备适配器（第一版）
/// </summary>
public class TaktDefaultAttendanceDeviceAdapterService : ITaktAttendanceDeviceAdapter
{
    /// <inheritdoc />
    public Task<TaktAttendancePullResultDto> PullAsync(TaktAttendanceDevice device, TaktAttendancePullRequestDto request)
    {
        return Task.FromResult(new TaktAttendancePullResultDto
        {
            Success = true,
            AcceptedCount = 0
        });
    }

    /// <inheritdoc />
    public Task<int> SyncUsersAsync(TaktAttendanceDevice device, IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users)
    {
        return Task.FromResult(0);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> ParsePushAsync(TaktAttendanceDevice device, TaktAttendancePushRequestDto request)
    {
        var result = new List<TaktAttendanceSourceIngestRowDto>();
        if (string.IsNullOrWhiteSpace(request.RawPayloadJson))
            return Task.FromResult<IReadOnlyList<TaktAttendanceSourceIngestRowDto>>(result);

        using var doc = JsonDocument.Parse(request.RawPayloadJson);
        if (doc.RootElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.Object)
                    result.Add(ToRow(item));
            }
        }
        else if (doc.RootElement.ValueKind == JsonValueKind.Object)
        {
            if (doc.RootElement.TryGetProperty("records", out var records) && records.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in records.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                        result.Add(ToRow(item));
                }
            }
            else if (doc.RootElement.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in data.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                        result.Add(ToRow(item));
                }
            }
            else
            {
                result.Add(ToRow(doc.RootElement));
            }
        }

        return Task.FromResult<IReadOnlyList<TaktAttendanceSourceIngestRowDto>>(result);
    }

    /// <inheritdoc />
    public Task<TaktAttendanceDeviceOnlineStatusDto> PingAsync(TaktAttendanceDevice device)
    {
        return Task.FromResult(new TaktAttendanceDeviceOnlineStatusDto
        {
            DeviceId = device.Id,
            IsOnline = device.DeviceStatus == 1,
            Message = "Default adapter status."
        });
    }

    private static TaktAttendanceSourceIngestRowDto ToRow(JsonElement obj)
    {
        long employeeId = 0;
        int verifyMode = 0;
        string enrollNumber = string.Empty;
        string? externalRecordKey = null;
        DateTime rawPunchTime = DateTime.Now;

        if (obj.TryGetProperty("employeeId", out var employeeIdElement) && employeeIdElement.TryGetInt64(out var eid))
            employeeId = eid;
        if (obj.TryGetProperty("verifyMode", out var verifyModeElement) && verifyModeElement.TryGetInt32(out var vm))
            verifyMode = vm;
        if (obj.TryGetProperty("enrollNumber", out var enrollElement))
            enrollNumber = enrollElement.GetString() ?? string.Empty;
        if (obj.TryGetProperty("externalRecordKey", out var keyElement))
            externalRecordKey = keyElement.GetString();
        if (obj.TryGetProperty("rawPunchTime", out var punchElement) && punchElement.ValueKind == JsonValueKind.String)
        {
            var text = punchElement.GetString();
            if (!string.IsNullOrWhiteSpace(text) && DateTime.TryParse(text, out var parsed))
                rawPunchTime = parsed;
        }
        else if (obj.TryGetProperty("punchTime", out var p2Element) && p2Element.ValueKind == JsonValueKind.String)
        {
            var text = p2Element.GetString();
            if (!string.IsNullOrWhiteSpace(text) && DateTime.TryParse(text, out var parsed))
                rawPunchTime = parsed;
        }

        return new TaktAttendanceSourceIngestRowDto
        {
            EmployeeId = employeeId,
            EnrollNumber = enrollNumber,
            RawPunchTime = rawPunchTime,
            VerifyMode = verifyMode,
            ExternalRecordKey = externalRecordKey,
            RawPayloadJson = obj.GetRawText()
        };
    }
}
