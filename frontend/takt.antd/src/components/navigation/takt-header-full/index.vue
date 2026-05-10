<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/navigation/takt-header-full
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:全屏切换组件,用于切换浏览器全屏模式

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <a-button
    type="text"
    class="takt-header-full"
    :title="isFullscreen ? $t('common.page.button.exitfullscreen') : $t('common.page.button.fullscreen')"
    @click="handleToggleFullscreen"
  >
    <template #icon>
      <RiFullscreenExitLine v-if="isFullscreen" />
      <RiFullscreenLine v-else />
    </template>
  </a-button>
</template>

<script setup lang="ts">
import { RiFullscreenLine, RiFullscreenExitLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'

const isFullscreen = ref(false)

const handleToggleFullscreen = () => {
  if (!document.fullscreenElement) {
    document.documentElement.requestFullscreen().then(() => {
      isFullscreen.value = true
    }).catch(() => {
      logger.error('[TaktHeaderFull] Failed to enter fullscreen')
    })
  } else {
    document.exitFullscreen().then(() => {
      isFullscreen.value = false
    }).catch(() => {
      logger.error('[TaktHeaderFull] Failed to exit fullscreen')
    })
  }
}

const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})
</script>

<style scoped lang="less">

</style>
