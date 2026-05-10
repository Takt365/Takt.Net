// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/security-engine
// 文件名称：data-permission.ts
// 功能描述：数据权限API，对应后端 TaktDataPermissionsController
// 路由前缀：api/TaktDataPermissions
// ========================================

import request from '@/api/request'
import type { TaktApiResult } from '@/types/common'

// ========================================
// 数据权限API
// ========================================
const dataPermissionUrl = 'api/TaktDataPermissions'

/**
 * 获取用户在数据权限下允许访问的部门Id列表（多角色结果做并集）
 * 对应后端：GetAllowedDepartmentsAsync
 * @param userId 用户主键
 * @returns 可见部门Id列表。
 *   - 超级管理员或任一启用角色为「全部数据」时返回全部启用部门Id
 *   - 用户不存在、无角色或无启用角色时返回空列表
 *   - 「仅本人」角色不向列表写入部门Id，业务需自行按创建人/用户Id过滤
 */
export function getAllowedDepartments(userId: number): Promise<TaktApiResult<number[]>> {
  return request({
    url: `${dataPermissionUrl}/allowed-departments`,
    method: 'get',
    params: { userId }
  })
}