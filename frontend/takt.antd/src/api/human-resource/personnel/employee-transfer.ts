// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-transfer.ts
// 功能描述：员工调动 API，对应后端 HumanResource/Personnel TaktEmployeeTransfersController
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  EmployeeTransfer,
  EmployeeTransferQuery,
  EmployeeTransferCreate,
  EmployeeTransferUpdate,
  EmployeeTransferStatus
} from '@/types/human-resource/personnel/employee-transfer'

// ========================================
// 员工调动相关 API（按后端控制器顺序）
// ========================================

const transferUrl = '/api/TaktEmployeeTransfers'

/**
 * 获取员工调动分页列表
 * 对应后端：GetListAsync
 */
export function getEmployeeTransferList(params: EmployeeTransferQuery): Promise<TaktPagedResult<EmployeeTransfer>> {
  return request({
    url: `${transferUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取员工调动详情
 * 对应后端：GetByIdAsync
 */
export function getEmployeeTransferById(id: string): Promise<EmployeeTransfer> {
  return request({
    url: `${transferUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建员工调动
 * 对应后端：CreateAsync
 */
export function createEmployeeTransfer(data: EmployeeTransferCreate): Promise<EmployeeTransfer> {
  return request({
    url: transferUrl,
    method: 'post',
    data
  })
}

/**
 * 更新员工调动
 * 对应后端：UpdateAsync
 */
export function updateEmployeeTransfer(id: string, data: EmployeeTransferUpdate): Promise<EmployeeTransfer> {
  return request({
    url: `${transferUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除员工调动（单条）
 * 对应后端：DeleteAsync
 */
export function deleteEmployeeTransferById(id: string): Promise<void> {
  return request({
    url: `${transferUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除员工调动
 * 对应后端：DeleteBatchAsync
 */
export function deleteEmployeeTransferBatch(ids: string[]): Promise<void> {
  return request({
    url: `${transferUrl}/batch`,
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 更新员工调动状态（流程回调时调用）
 * 对应后端：UpdateStatusAsync
 */
export function updateEmployeeTransferStatus(data: EmployeeTransferStatus): Promise<EmployeeTransfer> {
  return request({
    url: `${transferUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 导出员工调动数据
 * 对应后端：ExportAsync
 */
export function exportEmployeeTransferData(
  query: EmployeeTransferQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${transferUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
