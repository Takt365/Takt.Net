<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-transfer/components -->
<!-- 文件名称：transfer-form.vue -->
<!-- 功能描述：员工调动维护弹窗内嵌表单。由 employee-transfer/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。表单模型复用 `@/types/human-resource/personnel/employee-transfer` 中 EmployeeTransferCreate。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicInfo')"
    >
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
              :label="t('entity.employeetransfer.employeeId')"
              name="employeeId"
            >
              <a-input
                v-model:value="formState.employeeId"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.employeetransfer.employeeId') })"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.transferType')"
              name="transferType"
            >
              <a-select
                v-model:value="formState.transferType"
                :options="transferTypeOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.employeetransfer.transferType') })"
                allow-clear
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.fromDeptId')"
              name="fromDeptId"
            >
              <a-input
                v-model:value="formState.fromDeptId"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.fromDeptName')"
              name="fromDeptName"
            >
              <a-input
                v-model:value="formState.fromDeptName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.toDeptId')"
              name="toDeptId"
            >
              <a-input
                v-model:value="formState.toDeptId"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.toDeptName')"
              name="toDeptName"
            >
              <a-input
                v-model:value="formState.toDeptName"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeetransfer.effectiveDate')"
              name="effectiveDate"
            >
              <a-date-picker
                v-model:value="formState.effectiveDate"
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
              :label="t('entity.employeetransfer.reason')"
              name="reason"
              :label-col="{ span: 4 }"
              :wrapper-col="{ span: 20 }"
            >
              <a-textarea
                v-model:value="formState.reason"
                :rows="2"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
/**
 * 员工调动表单脚本：与 `employee-transfer/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { EmployeeTransfer, EmployeeTransferCreate } from '@/types/human-resource/personnel/employee-transfer'

const { t } = useI18n()
const entityKey = 'entity.employeetransfer'

interface Props {
  /** 当前编辑行数据，新增时为空对象 */
  formData?: Partial<EmployeeTransfer>
  /** 提交中状态，由父组件传入 */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/** Ant Design Vue 表单实例 */
const formRef = ref()
/** 当前激活的 Tab 键；仅 basic */
const activeTab = ref('basic')

/** 调动类型字典选项 */
const transferTypeOptions = [
  { label: t(`${entityKey}.transferType0`), value: 0 },
  { label: t(`${entityKey}.transferType1`), value: 1 }
]

interface FormState {
  employeeId?: string
  transferType?: number
  fromDeptId?: string
  fromDeptName?: string
  fromPostId?: string
  fromPostName?: string
  toDeptId?: string
  toDeptName?: string
  toPostId?: string
  toPostName?: string
  effectiveDate?: string
  reason?: string
}

/** 表单模型（对应 EmployeeTransferCreate） */
const formState = reactive<FormState>({
  employeeId: '',
  transferType: 0,
  fromDeptId: '',
  fromDeptName: '',
  toDeptId: '',
  toDeptName: '',
  effectiveDate: '',
  reason: ''
})

/** 表单校验规则 */
const rules = computed<Record<string, Rule[]>>(() => ({
  employeeId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.employeeId`) }),
      trigger: 'blur'
    }
  ],
  fromDeptId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.fromDeptId`) }),
      trigger: 'blur'
    }
  ],
  fromDeptName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.fromDeptName`) }),
      trigger: 'blur'
    }
  ],
  toDeptId: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.toDeptId`) }),
      trigger: 'blur'
    }
  ],
  toDeptName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.toDeptName`) }),
      trigger: 'blur'
    }
  ]
}))

/** 监听父级 formData，同步编辑态/新增态表单 */
watch(
  () => props.formData,
  (data) => {
    if (data && Object.keys(data).length > 0) {
      Object.assign(formState, {
        employeeId: data.employeeId ?? '',
        transferType: data.transferType ?? 0,
        fromDeptId: data.fromDeptId ?? '',
        fromDeptName: data.fromDeptName ?? '',
        fromPostId: data.fromPostId,
        fromPostName: data.fromPostName,
        toDeptId: data.toDeptId ?? '',
        toDeptName: data.toDeptName ?? '',
        toPostId: data.toPostId,
        toPostName: data.toPostName,
        effectiveDate: data.effectiveDate ?? '',
        reason: data.reason ?? ''
      })
    } else {
      Object.assign(formState, {
        employeeId: '',
        transferType: 0,
        fromDeptId: '',
        fromDeptName: '',
        toDeptId: '',
        toDeptName: '',
        effectiveDate: '',
        reason: ''
      })
    }
  },
  { immediate: true, deep: true }
)

/** 执行表单校验 */
const validate = async () => {
  await formRef.value?.validate()
}

/** 组装提交载荷 */
const getValues = (): EmployeeTransferCreate => ({
  employeeId: formState.employeeId ?? '',
  transferType: formState.transferType ?? 0,
  fromDeptId: formState.fromDeptId ?? '',
  fromDeptName: formState.fromDeptName ?? '',
  fromPostId: formState.fromPostId,
  fromPostName: formState.fromPostName,
  toDeptId: formState.toDeptId ?? '',
  toDeptName: formState.toDeptName ?? '',
  toPostId: formState.toPostId,
  toPostName: formState.toPostName,
  effectiveDate: formState.effectiveDate,
  reason: formState.reason
})

/** 重置表单 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    employeeId: '',
    transferType: 0,
    fromDeptId: '',
    fromDeptName: '',
    toDeptId: '',
    toDeptName: '',
    effectiveDate: '',
    reason: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
