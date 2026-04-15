<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-correction/components -->
<!-- 文件名称：attendance-correction-form.vue -->
<!-- 功能描述：补卡维护弹窗内嵌表单。由 attendance-correction/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。布局与 leave-form / holiday-form 一致（a-tabs → formContentClass → a-form）。字典 hr_attendance_correction_kind、hr_attendance_correction_approval。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')">
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
              <a-form-item :label="t('entity.attendancecorrection.employeeid')" name="employeeId">
                <a-input
                  v-model:value="formState.employeeId"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancecorrection.employeeid') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.attendancecorrection.targetdate')" name="targetDate">
                <a-date-picker
                  v-model:value="formState.targetDate"
                  value-format="YYYY-MM-DD"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancecorrection.targetdate') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.attendancecorrection.correctionkind')" name="correctionKind">
                <TaktSelect
                  v-model="formState.correctionKind"
                  dict-type="hr_attendance_correction_kind"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancecorrection.correctionkind') })"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.attendancecorrection.requestpunchtime')" name="requestPunchTime">
                <a-date-picker
                  v-model:value="formState.requestPunchTime"
                  show-time
                  value-format="YYYY-MM-DD HH:mm:ss"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancecorrection.requestpunchtime') })"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.attendancecorrection.approvalstatus')" name="approvalStatus">
                <TaktSelect
                  v-model="formState.approvalStatus"
                  dict-type="hr_attendance_correction_approval"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancecorrection.approvalstatus') })"
                  allow-clear
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.attendancecorrection.reason')" name="reason">
                <a-input
                  v-model:value="formState.reason"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.attendancecorrection.reason') })"
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
/**
 * 补卡表单脚本：与 `attendance-correction/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { AttendanceCorrection, AttendanceCorrectionCreate } from '@/types/human-resource/attendance-leave/attendance-correction'
import TaktSelect from '@/components/business/takt-select/index.vue'

const { t } = useI18n()

interface Props {
  formData?: Partial<AttendanceCorrection>
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

function createEmptyCorrectionForm(): AttendanceCorrectionCreate {
  return {
    employeeId: '',
    targetDate: '',
    correctionKind: 1,
    requestPunchTime: '',
    reason: '',
    approvalStatus: 0,
    remark: ''
  }
}

const formState = reactive<AttendanceCorrectionCreate>(createEmptyCorrectionForm())

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancecorrection.employeeid') }), trigger: 'blur' }
  ],
  targetDate: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancecorrection.targetdate') }), trigger: 'change' }
  ],
  correctionKind: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancecorrection.correctionkind') }), trigger: 'change' }
  ],
  requestPunchTime: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancecorrection.requestpunchtime') }), trigger: 'change' }
  ],
  reason: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.attendancecorrection.reason') }), trigger: 'blur' }],
  approvalStatus: [
    { required: true, message: t('common.form.placeholder.select', { field: t('entity.attendancecorrection.approvalstatus') }), trigger: 'change' }
  ]
}))

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      let rt = ''
      if (newData.requestPunchTime) {
        const s = String(newData.requestPunchTime)
        rt = s.length >= 19 ? s.slice(0, 19).replace('T', ' ') : s
      }
      Object.assign(formState, {
        employeeId: String(newData.employeeId ?? ''),
        targetDate: newData.targetDate ? String(newData.targetDate).slice(0, 10) : '',
        correctionKind: newData.correctionKind != null ? Number(newData.correctionKind) : 1,
        requestPunchTime: rt,
        reason: String(newData.reason ?? ''),
        approvalStatus: newData.approvalStatus != null ? Number(newData.approvalStatus) : 0,
        remark: newData.remark != null ? String(newData.remark) : ''
      })
    } else {
      Object.assign(formState, createEmptyCorrectionForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): AttendanceCorrectionCreate & { correctionId?: string } => {
  const payload: AttendanceCorrectionCreate & { correctionId?: string } = {
    employeeId: formState.employeeId,
    targetDate: formState.targetDate ? `${formState.targetDate}T00:00:00` : '',
    correctionKind: Number(formState.correctionKind ?? 1),
    requestPunchTime: String(formState.requestPunchTime ?? '').replace(' ', 'T'),
    reason: formState.reason,
    approvalStatus: Number(formState.approvalStatus ?? 0),
    remark: formState.remark || undefined
  }
  if (props.formData?.correctionId != null && String(props.formData.correctionId).length > 0) {
    payload.correctionId = String(props.formData.correctionId)
  }
  return payload
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyCorrectionForm())
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；占位与 holiday-form 的 style 结构一致 */
</style>
