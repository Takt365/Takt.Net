<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/form/components -->
<!-- 文件名称：form-form.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程表单编辑表单组件，Tabs 布局：基本信息 + 表单设计（FcDesigner） -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <a-tabs v-model:activeKey="activeTab">
    <a-tab-pane :key="'basic'" :tab="t('workflow.form.tabBasicInfo')">
      <a-form
        ref="formRef"
        :model="formState"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
        @submit.prevent
      >
        <a-form-item
          :label="t('entity.flowform.formcode')"
          name="formCode"
          :rules="[{ required: true, message: t('common.form.placeholder.input', { field: t('entity.flowform.formcode') }) }]"
        >
          <a-input
            v-model:value="formState.formCode"
            :disabled="!!formData?.formId"
            :placeholder="t('entity.flowform.formcode')"
          />
        </a-form-item>
        <a-form-item
          :label="t('entity.flowform.formname')"
          name="formName"
          :rules="[{ required: true, message: t('common.form.placeholder.input', { field: t('entity.flowform.formname') }) }]"
        >
          <a-input v-model:value="formState.formName" :placeholder="t('entity.flowform.formname')" />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.formcategory')" name="formCategory">
          <a-select
            v-model:value="formState.formCategory"
            :options="formCategoryOptions"
            :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formcategory') })"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.formtype')" name="formType">
          <a-select
            v-model:value="formState.formType"
            :options="formTypeOptions"
            :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formtype') })"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.formstatus')" name="formStatus">
          <a-select
            v-model:value="formState.formStatus"
            :options="formStatusOptions"
            :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formstatus') })"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.ordernum')" name="orderNum">
          <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.formversion')" name="formVersion">
          <a-input v-model:value="formState.formVersion" placeholder="1.0.0" />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.datasource')" name="dataSource">
          <a-input v-model:value="formState.dataSource" :placeholder="t('entity.flowform.datasource')" />
        </a-form-item>
        <a-form-item :label="t('entity.flowform.deptid')" name="deptId">
          <a-input v-model:value="formState.deptId" placeholder="0" />
        </a-form-item>
      </a-form>
    </a-tab-pane>
    <a-tab-pane :key="'design'" :tab="t('workflow.form.tabFormDesign')">
      <div class="form-design-tab">
        <p class="form-design-tip">{{ t('workflow.form.formTemplateDesign') }}</p>
        <div class="form-design-row">
          <a-input
            :value="formState.formTemplate"
            :placeholder="t('workflow.form.openFormDesigner')"
            readonly
            class="form-template-input"
          />
          <a-button type="primary" class="ml-2" @click="openFormDesigner">
            {{ t('workflow.form.openFormDesigner') }}
          </a-button>
        </div>
      </div>
    </a-tab-pane>
  </a-tabs>
  <a-modal
    v-model:open="formDesignerVisible"
    :title="t('workflow.form.formDesigner')"
    width="96%"
    :style="{ top: '24px' }"
    wrap-class-name="form-designer-modal"
    :footer="null"
    destroy-on-close
    :get-container="getFormDesignerContainer"
    @cancel="formDesignerVisible = false"
  >
    <div class="form-designer-box">
      <TaktFormDesign
        v-if="formDesignerVisible"
        ref="formDesignerRef"
        :rule-json="formDesignerRuleJson"
        :option-json="formDesignerOptionJson"
        height="70vh"
      />
    </div>
    <div class="mt-4 flex justify-end gap-2">
      <a-button @click="formDesignerVisible = false">{{ t('common.button.cancel') }}</a-button>
      <a-button type="primary" @click="onFormDesignerOk">{{ t('workflow.scheme.confirm') }}</a-button>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { FlowForm, FlowFormCreate } from '@/types/workflow/form'
import TaktFormDesign from '@/components/business/takt-form-designer/index.vue'

const { t } = useI18n()

const props = defineProps<{
  formData: Partial<FlowForm> | null
  loading?: boolean
}>()

const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const formState = ref<Partial<FlowFormCreate>>({
  formCode: '',
  formName: '',
  formCategory: 0,
  formType: 0,
  formStatus: 0,
  orderNum: 0,
  formTemplate: undefined,
  formVersion: '1.0.0',
  dataSource: undefined,
  deptId: '0',
  fields: undefined,
  contentParse: undefined
})

