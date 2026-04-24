<template>
  <div class="welcome-module">
    <a-row
      class="welcome-row"
      :gutter="16"
    >
      <a-col
        :span="12"
        class="welcome-greeting-col"
      >
        {{ greetingText }}
      </a-col>
      <a-col
        :span="12"
        class="welcome-date-col"
      >
        <a-tooltip :title="dateTooltip">
          <span>{{ t('dashboard.workspace.currenttimelabel') }} {{ dateText }}</span>
        </a-tooltip>
      </a-col>
    </a-row>
    <div class="welcome-quote">
      <span class="welcome-quote-icon">
        <RiLightbulbLine />
      </span>
      <span>{{ quoteText }}</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onBeforeUnmount, onMounted, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import dayjs from 'dayjs'
import localizedFormat from 'dayjs/plugin/localizedFormat'
import utc from 'dayjs/plugin/utc'
import timezone from 'dayjs/plugin/timezone'
import dayOfYear from 'dayjs/plugin/dayOfYear'
import weekOfYear from 'dayjs/plugin/weekOfYear'
import quarterOfYear from 'dayjs/plugin/quarterOfYear'
import { RiLightbulbLine } from '@remixicon/vue'
import { storeToRefs } from 'pinia'
import { useUserStore } from '@/stores/identity/user'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { logger } from '@/utils/logger'

dayjs.extend(localizedFormat)
dayjs.extend(utc)
dayjs.extend(timezone)
dayjs.extend(dayOfYear)
dayjs.extend(weekOfYear)
dayjs.extend(quarterOfYear)

const { t } = useI18n()
const userStore = useUserStore()
const { holidayFromToken, userInfo } = storeToRefs(userStore)
const { locale: currentLocale } = storeToRefs(useLocaleStore())
const safeTrim = (value: unknown): string =>
  typeof value === 'string' ? value.trim() : ''

// 切换语言后 userinfo（含假日）由 localeStore 在 loadTranslationsFromBackend 之后拉取，此处不再重复请求

const now = ref(new Date())
let timer: number | null = null

const updateNow = () => {
  now.value = new Date()
}

onMounted(() => {
  updateNow()
  timer = window.setInterval(updateNow, 1000)
})

onBeforeUnmount(() => {
  if (timer !== null) {
    clearInterval(timer)
    timer = null
  }
})

const localeTimeZoneMap: Record<string, string> = {
  'zh-CN': 'Asia/Shanghai',
  'zh-TW': 'Asia/Taipei',
  'zh-HK': 'Asia/Hong_Kong',
  'en-US': 'America/New_York',
  'fr-FR': 'Europe/Paris',
  'es-ES': 'Europe/Madrid',
  'ja-JP': 'Asia/Tokyo',
  'ko-KR': 'Asia/Seoul',
  'ru-RU': 'Europe/Moscow',
  'ar-SA': 'Asia/Riyadh'
}

const timeZone = computed(() => localeTimeZoneMap[currentLocale.value] || dayjs.tz.guess())

const dateText = computed(() =>
  dayjs(now.value).tz(timeZone.value).format('YYYY-MM-DD HH:mm:ss')
)

const dateTooltip = computed(() => {
  const d = dayjs(now.value).tz(timeZone.value)
  const dayOfYearLabel = t('dashboard.workspace.dayofyearlabel', { n: d.dayOfYear() })
  return d.format('LL') + ' · ' + d.format('dddd') + ' · Q' + d.quarter() + ' · W' + d.week() + ' · ' + dayOfYearLabel
})

// 问候语：假日时严格使用 holidayGreeting（无值则不走假日文案），非假日为时段问候 + 用户名
// 显式依赖 currentLocale，切换语言时重新计算（与 dateText 一致，不单靠 t() 的响应式）
// 显示名：优先 nickName，再 realName，否则 userName（与 TaktUserInfoDto 小驼峰一致，无 nickname 字段）
const greetingText = computed(() => {
  void currentLocale.value
  const holiday = holidayFromToken.value
  const name =
    safeTrim(userInfo.value?.nickName) ||
    safeTrim(userInfo.value?.realName) ||
    userInfo.value?.userName ||
    ''
  if (holiday?.isHolidayToday && holiday.holidayGreeting) {
    const greeting = holiday.holidayGreeting
    const text = name ? `${greeting}，${name}` : greeting
    logger.info('[工作台问候语] 使用假日: HolidayGreeting=', holiday.holidayGreeting, ', 展示=', text)
    return text
  }
  const hour = now.value.getHours()
  let key: string
  if (hour < 9) key = 'common.greeting.morning'
  else if (hour < 12) key = 'common.greeting.forenoon'
  else if (hour < 14) key = 'common.greeting.noon'
  else if (hour < 18) key = 'common.greeting.afternoon'
  else key = 'common.greeting.night'
  const greeting = t(key)
  return name ? `${greeting}${name}` : greeting
})

// 引用区：假日时严格显示 holidayQuote（无值则回退 common.quote），非假日显示按日轮换的 common.quote
// 显式依赖 currentLocale，切换语言时重新计算
const quoteText = computed(() => {
  void currentLocale.value
  const holiday = holidayFromToken.value
  if (holiday?.isHolidayToday && holiday.holidayQuote) return holiday.holidayQuote
  const letters = 'abcdefghijklmnopqrstuvwxyz'
  const idx = now.value.getDate() % 26
  const key = `common.quote.${letters[idx]}`
  return t(key)
})
</script>

<style scoped lang="less">
.welcome-module {
  padding: 16px 0;
  min-height: 128px;
  .welcome-row {
    margin-bottom: 12px;
  }
  .welcome-greeting-col {
    font-size: 18px;
    font-weight: 500;
  }
  .welcome-date-col {
    font-size: 14px;
    color: var(--ant-color-text-tertiary);
    text-align: right;
  }
  .welcome-quote {
    display: flex;
    align-items: center;
    gap: 8px;
    color: var(--ant-color-text-secondary);
    font-size: 22px;
    font-weight: bold;
    line-height: 1.6;
    .welcome-quote-icon {
      flex-shrink: 0;
      display: inline-flex;
      align-items: center;
      font-size: 22px;
      animation: fa-shake 1s linear infinite;
    }
  }
}
</style>