<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/personnel/employee-attachment/components -->
<!-- 文件名称：attachment-form.vue -->
<!-- 功能描述：员工附件维护弹窗内嵌表单。由 employee-attachment/index.vue 引用；defineExpose 提供 validate、getValues、resetFields。表单模型复用 `@/types/human-resource/personnel/employee-attachment` 中 EmployeeAttachmentCreate。 -->
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
              :label="t('entity.employeeattachment.employeeId')"
              name="employeeId"
            >
              <a-input
                v-model:value="formState.employeeId"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.employeeattachment.employeeId') })"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.fileId')"
              name="fileId"
            >
              <a-input
                v-model:value="formState.fileId"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.fileCode')"
              name="fileCode"
            >
              <a-input
                v-model:value="formState.fileCode"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.fileName')"
              name="fileName"
            >
              <a-input
                v-model:value="formState.fileName"
                :placeholder="t('common.form.placeholder.required', { field: t('entity.employeeattachment.fileName') })"
                allow-clear
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.filePath')"
              name="filePath"
            >
              <a-input
                v-model:value="formState.filePath"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.fileSize')"
              name="fileSize"
            >
              <a-input-number
                v-model:value="formState.fileSize"
                :min="0"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.attachmentType')"
              name="attachmentType"
            >
              <a-select
                v-model:value="formState.attachmentType"
                :options="attachmentTypeOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.employeeattachment.attachmentType') })"
                allow-clear
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.employeeattachment.orderNum')"
              name="orderNum"
            >
              <a-input-number
                v-model:value="formState.orderNum"
                :min="0"
                style="width: 100%"
              />
            </a-form-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-item
              :label="t('entity.employeeattachment.attachmentDescription')"
              name="attachmentDescription"
              :label-col="{ span: 4 }"
              :wrapper-col="{ span: 20 }"
            >
              <a-textarea
                v-model:value="formState.attachmentDescription"
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
 * 员工附件表单脚本：与 `employee-attachment/index.vue` 中表单 ref 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成弹窗提交流程。
 */
import { computed, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import type { EmployeeAttachment, EmployeeAttachmentCreate } from '@/types/human-resource/personnel/employee-attachment'

const { t } = useI18n()
const entityKey = 'entity.employeeattachment'

interface Props {
  /** 当前编辑行数据，新增时为空对象 */
  formData?: Partial<EmployeeAttachment>
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

/** 附件类型选项 */
const attachmentTypeOptions = computed(() =>
  [0, 1, 2, 3, 4, 5].map((v) => ({ label: t(`${entityKey}.attachmentType${v}`), value: v }))
)

interface FormState {
  employeeId?: string
  fileId?: string
  fileCode?: string
  fileName?: string
  filePath?: string
  fileSize?: number
  fileType?: string
  attachmentType?: number
  attachmentDescription?: string
  orderNum?: number
}

/** 表单模型（对应 EmployeeAttachmentCreate） */
const formState = reactive<FormState>({
  employeeId: '',
  fileId: '',
  fileCode: '',
  fileName: '',
  filePath: '',
  fileSize: 0,
  fileType: '',
  attachmentType: 0,
  attachmentDescription: '',
  orderNum: 0
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
  fileName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t(`${entityKey}.fileName`) }),
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
        fileId: data.fileId ?? '',
        fileCode: data.fileCode ?? '',
        fileName: data.fileName ?? '',
        filePath: data.filePath ?? '',
        fileSize: data.fileSize ?? 0,
        fileType: data.fileType ?? '',
        attachmentType: data.attachmentType ?? 0,
        attachmentDescription: data.attachmentDescription ?? '',
        orderNum: data.orderNum ?? 0
      })
    } else {
      Object.assign(formState, {
        employeeId: '',
        fileId: '',
        fileCode: '',
        fileName: '',
        filePath: '',
        fileSize: 0,
        fileType: '',
        attachmentType: 0,
        attachmentDescription: '',
        orderNum: 0
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
const getValues = (): EmployeeAttachmentCreate => ({
  employeeId: formState.employeeId ?? '',
  fileId: formState.fileId ?? '',
  fileCode: formState.fileCode ?? '',
  fileName: formState.fileName ?? '',
  filePath: formState.filePath ?? '',
  fileSize: formState.fileSize ?? 0,
  fileType: formState.fileType || undefined,
  attachmentType: formState.attachmentType ?? 0,
  attachmentDescription: formState.attachmentDescription || undefined,
  orderNum: formState.orderNum ?? 0
})

/** 重置表单 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    employeeId: '',
    fileId: '',
    fileCode: '',
    fileName: '',
    filePath: '',
    fileSize: 0,
    fileType: '',
    attachmentType: 0,
    attachmentDescription: '',
    orderNum: 0
  })
  activeTab.value = 'basic'
}

defineExpose({ validate, getValues, resetFields })
</script>
