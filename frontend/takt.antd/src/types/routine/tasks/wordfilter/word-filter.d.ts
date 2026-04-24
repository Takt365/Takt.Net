// ========================================
// 命名空间：@/types/routine/tasks/wordfilter/word-filter
// 对应后端 Takt.Application.Dtos.Routine.Tasks.WordFilter（JSON camelCase）
// ========================================

export interface CheckTextDto {
  text: string
}

export interface CheckTextResultDto {
  containsIllegalWords: boolean
  illegalWords: string[]
}

export interface FindWordsDto {
  text: string
  includeDetails: boolean
}

export interface IllegalWordDetailDto {
  keyword: string
  start: number
  end: number
}

export interface FindWordsResultDto {
  illegalWords: string[]
  illegalWordDetails: IllegalWordDetailDto[]
}

export interface ReplaceWordsDto {
  text: string
  replaceChar?: string
  replaceText?: string
}

export interface ReplaceWordsResultDto {
  originalText: string
  replacedText: string
  replacedCount: number
}

export interface HighlightWordsDto {
  text: string
  highlightClass: string
}

export interface HighlightWordsResultDto {
  originalText: string
  highlightedText: string
  highlightedCount: number
}

export interface AddWordsDto {
  words: string[]
  group?: string
}

export interface AddWordsResultDto {
  addedCount: number
  totalCount: number
}

export interface RemoveWordsDto {
  words: string[]
}

export interface RemoveWordsResultDto {
  removedCount: number
  remainingCount: number
}

export interface WordFilterStatsDto {
  totalCount: number
  isInitialized: boolean
}

export interface WordLibraryFileDto {
  fileName: string
  displayName: string
  fileSize: number
  wordCount: number
}
