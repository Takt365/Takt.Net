<!-- ========================================
项目名称:节拍数字工厂 ·Takt Digital Factory (TDF) 
命名空间:@/components/navigation/takt-footer
文件名称:index.vue
创建时间:2025-01-20
创建人:Takt365(Cursor AI)
功能描述:页面底部组件,显示版权信息

版权信息:Copyright (c) 2025 Takt  All rights reserved.
免责声明:此软件使用 MIT License,作者不承担任何使用风险。
======================================== -->
<template>
  <a-layout-footer
    v-if="show"
    class="takt-footer"
    :style="footerStyle"
    :data-height="height"
  >
    <slot>
      <div class="footer-content">
        <span>{{ settingSafe.copyright }}</span>
      </div>
    </slot>
  </a-layout-footer>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { defaultSetting, useSettingStore } from '@/stores/setting'

type FooterHeight = 32 | 40 | 48

interface Props {
  show?: boolean
  height?: FooterHeight
}

const props = withDefaults(defineProps<Props>(), {
  show: true,
  height: 40
})

const { setting } = storeToRefs(useSettingStore())
const settingSafe = computed(() => setting.value ?? defaultSetting)
const show = computed(() => props.show && settingSafe.value.showFooter)

const footerStyle = computed(() => ({
  '--footer-height': `${props.height}px`
}))
</script>

<style scoped lang="less">
.takt-footer {
  height: var(--footer-height, 40px) !important;
  line-height: var(--footer-height, 40px) !important;
  min-height: var(--footer-height, 40px) !important;
  max-height: var(--footer-height, 40px) !important;
  text-align: center;
  padding: 0 24px;

}
</style>