const formCategoryOptions = [
  { label: t('workflow.form.categoryGeneral'), value: 0 },
  { label: t('workflow.form.categoryBusiness'), value: 1 },
  { label: t('workflow.form.categorySystem'), value: 2 }
]
const formTypeOptions = [
  { label: t('workflow.form.formTypeDynamic'), value: 0 },
  { label: t('workflow.form.formTypeStatic'), value: 1 }
]
const formStatusOptions = [
  { label: t('workflow.form.statusDraft'), value: 0 },
  { label: t('workflow.form.statusDeployed'), value: 1 },
  { label: t('workflow.form.statusRetired'), value: 2 }
]

const formDesignerVisible = ref(false)
const formDesignerRef = ref<InstanceType<typeof TaktFormDesign> | null>(null)
const formDesignerRuleJson = ref('')
const formDesignerOptionJson = ref('')

function getFormDesignerContainer(): HTMLElement | false {
  if (typeof document === 'undefined') return false
  return document.body
}

function openFormDesigner() {
  try {
    const raw = formState.value.formTemplate?.trim()
    if (raw) {
      const obj = JSON.parse(raw) as { rule?: string; option?: string }
      formDesignerRuleJson.value = typeof obj.rule === 'string' ? obj.rule : '[]'
      formDesignerOptionJson.value = typeof obj.option === 'string' ? obj.option : '{}'
    } else {
      formDesignerRuleJson.value = '[]'
      formDesignerOptionJson.value = '{}'
    }
  } catch {
    formDesignerRuleJson.value = '[]'
    formDesignerOptionJson.value = '{}'
  }
  formDesignerVisible.value = true
}

function onFormDesignerOk() {
  const rule = formDesignerRef.value?.getJson() ?? '[]'
  const option = formDesignerRef.value?.getOptionsJson() ?? '{}'
  formState.value.formTemplate = JSON.stringify({ rule, option })
  formDesignerVisible.value = false
}

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        formCode: val.formCode ?? '',
        formName: val.formName ?? '',
        formCategory: val.formCategory ?? 0,
        formType: val.formType ?? 0,
        formStatus: val.formStatus ?? 0,
        orderNum: val.orderNum ?? 0,
        formTemplate: val.formTemplate ?? undefined,
        formVersion: val.formVersion ?? '1.0.0',
        dataSource: val.dataSource ?? undefined,
        deptId: val.deptId ?? '0',
        fields: val.fields ?? undefined,
        contentParse: val.contentParse ?? undefined
      }
    } else {
      formState.value = {
        formCode: '',
        formName: '',
        formCategory: 0,
        formType: 0,
        formStatus: 0,
        orderNum: 0,
        formTemplate: undefined,
        formVersion: '1.0.0',
        dataSource: undefined,
        deptId: '0',
        fields: undefined,
        contentParse: undefined
      }
    }
  },
  { immediate: true }
)

async function validate(): Promise<void> {
  await formRef.value?.validate()
}

function getValues(): FlowFormCreate {
  const s = formState.value
  return {
    formCode: s?.formCode ?? '',
    formName: s?.formName ?? '',
    formCategory: s?.formCategory ?? 0,
    formType: s?.formType ?? 0,
    formStatus: s?.formStatus ?? 0,
    orderNum: s?.orderNum ?? 0,
    formTemplate: s?.formTemplate ?? undefined,
    formVersion: s?.formVersion ?? '1.0.0',
    dataSource: s?.dataSource ?? undefined,
    deptId: s?.deptId ?? '0',
    fields: s?.fields ?? undefined,
    contentParse: s?.contentParse ?? undefined
  }
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
.form-design-tab {
  padding: 8px 0;
}
.form-design-tip {
  margin-bottom: 12px;
  color: #666;
  font-size: 13px;
}
.form-design-row {
  display: flex;
  align-items: flex-start;
  width: 100%;
}
.form-design-row :deep(.ant-input) {
  flex: 1;
}
.form-template-input {
  font-family: monospace;
  max-height: 80px;
  overflow: auto;
}
.ml-2 {
  margin-left: 8px;
}
.mt-4 {
  margin-top: 16px;
}
.flex {
  display: flex;
}
.justify-end {
  justify-content: flex-end;
}
.gap-2 {
  gap: 8px;
}
.form-designer-box {
  height: 70vh;
  min-height: 400px;
  width: 100%;
}
</style>
