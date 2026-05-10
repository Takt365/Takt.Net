// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/vocabulary/filtering-engine
// 文件名称：filtering.ts
// 功能描述：敏感词过滤引擎 API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Vocabulary.FilteringEngine.TaktFilterings
// ========================================

import request from '@/api/request'
import type { TaktApiResult } from '@/types/common'

// ========================================
// 敏感词过滤引擎 API（按后端控制器顺序）
// ========================================
const filteringUrl = '/api/TaktFilterings'

/** 敏感词匹配 DTO */
export interface SensitiveWordMatchDto {
  word: string
  startIndex: number
  endIndex: number
  position: number
  context: string
}

/** 敏感词统计 DTO */
export interface VocabularyStatsDto {
  totalWords: number
  categories: { name: string; count: number }[]
  lastUpdated: string
}

/**
 * 检查文本是否包含敏感词
 * 对应后端：ContainsVocabularyAsync
 */
export function containsVocabulary(text: string): Promise<TaktApiResult<boolean>> {
  return request({
    url: `${filteringUrl}/contains`,
    method: 'get',
    params: { text }
  })
}

/**
 * 查找文本中的所有敏感词
 * 对应后端：FindVocabularyAsync
 */
export function findVocabulary(text: string): Promise<TaktApiResult<string[]>> {
  return request({
    url: `${filteringUrl}/find`,
    method: 'get',
    params: { text }
  })
}

/**
 * 查找敏感词及其位置信息
 * 对应后端：FindVocabularyWithDetailsAsync
 */
export function findVocabularyWithDetails(text: string): Promise<TaktApiResult<SensitiveWordMatchDto[]>> {
  return request({
    url: `${filteringUrl}/find/details`,
    method: 'get',
    params: { text }
  })
}

/**
 * 替换文本中的敏感词
 * 对应后端：ReplaceVocabularyAsync
 * @param text 原始文本
 * @param replacement 替换字符（默认 *）
 */
export function replaceVocabulary(text: string, replacement = '*'): Promise<TaktApiResult<string>> {
  return request({
    url: `${filteringUrl}/replace`,
    method: 'get',
    params: { text, replacement }
  })
}

/**
 * 高亮文本中的敏感词（HTML格式）
 * 对应后端：HighlightVocabularyAsync
 * @param text 原始文本
 * @param highlightClass 高亮CSS类名（默认 sensitive-word）
 */
export function highlightVocabulary(text: string, highlightClass = 'sensitive-word'): Promise<TaktApiResult<string>> {
  return request({
    url: `${filteringUrl}/highlight`,
    method: 'get',
    params: { text, highlightClass }
  })
}

/**
 * 获取所有敏感词
 * 对应后端：GetAllVocabularyAsync
 */
export function getAllVocabulary(): Promise<TaktApiResult<string[]>> {
  return request({
    url: `${filteringUrl}/all`,
    method: 'get'
  })
}

/**
 * 获取敏感词统计信息
 * 对应后端：GetStatsAsync
 */
export function getVocabularyStats(): Promise<TaktApiResult<VocabularyStatsDto>> {
  return request({
    url: `${filteringUrl}/stats`,
    method: 'get'
  })
}

/**
 * 清除词库缓存
 * 对应后端：ClearCache
 */
export function clearVocabularyCache(): Promise<TaktApiResult<{ message: string }>> {
  return request({
    url: `${filteringUrl}/clear-cache`,
    method: 'post'
  })
}