<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/title/components -->
<!-- 文件名称：accounting-title-form.vue -->
<!-- 功能描述：会计科目维护弹窗内嵌表单；父页面 `title/index.vue` 通过 ref 调用 `validate`、`getValues`、`resetFields`；字段与 `AccountingTitle` / `AccountingTitleCreate` / `AccountingTitleUpdate` 对齐，科目类型/余额方向/辅助核算类型选项来自 `locales/accounting/title/*`。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 单 Tab：与 leave-form 一致，预留未来扩展 -->
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicinfo')"
    >
      <div :class="formContentClass">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
          label-align="right"
        >
          <!-- 科目编码、科目名称 -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.titlecode')"
                name="titleCode"
              >
                <a-input
                  v-model:value="formState.titleCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.accountingtitle.titlecode') })"
                  :maxlength="50"
                  :disabled="!!formData?.titleId"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.titlename')"
                name="titleName"
              >
                <a-input
                  v-model:value="formState.titleName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.accountingtitle.titlename') })"
                  :maxlength="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>

          <!-- 父级科目、科目类型 -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.parentid')"
                name="parentId"
              >
                <TaktTreeSelect
                  v-model="formState.parentId"
                  api-url="/api/TaktAccountingTitles/tree-options"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.accountingtitle.parentid') })"
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.titletype')"
                name="titleType"
              >
                <a-select
                  v-model:value="formState.titleType"
                  :options="titleTypeOptions"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.accountingtitle.titletype') })"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <!-- 余额方向、辅助核算类型 -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.balancedirection')"
                name="balanceDirection"
              >
                <a-select
                  v-model:value="formState.balanceDirection"
                  :options="balanceDirectionOptions"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.accountingtitle.balancedirection') })"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.auxiliarytype')"
                name="auxiliaryType"
              >
                <a-select
                  v-model:value="formState.auxiliaryType"
                  :options="auxiliaryTypeOptions"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.accountingtitle.auxiliarytype') })"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <!-- 业务开关（0/1） -->
          <a-row :gutter="24">
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.isleaf')"
                name="isLeaf"
              >
                <a-switch v-model:checked="isLeafChecked" />
              </a-form-item>
            </a-col>
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.isauxiliary')"
                name="isAuxiliary"
              >
                <a-switch v-model:checked="isAuxiliaryChecked" />
              </a-form-item>
            </a-col>
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.isquantity')"
                name="isQuantity"
              >
                <a-switch v-model:checked="isQuantityChecked" />
              </a-form-item>
            </a-col>
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.iscurrency')"
                name="isCurrency"
              >
                <a-switch v-model:checked="isCurrencyChecked" />
              </a-form-item>
            </a-col>
          </a-row>

          <!-- 现金/银行科目开关、排序号 -->
          <a-row :gutter="24">
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.iscash')"
                name="isCash"
              >
                <a-switch v-model:checked="isCashChecked" />
              </a-form-item>
            </a-col>
            <a-col :span="6">
              <a-form-item
                :label="t('entity.accountingtitle.isbank')"
                name="isBank"
              >
                <a-switch v-model:checked="isBankChecked" />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.accountingtitle.ordernum')"
                name="orderNum"
              >
                <a-input-number
                  v-model:value="formState.orderNum"
                  :placeholder="t('common.form.placeholder.ordernumhint')"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <!-- 备注 -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('common.entity.remark')"
                name="remark"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <a-textarea
                  v-model:value="formState.remark"
                  :rows="2"
                  :maxlength="500"
                  :placeholder="t('common.form.placeholder.required', { field: t('common.entity.remark') })"
                  allow-clear
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
/**
 * 会计科目表单脚本：父级通过 `ref` 调用 `validate`、`getValues`、`resetFields`；
 * 编辑态通过 `props.formData` 回填，新增态使用默认值。
 */
