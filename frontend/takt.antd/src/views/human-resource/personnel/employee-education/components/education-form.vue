<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-education/components -->
<!-- 文件名称：education-form.vue -->
<!-- 功能描述：员工教育经历维护弹窗内嵌表单。由 employee-education/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
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
              :label="t('entity.employeeeducation.employeeId')"
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
              :label="t('entity.employeeeducation.schoolName')"
              name="schoolName"
            >
              <a-input
                v-model:value="formState.schoolName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeeducation.educationLevel')"
              name="educationLevel"
            >
              <a-input-number
                v-model:value="formState.educationLevel"
                :min="0"
                :max="5"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeeducation.degreeLevel')"
              name="degreeLevel"
            >
              <a-input-number
                v-model:value="formState.degreeLevel"
                :min="0"
                :max="3"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeeducation.majorName')"
              name="majorName"
            >
              <a-input
                v-model:value="formState.majorName"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeeducation.certificateNo')"
              name="certificateNo"
            >
              <a-input
                v-model:value="formState.certificateNo"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeeducation.startDate')"
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
              :label="t('entity.employeeeducation.endDate')"
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
              :label="t('entity.employeeeducation.isHighest')"
              name="isHighest"
            >
              <a-select
                v-model:value="formState.isHighest"
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
 * 员工教育经历表单脚本：与 `employee-education/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { EmployeeEducation, EmployeeEducationCreate } from '@/types/human-resource/personnel/employee-education'

const { t } = useI18n()

/** 当前激活 Tab：basic */
const activeTab = ref('basic')
/** Ant Design Vue 表单实例，用于 validate、resetFields */
const formRef = ref()

/**
 * 组件入参：由列表页传入当前编辑数据或空对象（新增）。
 */
interface Props {
  /** 当前编辑数据（编辑时包含 employeeEducationId） */
  formData?: Partial<EmployeeEducation>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * 表单模型（对应 EmployeeEducationCreate）
 */
interface FormState extends EmployeeEducationCreate {}

/**
 * 构建空表单模型
 */
function createEmptyFormState(): FormState {
  return {
    employeeId: '',
    educationLevel: 0,
    schoolName: '',
    majorName: '',
    degreeLevel: 0,
    startDate: '',
    endDate: '',
    isHighest: 0,
    certificateNo: ''
  }
}

/** a-form 绑定模型 */
const formState = reactive<FormState>(createEmptyFormState())

/** 是否类枚举选项 */
const yesNoOptions = [
  { label: t('common.button.yes'), value: 1 },
  { label: t('common.button.no'), value: 0 }
]

/** 表单校验规则（name 与 a-form-item 的 name 一一对应） */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeeeducation.employeeId') }),
      trigger: 'blur'
    }
  ],
  schoolName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeeeducation.schoolName') }),
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
        educationLevel: data.educationLevel ?? 0,
        schoolName: data.schoolName ?? '',
        majorName: data.majorName ?? '',
        degreeLevel: data.degreeLevel ?? 0,
        startDate: data.startDate ?? '',
        endDate: data.endDate ?? '',
        isHighest: data.isHighest ?? 0,
        certificateNo: data.certificateNo ?? ''
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
const getValues = (): EmployeeEducationCreate => {
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
