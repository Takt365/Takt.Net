// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/dict
// 文件名称：dict-engine.ts
// 功能描述：字典引擎 API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Dict.DictEngine.TaktDictEngineController
// 提供字典数据查询的核心引擎能力，支持系统表和SQL查询两种数据源
// ========================================

import request from '@/api/request'
import type { TaktSelectOption } from '@/types/common'

// ========================================
// DictEngine 相关 API
// ========================================
const dictEngineUrl = '/api/TaktDictEngine'

/**
 * 获取字典数据选项列表（用于下拉框等）
 * 支持两种数据源：
 *   1. 系统表数据源（DataSource=0）：从字典数据表查询
 *   2. SQL查询数据源（DataSource=1）：执行自定义SQL脚本
 * 
 * 对应后端：GetDictOptionsAsync
 * 
 * @param dictTypeCode 字典类型编码（可选，为空时返回所有字典数据）
 * @returns 字典数据选项列表
 */
export function getDictOptions(dictTypeCode?: string): Promise<TaktSelectOption[]> {
  return request({
    url: `${dictEngineUrl}/options`,
    method: 'get',
    params: dictTypeCode ? { dictTypeCode } : undefined
  })
}
