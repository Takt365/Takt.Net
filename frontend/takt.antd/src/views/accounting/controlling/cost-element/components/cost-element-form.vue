<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/controlling/cost-element/components -->
<!-- 文件名称：cost-element-form.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：成本要素表单组件 -->
<!--
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div :class="formContentClass">
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 8 }"
      :wrapper-col="{ span: 16 }"
      layout="horizontal"
      label-align="right"
    >
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.costelement.code')" name="costElementCode">
            <a-input v-model:value="formState.costElementCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.costelement.code') })" show-count :maxlength="50" :disabled="!!formData?.costElementId" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.costelement.name')" name="costElementName">
            <a-input v-model:value="formState.costElementName" :placeholder="t('common.form.placeholder.required', { field: t('entity.costelement.name') })" show-count :maxlength="200" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.costelement.orderNum')" name="orderNum">
            <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('common.entity.remark')" name="remark">
            <a-input v-model:value="formState.remark" show-count :maxlength="500" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { CostElement, CostElementCreate } from '@/types/accounting/controlling/cost-element'

const { t } = useI18n()

interface Props {
  formData?: Partial<CostElement>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 4
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  costElementCode?: string
  costElementName?: string
  orderNum?: number
  remark?: string
}

const formState = reactive<FormState>({
  costElementCode: '',
  costElementName: '',
  orderNum: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  costElementCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.costelement.code') }), trigger: 'blur' }],
  costElementName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.costelement.name') }), trigger: 'blur' }]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        costElementCode: newData.costElementCode ?? '',
        costElementName: newData.costElementName ?? '',
        orderNum: newData.orderNum ?? 0,
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, {
        costElementCode: '',
        costElementName: '',
        orderNum: 0,
        remark: ''
      })
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): CostElementCreate & { costElementId?: string } => ({
  costElementCode: formState.costElementCode ?? '',
  costElementName: formState.costElementName ?? '',
  orderNum: formState.orderNum ?? 0,
  remark: formState.remark || undefined,
  ...(props.formData?.costElementId ? { costElementId: props.formData.costElementId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    costElementCode: '',
    costElementName: '',
    orderNum: 0,
    remark: ''
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
