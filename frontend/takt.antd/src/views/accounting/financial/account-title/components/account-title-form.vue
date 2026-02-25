<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/account-title/components -->
<!-- 文件名称：account-title-form.vue -->
<!-- 功能描述：会计科目新增/编辑表单 -->
<!-- ======================================== -->

<template>
  <div class="account-title-form">
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      layout="horizontal"
      label-align="right"
    >
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="科目编码" name="titleCode">
            <a-input
              v-model:value="formState.titleCode"
              placeholder="请输入科目编码"
              show-count
              :maxlength="50"
              :disabled="!!props.formData?.titleId"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="科目名称" name="titleName">
            <a-input
              v-model:value="formState.titleName"
              placeholder="请输入科目名称"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item label="上级科目" name="parentId" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
            <TaktTreeSelect
              v-model:value="formState.parentId"
              api-url="/api/TaktAccountTitles/tree-options"
              placeholder="请选择上级科目（不选为根）"
              allow-clear
              :field-names="{ label: 'dictLabel', value: 'dictValue' }"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="科目类型" name="titleType">
            <a-select
              v-model:value="formState.titleType"
              placeholder="请选择科目类型"
              :options="titleTypeOptions"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="余额方向" name="balanceDirection">
            <a-select
              v-model:value="formState.balanceDirection"
              placeholder="请选择余额方向"
              :options="balanceDirectionOptions"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="排序号" name="orderNum">
            <a-input-number
              v-model:value="formState.orderNum"
              placeholder="越小越靠前"
              :min="0"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="是否统驭科目" name="isReconciliationAccount">
            <a-select
              v-model:value="formState.isReconciliationAccount"
              placeholder="请选择"
              :options="reconciliationOptions"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item label="生效日期" name="effectiveDate">
            <a-date-picker
              v-model:value="formState.effectiveDate"
              placeholder="请选择"
              style="width: 100%"
              value-format="YYYY-MM-DD"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="失效日期" name="expiryDate">
            <a-date-picker
              v-model:value="formState.expiryDate"
              placeholder="默认长期有效"
              style="width: 100%"
              value-format="YYYY-MM-DD"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item label="备注" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
            <a-textarea
              v-model:value="formState.remark"
              placeholder="请输入备注"
              :rows="2"
              show-count
              :maxlength="500"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { AccountTitle, AccountTitleCreate } from '@/types/accounting/financial/account-title'

const props = withDefaults(
  defineProps<{
    formData: Partial<AccountTitle> | null
    loading?: boolean
  }>(),
  { loading: false }
)

const formRef = ref<FormInstance>()
const formState = ref<Partial<AccountTitleCreate & { titleId: string }>>({
  titleCode: '',
  titleName: '',
  parentId: '0',
  titleType: 0,
  balanceDirection: 0,
  orderNum: 0,
  effectiveDate: undefined,
  expiryDate: undefined,
  isReconciliationAccount: 0,
  remark: undefined
})

const titleTypeOptions = [
  { label: '资产', value: 0 },
  { label: '负债', value: 1 },
  { label: '所有者权益', value: 2 },
  { label: '收入', value: 3 },
  { label: '费用', value: 4 },
  { label: '成本', value: 5 }
]
const balanceDirectionOptions = [
  { label: '借方', value: 0 },
  { label: '贷方', value: 1 }
]
const reconciliationOptions = [
  { label: '否', value: 0 },
  { label: '是', value: 1 }
]

const rules: Record<string, Rule[]> = {
  titleCode: [{ required: true, message: '请输入科目编码', trigger: 'blur' }],
  titleName: [{ required: true, message: '请输入科目名称', trigger: 'blur' }]
}

watch(
  () => props.formData,
  (data) => {
    assignFormState(data ?? null)
  },
  { immediate: true, deep: true }
)

function assignFormState(data: Partial<AccountTitle> | null) {
  if (!data) {
    formState.value = {
      titleCode: '',
      titleName: '',
      parentId: '0',
      titleType: 0,
      balanceDirection: 0,
      orderNum: 0,
      effectiveDate: undefined,
      expiryDate: undefined,
      isReconciliationAccount: 0,
      remark: undefined
    }
    return
  }
  formState.value = {
    titleId: data.titleId!,
    titleCode: data.titleCode ?? '',
    titleName: data.titleName ?? '',
    parentId: data.parentId ?? '0',
    titleType: data.titleType ?? 0,
    balanceDirection: data.balanceDirection ?? 0,
    orderNum: data.orderNum ?? 0,
    effectiveDate: data.effectiveDate,
    expiryDate: data.expiryDate,
    isReconciliationAccount: data.isReconciliationAccount ?? 0,
    remark: data.remark
  }
}

defineExpose({
  validate: () => formRef.value?.validate(),
  getValues: (): AccountTitleCreate & { titleId?: string } => {
    const s = formState.value
    const parentId = s.parentId === undefined || s.parentId === '' ? '0' : String(s.parentId)
    return {
      ...(s.titleId ? { titleId: s.titleId } : {}),
      titleCode: s.titleCode ?? '',
      titleName: s.titleName ?? '',
      parentId,
      titleType: s.titleType ?? 0,
      balanceDirection: s.balanceDirection ?? 0,
      orderNum: s.orderNum ?? 0,
      effectiveDate: s.effectiveDate,
      expiryDate: s.expiryDate,
      isReconciliationAccount: s.isReconciliationAccount ?? 0,
      remark: s.remark
    }
  },
  resetFields: () => formRef.value?.resetFields(),
  assignFormState
})
</script>

<style scoped lang="less">
.account-title-form {
  padding: 0 8px;
}
</style>
