<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-skill/components -->
<!-- 文件名称：skill-form.vue -->
<!-- 功能描述：员工业务技能维护弹窗内嵌表单。由 employee-skill/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 根：a-form 包裹 a-tabs；formRef 供父级 validate、resetFields -->
  <a-form ref="formRef" :model="formState" :rules="rules" layout="horizontal" label-align="right">
    <a-tabs v-model:activeKey="activeTab">
      <!-- 标签1：基础信息 -->
      <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')" force-render>
        <a-row :gutter="24">
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.employeeId')" name="employeeId"><a-input v-model:value="formState.employeeId" allow-clear /></a-form-item></a-col>
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.skillName')" name="skillName"><a-input v-model:value="formState.skillName" allow-clear /></a-form-item></a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.skillLevel')" name="skillLevel"><a-input-number v-model:value="formState.skillLevel" :min="0" style="width: 100%" /></a-form-item></a-col>
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.certificateName')" name="certificateName"><a-input v-model:value="formState.certificateName" allow-clear /></a-form-item></a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.certificateNo')" name="certificateNo"><a-input v-model:value="formState.certificateNo" allow-clear /></a-form-item></a-col>
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.obtainedDate')" name="obtainedDate"><a-date-picker v-model:value="formState.obtainedDate" value-format="YYYY-MM-DD" style="width: 100%" /></a-form-item></a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12"><a-form-item :label="t('entity.employeeskill.expiryDate')" name="expiryDate"><a-date-picker v-model:value="formState.expiryDate" value-format="YYYY-MM-DD" style="width: 100%" /></a-form-item></a-col>
          <a-col :span="12" />
        </a-row>
      </a-tab-pane>
    </a-tabs>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 员工业务技能表单脚本：与 `employee-skill/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { EmployeeSkill, EmployeeSkillCreate } from '@/types/human-resource/personnel/employee-skill'

const { t } = useI18n()
const activeTab = ref('basic')
const formRef = ref()
interface Props { formData?: Partial<EmployeeSkill>; loading?: boolean }
const props = withDefaults(defineProps<Props>(), { formData: () => ({}), loading: false })
interface FormState extends EmployeeSkillCreate {}
function createEmptyFormState(): FormState {
  return { employeeId: '', skillName: '', skillLevel: 0, certificateName: '', certificateNo: '', obtainedDate: '', expiryDate: '' }
}
const formState = reactive<FormState>(createEmptyFormState())
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.employeeskill.employeeId') }), trigger: 'blur' }],
  skillName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.employeeskill.skillName') }), trigger: 'blur' }]
}))
watch(() => props.formData, (data) => {
  if (data && Object.keys(data).length > 0) {
    Object.assign(formState, { employeeId: data.employeeId ?? '', skillName: data.skillName ?? '', skillLevel: data.skillLevel ?? 0, certificateName: data.certificateName ?? '', certificateNo: data.certificateNo ?? '', obtainedDate: data.obtainedDate ?? '', expiryDate: data.expiryDate ?? '' })
  } else Object.assign(formState, createEmptyFormState())
  activeTab.value = 'basic'
}, { immediate: true, deep: true })
const validate = async () => { await formRef.value?.validate() }
const getValues = (): EmployeeSkillCreate => ({ ...formState })
const resetFields = () => { formRef.value?.resetFields(); Object.assign(formState, createEmptyFormState()); activeTab.value = 'basic' }
defineExpose({ validate, getValues, resetFields })
</script>
