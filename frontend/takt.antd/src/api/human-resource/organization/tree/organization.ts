// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/organization/tree
// 文件名称：organization.ts
// 功能描述：Organization API，对应后端 Takt.WebApi.Controllers.HumanResource.Organization.Tree.TaktOrganization
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption, TaktTreeSelectOption } from '@/types/common'
import type { Organization } from '@/types/human-resource/organization/tree/organization'

// ========================================
// Organization相关 API（按后端控制器顺序）
// ========================================
const organizationUrl = 'api/TaktOrganization';

/**
 * 获取Organization树形选项列表（用于树形下拉框等）
 * 对应后端：GetTreeOptionsAsync
 */
export function getOrganizationTreeOptions(): Promise<TaktTreeSelectOption[]> {
  return request({
    url: `${organizationUrl}/tree-options`,
    method: 'get'
  })
}

/**
 * 获取Organization树形列表
 * 对应后端：GetTreeAsync
 */
export function getOrganizationTree(parentId: number = 0, includeDisabled: boolean = false): Promise<OrganizationTree[]> {
  return request({
    url: `${organizationUrl}/tree`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}

/**
 * 获取Organization子节点列表
 * 对应后端：GetChildrenAsync
 */
export function getOrganizationChildren(parentId: number, includeDisabled: boolean = false): Promise<Organization[]> {
  return request({
    url: `${organizationUrl}/children`,
    method: 'get',
    params: { parentId, includeDisabled }
  })
}
