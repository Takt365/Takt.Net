import request, { type BlobDownloadWithMeta } from '@/api/request'

const purchaseOrderUrl = '/api/TaktPurchaseOrders'

// 采购订单相关 API
export function getPurchaseOrderList(params: any) {
  return request({
    url: `${purchaseOrderUrl}/list`,
    method: 'get',
    params
  })
}

export function getPurchaseOrderById(id: string) {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'get'
  })
}

export function createPurchaseOrder(data: any) {
  return request({
    url: purchaseOrderUrl,
    method: 'post',
    data
  })
}

export function updatePurchaseOrder(id: string, data: any) {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'put',
    data
  })
}

export function deletePurchaseOrder(id: string) {
  return request({
    url: `${purchaseOrderUrl}/${id}`,
    method: 'delete'
  })
}

export function getPurchaseOrderTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseOrderUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

export function importPurchaseOrder(file: File, sheetName?: string) {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${purchaseOrderUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

export function exportPurchaseOrder(query: any, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseOrderUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
