<template>
  <a-layout class="side-layout">
    <a-layout-sider
      v-model:collapsed="collapsed"
      :width="settingSafe.siderWidth"
      :collapsed-width="settingSafe.siderCollapsedWidth"
      class="layout-sider"
      :theme="themeStore.themeMode === 'dark' ? 'dark' : 'light'"
      :style="{ position: settingSafe.fixedSider ? 'fixed' : 'relative', height: '100vh', left: 0, top: 0 }"
    >
      <div class="sider-header" :class="{ collapsed }">
        <img
          v-if="logoUrl && settingSafe.showLogo"
          :src="logoUrl"
          :alt="settingSafe.logoText"
          class="logo-image"
          @error="handleLogoError"
          @load="handleLogoLoad"
        />
        <span class="title-text" v-if="!collapsed">{{ settingSafe.logoText }}</span>
        <span class="title-text collapsed" v-else>{{ settingSafe.logoCollapsedText }}</span>
      </div>
      <TaktSideMenu
        :collapsed="collapsed"
      />
    </a-layout-sider>
    <a-layout :style="{ marginLeft: settingSafe.fixedSider ? (collapsed ? settingSafe.siderCollapsedWidth + 'px' : settingSafe.siderWidth + 'px') : 0 }">
      <TaktHeader
        v-model:collapsed="collapsed"
        :fixed="settingSafe.fixedHeader"
        :height="headerHeight"
        :left-offset="settingSafe.fixedSider ? (collapsed ? settingSafe.siderCollapsedWidth + 'px' : settingSafe.siderWidth + 'px') : '0px'"
      />
      <div 
        :style="settingSafe.fixedHeader ? {
          position: 'fixed',
          top: `${headerHeight}px`,
          left: settingSafe.fixedSider ? (collapsed ? settingSafe.siderCollapsedWidth + 'px' : settingSafe.siderWidth + 'px') : 0,
          right: 0,
          zIndex: 99
        } : {
          marginTop: 0,
          marginLeft: 0
        }"
      >
        <TaktTabs />
      </div>
      <a-layout-content 
        :class="['layout-content', 'layout-content-fixed', `content-width-${settingSafe.contentWidth}`]"
        :style="{
          marginTop: settingSafe.fixedHeader ? `${headerHeight + 40}px` : '40px',
          marginBottom: '2px',
          maxWidth: settingSafe.contentWidth === 'fixed' ? '1200px' : 'none',
          marginLeft: settingSafe.contentWidth === 'fixed' ? 'auto' : '0',
          marginRight: settingSafe.contentWidth === 'fixed' ? 'auto' : '0',
          height: contentMaxHeight,
          maxHeight: contentMaxHeight,
          overflow: 'hidden'
        }"
      >
        <div class="layout-content-inner">
          <RouterView />
        </div>
      </a-layout-content>
      <TaktFooter :height="40" />
    </a-layout>
  </a-layout>
</template>

<script setup lang="ts">
import { ref, computed, provide } from 'vue'
import { storeToRefs } from 'pinia'
import { useThemeStore } from '@/stores/theme'
import { defaultSetting, useSettingStore } from '@/stores/setting'

/** 当前内容页注册的刷新函数，供标签栏“刷新当前标签”调用，避免整页 router.go(0)。 */
const currentRefreshFn = ref<(() => void) | null>(null)
provide('taktTabCurrentRefresh', currentRefreshFn)

const themeStore = useThemeStore()
const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)

const collapsed = ref(false)
const logoError = ref(false)

// Logo 图片 URL
const logoUrl = computed(() => {
  const s = settingSafe.value
  const logoPath = s.logo
  const showLogo = s.showLogo
  if (!showLogo) {
    return null
  }
  if (!logoPath || logoPath.trim() === '') return null
  try {
    if (logoPath.startsWith('@/')) return logoPath.replace('@/', '/src/')
    if (logoPath.startsWith('/')) return logoPath
    return `/src/${logoPath}`
  } catch {
    return null
  }
})

// Logo 图片加载错误处理
const handleLogoError = () => {
  logoError.value = true
}

// Logo 图片加载成功处理（预留，可扩展）
const handleLogoLoad = () => {}

// Header 高度，可以从设置中获取，默认 40px
const headerHeight = computed(() => (settingSafe.value as any)?.headerHeight ?? 40)

// Content 最大高度计算（用于滚动条）
const contentMaxHeight = computed(() => {
  if (settingSafe.value.fixedHeader) {
    return `calc(100vh - ${headerHeight.value}px - 84px)`
  }
  return 'calc(100vh - 84px)'
})
</script>

<style scoped lang="less">
.side-layout {
  height: 100vh;

  .sider-header {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 48px;
    padding: 0 16px;
    background: var(--ant-color-primary);
    transition: all 0.2s;
    gap: 8px;

    .logo-image {
      width: 32px;
      height: 32px;
      min-width: 32px;
      min-height: 32px;
      flex-shrink: 0;
      object-fit: contain;
      display: block !important;
      visibility: visible !important;
      opacity: 1 !important;
      background: transparent;
    }

    .title-text {
      color: var(--ant-color-text-inverse);
      font-size: 18px;
      font-weight: bold;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
      transition: all 0.2s;

      &.collapsed {
        font-size: 20px;
      }
    }

    &.collapsed {
      .title-text:not(.collapsed) {
        display: none;
      }
    }
  }
}
</style>
