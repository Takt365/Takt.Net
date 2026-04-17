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
          <span>{{ t('dashboard.workspace.currentTimeLabel') }} {{ dateText }}</span>
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
import { computed, onBeforeUnmount, onMounted, ref, watch } from 'vue'
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

// 切换语言时重新获取用户信息（含节假日），保证工作台问候语/引用随语言更新
watch(
  currentLocale,
  (newVal, oldVal) => {
    if (oldVal !== undefined && newVal !== oldVal && userStore.token) {
      userStore.getUserInfo().catch(() => {})
    }
  }
)

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
  const dayOfYearLabel = t('dashboard.workspace.dayOfYearLabel', { n: d.dayOfYear() })
  return d.format('LL') + ' · ' + d.format('dddd') + ' · Q' + d.quarter() + ' · W' + d.week() + ' · ' + dayOfYearLabel
})

// 问候语：假日时为「简短问候(holidayGreeting)或假日名，用户名」，非假日为时段问候 + 用户名
// 显式依赖 currentLocale，切换语言时重新计算（与 dateText 一致，不单靠 t() 的响应式）
// 显示名：优先用户表 nickName，再兼容 nickname、关联员工实名 realName，否则 userName（与后端 TaktUserInfoDto 字段一致）
const greetingText = computed(() => {
  void currentLocale.value
  const holiday = holidayFromToken.value
  const name =
    userInfo.value?.nickName?.trim() ||
    userInfo.value?.nickname?.trim() ||
    userInfo.value?.realName?.trim() ||
    userInfo.value?.userName ||
    ''
  if (holiday?.isHolidayToday && (holiday.holidayGreeting || holiday.holidayName)) {
    const greeting = holiday.holidayGreeting || holiday.holidayName
    const text = name ? `${greeting}，${name}` : greeting
    logger.info('[工作台问候语] 使用假日: HolidayGreeting=', holiday.holidayGreeting, ', HolidayName=', holiday.holidayName, ', 展示=', text)
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

// 引用区：假日时显示假日引用/诗句（holidayQuote 优先，否则 holidayGreeting），非假日显示按日轮换的 common.quote
// 显式依赖 currentLocale，切换语言时重新计算
const quoteText = computed(() => {
  void currentLocale.value
  const holiday = holidayFromToken.value
  if (holiday?.isHolidayToday && (holiday.holidayQuote ?? holiday.holidayGreeting)) return (holiday.holidayQuote ?? holiday.holidayGreeting)
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