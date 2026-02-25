<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/bank/components -->
<!-- 文件名称：bank-form.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：银行表单组件 -->
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
          <a-form-item :label="t('entity.bank.companyCode')" name="companyCode">
            <a-input v-model:value="formState.companyCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.bank.companyCode') })" show-count :maxlength="4" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.code')" name="bankCode">
            <a-input v-model:value="formState.bankCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.bank.code') })" show-count :maxlength="50" :disabled="!!formData?.bankId" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.name')" name="bankName">
            <a-input v-model:value="formState.bankName" :placeholder="t('common.form.placeholder.required', { field: t('entity.bank.name') })" show-count :maxlength="200" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.shortName')" name="shortName">
            <a-input v-model:value="formState.shortName" placeholder="" show-count :maxlength="100" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.swiftCode')" name="swiftCode">
            <a-input v-model:value="formState.swiftCode" show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.address')" name="address">
            <a-input v-model:value="formState.address" show-count :maxlength="500" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.contactPhone')" name="contactPhone">
            <a-input v-model:value="formState.contactPhone" show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.bank.orderNum')" name="orderNum">
            <a-input-number v-model:value="formState.orderNum" :min="0" placeholder="0" style="width: 100%" />
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
import type { Bank, BankCreate } from '@/types/accounting/financial/bank'

const { t } = useI18n()

interface Props {
  formData?: Partial<Bank>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 8
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  companyCode?: string
  bankCode?: string
  bankName?: string
  shortName?: string
  swiftCode?: string
  address?: string
  contactPhone?: string
  orderNum?: number
}

const formState = reactive<FormState>({
  companyCode: '',
  bankCode: '',
  bankName: '',
  shortName: '',
  swiftCode: '',
  address: '',
  contactPhone: '',
  orderNum: 0
})

const rules = computed<Record<string, Rule[]>>(() => ({
  companyCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.bank.companyCode') }), trigger: 'blur' },
    { len: 4, message: t('common.form.placeholder.lengthExact', { field: t('entity.bank.companyCode'), length: 4 }), trigger: 'blur' }
  ],
  bankCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.bank.code') }), trigger: 'blur' }],
  bankName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.bank.name') }), trigger: 'blur' }]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        companyCode: newData.companyCode ?? '',
        bankCode: newData.bankCode ?? '',
        bankName: newData.bankName ?? '',
        shortName: newData.shortName ?? '',
        swiftCode: newData.swiftCode ?? '',
        address: newData.address ?? '',
        contactPhone: newData.contactPhone ?? '',
        orderNum: newData.orderNum ?? 0
      })
    } else {
      Object.assign(formState, {
        companyCode: '',
        bankCode: '',
        bankName: '',
        shortName: '',
        swiftCode: '',
        address: '',
        contactPhone: '',
        orderNum: 0
      })
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): BankCreate & { bankId?: string } => ({
  companyCode: formState.companyCode ?? '',
  bankCode: formState.bankCode ?? '',
  bankName: formState.bankName ?? '',
  shortName: formState.shortName || undefined,
  swiftCode: formState.swiftCode || undefined,
  address: formState.address || undefined,
  contactPhone: formState.contactPhone || undefined,
  orderNum: formState.orderNum ?? 0,
  ...(props.formData?.bankId ? { bankId: props.formData.bankId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    companyCode: '',
    bankCode: '',
    bankName: '',
    shortName: '',
    swiftCode: '',
    address: '',
    contactPhone: '',
    orderNum: 0
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
