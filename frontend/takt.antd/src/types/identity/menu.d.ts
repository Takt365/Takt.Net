// 菜单相关类型定义
// 菜单列表项继承 TaktEntityBase，与 User、Role 一致
import type { TaktEntityBase, TaktPagedQuery, TaktTreeSelectOption } from '@/types/common'

/** 菜单列表项（对应后端 TaktMenuDto，平铺列表） */
export interface Menu extends TaktEntityBase {
  /** 菜单ID（对应后端 MenuId，序列化为 string 以避免精度问题） */
  menuId: string
  /** 菜单名称 */
  menuName: string
  /** 菜单编码 */
  menuCode: string
  /** 菜单本地化键 */
  menuL10nKey?: string
  /** 父级ID（0 表示根） */
  parentId: string
  /** 路由路径 */
  path?: string
  /** 组件路径 */
  component?: string
  /** 图标 */
  menuIcon?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 菜单类型（0=目录，1=菜单，2=按钮） */
  menuType: number
  /** 权限标识 */
  permission?: string
  /** 是否可见（1=是，0=否） */
  isVisible: number
  /** 是否缓存（1=是，0=否） */
  isCache: number
  /** 是否外链（1=是，0=否） */
  isExternal: number
  /** 外链地址 */
  linkUrl?: string
  /** 菜单状态（1=启用，0=禁用） */
  menuStatus: number
}

/** 菜单查询（对应后端 TaktMenuQueryDto） */
export interface MenuQuery extends TaktPagedQuery {
  keyWords?: string
  menuName?: string
  menuCode?: string
  parentId?: string
  menuType?: number
  menuStatus?: number
}

/** 创建菜单（对应后端 TaktMenuCreateDto） */
export interface MenuCreate {
  menuName: string
  menuCode: string
  menuL10nKey?: string
  parentId: number | string
  path?: string
  component?: string
  menuIcon?: string
  orderNum?: number
  menuType?: number
  permission?: string
  isVisible?: number
  isCache?: number
  isExternal?: number
  linkUrl?: string
  /** 菜单状态（1=启用，0=禁用） */
  menuStatus?: number
  remark?: string
}

/** 更新菜单（对应后端 TaktMenuUpdateDto） */
export interface MenuUpdate extends MenuCreate {
  menuId: string
}

/** 菜单状态 DTO（对应后端 TaktMenuStatusDto，用于 PUT /api/TaktMenus/status） */
export interface MenuStatusDto {
  menuId: string
  /** 菜单状态（1=启用，0=禁用） */
  menuStatus: number
}

/** 菜单排序号 DTO（对应后端 TaktMenuOrderNumDto，后端暂无独立接口） */
export interface MenuOrderNumDto {
  menuId: string
  /** 排序号（越小越靠前） */
  orderNum: number
}

/** 菜单可见性 DTO（对应后端 TaktMenuVisibleDto，后端暂无独立接口） */
export interface MenuVisibleDto {
  menuId: string
  /** 是否可见（1=是，0=否） */
  isVisible: number
}

/** 菜单缓存 DTO（对应后端 TaktMenuIsCacheDto，后端暂无独立接口） */
export interface MenuIsCacheDto {
  menuId: string
  /** 是否缓存（1=是，0=否） */
  isCache: number
}

export interface MenuTree extends TaktTreeSelectOption {
  /** 菜单图标（用于前端路由和菜单显示） */
  menuIcon?: string
  /** 组件路径（用于前端路由） */
  component?: string
  /** 菜单类型（0=目录，1=菜单，2=按钮） */
  menuType?: number
  /** 菜单状态（1=启用，0=禁用） */
  menuStatus?: number
  /** 是否可见（1=是，0=否） */
  isVisible?: number
  /** 权限标识 */
  permission?: string
  /** 子节点列表（菜单树） */
  children?: MenuTree[]
  // 兼容旧字段（从后端 TaktMenuTreeDto 映射时使用）
  menuId?: string
  menuName?: string
  menuCode?: string
  path?: string
  menuL10nKey?: string // 菜单本地化键（兼容字段，对应 dictL10nKey）
  parentId?: string
  // 兼容旧字段（PascalCase 格式）
  TransKey?: string // 兼容字段，对应 dictL10nKey
  MenuIcon?: string
  Component?: string
  MenuType?: number
  MenuStatus?: number
  IsVisible?: number
  Permission?: string
}
