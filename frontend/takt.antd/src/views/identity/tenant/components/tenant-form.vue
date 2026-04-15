<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/identity/tenant/components -->
<!-- 文件名称：tenant-form.vue -->
<!-- 创建时间：2025-02-27 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：租户维护弹窗内嵌表单。由 tenant/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="tenant-form-content">
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
          <a-form-item :label="t('entity.tenant.tenantname')" name="tenantName">
            <a-input
              v-model:value="formState.tenantName"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.tenantname') })"
              show-count
              :maxlength="50"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.tenant.tenantcode')" name="tenantCode">
            <a-input
              v-model:value="formState.tenantCode"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.tenantcode') })"
              :disabled="!!formData?.tenantId"
              show-count
              :maxlength="30"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.tenant.configid')" name="configId">
            <a-input
              v-model:value="formState.configId"
              :placeholder="t('common.form.placeholder.required', { field: t('entity.tenant.configid') })"
              show-count
              :maxlength="20"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('entity.tenant.starttime')" name="startTime">
            <a-date-picker
              v-model:value="formState.startTimeMoment"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.tenant.starttime') })"
              style="width: 100%"
              value-format="YYYY-MM-DD"
              format="YYYY-MM-DD"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('entity.tenant.endtime')" name="endTime">
            <a-date-picker
              v-model:value="formState.endTimeMoment"
              :placeholder="t('common.form.placeholder.select', { field: t('entity.tenant.endtime') })"
              style="width: 100%"
              value-format="YYYY-MM-DD"
              format="YYYY-MM-DD"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
            <a-textarea
              v-model:value="formState.remark"
              :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
              :rows="4"
              show-count
              :maxlength="500"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
/**
 * 租户表单脚本：与 `tenant/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { reactive, watch, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { Tenant } from '@/types/identity/tenant'
import dayjs, { type Dayjs } from 'dayjs'

const { t } = useI18n()

interface Props {
  formData?: Partial<Tenant>
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

// 表单实例引用（用于触发 validate/reset）
const formRef = ref()

interface FormState {
  tenantName?: string
  tenantCode?: string
  configId?: string
  startTime?: string
  endTime?: string
  startTimeMoment?: Dayjs
  endTimeMoment?: Dayjs
  remark?: string
}

// 本地表单状态（日期控件使用 Dayjs，中间态字段为 startTimeMoment/endTimeMoment）
const formState = reactive<FormState>({
  tenantName: '',
  tenantCode: '',
  configId: '0',
  startTime: '',
  endTime: '',
  startTimeMoment: undefined,
  endTimeMoment: undefined,
  remark: ''
})

// 表单校验规则（与后端租户 DTO 约束保持一致）
const rules: Record<string, Rule[]> = {
  tenantName: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.tenant.tenantname') }), trigger: 'blur' },
    { min: 2, max: 50, message: t('tenant.fields.tenantName.validation.format'), trigger: 'blur' }
  ],
  tenantCode: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.tenant.tenantcode') }), trigger: 'blur' },
    { min: 2, max: 30, message: t('tenant.fields.tenantCode.validation.format'), trigger: 'blur' }
  ],
  configId: [
    { required: true, message: t('common.form.placeholder.required', { field: t('entity.tenant.configid') }), trigger: 'blur' }
  ]
}

// 外部 formData 变更时，回填到表单状态（支持编辑与重置）
watch(() => props.formData, (newData) => {
  if (newData && Object.keys(newData).length > 0) {
    Object.assign(formState, {
      tenantName: newData.tenantName || '',
      tenantCode: newData.tenantCode || '',
      configId: newData.configId ?? '0',
      startTime: newData.startTime || '',
      endTime: newData.endTime || '',
      startTimeMoment: newData.startTime ? dayjs(newData.startTime) : undefined,
      endTimeMoment: newData.endTime ? dayjs(newData.endTime) : undefined,
      remark: newData.remark || ''
    })
  } else {
    Object.assign(formState, {
      tenantName: '',
      tenantCode: '',
      configId: '0',
      startTime: '',
      endTime: '',
      startTimeMoment: undefined,
      endTimeMoment: undefined,
      remark: ''
    })
  }
}, { immediate: true, deep: true })

// 提供给父组件：执行表单校验
const validate = async () => {
  await formRef.value?.validate()
}

// 提供给父组件：获取提交值（Dayjs -> yyyy-MM-dd 字符串）
const getValues = () => {
  return {
    tenantName: formState.tenantName || '',
    tenantCode: formState.tenantCode || '',
    configId: formState.configId || '0',
    startTime: formState.startTimeMoment ? formState.startTimeMoment.format('YYYY-MM-DD') : undefined,
    endTime: formState.endTimeMoment ? formState.endTimeMoment.format('YYYY-MM-DD') : undefined,
    remark: formState.remark || ''
  }
}

// 提供给父组件：重置校验与表单状态
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    tenantName: '',
    tenantCode: '',
    configId: '0',
    startTime: '',
    endTime: '',
    startTimeMoment: undefined,
    endTimeMoment: undefined,
    remark: ''
  })
}

defineExpose({
  validate,
  getValues,
  resetFields
})
</script>

<style scoped lang="less">
/* 表单容器最小高度，避免弹窗切换时抖动 */
.tenant-form-content {
  min-height: 200px;
}
</style>
