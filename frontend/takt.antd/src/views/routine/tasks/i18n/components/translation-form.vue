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
      <a-form-item
        :label="t('entity.translation.resourcekey')"
        name="resourceKey"
      >
        <a-input
          v-model:value="formState.resourceKey"
          :placeholder="t('routine.localization.translation.placeholders.resourceKeyExample')"
        />
      </a-form-item>
      <a-form-item
        :label="t('routine.localization.translation.form.languageSub')"
        name="cultureCode"
      >
        <TaktSelect
          v-model:value="formState.cultureCode"
          :options="languageOptions"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.translation.culturecode') })"
          :field-names="{ label: 'dictLabel', value: 'dictValue' }"
          :loading="languageOptionsLoading"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('entity.translation.translationvalue')"
        name="translationValue"
      >
        <a-input
          v-model:value="formState.translationValue"
          :placeholder="t('routine.localization.translation.placeholders.translationValueInLanguage')"
        />
      </a-form-item>
      <a-form-item
        :label="t('entity.translation.resourcetype')"
        name="resourceType"
      >
        <a-select
          v-model:value="formState.resourceType"
          :placeholder="t('routine.localization.translation.placeholders.resourceTypeSelect')"
          allow-clear
        >
          <a-select-option value="Frontend">
            {{ t('routine.localization.translation.options.frontend') }}
          </a-select-option>
          <a-select-option value="Backend">
            {{ t('routine.localization.translation.options.backend') }}
          </a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item
        :label="t('entity.translation.resourcegroup')"
        name="resourceGroup"
      >
        <a-input
          v-model:value="formState.resourceGroup"
          :placeholder="t('routine.localization.translation.placeholders.resourceGroupOptional')"
        />
      </a-form-item>
      <a-form-item
        :label="t('entity.translation.ordernum')"
        name="orderNum"
      >
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          :placeholder="t('routine.localization.translation.placeholders.orderNumHint')"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item
        :label="t('common.entity.remark')"
        name="remark"
      >
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('routine.localization.translation.placeholders.remarkOptional')"
          :rows="2"
        />
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { Rule } from 'ant-design-vue/es/form'
import * as languageApi from '@/api/routine/tasks/i18n/language'
import type { Translation, TranslationCreate, TranslationUpdate } from '@/types/routine/tasks/i18n/translation'
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

type TranslationFormState = Omit<TranslationCreate, 'resourceGroup' | 'remark'> & {
  translationId?: string
  resourceGroup: string
  remark: string
}

const formState = reactive<TranslationFormState>({
  resourceKey: '',
  languageId: '',
  cultureCode: '',
  translationValue: '',
  resourceType: 'Frontend',
  resourceGroup: '',
  orderNum: 0,
  remark: ''
})

const formRules = computed<Record<string, Rule[]>>(() => ({
  resourceKey: [{ required: true, message: t('routine.localization.translation.rules.resourceKeyRequired'), trigger: 'blur' }],
  cultureCode: [{ required: true, message: t('routine.localization.translation.rules.cultureCodeRequired'), trigger: 'change' }],
  translationValue: [{ required: true, message: t('routine.localization.translation.rules.translationValueRequired'), trigger: 'blur' }],
  resourceType: [{ required: true, message: t('routine.localization.translation.rules.resourceTypeRequired'), trigger: 'change' }]
}))

async function loadLanguageOptions() {
  try {
    languageOptionsLoading.value = true
    const list = await languageApi.getLanguageOptions()
    const listRaw = (list || []) as unknown[]
    languageOptions.value = listRaw.map((x): TaktSelectOption => {
      const r = x as Record<string, unknown>
      const ev = r['extValue']
      const base: TaktSelectOption = {
        dictLabel: `${String(r['dictLabel'] ?? '')} (${String(r['extLabel'] ?? r['dictValue'] ?? '')})`,
        dictValue: (r['dictValue'] ?? '') as string | number,
        orderNum: typeof r['orderNum'] === 'number' ? r['orderNum'] : Number(r['orderNum'] ?? 0)
      }
      if (ev != null && String(ev) !== '') {
        base.extValue = ev as string | number
      }
      return base
    })
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
    const opt = languageOptions.value.find(
      (o) => o.dictValue === code || o.extValue === code
    )
    formState.languageId = String(opt?.extValue ?? '')
  }
)

onMounted(() => {
  loadLanguageOptions()
})

async function validate() {
  await formRef.value?.validate()
}

function getFormData(): TranslationCreate | TranslationUpdate {
  const opt = languageOptions.value.find(
    (o) => o.dictValue === formState.cultureCode || o.extValue === formState.cultureCode
  )
  const languageId = String(opt?.extValue ?? formState.languageId ?? '')
  const base: TranslationCreate = {
    resourceKey: formState.resourceKey,
    cultureCode: formState.cultureCode,
    translationValue: formState.translationValue,
    resourceType: formState.resourceType,
    languageId,
    orderNum: formState.orderNum
  }
  if (formState.resourceGroup && formState.resourceGroup.trim() !== '') {
    base.resourceGroup = formState.resourceGroup
  }
  if (formState.remark && formState.remark.trim() !== '') {
    base.remark = formState.remark
  }
  if (formState.translationId) {
    return { ...base, translationId: formState.translationId } as TranslationUpdate
  }
  return base
}

defineExpose({ validate, getFormData })
</script>

<style scoped lang="less">
.translation-form {
  padding: 0;
}
</style>
