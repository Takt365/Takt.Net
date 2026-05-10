// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/compensation-benefits/salary-structure
// 文件名称：salary-structure.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：salary-structure相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SalaryStructure类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryStructureDto）
 */
export interface SalaryStructure extends TaktEntityBase {
  /** 对应后端字段 salaryStructureId */
  salaryStructureId: string
  /** 对应后端字段 structureCode */
  structureCode: string
  /** 对应后端字段 structureName */
  structureName: string
  /** 对应后端字段 salaryLevel */
  salaryLevel: string
  /** 对应后端字段 salaryGrade */
  salaryGrade: string
  /** 对应后端字段 minSalary */
  minSalary: number
  /** 对应后端字段 midSalary */
  midSalary: number
  /** 对应后端字段 maxSalary */
  maxSalary: number
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
}

/**
 * SalaryStructureQuery类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryStructureQueryDto）
 */
export interface SalaryStructureQuery extends TaktPagedQuery {
  /** 对应后端字段 structureCode */
  structureCode?: string
  /** 对应后端字段 structureName */
  structureName?: string
  /** 对应后端字段 salaryLevel */
  salaryLevel?: string
  /** 对应后端字段 salaryGrade */
  salaryGrade?: string
  /** 对应后端字段 minSalary */
  minSalary?: number
  /** 对应后端字段 midSalary */
  midSalary?: number
  /** 对应后端字段 maxSalary */
  maxSalary?: number
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicablePosition */
  applicablePosition?: string
  /** 对应后端字段 description */
  description?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 status */
  status?: number
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
 * SalaryStructureCreate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryStructureCreateDto）
 */
export interface SalaryStructureCreate {
  /** 对应后端字段 structureCode */
  structureCode: string
  /** 对应后端字段 structureName */
  structureName: string
  /** 对应后端字段 salaryLevel */
  salaryLevel: string
  /** 对应后端字段 salaryGrade */
  salaryGrade: string
  /** 对应后端字段 minSalary */
  minSalary: number
  /** 对应后端字段 midSalary */
  midSalary: number
  /** 对应后端字段 maxSalary */
  maxSalary: number
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicablePosition */
  applicablePosition: string
  /** 对应后端字段 description */
  description: string
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SalaryStructureUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryStructureUpdateDto）
 */
export interface SalaryStructureUpdate extends SalaryStructureCreate {
  /** 对应后端字段 salaryStructureId */
  salaryStructureId: string
}

/**
 * SalaryStructureStatus类型（对应后端 Takt.Application.Dtos.HumanResource.CompensationBenefits.TaktSalaryStructureStatusDto）
 */
export interface SalaryStructureStatus {
  /** 对应后端字段 salaryStructureId */
  salaryStructureId: string
  /** 对应后端字段 status */
  status: number
}
