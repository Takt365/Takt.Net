// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/organization/dept
// 文件名称：dept.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：部门相关类型定义，对应后端 Takt.Application.Dtos.Organization.TaktDeptDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 部门类型（对应后端 Takt.Application.Dtos.Organization.TaktDeptDto）
 */
export interface Dept extends TaktEntityBase {
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 部门名称 */
  deptName: string
  /** 部门编码 */
  deptCode: string
  /** 父级ID（对应后端 ParentId，序列化为string以避免Javascript精度问题） */
  parentId: string
  /** 部门负责人员工 Id（对应后端 DeptHeadId） */
  deptHeadId?: string
  /** 部门负责人展示名（接口返回时由后端根据 DeptHeadId 解析，非库字段） */
  deptHead?: string
  /** 绑定成本中心编码（对应会计库 TaktCostCenter.costCenterCode） */
  costCenterCode?: string
  /** 成本中心展示名（接口解析填充） */
  deptCostCenterName?: string
  /** 代理人员工 Id 列表 */
  delegateEmployeeIds?: string[]
  /** 部门类型（0=直接，1=间接） */
  deptType: number
  /** 部门电话 */
  deptPhone?: string
  /** 部门邮箱 */
  deptMail?: string
  /** 部门地址 */
  deptAddr?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 部门状态（0=启用，1=禁用） */
  deptStatus: number
  /** 用户ID列表 */
  userIds?: string[]
  /** 角色ID列表 */
  roleIds?: string[]
}

/**
 * 部门查询类型（对应后端 Takt.Application.Dtos.Organization.TaktDeptQueryDto）
 */
export interface DeptQuery extends TaktPagedQuery {
  /** 关键词（在部门名称、部门编码中模糊查询） */
  keyWords?: string
  /** 部门名称 */
  deptName?: string
  /** 部门编码 */
  deptCode?: string
  /** 父级ID（对应后端 ParentId，序列化为string以避免Javascript精度问题） */
  parentId?: string
  /** 部门状态（0=启用，1=禁用） */
  deptStatus?: number
}

/**
 * 创建部门类型（对应后端 Takt.Application.Dtos.Organization.TaktDeptCreateDto）
 */
export interface DeptCreate {
  /** 部门名称 */
  deptName: string
  /** 部门编码 */
  deptCode: string
  /** 父级ID（对应后端 ParentId，序列化为string以避免Javascript精度问题） */
  parentId: string
  /** 部门负责人员工 Id（对应后端 DeptHeadId） */
  deptHeadId: string
  /** 绑定成本中心编码（可选） */
  costCenterCode?: string
  /** 代理人员工 Id 列表（可选） */
  delegateEmployeeIds?: string[]
  /** 部门类型（0=直接，1=间接） */
  deptType: number
  /** 部门电话 */
  deptPhone?: string
  /** 部门邮箱 */
  deptMail?: string
  /** 部门地址 */
  deptAddr?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围） */
  dataScope: number
  /** 部门状态（0=启用，1=禁用） */
  deptStatus?: number
  /** 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔） */
  customScope?: string
  /** 备注 */
  remark?: string
  /** 用户ID列表 */
  userIds?: string[]
  /** 角色ID列表 */
  roleIds?: string[]
}

/**
 * 更新部门类型（对应后端 Takt.Application.Dtos.Organization.TaktDeptUpdateDto）
 */
export interface DeptUpdate extends DeptCreate {
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
}

/**
 * 部门状态类型（对应后端 Takt.Application.Dtos.Organization.TaktDeptStatusDto）
 */
export interface DeptStatus {
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 部门状态（0=启用，1=禁用） */
  deptStatus: number
}
