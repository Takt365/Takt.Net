// 采购价格相关类型定义
export interface PurchasePrice {
  priceId: string
  supplierCode: string
  supplierName: string
  priceStatus: number
  createTime?: string
  updateTime?: string
}

export interface PurchasePriceQuery {
  pageIndex?: number
  pageSize?: number
  keyWords?: string
  supplierCode?: string
  supplierName?: string
  priceStatus?: number
}
