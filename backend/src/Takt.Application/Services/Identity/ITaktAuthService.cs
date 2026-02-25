// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktAuthService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt认证服务接口，定义用户认证和授权操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// Takt认证服务接口
/// </summary>
public interface ITaktAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录请求DTO</param>
    /// <returns>登录响应DTO</returns>
    Task<TaktLoginResponseDto> LoginAsync(TaktLoginDto dto);

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="dto">刷新令牌请求DTO</param>
    /// <returns>登录响应DTO</returns>
    Task<TaktLoginResponseDto> RefreshTokenAsync(TaktRefreshTokenDto dto);

    /// <summary>
    /// 登出
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>用户名（用于日志记录）</returns>
    Task<string> LogoutAsync(string refreshToken);

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns>用户信息DTO</returns>
    Task<TaktUserInfoDto> GetUserInfoAsync();

    /// <summary>
    /// 记录登录成功（登录日志和在线记录）
    /// </summary>
    /// <param name="user">用户实体</param>
    /// <param name="userName">用户名</param>
    /// <returns>任务</returns>
    Task RecordLoginSuccessAsync(TaktUser user, string userName);

    /// <summary>
    /// 尝试记录登录成功：若已达设备数上限且未强制登录，则返回已在别处登录及会话列表，不踢旧会话、不发 token。
    /// </summary>
    /// <param name="user">用户实体</param>
    /// <param name="userName">用户名</param>
    /// <param name="forceLogin">是否强制登录（踢出最旧会话后写入在线记录）</param>
    /// <returns>成功则 Success 为 true；已达上限且未强制则 Success 为 false 且 ExistingSessions 有值</returns>
    Task<RecordLoginSuccessResult> TryRecordLoginSuccessAsync(TaktUser user, string userName, bool forceLogin);

    /// <summary>
    /// 撤销指定用户的所有刷新令牌（单设备登录时，新登录前调用以使旧设备无法再刷新 token）
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>被撤销的令牌数量</returns>
    Task<int> RevokeRefreshTokensByUserIdAsync(long userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 记录登录失败（登录日志和失败次数累加）
    /// </summary>
    /// <param name="user">用户实体（如果用户存在）</param>
    /// <param name="userName">用户名</param>
    /// <param name="failureReason">失败原因</param>
    /// <returns>任务</returns>
    Task RecordLoginFailureAsync(TaktUser? user, string userName, string failureReason);

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>角色代码列表</returns>
    Task<List<string>> GetUserRolesAsync(long userId);

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>权限标识列表</returns>
    Task<List<string>> GetUserPermissionsAsync(long userId);

    /// <summary>
    /// 获取当前假日（用于登录 token 写入今日是否假期，供前端问候语/主题色）
    /// </summary>
    /// <param name="date">日期</param>
    /// <param name="region">地区（由语言映射，如 zh-CN→CN）</param>
    /// <returns>当日假日 DTO，非假日返回 null</returns>
    Task<TaktHolidayDto?> GetCurrentHolidayForLoginAsync(DateTime date, string? region);
}
