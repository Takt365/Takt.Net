<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/workflow/instance -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：流程实例管理页面，包含全部实例列表、查询、导出、详情、更新与删除 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="workflow-instance">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.flowinstance.instancecode'), t('entity.flowinstance.processkey')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      update-permission="workflow:instance:update"
      delete-permission="workflow:instance:delete"
      export-permission="workflow:instance:export"
      :show-create="false"
      :show-update="true"
      :show-delete="true"
      :show-refresh="true"
      :show-export="true"
      :show-fullscreen="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :update-disabled="!selectedRow || selectedRows.length !== 1"
      :delete-disabled="selectedRows.length === 0"
      :refresh-loading="loading"
      @update="handleUpdate"
      @delete="handleDelete"
      @refresh="handleRefresh"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
    />

    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getInstanceId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'instanceStatus'">
          <a-tag :color="statusColor((record as FlowInstance).instanceStatus)">
            {{ statusText((record as FlowInstance).instanceStatus) }}
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
      v-model:open="detailVisible"
      :title="t('workflow.instance.detailTitle')"
      width="640px"
      :footer="detail?.canUndoVerify ? undefined : null"
      :cancel-text="t('common.button.cancel')"
      :ok-text="t('workflow.instance.undoVerify')"
      @cancel="detailVisible = false"
      @ok="detail?.canUndoVerify ? handleUndoVerify() : undefined"
    >
      <InstanceForm
        :detail="detail"
        @refresh="reloadInstanceDetail"
      />
    </TaktModal>

    <!-- 挂起 -->
    <TaktModal
      v-model:open="suspendVisible"
      :title="t('workflow.instance.suspend')"
      @ok="submitSuspend"
      @cancel="currentSuspendInstance = null; suspendReason = ''"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('workflow.instance.suspendReason')">
          <a-textarea
            v-model:value="suspendReason"
            :rows="3"
            :placeholder="t('workflow.instance.suspendReasonPlaceholder')"
          />
        </a-form-item>
      </a-form>
    </TaktModal>
    <!-- 终止 -->
    <TaktModal
      v-model:open="terminateVisible"
      :title="t('workflow.instance.terminate')"
      @ok="submitTerminate"
      @cancel="currentTerminateInstance = null; terminateReason = ''"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('workflow.instance.terminateReason')">
          <a-textarea
            v-model:value="terminateReason"
            :rows="3"
            :placeholder="t('workflow.instance.terminateReasonPlaceholder')"
          />
        </a-form-item>
      </a-form>
    </TaktModal>
    <!-- 编辑实例（流程标题与表单数据） -->
    <TaktModal
      v-model:open="updateVisible"
      :title="t('common.button.edit') + t('entity.flowinstance._self')"
      :confirm-loading="updateLoading"
      @ok="handleUpdateSubmit"
      @cancel="updateVisible = false"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('entity.flowinstance.processtitle')">
          <a-input
            v-model:value="updateProcessTitle"
            :placeholder="t('common.form.placeholder.required', { field: t('entity.flowinstance.processtitle') })"
          />
        </a-form-item>
        <a-form-item :label="t('entity.flowinstance.frmdata')">
          <a-textarea
            v-model:value="updateFrmData"
            :rows="6"
            :placeholder="t('workflow.instance.frmDataPlaceholder')"
          />
        </a-form-item>
      </a-form>
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.flowinstance.instancecode')">
        <a-input v-model:value="advancedQueryForm.instanceCode" />
      </a-form-item>
      <a-form-item :label="t('entity.flowinstance.processkey')">
        <a-input v-model:value="advancedQueryForm.processKey" />
      </a-form-item>
      <a-form-item :label="t('entity.flowinstance.instancestatus')">
        <a-select
          v-model:value="advancedQueryForm.instanceStatus"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.flowinstance.instancestatus') })"
          allow-clear
        >
          <a-select-option
            v-for="(label, val) in statusOptions"
            :key="val"
            :value="Number(val)"
          >
            {{ label }}
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
 * 流程实例列表页：全部实例查询、分页、详情、更新、撤回、挂起、恢复、终止、撤销审批、删除、导出。
 */
import { ref, onMounted, computed } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getFlowInstanceList, getFlowInstanceById, revoke, updateFlowInstance, deleteFlowInstanceById, deleteFlowInstanceBatch, exportFlowInstanceData, suspend, resume, terminate, undoVerification } from '@/api/workflow/instance'
import { useUserStore } from '@/stores/identity/user'
import InstanceForm from './components/instance-form.vue'
import type { FlowInstance, FlowInstanceDetail, FlowInstanceQuery } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'
import { RiEyeLine, RiArrowGoBackLine, RiEditLine, RiDeleteBinLine, RiPauseLine, RiPlayLine, RiStopLine } from '@remixicon/vue'

