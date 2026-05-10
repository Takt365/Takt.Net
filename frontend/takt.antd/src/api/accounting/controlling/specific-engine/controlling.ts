// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/specific-engine
// 文件名称：controlling.ts
// 功能描述：成本管控专用 API，对应后端 Takt.WebApi.Controllers.Accounting.Controlling.SpecificEngine.TaktControllings
// ========================================

import request from '@/api/request'

// ========================================
// 成本管控专用 API（按后端控制器顺序）
// ========================================
const controllingUrl = 'api/TaktControllings';

/**
 * 更新成本中心有效期
 * 对应后端：UpdateCostCenterValidityAsync
 */
export function updateCostCenterValidity(
  costCenterId: number,
  validFrom: Date,
  validTo: Date
): Promise<boolean> {
  return request({
    url: `${controllingUrl}/cost-center/validity/${costCenterId}`,
    method: 'put',
    data: validFrom,
    params: { validTo: validTo.toISOString() }
  })
}

/**
 * 更新成本要素有效期
 * 对应后端：UpdateCostElementValidityAsync
 */
export function updateCostElementValidity(
  costElementId: number,
  validFrom: Date,
  validTo: Date
): Promise<boolean> {
  return request({
    url: `${controllingUrl}/cost-element/validity/${costElementId}`,
    method: 'put',
    data: validFrom,
    params: { validTo: validTo.toISOString() }
  })
}

/**
 * 更新利润中心有效期
 * 对应后端：UpdateProfitCenterValidityAsync
 */
export function updateProfitCenterValidity(
  profitCenterId: number,
  validFrom: Date,
  validTo: Date
): Promise<boolean> {
  return request({
    url: `${controllingUrl}/profit-center/validity/${profitCenterId}`,
    method: 'put',
    data: validFrom,
    params: { validTo: validTo.toISOString() }
  })
}

