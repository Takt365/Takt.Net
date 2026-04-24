<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-exception/components -->
<!-- 文件名称：attendance-exception-form.vue -->
<!-- 功能描述：考勤异常维护弹窗内嵌表单。布局与 leave-form / holiday-form 一致；字典 hr_attendance_exception_type、hr_attendance_exception_handle_status。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicinfo')"
    >
      <div :class="formContentClass">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
          label-align="right"
        >
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceexception.employeeid')"
                name="employeeId"
              >
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendanceexception.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceexception.exceptiondate')"
                name="exceptionDate"
              >
                <a-date-picker
                  v-model:value="formState.exceptionDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceexception.exceptiondate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceexception.exceptiontype')"
                name="exceptionType"
              >
                <TaktSelect
                  v-model="formState.exceptionType"
                  dict-type="hr_attendance_exception_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceexception.exceptiontype') })"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendanceexception.handlestatus')"
                name="handleStatus"
              >
                <TaktSelect
                  v-model="formState.handleStatus"
                  dict-type="hr_attendance_exception_handle_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceexception.handlestatus') })"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.attendanceexception.summary')"
                name="summary"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-input
                  v-model:value="formState.summary"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendanceexception.summary') })"
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
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  :rows="2"
                  show-count
                  :maxlength="500"
                  allow-clear
                />
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
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { AttendanceException, AttendanceExceptionCreate } from '@/types/human-resource/attendance-leave/attendance-exception'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

interface Props {
  formData?: Partial<AttendanceException>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref<FormInstance>()
const activeTab = ref('basic')

const TOTAL_FIELDS = 8
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

function createEmptyExceptionForm(): AttendanceExceptionCreate {
  return {
    employeeId: '',
    exceptionDate: '',
    exceptionType: 1,
    summary: '',
    handleStatus: 0,
    remark: ''
  }
}

const formState = reactive<AttendanceExceptionCreate>(createEmptyExceptionForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendanceexception.employeeid') }), trigger: 'blur' }
  ],
  exceptionDate: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendanceexception.exceptiondate') }), trigger: 'change' }
  ],
  exceptionType: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendanceexception.exceptiontype') }), trigger: 'change' }
  ],
  summary: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.attendanceexception.summary') }), trigger: 'blur' }],
  handleStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendanceexception.handlestatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        exceptionDate: newData.exceptionDate ? String(newData.exceptionDate).slice(0, 10) : '',
        exceptionType: newData.exceptionType != null ? Number(newData.exceptionType) : 1,
        summary: String(newData.summary ?? ''),
        handleStatus: newData.handleStatus != null ? Number(newData.handleStatus) : 0,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyExceptionForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceExceptionCreate & { exceptionId?: string } => {
  const payload: AttendanceExceptionCreate & { exceptionId?: string } = {
    employeeId: formState.employeeId,
    exceptionDate: formState.exceptionDate ? `${formState.exceptionDate}T00:00:00` : '',
    exceptionType: Number(formState.exceptionType ?? 1),
    summary: formState.summary,
    handleStatus: Number(formState.handleStatus ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.exceptionId != null && String(props.formData.exceptionId).length > 0) {
    payload.exceptionId = String(props.formData.exceptionId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyExceptionForm())
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；占位与 holiday-form 的 style 结构一致 */
</style>
