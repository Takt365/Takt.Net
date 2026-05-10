// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Logging
// 文件名称：TaktServerMonitorsController.cs
// 创建时间：2026-05-06
// 创建人：Takt365(Cursor AI)
// 功能描述：服务器监控控制器，提供服务器硬件信息和应用状态查询API
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Application.Services.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Statistics.Logging;

/// <summary>
/// 服务器监控控制器
/// </summary>
[Route("api/[controller]", Name = "服务器监控")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:logging:monitor", "服务器监控管理")]
public class TaktServerMonitorsController : TaktControllerBase
{
    private readonly ITaktServerMonitorService _serverMonitorService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktServerMonitorsController(
        ITaktServerMonitorService serverMonitorService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _serverMonitorService = serverMonitorService;
    }

    /// <summary>
    /// 获取服务器硬件信息
    /// </summary>
    [HttpGet("hardware")]
    [TaktPermission("statistics:logging:monitor:query", "查询服务器硬件信息")]
    public async Task<ActionResult<TaktServerHardwareDto>> GetServerHardware()
    {
        var hardware = await _serverMonitorService.GetServerHardwareAsync();
        return Ok(hardware);
    }

    /// <summary>
    /// 获取应用运行状态
    /// </summary>
    [HttpGet("status")]
    [TaktPermission("statistics:logging:monitor:query", "查询应用运行状态")]
    public async Task<ActionResult<TaktAppStatusDto>> GetAppStatus()
    {
        var status = await _serverMonitorService.GetAppStatusAsync();
        return Ok(status);
    }

    /// <summary>
    /// 刷新硬件信息缓存
    /// </summary>
    [HttpPost("refresh")]
    [TaktPermission("statistics:logging:monitor:refresh", "刷新硬件信息缓存")]
    public ActionResult RefreshHardwareCache()
    {
        _serverMonitorService.RefreshHardwareCache();
        return Ok();
    }
}
