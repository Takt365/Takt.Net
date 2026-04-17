<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/clock-in/components -->
<!-- 文件名称：attendance-punch-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：打卡记录维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。字典 hr_attendance_punch_type、hr_attendance_punch_source。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    layout="horizontal"
    label-align="right"
  >
    <a-tabs v-model:active-key="activeTab">
      <a-tab-pane
        key="basic"
        :tab="t('common.form.tabs.basicInfo')"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancepunch.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancepunch.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancepunch.punchtime')"
                name="punchTime"
              >
                <a-date-picker
                  v-model:value="formState.punchTime"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancepunch.punchtime') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancepunch.punchtype')"
                name="punchType"
              >
                <TaktSelect
                  v-model:value="formState.punchType"
                  dict-type="hr_attendance_punch_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancepunch.punchtype') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancepunch.punchsource')"
                name="punchSource"
              >
                <TaktSelect
                  v-model:value="formState.punchSource"
                  dict-type="hr_attendance_punch_source"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancepunch.punchsource') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.attendancepunch.punchaddress')"
                name="punchAddress"
              >
                <a-input
                  v-model:value="formState.punchAddress"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('common.entity.remark')"
                name="remark"
              >
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
import type { AttendancePunch, AttendancePunchCreate } from '@/types/human-resource/attendance-leave/attendance-punch'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<AttendancePunch>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyAttendancePunchForm(): AttendancePunchCreate {
  return {
    employeeId: '',
    punchTime: '',
    punchType: 1,
    punchSource: 0,
    punchAddress: undefined,
    remark: ''
  }
}

const formState = reactive<AttendancePunchCreate>(createEmptyAttendancePunchForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancepunch.employeeid') }), trigger: 'blur' }
  ],
  punchTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancepunch.punchtime') }), trigger: 'change' }
  ],
  punchType: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancepunch.punchtype') }), trigger: 'change' }
  ],
  punchSource: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancepunch.punchsource') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      let pt = ''
      if (newData.punchTime) {
        const s = String(newData.punchTime)
        pt = s.length >= 19 ? s.slice(0, 19).replace('T', ' ') : s
      }
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        punchTime: pt,
        punchType: newData.punchType != null ? Number(newData.punchType) : 1,
        punchSource: newData.punchSource != null ? Number(newData.punchSource) : 0,
        punchAddress: newData.punchAddress != null ? String(newData.punchAddress) : undefined,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, {
        employeeId: '',
        punchTime: '',
        punchType: 1,
        punchSource: 0,
        punchAddress: undefined,
        remark: ''
      })
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendancePunchCreate & { punchId?: string } => {
  const pt = String(formState.punchTime ?? '').replace(' ', 'T')
  const payload: AttendancePunchCreate & { punchId?: string } = {
    employeeId: formState.employeeId,
    punchTime: pt,
    punchType: Number(formState.punchType ?? 1),
    punchSource: Number(formState.punchSource ?? 0),
    punchAddress: formState.punchAddress,
    remark: formState.remark || undefined
  }
  if (props.formData?.punchId != null && String(props.formData.punchId).length > 0) {
    payload.punchId = String(props.formData.punchId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyAttendancePunchForm())
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
