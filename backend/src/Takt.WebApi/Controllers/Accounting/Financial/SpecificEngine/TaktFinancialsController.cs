// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Financial.SpecificEngine
// 文件名称：TaktFinancialsController.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：财务专用控制器，提供会计科目、固定资产等有效期校验RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Services.Accounting.Financial.SpecificEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Accounting.Financial.SpecificEngine;

/// <summary>
/// 财务专用控制器
/// </summary>
[Route("api/[controller]", Name = "财务专用")]
[ApiModule("Accounting", "成本管控")]
[TaktPermission("accounting:financial:specific", "财务专用管理")]
[ApiController]
public class TaktFinancialsController : TaktControllerBase
{
    private readonly ITaktFinancialService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFinancialsController(
        ITaktFinancialService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    // ==================== 会计科目日期有效期 ====================

    /// <summary>
    /// 更新会计科目有效期
    /// </summary>
    [HttpPut("accounting-title/validity/{accountingTitleId}")]
    [TaktPermission("accounting:financial:accountingtitle:update", "更新会计科目有效期")]
    public async Task<ActionResult<bool>> UpdateAccountingTitleValidityAsync(long accountingTitleId, [FromQuery] DateTime validFrom, [FromBody] DateTime validTo)
    {
        var result = await _service.UpdateAccountingTitleValidityAsync(accountingTitleId, validFrom, validTo);
        return Ok(result);
    }

    // ==================== 固定资产日期有效期 ====================

    /// <summary>
    /// 更新固定资产购买日期
    /// </summary>
    [HttpPut("asset/purchase-date/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产购买日期")]
    public async Task<ActionResult<bool>> UpdateAssetPurchaseDateAsync(long assetId, [FromBody] DateTime purchaseDate)
    {
        var result = await _service.UpdateAssetPurchaseDateAsync(assetId, purchaseDate);
        return Ok(result);
    }

    /// <summary>
    /// 更新固定资产报废日期
    /// </summary>
    [HttpPut("asset/scrap-date/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产报废日期")]
    public async Task<ActionResult<bool>> UpdateAssetScrapDateAsync(long assetId, [FromBody] DateTime scrapDate)
    {
        var result = await _service.UpdateAssetScrapDateAsync(assetId, scrapDate);
        return Ok(result);
    }

    /// <summary>
    /// 更新固定资产处置日期
    /// </summary>
    [HttpPut("asset/disposal-date/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产处置日期")]
    public async Task<ActionResult<bool>> UpdateAssetDisposalDateAsync(long assetId, [FromBody] DateTime disposalDate)
    {
        var result = await _service.UpdateAssetDisposalDateAsync(assetId, disposalDate);
        return Ok(result);
    }

    /// <summary>
    /// 更新固定资产启用日期
    /// </summary>
    [HttpPut("asset/start-date/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产启用日期")]
    public async Task<ActionResult<bool>> UpdateAssetStartDateAsync(long assetId, [FromBody] DateTime startDate)
    {
        var result = await _service.UpdateAssetStartDateAsync(assetId, startDate);
        return Ok(result);
    }

    /// <summary>
    /// 更新固定资产折旧方法
    /// </summary>
    [HttpPut("asset/depreciation-method/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产折旧方法")]
    public async Task<ActionResult<bool>> UpdateAssetDepreciationMethodAsync(long assetId, [FromBody] int depreciationMethod)
    {
        var result = await _service.UpdateAssetDepreciationMethodAsync(assetId, depreciationMethod);
        return Ok(result);
    }

    /// <summary>
    /// 更新固定资产位置
    /// </summary>
    [HttpPut("asset/location/{assetId}")]
    [TaktPermission("accounting:financial:asset:update", "更新固定资产位置")]
    public async Task<ActionResult<bool>> UpdateAssetLocationAsync(long assetId, [FromBody] string location)
    {
        var result = await _service.UpdateAssetLocationAsync(assetId, location);
        return Ok(result);
    }
}