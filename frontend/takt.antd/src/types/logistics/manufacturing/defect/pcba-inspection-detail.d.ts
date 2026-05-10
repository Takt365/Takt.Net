// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/defect/pcba-inspection-detail
// 文件名称：pcba-inspection-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：pcba-inspection-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * PcbaInspectionDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetailDto）
 */
export interface PcbaInspectionDetail extends TaktEntityBase {
  /** 对应后端字段 pcbaInspectionDetailId */
  pcbaInspectionDetailId: string
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 visualInspectionLine */
  visualInspectionLine?: string
  /** 对应后端字段 aoiLine */
  aoiLine?: string
  /** 对应后端字段 bSideAssemblyDate */
  bSideAssemblyDate?: string
  /** 对应后端字段 tSideAssemblyDate */
  tSideAssemblyDate?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty: number
  /** 对应后端字段 inspectionQty */
  inspectionQty: number
  /** 对应后端字段 inspectionStatus */
  inspectionStatus: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 inspectionWorkHours */
  inspectionWorkHours: number
  /** 对应后端字段 aoiWorkHours */
  aoiWorkHours: number
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 handPlacement */
  handPlacement?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
  /** 对应后端字段 pcbaInspection */
  pcbaInspection?: unknown
}

/**
 * PcbaInspectionDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetailQueryDto）
 */
export interface PcbaInspectionDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 visualInspectionLine */
  visualInspectionLine?: string
  /** 对应后端字段 aoiLine */
  aoiLine?: string
  /** 对应后端字段 bSideAssemblyDate */
  bSideAssemblyDate?: string
  /** 对应后端字段 bSideAssemblyDateStart */
  bSideAssemblyDateStart?: string
  /** 对应后端字段 bSideAssemblyDateEnd */
  bSideAssemblyDateEnd?: string
  /** 对应后端字段 tSideAssemblyDate */
  tSideAssemblyDate?: string
  /** 对应后端字段 tSideAssemblyDateStart */
  tSideAssemblyDateStart?: string
  /** 对应后端字段 tSideAssemblyDateEnd */
  tSideAssemblyDateEnd?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty?: number
  /** 对应后端字段 inspectionQty */
  inspectionQty?: number
  /** 对应后端字段 inspectionStatus */
  inspectionStatus?: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 inspectionWorkHours */
  inspectionWorkHours?: number
  /** 对应后端字段 aoiWorkHours */
  aoiWorkHours?: number
  /** 对应后端字段 defectQty */
  defectQty?: number
  /** 对应后端字段 handPlacement */
  handPlacement?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
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
 * PcbaInspectionDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetailCreateDto）
 */
export interface PcbaInspectionDetailCreate {
  /** 对应后端字段 pcbaInspectionId */
  pcbaInspectionId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 pcbaBoardType */
  pcbaBoardType?: string
  /** 对应后端字段 visualInspectionLine */
  visualInspectionLine?: string
  /** 对应后端字段 aoiLine */
  aoiLine?: string
  /** 对应后端字段 bSideAssemblyDate */
  bSideAssemblyDate?: string
  /** 对应后端字段 tSideAssemblyDate */
  tSideAssemblyDate?: string
  /** 对应后端字段 shiftNo */
  shiftNo: number
  /** 对应后端字段 inspectorName */
  inspectorName?: string
  /** 对应后端字段 dailyCompletedQty */
  dailyCompletedQty: number
  /** 对应后端字段 inspectionQty */
  inspectionQty: number
  /** 对应后端字段 inspectionStatus */
  inspectionStatus: number
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 inspectionWorkHours */
  inspectionWorkHours: number
  /** 对应后端字段 aoiWorkHours */
  aoiWorkHours: number
  /** 对应后端字段 defectQty */
  defectQty: number
  /** 对应后端字段 handPlacement */
  handPlacement?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 defectLocation */
  defectLocation?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * PcbaInspectionDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetailUpdateDto）
 */
export interface PcbaInspectionDetailUpdate extends PcbaInspectionDetailCreate {
  /** 对应后端字段 pcbaInspectionDetailId */
  pcbaInspectionDetailId: string
}

/**
 * PcbaInspectionDetailInspectionStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetailInspectionStatusDto）
 */
export interface PcbaInspectionDetailInspectionStatus {
  /** 对应后端字段 pcbaInspectionDetailId */
  pcbaInspectionDetailId: string
  /** 对应后端字段 inspectionStatus */
  inspectionStatus: number
}
