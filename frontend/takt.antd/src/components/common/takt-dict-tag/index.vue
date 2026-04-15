<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/common/takt-dict-tag -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：字典标签组件，用于显示字典数据的标签样式 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tag
    :class="tagClass"
    v-bind="$attrs"
  >
    <slot>{{ displayLabel }}</slot>
  </a-tag>
</template>

<script setup lang="ts">
import { computed, onMounted, watch } from 'vue'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import { logger } from '@/utils/logger'
import type { TaktSelectOption } from '@/types/common'

interface Props {
  /** 字典选项数据（支持 TaktSelectOption 对象） */
  option?: TaktSelectOption | Record<string, any>
  /** 字典值（如果提供了 option，则优先使用 option.dictValue；如果提供了 dictType，则从 store 中查找） */
  value?: string | number
  /** 字典标签（如果提供了 option，则优先使用 option.dictLabel） */
  label?: string
  /** 字典类型编码（用于从字典 store 获取标签和样式） */
  dictType?: string
  /** 标签颜色（如果提供了，则优先使用，否则根据 cssClass 或 listClass 自动判断） */
  color?: string
  /** 标签尺寸 */
  size?: 'small' | 'middle' | 'large'
}

const props = withDefaults(defineProps<Props>(), {
  size: 'middle'
})

const dictDataStore = useDictDataStore()

// 当前字典选项（可能来自 props.option 或从 store 中查找）
const currentOption = computed<TaktSelectOption | undefined>(() => {
  // 如果直接提供了 option，直接使用
  if (props.option) {
    return props.option as TaktSelectOption
  }
  
  // 如果提供了 dictType 和 value，从 store 中查找
  // 注意：value 是 dictValue（字典值），应该使用 dictValue 匹配，而不是 extLabel
  if (props.dictType && props.value !== undefined && props.value !== null) {
    return dictDataStore.getDictOption(props.value, props.dictType, false) // useExtLabel = false，使用 dictValue 匹配
  }
  
  return undefined
})

// 获取字典标签
const displayLabel = computed(() => {
  // 优先使用 props.label
  if (props.label) {
    return props.label
  }
  
  // 其次使用 currentOption.dictLabel
  if (currentOption.value) {
    return currentOption.value.dictLabel || ''
  }
  
  // 最后使用 props.value
  if (props.value !== undefined && props.value !== null) {
    return String(props.value)
  }
  
  return ''
})

// 获取标签样式类（使用 takt-dict-tag-{classValue}）
const tagClass = computed(() => {
  const classObj: Record<string, boolean> = {}
  
  // 添加尺寸类
  classObj[`takt-dict-tag-${props.size}`] = true
  
  // 获取样式类值（0-69）
  let styleClassValue: number | null = null
  
  if (props.color && typeof props.color === 'string') {
    // 如果明确指定了颜色字符串，尝试解析为数字（0-69）
    const colorNum = parseInt(props.color, 10)
    if (!isNaN(colorNum) && colorNum >= 0 && colorNum <= 69) {
      styleClassValue = colorNum
    }
  } else {
    // 从 currentOption 中获取 cssClass 或 listClass
    const classValue = currentOption.value?.cssClass ?? currentOption.value?.listClass
    
    if (classValue !== undefined && classValue !== null) {
      // 使用 CSS 类名（支持 0-69）
      const classNum = typeof classValue === 'number' ? classValue : parseInt(String(classValue), 10)
      if (!isNaN(classNum) && classNum >= 0 && classNum <= 69) {
        styleClassValue = classNum
      }
    }
  }
  
  // 应用样式类（如果未指定，默认使用 0）
  const finalClassValue = styleClassValue ?? 0
  classObj[`takt-dict-tag-${finalClassValue}`] = true
  
  return classObj
})

// 确保字典数据已加载（仅在必要时加载，作为路由守卫的兜底）
// 注意：正常情况下字典数据在路由守卫中统一加载，此处的加载逻辑仅作为兜底
onMounted(async () => {
  if (props.dictType && props.value !== undefined && props.value !== null) {
    // 如果字典数据未加载且未在加载中，尝试加载（store 内部有等待机制，避免重复加载）
    if (!dictDataStore.isLoaded && !dictDataStore.loading) {
      try {
        await dictDataStore.loadAllDictData()
      logger.debug('[TaktDictTag] 兜底加载字典数据成功')
      } catch (error) {
        logger.error('[TaktDictTag] 兜底加载字典数据失败:', error)
      }
    }
  }
})

// 监听 dictType 和 value 变化，确保数据已加载（兜底逻辑）
watch(
  () => [props.dictType, props.value],
  async ([newDictType, newValue]) => {
    if (newDictType && newValue !== undefined && newValue !== null) {
      // 如果字典数据未加载且未在加载中，尝试加载（store 内部有等待机制，避免重复加载）
      if (!dictDataStore.isLoaded && !dictDataStore.loading) {
        try {
          await dictDataStore.loadAllDictData()
          logger.debug('[TaktDictTag] 兜底加载字典数据成功')
        } catch (error) {
          logger.error('[TaktDictTag] 兜底加载字典数据失败:', error)
        }
      }
    }
  },
  { immediate: false }
)
</script>

<style scoped lang="less">
:deep(.ant-tag) {
  border: none !important;
}

.takt-dict-tag-small {
  font-size: 12px;
  padding: 0 6px;
  height: 20px;
  line-height: 20px;
}

.takt-dict-tag-middle {
  font-size: 14px;
  padding: 0 8px;
  height: 24px;
  line-height: 24px;
}

.takt-dict-tag-large {
  font-size: 16px;
  padding: 0 10px;
  height: 28px;
  line-height: 28px;
}
</style>
