<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-source/components -->
<!-- 文件名称：attendance-source-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：考勤源记录维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。字典 hr_attendance_verify_mode。 -->
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
                :label="t('entity.attendancesource.deviceid')"
                name="deviceId"
              >
                <a-input
                  v-model:value="formState.deviceId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesource.deviceid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesource.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.enrollnumber')"
                name="enrollNumber"
              >
                <a-input
                  v-model:value="formState.enrollNumber"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesource.enrollnumber') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.rawpunchtime')"
                name="rawPunchTime"
              >
                <a-date-picker
                  v-model:value="formState.rawPunchTime"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancesource.rawpunchtime') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.verifymode')"
                name="verifyMode"
              >
                <TaktSelect
                  v-model:value="formState.verifyMode"
                  dict-type="hr_attendance_verify_mode"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancesource.verifymode') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.externalrecordkey')"
                name="externalRecordKey"
              >
                <a-input
                  v-model:value="formState.externalRecordKey"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.downloadbatchno')"
                name="downloadBatchNo"
              >
                <a-input
                  v-model:value="formState.downloadBatchNo"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesource.rawpayloadjson')"
                name="rawPayloadJson"
              >
                <a-textarea
                  v-model:value="formState.rawPayloadJson"
                  :rows="2"
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
import type { AttendanceSource, AttendanceSourceCreate } from '@/types/human-resource/attendance-leave/attendance-source'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<AttendanceSource>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyAttendanceSourceForm(): AttendanceSourceCreate {
  return {
    deviceId: '',
    employeeId: '',
    enrollNumber: '',
    rawPunchTime: '',
    verifyMode: 0,
    externalRecordKey: undefined,
    downloadBatchNo: undefined,
    rawPayloadJson: undefined,
    remark: ''
  }
}

const formState = reactive<AttendanceSourceCreate>(createEmptyAttendanceSourceForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  deviceId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesource.deviceid') }), trigger: 'blur' }
  ],
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesource.employeeid') }), trigger: 'blur' }
  ],
  enrollNumber: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesource.enrollnumber') }), trigger: 'blur' }
  ],
  rawPunchTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesource.rawpunchtime') }), trigger: 'change' }
  ],
  verifyMode: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancesource.verifymode') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      let rt = ''
      if (newData.rawPunchTime) {
        const s = String(newData.rawPunchTime)
        rt = s.length >= 19 ? s.slice(0, 19).replace('T', ' ') : s
      }
      Object.assign(formState, {
        deviceId: String(newData.deviceId ?? ''),
        employeeId: String(newData.employeeId ?? ''),
        enrollNumber: String(newData.enrollNumber ?? ''),
        rawPunchTime: rt,
        verifyMode: newData.verifyMode != null ? Number(newData.verifyMode) : 0,
        externalRecordKey: newData.externalRecordKey != null ? String(newData.externalRecordKey) : undefined,
        downloadBatchNo: newData.downloadBatchNo != null ? String(newData.downloadBatchNo) : undefined,
        rawPayloadJson: newData.rawPayloadJson != null ? String(newData.rawPayloadJson) : undefined,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyAttendanceSourceForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceSourceCreate & { sourceId?: string } => {
  const raw = String(formState.rawPunchTime ?? '').replace(' ', 'T')
  const payload: AttendanceSourceCreate & { sourceId?: string } = {
    deviceId: formState.deviceId,
    employeeId: formState.employeeId,
    enrollNumber: formState.enrollNumber,
    rawPunchTime: raw,
    verifyMode: Number(formState.verifyMode ?? 0),
    externalRecordKey: formState.externalRecordKey,
    downloadBatchNo: formState.downloadBatchNo,
    rawPayloadJson: formState.rawPayloadJson,
    remark: formState.remark || undefined
  }
  if (props.formData?.sourceId != null && String(props.formData.sourceId).length > 0) {
    payload.sourceId = String(props.formData.sourceId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyAttendanceSourceForm())
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
