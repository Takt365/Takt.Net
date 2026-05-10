// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity
// 文件名称：menu.ts
// 功能描述：Menu API，对应后端 Takt.WebApi.Controllers.Identity.TaktMenus
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type {
  Menu,
  MenuTree,
  MenuQuery,
  MenuCreate,
  MenuUpdate,
  MenuStatus,
  MenuSort
} from '@/types/identity/menu'

// ========================================
// Menu相关 API（按后端控制器顺序）
// ========================================
const menuUrl = '/api/TaktMenus';

/**
 * 获取Menu列表（分页）
 * 对应后端：GetMenuListAsync
 */
export function getMenuList(params: MenuQuery): Promise<TaktPagedResult<Menu>> {
  return request({
    url: `${menuUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Menu
 * 对应后端：GetMenuByIdAsync
 */
export function getMenuById(id: string): Promise<Menu> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Menu选项列表（用于下拉框等）
 * 对应后端：GetMenuOptionsAsync
 */
export function getMenuOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${menuUrl}/options`,
    method: 'get'
  })
}

/**
 * 获取Menu树形选项列表（用于树形下拉框等）
 * 对应后端：GetMenuTreeOptionsAsync
 */
export function getMenuTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${menuUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取Menu树形列表
 * 对应后端：GetMenuTreeAsync
 */
export function getMenuTree(parentId: number = 0, includeDisabled: boolean = false): Promise<MenuTree[]> {
  return request({
    url: `${menuUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取Menu子节点列表
 * 对应后端：GetMenuChildrenAsync
 */
export function getMenuChildren(parentId: number, includeDisabled: boolean = false): Promise<Menu[]> {
  return request({
    url: `${menuUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 创建Menu
 * 对应后端：CreateMenuAsync
 */
export function createMenu(data: MenuCreate): Promise<Menu> {
  return request({
    url: menuUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Menu
 * 对应后端：UpdateMenuAsync
 */
export function updateMenu(id: string, data: MenuUpdate): Promise<Menu> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Menu（单条）
 * 对应后端：DeleteMenuByIdAsync
 */
export function deleteMenuById(id: string): Promise<void> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Menu
 * 对应后端：DeleteMenuBatchAsync
 */
export function deleteMenuBatch(ids: string[]): Promise<void> {
  return request({
    url: `${menuUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Menu状态
 * 对应后端：UpdateMenuStatusAsync
 */
export function updateMenuStatus(data: MenuStatus): Promise<MenuStatus> {
  return request({
    url: `${menuUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Menu排序
 * 对应后端：UpdateMenuSortAsync
 */
export function updateMenuSort(data: MenuSort): Promise<MenuSort> {
  return request({
    url: `${menuUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMenuTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMenuTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${menuUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Menu
 * 对应后端：ImportMenuAsync
 */
export function importMenuData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${menuUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Menu
 * 对应后端：ExportMenuAsync；fileName 仅传名称不含后缀
 */
export function exportMenuData(query: MenuQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${menuUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
