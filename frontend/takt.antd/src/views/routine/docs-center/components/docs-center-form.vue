<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/docs-center/components -->
<!-- 文件名称：docs-center-form.vue -->
<!-- 功能描述：文控中心文档表单组件 -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane key="basic" :tab="t('common.form.tabs.basicInfo')">
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
              <a-form-item :label="t('entity.document.documentcode')" name="documentCode">
                <a-input
                  v-model:value="formState.documentCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.document.documentcode') })"
                  :disabled="!!formData?.documentId"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.document.documenttitle')" name="documentTitle">
                <a-input
                  v-model:value="formState.documentTitle"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.document.documenttitle') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.document.documenttype')" name="documentType">
                <TaktSelect
                  v-model:value="formState.documentType"
                  dict-type="sys_document_type"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.document.documenttype') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.document.documentversion')" name="documentVersion">
                <a-input
                  v-model:value="formState.documentVersion"
                  :placeholder="t('common.form.placeholder.optional')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.document.applicantname')" name="applicantName">
                <a-input
                  v-model:value="formState.applicantName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.document.applicantname') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.document.applicantdeptname')" name="applicantDeptName">
                <a-input
                  v-model:value="formState.applicantDeptName"
                  :placeholder="t('common.form.placeholder.optional')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.document.direction')" name="direction">
                <TaktSelect
                  v-model:value="formState.direction"
                  dict-type="sys_document_direction"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.document.direction') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.document.documentcategory')" name="documentCategory">
                <TaktSelect
                  v-model:value="formState.documentCategory"
                  dict-type="sys_document_category"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.document.documentcategory') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.document.lifecyclestage')" name="lifecycleStage">
                <TaktSelect
                  v-model:value="formState.lifecycleStage"
                  dict-type="sys_document_lifecycle_stage"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.document.lifecyclestage') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.document.retentionyears')" name="retentionYears">
                <a-input-number
                  v-model:value="formState.retentionYears"
                  :min="0"
                  style="width: 100%"
                  :placeholder="t('common.form.placeholder.optional')"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item :label="t('common.entity.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('common.form.placeholder.optional')"
                  :rows="3"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </div>
    </a-tab-pane>
  </a-tabs>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Document, DocumentCreate, DocumentUpdate } from '@/types/routine/document'

const props = withDefaults(
  defineProps<{
    formData?: Partial<Document> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const { t } = useI18n()
const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const TOTAL_FIELDS = 10
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

const formState = ref<DocumentCreate & { documentId?: string; applicantId?: string; applicantDeptId?: string; fileId?: string }>({
  documentCode: '',
  documentTitle: '',
  documentType: 0,
  documentVersion: undefined,
  applicantId: '',
  applicantName: '',
  applicantDeptId: undefined,
  applicantDeptName: undefined,
  fileId: undefined,
  direction: 0,
  documentCategory: 0,
  lifecycleStage: 0,
  retentionYears: undefined,
  remark: undefined
})

const rules = computed<Record<string, Rule[]>>(() => ({
  documentCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.document.documentcode') }), trigger: 'blur' }],
  documentTitle: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.document.documenttitle') }), trigger: 'blur' }],
  applicantName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.document.applicantname') }), trigger: 'blur' }]
}))

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        documentCode: val.documentCode ?? '',
        documentTitle: val.documentTitle ?? '',
        documentType: val.documentType ?? 0,
        documentVersion: val.documentVersion ?? undefined,
        applicantId: val.applicantId ?? '',
        applicantName: val.applicantName ?? '',
        applicantDeptId: val.applicantDeptId ?? undefined,
        applicantDeptName: val.applicantDeptName ?? undefined,
        fileId: val.fileId ?? undefined,
        direction: val.direction ?? 0,
        documentCategory: val.documentCategory ?? 0,
        lifecycleStage: val.lifecycleStage ?? 0,
        retentionYears: val.retentionYears ?? undefined,
        remark: val.remark ?? undefined
      }
      if (val.documentId) formState.value.documentId = val.documentId
    } else {
      formState.value = {
        documentCode: '',
        documentTitle: '',
        documentType: 0,
        documentVersion: undefined,
        applicantId: '',
        applicantName: '',
        applicantDeptId: undefined,
        applicantDeptName: undefined,
        fileId: undefined,
        direction: 0,
        documentCategory: 0,
        lifecycleStage: 0,
        retentionYears: undefined,
        remark: undefined
      }
    }
  },
  { immediate: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): DocumentCreate | DocumentUpdate => {
  const base: DocumentCreate = {
    documentCode: formState.value.documentCode,
    documentTitle: formState.value.documentTitle,
    documentType: formState.value.documentType ?? 0,
    documentVersion: formState.value.documentVersion || undefined,
    applicantId: formState.value.applicantId ?? '',
    applicantName: formState.value.applicantName ?? '',
    applicantDeptId: formState.value.applicantDeptId ?? undefined,
    applicantDeptName: formState.value.applicantDeptName || undefined,
    fileId: formState.value.fileId ?? undefined,
    direction: formState.value.direction ?? 0,
    documentCategory: formState.value.documentCategory ?? 0,
    lifecycleStage: formState.value.lifecycleStage ?? 0,
    retentionYears: formState.value.retentionYears ?? undefined,
    remark: formState.value.remark || undefined
  }
  if (formState.value.documentId) {
    return { ...base, documentId: formState.value.documentId } as DocumentUpdate
  }
  return base
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
// 与项目其他表单一致
</style>
