// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：role-menu.ts
// 功能描述：RoleMenu API，对应后端 Takt.WebApi.Controllers.Identity.TaktRoleMenus
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  RoleMenu,
  RoleMenuQuery,
  RoleMenuCreate,
  RoleMenuUpdate
} from '@/types/identity/role-menu'

// ========================================
// RoleMenu相关 API（按后端控制器顺序）
// ========================================
const roleMenuUrl = '/api/TaktRoleMenus';

/**
 * 获取RoleMenu列表（分页）
 * 对应后端：GetRoleMenuListAsync
 */
export function getRoleMenuList(params: RoleMenuQuery): Promise<TaktPagedResult<RoleMenu>> {
  return request({
    url: `${roleMenuUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取RoleMenu
 * 对应后端：GetRoleMenuByIdAsync
 */
export function getRoleMenuById(id: string): Promise<RoleMenu> {
  return request({
    url: `${roleMenuUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取RoleMenu选项列表（用于下拉框等）
 * 对应后端：GetRoleMenuOptionsAsync
 */
export function getRoleMenuOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${roleMenuUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建RoleMenu
 * 对应后端：CreateRoleMenuAsync
 */
export function createRoleMenu(data: RoleMenuCreate): Promise<RoleMenu> {
  return request({
    url: roleMenuUrl,
    method: 'post',
    data
  })
}

/**
 * 更新RoleMenu
 * 对应后端：UpdateRoleMenuAsync
 */
export function updateRoleMenu(id: string, data: RoleMenuUpdate): Promise<RoleMenu> {
  return request({
    url: `${roleMenuUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除RoleMenu（单条）
 * 对应后端：DeleteRoleMenuByIdAsync
 */
export function deleteRoleMenuById(id: string): Promise<void> {
  return request({
    url: `${roleMenuUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除RoleMenu
 * 对应后端：DeleteRoleMenuBatchAsync
 */
export function deleteRoleMenuBatch(ids: string[]): Promise<void> {
  return request({
    url: `${roleMenuUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetRoleMenuTemplateAsync；fileName 仅传名称不含后缀
 */
export function getRoleMenuTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${roleMenuUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入RoleMenu
 * 对应后端：ImportRoleMenuAsync
 */
export function importRoleMenuData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${roleMenuUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出RoleMenu
 * 对应后端：ExportRoleMenuAsync；fileName 仅传名称不含后缀
 */
export function exportRoleMenuData(query: RoleMenuQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${roleMenuUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
