// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-contract.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工合同 API，对应后端 TaktEmployeeContractsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { EmployeeContract, EmployeeContractCreate, EmployeeContractQuery, EmployeeContractUpdate } from '@/types/human-resource/personnel/employee-contract'

const contractUrl = '/api/TaktEmployeeContracts'

/** 获取员工合同分页列表 */
export function getEmployeeContractList(params: EmployeeContractQuery): Promise<TaktPagedResult<EmployeeContract>> {
  return request({ url: `${contractUrl}/list`, method: 'get', params })
}

/** 根据ID获取员工合同详情 */
export function getEmployeeContractById(id: string): Promise<EmployeeContract> {
  return request({ url: `${contractUrl}/${id}`, method: 'get' })
}

/** 创建员工合同 */
export function createEmployeeContract(data: EmployeeContractCreate): Promise<EmployeeContract> {
  return request({ url: contractUrl, method: 'post', data })
}

/** 更新员工合同 */
export function updateEmployeeContract(id: string, data: EmployeeContractUpdate): Promise<EmployeeContract> {
  return request({ url: `${contractUrl}/${id}`, method: 'put', data })
}

/** 删除员工合同（单条） */
export function deleteEmployeeContractById(id: string): Promise<void> {
  return request({ url: `${contractUrl}/${id}`, method: 'delete' })
}

/** 批量删除员工合同 */
export function deleteEmployeeContractBatch(ids: string[]): Promise<void> {
  return request({ url: `${contractUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/** 获取员工合同导入模板 */
export function getEmployeeContractTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${contractUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/** 导入员工合同数据 */
export function importEmployeeContractData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${contractUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/** 导出员工合同数据 */
export function exportEmployeeContractData(
  query: EmployeeContractQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${contractUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
