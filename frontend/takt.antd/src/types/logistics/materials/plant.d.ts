// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/materials/plant
// 文件名称：plant.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表相关类型定义，对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantDtos
// 规则：主类型 extends TaktEntityBase；查询 extends TaktPagedQuery；更新 extends Create；其它类型不继承。
// 字段名均为小驼峰（与后端 Dto 属性一一对应，后端 PascalCase 对应前端 camelCase）。不生成模板/导入/导出类型。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 工厂表Dto类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantDto）
 */
export interface Plant extends TaktEntityBase {
  /** 工厂表ID（对应后端 PlantId，序列化为string以避免Javascript精度问题） */
  plantId: string
  /** 工厂代码 */
  plantCode: string
  /** 工厂名称 */
  plantName: string
  /** 工厂简称 */
  plantShortName?: string
  /** 注册地址 */
  registrationAddress?: string
  /** 注册地区-国家 */
  registrationRegion?: string
  /** 注册地区-省 */
  registrationProvince?: string
  /** 注册地区-市 */
  registrationCity?: string
  /** 经营地区-国家 */
  businessRegion?: string
  /** 经营地区-省 */
  businessProvince?: string
  /** 经营地区-市 */
  businessCity?: string
  /** 经营地址 */
  businessAddress?: string
  /** 工厂地址 */
  plantAddress?: string
  /** 工厂电话 */
  plantPhone?: string
  /** 工厂邮箱 */
  plantEmail?: string
  /** 工厂负责人 */
  plantManager?: string
  /** 企业性质 */
  enterpriseNature?: string
  /** 行业属性 */
  industryAttribute?: string
  /** 企业规模 */
  enterpriseScale?: string
  /** 经营范围 */
  businessScope?: string
  /** 关联公司 */
  relatedCompany?: string
  /** 工厂状态 */
  plantStatus: number
  /** 排序号 */
  orderNum: number
  /** 扩展字段JSON */
  extFieldJson?: string
  /** 备注 */
  remark?: string
}

/**
 * 工厂表QueryDto类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantQueryDto）
 */
export interface PlantQuery extends TaktPagedQuery {
  /** 工厂代码 */
  plantCode?: string
  /** 工厂名称 */
  plantName?: string
}

/**
 * 工厂表CreateDto类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantCreateDto）
 */
export interface PlantCreate {
  /** 工厂代码 */
  plantCode: string
  /** 工厂名称 */
  plantName: string
  /** 工厂简称 */
  plantShortName?: string
  /** 注册地址 */
  registrationAddress?: string
  /** 注册地区-国家 */
  registrationRegion?: string
  /** 注册地区-省 */
  registrationProvince?: string
  /** 注册地区-市 */
  registrationCity?: string
  /** 经营地区-国家 */
  businessRegion?: string
  /** 经营地区-省 */
  businessProvince?: string
  /** 经营地区-市 */
  businessCity?: string
  /** 经营地址 */
  businessAddress?: string
  /** 工厂地址 */
  plantAddress?: string
  /** 工厂电话 */
  plantPhone?: string
  /** 工厂邮箱 */
  plantEmail?: string
  /** 工厂负责人 */
  plantManager?: string
  /** 企业性质 */
  enterpriseNature?: string
  /** 行业属性 */
  industryAttribute?: string
  /** 企业规模 */
  enterpriseScale?: string
  /** 经营范围 */
  businessScope?: string
  /** 关联公司 */
  relatedCompany?: string
  /** 工厂状态 */
  plantStatus: number
  /** 排序号 */
  orderNum: number
  /** 扩展字段JSON */
  extFieldJson?: string
  /** 备注 */
  remark?: string
}

/**
 * 工厂表UpdateDto类型（对应后端 Takt.Application.Dtos.Logistics.Materials.TaktPlantUpdateDto）
 */
export interface PlantUpdate extends TaktEntityBase {
  /** 工厂表ID（对应后端 PlantId，序列化为string以避免Javascript精度问题） */
  plantId: string
}
