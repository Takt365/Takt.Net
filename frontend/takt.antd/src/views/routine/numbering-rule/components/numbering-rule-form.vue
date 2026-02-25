<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/numbering-rule/components -->
<!-- 文件名称：numbering-rule-form.vue -->
<!-- 功能描述：单据编码规则表单组件 -->
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
              <a-form-item :label="t('entity.numberingrule.rulecode')" name="ruleCode">
                <a-input
                  v-model:value="formState.ruleCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.numberingrule.rulecode') })"
                  :disabled="!!formData?.ruleId"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.rulename')" name="ruleName">
                <a-input
                  v-model:value="formState.ruleName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.numberingrule.rulename') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.documenttype')" name="documentType">
                <a-input
                  v-model:value="formState.documentType"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.numberingrule.documenttype') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.prefix')" name="prefix">
                <a-input
                  v-model:value="formState.prefix"
                  :placeholder="t('common.form.placeholder.optional')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.dateformat')" name="dateFormat">
                <a-input
                  v-model:value="formState.dateFormat"
                  placeholder="yyyyMMdd / yyyyMM / yyyy"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.seriallength')" name="serialLength">
                <a-input-number
                  v-model:value="formState.serialLength"
                  :min="1"
                  :max="10"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.suffix')" name="suffix">
                <a-input
                  v-model:value="formState.suffix"
                  :placeholder="t('common.form.placeholder.optional')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.resetcycle')" name="resetCycle">
                <TaktSelect
                  v-model:value="formState.resetCycle"
                  dict-type="sys_numbering_reset_cycle"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.numberingrule.resetcycle') })"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.ordernum')" name="orderNum">
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('entity.numberingrule.rulestatus')" name="ruleStatus">
                <TaktSelect
                  v-model:value="formState.ruleStatus"
                  dict-type="sys_status"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.numberingrule.rulestatus') })"
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
import type { NumberingRule, NumberingRuleCreate, NumberingRuleUpdate } from '@/types/routine/numberingRule'

const props = withDefaults(
  defineProps<{
    formData?: Partial<NumberingRule> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const { t } = useI18n()
const formRef = ref<FormInstance>()
const activeTab = ref('basic')
const TOTAL_FIELDS = 11
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

const formState = ref<NumberingRuleCreate & { ruleId?: string }>({
  ruleCode: '',
  ruleName: '',
  documentType: '',
  prefix: undefined,
  dateFormat: undefined,
  serialLength: 5,
  suffix: undefined,
  resetCycle: 0,
  orderNum: 0,
  ruleStatus: 0,
  remark: undefined
})

const rules = computed<Record<string, Rule[]>>(() => ({
  ruleCode: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.numberingrule.rulecode') }), trigger: 'blur' }],
  ruleName: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.numberingrule.rulename') }), trigger: 'blur' }],
  documentType: [{ required: true, message: t('common.form.placeholder.required', { field: t('entity.numberingrule.documenttype') }), trigger: 'blur' }]
}))

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        ruleCode: val.ruleCode ?? '',
        ruleName: val.ruleName ?? '',
        documentType: val.documentType ?? '',
        prefix: val.prefix ?? undefined,
        dateFormat: val.dateFormat ?? undefined,
        serialLength: val.serialLength ?? 5,
        suffix: val.suffix ?? undefined,
        resetCycle: val.resetCycle ?? 0,
        orderNum: val.orderNum ?? 0,
        ruleStatus: val.ruleStatus ?? 0,
        remark: val.remark ?? undefined
      }
      if (val.ruleId) formState.value.ruleId = val.ruleId
    } else {
      formState.value = {
        ruleCode: '',
        ruleName: '',
        documentType: '',
        prefix: undefined,
        dateFormat: undefined,
        serialLength: 5,
        suffix: undefined,
        resetCycle: 0,
        orderNum: 0,
        ruleStatus: 0,
        remark: undefined
      }
    }
  },
  { immediate: true }
)

const validate = async () => {
  await formRef.value?.validate()
}

const getValues = (): NumberingRuleCreate | NumberingRuleUpdate => {
  const base: NumberingRuleCreate = {
    ruleCode: formState.value.ruleCode,
    ruleName: formState.value.ruleName,
    documentType: formState.value.documentType,
    prefix: formState.value.prefix || undefined,
    dateFormat: formState.value.dateFormat || undefined,
    serialLength: formState.value.serialLength ?? 5,
    suffix: formState.value.suffix || undefined,
    resetCycle: formState.value.resetCycle ?? 0,
    orderNum: formState.value.orderNum ?? 0,
    ruleStatus: formState.value.ruleStatus ?? 0,
    remark: formState.value.remark || undefined
  }
  if (formState.value.ruleId) {
    return { ...base, ruleId: formState.value.ruleId } as NumberingRuleUpdate
  }
  return base
}

defineExpose({ validate, getValues })
</script>

<style scoped lang="less">
// 与项目其他表单一致
</style>
