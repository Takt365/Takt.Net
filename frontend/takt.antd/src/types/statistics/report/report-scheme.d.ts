// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/report/report-scheme
// 文件名称：report-scheme.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：report-scheme相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ReportScheme类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeDto）
 */
export interface ReportScheme extends TaktEntityBase {
  /** 对应后端字段 reportSchemeId */
  reportSchemeId: string
  /** 对应后端字段 reportCode */
  reportCode: string
  /** 对应后端字段 reportName */
  reportName: string
  /** 对应后端字段 reportCategory */
  reportCategory: string
  /** 对应后端字段 applicationModule */
  applicationModule: string
  /** 对应后端字段 reportDescription */
  reportDescription: string
  /** 对应后端字段 selectionScreenConfig */
  selectionScreenConfig: string
  /** 对应后端字段 dataSourceType */
  dataSourceType: string
  /** 对应后端字段 dataSourceName */
  dataSourceName: string
  /** 对应后端字段 sqlQuery */
  sqlQuery: string
  /** 对应后端字段 outputType */
  outputType: string
  /** 对应后端字段 alvColumnConfig */
  alvColumnConfig: string
  /** 对应后端字段 defaultLayoutVariant */
  defaultLayoutVariant: string
  /** 对应后端字段 supportLayoutVariant */
  supportLayoutVariant: number
  /** 对应后端字段 subtotalFields */
  subtotalFields: string
  /** 对应后端字段 sortFields */
  sortFields: string
  /** 对应后端字段 filterConfig */
  filterConfig: string
  /** 对应后端字段 supportTotal */
  supportTotal: number
  /** 对应后端字段 supportSubtotal */
  supportSubtotal: number
  /** 对应后端字段 supportAggregation */
  supportAggregation: number
  /** 对应后端字段 supportDrillDown */
  supportDrillDown: number
  /** 对应后端字段 drillDownReportCode */
  drillDownReportCode: string
  /** 对应后端字段 supportBackground */
  supportBackground: number
  /** 对应后端字段 supportVariantSave */
  supportVariantSave: number
  /** 对应后端字段 defaultPageSize */
  defaultPageSize: number
  /** 对应后端字段 maxRowCount */
  maxRowCount: number
  /** 对应后端字段 isExportable */
  isExportable: number
  /** 对应后端字段 exportFormats */
  exportFormats: string
  /** 对应后端字段 isPrintable */
  isPrintable: number
  /** 对应后端字段 printTemplate */
  printTemplate: string
  /** 对应后端字段 applicablePlantCodes */
  applicablePlantCodes: string
  /** 对应后端字段 applicableCompanyCodes */
  applicableCompanyCodes: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicableRoles */
  applicableRoles: string
  /** 对应后端字段 developmentClass */
  developmentClass: string
  /** 对应后端字段 author */
  author: string
  /** 对应后端字段 version */
  version: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
}

/**
 * ReportSchemeQuery类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeQueryDto）
 */
