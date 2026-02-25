import request from '@/api/request'

// 设备管理相关 API
// 注意：需要确认后端控制器名称，暂时使用 TaktEquipments
export function getEquipmentList(params: any) {
  return request({
    url: '/api/TaktEquipments/list',
    method: 'get',
    params
  })
}

export function getEquipmentById(id: string) {
  return request({
    url: `/api/TaktEquipments/${id}`,
    method: 'get'
  })
}

export function createEquipment(data: any) {
  return request({
    url: '/api/TaktEquipments',
    method: 'post',
    data
  })
}

export function updateEquipment(id: string, data: any) {
  return request({
    url: `/api/TaktEquipments/${id}`,
    method: 'put',
    data
  })
}

export function deleteEquipment(id: string) {
  return request({
    url: `/api/TaktEquipments/${id}`,
    method: 'delete'
  })
}
