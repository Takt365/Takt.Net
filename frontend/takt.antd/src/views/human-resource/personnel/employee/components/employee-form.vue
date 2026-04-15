<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee/components -->
<!-- 文件名称：employee-form.vue -->
<!-- 功能描述：员工维护弹窗内嵌表单。由 employee/index.vue 引用；defineExpose 提供 validate、getValues、resetFields、setServerValidationErrors。 -->
<!--  -->
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
              <a-form-item :label="t('entity.employee.code')" name="employeeCode">
                <a-input
                  v-model:value="formState.employeeCode"
                  :disabled="!isEdit"
                  :placeholder="employeeCodePlaceholder"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.realname')" name="realName">
                <a-input
                  v-model:value="formState.realName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.employee.realname') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row v-if="!isEdit && isAdmin" :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.employee.systemEmployeeCode')"
                name="isSystemEmployeeCode"
                :label-col="{ span: 3 }"
                :wrapper-col="{ span: 21 }"
                :extra="t('entity.employee.systemEmployeeCodeHint')"
              >
                <a-switch v-model:checked="formState.isSystemEmployeeCode" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.formername')" name="formerName">
                <a-input v-model:value="formState.formerName" allow-clear />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.fullname')" name="fullName">
                <a-input v-model:value="formState.fullName" allow-clear />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.nativename')" name="nativeName">
                <a-input v-model:value="formState.nativeName" allow-clear />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.displayname')" name="displayName">
                <a-input v-model:value="formState.displayName" allow-clear />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.gender')" name="gender">
                <TaktSelect
                  v-model:value="formState.gender"
                  :options="genderOptions"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.employee.gender') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.birthdate')" name="birthDate">
                <a-date-picker
                  v-model:value="formState.birthDate"
                  value-format="YYYY-MM-DD"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.idcard')" name="idCard">
                <a-input v-model:value="formState.idCard" allow-clear />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.phone')" name="phone">
                <a-input v-model:value="formState.phone" allow-clear />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.email')" name="email">
                <a-input v-model:value="formState.email" allow-clear />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.nativeplace')" name="nativePlace">
                <TaktSelect
                  v-model:value="formState.nativePlace"
                  dict-type="hr_native_place"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.employee.nativeplace') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.employee.employeestatus')" name="employeeStatus">
                <TaktSelect
                  v-model:value="formState.employeeStatus"
                  dict-type="hr_employee_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.employee.employeestatus') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.employee.currentaddress')" name="currentAddress" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-input v-model:value="formState.currentAddress" allow-clear />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.employee.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-textarea v-model:value="formState.remark" :rows="2" allow-clear />
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
 * 员工表单脚本：与 `employee/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields`、`setServerValidationErrors` 完成弹窗提交流程。
 */
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Employee, EmployeeCreate } from '@/types/human-resource/personnel/employee'
import { useUserStore } from '@/stores/identity/user'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'

const { t } = useI18n()
const userStore = useUserStore()
const dictDataStore = useDictDataStore()

