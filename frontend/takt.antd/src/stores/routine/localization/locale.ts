// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/routine/localization/locale
// 文件名称：locale.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：语言管理 Store，管理语言列表、语言切换、语言加载等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import i18n, { supportedLocales, loadTranslationsFromBackend } from '@/locales'
import { useUserStore } from '@/stores/identity/user'
import { getLanguageOptions } from '@/api/routine/tasks/i18n/language'
import { getSetting } from '@/stores/setting'
import { logger } from '@/utils/logger'

export type Locale = string

export interface Language {
  /** 语言代码（如：zh-CN, en-US） */
  code: string
  /** 语言名称 */
  name: string
  /** 语言显示名称（本地化） */
  displayName?: string
  /** 是否启用 */
  enabled?: boolean
  /** 排序 */
  order?: number
  [key: string]: unknown
}
type LanguageOptionLike = Record<string, unknown> & {
  dictValue?: string
  dictLabel?: string
  extLabel?: string
  sortOrder?: number
}

/** 由 `locales` 扫描得到的语言码生成下拉项（无后端或接口失败时仍可选语言，与 `[I18n] 已加载支持的语言` 同源） */
function languagesFromStaticLocales(codes: readonly string[]): Language[] {
  return codes.map((code, i) => ({
    code,
    name: code,
    displayName: code,
    enabled: true,
    order: i
  }))
}

const STATIC_LANGUAGE_LIST: Language[] = languagesFromStaticLocales(supportedLocales)

/**
 * 获取默认语言
 * 优先级：localStorage 中的用户选择 > setting.ts 中的默认语言 > 'zh-CN'
 */
function getDefaultLocale(): Locale {
  // 1. 优先使用用户之前选择并保存的语言
  const savedLocale = localStorage.getItem('locale')
  if (savedLocale) {
    return savedLocale
  }
  
  // 2. 使用 setting.ts 中配置的默认语言
  try {
    const setting = getSetting()
    if (setting.defaultLocale) {
      return setting.defaultLocale
    }
  } catch {
    // logger.warn('[Locale Store] 读取设置失败，使用默认值:', error)
  }
  
  // 3. 最终默认值：中文
  return 'zh-CN'
}

