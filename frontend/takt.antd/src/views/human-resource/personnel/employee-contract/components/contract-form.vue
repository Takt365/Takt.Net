<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-contract/components -->
<!-- 文件名称：contract-form.vue -->
<!-- 功能描述：员工合同维护弹窗内嵌表单。由 employee-contract/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 根：a-form 包裹 a-tabs；formRef 供父级 validate、resetFields -->
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    layout="horizontal"
    label-align="right"
  >
    <a-tabs v-model:active-key="activeTab">
      <!-- 标签1：基础信息 -->
      <a-tab-pane
        key="basic"
        :tab="t('common.form.tabs.basicinfo')"
        force-render
      >
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.employeeId')"
              name="employeeId"
            >
              <a-input
                v-model:value="formState.employeeId"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.contractNo')"
              name="contractNo"
            >
              <a-input
                v-model:value="formState.contractNo"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.contractType')"
              name="contractType"
            >
              <a-input-number
                v-model:value="formState.contractType"
                :min="0"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.contractStatus')"
              name="contractStatus"
            >
              <a-input-number
                v-model:value="formState.contractStatus"
                :min="0"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.startDate')"
              name="startDate"
            >
              <a-date-picker
                v-model:value="formState.startDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.endDate')"
              name="endDate"
            >
              <a-date-picker
                v-model:value="formState.endDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.probationEndDate')"
              name="probationEndDate"
            >
              <a-date-picker
                v-model:value="formState.probationEndDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeecontract.signDate')"
              name="signDate"
            >
              <a-date-picker
                v-model:value="formState.signDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item
              :label="t('entity.employeecontract.signCompany')"
              name="signCompany"
            >
              <a-input
                v-model:value="formState.signCompany"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-tab-pane>
    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 员工合同表单脚本：与 `employee-contract/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { EmployeeContract, EmployeeContractCreate } from '@/types/human-resource/personnel/employee-contract'

const { t } = useI18n()
/** 当前激活 Tab：basic */
const activeTab = ref('basic')
/** Ant Design Vue 表单实例 */
const formRef = ref()

interface Props {
  /** 当前编辑数据（编辑时包含 employeeContractId） */
  formData?: Partial<EmployeeContract>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), { formData: () => ({}), loading: false })
interface FormState extends EmployeeContractCreate {}
function createEmptyFormState(): FormState {
  return { employeeId: '', contractNo: '', contractType: 0, startDate: '', endDate: '', probationEndDate: '', signDate: '', contractStatus: 0, signCompany: '' }
}
const formState = reactive<FormState>(createEmptyFormState())
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.employeecontract.employeeId') }), trigger: 'blur' }],
  contractNo: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.employeecontract.contractNo') }), trigger: 'blur' }]
}))
watch(() => props.formData, (data) => {
  if (data && Object.keys(data).length > 0) {
    Object.assign(formState, { employeeId: data.employeeId ?? '', contractNo: data.contractNo ?? '', contractType: data.contractType ?? 0, startDate: data.startDate ?? '', endDate: data.endDate ?? '', probationEndDate: data.probationEndDate ?? '', signDate: data.signDate ?? '', contractStatus: data.contractStatus ?? 0, signCompany: data.signCompany ?? '' })
  } else Object.assign(formState, createEmptyFormState())
  activeTab.value = 'basic'
}, { immediate: true, deep: true })
const validate = async () => { await formRef.value?.validate() }
const getValues = (): EmployeeContractCreate => ({ ...formState })
const resetFields = () => { formRef.value?.resetFields(); Object.assign(formState, createEmptyFormState()); activeTab.value = 'basic' }
defineExpose({ validate, getValues, resetFields })
</script>
