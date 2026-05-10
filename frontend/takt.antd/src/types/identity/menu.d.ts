// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/menu
// 文件名称：menu.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：menu相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Menu类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuDto）
 */
export interface Menu extends TaktEntityBase {
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 menuName */
  menuName: string
  /** 对应后端字段 menuCode */
  menuCode: string
  /** 对应后端字段 menuL10nKey */
  menuL10nKey?: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 path */
  path?: string
  /** 对应后端字段 component */
  component?: string
  /** 对应后端字段 menuIcon */
  menuIcon?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 menuType */
  menuType: number
  /** 对应后端字段 permission */
  permission?: string
  /** 对应后端字段 isVisible */
  isVisible: number
  /** 对应后端字段 isCache */
  isCache: number
  /** 对应后端字段 isExternal */
  isExternal: number
  /** 对应后端字段 linkUrl */
  linkUrl?: string
  /** 对应后端字段 menuStatus */
  menuStatus: number
}

/**
 * MenuTree类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuTreeDto）
 */
export interface MenuTree extends Menu {
  /** 对应后端字段 children */
  children: MenuTree[]
}

/**
 * MenuQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuQueryDto）
 */
export interface MenuQuery extends TaktPagedQuery {
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 menuName */
  menuName?: string
  /** 对应后端字段 menuCode */
  menuCode?: string
  /** 对应后端字段 menuL10nKey */
  menuL10nKey?: string
  /** 对应后端字段 path */
  path?: string
  /** 对应后端字段 component */
  component?: string
  /** 对应后端字段 menuIcon */
  menuIcon?: string
  /** 对应后端字段 menuType */
  menuType?: number
  /** 对应后端字段 permission */
  permission?: string
  /** 对应后端字段 isVisible */
  isVisible?: number
  /** 对应后端字段 isCache */
  isCache?: number
  /** 对应后端字段 isExternal */
  isExternal?: number
  /** 对应后端字段 linkUrl */
  linkUrl?: string
  /** 对应后端字段 menuStatus */
  menuStatus?: number
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * MenuCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuCreateDto）
 */
export interface MenuCreate {
  /** 对应后端字段 menuName */
  menuName: string
  /** 对应后端字段 menuCode */
  menuCode: string
  /** 对应后端字段 menuL10nKey */
  menuL10nKey?: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 path */
  path?: string
  /** 对应后端字段 component */
  component?: string
  /** 对应后端字段 menuIcon */
  menuIcon?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 menuType */
  menuType: number
  /** 对应后端字段 permission */
  permission?: string
  /** 对应后端字段 isVisible */
  isVisible: number
  /** 对应后端字段 isCache */
  isCache: number
  /** 对应后端字段 isExternal */
  isExternal: number
  /** 对应后端字段 linkUrl */
  linkUrl?: string
  /** 对应后端字段 menuStatus */
  menuStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * MenuUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuUpdateDto）
 */
export interface MenuUpdate extends MenuCreate {
  /** 对应后端字段 menuId */
  menuId: string
}

/**
 * MenuStatus类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuStatusDto）
 */
export interface MenuStatus {
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 menuStatus */
  menuStatus: number
}

/**
 * MenuSort类型（对应后端 Takt.Application.Dtos.Identity.TaktMenuSortDto）
 */
export interface MenuSort {
  /** 对应后端字段 menuId */
  menuId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
