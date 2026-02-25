<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/controlling/wage-rate/components -->
<!-- 文件名称：wage-rate-form.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：工资率表单组件 -->
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
          <a-form-item :label="t('entity.wagerate.plantCode')" name="plantCode">
            <a-input v-model:value="formState.plantCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.wagerate.plantCode') })" show-count :maxlength="4" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.wagerate.yearMonth')" name="yearMonth">
            <a-input v-model:value="formState.yearMonth" :placeholder="t('common.form.placeholder.required', { field: t('entity.wagerate.yearMonth') })" show-count :maxlength="6" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.wagerate.wageRateType')" name="wageRateType">
            <a-input-number v-model:value="formState.wageRateType" :min="0" style="width: 100%" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.wagerate.workingDays')" name="workingDays">
            <a-input-number v-model:value="formState.workingDays" :min="0" :precision="2" style="width: 100%" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.wagerate.directWageRate')" name="directWageRate">
            <a-input-number v-model:value="formState.directWageRate" :min="0" :precision="4" style="width: 100%" />
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
import type { WageRate, WageRateCreate } from '@/types/accounting/controlling/wage-rate'

const { t } = useI18n()

interface Props {
  formData?: Partial<WageRate>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 5
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  plantCode?: string
  yearMonth?: string
  wageRateType?: number
  workingDays?: number
  directWageRate?: number
}

const formState = reactive<FormState>({
  plantCode: '',
  yearMonth: '',
  wageRateType: 0,
  workingDays: 0,
  directWageRate: 0
})

const rules = computed<Record<string, Rule[]>>(() => ({
  plantCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.wagerate.plantCode') }), trigger: 'blur' },
    { len: 4, message: t('common.form.placeholder.lengthExact', { field: t('entity.wagerate.plantCode'), length: 4 }), trigger: 'blur' }
  ],
  yearMonth: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.wagerate.yearMonth') }), trigger: 'blur' }],
  wageRateType: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.wagerate.wageRateType') }), trigger: 'blur' }]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        plantCode: newData.plantCode ?? '',
        yearMonth: newData.yearMonth ?? '',
        wageRateType: newData.wageRateType ?? 0,
        workingDays: newData.workingDays ?? 0,
        directWageRate: newData.directWageRate ?? 0
      })
    } else {
      Object.assign(formState, {
        plantCode: '',
        yearMonth: '',
        wageRateType: 0,
        workingDays: 0,
        directWageRate: 0
      })
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): WageRateCreate & { wageRateId?: string } => ({
  plantCode: formState.plantCode ?? '',
  yearMonth: formState.yearMonth ?? '',
  wageRateType: formState.wageRateType ?? 0,
  workingDays: formState.workingDays ?? 0,
  directWageRate: formState.directWageRate ?? 0,
  ...(props.formData?.wageRateId ? { wageRateId: props.formData.wageRateId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    plantCode: '',
    yearMonth: '',
    wageRateType: 0,
    workingDays: 0,
    directWageRate: 0
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
