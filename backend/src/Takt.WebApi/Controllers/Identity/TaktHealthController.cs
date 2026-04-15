// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktHealthController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt健康检查控制器，提供系统健康状态验证（包括SignalR）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Takt.Infrastructure.Attributes;
using Takt.Infrastructure.SignalR;
namespace Takt.WebApi.Controllers.Identity;

/// <summary>
/// Takt健康检查控制器
/// </summary>
/// <remarks>
/// 创建者:Takt(Cursor AI)
/// 创建时间: 2025-01-20
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[ApiModule("Identity", "身份认证")]
[AllowAnonymous]
public class TaktHealthController : ControllerBase
{
    private readonly IHubContext<TaktConnectHub>? _connectHubContext;
    private readonly IHubContext<TaktNotificationHub>? _notificationHubContext;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectHubContext">连接Hub上下文（可选）</param>
    /// <param name="notificationHubContext">通知Hub上下文（可选）</param>
    public TaktHealthController(
        IHubContext<TaktConnectHub>? connectHubContext = null,
        IHubContext<TaktNotificationHub>? notificationHubContext = null)
    {
        _connectHubContext = connectHubContext;
        _notificationHubContext = notificationHubContext;
    }

    /// <summary>
    /// 健康检查
    /// </summary>
    /// <returns>健康状态</returns>
    [HttpGet]
    [HttpGet("check")]
    public ActionResult<object> Check()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown"
        });
    }

    /// <summary>
    /// 详细健康检查（包含更多系统信息）
    /// </summary>
    /// <returns>详细健康状态</returns>
    [HttpGet("detailed")]
    public ActionResult<object> Detailed()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version?.ToString() ?? "unknown";

        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            version = version,
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
            machineName = Environment.MachineName,
            osVersion = Environment.OSVersion.ToString(),
            processorCount = Environment.ProcessorCount,
            workingSet = Environment.WorkingSet,
            uptime = TimeSpan.FromMilliseconds(Environment.TickCount).ToString(@"dd\.hh\:mm\:ss")
        });
    }

    /// <summary>
    /// SignalR健康检查
    /// </summary>
    /// <returns>SignalR健康状态</returns>
    [HttpGet("signalr")]
    public ActionResult<object> SignalR()
    {
        try
        {
            var signalRStatus = new
            {
                connectHub = _connectHubContext != null ? "available" : "unavailable",
                notificationHub = _notificationHubContext != null ? "available" : "unavailable"
            };

            // 尝试检查 Hub 是否可用（通过检查 HubContext 是否已初始化）
            var isHealthy = _connectHubContext != null || _notificationHubContext != null;

            return Ok(new
            {
                status = isHealthy ? "healthy" : "unhealthy",
                timestamp = DateTime.UtcNow,
                signalR = signalRStatus
            });
        }
        catch (Exception ex)
        {
            return StatusCode(503, new
            {
                status = "unhealthy",
                timestamp = DateTime.UtcNow,
                error = ex.Message
            });
        }
    }
}
