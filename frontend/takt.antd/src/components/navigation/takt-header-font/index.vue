<template>
  <a-dropdown>
    <a-button type="text" class="takt-header-font">
      <template #icon>
        <RiFontSize />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="handleSizeChange">
        <a-menu-item 
          v-for="size in fontSizeOptions" 
          :key="size" 
          :class="{ 'selected': fontSize === size }"
        >
          {{ size }}px
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'
import { validateFontSize, defaultSetting } from '@/stores/setting'
import { RiFontSize } from '@remixicon/vue'

interface Props {
  modelValue?: number
}

const props = withDefaults(defineProps<Props>(), {
  modelValue: defaultSetting.fontSize
})

const emit = defineEmits<{
  'update:modelValue': [value: number]
  'change': [value: number]
}>()

const fontSize = ref<number>(validateFontSize(props.modelValue ?? 15))

// 字体大小选项：15-22
const fontSizeOptions = computed(() => {
  const options: number[] = []
  for (let i = 15; i <= 22; i++) {
    options.push(i)
  }
  return options
})

const handleSizeChange = (info: MenuInfo) => {
  const newSize = validateFontSize(parseInt(String(info.key), 10))
  fontSize.value = newSize
  emit('update:modelValue', newSize)
  emit('change', newSize)
  
  // 应用字体大小到根元素
  const root = document.documentElement
  root.style.fontSize = `${newSize}px`
}

watch(() => props.modelValue, (val) => {
  if (val !== undefined) {
    const newSize = validateFontSize(val)
    fontSize.value = newSize
    // 应用字体大小到根元素
    const root = document.documentElement
    root.style.fontSize = `${newSize}px`
  }
}, { immediate: true })
</script>

<style scoped lang="less">

</style>
