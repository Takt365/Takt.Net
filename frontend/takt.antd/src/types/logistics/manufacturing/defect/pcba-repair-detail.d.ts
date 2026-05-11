// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/pcba-repair-detail
// 文件名称：pcba-repair-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-repair-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaRepairDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairDetailDto）
 */
export interface PcbaRepairDetail extends TaktEntityBase {
  /** 对应后端字段 pcbaRepairDetailId */
  pcbaRepairDetailId: string
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 cardNo */
  cardNo?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectEngineering */
  defectEngineering?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 defectResponsibility */
  defectResponsibility?: string
  /** 对应后端字段 defectNature */
  defectNature?: string
  /** 对应后端字段 repairOperator */
  repairOperator?: string
  /** 对应后端字段 pcbaRepair */
  pcbaRepair?: unknown
}

/**
 * PcbaRepairDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairDetailQueryDto）
 */
export interface PcbaRepairDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 prodActualQty */
  prodActualQty?: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 cardNo */
  cardNo?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectEngineering */
  defectEngineering?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 defectQty */
  defectQty?: number
  /** 对应后端字段 defectResponsibility */
  defectResponsibility?: string
  /** 对应后端字段 defectNature */
  defectNature?: string
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
 * PcbaRepairDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairDetailCreateDto）
 */
export interface PcbaRepairDetailCreate {
  /** 对应后端字段 pcbaRepairId */
  pcbaRepairId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 cardNo */
  cardNo?: string
  /** 对应后端字段 defectSymptom */
  defectSymptom?: string
  /** 对应后端字段 defectEngineering */
  defectEngineering?: string
  /** 对应后端字段 defectReason */
  defectReason?: string
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 defectResponsibility */
  defectResponsibility?: string
  /** 对应后端字段 defectNature */
  defectNature?: string
  /** 对应后端字段 repairOperator */
  repairOperator?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PcbaRepairDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaRepairDetailUpdateDto）
 */
export interface PcbaRepairDetailUpdate extends PcbaRepairDetailCreate {
  /** 对应后端字段 pcbaRepairDetailId */
  pcbaRepairDetailId: string
}
