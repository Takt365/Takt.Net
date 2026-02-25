// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Identity
// 文件名称：ITaktLoginElsewhereNotifier.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：通知“已在别处请求登录”的接口，用于向旧会话推送消息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Identity;

/// <summary>
/// 通知旧会话“该用户名在xxx位置请求登录”的推送接口
/// </summary>
public interface ITaktLoginElsewhereNotifier
{
    /// <summary>
    /// 向指定连接推送“在xxx位置请求登录，是否退出当前登录”消息
    /// </summary>
    /// <param name="connectionIds">要通知的 SignalR 连接 ID 列表（仅限已连接 Hub 的会话）</param>
    /// <param name="requestLocation">请求登录的位置描述（如 IP 或地点）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>任务</returns>
    Task NotifyLoginRequestElsewhereAsync(
        IReadOnlyList<string> connectionIds,
        string requestLocation,
        CancellationToken cancellationToken = default);
}
