// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-skill.ts
// 功能描述：EmployeeSkill API，对应后端 Takt.WebApi.Controllers.HumanResource.Personnel.TaktEmployeeSkills
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeSkill,
  EmployeeSkillQuery,
  EmployeeSkillCreate,
  EmployeeSkillUpdate
} from '@/types/human-resource/personnel/employee-skill'

// ========================================
// EmployeeSkill相关 API（按后端控制器顺序）
// ========================================
const employeeSkillUrl = '/api/TaktEmployeeSkills';

/**
 * 获取EmployeeSkill列表（分页）
 * 对应后端：GetEmployeeSkillListAsync
 */
export function getEmployeeSkillList(params: EmployeeSkillQuery): Promise<TaktPagedResult<EmployeeSkill>> {
  return request({
    url: `${employeeSkillUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeSkill
 * 对应后端：GetEmployeeSkillByIdAsync
 */
export function getEmployeeSkillById(id: string): Promise<EmployeeSkill> {
  return request({
    url: `${employeeSkillUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeSkill选项列表（用于下拉框等）
 * 对应后端：GetEmployeeSkillOptionsAsync
 */
export function getEmployeeSkillOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeSkillUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeSkill
 * 对应后端：CreateEmployeeSkillAsync
 */
export function createEmployeeSkill(data: EmployeeSkillCreate): Promise<EmployeeSkill> {
  return request({
    url: employeeSkillUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeSkill
 * 对应后端：UpdateEmployeeSkillAsync
 */
export function updateEmployeeSkill(id: string, data: EmployeeSkillUpdate): Promise<EmployeeSkill> {
  return request({
    url: `${employeeSkillUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeSkill（单条）
 * 对应后端：DeleteEmployeeSkillByIdAsync
 */
export function deleteEmployeeSkillById(id: string): Promise<void> {
  return request({
    url: `${employeeSkillUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeSkill
 * 对应后端：DeleteEmployeeSkillBatchAsync
 */
export function deleteEmployeeSkillBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeSkillUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeSkillTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeSkillTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeSkillUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeSkill
 * 对应后端：ImportEmployeeSkillAsync
 */
export function importEmployeeSkillData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeSkillUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeSkill
 * 对应后端：ExportEmployeeSkillAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeSkillData(query: EmployeeSkillQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeSkillUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
