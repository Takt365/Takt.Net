<template>
  <a-form
    ref="formRef"
    :model="formState"
    :rules="rules"
    :label-col="{ span: 4 }"
    :wrapper-col="{ span: 20 }"
    layout="horizontal"
  >
    <a-form-item label="规则编码" name="ruleCode">
      <a-input
        v-model:value="formState.ruleCode"
        placeholder="如 ANNOUNCEMENT、PO"
        :disabled="!!formData?.numberingRuleId"
      />
    </a-form-item>
    <a-form-item label="规则名称" name="ruleName">
      <a-input v-model:value="formState.ruleName" placeholder="请输入规则名称" allow-clear />
    </a-form-item>
    <a-form-item label="公司编码" name="companyCode">
      <a-input v-model:value="formState.companyCode" placeholder="可选，用于匹配规则" allow-clear />
    </a-form-item>
    <a-form-item label="部门编码" name="deptCode">
      <a-input v-model:value="formState.deptCode" placeholder="可选，用于匹配规则" allow-clear />
    </a-form-item>
    <a-form-item label="前缀" name="prefix">
      <a-input v-model:value="formState.prefix" placeholder="如 ANN-" allow-clear />
    </a-form-item>
    <a-form-item label="日期格式" name="dateFormat">
      <a-input v-model:value="formState.dateFormat" placeholder="如 yyyyMMdd" allow-clear />
    </a-form-item>
    <a-form-item label="序号长度" name="numberLength">
      <a-input-number v-model:value="formState.numberLength" :min="1" :max="20" placeholder="如 5" style="width: 100%" />
    </a-form-item>
    <a-form-item label="后缀" name="suffix">
      <a-input v-model:value="formState.suffix" placeholder="可选" allow-clear />
    </a-form-item>
    <a-form-item label="步长" name="step">
      <a-input-number v-model:value="formState.step" :min="1" placeholder="如 1" style="width: 100%" />
    </a-form-item>
    <a-form-item label="排序号" name="orderNum">
      <a-input-number v-model:value="formState.orderNum" :min="0" style="width: 100%" />
    </a-form-item>
    <a-form-item label="备注" name="remark">
      <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="2" allow-clear />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { NumberingRule, NumberingRuleCreate, NumberingRuleUpdate } from '@/types/routine/tasks/numbering-rule'

const props = withDefaults(
  defineProps<{
    formData?: Partial<NumberingRule> | null
    loading?: boolean
  }>(),
  { formData: null, loading: false }
)

const formRef = ref<FormInstance>()
const formState = ref<NumberingRuleCreate & { numberingRuleId?: string }>({
  ruleCode: '',
  ruleName: '',
  companyCode: undefined,
  deptCode: undefined,
  prefix: undefined,
  dateFormat: undefined,
  numberLength: 5,
  suffix: undefined,
  step: 1,
  orderNum: 0,
  remark: undefined
})

const rules: Record<string, Rule[]> = {
  ruleCode: [{ required: true, message: '请输入规则编码' }],
  ruleName: [{ required: true, message: '请输入规则名称' }],
  numberLength: [{ required: true, message: '请输入序号长度' }]
}

watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = {
        ruleCode: val.ruleCode ?? '',
        ruleName: val.ruleName ?? '',
        companyCode: val.companyCode ?? undefined,
        deptCode: val.deptCode ?? undefined,
        prefix: val.prefix ?? undefined,
        dateFormat: val.dateFormat ?? undefined,
        numberLength: val.numberLength ?? 5,
        suffix: val.suffix ?? undefined,
        step: val.step ?? 1,
        orderNum: val.orderNum ?? 0,
        remark: val.remark ?? undefined
      }
      if (val.numberingRuleId) (formState.value as NumberingRuleUpdate).numberingRuleId = val.numberingRuleId
    } else {
      formState.value = {
        ruleCode: '',
        ruleName: '',
        companyCode: undefined,
        deptCode: undefined,
        prefix: undefined,
        dateFormat: undefined,
        numberLength: 5,
        suffix: undefined,
        step: 1,
        orderNum: 0,
        remark: undefined
      }
    }
  },
  { immediate: true }
)

function validate() {
  return formRef.value?.validate()
}

function getValues() {
  return formState.value
}

defineExpose({ validate, getValues })
</script>
