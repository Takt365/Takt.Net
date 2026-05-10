// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling.SpecificEngine
// 文件名称：TaktControllingService.cs
// 创建时间：2026-05-03
// 创建人：Takt365(Cursor AI)
// 功能描述：成本管控树形服务，提供成本中心、成本要素、利润中心的有效期校验等业务操作
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling.SpecificEngine;

/// <summary>
/// 成本管控专用服务
/// </summary>
public class TaktControllingService : TaktServiceBase, ITaktControllingService
{
    private readonly ITaktRepository<TaktCostCenter> _costCenterRepository;
    private readonly ITaktRepository<TaktCostElement> _costElementRepository;
    private readonly ITaktRepository<TaktProfitCenter> _profitCenterRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="costCenterRepository">成本中心仓储</param>
    /// <param name="costElementRepository">成本要素仓储</param>
    /// <param name="profitCenterRepository">利润中心仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktControllingService(
        ITaktRepository<TaktCostCenter> costCenterRepository,
        ITaktRepository<TaktCostElement> costElementRepository,
        ITaktRepository<TaktProfitCenter> profitCenterRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _costCenterRepository = costCenterRepository;
        _costElementRepository = costElementRepository;
        _profitCenterRepository = profitCenterRepository;
    }

    // ==================== 成本中心日期有效期实现 ====================

    /// <summary>
    /// 更新成本中心有效期
    /// </summary>
    public async Task<bool> UpdateCostCenterValidityAsync(long costCenterId, DateTime validFrom, DateTime validTo)
    {
        var entity = await _costCenterRepository.GetByIdAsync(costCenterId);
        if (entity == null) return false;
        entity.ValidFrom = validFrom;
        entity.ValidTo = validTo;
        await _costCenterRepository.UpdateAsync(entity);
        return true;
    }

    // ==================== 成本要素日期有效期实现 ====================

    /// <summary>
    /// 更新成本要素有效期
    /// </summary>
    public async Task<bool> UpdateCostElementValidityAsync(long costElementId, DateTime validFrom, DateTime validTo)
    {
        var entity = await _costElementRepository.GetByIdAsync(costElementId);
        if (entity == null) return false;
        entity.ValidFrom = validFrom;
        entity.ValidTo = validTo;
        await _costElementRepository.UpdateAsync(entity);
        return true;
    }

    // ==================== 利润中心日期有效期实现 ====================

    /// <summary>
    /// 更新利润中心有效期
    /// </summary>
    public async Task<bool> UpdateProfitCenterValidityAsync(long profitCenterId, DateTime validFrom, DateTime validTo)
    {
        var entity = await _profitCenterRepository.GetByIdAsync(profitCenterId);
        if (entity == null) return false;
        entity.ValidFrom = validFrom;
        entity.ValidTo = validTo;
        await _profitCenterRepository.UpdateAsync(entity);
        return true;
    }
}