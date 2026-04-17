<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/scheme -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程方案管理页面，包含列表、查询、导出、新增、编辑、删除及 ProcessContent 设计 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-scheme">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowscheme.processkey'), t('entity.flowscheme.processname')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="workflow:scheme:create"
      update-permission="workflow:scheme:update"
      delete-permission="workflow:scheme:delete"
      export-permission="workflow:scheme:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-refresh="true"
      :show-fullscreen="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :export-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @export="handleExport"
      @refresh="handleRefresh"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
    />

    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getSchemeId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'processStatus'">
          <a-tag :color="record.processStatus === 1 ? 'green' : 'default'">
            {{ formatStatus(record.processStatus) }}
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
      width="900px"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <SchemeForm
        ref="schemeFormRef"
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
      <a-form-item :label="t('entity.flowscheme.processkey')">
        <a-input v-model:value="advancedQueryForm.processKey" />
      </a-form-item>
      <a-form-item :label="t('entity.flowscheme.processname')">
        <a-input v-model:value="advancedQueryForm.processName" />
      </a-form-item>
      <a-form-item :label="t('entity.flowscheme.processstatus')">
        <a-select
          v-model:value="advancedQueryForm.processStatus"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.flowscheme.processstatus') })"
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
 * 流程方案列表页：查询、分页、导出、新增、编辑、删除；弹窗内使用 SchemeForm 编辑方案与 ProcessContent。
 */
