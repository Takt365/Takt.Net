import request from '@/api/request'

const equipmentUrl = '/api/TaktEquipments'

// 设备管理相关 API
export function getEquipmentList(params: any) {
  return request({
    url: `${equipmentUrl}/list`,
    method: 'get',
    params
  })
}

export function getEquipmentById(id: string) {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'get'
  })
}

export function createEquipment(data: any) {
  return request({
    url: equipmentUrl,
    method: 'post',
    data
  })
}

export function updateEquipment(id: string, data: any) {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'put',
    data
  })
}

export function deleteEquipment(id: string) {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'delete'
  })
}
