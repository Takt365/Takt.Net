<template>
  <a-layout-footer class="takt-footer" :style="footerStyle" :data-height="height" v-if="show">
    <slot>
      <div class="footer-content">
        <span>{{ settingSafe.copyright }}</span>
      </div>
    </slot>
  </a-layout-footer>
</template>

<script setup lang="ts">
import { computed } from 'vue'
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
