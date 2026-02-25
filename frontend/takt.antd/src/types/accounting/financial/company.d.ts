// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/company
// 文件名称：company.d.ts
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：公司相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 公司类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyDto）
 */
export interface Company extends Record<string, any> {
  companyId: string
  companyCode: string
  companyName: string
  companyName2?: string
  shortName?: string
  region?: string
  province?: string
  city?: string
  county?: string
  townStreet?: string
  village?: string
  address?: string
  localCurrency?: string
  languageCode?: string
  chartOfAccounts: string
  controllingArea?: string
  purchasingOrg?: string
  companyPhone?: string
  companyEmail?: string
  companyFax?: string
  companyWebsite?: string
  unifiedSocialCreditCode?: string
  taxRegistrationNumber?: string
  vatRegistrationNumber?: string
  legalRepresentative?: string
  industryType?: string
  enterpriseRegistrationType?: string
  enterpriseSize?: number
  registeredCapital: number
  establishmentDate?: string
  dissolutionDate?: string
  companyStatus: number
  orderNum: number
  configId?: string
  remark?: string
  createBy?: string
  createTime?: string
  updateBy?: string
  updateTime?: string
}

/**
 * 公司查询类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyQueryDto）
 */
export interface CompanyQuery extends TaktPagedQuery {
  keyWords?: string
  companyCode?: string
  companyName?: string
  companyStatus?: number
}

/**
 * 创建公司类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyCreateDto）
 */
export interface CompanyCreate {
  companyCode: string
  companyName: string
  companyName2?: string
  shortName?: string
  region?: string
  province?: string
  city?: string
  county?: string
  townStreet?: string
  village?: string
  address?: string
  localCurrency?: string
  languageCode?: string
  chartOfAccounts?: string
  controllingArea?: string
  purchasingOrg?: string
  companyPhone?: string
  companyEmail?: string
  companyFax?: string
  companyWebsite?: string
  unifiedSocialCreditCode?: string
  taxRegistrationNumber?: string
  vatRegistrationNumber?: string
  legalRepresentative?: string
  industryType?: string
  enterpriseRegistrationType?: string
  enterpriseSize?: number
  registeredCapital?: number
  establishmentDate?: string
  dissolutionDate?: string
  orderNum?: number
  remark?: string
}

/**
 * 更新公司类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyUpdateDto）
 */
export interface CompanyUpdate extends CompanyCreate {
  companyId: string
}

/**
 * 公司状态类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyStatusDto）
 */
export interface CompanyStatus {
  companyId: string
  companyStatus: number
}
