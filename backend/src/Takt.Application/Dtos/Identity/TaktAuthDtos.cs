// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Identity
// 文件名称：TaktAuthDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt认证相关DTO，包括登录、刷新token等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 登录请求DTO
/// </summary>
public class TaktLoginDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginDto()
    {
        UserName = string.Empty;
        Password = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 记住我
    /// </summary>
    public bool RememberMe { get; set; } = false;
}

/// <summary>
/// 登录响应DTO
/// </summary>
public class TaktLoginResponseDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginResponseDto()
    {
        AccessToken = string.Empty;
        RefreshToken = string.Empty;
        TokenType = "Bearer";
    }

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 令牌类型
    /// </summary>
    public string TokenType { get; set; }

    /// <summary>
    /// 过期时间（秒）
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    public TaktUserInfoDto? UserInfo { get; set; }
}

/// <summary>
/// 刷新令牌请求DTO
/// </summary>
public class TaktRefreshTokenDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktRefreshTokenDto()
    {
        RefreshToken = string.Empty;
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }
}

/// <summary>
/// 用户信息DTO
/// </summary>
public class TaktUserInfoDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserInfoDto()
    {
        UserId = string.Empty;
        UserName = string.Empty;
        RealName = string.Empty;
        Avatar = string.Empty;
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public string UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 英文名（非 zh-CN/zh-TW 时优先展示）
    /// </summary>
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称（zh-CN/zh-TW 时优先展示）
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<string> Roles { get; set; } = new();

    /// <summary>
    /// 权限列表
    /// </summary>
    public List<string> Permissions { get; set; } = new();

    /// <summary>
    /// 今日是否为假日（与 token 中 holiday_today 一致；access_token 为 JWE 时由 userinfo 返回供前端使用）
    /// </summary>
    public bool HolidayToday { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string? HolidayName { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key）
    /// </summary>
    public string? HolidayTheme { get; set; }
}

/// <summary>
/// 已有登录会话信息（用于「当前用户已在xxx位置登录，需要发送通知吗？」提示）
/// </summary>
public class TaktExistingSessionDto
{
    /// <summary>连接地点（IP 定位或空）</summary>
    public string? ConnectLocation { get; set; }
    /// <summary>连接 IP</summary>
    public string? ConnectIp { get; set; }
    /// <summary>设备类型（PC/Mobile 等）</summary>
    public string? DeviceType { get; set; }
    /// <summary>浏览器类型</summary>
    public string? BrowserType { get; set; }
    /// <summary>连接时间</summary>
    public DateTime ConnectTime { get; set; }
}

/// <summary>
/// 记录登录成功结果：成功 或 已在别处登录（含已有会话列表，供前端提示）
/// </summary>
public class RecordLoginSuccessResult
{
    /// <summary>是否已写入在线记录并允许发 token</summary>
    public bool Success { get; set; }
    /// <summary>已有会话列表（Success 为 false 时返回，用于提示「当前用户已在xxx位置登录」）</summary>
    public List<TaktExistingSessionDto>? ExistingSessions { get; set; }
}
