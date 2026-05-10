<!-- ========================================
项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间：@/layouts
文件名称：index.vue
创建时间：2025-01-20
创建人：Takt365(Cursor AI)
功能描述：主布局入口，根据配置切换侧边栏/顶部/混合布局

版权信息：Copyright (c) 2025 Takt  All rights reserved.
免责声明：此软件使用 MIT License，作者不承担任何使用风险。
======================================== -->

<template>
  <TaktWatermark>
    <SideLayout v-if="settingSafe.layout === 'side'" />
    <TopLayout v-else-if="settingSafe.layout === 'top'" />
    <MixLayout v-else-if="settingSafe.layout === 'mix'" />
    <SideLayout v-else />
  </TaktWatermark>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'
import { useEventBus, AuthEvents, SystemEvents, MenuEvents } from '@/utils/eventBus'
import { useRouter } from 'vue-router'
import TaktWatermark from '@/components/navigation/takt-watermark/index.vue'
import SideLayout from './side/index.vue'
import TopLayout from './top/index.vue'
import MixLayout from './mix/index.vue'

const router = useRouter()
const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)

// ========== 使用组合式函数（自动清理） ==========
const { on } = useEventBus()

/**
 * 监听登出事件 - 清除路由和菜单
 */
const handleLogout = () => {
  // 跳转到登录页
  router.push('/login')
}

/**
 * 监听登录成功 - 刷新菜单
 */
const handleLoginSuccess = () => {
  // 可以在这里执行登录后的初始化操作
  console.log('[Layout] 用户登录成功')
}

/**
 * 监听语言切换 - 刷新界面
 */
const handleLanguageChange = ({ locale }: { locale: string }) => {
  console.log('[Layout] 语言切换:', locale)
  // 语言切换后，刷新菜单和路由
}

/**
 * 监听主题切换
 */
const handleThemeChange = ({ theme }: { theme: 'light' | 'dark' }) => {
  console.log('[Layout] 主题切换:', theme)
}

// 注册事件监听（自动清理，无需 onUnmounted）
on(AuthEvents.DidLogout, handleLogout)
on(AuthEvents.LoginSuccess, handleLoginSuccess)
on(SystemEvents.LanguageChange, handleLanguageChange)
on(SystemEvents.ThemeChange, handleThemeChange)
</script>
