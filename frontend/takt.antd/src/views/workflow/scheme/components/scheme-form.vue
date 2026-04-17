<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/scheme/components -->
<!-- 文件名称：scheme-form.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程方案新增/编辑表单组件，含步骤与 ProcessContent 设计 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <a-form
    ref="formRef"
    layout="vertical"
    :model="form"
    :rules="formRules"
  >
    <a-steps
      :current="currentStep"
      :items="stepItems"
      class="scheme-steps"
    />
    <div class="steps-content">
      <!-- 步骤1：流程信息（与 AntFlow BasicSetting 职责对齐，绑定 FlowSchemeCreateOrUpdate） -->
      <div
        v-show="currentStep === 0"
        class="step-content"
      >
        <TaktFlowBasicSetting v-model:form="form" />
      </div>
      <!-- 步骤2：单选 关联表单 / 新建表单，均用 TaktFormDesigner 展示与设计 -->
      <div
        v-show="currentStep === 1"
        class="step-content"
      >
        <a-form-item :label="t('workflow.scheme.linkForm')">
          <a-radio-group
            v-model:value="linkFormMode"
            class="scheme-form__form-radio"
          >
            <a-radio value="link">
              {{ t('workflow.scheme.linkFormOptionLink') }}
            </a-radio>
            <a-radio value="new">
              {{ t('workflow.scheme.linkFormOptionNew') }}
            </a-radio>
          </a-radio-group>
        </a-form-item>
        <!-- 关联表单：表单选择器 -->
        <a-form-item
          v-if="linkFormMode === 'link'"
          :label="t('workflow.scheme.linkForm')"
          name="formCode"
        >
          <a-spin :spinning="formListLoading">
            <template v-if="formList.length > 0">
              <a-select
                v-model:value="formSelectValue"
                style="width: 100%"
                :placeholder="t('workflow.scheme.selectFormPlaceholder')"
                allow-clear
                show-search
                :filter-option="filterFormOption"
                option-filter-prop="label"
                @change="onFormSelectChange"
              >
                <a-select-option
                  v-for="item in formList"
                  :key="item.formId ?? item.formCode"
                  :value="item.formCode ?? ''"
                  :label="(item.formCode ?? '') + ' ' + (item.formName ?? '')"
                >
                  {{ item.formCode }} - {{ item.formName }}
                </a-select-option>
              </a-select>
            </template>
            <div
              v-else-if="!formListLoading"
              class="scheme-form__no-form"
            >
              <span class="scheme-form__hint">{{ t('workflow.scheme.noFormHint') }}</span>
            </div>
          </a-spin>
        </a-form-item>
        <!-- 表单设计器：关联与新建均在此展示/设计 -->
        <div class="scheme-form__form-designer-section">
          <div class="scheme-form__form-designer-section__title">
            {{ linkFormMode === 'link' ? t('workflow.scheme.linkFormOptionLink') : t('workflow.scheme.linkFormOptionNew') }}
          </div>
          <div class="scheme-form__form-designer-section__body">
            <TaktFormDesigner
              :key="'form-designer-' + linkFormMode + '-' + (form.formId ?? formSelectValue ?? 'new')"
              v-model="formDesignConfig"
              height="480px"
              :designer-config="formDesignerConfig"
            />
          </div>
        </div>
      </div>
      <!-- 步骤3：流程设计 -->
      <div
        v-show="currentStep === 2"
        class="step-content step-content-design"
      >
        <div class="scheme-designer-section">
          <div class="scheme-designer-section__title">
            {{ isEdit ? t('workflow.scheme.designerLabelEdit') : t('workflow.scheme.designerLabel') }}
          </div>
          <div class="scheme-designer-section__body">
            <TaktFlowAntflowDesigner
              ref="flowDesignerRef"
              :key="'flow-antflow-designer-' + (form.schemeId ?? 'new')"
              v-model="form.processContent"
            />
          </div>
        </div>
      </div>
    </div>
    <div class="steps-action">
      <a-button
        v-if="currentStep > 0"
        style="margin-right: 8px"
        @click="prev"
      >
        {{ t('workflow.scheme.step.prev') }}
      </a-button>
      <a-button
        v-if="currentStep < steps.length - 1"
        type="primary"
        @click="next"
      >
        {{ t('workflow.scheme.step.next') }}
      </a-button>
      <a-button
        v-if="currentStep === steps.length - 1"
        type="primary"
        @click="handleDone"
      >
        {{ t('workflow.scheme.step.done') }}
      </a-button>
    </div>
  </a-form>
