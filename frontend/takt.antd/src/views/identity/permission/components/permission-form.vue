<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/permission/components -->
<!-- 文件名称：permission-form.vue -->
<!-- 功能描述：权限表单组件，包含基本信息 -->
<!-- ======================================== -->

<template>
  <div :class="formContentClass">
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 8 }"
      :wrapper-col="{ span: 16 }"
      layout="horizontal"
      label-align="right"
    >
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.permission.code')" name="permissionCode">
            <a-input
              v-model:value="formState.permissionCode"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.permission.code') })"
              show-count
              :maxlength="100"
              :disabled="!!formData?.permissionId"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.permission.name')" name="permissionName">
            <a-input
              v-model:value="formState.permissionName"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.permission.name') })"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.permission.module')" name="module">
            <a-input v-model:value="formState.module" placeholder="如 identity、routine" :maxlength="50" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.permission.ordernum')" name="orderNum">
            <a-input-number
              v-model:value="formState.orderNum"
              :placeholder="t('common.form.placeholder.orderNumHint')"
              :min="0"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24" v-if="formData?.permissionId">
        <a-col :span="12">
          <a-form-item :label="t('entity.permission.status')" name="permissionStatus">
            <a-select
              v-model:value="formState.permissionStatus"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.permission.status') })"
              :options="statusOptions"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Permission, PermissionCreate } from '@/types/identity/permission'

const { t } = useI18n()

const statusOptions = computed(() => [
  { label: t('common.button.enable'), value: 0 },
  { label: t('common.button.disable'), value: 1 }
])

interface Props {
  formData?: Partial<Permission>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

const formRef = ref()
const TOTAL_FIELDS = 5
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

interface FormState {
  permissionCode?: string
  permissionName?: string
  module?: string
  menuId?: string
  orderNum?: number
  permissionStatus?: number
}

const formState = reactive<FormState>({
  permissionCode: '',
  permissionName: '',
  module: '',
  menuId: undefined,
  orderNum: 0,
  permissionStatus: 0
})

const rules = computed<Record<string, Rule[]>>(() => ({
  permissionCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.permission.code') }), trigger: 'blur' }
  ]
}))

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.permissionCode = val.permissionCode ?? ''
      formState.permissionName = val.permissionName ?? ''
      formState.module = val.module ?? ''
      formState.menuId = val.menuId ? Number(val.menuId) : undefined
      formState.orderNum = val.orderNum ?? 0
      formState.permissionStatus = val.permissionStatus ?? 0
    } else {
      formState.permissionCode = ''
      formState.permissionName = ''
      formState.module = ''
      formState.menuId = undefined
      formState.orderNum = 0
      formState.permissionStatus = 0
    }
  },
  { immediate: true }
)

function getValues(): PermissionCreate & { permissionStatus?: number } {
  return {
    permissionCode: formState.permissionCode ?? '',
    permissionName: formState.permissionName || undefined,
    module: formState.module || undefined,
    menuId: formState.menuId != null ? Number(formState.menuId) : undefined,
    orderNum: formState.orderNum ?? 0,
    permissionStatus: formState.permissionStatus
  }
}

function validate(): Promise<void> {
  return formRef.value?.validate()
}

function resetFields(): void {
  formRef.value?.resetFields()
}

defineExpose({
  getValues,
  validate,
  resetFields
})
</script>

<style scoped lang="less">
.takt-form-content-rows-5,
.takt-form-content-rows-10 {
  min-height: 120px;
}
</style>
