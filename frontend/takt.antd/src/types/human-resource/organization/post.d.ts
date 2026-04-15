// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/organization/post
// 文件名称：post.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：岗位相关类型定义，对应后端 Takt.Application.Dtos.Organization.TaktPostDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 岗位类型（对应后端 Takt.Application.Dtos.Organization.TaktPostDto）
 */
export interface Post extends TaktEntityBase {
  /** 岗位ID（对应后端 PostId，序列化为string以避免Javascript精度问题） */
  postId: string
  /** 岗位名称 */
  postName: string
  /** 岗位编码 */
  postCode: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 岗位类别 */
  postCategory?: string
  /** 岗位级别 */
  postLevel: number
  /** 岗位职责 */
  postDuty?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 岗位状态（0=启用，1=禁用） */
  postStatus: number
  /** 用户ID列表 */
  userIds?: string[]
}

/**
 * 岗位查询类型（对应后端 Takt.Application.Dtos.Organization.TaktPostQueryDto）
 */
export interface PostQuery extends TaktPagedQuery {
  /** 关键词（在岗位名称、岗位编码中模糊查询） */
  keyWords?: string
  /** 岗位名称 */
  postName?: string
  /** 岗位编码 */
  postCode?: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId?: string
  /** 岗位状态（0=启用，1=禁用） */
  postStatus?: number
}

/**
 * 创建岗位类型（对应后端 Takt.Application.Dtos.Organization.TaktPostCreateDto）
 */
export interface PostCreate {
  /** 岗位名称 */
  postName: string
  /** 岗位编码 */
  postCode: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 岗位类别 */
  postCategory?: string
  /** 岗位级别 */
  postLevel: number
  /** 岗位职责 */
  postDuty?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 备注 */
  remark?: string
  /** 用户ID列表 */
  userIds?: string[]
}

/**
 * 更新岗位类型（对应后端 Takt.Application.Dtos.Organization.TaktPostUpdateDto）
 */
export interface PostUpdate extends PostCreate {
  /** 岗位ID（对应后端 PostId，序列化为string以避免Javascript精度问题） */
  postId: string
}

/**
 * 岗位状态类型（对应后端 Takt.Application.Dtos.Organization.TaktPostStatusDto）
 */
export interface PostStatus {
  /** 岗位ID（对应后端 PostId，序列化为string以避免Javascript精度问题） */
  postId: string
  /** 岗位状态（0=启用，1=禁用） */
  postStatus: number
}
