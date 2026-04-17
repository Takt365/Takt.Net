<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/form -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程表单管理页面，包含列表、查询、新增、编辑、删除及表单设计 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-form">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowform.formcode'), t('entity.flowform.formname')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="workflow:form:create"
      update-permission="workflow:form:update"
      delete-permission="workflow:form:delete"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-refresh="true"
      :show-fullscreen="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @refresh="handleRefresh"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
    />

    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getFormId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'formCategory'">
          <a-tag>{{ formatCategory(record.formCategory) }}</a-tag>
        </template>
        <template v-else-if="column.key === 'formType'">
          <a-tag color="blue">
            {{ formatType(record.formType) }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'isDatasource'">
          {{ record[column.key as keyof FlowForm] === 1 ? t('common.button.yes') : t('common.button.no') }}
        </template>
        <template v-else-if="column.key === 'formStatus'">
          <a-tag :color="record.formStatus === 1 ? 'green' : record.formStatus === 2 ? 'red' : 'default'">
            {{ formatStatus(record.formStatus) }}
          </a-tag>
        </template>
      </template>
    </TaktSingleTable>

    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <FormForm
        ref="formFormRef"
        :form="form"
      />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.flowform.formcode')">
        <a-input v-model:value="advancedQueryForm.formCode" />
      </a-form-item>
      <a-form-item :label="t('entity.flowform.formname')">
        <a-input v-model:value="advancedQueryForm.formName" />
      </a-form-item>
      <a-form-item :label="t('entity.flowform.formstatus')">
        <a-select
          v-model:value="advancedQueryForm.formStatus"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.flowform.formstatus') })"
          allow-clear
        >
          <a-select-option :value="0">
            {{ t('common.button.draft') }}
          </a-select-option>
          <a-select-option :value="1">
            {{ t('common.button.publish') }}
          </a-select-option>
          <a-select-option :value="2">
            {{ t('common.button.disable') }}
          </a-select-option>
        </a-select>
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'id'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
/**
 * 流程表单列表页：查询、分页、新增、编辑、删除；弹窗内使用 FormForm 编辑表单与 formConfig。
 */
import { ref, reactive, onMounted, computed } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import TaktQueryBar from '@/components/business/takt-query-bar/index.vue'
import FormForm from './components/form-form.vue'
import {
  getFlowFormList,
  getFlowFormById,
  createFlowForm,
  updateFlowForm,
  deleteFlowFormById,
  deleteFlowFormBatch
} from '@/api/workflow/form'
import type {
  FlowForm,
  FlowFormQuery,
  FlowFormCreate,
  FlowFormUpdate
} from '@/types/workflow/form'
import type { TaktPagedResult } from '@/types/common'
import { RiEditLine, RiDeleteBinLine, RiBrushLine } from '@remixicon/vue'

const { t } = useI18n()
const loading = ref(false)
const queryKeyword = ref('')
const dataSource = ref<FlowForm[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowForm | null>(null)
const selectedRows = ref<FlowForm[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const formFormRef = ref<InstanceType<typeof FormForm> | null>(null)
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ formCode?: string; formName?: string; formStatus?: number }>({ formCode: '', formName: '', formStatus: undefined })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type FlowFormColumn = {
  key?: string | number
  dataIndex?: string | number
  title?: string | number
  width?: number
}

type TableSorterInfo = {
  field?: string
  order?: string
}

function getErrorMessage(error: unknown, fallback: string): string {
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const message = (error as { message?: unknown }).message
    if (typeof message === 'string' && message.trim()) return message
  }
  return fallback
}

function getColumnKey(column: FlowFormColumn): string {
  const key = column.key ?? column.dataIndex ?? column.title
  return key != null ? String(key) : ''
}

function getSorterInfo(sorter: unknown): TableSorterInfo {
  if (typeof sorter !== 'object' || sorter === null) return {}
  const sorterObj = sorter as { field?: unknown; order?: unknown }
  return {
    field: typeof sorterObj.field === 'string' ? sorterObj.field : undefined,
    order: typeof sorterObj.order === 'string' ? sorterObj.order : undefined
  }
}

const form = reactive<FlowFormCreate & { formId?: string }>({
  formCode: '',
  formName: '',
  formCategory: 0,
  formType: 0,
  formConfig: '',
  formTemplate: '',
  formVersion: 'v1.0.0',
  isDatasource: 0,
  relatedDataBaseName: '',
  relatedTableName: '',
  relatedFormField: '',
  orderNum: 0,
  formStatus: 0
})

const getFormId = (record: FlowForm): string => (record?.formId != null ? String(record.formId) : '')

/** 表格列：与 @/types/workflow/form FlowForm 字段一致（列表展示用，不含 formConfig/formTemplate 等大字段） */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'formId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: t('entity.flowform.formcode'),
    dataIndex: 'formCode',
    key: 'formCode',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowform.formname'),
    dataIndex: 'formName',
    key: 'formName',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowform.formcategory'),
    dataIndex: 'formCategory',
    key: 'formCategory',
    width: 100
  },
  {
    title: t('entity.flowform.formtype'),
    dataIndex: 'formType',
    key: 'formType',
    width: 120
  },
  {
    title: t('entity.flowform.formversion'),
    dataIndex: 'formVersion',
    key: 'formVersion',
    width: 90
  },
  {
    title: t('entity.flowform.isdatasource'),
    dataIndex: 'isDatasource',
    key: 'isDatasource',
    width: 80
  },
  {
    title: t('entity.flowform.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('entity.flowform.formstatus'),
    dataIndex: 'formStatus',
    key: 'formStatus',
    width: 90
  },
  CreateActionColumn({
    actions: [
      {
        key: 'edit',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'workflow:form:update',
        onClick: (_record: FlowForm, _index: number) => handleEdit(_record)
      },
      {
        key: 'design',
        label: t('common.button.design'),
        shape: 'plain',
        icon: RiBrushLine,
        permission: 'workflow:form:update',
        onClick: (_record: FlowForm, _index: number) => handleDesign(_record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'workflow:form:delete',
        onClick: (_record: FlowForm, _index: number) => handleDeleteOne(_record)
      }
    ]
  })
])

