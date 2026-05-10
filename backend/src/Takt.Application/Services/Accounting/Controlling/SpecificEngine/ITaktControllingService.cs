// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Controlling.SpecificEngine
// 文件名称：ITaktControllingService.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：成本管控树形服务接口，定义成本中心、成本要素、利润中心的有效期校验等业务操作
// ========================================

using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Shared.Models;

namespace Takt.Application.Services.Accounting.Controlling.SpecificEngine;

/// <summary>
/// 成本管控专用服务接口
/// </summary>
public interface ITaktControllingService
{
    // ==================== 成本中心日期有效期 ====================

    /// <summary>
    /// 更新成本中心有效期
    /// </summary>
    /// <param name="costCenterId">成本中心ID</param>
    /// <param name="validFrom">生效日期</param>
    /// <param name="validTo">失效日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateCostCenterValidityAsync(long costCenterId, DateTime validFrom, DateTime validTo);

    // ==================== 成本要素日期有效期 ====================

    /// <summary>
    /// 更新成本要素有效期
    /// </summary>
    /// <param name="costElementId">成本要素ID</param>
    /// <param name="validFrom">生效日期</param>
    /// <param name="validTo">失效日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateCostElementValidityAsync(long costElementId, DateTime validFrom, DateTime validTo);

    // ==================== 利润中心日期有效期 ====================

    /// <summary>
    /// 更新利润中心有效期
    /// </summary>
    /// <param name="profitCenterId">利润中心ID</param>
    /// <param name="validFrom">生效日期</param>
    /// <param name="validTo">失效日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateProfitCenterValidityAsync(long profitCenterId, DateTime validFrom, DateTime validTo);
}