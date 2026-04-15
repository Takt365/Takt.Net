<template>
  <!-- 始终用同一层 a-watermark 包裹，仅通过 content 控制显隐，避免开关水印时布局重挂载导致抽屉被关 -->
  <a-watermark
    :content="show ? watermarkContent : ''"
    :gap="gap"
    :offset="offset"
    :font="fontByTheme"
    :rotate="rotate"
    :z-index="zIndex"
    :width="width"
    :height="height"
    :image="show ? image : undefined"
    class="takt-watermark"
  >
    <div class="takt-watermark-content">
      <slot />
    </div>
  </a-watermark>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { useSettingStore } from '@/stores/setting'
import { useThemeStore } from '@/stores/theme'

interface FontConfig {
  fontSize?: number | string
  color?: string
  fontFamily?: string
  fontWeight?: 'normal' | 'light' | 'weight' | number
  fontStyle?: 'none' | 'normal' | 'italic' | 'oblique'
}

interface Props {
  /** 水印文字内容 */
  content?: string | string[]
  /** 水印之间的间距 [x, y] */
  gap?: [number, number]
  /** 水印距离容器左上角的偏移量 [x, y] */
  offset?: [number, number]
  /** 字体配置（不传 color 时按主题自动适配） */
  font?: FontConfig
  /** 旋转角度 */
  rotate?: number
  /** z-index */
  zIndex?: number
  /** 水印宽度 */
  width?: number
  /** 水印高度 */
  height?: number
  /** 图片水印地址 */
  image?: string
}

const props = withDefaults(defineProps<Props>(), {
  content: undefined,
  gap: () => [100, 100],
  offset: () => [0, 0],
  font: undefined,
  rotate: -22,
  zIndex: 1000,
  width: 120,
  height: 64
})

const { setting } = storeToRefs(useSettingStore())
const themeStore = useThemeStore()
const show = computed(() => setting.value.watermark)
const watermarkContent = computed(() => {
  return props.content ?? setting.value.watermarkContent
})

/** 按主题适配水印颜色：亮色用深色半透明，暗色用浅色半透明 */
const fontByTheme = computed(() => {
  const base = props.font ?? { fontSize: 16 }
  if (base.color !== undefined) return { ...base }
  const color =
    themeStore.themeMode === 'dark'
      ? 'rgba(255, 255, 255, 0.12)'
      : 'rgba(0, 0, 0, 0.15)'
  return { ...base, color }
})
</script>

<style scoped lang="less">
.takt-watermark {
  position: relative;
}
/* 给水印容器明确占满视口，水印层才能铺满整页（Ant Design 水印相对容器定位） */
.takt-watermark-content {
  min-height: 100vh;
  width: 100%;
}
</style>
