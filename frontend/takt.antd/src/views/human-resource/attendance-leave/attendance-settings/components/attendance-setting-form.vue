<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-settings/components -->
<!-- 文件名称：attendance-setting-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：考勤设置维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。默认方案字典 sys_is_default。 -->
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
                :label="t('entity.attendancesetting.settingcode')"
                name="settingCode"
              >
                <a-input
                  v-model:value="formState.settingCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.settingcode') })"
                  show-count
                  :maxlength="64"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.settingname')"
                name="settingName"
              >
                <a-input
                  v-model:value="formState.settingName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.settingname') })"
                  show-count
                  :maxlength="128"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.workstarttime')"
                name="workStartTime"
              >
                <a-input
                  v-model:value="formState.workStartTime"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.workstarttime') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.workendtime')"
                name="workEndTime"
              >
                <a-input
                  v-model:value="formState.workEndTime"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.workendtime') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.lategraceminutes')"
                name="lateGraceMinutes"
              >
                <a-input-number
                  v-model:value="formState.lateGraceMinutes"
                  :min="0"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.lategraceminutes') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.earlyleavegraceminutes')"
                name="earlyLeaveGraceMinutes"
              >
                <a-input-number
                  v-model:value="formState.earlyLeaveGraceMinutes"
                  :min="0"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.earlyleavegraceminutes') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.isdefault')"
                name="isDefault"
              >
                <TaktSelect
                  v-model:value="formState.isDefault"
                  dict-type="sys_is_default"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancesetting.isdefault') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancesetting.ordernum')"
                name="orderNum"
              >
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancesetting.ordernum') })"
                  style="width: 100%"
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
import type { AttendanceSetting, AttendanceSettingCreate } from '@/types/human-resource/attendance-leave/attendance-setting'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<AttendanceSetting>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyAttendanceSettingForm(): AttendanceSettingCreate {
  return {
    settingCode: '',
    settingName: '',
    workStartTime: '09:00',
    workEndTime: '18:00',
    lateGraceMinutes: 0,
    earlyLeaveGraceMinutes: 0,
    isDefault: 0,
    orderNum: 0,
    remark: ''
  }
}

const formState = reactive<AttendanceSettingCreate>(createEmptyAttendanceSettingForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  settingCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesetting.settingcode') }), trigger: 'blur' }
  ],
  settingName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesetting.settingname') }), trigger: 'blur' }
  ],
  workStartTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesetting.workstarttime') }), trigger: 'blur' }
  ],
  workEndTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancesetting.workendtime') }), trigger: 'blur' }
  ],
  isDefault: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancesetting.isdefault') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        settingCode: String(newData.settingCode ?? ''),
        settingName: String(newData.settingName ?? ''),
        workStartTime: String(newData.workStartTime ?? '09:00'),
        workEndTime: String(newData.workEndTime ?? '18:00'),
        lateGraceMinutes: Number(newData.lateGraceMinutes ?? 0),
        earlyLeaveGraceMinutes: Number(newData.earlyLeaveGraceMinutes ?? 0),
        isDefault: newData.isDefault != null ? Number(newData.isDefault) : 0,
        orderNum: Number(newData.orderNum ?? 0),
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyAttendanceSettingForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceSettingCreate & { settingId?: string } => {
  const payload: AttendanceSettingCreate & { settingId?: string } = {
    settingCode: formState.settingCode,
    settingName: formState.settingName,
    workStartTime: formState.workStartTime,
    workEndTime: formState.workEndTime,
    lateGraceMinutes: formState.lateGraceMinutes,
    earlyLeaveGraceMinutes: formState.earlyLeaveGraceMinutes,
    isDefault: Number(formState.isDefault ?? 0),
    orderNum: Number(formState.orderNum ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.settingId != null && String(props.formData.settingId).length > 0) {
    payload.settingId = String(props.formData.settingId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyAttendanceSettingForm())
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
