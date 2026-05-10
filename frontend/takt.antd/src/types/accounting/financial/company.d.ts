// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/company
// 文件名称：company.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：company相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Company类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyDto）
 */
export interface Company extends TaktEntityBase {
  /** 对应后端字段 companyId */
  companyId: string
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 companyShortName */
  companyShortName?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 companyPhone */
  companyPhone?: string
  /** 对应后端字段 companyEmail */
  companyEmail?: string
  /** 对应后端字段 companyFax */
  companyFax?: string
  /** 对应后端字段 companyWebsite */
  companyWebsite?: string
  /** 对应后端字段 unifiedSocialCreditCode */
  unifiedSocialCreditCode?: string
  /** 对应后端字段 taxRegistrationNumber */
  taxRegistrationNumber?: string
  /** 对应后端字段 legalRepresentative */
  legalRepresentative?: string
  /** 对应后端字段 companyManager */
  companyManager?: string
  /** 对应后端字段 registeredCapital */
  registeredCapital: number
  /** 对应后端字段 establishmentDate */
  establishmentDate?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 companyStatus */
  companyStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CompanyQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyQueryDto）
 */
export interface CompanyQuery extends TaktPagedQuery {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 companyName */
  companyName?: string
  /** 对应后端字段 companyShortName */
  companyShortName?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 companyPhone */
  companyPhone?: string
  /** 对应后端字段 companyEmail */
  companyEmail?: string
  /** 对应后端字段 companyFax */
  companyFax?: string
  /** 对应后端字段 companyWebsite */
  companyWebsite?: string
  /** 对应后端字段 unifiedSocialCreditCode */
  unifiedSocialCreditCode?: string
  /** 对应后端字段 taxRegistrationNumber */
  taxRegistrationNumber?: string
  /** 对应后端字段 legalRepresentative */
  legalRepresentative?: string
  /** 对应后端字段 companyManager */
  companyManager?: string
  /** 对应后端字段 registeredCapital */
  registeredCapital?: number
  /** 对应后端字段 establishmentDate */
  establishmentDate?: string
  /** 对应后端字段 establishmentDateStart */
  establishmentDateStart?: string
  /** 对应后端字段 establishmentDateEnd */
  establishmentDateEnd?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 companyStatus */
  companyStatus?: number
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
 * CompanyCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyCreateDto）
 */
export interface CompanyCreate {
  /** 对应后端字段 companyCode */
  companyCode: string
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 companyShortName */
  companyShortName?: string
  /** 对应后端字段 registrationRegion */
  registrationRegion?: string
  /** 对应后端字段 registrationProvince */
  registrationProvince?: string
  /** 对应后端字段 registrationCity */
  registrationCity?: string
  /** 对应后端字段 registrationAddress */
  registrationAddress?: string
  /** 对应后端字段 businessRegion */
  businessRegion?: string
  /** 对应后端字段 businessProvince */
  businessProvince?: string
  /** 对应后端字段 businessCity */
  businessCity?: string
  /** 对应后端字段 businessAddress */
  businessAddress?: string
  /** 对应后端字段 companyPhone */
  companyPhone?: string
  /** 对应后端字段 companyEmail */
  companyEmail?: string
  /** 对应后端字段 companyFax */
  companyFax?: string
  /** 对应后端字段 companyWebsite */
  companyWebsite?: string
  /** 对应后端字段 unifiedSocialCreditCode */
  unifiedSocialCreditCode?: string
  /** 对应后端字段 taxRegistrationNumber */
  taxRegistrationNumber?: string
  /** 对应后端字段 legalRepresentative */
  legalRepresentative?: string
  /** 对应后端字段 companyManager */
  companyManager?: string
  /** 对应后端字段 registeredCapital */
  registeredCapital: number
  /** 对应后端字段 establishmentDate */
  establishmentDate?: string
  /** 对应后端字段 enterpriseNature */
  enterpriseNature?: string
  /** 对应后端字段 industryAttribute */
  industryAttribute?: string
  /** 对应后端字段 enterpriseScale */
  enterpriseScale?: string
  /** 对应后端字段 businessScope */
  businessScope?: string
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 companyStatus */
  companyStatus: number
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CompanyUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyUpdateDto）
 */
export interface CompanyUpdate extends CompanyCreate {
  /** 对应后端字段 companyId */
  companyId: string
}

/**
 * CompanyStatus类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanyStatusDto）
 */
export interface CompanyStatus {
  /** 对应后端字段 companyId */
  companyId: string
  /** 对应后端字段 companyStatus */
  companyStatus: number
}

/**
 * CompanySort类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktCompanySortDto）
 */
export interface CompanySort {
  /** 对应后端字段 companyId */
  companyId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
