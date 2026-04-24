<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 20 }"
    layout="horizontal"
  >
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.ruleCode')"
      name="ruleCode"
    >
      <a-input
        v-model:value="formState.ruleCode"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderRuleCode')"
        :disabled="!!formData?.numberingRuleId"
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.ruleName')"
      name="ruleName"
    >
      <a-input
        v-model:value="formState.ruleName"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderRuleName')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.companyCode')"
      name="companyCode"
    >
      <a-input
        v-model:value="formState.companyCode"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderCompanyCode')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.deptCode')"
      name="deptCode"
    >
      <a-input
        v-model:value="formState.deptCode"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderDeptCode')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.prefix')"
      name="prefix"
    >
      <a-input
        v-model:value="formState.prefix"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderPrefix')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.dateFormat')"
      name="dateFormat"
    >
      <a-input
        v-model:value="formState.dateFormat"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderDateFormat')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.numberLength')"
      name="numberLength"
    >
      <a-input-number
        v-model:value="formState.numberLength"
        :min="1"
        :max="20"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderNumberLength')"
        style="width: 100%"
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.suffix')"
      name="suffix"
    >
      <a-input
        v-model:value="formState.suffix"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderSuffix')"
        allow-clear
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.step')"
      name="step"
    >
      <a-input-number
        v-model:value="formState.step"
        :min="1"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderStep')"
        style="width: 100%"
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.orderNum')"
      name="orderNum"
    >
      <a-input-number
        v-model:value="formState.orderNum"
        :min="0"
        style="width: 100%"
      />
    </a-form-item>
    <a-form-item
      :label="t('routine.tasks.numbering-rule.form.remark')"
      name="remark"
    >
      <a-textarea
        v-model:value="formState.remark"
        :placeholder="t('routine.tasks.numbering-rule.form.placeholderRemark')"
        :rows="2"
        allow-clear
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import type { NumberingRule, NumberingRuleCreate } from '@/types/routine/tasks/numbering-rule/numbering-rule'

/** 与 a-input 绑定的可选字段一律为 string，避免 exactOptionalPropertyTypes 下 undefined 与 v-model 不兼容。 */
type NumberingRuleFormModel = {
  numberingRuleId?: string
  ruleCode: string
  ruleName: string
  companyCode: string
  deptCode: string
  prefix: string
  dateFormat: string
  numberLength: number
  suffix: string
  step: number
  orderNum: number
  remark: string
}

export type NumberingRuleFormValues = NumberingRuleCreate & { numberingRuleId?: string }

const props = withDefaults(
  defineProps<{
    formData?: Partial<NumberingRule> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const { t } = useI18n()

const formRef = ref<FormInstance>()
const emptyModel = (): NumberingRuleFormModel => ({
  ruleCode: '',
  ruleName: '',
  companyCode: '',
  deptCode: '',
  prefix: '',
  dateFormat: '',
  numberLength: 5,
  suffix: '',
  step: 1,
  orderNum: 0,
  remark: ''
})

const formState = ref<NumberingRuleFormModel>(emptyModel())

const rules = computed<Record<string, Rule[]>>(() => ({
  ruleCode: [{ required: true, message: t('routine.tasks.numbering-rule.validation.ruleCode') }],
  ruleName: [{ required: true, message: t('routine.tasks.numbering-rule.validation.ruleName') }],
  numberLength: [{ required: true, message: t('routine.tasks.numbering-rule.validation.numberLength') }]
}))

watch(
  () => props.formData,
  val => {
    if (val) {
      const next: NumberingRuleFormModel = {
        ...emptyModel(),
        ruleCode: val.ruleCode ?? '',
        ruleName: val.ruleName ?? '',
        companyCode: val.companyCode ?? '',
        deptCode: val.deptCode ?? '',
        prefix: val.prefix ?? '',
        dateFormat: val.dateFormat ?? '',
        numberLength: val.numberLength ?? 5,
        suffix: val.suffix ?? '',
        step: val.step ?? 1,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? ''
      }
      if (val.numberingRuleId) {
        next.numberingRuleId = val.numberingRuleId
      }
      formState.value = next
    } else {
      formState.value = emptyModel()
    }
  },
  { immediate: true }
)

function optionalString(s: string): string | undefined {
  const trimmed = s.trim()
  return trimmed.length > 0 ? trimmed : undefined
}

function validate() {
  return formRef.value?.validate()
}

function getValues(): NumberingRuleFormValues {
  const s = formState.value
  const companyCode = optionalString(s.companyCode)
  const deptCode = optionalString(s.deptCode)
  const prefix = optionalString(s.prefix)
  const dateFormat = optionalString(s.dateFormat)
  const suffix = optionalString(s.suffix)
  const remark = optionalString(s.remark)
  const base: NumberingRuleCreate = {
    ruleCode: s.ruleCode,
    ruleName: s.ruleName,
    numberLength: s.numberLength,
    step: s.step,
    orderNum: s.orderNum,
    ...(companyCode !== undefined ? { companyCode } : {}),
    ...(deptCode !== undefined ? { deptCode } : {}),
    ...(prefix !== undefined ? { prefix } : {}),
    ...(dateFormat !== undefined ? { dateFormat } : {}),
    ...(suffix !== undefined ? { suffix } : {}),
    ...(remark !== undefined ? { remark } : {})
  }
  if (s.numberingRuleId) {
    return { ...base, numberingRuleId: s.numberingRuleId }
  }
  return base
}

defineExpose({ validate, getValues })
</script>
