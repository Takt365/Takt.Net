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
      <a-form-item :label="t('entity.flowscheme.processkey')" name="processKey" required>
        <a-input
          v-model:value="form.processKey"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.flowscheme.processkey') })"
          :disabled="!!form.schemeId"
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item :label="t('entity.flowscheme.processname')" name="processName" required>
        <a-input
          v-model:value="form.processName"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.flowscheme.processname') })"
        />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item :label="t('entity.flowscheme.processcategory')" name="processCategory">
        <a-input-number v-model:value="form.processCategory" :min="0" style="width: 100%" />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item :label="t('entity.flowscheme.processdescription')" name="processDescription">
        <a-input v-model:value="form.processDescription" placeholder="" />
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item :label="t('entity.flowscheme.processstatus')" name="processStatus">
        <a-select v-model:value="form.processStatus" style="width: 100%" :placeholder="t('common.form.placeholder.select', { field: t('entity.flowscheme.processstatus') })">
          <a-select-option :value="0">{{ t('common.button.draft') }}</a-select-option>
          <a-select-option :value="1">{{ t('common.button.publish') }}</a-select-option>
          <a-select-option :value="2">{{ t('common.button.disable') }}</a-select-option>
        </a-select>
      </a-form-item>
    </a-col>
    <a-col :span="12">
      <a-form-item :label="t('entity.flowscheme.ordernum')" name="orderNum">
        <a-input-number v-model:value="form.orderNum" :min="0" style="width: 100%" />
      </a-form-item>
    </a-col>
  </a-row>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import type { FlowSchemeCreateOrUpdate } from '@/types/workflow/scheme'

const { t } = useI18n()

defineProps<{
  form: FlowSchemeCreateOrUpdate & { schemeId?: string }
}>()
</script>