import { computed, reactive, ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { AccountingTitle, AccountingTitleCreate } from '@/types/accounting/financial/title'
import TaktTreeSelect from '@/components/business/takt-tree-select/index.vue'

const { t } = useI18n()

/**
 * 组件入参：由父级列表页传入当前编辑行或空对象。
 */
interface Props {
  /**
   * 编辑时的会计科目数据。
   */
  formData?: Partial<AccountingTitle>

  /**
   * 父级提交中的 loading 状态预留。
   */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * Ant Design Vue 表单实例。
 */
const formRef = ref<FormInstance>()

/**
 * 当前激活的 Tab 键；仅 `basic`。
 */
const activeTab = ref('basic')

/**
 * 估算字段数量，用于选择表单容器样式。
 */
const totalFields = 16

/**
 * 表单外层纵向布局类名。
 */
const formContentClass = computed(() => (totalFields >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

/**
 * 表单状态类型：对齐 `AccountingTitleCreate`。
 */
interface TitleFormState {
  titleCode: string
  titleName: string
  parentId: number
  titleType: number
  balanceDirection: number
  isLeaf: number
  isAuxiliary: number
  auxiliaryType: number
  isQuantity: number
  isCurrency: number
  isCash: number
  isBank: number
  orderNum: number
  remark: string
}

/**
 * 新增态默认值。
 *
 * @returns {TitleFormState}
 */
function createEmptyTitleForm(): TitleFormState {
  return {
    titleCode: '',
    titleName: '',
    parentId: 0,
    titleType: 0,
    balanceDirection: 0,
    isLeaf: 1,
    isAuxiliary: 0,
    auxiliaryType: 0,
    isQuantity: 0,
    isCurrency: 0,
    isCash: 0,
    isBank: 0,
    orderNum: 0,
    remark: ''
  }
}

/**
 * 表单响应式状态。
 */
const formState = reactive<TitleFormState>(createEmptyTitleForm())

/**
 * 科目类型选项（0-5）。
 */
const titleTypeOptions = computed(() =>
  [0, 1, 2, 3, 4, 5].map((value) => ({
    value,
    label: t(`accounting.title.page.titletype${value}`)
  }))
)

/**
 * 余额方向选项（0-1）。
 */
const balanceDirectionOptions = computed(() =>
  [0, 1].map((value) => ({
    value,
    label: t(`accounting.title.page.balancedirection${value}`)
  }))
)

/**
 * 辅助核算类型选项（0-6）。
 */
const auxiliaryTypeOptions = computed(() =>
  [0, 1, 2, 3, 4, 5, 6].map((value) => ({
    value,
    label: t(`accounting.title.page.auxiliarytype${value}`)
  }))
)

/**
 * `isLeaf` 的 switch 映射（布尔 <-> 0/1）。
 */
const isLeafChecked = computed({
  get: () => formState.isLeaf === 1,
  set: (checked: boolean) => {
    formState.isLeaf = checked ? 1 : 0
  }
})

/**
 * `isAuxiliary` 的 switch 映射（布尔 <-> 0/1）。
 */
const isAuxiliaryChecked = computed({
  get: () => formState.isAuxiliary === 1,
  set: (checked: boolean) => {
    formState.isAuxiliary = checked ? 1 : 0
  }
})

/**
 * `isQuantity` 的 switch 映射（布尔 <-> 0/1）。
 */
const isQuantityChecked = computed({
  get: () => formState.isQuantity === 1,
  set: (checked: boolean) => {
    formState.isQuantity = checked ? 1 : 0
  }
})

/**
 * `isCurrency` 的 switch 映射（布尔 <-> 0/1）。
 */
const isCurrencyChecked = computed({
  get: () => formState.isCurrency === 1,
  set: (checked: boolean) => {
    formState.isCurrency = checked ? 1 : 0
  }
})

/**
 * `isCash` 的 switch 映射（布尔 <-> 0/1）。
 */
const isCashChecked = computed({
  get: () => formState.isCash === 1,
  set: (checked: boolean) => {
    formState.isCash = checked ? 1 : 0
  }
})

/**
 * `isBank` 的 switch 映射（布尔 <-> 0/1）。
 */
const isBankChecked = computed({
  get: () => formState.isBank === 1,
  set: (checked: boolean) => {
    formState.isBank = checked ? 1 : 0
  }
})

/**
 * 表单校验规则（与模板 `name` 一一对应）。
 */
const rules = computed<Record<string, Rule[]>>(() => ({
  titleCode: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.accountingtitle.titlecode') }),
      trigger: 'blur'
    }
  ],
  titleName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.accountingtitle.titlename') }),
      trigger: 'blur'
    }
  ],
  titleType: [
    {
      required: true,
      message: t('common.form.placeholder.select', { field: t('entity.accountingtitle.titletype') }),
      trigger: 'change'
    }
  ],
  balanceDirection: [
    {
      required: true,
      message: t('common.form.placeholder.select', { field: t('entity.accountingtitle.balancedirection') }),
      trigger: 'change'
    }
  ]
}))

/**
 * 将 unknown 转数字，不可转时使用默认值。
 *
 * @param {unknown} value
 * @param {number} defaultValue
 * @returns {number}
 */
function toNum(value: unknown, defaultValue = 0): number {
  const n = Number(value)
  return Number.isFinite(n) ? n : defaultValue
}

/**
 * 同步父组件传入的编辑数据。
 */
watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        titleCode: newData.titleCode ?? '',
        titleName: newData.titleName ?? '',
        parentId: toNum(newData.parentId, 0),
        titleType: toNum(newData.titleType, 0),
        balanceDirection: toNum(newData.balanceDirection, 0),
        isLeaf: toNum(newData.isLeaf, 1),
        isAuxiliary: toNum(newData.isAuxiliary, 0),
        auxiliaryType: toNum(newData.auxiliaryType, 0),
        isQuantity: toNum(newData.isQuantity, 0),
        isCurrency: toNum(newData.isCurrency, 0),
        isCash: toNum(newData.isCash, 0),
        isBank: toNum(newData.isBank, 0),
        orderNum: toNum(newData.orderNum, 0),
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, createEmptyTitleForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/**
 * 对外：触发表单校验。
 *
 * @returns {Promise<void>}
 */
const validate = async () => {
  await formRef.value?.validate()
}

/**
 * 对外：获取提交数据，编辑态附带 `titleId`。
 *
 * @returns {AccountingTitleCreate & { titleId?: string }}
 */
const getValues = (): AccountingTitleCreate & { titleId?: string } => {
  const payload: AccountingTitleCreate & { titleId?: string } = {
    titleCode: formState.titleCode,
    titleName: formState.titleName,
    parentId: formState.parentId,
    titleType: formState.titleType,
    balanceDirection: formState.balanceDirection,
    isLeaf: formState.isLeaf,
    isAuxiliary: formState.isAuxiliary,
    auxiliaryType: formState.auxiliaryType,
    isQuantity: formState.isQuantity,
    isCurrency: formState.isCurrency,
    isCash: formState.isCash,
    isBank: formState.isBank,
    orderNum: formState.orderNum,
    remark: formState.remark
  }

  if (props.formData?.titleId != null) {
    payload.titleId = String(props.formData.titleId)
  }

  return payload
}

/**
 * 对外：重置表单。
 *
 * @returns {void}
 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyTitleForm())
  activeTab.value = 'basic'
}

/**
 * 暴露给父组件的 ref 方法。
 */
defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；保留占位结构。 */
</style>
