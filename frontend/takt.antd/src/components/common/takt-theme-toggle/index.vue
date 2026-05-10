<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/common/takt-theme-toggle
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:主题模式切换组件,用于切换亮色/暗色主题

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <a-dropdown
    :trigger="['click']"
    placement="bottomRight"
  >
    <a-button type="text">
      <template #icon>
        <RiMoonLine v-if="themeStore.themeMode === 'dark'" />
        <RiSunLine v-else />
      </template>
    </a-button>
    <template #overlay>
      <a-menu
        :selected-keys="[themeStore.themeMode]"
        @click="handleMenuClick"
      >
        <a-menu-item key="light">
          <span style="display: inline-flex; align-items: center;">
            <RiSunLine style="margin-right: 8px;" />
            {{ $t('common.page.settings.theme.light') }}
          </span>
        </a-menu-item>
        <a-menu-item key="dark">
          <span style="display: inline-flex; align-items: center;">
            <RiMoonLine style="margin-right: 8px;" />
            {{ $t('common.page.settings.theme.dark') }}
          </span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import { useThemeStore, type ThemeMode } from '@/stores/theme'
import { RiSunLine, RiMoonLine } from '@remixicon/vue'

const themeStore = useThemeStore()

// 处理菜单点击
const handleMenuClick = (info: MenuInfo) => {
  const mouseEvent = info.domEvent instanceof MouseEvent ? info.domEvent : undefined
  themeStore.setTheme(String(info.key) as ThemeMode, mouseEvent)
}
</script>

<style scoped lang="less">
</style>
