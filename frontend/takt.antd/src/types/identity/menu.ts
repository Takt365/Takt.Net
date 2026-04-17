import type { IdS } from '@/types/internal/openapi-pick'

type TaktMenuTree = IdS<'Takt.Application.Dtos.Identity.TaktMenuTreeDto'>

/** 与后端菜单树 DTO 一致，并保留历史/树选择器兼容字段（dict*，小驼峰）。 */
export type MenuTree = TaktMenuTree & {
  dictLabel?: string | null
  dictValue?: string | number | null
  extLabel?: string | null
  extValue?: string | number | null
  transKey?: string
  children?: MenuTree[] | null
}

export type Menu = IdS<'Takt.Application.Dtos.Identity.TaktMenuDto'>
export type MenuCreate = IdS<'Takt.Application.Dtos.Identity.TaktMenuCreateDto'>
export type MenuUpdate = IdS<'Takt.Application.Dtos.Identity.TaktMenuUpdateDto'>
export type MenuQuery = IdS<'Takt.Application.Dtos.Identity.TaktMenuQueryDto'>
export type MenuStatusDto = IdS<'Takt.Application.Dtos.Identity.TaktMenuStatusDto'>
