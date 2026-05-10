// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/report/report-execution-log
// 文件名称：report-execution-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：report-execution-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ReportExecutionLog类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportExecutionLogDto）
 */
export interface ReportExecutionLog extends TaktEntityBase {
  /** 对应后端字段 reportExecutionLogId */
  reportExecutionLogId: string
  /** 对应后端字段 reportId */
  reportId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 executionTime */
  executionTime: string
  /** 对应后端字段 variantName */
  variantName: string
  /** 对应后端字段 selectionParameters */
  selectionParameters: string
  /** 对应后端字段 layoutVariant */
  layoutVariant: string
  /** 对应后端字段 executionType */
  executionType: string
  /** 对应后端字段 backgroundJobName */
  backgroundJobName: string
  /** 对应后端字段 backgroundJobCount */
  backgroundJobCount: string
  /** 对应后端字段 executionDurationMs */
  executionDurationMs: number
  /** 对应后端字段 rowCount */
  rowCount: number
  /** 对应后端字段 isSuccess */
  isSuccess: number
  /** 对应后端字段 errorMessage */
  errorMessage: string
  /** 对应后端字段 messageType */
  messageType: string
  /** 对应后端字段 messageNumber */
  messageNumber: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 clientIp */
  clientIp: string
  /** 对应后端字段 terminalName */
  terminalName: string
  /** 对应后端字段 outputType */
  outputType: string
  /** 对应后端字段 spoolRequestNo */
  spoolRequestNo: string
  /** 对应后端字段 isExport */
  isExport: number
  /** 对应后端字段 exportFormat */
  exportFormat: string
  /** 对应后端字段 exportFilePath */
  exportFilePath: string
  /** 对应后端字段 isDownloaded */
  isDownloaded: number
  /** 对应后端字段 downloadTime */
  downloadTime: string
}

/**
 * ReportExecutionLogQuery类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportExecutionLogQueryDto）
 */
export interface ReportExecutionLogQuery extends TaktPagedQuery {
  /** 对应后端字段 reportId */
  reportId?: string
  /** 对应后端字段 userId */
  userId?: string
  /** 对应后端字段 executionTime */
  executionTime?: string
  /** 对应后端字段 executionTimeStart */
  executionTimeStart?: string
  /** 对应后端字段 executionTimeEnd */
  executionTimeEnd?: string
  /** 对应后端字段 variantName */
  variantName?: string
  /** 对应后端字段 selectionParameters */
  selectionParameters?: string
  /** 对应后端字段 layoutVariant */
  layoutVariant?: string
  /** 对应后端字段 executionType */
  executionType?: string
  /** 对应后端字段 backgroundJobName */
  backgroundJobName?: string
  /** 对应后端字段 backgroundJobCount */
  backgroundJobCount?: string
  /** 对应后端字段 executionDurationMs */
  executionDurationMs?: number
  /** 对应后端字段 rowCount */
  rowCount?: number
  /** 对应后端字段 isSuccess */
  isSuccess?: number
  /** 对应后端字段 errorMessage */
  errorMessage?: string
  /** 对应后端字段 messageType */
  messageType?: string
  /** 对应后端字段 messageNumber */
  messageNumber?: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 clientIp */
  clientIp?: string
  /** 对应后端字段 terminalName */
  terminalName?: string
  /** 对应后端字段 outputType */
  outputType?: string
  /** 对应后端字段 spoolRequestNo */
  spoolRequestNo?: string
  /** 对应后端字段 isExport */
  isExport?: number
  /** 对应后端字段 exportFormat */
  exportFormat?: string
  /** 对应后端字段 exportFilePath */
  exportFilePath?: string
  /** 对应后端字段 isDownloaded */
  isDownloaded?: number
  /** 对应后端字段 downloadTime */
  downloadTime?: string
  /** 对应后端字段 downloadTimeStart */
  downloadTimeStart?: string
  /** 对应后端字段 downloadTimeEnd */
  downloadTimeEnd?: string
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
 * ReportExecutionLogCreate类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportExecutionLogCreateDto）
 */
export interface ReportExecutionLogCreate {
  /** 对应后端字段 reportId */
  reportId: string
  /** 对应后端字段 userId */
  userId: string
  /** 对应后端字段 executionTime */
  executionTime: string
  /** 对应后端字段 variantName */
  variantName: string
  /** 对应后端字段 selectionParameters */
  selectionParameters: string
  /** 对应后端字段 layoutVariant */
  layoutVariant: string
  /** 对应后端字段 executionType */
  executionType: string
  /** 对应后端字段 backgroundJobName */
  backgroundJobName: string
  /** 对应后端字段 backgroundJobCount */
  backgroundJobCount: string
  /** 对应后端字段 executionDurationMs */
  executionDurationMs: number
  /** 对应后端字段 rowCount */
  rowCount: number
  /** 对应后端字段 isSuccess */
  isSuccess: number
  /** 对应后端字段 errorMessage */
  errorMessage: string
  /** 对应后端字段 messageType */
  messageType: string
  /** 对应后端字段 messageNumber */
  messageNumber: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 clientIp */
  clientIp: string
  /** 对应后端字段 terminalName */
  terminalName: string
  /** 对应后端字段 outputType */
  outputType: string
  /** 对应后端字段 spoolRequestNo */
  spoolRequestNo: string
  /** 对应后端字段 isExport */
  isExport: number
  /** 对应后端字段 exportFormat */
  exportFormat: string
  /** 对应后端字段 exportFilePath */
  exportFilePath: string
  /** 对应后端字段 isDownloaded */
  isDownloaded: number
  /** 对应后端字段 downloadTime */
  downloadTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ReportExecutionLogUpdate类型（对应后端 Takt.Application.Dtos.Statistics.Report.TaktReportExecutionLogUpdateDto）
 */
export interface ReportExecutionLogUpdate extends ReportExecutionLogCreate {
  /** 对应后端字段 reportExecutionLogId */
  reportExecutionLogId: string
}