import { ref, reactive, onMounted, computed } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { validateProcessContentForSave } from '@/utils/workflow/validate-process-content'
import SchemeForm from './components/scheme-form.vue'
import { getFlowSchemeList, getFlowSchemeById, createOrUpdateScheme, deleteFlowSchemeById, exportFlowSchemeData } from '@/api/workflow/scheme'
import type { FlowScheme, FlowSchemeQuery, FlowSchemeCreateOrUpdate } from '@/types/workflow/scheme'
import type { TaktPagedResult } from '@/types/common'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const loading = ref(false)
const queryKeyword = ref('')
const dataSource = ref<FlowScheme[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowScheme | null>(null)
const selectedRows = ref<FlowScheme[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const schemeFormRef = ref<InstanceType<typeof SchemeForm> | null>(null)
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ processKey?: string; processName?: string; processStatus?: number }>({ processKey: '', processName: '', processStatus: undefined })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type FlowSchemeColumn = {
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

function getColumnKey(column: FlowSchemeColumn): string {
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

const form = reactive<FlowSchemeCreateOrUpdate & { schemeId?: string }>({
  schemeId: undefined,
  processKey: '',
  processName: '',
  processCategory: 0,
  processStatus: 0,
  orderNum: 0,
  processContent: '',
  formId: undefined,
  formCode: undefined
})

const defaultProcessContent = '{"nodes":[{"id":"start","name":"开始","type":"start"},{"id":"node1","name":"审批","type":"userTask","assigneeType":"starter"},{"id":"end","name":"结束","type":"end"}],"edges":[{"from":"start","to":"node1"},{"from":"node1","to":"end"}]}'

const getSchemeId = (record: FlowScheme): string => (record?.schemeId != null ? String(record.schemeId) : '')

/** 表格列：与 @/types/workflow/scheme FlowScheme 字段一致（列表展示用，不含 processContent 大字段） */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'schemeId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: t('entity.flowscheme.processkey'),
    dataIndex: 'processKey',
    key: 'processKey',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowscheme.processname'),
    dataIndex: 'processName',
    key: 'processName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowscheme.processcategory'),
    dataIndex: 'processCategory',
    key: 'processCategory',
    width: 100
  },
  {
    title: t('entity.flowscheme.processversion'),
    dataIndex: 'processVersion',
    key: 'processVersion',
    width: 80
  },
  {
    title: t('entity.flowscheme.processdescription'),
    dataIndex: 'processDescription',
    key: 'processDescription',
    width: 140,
    ellipsis: true
  },
  {
    title: t('entity.flowscheme.formcode'),
    dataIndex: 'formCode',
    key: 'formCode',
    width: 100,
    ellipsis: true
  },
  {
    title: t('entity.flowscheme.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('entity.flowscheme.processstatus'),
    dataIndex: 'processStatus',
    key: 'processStatus',
    width: 90
  },
  CreateActionColumn({
    actions: [
      {
        key: 'edit',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'workflow:scheme:update',
        onClick: (_record: FlowScheme) => handleEdit(_record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'workflow:scheme:delete',
        onClick: (_record: FlowScheme) => handleDeleteOne(_record)
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
    const colKey = getColumnKey(col as FlowSchemeColumn)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: FlowScheme[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: FlowScheme, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getSchemeId(selectedRow.value) === getSchemeId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: FlowScheme[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: FlowScheme) => ({
  onClick: () => {
    const key = getSchemeId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getSchemeId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/** 流程状态数字转展示文案（0 草稿 / 1 已发布 / 2 已停用） */
function formatStatus(status: number): string {
  if (status === 1) return t('common.button.publish')
  if (status === 2) return t('common.button.disable')
  return t('common.button.draft')
}

/** 拉取流程方案列表（分页），结果写入 dataSource 与 total */
async function loadData() {
  try {
    loading.value = true
    const params: FlowSchemeQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      processKey: queryKeyword.value || advancedQueryForm.value.processKey || undefined,
      processName: queryKeyword.value || advancedQueryForm.value.processName || undefined,
      processStatus: advancedQueryForm.value.processStatus
    }
    const res = (await getFlowSchemeList(params))
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

/** 重置：清空关键词与高级查询、页码置 1 并重新拉取 */
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { processKey: '', processName: '', processStatus: undefined }
  currentPage.value = 1
  loadData()
}

/** 打开高级查询弹窗 */
function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

/** 高级查询提交：应用条件、页码置 1、拉取并关闭弹窗 */
function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/** 高级查询重置：清空高级查询表单 */
function handleAdvancedQueryReset() {
  advancedQueryForm.value = { processKey: '', processName: '', processStatus: undefined }
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

/** 导出：与列表查询条件一致，大页拉全量后下载 Excel（后端 POST TaktFlowSchemes/export，权限 workflow:scheme:export） */
async function handleExport() {
  try {
    loading.value = true
    const query: FlowSchemeQuery = {
      pageIndex: 1,
      pageSize: 10000,
      processKey: queryKeyword.value || advancedQueryForm.value.processKey || undefined,
      processName: queryKeyword.value || advancedQueryForm.value.processName || undefined,
      processStatus: advancedQueryForm.value.processStatus
    }
    const blob = await exportFlowSchemeData(query, undefined, t('entity.flowscheme._self'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.flowscheme._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportSuccess'))
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.exportFail')))
  } finally {
    loading.value = false
  }
}

/** 列宽拖拽后更新对应列的 width */
function handleResizeColumn(w: number, col: FlowSchemeColumn) {
  const column = columns.value.find((c) => getColumnKey(c as FlowSchemeColumn) === getColumnKey(col))
  if (column) (column as FlowSchemeColumn).width = w
}

/** 新增：清空 form、设置标题、打开弹窗并重置步骤 */
function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.flowscheme._self')
  form.schemeId = undefined
  form.processKey = ''
  form.processName = ''
  form.processCategory = 0
  form.processStatus = 0
  form.orderNum = 0
  form.processContent = defaultProcessContent
  form.formId = undefined
  form.formCode = undefined
  formVisible.value = true
  setTimeout(() => schemeFormRef.value?.resetSteps?.(), 0)
}

/** 编辑：调用 getFlowSchemeById（后端需 workflow:scheme:detail）；保存需 create/update */
async function handleEdit(record: FlowScheme) {
  formTitle.value = t('common.button.edit') + t('entity.flowscheme._self')
  formLoading.value = true
  try {
    const detail = await getFlowSchemeById(String(record.schemeId))
    form.schemeId = detail.schemeId
    form.processKey = detail.processKey
    form.processName = detail.processName
    form.processCategory = detail.processCategory ?? 0
    form.processStatus = detail.processStatus
    form.orderNum = detail.orderNum ?? 0
    const rawContent = (detail as { processContent?: string; ProcessContent?: string }).processContent ?? (detail as { processContent?: string; ProcessContent?: string }).ProcessContent
    const contentStr = typeof rawContent === 'string' ? rawContent.trim() : (rawContent != null ? JSON.stringify(rawContent) : '')
    form.processContent = contentStr || defaultProcessContent
    /** 与库表/接口一致的 ProcessContent：拉取后立即校验，非法时提示（条数无关，仅校验当前编辑这一条） */
    if (contentStr) {
      const pv = validateProcessContentForSave(contentStr)
      if (!pv.ok) message.warning(t('workflow.scheme.invalidProcessContent'))
    }
    form.formId = detail.formId != null ? String(detail.formId) : undefined
    form.formCode = detail.formCode ?? undefined
    formVisible.value = true
    setTimeout(() => schemeFormRef.value?.resetSteps?.(), 0)
  } catch {
    message.error(t('workflow.scheme.loadDetailFailed'))
  } finally {
    formLoading.value = false
  }
}

/** 更新：若有选中行则编辑该行，否则提示请选择 */
function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.flowscheme._self') }))
}

/** 单条删除：二次确认后 deleteById 并刷新列表 */
function handleDeleteOne(record: FlowScheme) {
  const name = record.processName || getSchemeId(record)
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.flowscheme._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFlowSchemeById(record.schemeId)
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

/** 批量删除：无选中则提示；有选中则二次确认后逐条 deleteById 并刷新 */
function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.flowscheme._self') }))
    return
  }
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.flowscheme._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        for (const row of selectedRows.value) {
          await deleteFlowSchemeById(row.schemeId)
        }
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

/** 关闭表单弹窗 */
function handleFormCancel() {
  formVisible.value = false
}

/** 提交：校验步骤、调用 createOrUpdateScheme 后关闭弹窗并刷新列表 */
async function handleFormSubmit() {
  const valid = await schemeFormRef.value?.validateAllSteps?.()
  if (valid === false) {
    message.warning(t('workflow.scheme.step.completeRequired'))
    return
  }
  const pcCheck = validateProcessContentForSave(form.processContent)
  if (!pcCheck.ok) {
    message.warning(t('workflow.scheme.invalidProcessContent'))
    return
  }
  try {
    formLoading.value = true
    const payload: FlowSchemeCreateOrUpdate = {
      schemeId: form.schemeId,
      processKey: form.processKey.trim(),
      processName: form.processName.trim(),
      processCategory: form.processCategory,
      processStatus: form.processStatus,
      orderNum: form.orderNum,
      processContent: form.processContent?.trim() || undefined,
      formId: form.formId || undefined,
      formCode: form.formCode || undefined
    }
    await createOrUpdateScheme(payload)
    message.success(t('common.msg.updateSuccess'))
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    formLoading.value = false
  }
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.workflow-scheme {
  padding: 16px;
}
</style>
