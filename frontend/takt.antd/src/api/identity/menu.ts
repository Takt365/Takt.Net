import request from '../request'
import type { TaktPagedResult, TaktTreeSelectOption } from '@/types/common'
import type { Menu, MenuTree, MenuQuery, MenuCreate, MenuUpdate, MenuStatusDto } from '@/types/identity/menu'

/**
 * 获取菜单列表（分页）
 */
export function getList(params: MenuQuery): Promise<TaktPagedResult<Menu>> {
  return request({
    url: '/api/TaktMenus/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取菜单
 */
export function getById(id: string): Promise<Menu> {
  return request({
    url: `/api/TaktMenus/${id}`,
    method: 'get'
  })
}

/**
 * 获取菜单树形选项列表（用于业务组件：components/business/takt-tree-select）
 */
export function getMenuTreeOptions() {
  return request<TaktTreeSelectOption[]>({
    url: '/api/TaktMenus/tree-options',
    method: 'get'
  })
}

/**
 * 获取模块名称选项列表（仅目录 MenuType=0，树形），用于代码生成中的模块名选择。
 */
export function getModuleNameOptions() {
  return request<MenuTree[]>({
    url: '/api/TaktMenus/module-name-options',
    method: 'get'
  })
}

/**
 * 获取当前用户的菜单树（用于路由生成）
 */
export function getMenuTree() {
  return request<MenuTree[]>({
    url: '/api/TaktMenus/current-tree',
    method: 'get'
  })
}

/**
 * 创建菜单
 */
export function create(data: MenuCreate): Promise<Menu> {
  return request({
    url: '/api/TaktMenus',
    method: 'post',
    data
  })
}

/**
 * 更新菜单（data 支持部分字段，如拖拽时仅传 parentId、orderNum）
 */
export function update(id: string, data: Partial<MenuUpdate>): Promise<Menu> {
  return request({
    url: `/api/TaktMenus/${id}`,
    method: 'put',
    data
  })
}

/**
 * 更新菜单状态（单字段更新，对应 PUT /api/TaktMenus/status）
 */
export function updateStatus(data: MenuStatusDto): Promise<Menu> {
  return request({
    url: '/api/TaktMenus/status',
    method: 'put',
    data
  })
}

/**
 * 删除菜单
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktMenus/${id}`,
    method: 'delete'
  })
}

/**
 * 获取导入模板
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktMenus/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入菜单
 */
export function importMenus(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktMenus/import',
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出菜单
 */
export function exportMenus(query: MenuQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktMenus/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
