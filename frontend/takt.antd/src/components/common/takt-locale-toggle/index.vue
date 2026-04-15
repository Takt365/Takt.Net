<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-button type="text">
      <template #icon>
        <RiTranslate />
      </template>
    </a-button>
    <template #overlay>
      <a-menu :selected-keys="[localeStore.locale]" @click="handleMenuClick">
        <a-menu-item 
          v-for="lang in localeStore.enabledLanguages" 
          :key="lang.code"
        >
          <span style="display: inline-flex; align-items: center;">
            <span :class="getFlagClass(lang.code)" style="font-size: 20px; margin-right: 8px;"></span>
            {{ lang.displayName || lang.name }}
          </span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useLocaleStore, type Locale } from '@/stores/routine/localization/locale'
import { RiTranslate } from '@remixicon/vue'

const localeStore = useLocaleStore()

/**
 * 将语言代码映射到国家代码（用于 flag-icons）
 * @param cultureCode 语言代码（如：zh-CN、en-US）
 * @returns 国家代码（如：cn、us）
 */
const getCountryCode = (cultureCode: string): string => {
  // 从 cultureCode 中提取国家代码（通常是最后两个字符，转为小写）
  const parts = cultureCode.split('-')
  if (parts.length >= 2) {
    return parts[parts.length - 1].toLowerCase()
  }
  // 如果没有分隔符，尝试直接使用（转为小写）
  return cultureCode.toLowerCase()
}

/**
 * 获取国旗 CSS 类名（仅用于菜单项）
 * @param cultureCode 语言代码
 * @returns flag-icons CSS 类名
 */
const getFlagClass = (cultureCode: string): string => {
  const countryCode = getCountryCode(cultureCode)
  return `fi fi-${countryCode}`
}

// 处理菜单点击
const handleMenuClick = ({ key }: { key: string | number }) => {
  localeStore.setLocale(String(key) as Locale)
}

// 组件挂载时确保语言列表已加载，并确保默认选中正确的语言
onMounted(async () => {
  if (localeStore.enabledLanguages.length === 0) {
    await localeStore.loadLanguages()
  }
  
  // 确保默认语言已设置（如果当前语言未设置或不在列表中）
  if (!localeStore.locale || !localeStore.enabledLanguages.find(lang => lang.code === localeStore.locale)) {
    // 获取设置中的默认语言
    let defaultLocaleFromSetting: string | null = null
    try {
      const { getSetting } = await import('@/stores/setting')
      const setting = getSetting()
      if (setting.defaultLocale) {
        defaultLocaleFromSetting = setting.defaultLocale
      }
    } catch (error) {
      logger.warn('[Locale Toggle] 读取设置失败:', error)
    }
    
    // 优先使用设置中的默认语言
    if (defaultLocaleFromSetting) {
      const settingLang = localeStore.enabledLanguages.find(lang => lang.code === defaultLocaleFromSetting)
      if (settingLang) {
        localeStore.setLocale(defaultLocaleFromSetting as Locale)
      } else {
        // 如果设置中的默认语言不可用，尝试使用中文
        const zhCnLang = localeStore.enabledLanguages.find(lang => lang.code === 'zh-CN')
        if (zhCnLang) {
          localeStore.setLocale('zh-CN')
        }
      }
    } else {
      // 如果没有设置默认语言，使用中文
      const zhCnLang = localeStore.enabledLanguages.find(lang => lang.code === 'zh-CN')
      if (zhCnLang) {
        localeStore.setLocale('zh-CN')
      }
    }
  }
  
  // 输出组件使用的语言数据
  if (import.meta.env.DEV) {
    logger.debug('[Locale Toggle] 组件使用的语言数据:', {
      当前语言: localeStore.locale,
      语言列表: localeStore.languages,
      启用的语言: localeStore.enabledLanguages,
      当前语言信息: localeStore.currentLanguage
    })
  }
})
</script>

<style scoped lang="less">

</style>
