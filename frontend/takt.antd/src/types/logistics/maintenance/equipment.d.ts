// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/maintenance/equipment
// 文件名称：equipment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：equipment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Equipment类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentDto）
 */
export interface Equipment extends TaktEntityBase {
  /** 对应后端字段 equipmentId */
  equipmentId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentType */
  equipmentType: number
  /** 对应后端字段 equipmentModel */
  equipmentModel?: string
  /** 对应后端字段 equipmentSpecification */
  equipmentSpecification?: string
  /** 对应后端字段 equipmentBrand */
  equipmentBrand?: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 dealerBy */
  dealerBy?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 workshopBy */
  workshopBy?: string
  /** 对应后端字段 productionLineBy */
  productionLineBy?: string
  /** 对应后端字段 workstationBy */
  workstationBy?: string
  /** 对应后端字段 deptBy */
  deptBy?: string
  /** 对应后端字段 equipmentLocation */
  equipmentLocation?: string
  /** 对应后端字段 responsibleUserBy */
  responsibleUserBy?: string
  /** 对应后端字段 operatorBy */
  operatorBy?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 installationDate */
  installationDate?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 warrantyStartDate */
  warrantyStartDate?: string
  /** 对应后端字段 warrantyEndDate */
  warrantyEndDate?: string
  /** 对应后端字段 equipmentOriginalValue */
  equipmentOriginalValue: number
  /** 对应后端字段 technicalParameters */
  technicalParameters?: string
  /** 对应后端字段 equipmentImages */
  equipmentImages?: string
  /** 对应后端字段 equipmentDocuments */
  equipmentDocuments?: string
  /** 对应后端字段 isCritical */
  isCritical: number
  /** 对应后端字段 warrantyStatus */
  warrantyStatus: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 maintenanceRecords */
  maintenanceRecords?: unknown[]
}

/**
 * EquipmentQuery类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentQueryDto）
 */
export interface EquipmentQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 equipmentType */
  equipmentType?: number
  /** 对应后端字段 equipmentModel */
  equipmentModel?: string
  /** 对应后端字段 equipmentSpecification */
  equipmentSpecification?: string
  /** 对应后端字段 equipmentBrand */
  equipmentBrand?: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 dealerBy */
  dealerBy?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 workshopBy */
  workshopBy?: string
  /** 对应后端字段 productionLineBy */
  productionLineBy?: string
  /** 对应后端字段 workstationBy */
  workstationBy?: string
  /** 对应后端字段 deptBy */
  deptBy?: string
  /** 对应后端字段 equipmentLocation */
  equipmentLocation?: string
  /** 对应后端字段 responsibleUserBy */
  responsibleUserBy?: string
  /** 对应后端字段 operatorBy */
  operatorBy?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 purchaseDateStart */
  purchaseDateStart?: string
  /** 对应后端字段 purchaseDateEnd */
  purchaseDateEnd?: string
  /** 对应后端字段 installationDate */
  installationDate?: string
  /** 对应后端字段 installationDateStart */
  installationDateStart?: string
  /** 对应后端字段 installationDateEnd */
  installationDateEnd?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 startDateStart */
  startDateStart?: string
  /** 对应后端字段 startDateEnd */
  startDateEnd?: string
  /** 对应后端字段 warrantyStartDate */
  warrantyStartDate?: string
  /** 对应后端字段 warrantyStartDateStart */
  warrantyStartDateStart?: string
  /** 对应后端字段 warrantyStartDateEnd */
  warrantyStartDateEnd?: string
  /** 对应后端字段 warrantyEndDate */
  warrantyEndDate?: string
  /** 对应后端字段 warrantyEndDateStart */
  warrantyEndDateStart?: string
  /** 对应后端字段 warrantyEndDateEnd */
  warrantyEndDateEnd?: string
  /** 对应后端字段 equipmentOriginalValue */
  equipmentOriginalValue?: number
  /** 对应后端字段 technicalParameters */
  technicalParameters?: string
  /** 对应后端字段 equipmentImages */
  equipmentImages?: string
  /** 对应后端字段 equipmentDocuments */
  equipmentDocuments?: string
  /** 对应后端字段 isCritical */
  isCritical?: number
  /** 对应后端字段 warrantyStatus */
  warrantyStatus?: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus?: number
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
 * EquipmentCreate类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentCreateDto）
 */
export interface EquipmentCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentType */
  equipmentType: number
  /** 对应后端字段 equipmentModel */
  equipmentModel?: string
  /** 对应后端字段 equipmentSpecification */
  equipmentSpecification?: string
  /** 对应后端字段 equipmentBrand */
  equipmentBrand?: string
  /** 对应后端字段 manufacturer */
  manufacturer?: string
  /** 对应后端字段 dealerBy */
  dealerBy?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 workshopBy */
  workshopBy?: string
  /** 对应后端字段 productionLineBy */
  productionLineBy?: string
  /** 对应后端字段 workstationBy */
  workstationBy?: string
  /** 对应后端字段 deptBy */
  deptBy?: string
  /** 对应后端字段 equipmentLocation */
  equipmentLocation?: string
  /** 对应后端字段 responsibleUserBy */
  responsibleUserBy?: string
  /** 对应后端字段 operatorBy */
  operatorBy?: string
  /** 对应后端字段 purchaseDate */
  purchaseDate?: string
  /** 对应后端字段 installationDate */
  installationDate?: string
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 warrantyStartDate */
  warrantyStartDate?: string
  /** 对应后端字段 warrantyEndDate */
  warrantyEndDate?: string
  /** 对应后端字段 equipmentOriginalValue */
  equipmentOriginalValue: number
  /** 对应后端字段 technicalParameters */
  technicalParameters?: string
  /** 对应后端字段 equipmentImages */
  equipmentImages?: string
  /** 对应后端字段 equipmentDocuments */
  equipmentDocuments?: string
  /** 对应后端字段 isCritical */
  isCritical: number
  /** 对应后端字段 warrantyStatus */
  warrantyStatus: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 maintenanceRecords */
  maintenanceRecords?: unknown[]
}

/**
 * EquipmentUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentUpdateDto）
 */
export interface EquipmentUpdate extends EquipmentCreate {
  /** 对应后端字段 equipmentId */
  equipmentId: string
}

/**
 * EquipmentStatus类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentStatusDto）
 */
export interface EquipmentStatus {
  /** 对应后端字段 equipmentId */
  equipmentId: string
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
}

/**
 * EquipmentWarrantyStatus类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentWarrantyStatusDto）
 */
export interface EquipmentWarrantyStatus {
  /** 对应后端字段 equipmentId */
  equipmentId: string
  /** 对应后端字段 warrantyStatus */
  warrantyStatus: number
}
