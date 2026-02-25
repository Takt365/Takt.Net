<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')">
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
              <a-form-item :label="t('entity.tenant.name')" name="tenantName">
                <a-input v-model:value="formState.tenantName" :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.name') })" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.tenant.code')" name="tenantCode">
                <a-input v-model:value="formState.tenantCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.code') })" show-count :maxlength="30" :disabled="!!formData?.tenantId" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('common.entity.configId')" name="configId">
                <a-input v-model:value="formState.configId" :placeholder="t('common.form.placeholder.required', { field: t('common.entity.configId') })" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.tenant.starttime')" name="startTime">
                <a-date-picker v-model:value="formState.startTimeValue" format="YYYY-MM-DD" value-format="YYYY-MM-DD" :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.starttime') })" style="width: 100%" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.tenant.endtime')" name="endTime">
                <a-date-picker v-model:value="formState.endTimeValue" format="YYYY-MM-DD" value-format="YYYY-MM-DD" :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.endtime') })" style="width: 100%" />
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
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Tenant, TenantCreate } from '@/types/identity/tenant'
const { t } = useI18n()

interface Props {
  formData?: Partial<Tenant>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
const TOTAL_FIELDS = 6
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  tenantName?: string
  tenantCode?: string
  configId?: string
  startTimeValue?: string
  endTimeValue?: string
  remark?: string
}

const formState = reactive<FormState>({
  tenantName: '',
  tenantCode: '',
  configId: '',
  startTimeValue: '',
  endTimeValue: '',
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  tenantName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.tenant.name') }), trigger: 'blur' }],
  tenantCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.tenant.code') }), trigger: 'blur' }],
  configId: [{ required: true, message: t('common.form.placeholder.required', { field: t('common.entity.configId') }), trigger: 'blur' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      tenantName: newData.tenantName ?? '',
      tenantCode: newData.tenantCode ?? '',
      configId: newData.configId != null ? String(newData.configId) : '',
      startTimeValue: newData.startTime ? newData.startTime.split('T')[0] : '',
      endTimeValue: newData.endTime ? newData.endTime.split('T')[0] : '',
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      tenantName: '',
      tenantCode: '',
      configId: '',
      startTimeValue: '',
      endTimeValue: '',
      remark: ''
    })
  }
  activeTab.value = 'basic'
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): TenantCreate & { tenantId?: string } => ({
  tenantName: formState.tenantName ?? '',
  tenantCode: formState.tenantCode ?? '',
  configId: formState.configId ?? '',
  startTime: formState.startTimeValue || undefined,
  endTime: formState.endTimeValue || undefined,
  remark: formState.remark || undefined,
  ...(props.formData?.tenantId ? { tenantId: props.formData.tenantId } : {})
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    tenantName: '',
    tenantCode: '',
    configId: '',
    startTimeValue: '',
    endTimeValue: '',
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