const mergedColumns = computed<TableColumnsType>(() => mergeDefaultColumns(columns.value, t, true))
const displayColumns = computed<TableColumnsType>(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col) => {
    const colKey = getColumnKey(col as FlowFormColumn)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: FlowForm[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: FlowForm, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getFormId(selectedRow.value) === getFormId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: FlowForm[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: FlowForm) => ({
  onClick: () => {
    const key = getFormId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getFormId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/** 表单分类数字转展示文案（0 通用 / 1 业务 / 2 系统） */
function formatCategory(category: number): string {
  if (category === 1) return t('workflow.form.category.business')
  if (category === 2) return t('workflow.form.category.system')
  return t('workflow.form.category.general')
}

/** 表单类型数字转展示文案（0 动态 / 1 静态 / 2 自定义） */
function formatType(type: number): string {
  if (type === 1) return t('workflow.form.type.static')
  if (type === 2) return t('workflow.form.type.custom')
  return t('workflow.form.type.dynamic')
}

/** 表单状态数字转展示文案（0 草稿 / 1 已发布 / 2 已停用） */
function formatStatus(status: number): string {
  if (status === 1) return t('common.button.publish')
  if (status === 2) return t('common.button.disable')
  return t('common.button.draft')
}

/** 拉取流程表单列表（分页），结果写入 dataSource 与 total */
async function loadData() {
  try {
    loading.value = true
    const query: FlowFormQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      formCode: queryKeyword.value || advancedQueryForm.value.formCode || undefined,
      formName: queryKeyword.value || advancedQueryForm.value.formName || undefined,
      formStatus: advancedQueryForm.value.formStatus
    }
    const res = (await getFlowFormList(query))
    dataSource.value = res.data ?? []
    total.value = res.total ?? 0
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.loadFail')))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/** 查询：页码置 1 并重新拉取列表 */
function handleSearch() {
  currentPage.value = 1
  loadData()
}

/** 重置：清空关键词与高级查询条件，页码置 1 并重新拉取 */
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { formCode: '', formName: '', formStatus: undefined }
  currentPage.value = 1
  loadData()
}

/** 打开高级查询弹窗 */
function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

/** 高级查询提交：应用条件、页码置 1、拉取列表并关闭弹窗 */
function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/** 高级查询重置：清空高级查询表单 */
function handleAdvancedQueryReset() {
  advancedQueryForm.value = { formCode: '', formName: '', formStatus: undefined }
}

/** 打开列设置弹窗 */
function handleColumnSetting() {
  columnSettingVisible.value = true
}

/** 列设置勾选变化时同步 visibleColumnKeys */
function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map(k => String(k))
}

/** 列设置重置：清空可见列 key */
function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

/** 表格变化（排序等），当前仅占位 */
function handleTableChange(_pagination: unknown, _filters: unknown, sorter: unknown) {
  const sorterInfo = getSorterInfo(sorter)
  if (sorterInfo.order) {
    // 如需服务端排序可在此处理
  }
}

/** 分页页码或每页条数变化时重新拉取 */
function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/** 每页条数变化：重置到第 1 页并拉取 */
function handlePaginationSizeChange(_current: number, size: number) {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/** 刷新：重新拉取列表 */
function handleRefresh() {
  loadData()
}

/** 列宽拖拽后更新对应列的 width */
function handleResizeColumn(w: number, col: FlowFormColumn) {
  const column = columns.value.find((c) => getColumnKey(c as FlowFormColumn) === getColumnKey(col))
  if (column) (column as FlowFormColumn).width = w
}

/** 重置表单对象为初始值。仅用于「打开新增」前，不在关闭弹窗时调用。 */
function resetForm() {
  form.formId = undefined
  form.formCode = ''
  form.formName = ''
  form.formCategory = 0
  form.formType = 0
  form.formConfig = ''
  form.formTemplate = ''
  form.formVersion = 'v1.0.0'
  form.isDatasource = 0
  form.relatedDataBaseName = ''
  form.relatedTableName = ''
  form.relatedFormField = ''
  form.orderNum = 0
  form.formStatus = 0
}

/** 新增：仅此时重置表单数据；打开弹窗后 nextTick 再重置子组件步骤与内部状态。 */
function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.flowform._self')
  resetForm()
  formVisible.value = true
  nextTick(() => formFormRef.value?.resetSteps?.())
}

/** 编辑：拉取详情回填 form，再打开弹窗，nextTick 后重置子组件步骤与内部状态（子组件会按 form 重新拉取数据源/表/列）。 */
async function handleEdit(record: FlowForm) {
  formTitle.value = t('common.button.edit') + t('entity.flowform._self')
  formLoading.value = true
  try {
    const detail = await getFlowFormById(String(record.formId))
    form.formId = detail.formId
    form.formCode = detail.formCode
    form.formName = detail.formName
    form.formCategory = detail.formCategory
    form.formType = detail.formType
    form.formConfig = detail.formConfig?.trim() ? detail.formConfig : ''
    form.formTemplate = detail.formTemplate ?? ''
    form.formVersion = detail.formVersion
    form.isDatasource = detail.isDatasource
    form.relatedDataBaseName = detail.relatedDataBaseName ?? ''
    form.relatedTableName = detail.relatedTableName ?? ''
    form.relatedFormField = detail.relatedFormField ?? ''
    form.orderNum = detail.orderNum
    form.formStatus = detail.formStatus
    formVisible.value = true
    nextTick(() => formFormRef.value?.resetSteps?.())
  } catch {
    message.error(t('workflow.form.loadDetailFailed'))
  } finally {
    formLoading.value = false
  }
}

/** 设计：复用编辑逻辑打开表单设计 */
function handleDesign(record: FlowForm) {
  handleEdit(record)
}

/** 更新：若有选中行则编辑该行，否则提示请选择 */
function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.flowform._self') }))
}

/** 单条删除：二次确认后调用 deleteById 并刷新列表 */
function handleDeleteOne(record: FlowForm) {
  const name = record.formName || getFormId(record)
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.flowform._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFlowFormById(record.formId)
        message.success(t('common.msg.deleteSuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, t('common.msg.deleteFail')))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 批量删除：无选中则提示；有选中则二次确认后 deleteBatch 并刷新 */
function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.flowform._self') }))
    return
  }
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.flowform._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFlowFormBatch(selectedRowKeys.value.map(k => String(k)))
        message.success(t('common.msg.deleteSuccess'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, t('common.msg.deleteFail')))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 关闭表单弹窗：不在此处重置表单或步骤，下次打开为新增会 resetForm + resetSteps，为编辑会回填 + resetSteps。 */
function handleFormCancel() {
  formVisible.value = false
}

/** 提交：校验步骤、同步设计器、调用 create/update 后关闭弹窗并刷新列表 */
async function handleFormSubmit() {
  const valid = await formFormRef.value?.validateAllSteps?.()
  if (valid === false) {
    message.warning(t('workflow.form.step.completeRequired'))
    return
  }
  try {
    formLoading.value = true
    formFormRef.value?.syncDesignerToModel()
    const payload: FlowFormCreate | FlowFormUpdate = {
      formCode: form.formCode.trim(),
      formName: form.formName.trim(),
      formCategory: form.formCategory,
      formType: form.formType,
      formConfig: form.formConfig?.trim() || undefined,
      formTemplate: form.formTemplate?.trim() || undefined,
      formVersion: form.formVersion?.trim() || 'v1.0.0',
      isDatasource: form.isDatasource,
      relatedDataBaseName: form.relatedDataBaseName?.trim() || undefined,
      relatedTableName: form.relatedTableName?.trim() || undefined,
      relatedFormField: form.relatedFormField?.trim() || undefined,
      orderNum: form.orderNum ?? 0,
      formStatus: form.formStatus
    } as FlowFormCreate | FlowFormUpdate
    if (form.formId) {
      (payload as FlowFormUpdate).formId = form.formId
      await updateFlowForm(form.formId, payload as FlowFormUpdate)
      message.success(t('common.msg.updateSuccess'))
    } else {
      await createFlowForm(payload as FlowFormCreate)
      message.success(t('common.msg.createSuccess'))
    }
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    formLoading.value = false
  }
}

onMounted(() => {
  loadData()
})
</script>

<style scoped lang="less">
.workflow-form {
  padding: 16px;
}
</style>
