<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/workflow/scheme/components -->
<!-- 文件名称：scheme-form.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程方案表单（流程建模），含 BPMN XML / ProcessJson 编辑 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    :model="formState"
    :label-col="{ span: 8 }"
    :wrapper-col="{ span: 16 }"
    @submit.prevent
  >
    <a-tabs v-model:activeKey="activeTab">
      <!-- Tab1：基本信息 -->
      <a-tab-pane :key="'basic'" :tab="t('workflow.scheme.tabBasicInfo')">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item
              :label="t('entity.flowscheme.processkey')"
              name="processKey"
              :rules="[{ required: true, message: t('common.form.placeholder.input', { field: t('entity.flowscheme.processkey') }) }]"
            >
              <a-input
                v-model:value="formState.processKey"
                :placeholder="t('workflow.scheme.processKeyPlaceholder')"
                :disabled="!!formData?.schemeId"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item
              :label="t('entity.flowscheme.processname')"
              name="processName"
              :rules="[{ required: true, message: t('common.form.placeholder.input', { field: t('entity.flowscheme.processname') }) }]"
            >
              <a-input v-model:value="formState.processName" :placeholder="t('entity.flowscheme.processname')" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.processcategory')" name="processCategory">
              <a-select
                v-model:value="formState.processCategory"
                :options="processCategoryOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.flowscheme.processcategory') })"
                allow-clear
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.processversion')" name="processVersion">
              <a-input-number v-model:value="formState.processVersion" :min="1" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="24">
            <a-form-item
              :label="t('entity.flowscheme.processdescription')"
              name="processDescription"
              :label-col="{ span: 4 }"
              :wrapper-col="{ span: 20 }"
            >
              <a-textarea
                v-model:value="formState.processDescription"
                :rows="2"
                :placeholder="t('entity.flowscheme.processdescription')"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.formcode')" name="formCode">
              <a-input v-model:value="formState.formCode" :placeholder="t('entity.flowscheme.formcode')" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.formid')" name="formId">
              <a-input v-model:value="formState.formId" :placeholder="t('entity.flowscheme.formcode')" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.processicon')" name="processIcon">
              <a-input v-model:value="formState.processIcon" :placeholder="t('entity.flowscheme.processicon')" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.processstatus')" name="processStatus">
              <a-select
                v-model:value="formState.processStatus"
                :options="processStatusOptions"
                :placeholder="t('common.form.placeholder.select', { field: t('entity.flowscheme.processstatus') })"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.ordernum')" name="orderNum">
              <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-tab-pane>
      <!-- Tab2：高级选项 -->
      <a-tab-pane :key="'advanced'" :tab="t('workflow.scheme.advancedOptions')">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.issuspendable')" name="isSuspendable">
              <a-select v-model:value="formState.isSuspendable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.isrevocable')" name="isRevocable">
              <a-select v-model:value="formState.isRevocable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.istransferable')" name="isTransferable">
              <a-select v-model:value="formState.isTransferable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.isaddsignable')" name="isAddsignable">
              <a-select v-model:value="formState.isAddsignable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.isreducesignable')" name="isReduceSignable">
              <a-select v-model:value="formState.isReduceSignable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.isreturnable')" name="isReturnable">
              <a-select v-model:value="formState.isReturnable" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.isautocomplete')" name="isAutoComplete">
              <a-select v-model:value="formState.isAutoComplete" :options="yesNoOptions" style="width: 100%" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.timeoutconfig')" name="timeoutConfig">
              <a-input v-model:value="formState.timeoutConfig" :placeholder="t('entity.flowscheme.timeoutconfig')" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('entity.flowscheme.notificationconfig')" name="notificationConfig">
              <a-input v-model:value="formState.notificationConfig" :placeholder="t('entity.flowscheme.notificationconfig')" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-tab-pane>
      <!-- Tab3：流程定义（按钮弹出设计器） -->
      <a-tab-pane :key="'flow'" :tab="t('workflow.scheme.tabFlowDefinition')">
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item
              :label="t('entity.flowscheme.bpmnxml')"
              name="bpmnXml"
              :label-col="{ span: 4 }"
              :wrapper-col="{ span: 20 }"
            >
              <div class="bpmn-xml-column">
                <a-textarea
                  v-model:value="formState.bpmnXml"
                  :rows="6"
                  :placeholder="t('workflow.scheme.bpmnXmlPlaceholder')"
                />
                <a-button type="primary" class="mt-2" @click="openBpmnDesigner">
                  {{ t('workflow.scheme.openFlowDesigner') }}
                </a-button>
              </div>
            </a-form-item>
          </a-col>
          <a-col :span="24">
            <a-form-item
              :label="t('entity.flowscheme.processjson')"
              name="processJson"
              :label-col="{ span: 4 }"
              :wrapper-col="{ span: 20 }"
            >
              <a-textarea
                v-model:value="formState.processJson"
                :rows="6"
                :placeholder="t('workflow.scheme.processJsonPlaceholder')"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-tab-pane>
      <!-- Tab4：预览流程（只读查看器） -->
      <a-tab-pane :key="'preview'" :tab="t('workflow.scheme.tabPreviewFlow')">
        <div class="scheme-form-embed-preview">
          <BpmnViewer :xml="formState.bpmnXml ?? ''" />
        </div>
      </a-tab-pane>
    </a-tabs>
    <a-modal
      v-model:open="bpmnDesignerVisible"
      :title="t('workflow.scheme.flowDesigner')"
      width="96%"
      :style="{ top: '24px' }"
      wrap-class-name="bpmn-designer-modal"
      :footer="null"
      destroy-on-close
      :get-container="getBpmnModalContainer"
      :body-style="bpmnModalBodyStyle"
      @cancel="bpmnDesignerVisible = false"
    >
      <div class="bpmn-designer-box">
        <TaktFlowDesign
          v-if="bpmnDesignerVisible"
          ref="bpmnModelerRef"
          :xml="formState.bpmnXml ?? ''"
          @loaded="onBpmnLoaded"
        />
      </div>
      <div class="bpmn-designer-footer">
        <a-button @click="bpmnDesignerVisible = false">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" @click="onBpmnDesignerOk">{{ t('workflow.scheme.confirm') }}</a-button>
      </div>
    </a-modal>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { FlowScheme, FlowSchemeCreate } from '@/types/workflow/scheme'
