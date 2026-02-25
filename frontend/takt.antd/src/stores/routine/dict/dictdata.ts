// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/routine/dict/dictdata
// 文件名称：dictdata.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：字典数据 Store，管理从后端动态获取的字典数据
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { getOptions } from '@/api/routine/dict/dictdata'
import { logger } from '@/utils/logger'
import request from '@/api/request'
import type { TaktSelectOption, TaktTreeSelectOption } from '@/types/common'

/**
 * 字典数据 Store
 */
export const useDictDataStore = defineStore('dictData', () => {
  // 字典数据缓存：{ dictTypeCode: TaktSelectOption[] }
  // 如果 dictTypeCode 为空字符串或 undefined，表示所有字典数据（未分类）
  const dictDataCache = ref<Record<string, TaktSelectOption[]>>({})
  
  // 业务选项数据缓存：{ apiUrl: TaktSelectOption[] | TaktTreeSelectOption[] }
  // 使用 API URL 作为 key，支持任意业务选项的动态加载和缓存
  const businessOptionsCache = ref<Record<string, TaktSelectOption[] | TaktTreeSelectOption[]>>({})
  
  // 业务选项数据加载状态：{ apiUrl: boolean }
  const businessOptionsLoading = ref<Record<string, boolean>>({})
  
  // 加载状态
  const loading = ref(false)
  
  // 是否已加载所有字典数据
  const isLoaded = ref(false)

  /**
   * 加载所有字典数据
   * @param forceRefresh 是否强制刷新
   */
  const loadAllDictData = async (forceRefresh: boolean = false): Promise<void> => {
    // 如果已加载且不强制刷新，直接返回
    if (!forceRefresh && isLoaded.value) {
      return
    }

    // 如果正在加载，等待加载完成
    if (loading.value) {
      return new Promise((resolve) => {
        const checkInterval = setInterval(() => {
          if (!loading.value && isLoaded.value) {
            clearInterval(checkInterval)
            resolve()
          }
        }, 100)
      })
    }

    try {
      loading.value = true

      // 优化：一次性批量加载所有字典数据（只调用一次 API，而不是每个字典类型调用一次）
      // 后端返回所有数据时，ExtValue 存储 DictTypeCode（字典类型编码），用于前端分组
      logger.info('[DictData] 开始批量加载所有字典数据...')
      const allDictData = await getOptions() // 不传参数，获取所有字典数据
      
      // 按字典类型编码分组（ExtValue = DictTypeCode）
      const groupedDictData: Record<string, TaktSelectOption[]> = {}
      
      allDictData.forEach((option: TaktSelectOption) => {
        // 兼容 PascalCase 和 camelCase
        // 后端已统一转换为 camelCase
        // 批量加载时：DictTypeCode 用于分组，ExtLabel 和 ExtValue 保持原始值
        const dictTypeCode = String(option.dictTypeCode ?? '') // DictTypeCode 用于分组
        const dictValue = option.dictValue
        const dictLabel = option.dictLabel
        const extLabel = option.extLabel // ExtLabel 保持原始值
        const extValue = option.extValue // ExtValue 保持原始值
        const dictL10nKey = option.dictL10nKey // DictL10nKey 保持原始值
        
        if (!dictTypeCode) {
          logger.warn('[DictData] 字典数据缺少 dictTypeCode（字典类型编码）:', option)
          return
        }
        
        if (!groupedDictData[dictTypeCode]) {
          groupedDictData[dictTypeCode] = []
        }
        
        // 保持原始数据结构，与单个查询完全一致
        // 批量加载时：所有字段都保持原始值，DictTypeCode 用于前端分组
        groupedDictData[dictTypeCode].push({
          dictLabel: String(dictLabel),
          dictValue: dictValue, // 后端：DictValue = DictValue（字典值，如 "0", "1", "2"）
          dictL10nKey: dictL10nKey, // 后端：DictL10nKey = DictL10nKey（原始字典本地化键）
          extLabel: extLabel, // 后端：ExtLabel = ExtLabel（原始扩展标签）
          extValue: extValue, // 后端：ExtValue = ExtValue（原始扩展值）
          // 后端已统一转换为 camelCase
          cssClass: option.cssClass,
          listClass: option.listClass,
          orderNum: option.orderNum ?? 0
        })
      })
      
      // 对每个字典类型的数据按 orderNum 排序
      Object.keys(groupedDictData).forEach(key => {
        groupedDictData[key].sort((a, b) => (a.orderNum || 0) - (b.orderNum || 0))
      })

      // 更新缓存
      dictDataCache.value = groupedDictData
      isLoaded.value = true

      const totalCount = Object.values(groupedDictData).reduce((sum, data) => sum + data.length, 0)
      logger.info(`[DictData] 已批量加载字典数据，共 ${totalCount} 条，分为 ${Object.keys(groupedDictData).length} 个字典类型（仅调用 1 次 API）`)
    } catch (error) {
      logger.error('[DictData] 加载字典数据失败:', error)
      throw error
    } finally {
      loading.value = false
    }
  }

  /**
   * 获取指定字典类型的数据
   * @param dictTypeCode 字典类型编码（可选，如果不传则返回所有数据）
   */
  const getDictData = (dictTypeCode?: string): TaktSelectOption[] => {
    if (dictTypeCode) {
      return dictDataCache.value[dictTypeCode] || []
    }
    // 如果不传 dictTypeCode，返回所有字典数据（合并所有分组）
    const allData: TaktSelectOption[] = []
    Object.values(dictDataCache.value).forEach(data => {
      allData.push(...data)
    })
    return allData
  }

  /**
   * 获取指定字典类型的选项数据（转换为标准选项格式，支持自定义字段映射）
   * @param dictTypeCode 字典类型编码
   * @param fieldNames 字段映射配置（可选）
   * @returns 转换后的选项数组，格式：{ label: string, value: string | number }[]
   */
  const getDictOptions = (
    dictTypeCode: string,
    fieldNames?: {
      /** value 字段名，默认为 'dictValue'，可选值：'dictValue' | 'extLabel' | 'extValue' */
      valueField?: 'dictValue' | 'extLabel' | 'extValue'
      /** label 字段名，默认为 'dictLabel'，可选值：'dictLabel' | 'extLabel' */
      labelField?: 'dictLabel' | 'extLabel'
    }
  ): Array<{ label: string; value: string | number }> => {
    const dictData = getDictData(dictTypeCode)
    const valueField = fieldNames?.valueField || 'dictValue'
    const labelField = fieldNames?.labelField || 'dictLabel'

    return dictData.map(item => {
      // 获取 value
      let value: string | number
      switch (valueField) {
        case 'extLabel':
          value = item.extLabel ?? item.dictValue ?? ''
          break
        case 'extValue':
          value = item.extValue ?? item.dictValue ?? ''
          break
        case 'dictValue':
        default:
          value = item.dictValue ?? ''
          break
      }

      // 获取 label
      let label: string
      switch (labelField) {
        case 'extLabel':
          label = String(item.extLabel ?? item.dictLabel ?? '')
          break
        case 'dictLabel':
        default:
          label = String(item.dictLabel ?? '')
          break
      }

      return { label, value }
    })
  }

  /**
   * 根据字典值查找字典标签
   * @param dictValue 字典值（可能是 dictValue 或 extLabel）
   * @param dictTypeCode 字典类型编码（可选）
   * @param useExtLabel 是否使用 extLabel 来匹配（默认 true，因为实际值存储在 extLabel 中）
   */
  const getDictLabel = (dictValue: string | number, dictTypeCode?: string, useExtLabel: boolean = true): string => {
    const option = getDictOption(dictValue, dictTypeCode, useExtLabel)
    return option?.dictLabel || String(dictValue)
  }

  /**
   * 根据字典值查找字典选项对象
   * @param dictValue 字典值（可能是 dictValue 或 extLabel）
   * @param dictTypeCode 字典类型编码（可选）
   * @param useExtLabel 是否使用 extLabel 来匹配（默认 true，因为实际值存储在 extLabel 中）
   */
  const getDictOption = (dictValue: string | number, dictTypeCode?: string, useExtLabel: boolean = true): TaktSelectOption | undefined => {
    const data = getDictData(dictTypeCode)
    return data.find(item => {
      if (useExtLabel) {
        // 优先使用 extLabel 匹配（实际值存储在 extLabel 中）
        const extLabel = item.extLabel
        if (extLabel !== undefined && extLabel !== null) {
          return String(extLabel) === String(dictValue)
        }
      }
      // 回退到 dictValue 匹配
      const value = item.dictValue
      return String(value) === String(dictValue)
    })
  }

  /**
   * 清除字典数据缓存
   */
  const clearCache = () => {
    dictDataCache.value = {}
    isLoaded.value = false
  }

  /**
   * 检查字典数据是否已加载
   */
  const checkIsLoaded = (): boolean => {
    return isLoaded.value
  }

  /**
   * 加载业务选项数据（按需加载，支持任意 API URL）
   * @param apiUrl API URL（例如：'/api/TaktRoles/options'）
   * @param forceRefresh 是否强制刷新
   * @returns 业务选项数据
   */
  const loadBusinessOptions = async (
    apiUrl: string,
    forceRefresh: boolean = false
  ): Promise<TaktSelectOption[] | TaktTreeSelectOption[]> => {
    // 如果已缓存且不强制刷新，直接返回缓存
    if (!forceRefresh && businessOptionsCache.value[apiUrl]) {
      return businessOptionsCache.value[apiUrl]
    }

    // 如果正在加载，等待加载完成
    if (businessOptionsLoading.value[apiUrl]) {
      return new Promise((resolve) => {
        const checkInterval = setInterval(() => {
          if (!businessOptionsLoading.value[apiUrl] && businessOptionsCache.value[apiUrl]) {
            clearInterval(checkInterval)
            resolve(businessOptionsCache.value[apiUrl])
          }
        }, 100)
      })
    }

    try {
      businessOptionsLoading.value[apiUrl] = true

      // 动态调用 API
      const data = await request<TaktSelectOption[] | TaktTreeSelectOption[]>({
        url: apiUrl,
        method: 'get'
      })

      const options = Array.isArray(data) ? data : []
      
      // 更新缓存
      businessOptionsCache.value[apiUrl] = options

      logger.debug(`[DictData] 已加载业务选项数据: ${apiUrl}，共 ${options.length} 条`)
      return options
    } catch (error) {
      logger.error(`[DictData] 加载业务选项数据失败: ${apiUrl}`, error)
      businessOptionsCache.value[apiUrl] = []
      return []
    } finally {
      businessOptionsLoading.value[apiUrl] = false
    }
  }

  /**
   * 获取业务选项数据（从缓存中获取，如果不存在则返回空数组）
   * @param apiUrl API URL
   * @returns 业务选项数据
   */
  const getBusinessOptions = (apiUrl: string): TaktSelectOption[] | TaktTreeSelectOption[] => {
    return businessOptionsCache.value[apiUrl] || []
  }

  /**
   * 清除业务选项数据缓存
   * @param apiUrl API URL（可选，如果不传则清除所有缓存）
   */
  const clearBusinessOptionsCache = (apiUrl?: string) => {
    if (apiUrl) {
      delete businessOptionsCache.value[apiUrl]
      delete businessOptionsLoading.value[apiUrl]
    } else {
      businessOptionsCache.value = {}
      businessOptionsLoading.value = {}
    }
  }

  return {
    dictDataCache: computed(() => dictDataCache.value),
    loading: computed(() => loading.value),
    isLoaded: computed(() => isLoaded.value),
    businessOptionsCache: computed(() => businessOptionsCache.value),
    businessOptionsLoading: computed(() => businessOptionsLoading.value),
    loadAllDictData,
    getDictData,
    getDictOptions,
    getDictLabel,
    getDictOption,
    clearCache,
    checkIsLoaded,
    // 业务选项数据相关方法（按需加载，支持任意 API URL）
    loadBusinessOptions,
    getBusinessOptions,
    clearBusinessOptionsCache
  }
})
