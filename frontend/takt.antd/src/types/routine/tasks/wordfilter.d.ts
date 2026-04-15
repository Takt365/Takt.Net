// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/wordfilter
// 文件名称：wordfilter.d.ts
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：敏感词过滤类型定义，对应后端 Takt.Application.Dtos.Routine.WordFilter.TaktWordFilterDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 检查文本请求 DTO
 */
export interface CheckTextDto {
  /** 待检查的文本 */
  text: string
}

/**
 * 检查文本结果 DTO
 */
export interface CheckTextResultDto {
  /** 是否包含敏感词 */
  containsIllegalWords: boolean
  /** 敏感词列表 */
  illegalWords: string[]
}

/**
 * 查找敏感词请求 DTO
 */
export interface FindWordsDto {
  /** 待查找的文本 */
  text: string
  /** 是否包含详细信息（位置信息） */
  includeDetails?: boolean
}

/**
 * 敏感词详情 DTO
 */
export interface IllegalWordDetailDto {
  /** 敏感词 */
  keyword: string
  /** 起始位置 */
  start: number
  /** 结束位置 */
  end: number
}

/**
 * 查找敏感词结果 DTO
 */
export interface FindWordsResultDto {
  /** 敏感词列表 */
  illegalWords: string[]
  /** 敏感词详情列表（包含位置信息） */
  illegalWordDetails?: IllegalWordDetailDto[]
}

/**
 * 替换敏感词请求 DTO
 */
export interface ReplaceWordsDto {
  /** 待替换的文本 */
  text: string
  /** 替换字符串（如果提供，则使用此字符串替换） */
  replaceText?: string
  /** 替换字符（如果提供且 replaceText 为空，则使用此字符替换） */
  replaceChar?: string
}

/**
 * 替换敏感词结果 DTO
 */
export interface ReplaceWordsResultDto {
  /** 原始文本 */
  originalText: string
  /** 替换后的文本 */
  replacedText: string
  /** 替换的敏感词数量 */
  replacedCount: number
}

/**
 * 高亮敏感词请求 DTO
 */
export interface HighlightWordsDto {
  /** 待高亮的文本 */
  text: string
  /** 高亮 CSS 类名（默认为 "illegal-word"） */
  highlightClass?: string
}

/**
 * 高亮敏感词结果 DTO
 */
export interface HighlightWordsResultDto {
  /** 原始文本 */
  originalText: string
  /** 高亮后的文本（HTML） */
  highlightedText: string
  /** 高亮的敏感词数量 */
  highlightedCount: number
}

/**
 * 添加敏感词请求 DTO
 */
export interface AddWordsDto {
  /** 敏感词列表 */
  words: string[]
  /** 词库组（词库文件名，如：baokongciku.txt，可选） */
  group?: string
}

/**
 * 添加敏感词结果 DTO
 */
export interface AddWordsResultDto {
  /** 添加的敏感词数量 */
  addedCount: number
  /** 当前敏感词总数 */
  totalCount: number
}

/**
 * 移除敏感词请求 DTO
 */
export interface RemoveWordsDto {
  /** 敏感词列表 */
  words: string[]
}

/**
 * 移除敏感词结果 DTO
 */
export interface RemoveWordsResultDto {
  /** 移除的敏感词数量 */
  removedCount: number
  /** 剩余的敏感词数量 */
  remainingCount: number
}

/**
 * 敏感词统计信息 DTO
 */
export interface WordFilterStatsDto {
  /** 敏感词总数 */
  totalCount: number
  /** 是否已初始化 */
  isInitialized: boolean
}

/**
 * 词库文件信息 DTO
 */
export interface WordLibraryFileDto {
  /** 文件名 */
  fileName: string
  /** 显示名称 */
  displayName: string
  /** 文件大小（字节） */
  fileSize: number
  /** 词数 */
  wordCount: number
}
