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
              <a-form-item :label="t('entity.role.name')" name="roleName">
                <a-input v-model:value="formState.roleName" :placeholder="t('common.form.placeholder.required', { field: t('entity.role.name') })" show-count :maxlength="50" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.role.code')" name="roleCode">
                <a-input v-model:value="formState.roleCode" :placeholder="t('common.form.placeholder.required', { field: t('entity.role.code') })" show-count :maxlength="50" :disabled="!!formData?.roleId" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.role.ordernum')" name="orderNum">
                <a-input-number v-model:value="formState.orderNum" :placeholder="t('common.form.placeholder.orderNumHint')" :min="0" style="width: 100%" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.role.datascope')" name="dataScope" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-select v-model:value="formState.dataScope" :placeholder="t('common.form.placeholder.select', { field: t('entity.role.datascope') })" :options="dataScopeOptions" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('entity.role.customscope')" name="customScope" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-input v-model:value="formState.customScope" :placeholder="t('identity.role.placeholder.customScopeHint')" show-count :maxlength="500" />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-textarea v-model:value="formState.remark" :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })" :rows="3" show-count :maxlength="500" />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </div>
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Role, RoleCreate } from '@/types/identity/role'

const { t } = useI18n()

const dataScopeOptions = computed(() => [
  { label: t('identity.role.dataScope.all'), value: 0 },
  { label: t('identity.role.dataScope.dept'), value: 1 },
  { label: t('identity.role.dataScope.deptAndBelow'), value: 2 },
  { label: t('identity.role.dataScope.self'), value: 3 },
  { label: t('identity.role.dataScope.custom'), value: 4 }
])

interface Props {
  formData?: Partial<Role>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const activeTab = ref('basic')
/** 表单总字段数（用于内容区高度：>=30 为 10 行，<30 为 5 行） */
const TOTAL_FIELDS = 6
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  roleName?: string
  roleCode?: string
  orderNum?: number
  dataScope?: number
  customScope?: string
  remark?: string
}

const formState = reactive<FormState>({
  roleName: '',
  roleCode: '',
  orderNum: 0,
  dataScope: 0,
  customScope: '',
  remark: ''
})

const rules = computed<Record<string, Rule[]>>(() => ({
  roleName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.role.name') }), trigger: 'blur' }],
  roleCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.role.code') }), trigger: 'blur' }],
  dataScope: [{ required: true, message: t('common.form.placeholder.select', { field: t('entity.role.datascope') }), trigger: 'change' }]
}))

watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      roleName: newData.roleName ?? '',
      roleCode: newData.roleCode ?? '',
      orderNum: newData.orderNum ?? 0,
      dataScope: newData.dataScope ?? 0,
      customScope: newData.customScope ?? '',
      remark: newData.remark ?? ''
    })
  } else {
    Object.assign(formState, {
      roleName: '',
      roleCode: '',
      orderNum: 0,
      dataScope: 0,
      customScope: '',
      remark: ''
    })
  }
  activeTab.value = 'basic'
}, { immediate: true, deep: true })

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): RoleCreate => ({
  roleName: formState.roleName ?? '',
  roleCode: formState.roleCode ?? '',
  orderNum: formState.orderNum ?? 0,
  dataScope: formState.dataScope ?? 0,
  customScope: formState.customScope || undefined,
  remark: formState.remark || undefined
})

const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    roleName: '',
    roleCode: '',
    orderNum: 0,
    dataScope: 0,
    customScope: '',
    remark: ''
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
