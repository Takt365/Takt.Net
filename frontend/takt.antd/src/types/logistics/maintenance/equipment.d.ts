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
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentCategoryId */
  equipmentCategoryId: string
  /** 对应后端字段 equipmentCategoryName */
  equipmentCategoryName?: string
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
  /** 对应后端字段 supplierId */
  supplierId?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 workshopId */
  workshopId?: string
  /** 对应后端字段 workshopName */
  workshopName?: string
  /** 对应后端字段 productionLineId */
  productionLineId?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 workstationId */
  workstationId?: string
  /** 对应后端字段 workstationName */
  workstationName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 equipmentLocation */
  equipmentLocation?: string
  /** 对应后端字段 responsibleUserId */
  responsibleUserId?: string
  /** 对应后端字段 responsibleUserName */
  responsibleUserName?: string
  /** 对应后端字段 operatorId */
  operatorId?: string
  /** 对应后端字段 operatorName */
  operatorName?: string
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
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 lastMaintenanceDate */
  lastMaintenanceDate?: string
  /** 对应后端字段 nextMaintenanceDate */
  nextMaintenanceDate?: string
  /** 对应后端字段 maintenanceCycleDays */
  maintenanceCycleDays: number
  /** 对应后端字段 totalRunningHours */
  totalRunningHours: number
  /** 对应后端字段 totalDowntimeHours */
  totalDowntimeHours: number
}

/**
 * EquipmentQuery类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentQueryDto）
 */
export interface EquipmentQuery extends TaktPagedQuery {
  /** 对应后端字段 equipmentCode */
  equipmentCode?: string
  /** 对应后端字段 equipmentName */
  equipmentName?: string
  /** 对应后端字段 equipmentCategoryId */
  equipmentCategoryId?: string
  /** 对应后端字段 equipmentType */
  equipmentType?: number
  /** 对应后端字段 workshopId */
  workshopId?: string
  /** 对应后端字段 productionLineId */
  productionLineId?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 isEnabled */
  isEnabled?: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus?: number
}

/**
 * EquipmentCreate类型（对应后端 Takt.Application.Dtos.Logistics.Maintenance.TaktEquipmentCreateDto）
 */
export interface EquipmentCreate {
  /** 对应后端字段 equipmentCode */
  equipmentCode: string
  /** 对应后端字段 equipmentName */
  equipmentName: string
  /** 对应后端字段 equipmentCategoryId */
  equipmentCategoryId: string
  /** 对应后端字段 equipmentCategoryName */
  equipmentCategoryName?: string
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
  /** 对应后端字段 supplierId */
  supplierId?: string
  /** 对应后端字段 supplierName */
  supplierName?: string
  /** 对应后端字段 serialNumber */
  serialNumber?: string
  /** 对应后端字段 workshopId */
  workshopId?: string
  /** 对应后端字段 workshopName */
  workshopName?: string
  /** 对应后端字段 productionLineId */
  productionLineId?: string
  /** 对应后端字段 productionLineName */
  productionLineName?: string
  /** 对应后端字段 workstationId */
  workstationId?: string
  /** 对应后端字段 workstationName */
  workstationName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 equipmentLocation */
  equipmentLocation?: string
  /** 对应后端字段 responsibleUserId */
  responsibleUserId?: string
  /** 对应后端字段 responsibleUserName */
  responsibleUserName?: string
  /** 对应后端字段 operatorId */
  operatorId?: string
  /** 对应后端字段 operatorName */
  operatorName?: string
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
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 equipmentStatus */
  equipmentStatus: number
  /** 对应后端字段 lastMaintenanceDate */
  lastMaintenanceDate?: string
  /** 对应后端字段 nextMaintenanceDate */
  nextMaintenanceDate?: string
  /** 对应后端字段 maintenanceCycleDays */
  maintenanceCycleDays: number
  /** 对应后端字段 totalRunningHours */
  totalRunningHours: number
  /** 对应后端字段 totalDowntimeHours */
  totalDowntimeHours: number
  /** 对应后端字段 remark */
  remark?: string
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
