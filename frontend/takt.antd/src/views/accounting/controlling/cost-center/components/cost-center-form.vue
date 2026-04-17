<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/controlling/cost-center/components -->
<!-- 文件名称：cost-center-form.vue -->
<!-- 功能描述：成本中心新增/编辑弹窗内嵌表单；父级通过 ref 调用 `validate`、`getValues`、`resetFields`；字段与 `@/types/accounting/controlling/cost-center` 中 `CostCenter` / `CostCenterCreate` / `CostCenterUpdate` 对齐；父级 `TaktSelect` 拉取 `/api/TaktCostCenters/options`、`TaktTreeSelect` 拉取 `/api/TaktDepts/tree-options`；类型枚举 0/1/2 对应后端 `TaktCostCenterCreateDto.CostCenterType` 注释。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <!-- 单 Tab：与 leave-form 一致，预留未来扩展 -->
  <a-tabs v-model:active-key="activeTab">
    <a-tab-pane
      key="basic"
      :tab="t('common.form.tabs.basicInfo')"
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
          <!-- 编码、名称（编辑态编码禁用，与 `CostCenter.costCenterId` 是否存在一致） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.costcentercode')"
                name="costCenterCode"
              >
                <a-input
                  v-model:value="formState.costCenterCode"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.costcenter.costcentercode') })"
                  show-count
                  :maxlength="50"
                  :disabled="!!formData?.costCenterId"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.costcentername')"
                name="costCenterName"
              >
                <a-input
                  v-model:value="formState.costCenterName"
                  :placeholder="t('common.form.placeholder.required', { field: t('entity.costcenter.costcentername') })"
                  show-count
                  :maxlength="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 父级、成本中心类型（类型选项文案见 `locales/accounting/controlling/cost-center`） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.parentid')"
                name="parentId"
              >
                <TaktSelect
                  v-model="formState.parentId"
                  api-url="/api/TaktCostCenters/options"
                  :placeholder="t('accounting.controlling.cost-center.parentSelectPlaceholder')"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.costcentertype')"
                name="costCenterType"
              >
                <a-select
                  v-model:value="formState.costCenterType"
                  :options="costCenterTypeOptions"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.costcenter.costcentertype') })"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 所属部门（`deptId`，与 `TaktCostCenterCreateDto.DeptId` 一致） -->
          <a-row :gutter="24">
            <a-col :span="24">
              <a-form-item
                :label="t('entity.costcenter.deptid')"
                name="deptId"
                :label-col="{ span: 4 }"
                :wrapper-col="{ span: 20 }"
              >
                <TaktTreeSelect
                  v-model="formState.deptId"
                  api-url="/api/TaktDepts/tree-options"
                  :placeholder="t('common.form.placeholder.select', { field: t('entity.costcenter.deptid') })"
                  allow-clear
                  :field-names="{ label: 'dictLabel', value: 'dictValue' }"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 负责人 ID、负责人姓名（均可选） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.managerid')"
                name="managerId"
              >
                <a-input-number
                  v-model:value="managerIdModel"
                  :placeholder="t('accounting.controlling.cost-center.managerIdPlaceholder')"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.managername')"
                name="managerName"
              >
                <a-input
                  v-model:value="formState.managerName"
                  :placeholder="t('accounting.controlling.cost-center.managerNamePlaceholder')"
                  :maxlength="50"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 排序号（与 `entity.costcenter.ordernum` 一致） -->
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item
                :label="t('entity.costcenter.ordernum')"
                name="orderNum"
              >
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :placeholder="t('common.form.placeholder.orderNumHint')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
          <!-- 备注（与 `TaktCostCenterCreateDto.Remark` 一致；标签走 `common.entity.remark`） -->
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
                  :placeholder="t('accounting.controlling.cost-center.remarkPlaceholder')"
                  show-count
                  :maxlength="500"
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
 * 成本中心表单脚本：与列表页弹窗内 `ref` 配合；
 * 父组件通过 `validate`、`getValues`、`resetFields` 完成提交流程。
 */
import { reactive, watch, ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { CostCenter, CostCenterCreate } from '@/types/accounting/controlling/cost-center'
import TaktSelect from '@/components/business/takt-select/index.vue'
import TaktTreeSelect from '@/components/business/takt-tree-select/index.vue'

const { t } = useI18n()

/**
 * 组件入参：由父级列表页传入当前编辑行或空对象（新增）。
 */
interface Props {
  /**
   * 当前成本中心数据（编辑时含 `costCenterId` 等），与 `@/types/accounting/controlling/cost-center` 中 `CostCenter` 一致。
   */
  formData?: Partial<CostCenter>
  /**
   * 提交中状态，由父级传入；预留与父级 loading 联动。
   */
  loading?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  formData: () => ({}),
  loading: false
})

/**
 * Ant Design Vue 表单实例，用于 `validate`、`resetFields`。
 */
const formRef = ref<FormInstance>()

/**
 * 当前激活的 Tab 键；仅 `basic`。
 */
const activeTab = ref('basic')

/**
 * 估算表单项数量，用于 `takt-form-content-rows-*` 容器类名。
 */
const TOTAL_FIELDS = 14

/**
 * 表单外层纵向布局类名。
 */
const formContentClass = computed(() => (TOTAL_FIELDS >= 30 ? 'takt-form-content-rows-10' : 'takt-form-content-rows-5'))

/**
 * 表单本地状态：下拉/树选择以 `string | number | undefined` 承载 ID，提交时由 `getValues` 转为 DTO 数值类型。
 */
