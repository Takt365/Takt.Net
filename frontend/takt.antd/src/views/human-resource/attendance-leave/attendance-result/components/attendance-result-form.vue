<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-result/components -->
<!-- 文件名称：attendance-result-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：考勤日结结果维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。出勤状态字典 hr_attendance_result_status。 -->
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
        :tab="t('common.form.tabs.basicinfo')"
        force-render
      >
        <div :class="formContentClass">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendanceresult.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.attendancedate')"
                name="attendanceDate"
              >
                <a-date-picker
                  v-model:value="formState.attendanceDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.attendancedate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.shiftscheduleid')"
                name="shiftScheduleId"
              >
                <a-input
                  v-model:value="formState.shiftScheduleId"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.attendancestatus')"
                name="attendanceStatus"
              >
                <TaktSelect
                  v-model:value="formState.attendanceStatus"
                  dict-type="hr_attendance_result_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.attendancestatus') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.firstintime')"
                name="firstInTime"
              >
                <a-date-picker
                  v-model:value="formState.firstInTime"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.firstintime') })"
                  style="width: 100%"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.lastouttime')"
                name="lastOutTime"
              >
                <a-date-picker
                  v-model:value="formState.lastOutTime"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.lastouttime') })"
                  style="width: 100%"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.workminutes')"
                name="workMinutes"
              >
                <a-input-number
                  v-model:value="formState.workMinutes"
                  :min="0"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendanceresult.workminutes') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceresult.calculatedat')"
                name="calculatedAt"
              >
                <a-date-picker
                  v-model:value="formState.calculatedAt"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.calculatedat') })"
                  style="width: 100%"
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
import type { AttendanceResult, AttendanceResultCreate } from '@/types/human-resource/attendance-leave/attendance-result'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<AttendanceResult>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function toIso(s: string | undefined): string | undefined {
  if (!s) return undefined
  return s.includes('T') ? s : s.replace(' ', 'T')
}

function fmtDateTime(x: unknown): string | undefined {
  if (!x) return undefined
  const s = String(x)
  return s.length >= 19 ? s.slice(0, 19).replace('T', ' ') : s
}

function createEmptyAttendanceResultForm(): AttendanceResultCreate {
  return {
    employeeId: '',
    attendanceDate: '',
    shiftScheduleId: '',
    attendanceStatus: 0,
    firstInTime: undefined,
    lastOutTime: undefined,
    workMinutes: 0,
    calculatedAt: undefined,
    remark: ''
  }
}

const formState = reactive<AttendanceResultCreate>(createEmptyAttendanceResultForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendanceresult.employeeid') }), trigger: 'blur' }
  ],
  attendanceDate: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendanceresult.attendancedate') }), trigger: 'change' }
  ],
  attendanceStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendanceresult.attendancestatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      const ad = newData.attendanceDate ? String(newData.attendanceDate).slice(0, 10) : ''
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        attendanceDate: ad,
        shiftScheduleId: newData.shiftScheduleId != null && String(newData.shiftScheduleId) !== '' ? String(newData.shiftScheduleId) : '',
        attendanceStatus: newData.attendanceStatus != null ? Number(newData.attendanceStatus) : 0,
        firstInTime: fmtDateTime(newData.firstInTime),
        lastOutTime: fmtDateTime(newData.lastOutTime),
        workMinutes: Number(newData.workMinutes ?? 0),
        calculatedAt: fmtDateTime(newData.calculatedAt),
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyAttendanceResultForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceResultCreate & { resultId?: string } => {
  const payload: AttendanceResultCreate & { resultId?: string } = {
    employeeId: formState.employeeId,
    attendanceDate: formState.attendanceDate ? `${formState.attendanceDate}T00:00:00` : '',
    shiftScheduleId: formState.shiftScheduleId?.trim() || undefined,
    attendanceStatus: Number(formState.attendanceStatus ?? 0),
    firstInTime: toIso(formState.firstInTime),
    lastOutTime: toIso(formState.lastOutTime),
    workMinutes: Number(formState.workMinutes ?? 0),
    calculatedAt: toIso(formState.calculatedAt),
    remark: formState.remark || undefined
  }
  if (props.formData?.resultId != null && String(props.formData.resultId).length > 0) {
    payload.resultId = String(props.formData.resultId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyAttendanceResultForm())
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
