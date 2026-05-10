// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/asset-change-log
// 文件名称：asset-change-log.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：asset-change-log相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AssetChangeLog类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetChangeLogDto）
 */
export interface AssetChangeLog extends TaktEntityBase {
  /** 对应后端字段 assetChangeLogId */
  assetChangeLogId: string
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 assetCode */
  assetCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
}

/**
 * AssetChangeLogQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetChangeLogQueryDto）
 */
export interface AssetChangeLogQuery extends TaktPagedQuery {
  /** 对应后端字段 assetId */
  assetId?: string
  /** 对应后端字段 assetCode */
  assetCode?: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime?: string
  /** 对应后端字段 changeTimeStart */
  changeTimeStart?: string
  /** 对应后端字段 changeTimeEnd */
  changeTimeEnd?: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
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
 * AssetChangeLogCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetChangeLogCreateDto）
 */
export interface AssetChangeLogCreate {
  /** 对应后端字段 assetId */
  assetId: string
  /** 对应后端字段 assetCode */
  assetCode: string
  /** 对应后端字段 changeFields */
  changeFields?: string
  /** 对应后端字段 changeTime */
  changeTime: string
  /** 对应后端字段 changeBy */
  changeBy?: string
  /** 对应后端字段 changeReason */
  changeReason?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AssetChangeLogUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAssetChangeLogUpdateDto）
 */
export interface AssetChangeLogUpdate extends AssetChangeLogCreate {
  /** 对应后端字段 assetChangeLogId */
  assetChangeLogId: string
}
