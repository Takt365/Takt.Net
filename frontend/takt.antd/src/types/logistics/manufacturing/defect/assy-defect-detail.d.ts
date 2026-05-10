// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/assy-defect-detail
// 文件名称：assy-defect-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：assy-defect-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AssyDefectDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectDetailDto）
 */
export interface AssyDefectDetail extends TaktEntityBase {
  /** 对应后端字段 assyDefectDetailId */
  assyDefectDetailId: string
  /** 对应后端字段 assyDefectId */
  assyDefectId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 defectCategory */
  defectCategory?: string
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 cumulativeDefectQty */
  cumulativeDefectQty: number
  /** 对应后端字段 randomCardNo */
  randomCardNo?: string
  /** 对应后端字段 occurrenceEngineering */
  occurrenceEngineering?: string
  /** 对应后端字段 testStep */
  testStep?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 repairOperator */
  repairOperator?: string
  /** 对应后端字段 assyDefect */
  assyDefect?: unknown
}

/**
 * AssyDefectDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectDetailQueryDto）
 */
export interface AssyDefectDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 assyDefectId */
  assyDefectId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 defectCategory */
  defectCategory?: string
  /** 对应后端字段 defectQty */
  defectQty?: number
  /** 对应后端字段 cumulativeDefectQty */
  cumulativeDefectQty?: number
  /** 对应后端字段 randomCardNo */
  randomCardNo?: string
  /** 对应后端字段 occurrenceEngineering */
  occurrenceEngineering?: string
  /** 对应后端字段 testStep */
  testStep?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 repairOperator */
  repairOperator?: string
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
 * AssyDefectDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectDetailCreateDto）
 */
export interface AssyDefectDetailCreate {
  /** 对应后端字段 assyDefectId */
  assyDefectId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 defectCategory */
  defectCategory?: string
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 cumulativeDefectQty */
  cumulativeDefectQty: number
  /** 对应后端字段 randomCardNo */
  randomCardNo?: string
  /** 对应后端字段 occurrenceEngineering */
  occurrenceEngineering?: string
  /** 对应后端字段 testStep */
  testStep?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 repairOperator */
  repairOperator?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AssyDefectDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktAssyDefectDetailUpdateDto）
 */
export interface AssyDefectDetailUpdate extends AssyDefectDetailCreate {
  /** 对应后端字段 assyDefectDetailId */
  assyDefectDetailId: string
}