interface Props {
  formData?: Partial<Employee>
  /** 是否为编辑已有员工（有 employeeId） */
  isEdit?: boolean
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  isEdit: false,
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
const TOTAL_FIELDS = 14
const formContentClass = computed(() =>
  TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'
)

const isAdmin = computed(() => {
  const ut = userStore.userInfo?.userType
  return ut === 1 || ut === 2
})

const genderOptions = computed(() => {
  const all = dictDataStore.getDictOptions('sys_user_gender').map((o) => ({
    label: o.label,
    value: Number(o.value)
  }))
  if (!props.isEdit && !formState.isSystemEmployeeCode) {
    return all.filter((o) => o.value === 1 || o.value === 2)
  }
  return all
})

const employeeCodePlaceholder = computed(() => {
  if (props.isEdit) {
    return t('common.form.placeholder.required', { field: t('entity.employee.code') })
  }
  if (formState.isSystemEmployeeCode) {
    return t('entity.employee.systemEmployeeCodeHint')
  }
  return t('entity.employee.employeeCodeCreateHint')
})

interface FormState {
  employeeCode?: string
  isSystemEmployeeCode: boolean
  formerName?: string
  realName?: string
  fullName?: string
  nativeName?: string
  displayName?: string
  gender?: number
  birthDate?: string
  idCard?: string
  phone?: string
  email?: string
  nativePlace?: string
  currentAddress?: string
  employeeStatus?: number
  remark?: string
}

const formState = reactive<FormState>({
  employeeCode: '',
  isSystemEmployeeCode: false,
  formerName: '',
  realName: '',
  fullName: '',
  nativeName: '',
  displayName: '',
  gender: undefined,
  birthDate: '',
  idCard: '',
  phone: '',
  email: '',
  nativePlace: '',
  currentAddress: '',
  employeeStatus: 0,
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  employeeCode: [
    {
      validator: async (_rule, value) => {
        if (!props.isEdit) return Promise.resolve()
        if (value === undefined || value === null || String(value).trim() === '') {
          return Promise.reject(
            new Error(t('common.form.placeholder.required', { field: t('entity.employee.code') }))
          )
        }
        return Promise.resolve()
      },
      trigger: 'blur'
    }
  ],
  realName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.employee.realname') }),
      trigger: 'blur'
    }
  ],
  gender: [
    {
      validator: async (_rule, value) => {
        if (props.isEdit) return Promise.resolve()
        if (formState.isSystemEmployeeCode) return Promise.resolve()
        if (value !== 1 && value !== 2) {
          return Promise.reject(new Error(t('entity.employee.genderRequiredForD12')))
        }
        return Promise.resolve()
      },
      trigger: ['change', 'blur']
    }
  ]
}))

watch(
  () => [userStore.userInfo?.userType, props.isEdit] as const,
  () => {
    if (props.isEdit || !isAdmin.value) {
      formState.isSystemEmployeeCode = false
    }
  }
)

watch(
  () => formState.isSystemEmployeeCode,
  (sys) => {
    if (!props.isEdit && !sys && formState.gender === 0) {
      formState.gender = undefined
    }
  }
)

watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        employeeCode: newData.employeeCode ?? '',
        isSystemEmployeeCode: false,
        formerName: newData.formerName ?? '',
        realName: newData.realName ?? '',
        fullName: newData.fullName ?? '',
        nativeName: newData.nativeName ?? '',
        displayName: newData.displayName ?? '',
        gender: newData.gender ?? 0,
        birthDate: newData.birthDate ?? '',
        idCard: newData.idCard ?? '',
        phone: newData.phone ?? '',
        email: newData.email ?? '',
        nativePlace: newData.nativePlace ?? '',
        currentAddress: newData.currentAddress ?? '',
        employeeStatus: newData.employeeStatus ?? 0,
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, {
        employeeCode: '',
        isSystemEmployeeCode: false,
        formerName: '',
        realName: '',
        fullName: '',
        nativeName: '',
        displayName: '',
        gender: undefined,
        birthDate: '',
        idCard: '',
        phone: '',
        email: '',
        nativePlace: '',
        currentAddress: '',
        employeeStatus: 0,
        remark: ''
      })
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): EmployeeCreate => {
  const base: EmployeeCreate = {
    realName: formState.realName ?? '',
    formerName: formState.formerName || undefined,
    fullName: formState.fullName || undefined,
    nativeName: formState.nativeName || undefined,
    displayName: formState.displayName || undefined,
    gender: formState.gender ?? 0,
    birthDate: formState.birthDate || undefined,
    idCard: formState.idCard || undefined,
    phone: formState.phone || undefined,
    email: formState.email || undefined,
    nativePlace: formState.nativePlace || undefined,
    currentAddress: formState.currentAddress || undefined,
    employeeStatus: formState.employeeStatus ?? 0,
    remark: formState.remark || undefined
  }
  if (!props.isEdit) {
    base.isSystemEmployeeCode = formState.isSystemEmployeeCode ?? false
    base.employeeCode = ''
  } else {
    base.employeeCode = formState.employeeCode ?? ''
  }
  return base
}

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    employeeCode: '',
    isSystemEmployeeCode: false,
    formerName: '',
    realName: '',
    fullName: '',
    nativeName: '',
    displayName: '',
    gender: undefined,
    birthDate: '',
    idCard: '',
    phone: '',
    email: '',
    nativePlace: '',
    currentAddress: '',
    employeeStatus: 0,
    remark: ''
  })
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

defineExpose({ validate, getValues, resetFields, setServerValidationErrors })
</script>

<style scoped lang="less">
</style>
