<template>
  <a-layout-header class="takt-header" :style="headerStyle" :data-height="height">
    <div class="header-left">
      <slot name="left">
        <a-button type="text" class="trigger" @click.stop="handleToggle">
          <template #icon>
            <RiMenuUnfoldLine v-if="props.collapsed" />
            <RiMenuFoldLine v-else />
          </template>
        </a-button>
        <TaktBreadcrumb />
      </slot>
    </div>
    <div class="header-right">
      <slot name="right">
        <TaktHeaderQuery />
        <TaktHeaderFull />
        <TaktHeaderFont />
        <TaktHeaderNotification
          :notifications="notifications"
          :dot="notificationDot"
          :overflow-count="notificationOverflowCount"
          @click="handleNotificationClick"
          @read="handleNotificationRead"
          @delete="handleNotificationDelete"
          @clear-all="handleNotificationClearAll"
        />
        <TaktLocaleToggle type="icon" />
        <TaktThemeToggle type="icon" />
        <TaktHeaderSetting type="icon"/>
        <TaktHeaderUser 
          type="icon"
          @profile="handleProfile"
          @settings="handleSettings"
          @logout="handleLogout"
        />
      </slot>
    </div>
  </a-layout-header>
</template>

<script setup lang="ts">
import { computed, ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/identity/user'
import { RiMenuUnfoldLine, RiMenuFoldLine } from '@remixicon/vue'
import TaktLocaleToggle from '@/components/common/takt-locale-toggle/index.vue'
import TaktThemeToggle from '@/components/common/takt-theme-toggle/index.vue'
import TaktHeaderQuery from '../takt-header-query/index.vue'
import TaktHeaderFull from '../takt-header-full/index.vue'
import TaktHeaderFont from '../takt-header-font/index.vue'
import TaktHeaderNotification from '../takt-header-notification/index.vue'
import TaktHeaderSetting from '../takt-header-setting/index.vue'
import TaktHeaderUser from '../takt-header-user/index.vue'
import TaktBreadcrumb from '../takt-breadcrumb/index.vue'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'

interface Notification {
  id: string
  title: string
  content: string
  time: string
  read: boolean
}

type HeaderHeight = 32 | 40 | 48

interface Props {
  collapsed?: boolean
  fixed?: boolean
  leftOffset?: string
  height?: HeaderHeight
  notifications?: Notification[]
  notificationDot?: boolean
  notificationOverflowCount?: number
}

const props = withDefaults(defineProps<Props>(), {
  collapsed: false,
  fixed: true,
  leftOffset: '0px',
  height: 40,
  notifications: () => [],
  notificationDot: false,
  notificationOverflowCount: 99
})

const emit = defineEmits<{
  'update:collapsed': [value: boolean]
  'toggle': []
  'profile': []
  'settings': []
  'logout': []
  'notification-click': []
  'notification-read': [id: string]
  'notification-delete': [id: string]
  'notification-clear-all': []
}>()

const router = useRouter()
const userStore = useUserStore()
const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)

// 视口宽度
const viewportWidth = ref(typeof window !== 'undefined' ? window.innerWidth : 0)

// 计算 header 宽度：视口宽度 - leftOffset
const headerWidth = computed(() => {
  const leftOffsetValue = parseFloat(props.leftOffset) || 0
  return viewportWidth.value - leftOffsetValue
})


// 监听视口宽度变化
const handleResize = () => {
  if (typeof window !== 'undefined') {
    viewportWidth.value = window.innerWidth
  }
}

onMounted(() => {
  if (typeof window !== 'undefined') {
    viewportWidth.value = window.innerWidth
    window.addEventListener('resize', handleResize)
  }
})

onUnmounted(() => {
  if (typeof window !== 'undefined') {
    window.removeEventListener('resize', handleResize)
  }
})

const headerStyle = computed(() => {
  const isFixed = (props.fixed || settingSafe.value.fixedHeader)
  return {
    position: isFixed ? 'fixed' : 'relative',
    top: isFixed ? 0 : undefined,
    right: isFixed ? 0 : undefined,
    left: isFixed ? props.leftOffset : undefined,
    width: isFixed ? `${headerWidth.value}px` : undefined,
    zIndex: isFixed ? 100 : undefined,
    '--header-height': `${props.height}px`,
    '--header-width': isFixed ? `${headerWidth.value}px` : undefined
  }
})

const handleToggle = (e?: Event) => {
  console.log('handleToggle called', { collapsed: props.collapsed, event: e })
  const newCollapsed = !props.collapsed
  emit('update:collapsed', newCollapsed)
  emit('toggle')
}

const handleProfile = () => {
  emit('profile')
}

const handleSettings = () => {
  emit('settings')
}

const handleLogout = async () => {
  emit('logout')
  await userStore.logout()
  router.push('/login')
}

const handleNotificationClick = () => {
  emit('notification-click')
}

const handleNotificationRead = (id: string) => {
  emit('notification-read', id)
}

const handleNotificationDelete = (id: string) => {
  emit('notification-delete', id)
}

const handleNotificationClearAll = () => {
  emit('notification-clear-all')
}
</script>

<style scoped lang="less">
.takt-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: var(--header-height, 40px);
  line-height: var(--header-height, 40px);
  padding: 0;
  // 确保 header 能够正确适配主题系统
  // 使用 Ant Design Vue 的主题变量，确保背景色能够根据主题自动切换
  background: var(--ant-color-bg-container) !important;
  .header-left {
    display: flex;
    align-items: center;
    padding-left: 8px;
    gap: 8px;
  }

  .header-right {
    display: flex;
    align-items: center;
    padding-right: 8px;
  }
}
</style>
