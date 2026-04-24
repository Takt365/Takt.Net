<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/i18n/components -->
<!-- 文件名称：translation-transposed-form.vue -->
<!-- 创建时间：2026-01-29 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：翻译转置表单，用于创建/编辑一个资源键下的所有语言翻译 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="translation-transposed-form">
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
          :disabled="isEdit"
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
      <a-divider orientation="left">
        {{ t('routine.localization.translation.divider.perCultureValues') }}
      </a-divider>
      <a-form-item
        v-for="lang in languageList"
        :key="lang.cultureCode"
        :label="lang.label"
        :name="['translations', lang.cultureCode]"
      >
        <a-input
          :value="formState.translations[lang.cultureCode] ?? ''"
          @update:value="(val) => { formState.translations[lang.cultureCode] = val }"
          :placeholder="t('routine.localization.translation.placeholders.translationValueForLang', { label: lang.label })"
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
import type { Translation } from '@/types/routine/tasks/i18n/translation'

interface LanguageItem {
  languageId: string
  cultureCode: string
  label: string
}

interface TransposedFormData {
  resourceKey: string
  resourceType: string
  resourceGroup: string
  orderNum: number
  remark: string
  translations: Record<string, string>
  /** 编辑模式下，存放每个 cultureCode 对应的 translationId */
  translationIds: Record<string, string>
  /** 编辑模式下，存放每个 cultureCode 对应的 languageId */
  languageIds: Record<string, string>
}

interface Props {
  /** 编辑模式下，传入该资源键下的所有翻译 */
  formData?: Translation[] | null
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: null,
  loading: false
})

const { t } = useI18n()

const formRef = ref()
const languageList = ref<LanguageItem[]>([])
const languageListLoading = ref(false)

const formState = reactive<TransposedFormData>({
  resourceKey: '',
  resourceType: 'Frontend',
  resourceGroup: '',
  orderNum: 0,
  remark: '',
  translations: {},
  translationIds: {},
  languageIds: {}
})

const isEdit = computed(() => Boolean(props.formData && props.formData.length > 0))

const formRules = computed<Record<string, Rule[]>>(() => ({
  resourceKey: [{ required: true, message: t('routine.localization.translation.rules.resourceKeyRequired'), trigger: 'blur' }],
  resourceType: [{ required: true, message: t('routine.localization.translation.rules.resourceTypeRequired'), trigger: 'change' }]
}))

async function loadLanguageList() {
  try {
    languageListLoading.value = true
    const listRaw = (await languageApi.getLanguageOptions()) as unknown[]
    languageList.value = listRaw.map((x) => {
      const r = x as Record<string, unknown>
      return {
        languageId: String(r['extValue'] ?? ''),
        cultureCode: String(r['dictValue'] ?? ''),
        label: `${String(r['dictLabel'] ?? '')} (${String(r['extLabel'] ?? r['dictValue'] ?? '')})`
      }
    })
    // 初始化 translations 对象
    languageList.value.forEach((lang) => {
      if (!(lang.cultureCode in formState.translations)) {
        formState.translations[lang.cultureCode] = ''
      }
      if (!(lang.cultureCode in formState.languageIds)) {
        formState.languageIds[lang.cultureCode] = lang.languageId
      }
    })
  } finally {
    languageListLoading.value = false
  }
}

watch(
  () => props.formData,
  (v) => {
    if (v && v.length > 0) {
      const first = v[0]!
      formState.resourceKey = first.resourceKey ?? ''
      formState.resourceType = first.resourceType ?? 'Frontend'
      formState.resourceGroup = first.resourceGroup ?? ''
      formState.orderNum = first.orderNum ?? 0
      formState.remark = first.remark ?? ''
      // 填充各语言翻译值
      formState.translations = {}
      formState.translationIds = {}
      formState.languageIds = {}
      v.forEach((tr) => {
        if (tr.cultureCode) {
          formState.translations[tr.cultureCode] = tr.translationValue ?? ''
          formState.translationIds[tr.cultureCode] = tr.translationId ?? ''
          formState.languageIds[tr.cultureCode] = tr.languageId ?? ''
        }
      })
      // 确保所有语言都有条目
      languageList.value.forEach((lang) => {
        if (!(lang.cultureCode in formState.translations)) {
          formState.translations[lang.cultureCode] = ''
          formState.languageIds[lang.cultureCode] = lang.languageId
        }
      })
    } else {
      formState.resourceKey = ''
      formState.resourceType = 'Frontend'
      formState.resourceGroup = ''
      formState.orderNum = 0
      formState.remark = ''
      formState.translations = {}
      formState.translationIds = {}
      formState.languageIds = {}
      languageList.value.forEach((lang) => {
        formState.translations[lang.cultureCode] = ''
        formState.languageIds[lang.cultureCode] = lang.languageId
      })
    }
  },
  { immediate: true, deep: true }
)

onMounted(() => {
  loadLanguageList()
})

async function validate() {
  await formRef.value?.validate()
}

function getFormData(): TransposedFormData {
  return {
    resourceKey: formState.resourceKey,
    resourceType: formState.resourceType,
    resourceGroup: formState.resourceGroup,
    orderNum: formState.orderNum,
    remark: formState.remark,
    translations: { ...formState.translations },
    translationIds: { ...formState.translationIds },
    languageIds: { ...formState.languageIds }
  }
}

defineExpose({ validate, getFormData })
</script>

<style scoped lang="less">
.translation-transposed-form {
  padding: 0;
  max-height: 60vh;
  overflow-y: auto;
}
</style>
