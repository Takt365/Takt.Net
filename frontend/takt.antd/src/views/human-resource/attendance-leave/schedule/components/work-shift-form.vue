<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/schedule/components -->
<!-- 文件名称：work-shift-form.vue -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：班次维护弹窗内嵌表单；结构与 identity/user/components/user-form.vue 一致。跨午夜字典 sys_yes_no。 -->
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
              <a-form-item :label="t('entity.workshift.shiftcode')" name="shiftCode">
                <a-input
                  v-model:value="formState.shiftCode"
                  show-count
                  :maxlength="64"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.workshift.shiftcode') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.workshift.shiftname')" name="shiftName">
                <a-input
                  v-model:value="formState.shiftName"
                  show-count
                  :maxlength="128"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.workshift.shiftname') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.workshift.starttime')" name="startTime">
                <a-input
                  v-model:value="formState.startTime"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.workshift.starttime') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.workshift.endtime')" name="endTime">
                <a-input
                  v-model:value="formState.endTime"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.workshift.endtime') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.workshift.crossmidnight')" name="crossMidnight">
                <TaktSelect
                  v-model:value="formState.crossMidnight"
                  dict-type="sys_yes_no"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.workshift.crossmidnight') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.workshift.ordernum')" name="orderNum">
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.workshift.ordernum') })"
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
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { WorkShift, WorkShiftCreate } from '@/types/human-resource/attendance-leave/work-shift'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

const activeTab = ref('basic')
const formContentClass = 'takt-form-content-rows-10'

interface Props {
  formData?: Partial<WorkShift>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()

function createEmptyWorkShiftForm(): WorkShiftCreate {
  return {
    shiftCode: '',
    shiftName: '',
    startTime: '',
    endTime: '',
    crossMidnight: 0,
    orderNum: 0,
    remark: ''
  }
}

const formState = reactive<WorkShiftCreate>(createEmptyWorkShiftForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  shiftCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.workshift.shiftcode') }), trigger: 'blur' }
  ],
  shiftName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.workshift.shiftname') }), trigger: 'blur' }
  ],
  startTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.workshift.starttime') }), trigger: 'blur' }
  ],
  endTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.workshift.endtime') }), trigger: 'blur' }
  ],
  crossMidnight: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.workshift.crossmidnight') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        shiftCode: String(newData.shiftCode ?? ''),
        shiftName: String(newData.shiftName ?? ''),
        startTime: String(newData.startTime ?? ''),
        endTime: String(newData.endTime ?? ''),
        crossMidnight: newData.crossMidnight != null ? Number(newData.crossMidnight) : 0,
        orderNum: Number(newData.orderNum ?? 0),
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyWorkShiftForm())
    }
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): WorkShiftCreate & { shiftId?: string } => {
  const payload: WorkShiftCreate & { shiftId?: string } = {
    shiftCode: formState.shiftCode,
    shiftName: formState.shiftName,
    startTime: formState.startTime,
    endTime: formState.endTime,
    crossMidnight: Number(formState.crossMidnight ?? 0),
    orderNum: Number(formState.orderNum ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.shiftId != null && String(props.formData.shiftId).length > 0) {
    payload.shiftId = String(props.formData.shiftId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyWorkShiftForm())
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
