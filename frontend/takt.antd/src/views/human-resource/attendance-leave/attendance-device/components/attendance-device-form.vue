<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-device/components -->
<!-- 文件名称：attendance-device-form.vue -->
<!-- 功能描述：考勤设备维护弹窗内嵌表单。布局与 leave-form / holiday-form 一致；字典 hr_attendance_device_status。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicInfo')"
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
                :label="t('entity.attendancedevice.devicecode')"
                name="deviceCode"
              >
                <a-input
                  v-model:value="formState.deviceCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancedevice.devicecode') })"
                  show-count
                  :maxlength="64"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.devicename')"
                name="deviceName"
              >
                <a-input
                  v-model:value="formState.deviceName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancedevice.devicename') })"
                  show-count
                  :maxlength="128"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.manufacturer')"
                name="manufacturer"
              >
                <TaktSelect
                  v-model:value="formState.manufacturer"
                  dict-type="hr_attendance_device_brand"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancedevice.manufacturer') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.ipaddress')"
                name="ipAddress"
              >
                <a-input
                  v-model:value="formState.ipAddress"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.port')"
                name="port"
              >
                <a-input-number
                  v-model:value="formState.port"
                  :min="0"
                  :max="65535"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.devicemodel')"
                name="deviceModel"
              >
                <a-input
                  v-model:value="formState.deviceModel"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.attendancedevice.devicestatus')"
                name="deviceStatus"
              >
                <TaktSelect
                  v-model="formState.deviceStatus"
                  dict-type="hr_attendance_device_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancedevice.devicestatus') })"
                  allow-clear
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
import type { AttendanceDevice, AttendanceDeviceCreate } from '@/types/human-resource/attendance-leave/attendance-device'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

interface Props {
  formData?: Partial<AttendanceDevice>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref<FormInstance>()
const activeTab = ref('basic')

const TOTAL_FIELDS = 9
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

function createEmptyDeviceForm(): AttendanceDeviceCreate {
  return {
    deviceCode: '',
    deviceName: '',
    deviceType: 'Generic',
    manufacturer: undefined,
    ipAddress: undefined,
    port: undefined,
    deviceModel: undefined,
    deviceStatus: 1,
    remark: ''
  }
}

const formState = reactive<AttendanceDeviceCreate>(createEmptyDeviceForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  deviceCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancedevice.devicecode') }), trigger: 'blur' }
  ],
  deviceName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancedevice.devicename') }), trigger: 'blur' }
  ],
  manufacturer: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancedevice.manufacturer') }), trigger: 'change' }
  ],
  deviceStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancedevice.devicestatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        deviceCode: String(newData.deviceCode ?? ''),
        deviceName: String(newData.deviceName ?? ''),
        deviceType: newData.deviceType != null ? String(newData.deviceType) : 'Generic',
        manufacturer: newData.manufacturer != null ? String(newData.manufacturer) : undefined,
        ipAddress: newData.ipAddress != null ? String(newData.ipAddress) : undefined,
        port: typeof newData.port === 'number' ? newData.port : newData.port != null ? Number(newData.port) : undefined,
        deviceModel: newData.deviceModel != null ? String(newData.deviceModel) : undefined,
        deviceStatus: newData.deviceStatus != null ? Number(newData.deviceStatus) : 1,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyDeviceForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceDeviceCreate & { deviceId?: string } => {
  const payload: AttendanceDeviceCreate & { deviceId?: string } = {
    deviceCode: formState.deviceCode,
    deviceName: formState.deviceName,
    manufacturer: formState.manufacturer || undefined,
    deviceType: formState.manufacturer || formState.deviceType || 'Generic',
    ipAddress: formState.ipAddress || undefined,
    port: formState.port,
    deviceModel: formState.deviceModel || undefined,
    deviceStatus: Number(formState.deviceStatus ?? 1),
    remark: formState.remark || undefined
  }
  if (props.formData?.deviceId != null && String(props.formData.deviceId).length > 0) {
    payload.deviceId = String(props.formData.deviceId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyDeviceForm())
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；占位与 holiday-form 的 style 结构一致 */
</style>
