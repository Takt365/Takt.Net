// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/routine/localization/translation
// 文件名称：translation.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：翻译数据 Store，管理从后端动态获取的翻译数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { getTranslationOptions } from '@/api/routine/tasks/i18n/translation'

/**
 * 翻译数据 Store
 */
export const useTranslationStore = defineStore('translation', () => {
  // 翻译数据缓存：{ cultureCode: { resourceKey: translationValue } }
  const translationsCache = ref<Record<string, Record<string, string>>>({})
  
  // 加载状态：{ cultureCode: boolean }
  const loadingStates = ref<Record<string, boolean>>({})

  /**
   * 获取指定语言和资源类型的翻译数据
   * @param cultureCode 语言编码（如：zh-CN、en-US）
   * @param resourceType 资源类型（Frontend=前端，Backend=后端）
   * @param forceRefresh 是否强制刷新
   */
  const loadTranslations = async (cultureCode: string, resourceType: string = 'Frontend', forceRefresh: boolean = false): Promise<Record<string, string>> => {
    const cacheKey = `${cultureCode}_${resourceType}`
    
    // 如果已缓存且不强制刷新，直接返回
    if (!forceRefresh && translationsCache.value[cacheKey]) {
      return translationsCache.value[cacheKey]
    }

    // 如果正在加载，等待加载完成
    if (loadingStates.value[cacheKey]) {
      return new Promise((resolve) => {
        const checkInterval = setInterval(() => {
          if (!loadingStates.value[cacheKey] && translationsCache.value[cacheKey]) {
            clearInterval(checkInterval)
            resolve(translationsCache.value[cacheKey])
          }
        }, 100)
      })
    }

    try {
      loadingStates.value[cacheKey] = true

      // 使用 getOptions 获取所有翻译选项（不分页，已审核）
      const options = await getTranslationOptions()
      
      // 根据 cultureCode 和 resourceType 过滤，并转换为 key-value 格式
      // 后端映射：extLabel = CultureCode, extValue = ResourceType, dictValue = ResourceKey, dictLabel = TranslationValue
      // 后端已统一转换为 camelCase
      const allTranslations: Record<string, string> = {}
      options
        .filter(option => {
          const optionCultureCode = String(option.extLabel ?? '')
          const optionResourceType = String(option.extValue ?? '')
          return optionCultureCode === cultureCode && optionResourceType === resourceType
        })
        .forEach(option => {
          // dictValue = ResourceKey, dictLabel = TranslationValue
          const resourceKey = String(option.dictValue ?? '')
          const translationValue = String(option.dictLabel ?? '')
          if (resourceKey && translationValue) {
            allTranslations[resourceKey] = translationValue
          }
        })

      // 缓存翻译数据
      translationsCache.value[cacheKey] = allTranslations

      // 调试日志：输出加载的翻译数据数量
      // if (import.meta.env.DEV) {
      //   logger.debug(`[Translation] 已加载翻译数据 (${cultureCode}, ${resourceType}):`, {
      //     count: Object.keys(allTranslations).length,
      //     sampleKeys: Object.keys(allTranslations).slice(0, 5)
      //   })
      // }

      return allTranslations
    } catch {
      // logger.error(`[Translation] 加载翻译数据失败 (${cultureCode}, ${resourceType}):`, error)
      return {}
    } finally {
      loadingStates.value[cacheKey] = false
    }
  }

  /**
   * 获取指定语言的翻译数据（已缓存）
   */
  const getTranslations = (cultureCode: string, resourceType: string = 'Frontend'): Record<string, string> => {
    const cacheKey = `${cultureCode}_${resourceType}`
    return translationsCache.value[cacheKey] || {}
  }

  /**
   * 清除指定语言的翻译缓存
   */
  const clearCache = (cultureCode?: string, resourceType?: string) => {
    if (cultureCode && resourceType) {
      const cacheKey = `${cultureCode}_${resourceType}`
      delete translationsCache.value[cacheKey]
    } else if (cultureCode) {
      // 清除该语言的所有资源类型缓存
      Object.keys(translationsCache.value).forEach(key => {
        if (key.startsWith(`${cultureCode}_`)) {
          delete translationsCache.value[key]
        }
      })
    } else {
      // 清除所有缓存
      translationsCache.value = {}
    }
  }

  /**
   * 检查指定语言的翻译是否已加载
   */
  const isLoaded = (cultureCode: string, resourceType: string = 'Frontend'): boolean => {
    const cacheKey = `${cultureCode}_${resourceType}`
    return !!translationsCache.value[cacheKey]
  }

  return {
    translationsCache: computed(() => translationsCache.value),
    loadTranslations,
    getTranslations,
    clearCache,
    isLoaded
  }
})
