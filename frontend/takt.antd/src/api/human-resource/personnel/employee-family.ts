// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-family.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工家庭成员 API，对应后端 TaktEmployeeFamiliesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { EmployeeFamily, EmployeeFamilyCreate, EmployeeFamilyQuery, EmployeeFamilyUpdate } from '@/types/human-resource/personnel/employee-family'

const familyUrl = '/api/TaktEmployeeFamilies'

/** 获取员工家庭成员分页列表 */
export function getEmployeeFamilyList(params: EmployeeFamilyQuery): Promise<TaktPagedResult<EmployeeFamily>> {
  return request({ url: `${familyUrl}/list`, method: 'get', params })
}

/** 根据ID获取员工家庭成员详情 */
export function getEmployeeFamilyById(id: string): Promise<EmployeeFamily> {
  return request({ url: `${familyUrl}/${id}`, method: 'get' })
}

/** 创建员工家庭成员 */
export function createEmployeeFamily(data: EmployeeFamilyCreate): Promise<EmployeeFamily> {
  return request({ url: familyUrl, method: 'post', data })
}

/** 更新员工家庭成员 */
export function updateEmployeeFamily(id: string, data: EmployeeFamilyUpdate): Promise<EmployeeFamily> {
  return request({ url: `${familyUrl}/${id}`, method: 'put', data })
}

/** 删除员工家庭成员（单条） */
export function deleteEmployeeFamilyById(id: string): Promise<void> {
  return request({ url: `${familyUrl}/${id}`, method: 'delete' })
}

/** 批量删除员工家庭成员 */
export function deleteEmployeeFamilyBatch(ids: string[]): Promise<void> {
  return request({ url: `${familyUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/** 获取员工家庭成员导入模板 */
export function getEmployeeFamilyTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${familyUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/** 导入员工家庭成员数据 */
export function importEmployeeFamilyData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${familyUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/** 导出员工家庭成员数据 */
export function exportEmployeeFamilyData(
  query: EmployeeFamilyQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${familyUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
