import request from '@/api/request'
import type { Equipment, EquipmentQuery } from '@/types/logistics/maintenance/equipment'

const equipmentUrl = '/api/TaktEquipments'

type EquipmentPayload = Partial<Equipment>

// 设备管理相关 API
export function getEquipmentList(params: EquipmentQuery) {
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

export function createEquipment(data: EquipmentPayload) {
  return request({
    url: equipmentUrl,
    method: 'post',
    data
  })
}

export function updateEquipment(id: string, data: EquipmentPayload) {
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
