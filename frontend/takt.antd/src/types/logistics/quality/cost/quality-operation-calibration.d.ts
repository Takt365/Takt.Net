// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-operation-calibration
// 文件名称：quality-operation-calibration.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-operation-calibration相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityOperationCalibration类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCalibrationDto）
 */
export interface QualityOperationCalibration extends TaktEntityBase {
  /** 对应后端字段 qualityOperationCalibrationId */
  qualityOperationCalibrationId: string
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 calibrationCost */
  calibrationCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 externalAgentServiceFee */
  externalAgentServiceFee: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 calibrationNote */
  calibrationNote?: string
  /** 对应后端字段 operation */
  operation?: unknown
}

/**
 * QualityOperationCalibrationQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCalibrationQueryDto）
 */
export interface QualityOperationCalibrationQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 calibrationCost */
  calibrationCost?: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes?: number
  /** 对应后端字段 externalAgentServiceFee */
  externalAgentServiceFee?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 calibrationNote */
  calibrationNote?: string
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
 * QualityOperationCalibrationCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCalibrationCreateDto）
 */
export interface QualityOperationCalibrationCreate {
  /** 对应后端字段 qualityOperationId */
  qualityOperationId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 calibrationCost */
  calibrationCost: number
  /** 对应后端字段 workTimeMinutes */
  workTimeMinutes: number
  /** 对应后端字段 externalAgentServiceFee */
  externalAgentServiceFee: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 calibrationNote */
  calibrationNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityOperationCalibrationUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityOperationCalibrationUpdateDto）
 */
export interface QualityOperationCalibrationUpdate extends QualityOperationCalibrationCreate {
  /** 对应后端字段 qualityOperationCalibrationId */
  qualityOperationCalibrationId: string
}
