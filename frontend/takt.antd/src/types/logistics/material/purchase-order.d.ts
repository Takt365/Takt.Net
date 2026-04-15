// 采购订单相关类型定义
export interface PurchaseOrder {
  orderId: string
  orderCode: string
  supplierCode: string
  supplierName: string
  orderDate: string
  totalAmount: number
  orderStatus: number
  createTime?: string
  updateTime?: string
}

export interface PurchaseOrderQuery {
  pageIndex?: number
  pageSize?: number
  keyWords?: string
  orderCode?: string
  supplierCode?: string
  orderStatus?: number
}
