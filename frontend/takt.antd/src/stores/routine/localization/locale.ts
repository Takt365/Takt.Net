import { defineStore } from 'pinia'
import { ref, watch, computed, nextTick } from 'vue'
import i18n from '@/locales'
import { getOptions } from '@/api/routine/i18n/language'
import { getSetting } from '@/stores/setting'
import { showApiError } from '@/utils/notification'
import type { TaktSelectOption } from '@/types/common'

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
  } catch (error) {
    // logger.warn('[Locale Store] 读取设置失败，使用默认值:', error)
  }
  
  // 3. 最终默认值：中文
  return 'zh-CN'
}

export const useLocaleStore = defineStore('locale', () => {
  const locale = ref<Locale>(getDefaultLocale())
  
  // 语言列表
  const languages = ref<Language[]>([])
  const loading = ref(false)

  // 当前语言信息
  const currentLanguage = computed(() => {
    return languages.value.find(lang => lang.code === locale.value) || languages.value[0]
  })

  // 启用的语言列表
  const enabledLanguages = computed(() => {
    return languages.value.filter(lang => lang.enabled !== false)
  })

  // 加载语言列表
  const loadLanguages = async () => {
    if (loading.value) {
      return
    }
    try {
      loading.value = true
      // 使用 getOptions 获取所有语言选项（不分页）
      const options = await getOptions()
      
      // 将 TaktSelectOption 转换为 Language 格式
      // 后端映射：DictLabel=LanguageName(中文名称), DictValue=CultureCode(语言代码), ExtLabel=NativeName(本地化名称), ExtValue=Id(语言ID)
      // 后端已统一转换为 camelCase
      languages.value = options.map((option: TaktSelectOption) => {
        // 后端已统一转换为 camelCase
        const dictValue = option.dictValue
        const dictLabel = option.dictLabel
        const extLabel = option.extLabel
        const orderNum = option.orderNum ?? 0

        const mapped: Language = {
          code: String(dictValue), // 语言代码，如：'ar-SA'
          name: dictLabel, // 中文名称，如：'阿拉伯语'
          displayName: (extLabel as string | undefined) ?? dictLabel, // 本地化名称
          enabled: true, // getOptions 返回的都是启用的语言（LanguageStatus == 0）
          order: orderNum
        }
        return mapped
      }).sort((a, b) => a.code.localeCompare(b.code))
      
      // 如果当前语言不在列表中，使用设置中的默认语言，如果没有则优先使用中文（zh-CN），最后使用第一个启用的语言
      if (languages.value.length > 0 && !languages.value.find(lang => lang.code === locale.value)) {
        // 获取设置中的默认语言
        let defaultLocaleFromSetting: Locale | null = null
        try {
          const setting = getSetting()
          if (setting.defaultLocale) {
            defaultLocaleFromSetting = setting.defaultLocale
          }
        } catch (error) {
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
        } catch (error) {
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
      showApiError((i18n.global.t as (k: string, values?: object) => string)('common.msg.loadListFail', { target: (i18n.global.t as (k: string) => string)('common.settings.locale.title') }))
    } finally {
      loading.value = false
    }
  }

  // 监听语言变化
  watch(
    locale,
    async (newLocale, oldLocale) => {
      // 如果语言没有变化，跳过
      if (newLocale === oldLocale) {
        return
      }

      localStorage.setItem('locale', newLocale)
      const localeRef = i18n.global.locale as { value: string }
      localeRef.value = newLocale
      await nextTick()

      // 切换语言后立即重新获取用户信息（含节假日），与加载翻译并行，请求会带新 Accept-Language
      const userInfoPromise = (async () => {
        try {
          const { useUserStore } = await import('@/stores/identity/user')
          const userStore = useUserStore()
          if (userStore.token) await userStore.getUserInfo()
        } catch (_) {
          // 未登录或接口失败时忽略
        }
      })()

      try {
        const { loadTranslationsFromBackend } = await import('@/locales')
        await loadTranslationsFromBackend(newLocale, 'Frontend')
        localeRef.value = newLocale
        await nextTick()
      } catch (error) {
        // 翻译加载失败不影响语言切换
      }

      await userInfoPromise
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
    if (enabled.length < 2) {
      return
    }

    const currentIndex = enabled.findIndex(lang => lang.code === locale.value)
    const nextIndex = (currentIndex + 1) % enabled.length
    locale.value = enabled[nextIndex].code
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
