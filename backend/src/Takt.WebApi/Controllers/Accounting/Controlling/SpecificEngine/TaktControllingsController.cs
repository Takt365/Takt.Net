// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling.SpecificEngine
// 文件名称：TaktControllingsController.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：成本管控专用控制器，提供成本中心、成本要素、利润中心的有效期校验等RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Services.Accounting.Controlling.SpecificEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Accounting.Controlling.SpecificEngine;

/// <summary>
/// 成本管控专用控制器
/// </summary>
[Route("api/[controller]", Name = "成本管控专用")]
[ApiModule("Accounting", "成本管控")]
[TaktPermission("accounting:controlling:specific", "成本管控专用管理")]
[ApiController]
public class TaktControllingsController : TaktControllerBase
{
    private readonly ITaktControllingService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktControllingsController(
        ITaktControllingService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    // ==================== 成本中心日期有效期 ====================

    /// <summary>
    /// 更新成本中心有效期
    /// </summary>
    [HttpPut("cost-center/validity/{costCenterId}")]
    [TaktPermission("accounting:controlling:costcenter:update", "更新成本中心有效期")]
    public async Task<ActionResult<bool>> UpdateCostCenterValidityAsync(long costCenterId, [FromQuery] DateTime validFrom, [FromBody] DateTime validTo)
    {
        var result = await _service.UpdateCostCenterValidityAsync(costCenterId, validFrom, validTo);
        return Ok(result);
    }

    // ==================== 成本要素日期有效期 ====================

    /// <summary>
    /// 更新成本要素有效期
    /// </summary>
    [HttpPut("cost-element/validity/{costElementId}")]
    [TaktPermission("accounting:controlling:costelement:update", "更新成本要素有效期")]
    public async Task<ActionResult<bool>> UpdateCostElementValidityAsync(long costElementId, [FromQuery] DateTime validFrom, [FromBody] DateTime validTo)
    {
        var result = await _service.UpdateCostElementValidityAsync(costElementId, validFrom, validTo);
        return Ok(result);
    }

    // ==================== 利润中心日期有效期 ====================

    /// <summary>
    /// 更新利润中心有效期
    /// </summary>
    [HttpPut("profit-center/validity/{profitCenterId}")]
    [TaktPermission("accounting:controlling:profitcenter:update", "更新利润中心有效期")]
    public async Task<ActionResult<bool>> UpdateProfitCenterValidityAsync(long profitCenterId, [FromQuery] DateTime validFrom, [FromBody] DateTime validTo)
    {
        var result = await _service.UpdateProfitCenterValidityAsync(profitCenterId, validFrom, validTo);
        return Ok(result);
    }
}