</template>

<script setup lang="ts">
/**
 * 流程方案表单：步骤 1 基本信息、步骤 2 选择表单、步骤 3 流程图设计、步骤 4 预览；对外暴露 validate、getFormData。
 */
import { ref, computed, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import TaktFlowAntflowDesigner from '@/components/business/takt-flow-antflow-designer/index.vue'
import TaktFlowBasicSetting from '@/components/business/takt-flow-antflow-designer/basic-setting/takt-flow-basic-setting.vue'
import TaktFormDesigner from '@/components/business/takt-form-designer/index.vue'
import { getFlowFormList, getFlowFormById } from '@/api/workflow/form'
import type { FlowSchemeCreateOrUpdate } from '@/types/workflow/scheme'
import type { FlowForm } from '@/types/workflow/form'

const { t } = useI18n()

/** 父组件传入的表单数据（含 schemeId 表示编辑） */
interface Props {
  form: FlowSchemeCreateOrUpdate & { schemeId?: string }
}

const props = defineProps<Props>()
const form = props.form

/** 是否编辑（有 schemeId 为编辑，反之为新增）；编辑时流程由 form.processContent 还原 */
const isEdit = computed(() => !!form.schemeId)
const currentStep = ref(0)
const formRef = ref()
const flowDesignerRef = ref<InstanceType<typeof TaktFlowAntflowDesigner> | null>(null)

/** 步骤配置：1 流程信息、2 关联表单、3 流程设计 */
const steps = computed(() => [
  { title: t('workflow.scheme.step.step1FlowInfo'), content: 0 },
  { title: t('workflow.scheme.step.step2SelectForm'), content: 1 },
  { title: t('workflow.scheme.step.step3FlowDesign'), content: 2 }
])
const stepItems = computed(() => steps.value.map(item => ({ key: item.title, title: item.title })))

/** 步骤2 单选：'link' 关联表单，'new' 新建表单 */
const linkFormMode = ref<'link' | 'new'>('link')
/** 步骤2 表单设计器绑定内容（关联=选中表单的 formConfig，新建=空） */
const formDesignConfig = ref<string>('')

/** 设计器 config（官方 props.config），按文档配置显隐：https://view.form-create.com/props */
const formDesignerConfig = {
  showSaveBtn: true,
  showPreviewBtn: true,
  showJsonPreview: true,
  showLanguage: true,
  showInputData: true
}

/** 选择表单：表单列表与当前选中（formCode 用于展示，提交时 form 含 formId/formCode） */
const formList = ref<FlowForm[]>([])
const formListLoading = ref(false)
const formSelectValue = ref<string | undefined>(form.formCode ?? undefined)

watch(
  () => form.formCode,
  (v) => { formSelectValue.value = v ?? undefined },
  { immediate: true }
)

/** 表单下拉的 filter-option：按 label 包含输入文本（不区分大小写） */
function filterFormOption(input: string, option?: { label?: string }) {
  const label = option?.label ?? ''
  return label.toLowerCase().includes((input ?? '').toLowerCase())
}

/** 关联表单选择变化：同步 form.formCode 与 form.formId，并拉取表单详情写入设计器 */
function onFormSelectChange(value: unknown) {
  const formCode = typeof value === 'string' ? value : undefined
  form.formCode = formCode ?? undefined
  const found = formCode ? formList.value.find(f => (f.formCode ?? '') === formCode) : undefined
  form.formId = found?.formId != null ? String(found.formId) : undefined
  if (linkFormMode.value === 'link') loadFormDetailIntoDesigner()
  else formDesignConfig.value = ''
}

/** 根据 form.formId 拉取表单详情并写入 formDesignConfig（仅关联表单模式） */
async function loadFormDetailIntoDesigner() {
  const id = form.formId
  if (!id || linkFormMode.value !== 'link') return
  try {
    const detail = await getFlowFormById(String(id))
    const raw = detail.formConfig
    formDesignConfig.value = typeof raw === 'string' && raw?.trim() ? raw : ''
  } catch {
    formDesignConfig.value = ''
  }
}

/** 拉取流程表单列表；有则默认选中已关联的，并加载详情到设计器 */
async function loadFormList() {
  formListLoading.value = true
  try {
    const res = await getFlowFormList({ pageIndex: 1, pageSize: 500 })
    const list = res.data ?? []
    formList.value = list
    if (list.length === 0) linkFormMode.value = 'new'
    else if (linkFormMode.value === 'link' && form.formId) await loadFormDetailIntoDesigner()
    else if (linkFormMode.value === 'new') formDesignConfig.value = ''
  } finally {
    formListLoading.value = false
  }
}

watch(linkFormMode, mode => {
  if (mode === 'new') formDesignConfig.value = ''
  else if (form.formId) loadFormDetailIntoDesigner()
  else formDesignConfig.value = ''
})

onMounted(() => loadFormList())

/** 当前步骤需要校验的字段名 */
const stepFieldNames: Record<number, string[]> = {
  0: ['processKey', 'processName'],
  1: [],
  2: []
}

const formRules = computed(() => ({
  processKey: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.flowscheme.processkey') }) }],
  processName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.flowscheme.processname') }) }]
}))

/** 校验当前步骤需校验的字段，通过返回 true */
async function validateCurrentStep(): Promise<boolean> {
  const fields = stepFieldNames[currentStep.value]
  if (!fields?.length) return true
  try {
    await formRef.value?.validateFields(fields)
    return true
  } catch {
    return false
  }
}

/** 下一步：校验通过则 currentStep + 1 */
async function next() {
  const ok = await validateCurrentStep()
  if (!ok) return
  currentStep.value++
}

/** 上一步：currentStep - 1 */
function prev() {
  currentStep.value--
}

/** 完成：校验当前步骤通过则提示成功（提交由父组件处理） */
async function handleDone() {
  const ok = await validateCurrentStep()
  if (!ok) return
  message.success(t('workflow.scheme.step.done'))
}

/** 校验所有步骤；未通过时切换到对应步骤并返回 false */
async function validateAllSteps(): Promise<boolean> {
  for (let i = 0; i < steps.value.length; i++) {
    const fields = stepFieldNames[i]
    if (fields?.length) {
      try {
        await formRef.value?.validateFields(fields)
      } catch {
        currentStep.value = i
        message.warning(t('workflow.scheme.step.validateFail', { step: i + 1 }))
        return false
      }
    }
  }
  const pc = form.processContent?.trim()
  if (pc && flowDesignerRef.value?.validateDesign) {
    const ok = flowDesignerRef.value.validateDesign({ silent: true })
    if (!ok) return false
  }
  return true
}

/** 重置当前步骤为 0（父组件关闭弹窗等时可调用） */
function resetSteps() {
  currentStep.value = 0
}

defineExpose({
  currentStep,
  validateAllSteps,
  resetSteps
})
</script>

<style scoped lang="less">
.scheme-steps {
  margin-bottom: 16px;
}
.steps-content {
  margin-top: 16px;
  min-height: 320px;
}
.step-content {
  margin-top: 0;
}
.step-content-design {
  min-height: 80vh;
}
.scheme-designer-section {
  margin-bottom: 16px;
  border: 1px solid var(--ant-color-border);
  border-radius: var(--ant-border-radius);
  overflow: hidden;
}
.scheme-designer-section__title {
  padding: 8px 12px;
  font-weight: 600;
  color: var(--ant-color-text-heading);
  background: var(--ant-color-fill-quaternary);
  border-bottom: 1px solid var(--ant-color-border);
}
/* 流程设计器所在区域：高度 80vh，使内部画布能撑满 */
.scheme-designer-section__body {
  padding: 12px;
  height: 80vh;
  min-height: 360px;
  background: var(--ant-color-bg-container);
  display: flex;
  flex-direction: column;
}
.scheme-designer-section__body .takt-flow-logic-designer {
  flex: 1;
  min-height: 0;
}
.scheme-form__form-radio {
  display: flex;
  gap: 16px;
}
.scheme-form__no-form {
  color: var(--ant-color-text-tertiary);
  font-size: 13px;
}
.scheme-form__hint {
  color: var(--ant-color-text-tertiary);
}
.scheme-form__form-designer-section {
  margin-top: 16px;
  margin-bottom: 16px;
  border: 1px solid var(--ant-color-border);
  border-radius: var(--ant-border-radius);
  overflow: hidden;
}
.scheme-form__form-designer-section__title {
  padding: 8px 12px;
  font-weight: 600;
  color: var(--ant-color-text-heading);
  background: var(--ant-color-fill-quaternary);
  border-bottom: 1px solid var(--ant-color-border);
}
.scheme-form__form-designer-section__body {
  padding: 12px;
  min-height: 520px;
  background: var(--ant-color-bg-container);
}
.steps-action {
  margin-top: 24px;
}
</style>

