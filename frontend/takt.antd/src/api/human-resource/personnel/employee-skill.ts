// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-skill.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工业务技能 API，对应后端 TaktEmployeeSkillsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { EmployeeSkill, EmployeeSkillCreate, EmployeeSkillQuery, EmployeeSkillUpdate } from '@/types/human-resource/personnel/employee-skill'

const skillUrl = '/api/TaktEmployeeSkills'

/** 获取员工业务技能分页列表 */
export function getEmployeeSkillList(params: EmployeeSkillQuery): Promise<TaktPagedResult<EmployeeSkill>> {
  return request({ url: `${skillUrl}/list`, method: 'get', params })
}

/** 根据ID获取员工业务技能详情 */
export function getEmployeeSkillById(id: string): Promise<EmployeeSkill> {
  return request({ url: `${skillUrl}/${id}`, method: 'get' })
}

/** 创建员工业务技能 */
export function createEmployeeSkill(data: EmployeeSkillCreate): Promise<EmployeeSkill> {
  return request({ url: skillUrl, method: 'post', data })
}

/** 更新员工业务技能 */
export function updateEmployeeSkill(id: string, data: EmployeeSkillUpdate): Promise<EmployeeSkill> {
  return request({ url: `${skillUrl}/${id}`, method: 'put', data })
}

/** 删除员工业务技能（单条） */
export function deleteEmployeeSkillById(id: string): Promise<void> {
  return request({ url: `${skillUrl}/${id}`, method: 'delete' })
}

/** 批量删除员工业务技能 */
export function deleteEmployeeSkillBatch(ids: string[]): Promise<void> {
  return request({ url: `${skillUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/** 获取员工业务技能导入模板 */
export function getEmployeeSkillTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${skillUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/** 导入员工业务技能数据 */
export function importEmployeeSkillData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${skillUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/** 导出员工业务技能数据 */
export function exportEmployeeSkillData(
  query: EmployeeSkillQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${skillUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
