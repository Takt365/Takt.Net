// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/organization/role-dept
// 文件名称：role-dept.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：角色部门关联类型定义，对应后端 Takt.Application.Dtos.Organization.TaktRoleDeptDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 角色部门关联类型（对应后端 Takt.Application.Dtos.Organization.TaktRoleDeptDto）
 * 用于从角色角度查看部门，或从部门角度查看角色（双向关联）
 */
export interface RoleDept extends TaktEntityBase {
  /** 角色部门关联ID（对应后端 RoleDeptId，序列化为string以避免Javascript精度问题） */
  roleDeptId: string
  /** 角色ID（对应后端 RoleId，序列化为string以避免Javascript精度问题） */
  roleId: string
  /** 角色名称（对应后端 RoleName） */
  roleName: string
  /** 角色编码（对应后端 RoleCode） */
  roleCode: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 部门名称（对应后端 DeptName） */
  deptName: string
  /** 部门编码（对应后端 DeptCode） */
  deptCode: string
}
