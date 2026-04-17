import type { IdentityComponents } from '@/types/generated'
import type { IdS, OrgS, RouS } from '@/types/internal/openapi-pick'

type UserPaged =
  IdentityComponents['schemas']["Takt.Shared.Models.TaktPagedResult`1[[Takt.Application.Dtos.Identity.TaktUserDto, Takt.Application, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null]]"]

export type TaktPagedResult<T> = Omit<UserPaged, 'data'> & { data: T[] }

type ApiObj =
  IdentityComponents['schemas']["Takt.Shared.Models.TaktApiResult`1[[System.Object, System.Private.CoreLib, Version=8.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]"]

export type TaktApiResult<T> = Omit<ApiObj, 'data'> & { data: T | null }

export type TaktPagedQuery = Pick<
  OrgS<'Takt.Application.Dtos.HumanResource.Organization.TaktDeptQueryDto'>,
  'pageIndex' | 'pageSize' | 'keyWords'
> & { KeyWords?: string }

type RawSelect = IdS<'Takt.Shared.Models.TaktSelectOption'>
export type TaktSelectOption = Omit<RawSelect, 'dictValue' | 'extValue'> & {
  dictValue?: string | number
  extValue?: string | number
}

type RawTree = IdS<'Takt.Shared.Models.TaktTreeSelectOption'>
export type TaktTreeSelectOption = Omit<RawTree, 'dictValue' | 'extValue' | 'children'> & {
  dictValue?: string | number
  extValue?: string | number
  children?: TaktTreeSelectOption[]
}

export type LeaveProofAttachment = RouS<'Takt.Application.Dtos.Routine.Tasks.Files.TaktFileDto'>

export type { TaktResultCode } from '@/utils/enum'
