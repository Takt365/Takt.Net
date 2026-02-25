<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/login/components/svgator-player -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：SVGator 动效 SVG 播放器 -->
<!-- ======================================== -->

<template>
  <object
    type="image/svg+xml"
    :aria-label="ariaLabel"
    :width="width"
    :height="height"
    :data="objectData"
    class="svgator-object"
  >
    <img :alt="ariaLabel" :src="objectData" class="svgator-fallback" />
  </object>
</template>

<script setup lang="ts">
import { ref, watch, onMounted, onBeforeUnmount } from 'vue'
import taktSmartSvg from '@/assets/images/takt-smart.svg'

defineOptions({ name: 'LoginSvgatorPlayer' })

const props = withDefaults(
  defineProps<{
    src?: string
    width?: number | string
    height?: number | string
    ariaLabel?: string
  }>(),
  {
    src: taktSmartSvg,
    width: 800,
    height: 256,
    ariaLabel: 'Onboarding app explainer animation'
  }
)

const objectData = ref(props.src)
let blobUrlToRevoke: string | null = null

async function loadSvgWithTransparentBackground(sourceUrl: string): Promise<string> {
  const response = await fetch(sourceUrl)
  const text = await response.text()
  const transparent = text.replace(
    /style="background-color:#edf2ff"/i,
    'style="background-color:transparent"'
  )
  const blob = new Blob([transparent], { type: 'image/svg+xml' })
  const blobUrl = URL.createObjectURL(blob)
  blobUrlToRevoke = blobUrl
  return blobUrl
}

function ensureObjectData() {
  const sourceUrl = props.src ?? taktSmartSvg
  if (sourceUrl === taktSmartSvg) {
    loadSvgWithTransparentBackground(sourceUrl).then((blobUrl) => {
      objectData.value = blobUrl
    })
  } else {
    if (blobUrlToRevoke) {
      URL.revokeObjectURL(blobUrlToRevoke)
      blobUrlToRevoke = null
    }
    objectData.value = sourceUrl
  }
}

watch(() => props.src, ensureObjectData)

onMounted(ensureObjectData)

onBeforeUnmount(() => {
  if (blobUrlToRevoke) {
    URL.revokeObjectURL(blobUrlToRevoke)
    blobUrlToRevoke = null
  }
})
</script>

<style scoped lang="less">
.svgator-object {
  display: block;
  object-fit: contain;
}

.svgator-fallback {
  max-width: 100%;
  height: auto;
  object-fit: contain;
}
</style>