export const useLocaleStore = defineStore('locale', () => {
  const locale = ref<Locale>(getDefaultLocale())
  
  // 语言列表：默认与静态 locales 扫描结果一致；`/api/TaktLanguages/options` 成功后再替换为后端启用语言
  const languages = ref<Language[]>([...STATIC_LANGUAGE_LIST])
  const loading = ref(false)
  /** 合并并发 `loadLanguages`，避免 `loading` 早退导致其它调用方永远拿不到列表 */
  let loadLanguagesInFlight: Promise<void> | null = null
  
  // 初始化：确保 i18n.locale 与 store 一致（应用启动时由 locales/index.ts 构造函数设置，此处做双重保障）
  const localeRef = i18n.global.locale as { value: string }
  if (localeRef.value !== locale.value) {
    localeRef.value = locale.value
    logger.debug('[Locale Store] 初始化：同步 i18n.locale 为', locale.value)
  }

  // 当前语言信息
  const currentLanguage = computed(() => {
    return languages.value.find(lang => lang.code === locale.value) || languages.value[0]
  })

  // 启用的语言列表
  const enabledLanguages = computed(() => {
    return languages.value.filter(lang => lang.enabled !== false)
  })

  // 加载语言列表
  const loadLanguages = async (): Promise<void> => {
    if (loadLanguagesInFlight) return loadLanguagesInFlight
    loadLanguagesInFlight = (async () => {
    try {
      loading.value = true
      logger.info('[Locale Store] 开始从后端加载语言列表...')
      logger.debug('[Locale Store] 本地静态语言列表:', STATIC_LANGUAGE_LIST.map(l => l.code))
      
      // 使用 getOptions 获取所有语言选项（不分页）
      const raw = await getLanguageOptions()
      const options = Array.isArray(raw) ? raw : []
      
      // 调试日志：查看后端返回的原始数据
      logger.debug('[Locale Store] 后端返回的语言选项:', options)

      // 检查后端数据是否为空
      if (options.length === 0) {
        logger.error('[Locale Store] ⚠️ 后端语言列表为空！请检查数据库中是否有启用的语言数据')
        logger.error('[Locale Store] 当前使用本地静态语言列表:', STATIC_LANGUAGE_LIST.map(l => l.code))
      }

      // 将 TaktSelectOption 转为 Language；兼容 camelCase / PascalCase，并丢弃无 cultureCode 的项
      const mapped = options
        .map((option) => {
          const o = option as unknown as Record<string, unknown>
          const dictValue = o.dictValue ?? o.DictValue
          const dictLabel = o.dictLabel ?? o.DictLabel
          const extLabel = o.extLabel ?? o.ExtLabel
          const sortOrder = o.sortOrder ?? o.SortOrder ?? o.orderNum ?? o.OrderNum
          const code = String(dictValue ?? '').trim()
          const name = String(dictLabel ?? '')
          const displayName = String(extLabel ?? dictLabel ?? '')
          const optionLike = option as LanguageOptionLike
          return {
            code,
            name,
            displayName,
            enabled: true,
            order: typeof sortOrder === 'number' ? sortOrder : Number(sortOrder) || 0,
            ...optionLike,
            dictValue,
            dictLabel,
            extLabel,
            sortOrder
          } as Language
        })
        .filter((lang) => lang.code.length > 0)
        .sort((a, b) => (a.order || 0) - (b.order || 0))
      
      // 调试日志：查看映射后的数据
      logger.debug('[Locale Store] 后端语言列表（映射后）:', mapped.map(l => ({ code: l.code, name: l.name, displayName: l.displayName })))

      languages.value = mapped.length > 0 ? mapped : [...STATIC_LANGUAGE_LIST]
      
      if (mapped.length > 0) {
        logger.info('[Locale Store] ✅ 成功使用后端语言列表:', mapped.map(l => l.code))
      } else {
        logger.warn('[Locale Store] ⚠️ 后端语言列表为空，已降级使用本地静态语言列表:', STATIC_LANGUAGE_LIST.map(l => l.code))
      }
      
      // 如果当前语言不在列表中，使用设置中的默认语言，如果没有则优先使用中文（zh-CN），最后使用第一个启用的语言
      if (languages.value.length > 0 && !languages.value.find(lang => lang.code === locale.value)) {
        // 获取设置中的默认语言
        let defaultLocaleFromSetting: Locale | null = null
        try {
          const setting = getSetting()
          if (setting.defaultLocale) {
            defaultLocaleFromSetting = setting.defaultLocale
          }
        } catch {
          // logger.warn('[Locale Store] 读取设置失败:', error)
        }
        
        // 优先使用设置中的默认语言
        if (defaultLocaleFromSetting) {
          const settingLang = languages.value.find(lang => lang.code === defaultLocaleFromSetting && lang.enabled !== false)
          if (settingLang) {
            locale.value = defaultLocaleFromSetting
          } else {
            // 如果设置中的默认语言不可用，尝试使用中文
            const zhCnLang = languages.value.find(lang => lang.code === 'zh-CN' && lang.enabled !== false)
            if (zhCnLang) {
              locale.value = 'zh-CN'
            } else {
              // 最后使用第一个启用的语言
              const firstEnabled = enabledLanguages.value[0]
              if (firstEnabled) {
                locale.value = firstEnabled.code
              }
            }
          }
        } else {
          // 如果没有设置默认语言，优先使用中文
          const zhCnLang = languages.value.find(lang => lang.code === 'zh-CN' && lang.enabled !== false)
          if (zhCnLang) {
            locale.value = 'zh-CN'
          } else {
            // 最后使用第一个启用的语言
            const firstEnabled = enabledLanguages.value[0]
            if (firstEnabled) {
              locale.value = firstEnabled.code
            }
          }
        }
      }
      
      // 确保默认语言已设置（如果语言列表已加载但当前语言未设置）
      if (languages.value.length > 0 && !locale.value) {
        // 获取设置中的默认语言
        let defaultLocaleFromSetting: Locale | null = null
        try {
          const setting = getSetting()
          if (setting.defaultLocale) {
            defaultLocaleFromSetting = setting.defaultLocale
          }
        } catch {
          // logger.warn('[Locale Store] 读取设置失败:', error)
        }
        
        // 优先使用设置中的默认语言
        if (defaultLocaleFromSetting) {
          const settingLang = languages.value.find(lang => lang.code === defaultLocaleFromSetting && lang.enabled !== false)
          if (settingLang) {
            locale.value = defaultLocaleFromSetting
          } else {
            // 如果设置中的默认语言不可用，尝试使用中文
            const zhCnLang = languages.value.find(lang => lang.code === 'zh-CN' && lang.enabled !== false)
            if (zhCnLang) {
              locale.value = 'zh-CN'
            } else {
              // 最后使用第一个启用的语言
              const firstEnabled = enabledLanguages.value[0]
              if (firstEnabled) {
                locale.value = firstEnabled.code
              }
            }
          }
        } else {
          // 如果没有设置默认语言，优先使用中文
          const zhCnLang = languages.value.find(lang => lang.code === 'zh-CN' && lang.enabled !== false)
          if (zhCnLang) {
            locale.value = 'zh-CN'
          } else {
            // 最后使用第一个启用的语言
            const firstEnabled = enabledLanguages.value[0]
            if (firstEnabled) {
              locale.value = firstEnabled.code
            }
          }
        }
      }
    } catch (error) {
      logger.error('[Locale Store] ❌ 从后端加载语言列表失败:', error)
      logger.error('[Locale Store] 已降级使用本地静态语言列表:', STATIC_LANGUAGE_LIST.map(l => l.code))
      languages.value = [...STATIC_LANGUAGE_LIST]
    } finally {
      loading.value = false
      loadLanguagesInFlight = null
    }
    })()
    return loadLanguagesInFlight
  }

  // 监听语言变化：先加载新语言的后端翻译，再切换 i18n locale，避免菜单等依赖 t() 的 computed 在翻译未就绪时重算并触发大量 fallback 警告
  watch(
    locale,
    async (newLocale, oldLocale) => {
      // 初始化时 oldLocale 为 undefined，跳过（由 locales/index.ts 的 i18n 构造函数处理）
      if (newLocale === oldLocale || oldLocale === undefined) return
      
      localStorage.setItem('locale', newLocale)
      
      try {
        // 1. 先加载新语言的后端翻译（此时 i18n.global.locale 还是旧语言）
        await loadTranslationsFromBackend(newLocale, 'Frontend')
        
        // 2. 翻译加载完成后再切换 i18n locale（loadTranslationsFromBackend 内部已设置，此处确保生效）
        const localeRef = i18n.global.locale as { value: string }
        if (localeRef.value !== newLocale) {
          localeRef.value = newLocale
        }
        
        await nextTick()
        
        // 3. 翻译合并后再拉 userinfo，同步假日等随语言的字段（与登录流程顺序一致）
        try {
          const userStore = useUserStore()
          if (userStore.token) {
            await userStore.getUserInfo().catch(() => {})
          }
        } catch {
          /* 忽略 */
        }
        
        logger.info('[Locale Store] 语言切换成功:', newLocale)
      } catch (error) {
        logger.error('[Locale Store] 语言切换失败:', error)
        // 翻译加载失败也要切换 i18n locale（至少静态翻译可用）
        const localeRef = i18n.global.locale as { value: string }
        if (localeRef.value !== newLocale) {
          localeRef.value = newLocale
        }
      }
    }
  )

  // 设置语言
  const setLocale = async (newLocale: Locale) => {
    locale.value = newLocale
    // watch 会自动触发加载翻译数据
  }

  // 切换语言（在支持的语言之间切换）
  const toggleLocale = () => {
    const enabled = enabledLanguages.value
    if (enabled.length < 2) return
    
    const currentIndex = enabled.findIndex(lang => lang.code === locale.value)
    const nextIndex = (currentIndex + 1) % enabled.length
    const nextLang = enabled[nextIndex]
    if (nextLang === undefined) return
    locale.value = nextLang.code
  }

  return {
    locale,
    languages,
    enabledLanguages,
    currentLanguage,
    loading,
    setLocale,
    toggleLocale,
    loadLanguages
  }
})
