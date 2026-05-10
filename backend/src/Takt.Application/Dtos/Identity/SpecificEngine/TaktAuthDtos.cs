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
/// 注意：此类已废弃，前端已使用 OpenIddict 标准 OAuth2 参数（通过 URLSearchParams 传递）
/// 保留此类仅供参考和历史代码兼容性
/// </summary>
public partial class TaktLoginDto
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
/// 注意：此类已废弃，OpenIddict 直接返回标准 OAuth2 响应格式
/// 保留此类仅供参考和历史代码兼容性
/// </summary>
public partial class TaktLoginResponseDto
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
/// 注意：此类已废弃，前端已使用 OpenIddict 标准 OAuth2 参数（通过 URLSearchParams 传递）
/// 保留此类仅供参考和历史代码兼容性
/// </summary>
public partial class TaktRefreshTokenDto
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
public partial class TaktUserInfoDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserInfoDto()
    {
        UserId = string.Empty;
        UserName = string.Empty;
        NickName = string.Empty;
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
    /// 昵称（用户表持久化字段；为空时前端可与 <see cref="RealName"/> 组合展示）
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<string> Roles { get; set; } = [];

    /// <summary>
    /// 权限列表
    /// </summary>
    public List<string> Permissions { get; set; } = [];

    /// <summary>
    /// 用户类型（0=普通用户，1=管理员，2=超级管理员）
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 关联员工 ID（与 TaktUser.EmployeeId 一致，发起流程等场景用于默认「申请人」）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public string EmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// 今日是否为假日
    /// </summary>
    public bool? HolidayToday { get; set; }

    /// <summary>
    /// 假日名称
    /// </summary>
    public string? HolidayName { get; set; }

    /// <summary>
    /// 假日问候语
    /// </summary>
    public string? HolidayGreeting { get; set; }

    /// <summary>
    /// 假日名言/格言
    /// </summary>
    public string? HolidayQuote { get; set; }

    /// <summary>
    /// 假日主题色
    /// </summary>
    public string? HolidayTheme { get; set; }
}
