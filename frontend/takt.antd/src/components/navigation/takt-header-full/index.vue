<template>
  <a-button
    type="text"
    @click="handleToggleFullscreen"
    class="takt-header-full"
    :title="isFullscreen ? $t('common.button.exitFullscreen') : $t('common.button.fullscreen')"
  >
    <template #icon>
      <RiFullscreenExitLine v-if="isFullscreen" />
      <RiFullscreenLine v-else />
    </template>
  </a-button>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
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
