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
          >
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
import { computed, ref, onMounted } from 'vue'
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'
import { useUserStore } from '@/stores/identity/user'
import { useMenuStore } from '@/stores/identity/menu'
import { ensureMenuAndRoutesLoaded } from '@/router'
type HeaderHeight = 32 | 40 | 48

const { setting } = storeToRefs(useSettingStore())
const userStore = useUserStore()
const menuStore = useMenuStore()

onMounted(async () => {
  if (userStore.token && (!menuStore.menuList || menuStore.menuList.length === 0)) {
    await ensureMenuAndRoutesLoaded()
  }
})
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

const headerHeight = computed(() => {
  const headerHeightValue = (settingSafe.value as Record<string, unknown>)?.headerHeight
  if (headerHeightValue === 32 || headerHeightValue === 40 || headerHeightValue === 48) {
    return headerHeightValue
  }
  return 40 as HeaderHeight
})

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
