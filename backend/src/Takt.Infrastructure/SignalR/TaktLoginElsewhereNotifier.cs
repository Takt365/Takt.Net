// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.SignalR
// 文件名称：TaktLoginElsewhereNotifier.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：通过 SignalR 向旧会话推送“该用户名在xxx位置请求登录，是否退出当前登录”
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.SignalR;
using Takt.Application.Services.Identity;

namespace Takt.Infrastructure.SignalR;

/// <summary>
/// 通过 TaktConnectHub 向指定连接推送“在别处请求登录”通知
/// </summary>
public class TaktLoginElsewhereNotifier : ITaktLoginElsewhereNotifier
{
    private readonly IHubContext<TaktConnectHub> _hubContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="hubContext">TaktConnectHub 的 Hub 上下文</param>
    public TaktLoginElsewhereNotifier(IHubContext<TaktConnectHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc />
    public async Task NotifyLoginRequestElsewhereAsync(
        IReadOnlyList<string> connectionIds,
        string requestLocation,
        CancellationToken cancellationToken = default)
    {
        if (connectionIds == null || connectionIds.Count == 0)
        {
            return;
        }

        var payload = new { requestLocation };
        await _hubContext.Clients.Clients(connectionIds.ToList()).SendAsync("LoginRequestElsewhere", payload, cancellationToken);
    }
}
