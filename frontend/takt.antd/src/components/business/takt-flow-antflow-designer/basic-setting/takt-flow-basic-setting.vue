<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/components/business/takt-flow-antflow-designer/basic-setting -->
<!-- 文件名称：takt-flow-basic-setting.vue -->
<!-- 创建时间：2026-04-07 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程方案「基础信息」表单片段（与 AntFlow BasicSetting 职责对齐，绑定 FlowSchemeCreateOrUpdate） -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->
<template>
  <a-row :gutter="16">
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.processkey')"
        name="processKey"
        required
      >
        <a-input
          v-model:value="processKeyModel"
          :placeholder="t('common.page.form.placeholder.required', { field: t('entity.flowscheme.processkey') })"
          :disabled="!!props.form.schemeId"
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.processname')"
        name="processName"
        required
      >
        <a-input
          v-model:value="processNameModel"
          :placeholder="t('common.page.form.placeholder.required', { field: t('entity.flowscheme.processname') })"
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.processcategory')"
        name="processCategory"
      >
        <a-input-number
          v-model:value="processCategoryModel"
          :min="0"
          style="width: 100%"
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.processdescription')"
        name="processDescription"
      >
        <a-input
          v-model:value="processDescriptionModel"
          placeholder=""
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.processstatus')"
        name="processStatus"
      >
        <a-select
          v-model:value="processStatusModel"
          style="width: 100%"
          :placeholder="t('common.page.form.placeholder.select', { field: t('entity.flowscheme.processstatus') })"
        >
          <a-select-option :value="0">
            {{ t('common.page.button.draft') }}
          </a-select-option>
          <a-select-option :value="1">
            {{ t('common.page.button.publish') }}
          </a-select-option>
          <a-select-option :value="2">
            {{ t('common.page.button.disable') }}
          </a-select-option>
        </a-select>
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item
        :label="t('entity.flowscheme.sortorder')"
        name="sortOrder"
      >
        <a-input-number
          v-model:value="sortOrderModel"
          :min="0"
          style="width: 100%"
        />
      </a-form-item>
    </a-col>
  </a-row>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import type { FlowSchemeCreate } from '@/types/workflow/flow-scheme'

const { t } = useI18n()

const props = defineProps<{
  form: FlowSchemeCreate & { schemeId?: string }
}>()

const emit = defineEmits<{
  'update:form': [form: FlowSchemeCreate & { schemeId?: string }]
}>()

function updateFormField<K extends keyof (FlowSchemeCreate & { schemeId?: string })>(
  key: K,
  value: (FlowSchemeCreate & { schemeId?: string })[K]
) {
  emit('update:form', { ...props.form, [key]: value })
}

const processKeyModel = computed({
  get: () => props.form.schemeKey ?? '',
  set: (value: string) => updateFormField('schemeKey', value)
})

const processNameModel = computed({
  get: () => props.form.schemeName ?? '',
  set: (value: string) => updateFormField('schemeName', value)
})

const processCategoryModel = computed({
  get: () => props.form.schemeCategory ?? 0,
  set: (value: number | null) => updateFormField('schemeCategory', value ?? 0)
})

const processDescriptionModel = computed({
  get: () => props.form.schemeDescription ?? '',
  set: (value: string) => updateFormField('schemeDescription', value)
})

const processStatusModel = computed({
  get: () => props.form.schemeStatus ?? 0,
  set: (value: number | null) => updateFormField('schemeStatus', value ?? 0)
})

const sortOrderModel = computed({
  get: () => props.form.sortOrder ?? 0,
  set: (value: number | null) => updateFormField('sortOrder', value ?? 0)
})
</script>