const { t } = useI18n()
const loading = ref(false)
const queryKeyword = ref('')
const queryStatus = ref<number | undefined>(undefined)
const dataSource = ref<FlowInstance[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FlowInstance | null>(null)
const selectedRows = ref<FlowInstance[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const detailVisible = ref(false)
const detail = ref<FlowInstanceDetail | null>(null)
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ instanceCode?: string; processKey?: string; instanceStatus?: number }>({ instanceCode: '', processKey: '', instanceStatus: undefined })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])
const updateVisible = ref(false)
const updateProcessTitle = ref('')
const updateFrmData = ref('')
const updateLoading = ref(false)
const currentEditInstance = ref<FlowInstance | null>(null)
const suspendVisible = ref(false)
const suspendReason = ref('')
const currentSuspendInstance = ref<FlowInstance | null>(null)
const terminateVisible = ref(false)
const terminateReason = ref('')
const currentTerminateInstance = ref<FlowInstance | null>(null)

type FlowInstanceColumn = {
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

function getColumnKey(column: FlowInstanceColumn): string {
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

const userStore = useUserStore()
const currentUserId = computed(() => userStore.userInfo?.userId ?? '')

const statusOptions = computed(() => ({
  0: t('workflow.instance.status.0'),
  1: t('workflow.instance.status.1'),
  2: t('workflow.instance.status.2'),
  3: t('workflow.instance.status.3'),
  4: t('workflow.instance.status.4')
}))

const getInstanceId = (record: FlowInstance): string => (record?.instanceId != null ? String(record.instanceId) : '')

/** 表格列：与 @/types/workflow/instance FlowInstance 字段一致（列表展示用，不含 frmData 等大字段） */
const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'instanceId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: t('entity.flowinstance.instancecode'),
    dataIndex: 'instanceCode',
    key: 'instanceCode',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowinstance.processkey'),
    dataIndex: 'processKey',
    key: 'processKey',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowinstance.processname'),
    dataIndex: 'processName',
    key: 'processName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.flowinstance.processtitle'),
    dataIndex: 'processTitle',
    key: 'processTitle',
    width: 140,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.flowinstance.instancestatus'),
    dataIndex: 'instanceStatus',
    key: 'instanceStatus',
    width: 90
  },
  {
    title: t('entity.flowinstance.currentnodename'),
    dataIndex: 'currentNodeName',
    key: 'currentNodeName',
    width: 100,
    ellipsis: true
  },
  {
    title: t('entity.flowinstance.startusername'),
    dataIndex: 'startUserName',
    key: 'startUserName',
    width: 100
  },
  {
    title: t('entity.flowinstance.starttime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 160
  },
  {
    title: t('entity.flowinstance.endtime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 160
  },
  CreateActionColumn({
    actions: [
      {
        key: 'detail',
        label: t('common.button.detail'),
        shape: 'plain',
        icon: RiEyeLine,
        permission: 'workflow:instance:detail',
        onClick: (_record: FlowInstance) => showDetail(_record)
      },
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'workflow:instance:update',
        visible: (record: FlowInstance) => (record.instanceStatus === 0 || record.instanceStatus === 5) && isStarter(record),
        onClick: (_record: FlowInstance) => handleEditInstance(_record)
      },
      {
        key: 'revoke',
        label: t('common.button.revoke'),
        shape: 'plain',
        icon: RiArrowGoBackLine,
        permission: 'workflow:instance:revoke',
        visible: (record: FlowInstance) => record.instanceStatus === 0 && isStarter(record),
        onClick: (_record: FlowInstance) => handleRevoke(_record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'workflow:instance:delete',
        onClick: (_record: FlowInstance) => handleDeleteOne(_record)
      },
      {
        key: 'suspend',
        label: t('workflow.instance.suspend'),
        shape: 'plain',
        icon: RiPauseLine,
        permission: 'workflow:instance:suspend',
        visible: (record: FlowInstance) => record.instanceStatus === 0,
        onClick: (_record: FlowInstance) => openSuspendModal(_record)
      },
      {
        key: 'resume',
        label: t('workflow.instance.resume'),
        shape: 'plain',
        icon: RiPlayLine,
        permission: 'workflow:instance:resume',
        visible: (record: FlowInstance) => record.instanceStatus === 3,
        onClick: (_record: FlowInstance) => handleResume(_record)
      },
      {
        key: 'terminate',
        label: t('workflow.instance.terminate'),
        shape: 'plain',
        icon: RiStopLine,
        permission: 'workflow:instance:terminate',
        visible: (record: FlowInstance) => record.instanceStatus === 0 || record.instanceStatus === 3,
        onClick: (_record: FlowInstance) => openTerminateModal(_record)
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
    const colKey = getColumnKey(col as FlowInstanceColumn)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: FlowInstance[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: FlowInstance, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getInstanceId(selectedRow.value) === getInstanceId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: FlowInstance[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: FlowInstance) => ({
  onClick: () => {
    const key = getInstanceId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getInstanceId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/** 实例状态码转展示文案 */
function statusText(s: number): string {
  return t(`workflow.instance.status.${s}`) || t('workflow.instance.status.unknown')
}

/** 实例状态对应 Tag 颜色 */
function statusColor(s: number): string {
  const m: Record<number, string> = { 0: 'processing', 1: 'success', 2: 'error', 3: 'warning', 4: 'default' }
  return m[s] ?? 'default'
}

/** 判断当前用户是否为该实例发起人 */
function isStarter(r: FlowInstance): boolean {
  return String(r.startUserId) === String(currentUserId.value)
}

/** 拉取流程实例列表（分页），结果写入 dataSource 与 total */
async function loadData() {
  try {
    loading.value = true
    const res = (await getFlowInstanceList({
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      processKey: queryKeyword.value || advancedQueryForm.value.processKey || undefined,
      instanceCode: queryKeyword.value || advancedQueryForm.value.instanceCode || undefined,
      instanceStatus: advancedQueryForm.value.instanceStatus
    }))
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

/** 查询：页码置 1 并重新拉取 */
function handleSearch() {
  currentPage.value = 1
  loadData()
}

/** 重置关键词、状态、高级查询并重新拉取 */
function handleReset() {
  queryKeyword.value = ''
  queryStatus.value = undefined
  advancedQueryForm.value = { instanceCode: '', processKey: '' }
  currentPage.value = 1
  loadData()
}

/** 打开高级查询弹窗 */
function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

/** 高级查询提交：应用条件、拉取并关闭弹窗 */
function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/** 高级查询重置：清空高级查询表单 */
function handleAdvancedQueryReset() {
  advancedQueryForm.value = { instanceCode: '', processKey: '' }
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

/** 刷新：重新拉取列表 */
function handleRefresh() {
  loadData()
}

/** 导出流程实例为 Excel 并触发下载 */
async function handleExport() {
  try {
    loading.value = true
    const query: FlowInstanceQuery = {
      pageIndex: 1,
      pageSize: 99999,
      processKey: queryKeyword.value || advancedQueryForm.value.processKey || undefined,
      instanceCode: queryKeyword.value || advancedQueryForm.value.instanceCode || undefined,
      instanceStatus: advancedQueryForm.value.instanceStatus
    }
    const blob = await exportFlowInstanceData(query)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.flowinstance._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess'))
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.exportFail')))
  } finally {
    loading.value = false
  }
}

/** 表格变化（排序等）占位 */
function handleTableChange(_pagination: unknown, _filters: unknown, sorter: unknown) {
  const sorterInfo = getSorterInfo(sorter)
  if (sorterInfo.order) {
    // 如需服务端排序可在此处理
  }
}

/** 分页变化时更新并拉取 */
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

/** 列宽拖拽后更新对应列的 width */
function handleResizeColumn(w: number, col: FlowInstanceColumn) {
  const column = columns.value.find((c) => getColumnKey(c as FlowInstanceColumn) === getColumnKey(col))
  if (column) (column as FlowInstanceColumn).width = w
}

/** 拉取实例详情并打开详情弹窗 */
async function showDetail(record: FlowInstance) {
  try {
    detail.value = await getFlowInstanceById(record.instanceId)
    detailVisible.value = true
  } catch {
    message.error(t('common.msg.loadFail'))
  }
}

/** 加签/减签后刷新当前详情（保持弹窗打开） */
async function reloadInstanceDetail() {
  if (!detail.value?.instanceId) return
  try {
    detail.value = await getFlowInstanceById(detail.value.instanceId)
  } catch {
    message.error(t('common.msg.loadFail'))
  }
}

/** 撤销当前节点审批：调用 undoVerification 后关闭详情并刷新列表 */
async function handleUndoVerify() {
  if (!detail.value?.instanceId) return
  try {
    loading.value = true
    await undoVerification({ flowInstanceId: detail.value.instanceId })
    message.success(t('workflow.instance.undoVerifySuccess'))
    detailVisible.value = false
    detail.value = null
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    loading.value = false
  }
}

/** 打开挂起弹窗并设置当前实例 */
function openSuspendModal(record: FlowInstance) {
  currentSuspendInstance.value = record
  suspendReason.value = ''
  suspendVisible.value = true
}

/** 提交挂起：调用 suspend 接口后关闭弹窗并刷新列表 */
async function submitSuspend() {
  if (!currentSuspendInstance.value) return
  try {
    loading.value = true
    await suspend({ flowInstanceId: currentSuspendInstance.value.instanceId, reason: suspendReason.value || undefined })
    message.success(t('workflow.instance.suspendSuccess'))
    suspendVisible.value = false
    currentSuspendInstance.value = null
    suspendReason.value = ''
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    loading.value = false
  }
}

/** 恢复：二次确认后调用 resume 接口并刷新列表 */
function handleResume(record: FlowInstance) {
  const name = record.processTitle || record.instanceCode
  Modal.confirm({
    centered: true,
    title: t('workflow.instance.resume'),
    content: t('workflow.instance.confirmResume', { name }),
    okText: t('common.button.ok'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await resume({ flowInstanceId: record.instanceId })
        message.success(t('workflow.instance.resumeSuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, t('common.msg.operateFail')))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 打开终止弹窗并设置当前实例 */
function openTerminateModal(record: FlowInstance) {
  currentTerminateInstance.value = record
  terminateReason.value = ''
  terminateVisible.value = true
}

/** 提交终止：调用 terminate 接口后关闭弹窗并刷新列表 */
async function submitTerminate() {
  if (!currentTerminateInstance.value) return
  try {
    loading.value = true
    await terminate({ flowInstanceId: currentTerminateInstance.value.instanceId, reason: terminateReason.value || undefined })
    message.success(t('workflow.instance.terminateSuccess'))
    terminateVisible.value = false
    currentTerminateInstance.value = null
    terminateReason.value = ''
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    loading.value = false
  }
}

/** 撤回：二次确认后调用 revoke 接口并刷新列表 */
function handleRevoke(record: FlowInstance) {
  const name = record.processTitle || record.instanceCode
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmAction', { action: t('common.button.revoke') }),
    content: t('workflow.instance.confirmRevoke', { name }),
    okText: t('common.button.ok'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await revoke(record.instanceCode)
        message.success(t('common.msg.actionSuccess', { action: t('common.button.revoke') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, t('common.msg.operateFail')))
      } finally {
        loading.value = false
      }
    }
  })
}

/** 更新：若有选中行则打编辑弹窗，否则提示请选择 */
function handleUpdate() {
  if (selectedRow.value) handleEditInstance(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.flowinstance._self') }))
}

/** 打开编辑弹窗：回填标题与 frmData，拉取最新详情 */
async function handleEditInstance(record: FlowInstance) {
  currentEditInstance.value = record
  updateProcessTitle.value = record.processTitle ?? ''
  updateFrmData.value = record.frmData ?? ''
  try {
    const d = await getFlowInstanceById(record.instanceId)
    if (d) {
      updateProcessTitle.value = d.processTitle ?? ''
      updateFrmData.value = d.frmData ?? ''
    }
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.loadFail')))
  }
  updateVisible.value = true
}

/** 编辑提交：调用 update 接口后关闭弹窗并刷新列表 */
async function handleUpdateSubmit() {
  if (!currentEditInstance.value) return
  try {
    updateLoading.value = true
    await updateFlowInstance({
      id: currentEditInstance.value.instanceId,
      processTitle: updateProcessTitle.value?.trim() || undefined,
      frmData: updateFrmData.value?.trim() || undefined
    })
    message.success(t('common.msg.updateSuccess'))
    updateVisible.value = false
    currentEditInstance.value = null
    loadData()
  } catch (error: unknown) {
    message.error(getErrorMessage(error, t('common.msg.operateFail')))
  } finally {
    updateLoading.value = false
  }
}

/** 单条删除：二次确认后 deleteById 并刷新列表 */
function handleDeleteOne(record: FlowInstance) {
  const name = record.processTitle || record.instanceCode
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.flowinstance._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFlowInstanceById(record.instanceId)
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
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.flowinstance._self') }))
    return
  }
  Modal.confirm({
    centered: true,
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t('entity.flowinstance._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFlowInstanceBatch(selectedRows.value.map(r => r.instanceId))
        message.success(t('common.msg.deleteSuccess'))
        selectedRows.value = []
        selectedRowKeys.value = []
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

onMounted(() => loadData())
</script>

<style scoped lang="less">
.workflow-instance {
  padding: 16px;

  &__toolbar {
    display: flex;
    align-items: center;
    gap: 8px;
    margin-bottom: 12px;
  }

  &__status-select {
    width: 120px;
  }
}
</style>
