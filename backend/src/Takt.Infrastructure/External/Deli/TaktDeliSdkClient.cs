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

using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
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

    private const string DefaultAttendanceEndpoint = "/api/attendance/records";
    private const string DefaultUserSyncEndpoint = "/api/attendance/users/sync";

    private static readonly JsonSerializerOptions SnakeJson = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// 拉取得力云考勤原始记录（综合签到 checkin_query），并返回更新后的设备 ConfigJson（含 deliNextId、deliInitDone）。
    /// 注意：协议在初始化后仅同步「新产生」数据，与 <see cref="TaktAttendancePullRequestDto"/> 的时间窗无对应关系，时间参数对云模式忽略。
    /// </summary>
    public async Task<TaktDeliAttendanceDownloadResult> DownloadAttendanceAsync(
        TaktAttendanceDevice device,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken = default)
    {
        _ = request;
        var config = ParseConfig(device);
        if (IsCloudConfigured(config))
            return await DownloadAttendanceCloudAsync(device, config, cancellationToken).ConfigureAwait(false);

        return new TaktDeliAttendanceDownloadResult
        {
            Rows = await DownloadAttendanceLegacyAsync(device, config, request, cancellationToken).ConfigureAwait(false)
        };
    }

    /// <summary>
    /// 同步员工至得力云（/v2.0/employee）。须配置部门外部 id（deliDefaultDepartmentExtId）等，参见官方文档。
    /// </summary>
    public async Task<int> SyncUsersAsync(
        TaktAttendanceDevice device,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken = default)
    {
        if (users == null || users.Count == 0)
            return 0;

        var config = ParseConfig(device);
        if (IsCloudConfigured(config))
            return await SyncUsersCloudAsync(device, config, users, cancellationToken).ConfigureAwait(false);

        return await SyncUsersLegacyAsync(device, config, users, cancellationToken).ConfigureAwait(false);
    }

    private static bool IsCloudConfigured(DeliConfig c)
        => !string.IsNullOrWhiteSpace(c.AppKey) && !string.IsNullOrWhiteSpace(c.AppSecret);

    private static async Task<TaktDeliAttendanceDownloadResult> DownloadAttendanceCloudAsync(
        TaktAttendanceDevice device,
        DeliConfig config,
        CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var rows = new List<TaktAttendanceSourceIngestRowDto>();
        var initDone = config.DeliInitDone;
        long nextId = config.DeliNextId;
        var pageSize = Math.Clamp(config.PageSize, 1, 500);

        using var client = CreateCloudHttpClient(config);

        if (!initDone)
        {
            var (codeInit, msgInit, _) = await PostCloudSignedAsync(
                client, PathCloudAppApi, "{}", HeaderApiModule, CmdCheckinInit, config, cancellationToken).ConfigureAwait(false);
            if (codeInit == 0)
                initDone = true;
            else
                errors.Add($"得力考勤初始化 checkin_query_init 失败: code={codeInit}, msg={msgInit}");
        }

        if (!initDone)
        {
            return new TaktDeliAttendanceDownloadResult
            {
                Rows = Array.Empty<TaktAttendanceSourceIngestRowDto>(),
                Errors = errors,
                UpdatedDeviceConfigJson = MergeDeliVendorState(device.ConfigJson, nextId, false)
            };
        }

        const int maxPages = 80;
        for (var page = 0; page < maxPages; page++)
        {
            var body = new JsonObject { ["next_id"] = nextId, ["page_size"] = pageSize }.ToJsonString();
            var (code, msg, dataEl) = await PostCloudSignedAsync(
                client, PathCloudAppApi, body, HeaderApiModule, CmdCheckinQuery, config, cancellationToken).ConfigureAwait(false);
            if (code != 0)
            {
                errors.Add($"得力考勤同步 checkin_query 失败: code={code}, msg={msg}");
                break;
            }

            if (dataEl == null || dataEl.Value.ValueKind != JsonValueKind.Object)
                break;

            var dataObj = dataEl.Value;
            if (dataObj.TryGetProperty("next_id", out var nextEl) && nextEl.ValueKind == JsonValueKind.Number && nextEl.TryGetInt64(out var nxt))
                nextId = nxt;

            if (!dataObj.TryGetProperty("data", out var arrEl) || arrEl.ValueKind != JsonValueKind.Array || arrEl.GetArrayLength() == 0)
                break;

            foreach (var item in arrEl.EnumerateArray())
                AddCheckinRow(rows, item);
        }

        return new TaktDeliAttendanceDownloadResult
        {
            Rows = rows,
            Errors = errors,
            UpdatedDeviceConfigJson = MergeDeliVendorState(device.ConfigJson, nextId, initDone)
        };
    }

    private static void AddCheckinRow(List<TaktAttendanceSourceIngestRowDto> rows, JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Object)
            return;

        var extId = ReadString(item, "ext_id") ?? ReadString(item, "user_id") ?? string.Empty;
        var idVal = ReadString(item, "id") ?? Guid.NewGuid().ToString("N");
        var externalKey = $"deli-{idVal}";
        var checkType = ReadString(item, "check_type") ?? string.Empty;
        var verifyMode = checkType.GetHashCode(StringComparison.Ordinal);

        DateTime punchTime = DateTime.UtcNow;
        if (item.TryGetProperty("check_time", out var ct))
        {
            if (ct.ValueKind == JsonValueKind.Number && ct.TryGetInt64(out var unix))
                punchTime = DateTimeOffset.FromUnixTimeSeconds(unix).LocalDateTime;
            else if (ct.ValueKind == JsonValueKind.String && long.TryParse(ct.GetString(), out var u2))
                punchTime = DateTimeOffset.FromUnixTimeSeconds(u2).LocalDateTime;
        }

        rows.Add(new TaktAttendanceSourceIngestRowDto
        {
            EmployeeId = 0,
            EnrollNumber = extId,
            RawPunchTime = punchTime,
            VerifyMode = verifyMode,
            ExternalRecordKey = externalKey,
            RawPayloadJson = item.GetRawText()
        });
    }

    private static async Task<(int Code, string Msg, JsonElement? Data)> PostCloudSignedAsync(
        HttpClient client,
        string path,
        string bodyJson,
        string apiModule,
        string apiCmd,
        DeliConfig config,
        CancellationToken cancellationToken)
    {
        var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(CultureInvariant);
        var sig = ComputeSignature(path, ts, config.AppKey!, config.AppSecret!);
        using var req = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
        };
        req.Headers.TryAddWithoutValidation("App-Key", config.AppKey);
        req.Headers.TryAddWithoutValidation("App-Timestamp", ts);
        req.Headers.TryAddWithoutValidation("App-Sig", sig);
        req.Headers.TryAddWithoutValidation("Api-Module", apiModule);
        req.Headers.TryAddWithoutValidation("Api-Cmd", apiCmd);

        using var resp = await client.SendAsync(req, cancellationToken).ConfigureAwait(false);
        var text = await resp.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            using var doc = JsonDocument.Parse(text);
            var root = doc.RootElement;
            var code = root.TryGetProperty("code", out var c) && c.ValueKind == JsonValueKind.Number && c.TryGetInt32(out var ci) ? ci : -1;
            var msg = root.TryGetProperty("msg", out var m) ? m.ToString() : string.Empty;
            if (root.TryGetProperty("data", out var d) && d.ValueKind != JsonValueKind.Null && d.ValueKind != JsonValueKind.Undefined)
                return (code, msg, d.Clone());
            return (code, msg, null);
        }
        catch
        {
            var snippet = text.Length > 200 ? text[..200] : text;
            return (-1, snippet, default(JsonElement?));
        }
    }

    private static async Task<(int Code, string Msg, JsonElement? Data)> PostEmployeeSignedAsync(
        HttpClient client,
        string bodyJson,
        DeliConfig config,
        CancellationToken cancellationToken)
    {
        var path = PathEmployee;
        var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(CultureInvariant);
        var sig = ComputeSignature(path, ts, config.AppKey!, config.AppSecret!);
        using var req = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = new StringContent(bodyJson, Encoding.UTF8, "application/json")
        };
        req.Headers.TryAddWithoutValidation("App-Key", config.AppKey);
        req.Headers.TryAddWithoutValidation("App-Timestamp", ts);
        req.Headers.TryAddWithoutValidation("App-Sig", sig);

        using var resp = await client.SendAsync(req, cancellationToken).ConfigureAwait(false);
        var text = await resp.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            using var doc = JsonDocument.Parse(text);
            var root = doc.RootElement;
            var code = root.TryGetProperty("code", out var c) && c.ValueKind == JsonValueKind.Number && c.TryGetInt32(out var ci) ? ci : -1;
            var msg = root.TryGetProperty("msg", out var m) ? m.ToString() : string.Empty;
            if (root.TryGetProperty("data", out var d) && d.ValueKind != JsonValueKind.Null && d.ValueKind != JsonValueKind.Undefined)
                return (code, msg, d.Clone());
            return (code, msg, null);
        }
        catch
        {
            var snippet = text.Length > 200 ? text[..200] : text;
            return (-1, snippet, default(JsonElement?));
        }
    }

    private static string ComputeSignature(string path, string timestampMs, string appKey, string appSecret)
    {
        var plain = $"{path}{timestampMs}{appKey}{appSecret}";
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(plain));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }

    private static HttpClient CreateCloudHttpClient(DeliConfig config)
    {
        var baseUri = string.IsNullOrWhiteSpace(config.CloudApiBase)
            ? DeliCloudApiBase
            : config.CloudApiBase!.TrimEnd('/');
        return new HttpClient
        {
            BaseAddress = new Uri(baseUri + "/", UriKind.Absolute),
            Timeout = TimeSpan.FromSeconds(Math.Clamp(config.TimeoutSeconds, 3, 120))
        };
    }

    private static async Task<int> SyncUsersCloudAsync(
        TaktAttendanceDevice _,
        DeliConfig config,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken)
    {
        using var client = CreateCloudHttpClient(config);
        var deptExt = string.IsNullOrWhiteSpace(config.DefaultDepartmentExtId) ? "1" : config.DefaultDepartmentExtId.Trim();
        var synced = 0;

        foreach (var user in users)
        {
            if (!user.Enabled)
                continue;
            var extId = string.IsNullOrWhiteSpace(user.EnrollNumber)
                ? user.EmployeeId.ToString(CultureInvariant)
                : user.EnrollNumber.Trim();
            if (string.IsNullOrWhiteSpace(extId) || extId.Length > 32)
                continue;

            var mobile = BuildMobile(user);
            var name = string.IsNullOrWhiteSpace(user.UserName) ? extId : user.UserName.Trim();
            if (name.Length > 30)
                name = name[..30];

            var employeeNum = string.IsNullOrWhiteSpace(user.EnrollNumber)
                ? user.EmployeeId.ToString(CultureInvariant)
                : user.EnrollNumber.Trim();
            if (employeeNum.Length > 20)
                employeeNum = employeeNum[..20];

            var payload = new
            {
                employee_ext_id = extId,
                name,
                mobile,
                employee_num = employeeNum,
                department_infos = new[]
                {
                    new { ext_id = deptExt, title = string.IsNullOrWhiteSpace(config.DefaultDepartmentTitle) ? "员工" : config.DefaultDepartmentTitle }
                }
            };
            var body = JsonSerializer.Serialize(payload, SnakeJson);
            var (code, _, _) = await PostEmployeeSignedAsync(client, body, config, cancellationToken).ConfigureAwait(false);
            if (code == 0)
                synced++;
        }

        return synced;
    }

    private static string BuildMobile(TaktAttendanceDeviceUserSyncItemDto user)
    {
        var m = user.Mobile?.Trim();
        if (!string.IsNullOrEmpty(m) && m.Length >= 11)
            return m[..11];
        var tail = (uint)(Math.Abs(user.EmployeeId) % 100000000);
        return $"139{tail:D8}";
    }

    private static async Task<IReadOnlyList<TaktAttendanceSourceIngestRowDto>> DownloadAttendanceLegacyAsync(
        TaktAttendanceDevice device,
        DeliConfig config,
        TaktAttendancePullRequestDto request,
        CancellationToken cancellationToken)
    {
        using var client = CreateLegacyHttpClient(device, config);
        var endpoint = config.AttendanceEndpoint ?? DefaultAttendanceEndpoint;
        var payload = new
        {
            startTime = request.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
            endTime = request.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
            pageIndex = 1,
            pageSize = Math.Clamp(config.PageSize, 1, 500)
        };

        var body = JsonSerializer.Serialize(payload);
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            return Array.Empty<TaktAttendanceSourceIngestRowDto>();

        var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        using var doc = JsonDocument.Parse(json);
        return ParseRows(doc.RootElement);
    }

    private static async Task<int> SyncUsersLegacyAsync(
        TaktAttendanceDevice device,
        DeliConfig config,
        IReadOnlyList<TaktAttendanceDeviceUserSyncItemDto> users,
        CancellationToken cancellationToken)
    {
        using var client = CreateLegacyHttpClient(device, config);
        var endpoint = config.UserSyncEndpoint ?? DefaultUserSyncEndpoint;
        var synced = 0;

        foreach (var user in users)
        {
            var userCode = string.IsNullOrWhiteSpace(user.EnrollNumber)
                ? user.EmployeeId.ToString(CultureInvariant)
                : user.EnrollNumber.Trim();
            if (string.IsNullOrWhiteSpace(userCode))
                continue;

            var payload = new
            {
                userCode,
                userName = user.UserName ?? string.Empty,
                cardNo = user.CardNo ?? string.Empty,
                enabled = user.Enabled
            };
            var body = JsonSerializer.Serialize(payload);
            using var content = new StringContent(body, Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
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
                AddLegacyRow(list, item);
            return list;
        }

        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("data", out var data) && data.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in data.EnumerateArray())
                AddLegacyRow(list, item);
        }
        return list;
    }

    private static void AddLegacyRow(List<TaktAttendanceSourceIngestRowDto> rows, JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Object)
            return;
        var enrollNumber = ReadString(item, "userCode") ?? ReadString(item, "enrollNumber") ?? string.Empty;
        var externalKey = ReadString(item, "recordId") ?? ReadString(item, "id") ?? Guid.NewGuid().ToString("N");
        var verifyMode = ReadInt(item, "verifyMode") ?? 0;
        var punchText = ReadString(item, "punchTime") ?? ReadString(item, "time") ?? string.Empty;
        var punchTime = DateTime.Now;
        if (!string.IsNullOrWhiteSpace(punchText) && DateTime.TryParse(punchText, out var dt))
            punchTime = dt;

        rows.Add(new TaktAttendanceSourceIngestRowDto
        {
            EmployeeId = 0,
            EnrollNumber = enrollNumber,
            RawPunchTime = punchTime,
            VerifyMode = verifyMode,
            ExternalRecordKey = externalKey,
            RawPayloadJson = item.GetRawText()
        });
    }

    private static string? ReadString(JsonElement obj, string key)
        => obj.TryGetProperty(key, out var p) && p.ValueKind != JsonValueKind.Null ? p.ToString() : null;

    private static int? ReadInt(JsonElement obj, string key)
    {
        if (!obj.TryGetProperty(key, out var p))
            return null;
        if (p.ValueKind == JsonValueKind.Number && p.TryGetInt32(out var i))
            return i;
        return int.TryParse(p.ToString(), out var parsed) ? parsed : null;
    }

    private static HttpClient CreateLegacyHttpClient(TaktAttendanceDevice device, DeliConfig config)
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

    private static string MergeDeliVendorState(string? existingJson, long nextId, bool initDone)
    {
        JsonObject root;
        if (string.IsNullOrWhiteSpace(existingJson))
            root = new JsonObject();
        else
        {
            try
            {
                var n = JsonNode.Parse(existingJson);
                root = n as JsonObject ?? new JsonObject();
            }
            catch
            {
                root = new JsonObject();
            }
        }

        root["deliNextId"] = nextId;
        root["deliInitDone"] = initDone;
        return root.ToJsonString();
    }

    private static DeliConfig ParseConfig(TaktAttendanceDevice device)
    {
        var config = new DeliConfig();
        if (!string.IsNullOrWhiteSpace(device.ApiSecret))
            config.AppSecret = device.ApiSecret.Trim();

        if (string.IsNullOrWhiteSpace(device.ConfigJson))
            return config;

        try
        {
            using var doc = JsonDocument.Parse(device.ConfigJson);
            var root = doc.RootElement;
            config.Protocol = ReadString(root, "protocol") ?? config.Protocol;
            config.UserName = ReadString(root, "userName") ?? ReadString(root, "username");
            config.Password = ReadString(root, "password") ?? config.Password;
            config.AttendanceEndpoint = ReadString(root, "attendanceEndpoint");
            config.UserSyncEndpoint = ReadString(root, "userSyncEndpoint");
            config.TimeoutSeconds = ReadInt(root, "timeoutSeconds") ?? config.TimeoutSeconds;
            config.PageSize = ReadInt(root, "pageSize") ?? config.PageSize;
            config.AppKey = ReadString(root, "appKey") ?? ReadString(root, "app_key");
            config.AppSecret = ReadString(root, "appSecret") ?? ReadString(root, "app_secret") ?? config.AppSecret;
            config.CloudApiBase = ReadString(root, "cloudApiBase") ?? ReadString(root, "cloud_api_base");
            config.DefaultDepartmentExtId = ReadString(root, "deliDefaultDepartmentExtId") ?? ReadString(root, "deli_default_department_ext_id");
            config.DefaultDepartmentTitle = ReadString(root, "deliDefaultDepartmentTitle") ?? ReadString(root, "deli_default_department_title");
            if (root.TryGetProperty("deliNextId", out var nextEl) && nextEl.ValueKind == JsonValueKind.Number && nextEl.TryGetInt64(out var nid))
                config.DeliNextId = nid;
            if (root.TryGetProperty("deliInitDone", out var initEl) && initEl.ValueKind is JsonValueKind.True or JsonValueKind.False)
                config.DeliInitDone = initEl.GetBoolean();
        }
        catch
        {
            // ignore
        }

        return config;
    }

    private static readonly IFormatProvider CultureInvariant = CultureInfo.InvariantCulture;

    private sealed class DeliConfig
    {
        public string Protocol { get; set; } = "http";
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? AttendanceEndpoint { get; set; }
        public string? UserSyncEndpoint { get; set; }
        public int TimeoutSeconds { get; set; } = 20;
        public int PageSize { get; set; } = 100;
        public string? AppKey { get; set; }
        public string? AppSecret { get; set; }
        public string? CloudApiBase { get; set; }
        public string? DefaultDepartmentExtId { get; set; }
        public string? DefaultDepartmentTitle { get; set; }
        public long DeliNextId { get; set; }
        public bool DeliInitDone { get; set; }
    }
}

/// <summary>
/// 得力云拉取结果（含可选的设备 ConfigJson 更新片段）。
/// </summary>
public sealed class TaktDeliAttendanceDownloadResult
{
    public IReadOnlyList<TaktAttendanceSourceIngestRowDto> Rows { get; init; } = Array.Empty<TaktAttendanceSourceIngestRowDto>();

    public string? UpdatedDeviceConfigJson { get; init; }

    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();
}
