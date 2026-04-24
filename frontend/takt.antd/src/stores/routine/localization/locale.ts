import { defineStore } from 'pinia'
import { ref, watch, computed, nextTick } from 'vue'
import i18n, { supportedLocales } from '@/locales'
import { getLanguageOptions } from '@/api/routine/tasks/i18n/language'
import { getSetting } from '@/stores/setting'

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
  orderNum?: number
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
      // 使用 getOptions 获取所有语言选项（不分页）
      const raw = await getLanguageOptions()
      const options = Array.isArray(raw) ? raw : []

      // 将 TaktSelectOption 转为 Language；兼容 camelCase / PascalCase，并丢弃无 cultureCode 的项
      const mapped = options
        .map((option) => {
          const o = option as Record<string, unknown>
          const dictValue = o.dictValue ?? o.DictValue
          const dictLabel = o.dictLabel ?? o.DictLabel
          const extLabel = o.extLabel ?? o.ExtLabel
          const orderNum = o.orderNum ?? o.OrderNum
          const code = String(dictValue ?? '').trim()
          const name = String(dictLabel ?? '')
          const displayName = String(extLabel ?? dictLabel ?? '')
          const optionLike = option as LanguageOptionLike
          return {
            code,
            name,
            displayName,
            enabled: true,
            order: typeof orderNum === 'number' ? orderNum : Number(orderNum) || 0,
            ...optionLike,
            dictValue,
            dictLabel,
            extLabel,
            orderNum
          } as Language
        })
        .filter((lang) => lang.code.length > 0)
        .sort((a, b) => (a.order || 0) - (b.order || 0))

      languages.value = mapped.length > 0 ? mapped : [...STATIC_LANGUAGE_LIST]
      
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
    } catch {
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
      if (newLocale === oldLocale) return
      localStorage.setItem('locale', newLocale)
      const localeRef = i18n.global.locale as { value: string }
      try {
        const { loadTranslationsFromBackend } = await import('@/locales')
        await loadTranslationsFromBackend(newLocale, 'Frontend')
        await nextTick()
        // 翻译合并后再拉 userinfo，同步假日等随语言的字段（与登录流程顺序一致）
        try {
          const { useUserStore } = await import('@/stores/identity/user')
          const userStore = useUserStore()
          if (userStore.token) {
            await userStore.getUserInfo().catch(() => {})
          }
        } catch {
          /* 忽略 */
        }
      } catch {
        localeRef.value = newLocale
      }
    },
    { immediate: true }
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
