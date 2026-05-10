// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial/specific-engine
// 文件名称：financial.ts
// 功能描述：财务专用 API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.SpecificEngine.TaktFinancials
// ========================================

import request from '@/api/request'

// ========================================
// 财务专用 API（按后端控制器顺序）
// ========================================
const financialUrl = 'api/TaktFinancials';

/**
 * 更新会计科目有效期
 * 对应后端：UpdateAccountingTitleValidityAsync
 */
export function updateAccountingTitleValidity(
  accountingTitleId: number,
  validFrom: string,
  validTo: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/accounting-title/validity/${accountingTitleId}`,
    method: 'put',
    data: { validFrom, validTo }
  })
}

/**
 * 更新固定资产购买日期
 * 对应后端：UpdateAssetPurchaseDateAsync
 */
export function updateAssetPurchaseDate(
  assetId: number,
  purchaseDate: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/purchase-date/${assetId}`,
    method: 'put',
    data: purchaseDate
  })
}

/**
 * 更新固定资产报废日期
 * 对应后端：UpdateAssetScrapDateAsync
 */
export function updateAssetScrapDate(
  assetId: number,
  scrapDate: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/scrap-date/${assetId}`,
    method: 'put',
    data: scrapDate
  })
}

/**
 * 更新固定资产处置日期
 * 对应后端：UpdateAssetDisposalDateAsync
 */
export function updateAssetDisposalDate(
  assetId: number,
  disposalDate: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/disposal-date/${assetId}`,
    method: 'put',
    data: disposalDate
  })
}

/**
 * 更新固定资产启用日期
 * 对应后端：UpdateAssetStartDateAsync
 */
export function updateAssetStartDate(
  assetId: number,
  startDate: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/start-date/${assetId}`,
    method: 'put',
    data: startDate
  })
}

/**
 * 更新固定资产折旧方法
 * 对应后端：UpdateAssetDepreciationMethodAsync
 */
export function updateAssetDepreciationMethod(
  assetId: number,
  depreciationMethod: number
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/depreciation-method/${assetId}`,
    method: 'put',
    data: depreciationMethod
  })
}

/**
 * 更新固定资产位置
 * 对应后端：UpdateAssetLocationAsync
 */
export function updateAssetLocation(
  assetId: number,
  location: string
): Promise<boolean> {
  return request({
    url: `${financialUrl}/asset/location/${assetId}`,
    method: 'put',
    data: location
  })
}