// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/humanresource/organization/dept
// 功能描述：部门相关 API，对应后端 HumanResource/Organization TaktDeptsController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktTreeSelectOption } from '@/types/common'
import type {
  Dept,
  DeptQuery,
  DeptCreate,
  DeptUpdate,
  DeptStatus
} from '@/types/human-resource/organization/dept'
import type { UserDept } from '@/types/human-resource/organization/user-dept'
import type { RoleDept } from '@/types/human-resource/organization/role-dept'
import type { RoleQuery } from '@/types/identity/role'

const deptUrl = '/api/TaktDepts'

/**
 * 获取部门列表（分页）
 */
export function getDeptList(params: DeptQuery): Promise<TaktPagedResult<Dept>> {
  return request({
    url: `${deptUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取部门
 */
export function getDeptById(id: string): Promise<Dept> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取部门树形选项列表（用于树形下拉框等）
 */
export function getDeptTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${deptUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 创建部门
 */
export function createDept(data: DeptCreate): Promise<Dept> {
  return request({
    url: deptUrl,
    method: 'post',
    data
  })
}

/**
 * 更新部门（data 支持部分字段，如拖拽时仅传 parentId、orderNum）
 */
export function updateDept(id: string, data: Partial<DeptUpdate>): Promise<Dept> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除部门
 */
export function deleteDeptById(id: string): Promise<void> {
  return request({
    url: `${deptUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 更新部门状态
 */
export function updateDeptStatus(data: DeptStatus): Promise<Dept> {
  return request({
    url: `${deptUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取部门用户列表
 */
export function getUserDeptIds(deptId: string): Promise<UserDept[]> {
  return request({
    url: `${deptUrl}/${deptId}/users`,
    method: 'get'
  })
}

/**
 * 分配部门用户
 */
export function assignUserDepts(deptId: string, userIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `${deptUrl}/${deptId}/users`,
    method: 'put',
    data: userIds
  })
}

/**
 * 获取部门角色列表
 */
export function getRoleDeptIds(deptId: string, query: RoleQuery): Promise<TaktPagedResult<RoleDept>> {
  return request({
    url: `${deptUrl}/${deptId}/roles`,
    method: 'post',
    data: query
  })
}

/**
 * 分配部门角色
 */
export function assignRoleDepts(deptId: string, roleIds: string[]): Promise<{ success: boolean }> {
  return request({
    url: `${deptUrl}/${deptId}/roles`,
    method: 'put',
    data: roleIds
  })
}

/**
 * 获取导入模板
 */
export function getDeptTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${deptUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入部门
 */
export function importDeptData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${deptUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出部门
 */
export function exportDeptData(query: DeptQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${deptUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
