<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/schedule/components -->
<!-- 文件名称：shift-schedule-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：排班计划维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致；班次选项来自 getWorkShiftOptions。 -->
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
              <a-form-item :label="t('entity.shiftschedule.employeeid')" name="employeeId">
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.shiftschedule.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.shiftschedule.scheduledate')" name="scheduleDate">
                <a-date-picker
                  v-model:value="formState.scheduleDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.shiftschedule.scheduledate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.shiftschedule.shiftid')" name="shiftId">
                <a-select
                  v-model:value="formState.shiftId"
                  :options="shiftOptions"
                  :loading="optsLoading"
                  show-search
                  option-filter-prop="label"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.shiftschedule.shiftid') })"
                  style="width: 100%"
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
import { reactive, watch, ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { ShiftSchedule, ShiftScheduleCreate } from '@/types/human-resource/attendance-leave/shift-schedule'
import { getWorkShiftOptions } from '@/api/human-resource/attendance-leave/work-shift'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<ShiftSchedule>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

const shiftOptions = ref<{ label: string; value: string }[]>([])
const optsLoading = ref(false)

function createEmptyShiftScheduleForm(): ShiftScheduleCreate {
  return {
    employeeId: '',
    scheduleDate: '',
    shiftId: '',
    remark: ''
  }
}

const formState = reactive<ShiftScheduleCreate>(createEmptyShiftScheduleForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.shiftschedule.employeeid') }), trigger: 'blur' }
  ],
  scheduleDate: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.shiftschedule.scheduledate') }), trigger: 'change' }
  ],
  shiftId: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.shiftschedule.shiftid') }), trigger: 'change' }
  ]
}))

const loadBusinessOptions = async () => {
  optsLoading.value = true
  try {
    const opts = await getWorkShiftOptions()
    shiftOptions.value = (opts || []).map((o) => ({
      label: String(o.dictLabel ?? ''),
      value: String(o.dictValue ?? '')
    }))
  } finally {
    optsLoading.value = false
  }
}

onMounted(() => {
  void loadBusinessOptions()
})

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        scheduleDate: newData.scheduleDate ? String(newData.scheduleDate).slice(0, 10) : '',
        shiftId: String(newData.shiftId ?? ''),
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyShiftScheduleForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): ShiftScheduleCreate & { shiftScheduleId?: string } => {
  const payload: ShiftScheduleCreate & { shiftScheduleId?: string } = {
    employeeId: formState.employeeId,
    scheduleDate: formState.scheduleDate ? `${formState.scheduleDate}T00:00:00` : '',
    shiftId: formState.shiftId,
    remark: formState.remark || undefined
  }
  if (props.formData?.shiftScheduleId != null && String(props.formData.shiftScheduleId).length > 0) {
    payload.shiftScheduleId = String(props.formData.shiftScheduleId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyShiftScheduleForm())
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
