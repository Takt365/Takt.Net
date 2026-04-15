<template>
  <div class="takt-form-designer">
    <fc-designer
      ref="designerRef"
      :height="height"
      :config="designerConfig"
      :locale="zhLocale"
      v-bind="designerAttrsRest"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, watch, nextTick, computed } from 'vue'
import zhLocale from '@form-create/antd-designer/locale/zh-cn.js'

export type FormDesignerRule = Record<string, unknown>[]

const props = withDefaults(
  defineProps<{
    height?: string
    modelValue?: string
    /** 设计器 config（FcDesigner config），透传给 @form-create/antd-designer */
    designerConfig?: Record<string, unknown>
    /** 额外透传给 fc-designer 的其它 props（不含 config/locale/handle） */
    designerAttrs?: Record<string, unknown>
  }>(),
  {
    height: '100vh',
    modelValue: '',
    designerConfig: () => ({}),
    designerAttrs: () => ({})
  }
)

const emit = defineEmits<{ 'update:modelValue': [value: string] }>()

const designerRef = ref<{
  getRule: () => FormDesignerRule
  getJson?: () => string
  setRule: (rule: FormDesignerRule) => void
} | null>(null)

function getRule(): FormDesignerRule {
  return designerRef.value?.getRule() ?? []
}

/** designerConfig：只做最小化可编辑注入，避免影响官方其他功能 */
const designerConfig = computed(() => {
  const input = props.designerConfig ?? {}
  return {
    // fc-designer 内部基于 fieldReadonly 决定表单属性控件是否可编辑（横向/竖向属于表单属性）
    fieldReadonly: false,
    nameReadonly: false,
    ...input
  }
})

/** 透传 menu、mask 等（剔除可能冲突的 config/handle/locale 字段） */
const designerAttrsRest = computed(() => {
  const { config: _c, handle: _h, locale: _l, ...rest } = (props.designerAttrs ?? {}) as Record<string, unknown>
  return rest
})

watch(
  () => props.modelValue,
  (json) => {
    nextTick(() => {
      if (!designerRef.value) return

      const v = String(json ?? '').trim()
      if (!v) {
        designerRef.value.setRule([])
        return
      }

      try {
        const rule = JSON.parse(v) as unknown
        if (Array.isArray(rule)) designerRef.value.setRule(rule as FormDesignerRule)
        else designerRef.value.setRule([])
      } catch {
        designerRef.value.setRule([])
      }
    })
  },
  { immediate: true }
)

function syncToModel() {
  emit('update:modelValue', JSON.stringify(getRule()))
}

defineExpose({
  designerRef,
  getRule,
  getRuleJson: () => JSON.stringify(getRule()),
  setRule: (rule: FormDesignerRule) => designerRef.value?.setRule(rule),
  syncToModel
})
</script>

<style scoped lang="less">
.takt-form-designer {
  width: 100%;
  min-width: 900px;
}
</style>


