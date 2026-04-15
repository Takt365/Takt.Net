// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-skill
// 文件名称：employee-skill.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工业务技能相关类型定义，对应后端 TaktEmployeeSkillDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工业务技能类型（对应后端 TaktEmployeeSkillDto）
 */
export interface EmployeeSkill extends TaktEntityBase {
  /** 员工业务技能ID（对应后端 EmployeeSkillId） */
  employeeSkillId: string
  /** 员工ID（对应后端 EmployeeId） */
  employeeId: string
  /** 技能名称 */
  skillName: string
  /** 技能等级 */
  skillLevel: number
  /** 证书名称 */
  certificateName?: string
  /** 证书编号 */
  certificateNo?: string
  /** 获得日期 */
  obtainedDate?: string
  /** 到期日期 */
  expiryDate?: string
}

/**
 * 员工业务技能查询类型（对应后端 TaktEmployeeSkillQueryDto）
 */
export interface EmployeeSkillQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 技能名称（模糊） */
  skillName?: string
  /** 技能等级（精确） */
  skillLevel?: number
}

/**
 * 创建员工业务技能类型（对应后端 TaktEmployeeSkillCreateDto）
 */
export interface EmployeeSkillCreate {
  /** 员工ID */
  employeeId: string
  /** 技能名称 */
  skillName: string
  /** 技能等级 */
  skillLevel: number
  /** 证书名称 */
  certificateName?: string
  /** 证书编号 */
  certificateNo?: string
  /** 获得日期 */
  obtainedDate?: string
  /** 到期日期 */
  expiryDate?: string
}

/**
 * 更新员工业务技能类型（对应后端 TaktEmployeeSkillUpdateDto）
 */
export interface EmployeeSkillUpdate extends EmployeeSkillCreate {
  /** 员工业务技能ID */
  employeeSkillId: string
}
