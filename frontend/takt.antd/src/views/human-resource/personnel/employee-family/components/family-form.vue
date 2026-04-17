<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-family/components -->
<!-- 文件名称：family-form.vue -->
<!-- 功能描述：员工家庭成员维护弹窗内嵌表单。由 employee-family/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
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
        :tab="t('common.form.tabs.basicInfo')"
        force-render
      >
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.employeeId')"
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
              :label="t('entity.employeefamily.memberName')"
              name="memberName"
            >
              <a-input
                v-model:value="formState.memberName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.relationType')"
              name="relationType"
            >
              <a-input-number
                v-model:value="formState.relationType"
                :min="0"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.phoneNumber')"
              name="phoneNumber"
            >
              <a-input
                v-model:value="formState.phoneNumber"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.workUnit')"
              name="workUnit"
            >
              <a-input
                v-model:value="formState.workUnit"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.jobTitle')"
              name="jobTitle"
            >
              <a-input
                v-model:value="formState.jobTitle"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.birthDate')"
              name="birthDate"
            >
              <a-date-picker
                v-model:value="formState.birthDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeefamily.isEmergencyContact')"
              name="isEmergencyContact"
            >
              <a-select
                v-model:value="formState.isEmergencyContact"
                :options="yesNoOptions"
                allow-clear
                style="width: 100%"
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
 * 员工家庭成员表单脚本：与 `employee-family/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { EmployeeFamily, EmployeeFamilyCreate } from '@/types/human-resource/personnel/employee-family'

const { t } = useI18n()

/** 当前激活 Tab：basic */
const activeTab = ref('basic')
/** Ant Design Vue 表单实例，用于 validate、resetFields */
const formRef = ref()

/**
 * 组件入参：由列表页传入当前编辑数据或空对象（新增）。
 */
interface Props {
  /** 当前编辑数据（编辑时包含 employeeFamilyId） */
  formData?: Partial<EmployeeFamily>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * 表单模型（对应 EmployeeFamilyCreate）
 */
interface FormState extends EmployeeFamilyCreate {}

/**
 * 构建空表单模型
 */
function createEmptyFormState(): FormState {
  return {
    employeeId: '',
    memberName: '',
    relationType: 0,
    phoneNumber: '',
    workUnit: '',
    jobTitle: '',
    birthDate: '',
    isEmergencyContact: 0
  }
}

/** a-form 绑定模型 */
const formState = reactive<FormState>(createEmptyFormState())

/** 是否类枚举选项 */
const yesNoOptions = [{ label: t('common.button.yes'), value: 1 }, { label: t('common.button.no'), value: 0 }]

/** 表单校验规则（name 与 a-form-item 的 name 一一对应） */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeefamily.employeeId') }),
      trigger: 'blur'
    }
  ],
  memberName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeefamily.memberName') }),
      trigger: 'blur'
    }
  ]
}))

/** 监听 formData：编辑态回填，新增态清空 */
watch(
  () => props.formData,
  (data) => {
    if (data && Object.keys(data).length > 0) {
      Object.assign(formState, {
        employeeId: data.employeeId ?? '',
        memberName: data.memberName ?? '',
        relationType: data.relationType ?? 0,
        phoneNumber: data.phoneNumber ?? '',
        workUnit: data.workUnit ?? '',
        jobTitle: data.jobTitle ?? '',
        birthDate: data.birthDate ?? '',
        isEmergencyContact: data.isEmergencyContact ?? 0
      })
    } else {
      Object.assign(formState, createEmptyFormState())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/** 执行 a-form 校验；父组件在提交前 await 本方法 */
const validate = async () => {
  await formRef.value?.validate()
}

/** 获取提交载荷 */
const getValues = (): EmployeeFamilyCreate => {
  return { ...formState }
}

/** 重置表单字段与校验状态 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyFormState())
  activeTab.value = 'basic'
}

/** 暴露给父组件：validate/getValues/resetFields */
defineExpose({
  validate,
  getValues,
  resetFields
})
</script>
