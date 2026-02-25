// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/identity/permission
// 文件名称：permission.d.ts
// 功能描述：权限相关类型定义，对应后端 Takt.Application.Dtos.Identity.TaktPermissionDtos
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 权限类型（对应后端 TaktPermissionDto）
 */
export interface Permission extends TaktEntityBase {
  /** 权限ID（对应后端 PermissionId，序列化为 string） */
  permissionId: string
  /** 权限标识（唯一） */
  permissionCode: string
  /** 权限名称 */
  permissionName?: string
  /** 模块（如 identity、routine） */
  module?: string
  /** 关联菜单ID（可选） */
  menuId?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 权限状态（0=启用，1=禁用） */
  permissionStatus: number
}

/**
 * 权限查询类型（对应后端 TaktPermissionQueryDto）
 */
export interface PermissionQuery extends TaktPagedQuery {
  /** 权限标识（模糊） */
  permissionCode?: string
  /** 权限名称（模糊） */
  permissionName?: string
  /** 模块 */
  module?: string
  /** 权限状态（0=启用，1=禁用） */
  permissionStatus?: number
}

/**
 * 创建权限类型（对应后端 TaktPermissionCreateDto）
 */
export interface PermissionCreate {
  /** 权限标识（唯一） */
  permissionCode: string
  /** 权限名称 */
  permissionName?: string
  /** 模块 */
  module?: string
  /** 关联菜单ID（可选） */
  menuId?: string
  /** 排序号 */
  orderNum: number
}

/**
 * 更新权限类型（对应后端 TaktPermissionUpdateDto）
 */
export interface PermissionUpdate extends PermissionCreate {
  /** 权限ID */
  permissionId: string
  /** 权限状态（0=启用，1=禁用） */
  permissionStatus: number
}

/**
 * 权限状态类型（对应后端 TaktPermissionStatusDto）
 */
export interface PermissionStatus {
  /** 权限ID */
  permissionId: string
  /** 权限状态（0=启用，1=禁用） */
  permissionStatus: number
}