import TaktFlowDesign from '@/components/business/takt-flow-designer/index.vue'
import BpmnViewer from './bpmn-viewer.vue'

const { t } = useI18n()

const bpmnDesignerVisible = ref(false)
const bpmnModelerRef = ref<InstanceType<typeof TaktFlowDesign> | null>(null)

function getBpmnModalContainer(): HTMLElement {
  return document.body
}

const bpmnModalBodyStyle = {
  display: 'flex',
  flexDirection: 'column' as const,
  flex: 1,
  minHeight: 0,
  overflow: 'hidden',
  padding: '16px'
}

function openBpmnDesigner() {
  bpmnDesignerVisible.value = true
}

function onBpmnLoaded(xml: string) {
  formState.value.bpmnXml = xml
}

async function onBpmnDesignerOk() {
  const xml = await bpmnModelerRef.value?.getXml()
  if (xml != null) formState.value.bpmnXml = xml
  bpmnDesignerVisible.value = false
}

const props = defineProps<{
  formData: Partial<FlowScheme> | null
  loading?: boolean
}>()

const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const formState = ref<Partial<FlowSchemeCreate>>({
  processKey: '',
  processName: '',
  processCategory: 0,
  processVersion: 1,
  processDescription: '',
  formId: undefined,
  formCode: '',
  bpmnXml: '',
  processJson: '',
  processIcon: undefined,
  isSuspendable: 1,
  isRevocable: 1,
  isTransferable: 1,
  isAddsignable: 0,
  isReduceSignable: 0,
  isReturnable: 1,
  isAutoComplete: 0,
  timeoutConfig: undefined,
  notificationConfig: undefined,
  processStatus: 0,
  orderNum: 0
})

