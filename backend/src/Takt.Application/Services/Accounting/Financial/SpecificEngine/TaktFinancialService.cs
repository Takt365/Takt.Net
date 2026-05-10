// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial.SpecificEngine
// 文件名称：TaktFinancialService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：财务专用服务，提供会计科目、固定资产等有效期校验业务操作
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Services;
using Takt.Domain.Entities.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Financial.SpecificEngine;

/// <summary>
/// 财务专用服务
/// </summary>
public class TaktFinancialService : TaktServiceBase, ITaktFinancialService
{
    private readonly ITaktRepository<TaktAccountingTitle> _accountingTitleRepository;
    private readonly ITaktRepository<TaktAsset> _assetRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="accountingTitleRepository">会计科目仓储</param>
    /// <param name="assetRepository">固定资产仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktFinancialService(
        ITaktRepository<TaktAccountingTitle> accountingTitleRepository,
        ITaktRepository<TaktAsset> assetRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _accountingTitleRepository = accountingTitleRepository;
        _assetRepository = assetRepository;
    }

    // ==================== 会计科目日期有效期实现 ====================

    /// <summary>
    /// 更新会计科目有效期
    /// </summary>
    public async Task<bool> UpdateAccountingTitleValidityAsync(long accountingTitleId, DateTime validFrom, DateTime validTo)
    {
        var entity = await _accountingTitleRepository.GetByIdAsync(accountingTitleId);
        if (entity == null) return false;
        entity.ValidFrom = validFrom;
        entity.ValidTo = validTo;
        await _accountingTitleRepository.UpdateAsync(entity);
        return true;
    }

    // ==================== 固定资产日期有效期实现 ====================

    /// <summary>
    /// 更新固定资产购买日期
    /// </summary>
    public async Task<bool> UpdateAssetPurchaseDateAsync(long assetId, DateTime purchaseDate)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.PurchaseDate = purchaseDate;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }

    /// <summary>
    /// 更新固定资产报废日期
    /// </summary>
    public async Task<bool> UpdateAssetScrapDateAsync(long assetId, DateTime scrapDate)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.ScrapDate = scrapDate;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }

    /// <summary>
    /// 更新固定资产处置日期
    /// </summary>
    public async Task<bool> UpdateAssetDisposalDateAsync(long assetId, DateTime disposalDate)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.DisposalDate = disposalDate;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }

    /// <summary>
    /// 更新固定资产启用日期
    /// </summary>
    public async Task<bool> UpdateAssetStartDateAsync(long assetId, DateTime startDate)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.StartDate = startDate;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }

    /// <summary>
    /// 更新固定资产折旧方法
    /// </summary>
    public async Task<bool> UpdateAssetDepreciationMethodAsync(long assetId, int depreciationMethod)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.DepreciationMethod = depreciationMethod;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }

    /// <summary>
    /// 更新固定资产位置
    /// </summary>
    public async Task<bool> UpdateAssetLocationAsync(long assetId, string location)
    {
        var entity = await _assetRepository.GetByIdAsync(assetId);
        if (entity == null) return false;
        entity.AssetLocation = location;
        await _assetRepository.UpdateAsync(entity);
        return true;
    }
}