export interface ReportSchemeQuery extends TaktPagedQuery {
  /** 对应后端字段 reportCode */
  reportCode?: string
  /** 对应后端字段 reportName */
  reportName?: string
  /** 对应后端字段 reportCategory */
  reportCategory?: string
  /** 对应后端字段 applicationModule */
  applicationModule?: string
  /** 对应后端字段 reportDescription */
  reportDescription?: string
  /** 对应后端字段 selectionScreenConfig */
  selectionScreenConfig?: string
  /** 对应后端字段 dataSourceType */
  dataSourceType?: string
  /** 对应后端字段 dataSourceName */
  dataSourceName?: string
  /** 对应后端字段 sqlQuery */
  sqlQuery?: string
  /** 对应后端字段 outputType */
  outputType?: string
  /** 对应后端字段 alvColumnConfig */
  alvColumnConfig?: string
  /** 对应后端字段 defaultLayoutVariant */
  defaultLayoutVariant?: string
  /** 对应后端字段 supportLayoutVariant */
  supportLayoutVariant?: number
  /** 对应后端字段 subtotalFields */
  subtotalFields?: string
  /** 对应后端字段 sortFields */
  sortFields?: string
  /** 对应后端字段 filterConfig */
  filterConfig?: string
  /** 对应后端字段 supportTotal */
  supportTotal?: number
  /** 对应后端字段 supportSubtotal */
  supportSubtotal?: number
  /** 对应后端字段 supportAggregation */
  supportAggregation?: number
  /** 对应后端字段 supportDrillDown */
  supportDrillDown?: number
  /** 对应后端字段 drillDownReportCode */
  drillDownReportCode?: string
  /** 对应后端字段 supportBackground */
  supportBackground?: number
  /** 对应后端字段 supportVariantSave */
  supportVariantSave?: number
  /** 对应后端字段 defaultPageSize */
  defaultPageSize?: number
  /** 对应后端字段 maxRowCount */
  maxRowCount?: number
  /** 对应后端字段 isExportable */
  isExportable?: number
  /** 对应后端字段 exportFormats */
  exportFormats?: string
  /** 对应后端字段 isPrintable */
  isPrintable?: number
  /** 对应后端字段 printTemplate */
  printTemplate?: string
  /** 对应后端字段 applicablePlantCodes */
  applicablePlantCodes?: string
  /** 对应后端字段 applicableCompanyCodes */
  applicableCompanyCodes?: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment?: string
  /** 对应后端字段 applicableRoles */
  applicableRoles?: string
  /** 对应后端字段 developmentClass */
  developmentClass?: string
  /** 对应后端字段 author */
  author?: string
  /** 对应后端字段 version */
  version?: string
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
 * ReportSchemeCreate类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeCreateDto）
 */
export interface ReportSchemeCreate {
  /** 对应后端字段 reportCode */
  reportCode: string
  /** 对应后端字段 reportName */
  reportName: string
  /** 对应后端字段 reportCategory */
  reportCategory: string
  /** 对应后端字段 applicationModule */
  applicationModule: string
  /** 对应后端字段 reportDescription */
  reportDescription: string
  /** 对应后端字段 selectionScreenConfig */
  selectionScreenConfig: string
  /** 对应后端字段 dataSourceType */
  dataSourceType: string
  /** 对应后端字段 dataSourceName */
  dataSourceName: string
  /** 对应后端字段 sqlQuery */
  sqlQuery: string
  /** 对应后端字段 outputType */
  outputType: string
  /** 对应后端字段 alvColumnConfig */
  alvColumnConfig: string
  /** 对应后端字段 defaultLayoutVariant */
  defaultLayoutVariant: string
  /** 对应后端字段 supportLayoutVariant */
  supportLayoutVariant: number
  /** 对应后端字段 subtotalFields */
  subtotalFields: string
  /** 对应后端字段 sortFields */
  sortFields: string
  /** 对应后端字段 filterConfig */
  filterConfig: string
  /** 对应后端字段 supportTotal */
  supportTotal: number
  /** 对应后端字段 supportSubtotal */
  supportSubtotal: number
  /** 对应后端字段 supportAggregation */
  supportAggregation: number
  /** 对应后端字段 supportDrillDown */
  supportDrillDown: number
  /** 对应后端字段 drillDownReportCode */
  drillDownReportCode: string
  /** 对应后端字段 supportBackground */
  supportBackground: number
  /** 对应后端字段 supportVariantSave */
  supportVariantSave: number
  /** 对应后端字段 defaultPageSize */
  defaultPageSize: number
  /** 对应后端字段 maxRowCount */
  maxRowCount: number
  /** 对应后端字段 isExportable */
  isExportable: number
  /** 对应后端字段 exportFormats */
  exportFormats: string
  /** 对应后端字段 isPrintable */
  isPrintable: number
  /** 对应后端字段 printTemplate */
  printTemplate: string
  /** 对应后端字段 applicablePlantCodes */
  applicablePlantCodes: string
  /** 对应后端字段 applicableCompanyCodes */
  applicableCompanyCodes: string
  /** 对应后端字段 applicableDepartment */
  applicableDepartment: string
  /** 对应后端字段 applicableRoles */
  applicableRoles: string
  /** 对应后端字段 developmentClass */
  developmentClass: string
  /** 对应后端字段 author */
  author: string
  /** 对应后端字段 version */
  version: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ReportSchemeUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeUpdateDto）
 */
export interface ReportSchemeUpdate extends ReportSchemeCreate {
  /** 对应后端字段 reportSchemeId */
  reportSchemeId: string
}

/**
 * ReportSchemeStatus类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeStatusDto）
 */
export interface ReportSchemeStatus {
  /** 对应后端字段 reportSchemeId */
  reportSchemeId: string
  /** 对应后端字段 status */
  status: number
}

/**
 * ReportSchemeSort类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportSchemeSortDto）
 */
export interface ReportSchemeSort {
  /** 对应后端字段 reportSchemeId */
  reportSchemeId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
