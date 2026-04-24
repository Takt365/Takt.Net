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

using System.Net;
using System.Text;
using System.Text.Json;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;

namespace Takt.Infrastructure.External.ZKTeco;

/// <summary>
/// 中控对接占位客户端：标准场景应接入厂商 TCP SDK；本类 HTTP 调用不代表中控机原生 REST 能力，亦与 HTTP 推送无关。
/// </summary>
public class TaktZKTecoSdkClient
{
    private const string DefaultAttendanceEndpoint = "/api/iclock/transactions";
    private const string DefaultUserSyncEndpoint = "/api/iclock/users";

    public async Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> DownloadAttendanceAsync(
        TaktAttendanceDevice device,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var config = ParseConfig(device);
        var client = CreateHttpClient(device, config);
        var endpoint = config.AttendanceEndpoint ?? DefaultAttendanceEndpoint;
        var payload = new
        {
            start = request.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
            end = request.EndTime.ToString("yyyy-MM-dd HH:mm:ss")
        };
        var body = JsonSerializer.Serialize(payload);
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(endpoint, content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return Array.Empty<TaktAttendanceSourceIngestRowDto>();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(json);
        return ParseRows(doc.RootElement);
    }

    public async Task<int> SyncUsersAsync(
        TaktAttendanceDevice device,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken = default)
    {
        if (users == null || users.Count == 0)
            return 0;

        var config = ParseConfig(device);
        var client = CreateHttpClient(device, config);
        var endpoint = config.UserSyncEndpoint ?? DefaultUserSyncEndpoint;
        var synced = 0;

        foreach (var user in users)
        {
            var enroll = string.IsNullOrWhiteSpace(user.EnrollNumber) ? user.EmployeeId.ToString() : user.EnrollNumber.Trim();
            if (string.IsNullOrWhiteSpace(enroll))
                continue;
            var payload = new
            {
                pin = enroll,
                name = user.UserName ?? string.Empty,
                card = user.CardNo ?? string.Empty,
                enabled = user.Enabled ? 1 : 0
            };
            var body = JsonSerializer.Serialize(payload);
            using var content = new StringContent(body, Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(endpoint, content, cancellationToken);
            if (response.IsSuccessStatusCode)
                synced++;
        }
        return synced;
    }

    private static IReadOnlyList<TaktAttendanceSourceIngestRowDto> ParseRows(JsonElement root)
    {
        var list = new List<TaktAttendanceSourceIngestRowDto>();
        if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in root.EnumerateArray())
                AddRow(list, item);
        }
        else if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in data.EnumerateArray())
                AddRow(list, item);
        }
        return list;
    }

    private static void AddRow(List<TaktAttendanceSourceIngestRowDto> rows, JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Object)
            return;
        var enroll = ReadString(item, "pin") ?? ReadString(item, "enrollNumber") ?? string.Empty;
        var key = ReadString(item, "id") ?? ReadString(item, "recordId") ?? Guid.NewGuid().ToString("N");
        var punchText = ReadString(item, "punchTime") ?? ReadString(item, "checkTime") ?? string.Empty;
        var verifyMode = ReadInt(item, "verifyMode") ?? ReadInt(item, "verify") ?? 0;
        var punchTime = DateTime.Now;
        if (!string.IsNullOrWhiteSpace(punchText) && DateTime.TryParse(punchText, out var dt))
            punchTime = dt;
        rows.Add(new TaktAttendanceSourceIngestRowDto
        {
            EmployeeId = 0,
            EnrollNumber = enroll,
            RawPunchTime = punchTime,
            VerifyMode = verifyMode,
            ExternalRecordKey = key,
            RawPayloadJson = item.GetRawText()
        });
    }

    private static string? ReadString(JsonElement obj, string key)
        => obj.TryGetProperty(key, out var p) && p.ValueKind != JsonValueKind.Null ? p.ToString() : null;

    private static int? ReadInt(JsonElement obj, string key)
    {
        if (!obj.TryGetProperty(key, out var p)) return null;
        if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var i)) return i;
        return int.TryParse(p.ToString(), out var parsed) ? parsed : null;
    }

    private static HttpClient CreateHttpClient(TaktAttendanceDevice device, ZKTecoConfig config)
    {
        var handler = new HttpClientHandler();
        if (!string.IsNullOrWhiteSpace(config.UserName) && !string.IsNullOrWhiteSpace(config.Password))
        {
            handler.Credentials = new NetworkCredential(config.UserName, config.Password);
            handler.PreAuthenticate = true;
        }
        var protocol = string.IsNullOrWhiteSpace(config.Protocol) ? "http" : config.Protocol.Trim().ToLowerInvariant();
        var host = string.IsNullOrWhiteSpace(device.IpAddress) ? "127.0.0.1" : device.IpAddress.Trim();
        var port = device.Port.GetValueOrDefault(protocol == "https" ? 443 : 80);
        return new HttpClient(handler)
        {
            BaseAddress = new Uri($"{protocol}://{host}:{port}", UriKind.Absolute),
            Timeout = TimeSpan.FromSeconds(Math.Clamp(config.TimeoutSeconds, 3, 120))
        };
    }

    private static ZKTecoConfig ParseConfig(TaktAttendanceDevice device)
    {
        var config = new ZKTecoConfig();
        if (string.IsNullOrWhiteSpace(device.ConfigJson))
            return config;
        try
        {
            using var doc = JsonDocument.Parse(device.ConfigJson);
            var root = doc.RootElement;
            config.Protocol = ReadString(root, "protocol") ?? config.Protocol;
            config.UserName = ReadString(root, "userName") ?? ReadString(root, "username");
            config.Password = ReadString(root, "password") ?? device.ApiSecret;
            config.AttendanceEndpoint = ReadString(root, "attendanceEndpoint");
            config.UserSyncEndpoint = ReadString(root, "userSyncEndpoint");
            config.TimeoutSeconds = ReadInt(root, "timeoutSeconds") ?? config.TimeoutSeconds;
        }
        catch
        {
            // ignore
        }
        return config;
    }

    private sealed class ZKTecoConfig
    {
        public string Protocol { get; set; } = "http";
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? AttendanceEndpoint { get; set; }
        public string? UserSyncEndpoint { get; set; }
        public int TimeoutSeconds { get; set; } = 20;
    }
}