const processCategoryOptions = [
  { label: t('workflow.scheme.categoryGeneral'), value: 0 },
  { label: t('workflow.scheme.categoryBusiness'), value: 1 },
  { label: t('workflow.scheme.categorySystem'), value: 2 }
]
const processStatusOptions = [
  { label: t('workflow.scheme.statusDraft'), value: 0 },
  { label: t('workflow.scheme.statusDeployed'), value: 1 },
  { label: t('workflow.scheme.statusRetired'), value: 2 }
]
const yesNoOptions = [
  { label: t('common.no'), value: 0 },
  { label: t('common.yes'), value: 1 }
]

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        processKey: val.processKey ?? '',
        processName: val.processName ?? '',
        processCategory: val.processCategory ?? 0,
        processVersion: val.processVersion ?? 1,
        processDescription: val.processDescription ?? '',
        formId: val.formId ?? undefined,
        formCode: val.formCode ?? '',
        bpmnXml: val.bpmnXml ?? '',
        processJson: val.processJson ?? '',
        processIcon: val.processIcon ?? undefined,
        isSuspendable: val.isSuspendable ?? 1,
        isRevocable: val.isRevocable ?? 1,
        isTransferable: val.isTransferable ?? 1,
        isAddsignable: val.isAddsignable ?? 0,
        isReduceSignable: val.isReduceSignable ?? 0,
        isReturnable: val.isReturnable ?? 1,
        isAutoComplete: val.isAutoComplete ?? 0,
        timeoutConfig: val.timeoutConfig ?? undefined,
        notificationConfig: val.notificationConfig ?? undefined,
        processStatus: val.processStatus ?? 0,
        orderNum: val.orderNum ?? 0
      }
    } else {
      formState.value = {
        processKey: '',
        processName: '',
        processCategory: 0,
        processVersion: 1,
        processDescription: '',
        formId: undefined,
        formCode: '',
        bpmnXml: '',
        processJson: '',
        processIcon: undefined,
        isSuspendable: 1,
        isRevocable: 1,
        isTransferable: 1,
        isAddsignable: 0,
        isReduceSignable: 0,
        isReturnable: 1,
        isAutoComplete: 0,
        timeoutConfig: undefined,
        notificationConfig: undefined,
        processStatus: 0,
        orderNum: 0
      }
    }
  },
  { immediate: true }
)

async function validate(): Promise<void> {
  await formRef.value?.validate()
}

function getValues(): FlowSchemeCreate {
  const s = formState.value
  return {
    processKey: s?.processKey ?? '',
    processName: s?.processName ?? '',
    processCategory: s?.processCategory ?? 0,
    processVersion: s?.processVersion ?? 1,
    processDescription: s?.processDescription ?? '',
    formId: s?.formId,
    formCode: s?.formCode ?? '',
    bpmnXml: s?.bpmnXml ?? '',
    processJson: s?.processJson ?? '',
    processIcon: s?.processIcon,
    isSuspendable: s?.isSuspendable ?? 1,
    isRevocable: s?.isRevocable ?? 1,
    isTransferable: s?.isTransferable ?? 1,
    isAddsignable: s?.isAddsignable ?? 0,
    isReduceSignable: s?.isReduceSignable ?? 0,
    isReturnable: s?.isReturnable ?? 1,
    isAutoComplete: s?.isAutoComplete ?? 0,
    timeoutConfig: s?.timeoutConfig,
    notificationConfig: s?.notificationConfig,
    processStatus: s?.processStatus ?? 0,
    orderNum: s?.orderNum ?? 0
  }
}

defineExpose({ validate, getValues })
</script>

<style scoped>
.bpmn-xml-column {
  width: 100%;
}
.bpmn-xml-column .ant-input {
  width: 100%;
}
.mt-2 { margin-top: 8px; }
.flex { display: flex; }
.justify-end { justify-content: flex-end; }
.gap-2 { gap: 8px; }
.bpmn-designer-box {
  flex: 1;
  min-height: 0;
  width: 100%;
  display: flex;
  flex-direction: column;
}
.bpmn-designer-box > * {
  flex: 1;
  min-height: 0;
}
.bpmn-designer-footer {
  flex-shrink: 0;
  margin-top: 16px;
  display: flex;
  justify-content: flex-end;
  gap: 8px;
}
.scheme-form-embed-preview {
  min-height: 420px;
  height: 55vh;
  width: 100%;
}
</style>
<style>
.bpmn-designer-modal .ant-modal {
  max-width: 96%;
  padding-bottom: 0;
}
.bpmn-designer-modal .ant-modal-content {
  height: calc(100vh - 48px);
  max-height: calc(100vh - 48px);
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
.bpmn-designer-modal .ant-modal-body {
  flex: 1;
  min-height: 0;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
</style>