interface CostCenterFormState {
  costCenterCode: string
  costCenterName: string
  parentId: string | number | undefined
  costCenterType: number | undefined
  deptId: string
  managerId: number | undefined
  managerName: string
  orderNum: number
  remark: string
}

/**
 * 新增态默认值，字段与 `CostCenterCreate` 对应（后端 `TaktCostCenterCreateDto`）。
 *
 * @returns {CostCenterFormState} 空表单初始对象
 */
function createEmptyCostCenterForm(): CostCenterFormState {
  return {
    costCenterCode: '',
    costCenterName: '',
    parentId: undefined,
    costCenterType: 0,
    deptId: '',
    managerId: undefined,
    managerName: '',
    orderNum: 0,
    remark: ''
  }
}

/** 表单模型。 */
const formState = reactive<CostCenterFormState>(createEmptyCostCenterForm())

/**
 * `a-input-number` 与 `formState.managerId` 的桥接：控件清空时为 `null`，DTO 侧使用 `undefined`。
 */
const managerIdModel = computed({
  get(): number | undefined {
    return formState.managerId
  },
  set(v: number | null | undefined) {
    formState.managerId = v == null ? undefined : v
  }
})

/**
 * 成本中心类型下拉项（0/1/2 与后端 `TaktCostCenter` 注释一致）；标签来自模块静态文案。
 */
const costCenterTypeOptions = computed(() => [
  { label: t('accounting.controlling.cost-center.costCenterTypeOption0'), value: 0 },
  { label: t('accounting.controlling.cost-center.costCenterTypeOption1'), value: 1 },
  { label: t('accounting.controlling.cost-center.costCenterTypeOption2'), value: 2 }
])

/**
 * 校验规则：`name` 与 `a-form-item` 的 `name` 一致。
 */
const rules = computed<Record<string, Rule[]>>(() => ({
  costCenterCode: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.costcenter.costcentercode') }),
      trigger: 'blur'
    }
  ],
  costCenterName: [
    {
      required: true,
      message: t('common.form.placeholder.required', { field: t('entity.costcenter.costcentername') }),
      trigger: 'blur'
    }
  ]
}))

/**
 * 将表单中的 ID 类字段解析为 `number`（空或非法时按场景返回 0 或用于 `Number` 转换）。
 *
 * @param {string | number | undefined | null} v - 选择器或输入原始值
 * @returns {number} 非负整数或 0
 */
function num(v: string | number | undefined | null): number {
  if (v === undefined || v === null || v === '') return 0
  const n = Number(v)
  return Number.isFinite(n) ? n : 0
}

/**
 * 同步父组件 `formData`：有数据则填充，否则重置为 `createEmptyCostCenterForm()`。
 */
watch(
  () => props.formData,
  (newData) => {
    if (newData && Object.keys(newData).length > 0) {
      Object.assign(formState, {
        costCenterCode: newData.costCenterCode ?? '',
        costCenterName: newData.costCenterName ?? '',
        parentId:
          newData.parentId != null && newData.parentId !== undefined ? String(newData.parentId) : undefined,
        costCenterType: newData.costCenterType ?? 0,
        deptId: newData.deptId != null ? String(newData.deptId) : '',
        managerId: newData.managerId != null ? Number(newData.managerId) : undefined,
        managerName: newData.managerName ?? '',
        orderNum: newData.orderNum ?? 0,
        remark: newData.remark ?? ''
      })
    } else {
      Object.assign(formState, createEmptyCostCenterForm())
    }
    activeTab.value = 'basic'
  },
  { immediate: true, deep: true }
)

/**
 * 触发表单校验；由父级在保存前 `await formRef.validate()`。
 *
 * @returns {Promise<void>}
 */
const validate = async () => {
  await formRef.value?.validate()
}

/**
 * 组装提交载荷；编辑时附带 `costCenterId`（与列表页更新接口约定一致）。
 *
 * @returns {CostCenterCreate & { costCenterId?: string }} 提交体，结构满足创建/更新 DTO
 */
const getValues = (): CostCenterCreate & { costCenterId?: string } => {
  const parentId = num(formState.parentId as string | number)
  const deptIdStr = formState.deptId ?? ''
  const deptId = deptIdStr ? Number(deptIdStr) : undefined
  const base: CostCenterCreate & { costCenterId?: string } = {
    costCenterCode: formState.costCenterCode ?? '',
    costCenterName: formState.costCenterName ?? '',
    parentId,
    costCenterType: formState.costCenterType ?? 0,
    managerId:
      formState.managerId != null && formState.managerId !== undefined ? Number(formState.managerId) : undefined,
    managerName: formState.managerName || undefined,
    deptId: deptId !== undefined && !Number.isNaN(deptId) ? deptId : undefined,
    deptName: undefined,
    orderNum: formState.orderNum ?? 0,
    remark: formState.remark || undefined
  }
  if (props.formData?.costCenterId) {
    base.costCenterId = String(props.formData.costCenterId)
  }
  return base
}

/**
 * 重置表单与本地 `formState`。
 *
 * @returns {void}
 */
const resetFields = () => {
  formRef.value?.resetFields()
  Object.assign(formState, createEmptyCostCenterForm())
  activeTab.value = 'basic'
}

/**
 * 供父级 `ref` 调用：`validate()`、`getValues()`、`resetFields()`。
 */
defineExpose({ validate, getValues, resetFields })
</script>

<style scoped lang="less">
/* 本表单无局部样式；与 leave-form 的 style 结构一致 */
</style>
