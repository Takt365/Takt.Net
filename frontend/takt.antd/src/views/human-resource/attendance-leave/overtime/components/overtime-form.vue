<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/overtime/components -->
<!-- 文件名称：overtime-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：加班维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。字典 hr_overtime_status。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form ref="formRef" :model="formState" :rules="rules" layout="horizontal" label-align="right">
    <a-tabs v-model:activeKey="activeTab">
      <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')" force-render>
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.employeeid')" name="employeeId">
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.overtime.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.overtimedate')" name="overtimeDate">
                <a-date-picker
                  v-model:value="formState.overtimeDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.overtime.overtimedate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.plannedhours')" name="plannedHours">
                <a-input-number
                  v-model:value="formState.plannedHours"
                  :min="0"
                  :step="0.5"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.overtime.plannedhours') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.actualhours')" name="actualHours">
                <a-input-number
                  v-model:value="formState.actualHours"
                  :min="0"
                  :step="0.5"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.overtimestatus')" name="overtimeStatus">
                <TaktSelect
                  v-model:value="formState.overtimeStatus"
                  dict-type="hr_overtime_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.overtime.overtimestatus') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.overtime.reason')" name="reason">
                <a-input
                  v-model:value="formState.reason"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.overtime.reason') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark">
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  :rows="4"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </div>
      </a-tab-pane>
    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Overtime, OvertimeCreate } from '@/types/human-resource/attendance-leave/overtime'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<Overtime>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyOvertimeForm(): OvertimeCreate {
  return {
    employeeId: '',
    overtimeDate: '',
    plannedHours: 0,
    actualHours: undefined,
    reason: '',
    overtimeStatus: 0,
    remark: ''
  }
}

const formState = reactive<OvertimeCreate>(createEmptyOvertimeForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.overtime.employeeid') }), trigger: 'blur' }
  ],
  overtimeDate: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.overtime.overtimedate') }), trigger: 'change' }
  ],
  plannedHours: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.overtime.plannedhours') }), trigger: 'change' }
  ],
  reason: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.overtime.reason') }), trigger: 'blur' }],
  overtimeStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.overtime.overtimestatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        overtimeDate: newData.overtimeDate ? String(newData.overtimeDate).slice(0, 10) : '',
        plannedHours: Number(newData.plannedHours ?? 0),
        actualHours: newData.actualHours != null ? Number(newData.actualHours) : undefined,
        reason: String(newData.reason ?? ''),
        overtimeStatus: newData.overtimeStatus != null ? Number(newData.overtimeStatus) : 0,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyOvertimeForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): OvertimeCreate & { overtimeId?: string } => {
  const payload: OvertimeCreate & { overtimeId?: string } = {
    employeeId: formState.employeeId,
    overtimeDate: formState.overtimeDate ? `${formState.overtimeDate}T00:00:00` : '',
    plannedHours: Number(formState.plannedHours ?? 0),
    actualHours: formState.actualHours != null ? Number(formState.actualHours) : undefined,
    reason: formState.reason,
    overtimeStatus: Number(formState.overtimeStatus ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.overtimeId != null && String(props.formData.overtimeId).length > 0) {
    payload.overtimeId = String(props.formData.overtimeId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyOvertimeForm())
  activeTab.value = 'basic'
}

const setServerValidationErrors = (errors: Array<{ field?: string; message?: string }>) => {
  if (!Array.isArray(errors) || errors.length === 0) return
  const fields = errors
    .filter((e) => e?.field && e?.message)
    .map((e) => ({ name: e.field as string, errors: [e.message as string] }))
  if (fields.length > 0) {
    formRef.value?.setFields(fields)
  }
}

defineExpose({
  validate,
  getValues,
  resetFields,
  setServerValidationErrors
})
</script>

<style scoped lang="less">
:deep(.ant-tabs-content-holder) {
  min-height: 50vh;
}

:deep(.ant-tabs-tabpane) {
  min-height: 50vh;
}
</style>
