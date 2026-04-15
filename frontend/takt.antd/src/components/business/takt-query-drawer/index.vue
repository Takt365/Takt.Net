<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-query-drawer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：Takt 高级查询抽屉组件，封装 a-drawer，统一设置中文按钮和布局 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-drawer
    v-model:open="internalOpen"
    v-bind="drawerProps"
    :title="title"
    :placement="placement"
    :width="width"
    @close="handleClose"
  >
    <a-form
      ref="formRef"
      :model="formModel"
      :layout="formLayout"
      @finish="handleSubmit"
    >
      <slot />
      
      <a-form-item>
        <a-space>
          <a-button type="primary" html-type="submit" :loading="submitLoading">
            {{ submitTextDisplay }}
          </a-button>
          <a-button @click="handleReset">
            {{ resetTextDisplay }}
          </a-button>
        </a-space>
      </a-form-item>
    </a-form>
  </a-drawer>
</template>

<script setup lang="ts">
import { computed, useAttrs, ref } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()

interface Props {
  /** 是否显示抽屉 */
  open?: boolean
  /** 抽屉标题，默认为"高级查询" */
  title?: string
  /** 抽屉位置，默认为"right" */
  placement?: 'top' | 'right' | 'bottom' | 'left'
  /** 抽屉宽度，默认为 400 */
  width?: string | number
  /** 表单数据模型 */
  formModel?: Record<string, any>
  /** 表单布局，默认为"vertical" */
  formLayout?: 'horizontal' | 'vertical' | 'inline'
  /** 查询按钮文本，默认为"查询" */
  submitText?: string
  /** 重置按钮文本，默认为"重置" */
  resetText?: string
  /** 查询按钮加载状态 */
  submitLoading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  title: undefined,
  placement: 'right',
  width: 400,
  formModel: () => ({}),
  formLayout: 'vertical',
  submitText: undefined,
  resetText: undefined,
  submitLoading: false
})

const title = computed(() => props.title ?? t('common.button.advancedQuery'))
const submitTextDisplay = computed(() => props.submitText ?? t('common.button.query'))
const resetTextDisplay = computed(() => props.resetText ?? t('common.button.reset'))

const emit = defineEmits<{
  'update:open': [open: boolean]
  'submit': [values: Record<string, any>]
  'reset': []
  'close': []
}>()

const attrs = useAttrs()
const formRef = ref<FormInstance>()

// 内部 open 状态
const internalOpen = computed({
  get: () => props.open,
  set: (value: boolean) => {
    emit('update:open', value)
  }
})

// 计算 drawer 的所有属性，排除已定义的 props
const drawerProps = computed(() => {
  const { open, title, placement, width, formModel, formLayout, submitText, resetText, submitLoading, ...rest } = attrs
  return rest
})

// 处理查询提交
const handleSubmit = (values: Record<string, any>) => {
  emit('submit', values)
}

// 处理重置
const handleReset = () => {
  formRef.value?.resetFields()
  emit('reset')
}

// 处理关闭
const handleClose = () => {
  emit('close')
  emit('update:open', false)
}

// 暴露方法
defineExpose({
  resetFields: () => formRef.value?.resetFields(),
  validate: () => formRef.value?.validate(),
  validateFields: (nameList?: string[]) => formRef.value?.validateFields(nameList),
  getFieldsValue: () => formRef.value?.getFieldsValue(),
  setFieldsValue: (values: Record<string, any>) => formRef.value?.setFieldsValue(values)
})
</script>

<style scoped lang="less">
// 组件样式（如果需要）
</style>
