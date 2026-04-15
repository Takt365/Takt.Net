import request, { type BlobDownloadWithMeta } from '@/api/request'

const purchasePriceUrl = '/api/TaktPurchasePrices'

// 采购价格相关 API
export function getPurchasePriceList(params: any) {
  return request({
    url: `${purchasePriceUrl}/list`,
    method: 'get',
    params
  })
}

export function getPurchasePriceById(id: string) {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'get'
  })
}

export function createPurchasePrice(data: any) {
  return request({
    url: purchasePriceUrl,
    method: 'post',
    data
  })
}

export function updatePurchasePrice(id: string, data: any) {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'put',
    data
  })
}

export function deletePurchasePrice(id: string) {
  return request({
    url: `${purchasePriceUrl}/${id}`,
    method: 'delete'
  })
}

export function getPurchasePriceTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchasePriceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

export function importPurchasePrice(file: File, sheetName?: string) {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${purchasePriceUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

export function exportPurchasePrice(query: any, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchasePriceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
