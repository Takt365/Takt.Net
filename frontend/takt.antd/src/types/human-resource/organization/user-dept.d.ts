// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/organization/user-dept
// 文件名称：user-dept.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户部门关联类型定义，对应后端 Takt.Application.Dtos.Organization.TaktUserDeptDto
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase } from '@/types/common'

/**
 * 用户部门关联类型（对应后端 Takt.Application.Dtos.Organization.TaktUserDeptDto）
 * 用于获取部门用户列表，即根据部门ID获取该部门下的用户列表
 */
export interface UserDept extends TaktEntityBase {
  /** 用户部门关联ID（对应后端 UserDeptId，序列化为string以避免Javascript精度问题） */
  userDeptId: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户名（对应后端 UserName） */
  userName: string
  /** 用户真实姓名（对应后端 RealName） */
  realName: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 部门名称（对应后端 DeptName） */
  deptName: string
  /** 部门编码（对应后端 DeptCode） */
  deptCode: string
}
