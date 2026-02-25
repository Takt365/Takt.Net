<template>
  <a-layout class="top-layout">
    <TaktHeader
      :fixed="settingSafe.fixedHeader"
      :height="headerHeight"
      left-offset="0px"
    >
      <template #left>
        <div class="header-title">
          <img
            v-if="logoUrl && settingSafe.showLogo && !logoError"
            :src="logoUrl"
            :alt="settingSafe.logoText"
            class="logo-image"
            @error="() => { logoError = true }"
            @load="() => {}"
          />
          <span class="title-text">{{ settingSafe.logoText }}</span>
        </div>
        <TaktTopMenu />
      </template>
    </TaktHeader>
    <div 
      :style="settingSafe.fixedHeader ? {
        position: 'fixed',
        top: `${headerHeight}px`,
        left: 0,
        right: 0,
        zIndex: 99
      } : {
        marginTop: 0
      }"
    >
      <TaktTabs />
    </div>
    <a-layout-content 
      :class="['layout-content', `content-width-${settingSafe.contentWidth}`]"
      :style="{
        marginTop: settingSafe.fixedHeader ? `${headerHeight + 40}px` : '40px',
        marginBottom: '2px',
        maxWidth: settingSafe.contentWidth === 'fixed' ? '1200px' : 'none',
        marginLeft: settingSafe.contentWidth === 'fixed' ? 'auto' : '0',
        marginRight: settingSafe.contentWidth === 'fixed' ? 'auto' : '0',
        maxHeight: contentMaxHeight
      }"
    >
      <RouterView />
    </a-layout-content>
    <TaktFooter :height="40" />
  </a-layout>
</template>

<script setup lang="ts">
import { computed, ref, provide } from 'vue'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'

/** 当前内容页注册的刷新函数，供标签栏“刷新当前标签”调用，避免整页 router.go(0)。 */
const currentRefreshFn = ref<(() => void) | null>(null)
provide('taktTabCurrentRefresh', currentRefreshFn)

const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)
const logoError = ref(false)

const logoUrl = computed(() => {
  const s = settingSafe.value
  const logoPath = s.logo
  if (!logoPath || logoPath.trim() === '') return null
  try {
    if (logoPath.startsWith('@/')) return logoPath.replace('@/', '/src/')
    if (logoPath.startsWith('/')) return logoPath
    return `/src/${logoPath}`
  } catch {
    return null
  }
})

const headerHeight = computed(() => (settingSafe.value as any)?.headerHeight ?? 40)

const contentMaxHeight = computed(() => {
  if (settingSafe.value.fixedHeader) {
    return `calc(100vh - ${headerHeight.value}px - 84px)`
  }
  return 'calc(100vh - 84px)'
})
</script>

<style scoped lang="less">
.top-layout {
  height: 100vh;

  .header-title {
    display: flex;
    align-items: center;
    height: 48px;
    gap: 8px;
    margin-right: 16px;

    .logo-image {
      width: 32px;
      height: 32px;
      object-fit: contain;
    }

    .title-text {
      font-size: 18px;
      font-weight: bold;
      color: var(--ant-color-text);
      white-space: nowrap;
    }
  }
}
</style>
