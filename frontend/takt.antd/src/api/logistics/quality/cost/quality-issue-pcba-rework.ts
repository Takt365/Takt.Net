// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-issue-pcba-rework.ts
// 功能描述：QualityIssuePcbaRework API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityIssuePcbaReworks
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityIssuePcbaRework,
  QualityIssuePcbaReworkQuery,
  QualityIssuePcbaReworkCreate,
  QualityIssuePcbaReworkUpdate
} from '@/types/logistics/quality/cost/quality-issue-pcba-rework'

// ========================================
// QualityIssuePcbaRework相关 API（按后端控制器顺序）
// ========================================
const qualityIssuePcbaReworkUrl = '/api/TaktQualityIssuePcbaReworks';

/**
 * 获取QualityIssuePcbaRework列表（分页）
 * 对应后端：GetQualityIssuePcbaReworkListAsync
 */
export function getQualityIssuePcbaReworkList(params: QualityIssuePcbaReworkQuery): Promise<TaktPagedResult<QualityIssuePcbaRework>> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityIssuePcbaRework
 * 对应后端：GetQualityIssuePcbaReworkByIdAsync
 */
export function getQualityIssuePcbaReworkById(id: string): Promise<QualityIssuePcbaRework> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityIssuePcbaRework选项列表（用于下拉框等）
 * 对应后端：GetQualityIssuePcbaReworkOptionsAsync
 */
export function getQualityIssuePcbaReworkOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityIssuePcbaRework
 * 对应后端：CreateQualityIssuePcbaReworkAsync
 */
export function createQualityIssuePcbaRework(data: QualityIssuePcbaReworkCreate): Promise<QualityIssuePcbaRework> {
  return request({
    url: qualityIssuePcbaReworkUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityIssuePcbaRework
 * 对应后端：UpdateQualityIssuePcbaReworkAsync
 */
export function updateQualityIssuePcbaRework(id: string, data: QualityIssuePcbaReworkUpdate): Promise<QualityIssuePcbaRework> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityIssuePcbaRework（单条）
 * 对应后端：DeleteQualityIssuePcbaReworkByIdAsync
 */
export function deleteQualityIssuePcbaReworkById(id: string): Promise<void> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityIssuePcbaRework
 * 对应后端：DeleteQualityIssuePcbaReworkBatchAsync
 */
export function deleteQualityIssuePcbaReworkBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityIssuePcbaReworkTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityIssuePcbaReworkTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityIssuePcbaRework
 * 对应后端：ImportQualityIssuePcbaReworkAsync
 */
export function importQualityIssuePcbaReworkData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityIssuePcbaReworkUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityIssuePcbaRework
 * 对应后端：ExportQualityIssuePcbaReworkAsync；fileName 仅传名称不含后缀
 */
export function exportQualityIssuePcbaReworkData(query: QualityIssuePcbaReworkQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityIssuePcbaReworkUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
