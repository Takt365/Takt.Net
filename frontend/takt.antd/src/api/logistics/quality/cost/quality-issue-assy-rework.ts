// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-issue-assy-rework.ts
// 功能描述：QualityIssueAssyRework API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityIssueAssyReworks
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityIssueAssyRework,
  QualityIssueAssyReworkQuery,
  QualityIssueAssyReworkCreate,
  QualityIssueAssyReworkUpdate
} from '@/types/logistics/quality/cost/quality-issue-assy-rework'

// ========================================
// QualityIssueAssyRework相关 API（按后端控制器顺序）
// ========================================
const qualityIssueAssyReworkUrl = '/api/TaktQualityIssueAssyReworks';

/**
 * 获取QualityIssueAssyRework列表（分页）
 * 对应后端：GetQualityIssueAssyReworkListAsync
 */
export function getQualityIssueAssyReworkList(params: QualityIssueAssyReworkQuery): Promise<TaktPagedResult<QualityIssueAssyRework>> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityIssueAssyRework
 * 对应后端：GetQualityIssueAssyReworkByIdAsync
 */
export function getQualityIssueAssyReworkById(id: string): Promise<QualityIssueAssyRework> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityIssueAssyRework选项列表（用于下拉框等）
 * 对应后端：GetQualityIssueAssyReworkOptionsAsync
 */
export function getQualityIssueAssyReworkOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityIssueAssyRework
 * 对应后端：CreateQualityIssueAssyReworkAsync
 */
export function createQualityIssueAssyRework(data: QualityIssueAssyReworkCreate): Promise<QualityIssueAssyRework> {
  return request({
    url: qualityIssueAssyReworkUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityIssueAssyRework
 * 对应后端：UpdateQualityIssueAssyReworkAsync
 */
export function updateQualityIssueAssyRework(id: string, data: QualityIssueAssyReworkUpdate): Promise<QualityIssueAssyRework> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityIssueAssyRework（单条）
 * 对应后端：DeleteQualityIssueAssyReworkByIdAsync
 */
export function deleteQualityIssueAssyReworkById(id: string): Promise<void> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityIssueAssyRework
 * 对应后端：DeleteQualityIssueAssyReworkBatchAsync
 */
export function deleteQualityIssueAssyReworkBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityIssueAssyReworkTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityIssueAssyReworkTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityIssueAssyRework
 * 对应后端：ImportQualityIssueAssyReworkAsync
 */
export function importQualityIssueAssyReworkData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityIssueAssyReworkUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityIssueAssyRework
 * 对应后端：ExportQualityIssueAssyReworkAsync；fileName 仅传名称不含后缀
 */
export function exportQualityIssueAssyReworkData(query: QualityIssueAssyReworkQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityIssueAssyReworkUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
