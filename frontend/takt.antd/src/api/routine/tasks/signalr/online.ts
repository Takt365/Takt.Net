// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/signalr/online
// 功能描述：在线用户 API，对应后端 Routine.Tasks.SignalR TaktOnlinesController
// ========================================

import request from '../../../request'
import type { TaktPagedResult } from '@/types/common'
import type { Online, OnlineQuery } from '@/types/routine/tasks/signalr/online'

const onlineUrl = '/api/TaktOnlines'

/**
 * 获取在线用户列表（分页）
 * 对应后端：GetListAsync，GET /api/TaktOnlines/list
 */
export function getOnlineList(params: OnlineQuery): Promise<TaktPagedResult<Online>> {
  return request({
    url: `${onlineUrl}/list`,
    method: 'get',
    params
  })
}
