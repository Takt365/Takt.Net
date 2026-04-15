import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktTreeSelectOption } from '@/types/common'
import type { Menu, MenuTree, MenuQuery, MenuCreate, MenuUpdate, MenuStatusDto } from '@/types/identity/menu'

const menuUrl = '/api/TaktMenus'

/**
 * 获取菜单列表（分页）
 */
export function getMenuList(params: MenuQuery): Promise<TaktPagedResult<Menu>> {
  return request({
    url: `${menuUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取菜单
 */
export function getMenuById(id: string): Promise<Menu> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取菜单树形选项列表（用于业务组件：components/business/takt-tree-select）
 */
export function getMenuTreeOptions() {
  return request<TaktTreeSelectOption[]>({
    url: `${menuUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取模块名称选项列表（仅目录 MenuType=0，树形），用于代码生成中的模块名选择。
 */
export function getModuleNameOptions() {
  return request<MenuTree[]>({
    url: `${menuUrl}/module-name-options`,
    method: 'get'
  })
}

/**
 * 获取当前用户的菜单树（用于路由生成）
 */
export function getMenuTree() {
  return request<MenuTree[]>({
    url: `${menuUrl}/current-tree`,
    method: 'get'
  })
}

/**
 * 创建菜单
 */
export function createMenu(data: MenuCreate): Promise<Menu> {
  return request({
    url: menuUrl,
    method: 'post',
    data
  })
}

/**
 * 更新菜单（data 支持部分字段，如拖拽时仅传 parentId、orderNum）
 */
export function updateMenu(id: string, data: Partial<MenuUpdate>): Promise<Menu> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 更新菜单状态（单字段更新，对应 PUT /api/TaktMenus/status）
 */
export function updateMenuStatus(data: MenuStatusDto): Promise<Menu> {
  return request({
    url: `${menuUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 删除菜单
 */
export function deleteMenuById(id: string): Promise<void> {
  return request({
    url: `${menuUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 获取导入模板
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
 * 导入菜单
 */
export function importMenuData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
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
 * 导出菜单
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
