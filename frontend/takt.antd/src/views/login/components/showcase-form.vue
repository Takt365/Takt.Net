<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/login/components -->
<!-- 文件名称：showcase-form.vue -->
<!-- 功能描述：登录页左侧展示区 SVG（默认 `takt-smart.svg`）。由 `login/index.vue` 引用；`object` 嵌入动效 SVG，`fetch` 将 SVG 内联背景改为透明后生成 blob URL 供展示；`src` 变化时重建 blob 并在卸载时 `revokeObjectURL`。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 动效 SVG：object 优先，内嵌 img 作降级 -->
  <object
    type="image/svg+xml"
    :aria-label="ariaLabel"
    :width="width"
    :height="height"
    :data="objectData"
    class="showcase-form-object"
  >
    <img
      :alt="ariaLabel"
      :src="objectData"
      class="showcase-form-fallback"
    >
  </object>
</template>

<script setup lang="ts">
/**
 * 展示用 SVG 组件：本地默认资源走 fetch+Blob 去背景色；外部 URL 直接绑定。
 */
import { ref, watch, onMounted, onBeforeUnmount } from 'vue'
import taktSmartSvg from '@/assets/images/takt-smart.svg'

/**
 * 可配置嵌入源与尺寸、无障碍文案。
 */
interface Props {
  /**
   * SVG 地址；未传时使用内置 `takt-smart.svg`。
   */
  src?: string
  /**
   * 展示宽度（传给 `object`）。
   */
  width?: number | string
  /**
   * 展示高度（传给 `object`）。
   */
  height?: number | string
  /**
   * `object` / 降级 `img` 的 `aria-label` 与 `alt`。
   */
  ariaLabel?: string
}

const props = withDefaults(defineProps<Props>(), {
  src: taktSmartSvg,
  width: 800,
  height: 256,
  ariaLabel: 'Onboarding app explainer animation'
})

/** 绑定到 `object :data` 与降级 `img :src` */
const objectData = ref(props.src)
/** 本地 SVG 经 Blob 生成的 URL，卸载或切换源时需释放 */
let blobUrlToRevoke: string | null = null

/**
 * 拉取 SVG 文本，将已知浅色背景替换为透明后生成 blob URL。
 *
 * @param url - 可 fetch 的 SVG 地址
 * @returns blob object URL
 */
async function loadSvgWithTransparentBackground(url: string): Promise<string> {
  const res = await fetch(url)
  const text = await res.text()
  const transparent = text.replace(
    /style="background-color:#edf2ff"/i,
    'style="background-color:transparent"'
  )
  const blob = new Blob([transparent], { type: 'image/svg+xml' })
  const blobUrl = URL.createObjectURL(blob)
  blobUrlToRevoke = blobUrl
  return blobUrl
}

/** 按 `props.src` 决定使用透明化 blob 或直连 URL，并清理旧 blob */
function ensureObjectData() {
  const url = props.src ?? taktSmartSvg
  if (url === taktSmartSvg) {
    loadSvgWithTransparentBackground(url).then((blobUrl) => {
      objectData.value = blobUrl
    })
  } else {
    if (blobUrlToRevoke) {
      URL.revokeObjectURL(blobUrlToRevoke)
      blobUrlToRevoke = null
    }
    objectData.value = url
  }
}

/** 外部修改 `src` 时重建展示数据 */
watch(() => props.src, ensureObjectData)

/** 首次挂载与默认 src 一致时触发透明化处理 */
onMounted(ensureObjectData)

/** 释放 blob，避免内存泄漏 */
onBeforeUnmount(() => {
  if (blobUrlToRevoke) {
    URL.revokeObjectURL(blobUrlToRevoke)
    blobUrlToRevoke = null
  }
})
</script>

<style scoped lang="less">
/* object 区域按比例缩放 */
.showcase-form-object {
  display: block;
  object-fit: contain;
}

.showcase-form-fallback {
  max-width: 100%;
  height: auto;
  object-fit: contain;
}
</style>
