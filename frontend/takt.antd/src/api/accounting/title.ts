import request from '@/api/request'

const titleUrl = '/api/TaktAccountingTitles'

// 会计科目相关 API
export function getAccountingTitleList(params: any) {
  return request({
    url: `${titleUrl}/list`,
    method: 'get',
    params
  })
}

export function getAccountingTitleById(id: string) {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'get'
  })
}

export function createAccountingTitle(data: any) {
  return request({
    url: titleUrl,
    method: 'post',
    data
  })
}

export function updateAccountingTitle(id: string, data: any) {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'put',
    data
  })
}

export function deleteAccountingTitle(id: string) {
  return request({
    url: `${titleUrl}/${id}`,
    method: 'delete'
  })
}
