// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Accounting.Financial.SpecificEngine
// 文件名称：ITaktFinancialService.cs
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：财务专用服务接口，定义会计科目、固定资产等有效期校验业务操作
// ========================================

using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Services.Accounting.Financial.SpecificEngine;

/// <summary>
/// 财务专用服务接口
/// </summary>
public interface ITaktFinancialService
{
    // ==================== 会计科目日期有效期 ====================

    /// <summary>
    /// 更新会计科目有效期
    /// </summary>
    /// <param name="accountingTitleId">会计科目ID</param>
    /// <param name="validFrom">生效日期</param>
    /// <param name="validTo">失效日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAccountingTitleValidityAsync(long accountingTitleId, DateTime validFrom, DateTime validTo);

    // ==================== 固定资产日期有效期 ====================

    /// <summary>
    /// 更新固定资产购买日期
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="purchaseDate">购买日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetPurchaseDateAsync(long assetId, DateTime purchaseDate);

    /// <summary>
    /// 更新固定资产报废日期
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="scrapDate">报废日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetScrapDateAsync(long assetId, DateTime scrapDate);

    /// <summary>
    /// 更新固定资产处置日期
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="disposalDate">处置日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetDisposalDateAsync(long assetId, DateTime disposalDate);

    /// <summary>
    /// 更新固定资产启用日期
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="startDate">启用日期</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetStartDateAsync(long assetId, DateTime startDate);

    /// <summary>
    /// 更新固定资产折旧方法
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="depreciationMethod">折旧方法</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetDepreciationMethodAsync(long assetId, int depreciationMethod);

    /// <summary>
    /// 更新固定资产位置
    /// </summary>
    /// <param name="assetId">固定资产ID</param>
    /// <param name="location">资产位置</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAssetLocationAsync(long assetId, string location);
}