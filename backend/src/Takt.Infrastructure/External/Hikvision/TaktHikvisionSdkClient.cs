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

using System.Net;
using System.Text;
using System.Text.Json;
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
    /// 下载考勤事件（分页）
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
        var config = ParseConfig(device);
        var client = CreateHttpClient(device, config);

        var rows = new List<TaktAttendanceSourceIngestRowDto>();
        var searchPosition = 0;
        var maxResults = Math.Clamp(config.PageSize, 1, 500);

        for (var pageIndex = 0; pageIndex < config.MaxPages; pageIndex++)
        {
            var endpoint = config.AttendanceSearchEndpoint ?? DefaultAttendanceSearchEndpoint;
            var requestBody = new
            {
                AcsEventCond = new
                {
                    searchID = $"takt-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",
                    searchResultPosition = searchPosition,
                    maxResults,
                    startTime = request.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    endTime = request.EndTime.ToString("yyyy-MM-ddTHH:mm:ss")
                }
            };

            var body = JsonSerializer.Serialize(requestBody);
            using var content = new StringContent(body, Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(endpoint, content, cancellationToken);
            if (!response.IsSuccessStatusCode)
                break;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(json);
            var pageRows = ParseAttendanceRows(doc.RootElement);
            if (pageRows.Count == 0)
                break;

            rows.AddRange(pageRows);
            if (pageRows.Count < maxResults)
                break;

            searchPosition += pageRows.Count;
        }

        return rows;
    }

    /// <summary>
    /// 同步用户信息到海康设备
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
        if (users == null || users.Count == 0)
            return 0;

        var config = ParseConfig(device);
        var client = CreateHttpClient(device, config);
        var endpoint = config.UserSyncEndpoint ?? DefaultUserSyncEndpoint;
        var synced = 0;

        foreach (var user in users)
        {
            var employeeNo = string.IsNullOrWhiteSpace(user.EnrollNumber)
                ? user.EmployeeId.ToString()
                : user.EnrollNumber.Trim();
            if (string.IsNullOrWhiteSpace(employeeNo))
                continue;

            var payload = new
            {
                UserInfo = new
                {
                    employeeNo,
                    name = user.UserName ?? string.Empty,
                    userType = "normal",
                    Valid = new
                    {
                        enable = user.Enabled,
                        beginTime = "2000-01-01T00:00:00",
                        endTime = "2099-12-31T23:59:59"
                    },
                    certificateType = 1,
                    certificateNo = user.CertificateNo ?? string.Empty,
                    doorRight = "1",
                    RightPlan = new[]
                    {
                        new { doorNo = 1, planTemplateNo = "1" }
                    },
                    localUIRight = true,
                    maxOpenDoorTime = 0,
                    openDelayEnabled = false,
                    cardNo = user.CardNo ?? string.Empty
                }
            };

            var body = JsonSerializer.Serialize(payload);
            using var content = new StringContent(body, Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(endpoint, content, cancellationToken);
            if (response.IsSuccessStatusCode)
                synced++;
        }

        return synced;
    }

    private static List<TaktAttendanceSourceIngestRowDto> ParseAttendanceRows(JsonElement root)
    {
        var result = new List<TaktAttendanceSourceIngestRowDto>();
        if (root.ValueKind != JsonValueKind.Object)
            return result;

        if (root.TryGetProperty("AcsEvent", out var acsEvent) && acsEvent.ValueKind == JsonValueKind.Array)
        {
            foreach (var eventItem in acsEvent.EnumerateArray())
                AddAttendanceRow(result, eventItem);
            return result;
        }

        if (root.TryGetProperty("InfoList", out var infoList) && infoList.ValueKind == JsonValueKind.Array)
        {
            foreach (var eventItem in infoList.EnumerateArray())
                AddAttendanceRow(result, eventItem);
            return result;
        }

        if (root.TryGetProperty("AcsEventInfoList", out var infoObj) && infoObj.ValueKind == JsonValueKind.Object)
        {
            if (infoObj.TryGetProperty("AcsEventInfo", out var arr) && arr.ValueKind == JsonValueKind.Array)
            {
                foreach (var eventItem in arr.EnumerateArray())
                    AddAttendanceRow(result, eventItem);
            }
            return result;
        }

        return result;
    }

    private static void AddAttendanceRow(List<TaktAttendanceSourceIngestRowDto> rows, JsonElement eventItem)
    {
        if (eventItem.ValueKind != JsonValueKind.Object)
            return;

        var enrollNumber = ReadString(eventItem, "employeeNo")
                           ?? ReadString(eventItem, "employeeNoString")
                           ?? ReadString(eventItem, "cardNo")
                           ?? string.Empty;
        var eventSerialNo = ReadString(eventItem, "serialNo")
                            ?? ReadString(eventItem, "eventId")
                            ?? ReadString(eventItem, "major")
                            ?? Guid.NewGuid().ToString("N");
        var verifyMode = ReadInt(eventItem, "currentVerifyMode")
                         ?? ReadInt(eventItem, "verifyMode")
                         ?? 0;
        var eventTimeText = ReadString(eventItem, "time")
                            ?? ReadString(eventItem, "eventTime")
                            ?? ReadString(eventItem, "captureTime")
                            ?? string.Empty;
        var rawPunchTime = DateTime.Now;
        if (!string.IsNullOrWhiteSpace(eventTimeText) && DateTime.TryParse(eventTimeText, out var parsed))
            rawPunchTime = parsed;

        rows.Add(new TaktAttendanceSourceIngestRowDto
        {
            EmployeeId = 0,
            EnrollNumber = enrollNumber,
            RawPunchTime = rawPunchTime,
            VerifyMode = verifyMode,
            ExternalRecordKey = eventSerialNo,
            RawPayloadJson = eventItem.GetRawText()
        });
    }

    private static string? ReadString(JsonElement obj, string propertyName)
    {
        return obj.TryGetProperty(propertyName, out var p) && p.ValueKind != JsonValueKind.Null
            ? p.ToString()
            : null;
    }

    private static int? ReadInt(JsonElement obj, string propertyName)
    {
        if (!obj.TryGetProperty(propertyName, out var p))
            return null;
        if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var i))
            return i;
        return int.TryParse(p.ToString(), out var parsed) ? parsed : null;
    }

    private static HttpClient CreateHttpClient(TaktAttendanceDevice device, HikvisionConfig config)
    {
        var timeoutSeconds = Math.Clamp(config.TimeoutSeconds, 3, 120);
        var handler = new HttpClientHandler();

        if (!string.IsNullOrWhiteSpace(config.UserName) && !string.IsNullOrWhiteSpace(config.Password))
        {
            handler.Credentials = new NetworkCredential(config.UserName, config.Password);
            handler.PreAuthenticate = true;
        }

        var protocol = string.IsNullOrWhiteSpace(config.Protocol) ? "http" : config.Protocol.Trim().ToLowerInvariant();
        var host = string.IsNullOrWhiteSpace(device.IpAddress) ? "127.0.0.1" : device.IpAddress.Trim();
        var port = device.Port.GetValueOrDefault(protocol == "https" ? 443 : 80);
        var baseUri = $"{protocol}://{host}:{port}";

        return new HttpClient(handler)
        {
            BaseAddress = new Uri(baseUri, UriKind.Absolute),
            Timeout = TimeSpan.FromSeconds(timeoutSeconds)
        };
    }

    private static HikvisionConfig ParseConfig(TaktAttendanceDevice device)
    {
        var config = new HikvisionConfig();
        if (string.IsNullOrWhiteSpace(device.ConfigJson))
            return config;

        try
        {
            using var doc = JsonDocument.Parse(device.ConfigJson);
            var root = doc.RootElement;
            config.Protocol = ReadString(root, "protocol") ?? config.Protocol;
            config.UserName = ReadString(root, "userName") ?? ReadString(root, "username");
            config.Password = ReadString(root, "password") ?? device.ApiSecret;
            config.AttendanceSearchEndpoint = ReadString(root, "attendanceSearchEndpoint");
            config.UserSyncEndpoint = ReadString(root, "userSyncEndpoint");
            config.TimeoutSeconds = ReadInt(root, "timeoutSeconds") ?? config.TimeoutSeconds;
            config.PageSize = ReadInt(root, "pageSize") ?? config.PageSize;
            config.MaxPages = ReadInt(root, "maxPages") ?? config.MaxPages;
        }
        catch
        {
            // 配置不合法时使用默认值
        }

        return config;
    }

    private sealed class HikvisionConfig
    {
        public string Protocol { get; set; } = "http";
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? AttendanceSearchEndpoint { get; set; }
        public string? UserSyncEndpoint { get; set; }
        public int TimeoutSeconds { get; set; } = 20;
        public int PageSize { get; set; } = 100;
        public int MaxPages { get; set; } = 100;
    }
}
