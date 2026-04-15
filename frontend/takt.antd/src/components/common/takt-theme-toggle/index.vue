<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-button type="text">
      <template #icon>
        <RiMoonLine v-if="themeStore.themeMode === 'dark'" />
        <RiSunLine v-else />
      </template>
    </a-button>
    <template #overlay>
      <a-menu :selected-keys="[themeStore.themeMode]" @click="handleMenuClick">
        <a-menu-item key="light">
          <span style="display: inline-flex; align-items: center;">
            <RiSunLine style="margin-right: 8px;" />
            {{ $t('common.settings.theme.light') }}
          </span>
        </a-menu-item>
        <a-menu-item key="dark">
          <span style="display: inline-flex; align-items: center;">
            <RiMoonLine style="margin-right: 8px;" />
            {{ $t('common.settings.theme.dark') }}
          </span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { computed } from 'vue'
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
