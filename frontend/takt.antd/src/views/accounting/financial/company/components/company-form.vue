<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/company/components -->
<!-- 文件名称：company-form.vue -->
<!-- 创建时间：2025-02-13 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：公司表单组件 -->
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
          <a-form-item :label="t('entity.company.code')" name="companyCode">
            <a-input v-model:value="formState.companyCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.company.code') })" show-count :maxlength="4" :disabled="!!formData?.companyId" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.company.name')" name="companyName">
            <a-input v-model:value="formState.companyName" :placeholder="t('common.form.placeholder.required', { field: t('entity.company.name') })" show-count :maxlength="200" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="简称" name="shortName">
            <a-input v-model:value="formState.shortName" placeholder="请输入简称" show-count :maxlength="100" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="完整地址" name="address">
            <a-input v-model:value="formState.address" placeholder="请输入完整地址" show-count :maxlength="500" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="公司电话" name="companyPhone">
            <a-input v-model:value="formState.companyPhone" placeholder="请输入公司电话" show-count :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="公司邮箱" name="companyEmail">
            <a-input v-model:value="formState.companyEmail" placeholder="请输入公司邮箱" show-count :maxlength="100" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="排序号" name="orderNum">
            <a-input-number v-model:value="formState.orderNum" :min="0" placeholder="越小越靠前" style="width: 100%" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
            <a-textarea v-model:value="formState.remark" :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })" :rows="3" show-count :maxlength="500" />
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
import type { Company, CompanyCreate } from '@/types/accounting/financial/company'

const { t } = useI18n()

interface Props {
  formData?: Partial<Company>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 6
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  companyCode?: string
  companyName?: string
  shortName?: string
  address?: string
  companyPhone?: string
  companyEmail?: string
  orderNum?: number
  remark?: string
}

const formState = reactive<FormState>({
  companyCode: '',
  companyName: '',
  shortName: '',
  address: '',
  companyPhone: '',
  companyEmail: '',
  orderNum: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  companyCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.company.code') }), trigger: 'blur' },
    { len: 4, message: t('common.form.placeholder.lengthExact', { field: t('entity.company.code'), length: 4 }), trigger: 'blur' }
  ],
  companyName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.company.name') }), trigger: 'blur' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      companyCode: newData.companyCode ?? '',
      companyName: newData.companyName ?? '',
      shortName: newData.shortName ?? '',
      address: newData.address ?? '',
      companyPhone: newData.companyPhone ?? '',
      companyEmail: newData.companyEmail ?? '',
      orderNum: newData.orderNum ?? 0,
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      companyCode: '',
      companyName: '',
      shortName: '',
      address: '',
      companyPhone: '',
      companyEmail: '',
      orderNum: 0,
      remark: ''
    })
  }
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): CompanyCreate & { companyId?: string } => ({
  companyCode: formState.companyCode ?? '',
  companyName: formState.companyName ?? '',
  shortName: formState.shortName || undefined,
  address: formState.address || undefined,
  companyPhone: formState.companyPhone || undefined,
  companyEmail: formState.companyEmail || undefined,
  orderNum: formState.orderNum ?? 0,
  remark: formState.remark || undefined,
  ...(props.formData?.companyId ? { companyId: props.formData.companyId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    companyCode: '',
    companyName: '',
    shortName: '',
    address: '',
    companyPhone: '',
    companyEmail: '',
    orderNum: 0,
    remark: ''
  })
}

defineExpose({ validate, getValues, resetFields })
</script>
