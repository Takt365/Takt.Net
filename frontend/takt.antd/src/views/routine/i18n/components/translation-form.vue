<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/i18n/components -->
<!-- 文件名称：translation-form.vue -->
<!-- 创建时间：2026-01-29 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：翻译表单（主表），用于创建/编辑单条翻译 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="translation-form">
    <a-form
      ref="formRef"
      :model="formState"
      :rules="formRules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      layout="horizontal"
    >
      <a-form-item label="资源键" name="resourceKey">
        <a-input
          v-model:value="formState.resourceKey"
          placeholder="如：UserNotFound、menu.home._self"
        />
      </a-form-item>
      <a-form-item label="语言（子表）" name="cultureCode">
        <TaktSelect
          v-model:value="formState.cultureCode"
          :options="languageOptions"
          placeholder="请选择语言"
          :field-names="{ label: 'dictLabel', value: 'dictValue' }"
          :loading="languageOptionsLoading"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="翻译值" name="translationValue">
        <a-input
          v-model:value="formState.translationValue"
          placeholder="该语言下的文本内容"
        />
      </a-form-item>
      <a-form-item label="资源类型" name="resourceType">
        <a-select
          v-model:value="formState.resourceType"
          placeholder="Frontend / Backend"
          allow-clear
        >
          <a-select-option value="Frontend">Frontend</a-select-option>
          <a-select-option value="Backend">Backend</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="资源分组" name="resourceGroup">
        <a-input
          v-model:value="formState.resourceGroup"
          placeholder="如：Validation、Menu（可选）"
        />
      </a-form-item>
      <a-form-item label="排序号" name="orderNum">
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          placeholder="越小越靠前"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('common.entity.remark')" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
          :rows="2"
        />
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import * as languageApi from '@/api/routine/i18n/language'
import type { Translation, TranslationCreate, TranslationUpdate } from '@/types/routine/i18n/translation'
import type { TaktSelectOption } from '@/types/common'

interface Props {
  formData?: Translation | null
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: null,
  loading: false
})

const { t } = useI18n()
const formRef = ref()
const languageOptions = ref<TaktSelectOption[]>([])
const languageOptionsLoading = ref(false)

const formState = reactive<TranslationCreate & { translationId?: string }>({
  resourceKey: '',
  languageId: '',
  cultureCode: '',
  translationValue: '',
  resourceType: 'Frontend',
  resourceGroup: '',
  orderNum: 0,
  remark: ''
})

const formRules: Record<string, Rule[]> = {
  resourceKey: [{ required: true, message: '请输入资源键', trigger: 'blur' }],
  cultureCode: [{ required: true, message: '请选择语言', trigger: 'change' }],
  translationValue: [{ required: true, message: '请输入翻译值', trigger: 'blur' }],
  resourceType: [{ required: true, message: '请选择资源类型', trigger: 'change' }]
}

async function loadLanguageOptions() {
  try {
    languageOptionsLoading.value = true
    const list = await languageApi.getOptions()
    languageOptions.value = (list || []).map((x: any) => ({
      dictLabel: `${x.dictLabel} (${x.extLabel ?? x.dictValue})`,
      dictValue: x.dictValue,
      extValue: x.extValue,
      orderNum: x.orderNum ?? 0
    }))
  } finally {
    languageOptionsLoading.value = false
  }
}

watch(
  () => props.formData,
  (v) => {
    if (v) {
      Object.assign(formState, {
        translationId: v.translationId,
        resourceKey: v.resourceKey ?? '',
        languageId: v.languageId ?? '',
        cultureCode: v.cultureCode ?? '',
        translationValue: v.translationValue ?? '',
        resourceType: v.resourceType ?? 'Frontend',
        resourceGroup: v.resourceGroup ?? '',
        orderNum: v.orderNum ?? 0,
        remark: v.remark ?? ''
      })
    } else {
      Object.assign(formState, {
        translationId: undefined,
        resourceKey: '',
        languageId: '',
        cultureCode: '',
        translationValue: '',
        resourceType: 'Frontend',
        resourceGroup: '',
        orderNum: 0,
        remark: ''
      })
    }
  },
  { immediate: true, deep: true }
)

watch(
  () => formState.cultureCode,
  (code) => {
    const opt = languageOptions.value.find((o: any) => o.dictValue === code || o.extValue === code)
    ;(formState as any).languageId = opt?.extValue ?? ''
  }
)

onMounted(() => {
  loadLanguageOptions()
})

async function validate() {
  await formRef.value?.validate()
}

function getFormData(): TranslationCreate | TranslationUpdate {
  const base: any = {
    resourceKey: formState.resourceKey,
    cultureCode: formState.cultureCode,
    translationValue: formState.translationValue,
    resourceType: formState.resourceType,
    resourceGroup: formState.resourceGroup || undefined,
    orderNum: formState.orderNum,
    remark: formState.remark || undefined
  }
  const opt = languageOptions.value.find((o: any) => o.dictValue === formState.cultureCode || o.extValue === formState.cultureCode)
  base.languageId = String(opt?.extValue ?? formState.languageId ?? '')
  if (formState.translationId) {
    return { ...base, translationId: formState.translationId } as TranslationUpdate
  }
  return base as TranslationCreate
}

defineExpose({ validate, getFormData })
</script>

<style scoped lang="less">
.translation-form {
  padding: 0;
}
</style>
