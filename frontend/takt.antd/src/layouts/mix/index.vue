<template>
  <a-layout class="mix-layout">
    <!-- 顶部导航栏 -->
    <TaktHeader
      v-model:collapsed="collapsed"
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
            @error="(e) => { logoError = true; console.error('[MixLayout] Logo 图片加载失败:', { logoUrl, error: e }) }"
            @load="() => console.log('[MixLayout] Logo 图片加载成功:', logoUrl)"
          />
          <span class="title-text">{{ settingSafe.logoText }}</span>
        </div>
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
    
    <a-layout :style="{ marginTop: settingSafe.fixedHeader ? `${headerHeight + 40}px` : '40px' }">
      <a-layout-sider
        v-model:collapsed="collapsed"
        :width="settingSafe.siderWidth"
        :collapsed-width="settingSafe.siderCollapsedWidth"
        class="layout-sider"
        :theme="themeStore.themeMode === 'dark' ? 'dark' : 'light'"
        :style="{ position: settingSafe.fixedSider ? 'fixed' : 'relative', height: `calc(100vh - ${headerHeight}px)`, left: 0, top: settingSafe.fixedHeader ? `${headerHeight}px` : 0 }"
      >
        <TaktMixMenu
          :collapsed="collapsed"
        />
      </a-layout-sider>
      
      <!-- 内容区域 -->
      <a-layout :style="{ marginLeft: settingSafe.fixedSider ? (collapsed ? settingSafe.siderCollapsedWidth + 'px' : settingSafe.siderWidth + 'px') : 0 }">
        <a-layout-content :class="['layout-content', `content-width-${settingSafe.contentWidth}`]" :style="{ marginBottom: '2px', maxWidth: settingSafe.contentWidth === 'fixed' ? '1200px' : 'none', marginLeft: settingSafe.contentWidth === 'fixed' ? 'auto' : '0', marginRight: settingSafe.contentWidth === 'fixed' ? 'auto' : '0', maxHeight: contentMaxHeight }">
          <RouterView />
        </a-layout-content>
        <TaktFooter :height="40" />
      </a-layout>
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
const logoError = ref(false)
const collapsed = ref(false)

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

const contentMaxHeight = computed(() => 'calc(100vh - 44px)')
</script>

<style scoped lang="less">
.mix-layout {
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
