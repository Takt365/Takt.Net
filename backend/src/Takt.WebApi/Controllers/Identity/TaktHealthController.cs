// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Identity
// 文件名称：TaktHealthController.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt健康检查控制器，提供系统健康状态验证（包括 SignalR 元信息）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Enums;
using Takt.Shared.Models;

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
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <returns>健康状态</returns>
    [HttpGet]
    [HttpGet("check")]
    public ActionResult<TaktApiResult<object>> Check()
    {
        var payload = new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown"
        };
        return Ok(TaktApiResult<object>.Ok(payload));
    }

    /// <summary>
    /// 详细健康检查（包含更多系统信息）
    /// </summary>
    /// <returns>详细健康状态</returns>
    [HttpGet("detailed")]
    public ActionResult<TaktApiResult<object>> Detailed()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version?.ToString() ?? "unknown";

        var payload = new
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
        };
        return Ok(TaktApiResult<object>.Ok(payload));
    }

    /// <summary>
    /// SignalR健康检查（路由已映射即视为可用；与全局 API 统一返回 TaktApiResult）
    /// </summary>
    /// <returns>SignalR健康状态</returns>
    [HttpGet("signalr")]
    public ActionResult<TaktApiResult<object>> SignalR()
    {
        try
        {
            var signalRStatus = new
            {
                connectHub = "available",
                notificationHub = "available"
            };

            var payload = new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                signalR = signalRStatus
            };

            return Ok(TaktApiResult<object>.Ok(payload));
        }
        catch (Exception ex)
        {
            var failPayload = new
            {
                status = "unhealthy",
                timestamp = DateTime.UtcNow,
                error = ex.Message
            };
            return Ok(new TaktApiResult<object>
            {
                Code = TaktResultCode.Failed,
                Message = ex.Message,
                Data = failPayload
            });
        }
    }
}
