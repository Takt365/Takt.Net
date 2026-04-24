<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-work/components -->
<!-- 文件名称：work-form.vue -->
<!-- 功能描述：员工工作经历维护弹窗内嵌表单。由 employee-work/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
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
              :label="t('entity.employeework.employeeId')"
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
              :label="t('entity.employeework.companyName')"
              name="companyName"
            >
              <a-input
                v-model:value="formState.companyName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeework.positionName')"
              name="positionName"
            >
              <a-input
                v-model:value="formState.positionName"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeework.witnessName')"
              name="witnessName"
            >
              <a-input
                v-model:value="formState.witnessName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeework.witnessPhone')"
              name="witnessPhone"
            >
              <a-input
                v-model:value="formState.witnessPhone"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeework.startDate')"
              name="startDate"
            >
              <a-date-picker
                v-model:value="formState.startDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeework.endDate')"
              name="endDate"
            >
              <a-date-picker
                v-model:value="formState.endDate"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12" />
        </a-row>

        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item
              :label="t('entity.employeework.jobContent')"
              name="jobContent"
            >
              <a-textarea
                v-model:value="formState.jobContent"
                :rows="4"
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
 * 员工工作经历表单脚本：与 `employee-work/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { EmployeeWork, EmployeeWorkCreate } from '@/types/human-resource/personnel/employee-work'

const { t } = useI18n()

/** 当前激活 Tab：basic */
const activeTab = ref('basic')
/** Ant Design Vue 表单实例，用于 validate、resetFields */
const formRef = ref()

/**
 * 组件入参：由列表页传入当前编辑数据或空对象（新增）。
 */
interface Props {
  /** 当前编辑数据（编辑时包含 employeeWorkId） */
  formData?: Partial<EmployeeWork>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * 表单模型（对应 EmployeeWorkCreate）
 */
interface FormState extends EmployeeWorkCreate {}

/**
 * 构建空表单模型
 */
function createEmptyFormState(): FormState {
  return {
    employeeId: '',
    companyName: '',
    positionName: '',
    jobContent: '',
    startDate: '',
    endDate: '',
    witnessName: '',
    witnessPhone: ''
  }
}

/** a-form 绑定模型 */
const formState = reactive<FormState>(createEmptyFormState())

/** 表单校验规则（name 与 a-form-item 的 name 一一对应） */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeework.employeeId') }),
      trigger: 'blur'
    }
  ],
  companyName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employeework.companyName') }),
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
        companyName: data.companyName ?? '',
        positionName: data.positionName ?? '',
        jobContent: data.jobContent ?? '',
        startDate: data.startDate ?? '',
        endDate: data.endDate ?? '',
        witnessName: data.witnessName ?? '',
        witnessPhone: data.witnessPhone ?? ''
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
const getValues = (): EmployeeWorkCreate => {
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
