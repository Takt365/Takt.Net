<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/statistics/logging/oper-log -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-04-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：操作日志页面，包含关键字查询、列表分页、批量删除、导出与列设置 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="logging-oper-log">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="searchPlaceholder"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      delete-permission="statistics:logging:operlog:delete"
      export-permission="statistics:logging:operlog:export"
      :show-create="false"
      :show-update="false"
      :show-delete="true"
      :show-import="false"
      :show-export="true"
      :show-advanced-query="false"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :delete-disabled="selectedRows.length === 0"
      :export-loading="loading"
      :refresh-loading="loading"
      @delete="handleDelete"
      @export="handleExport"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getOperLogId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'operStatus'">
          <a-tag :color="record.operStatus === 0 ? 'success' : 'error'">
            {{ record.operStatus === 0 ? t('common.state.success') : t('common.state.failed') }}
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
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'operLogId'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getOperLogList, deleteOperLogById, deleteOperLogBatch, exportOperLogData } from '@/api/statistics/logging/oper-log'
import type { OperLog, OperLogQuery } from '@/types/statistics/logging/oper-log'
import { logger } from '@/utils/logger'
import { RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<OperLog[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<OperLog | null>(null)
const selectedRows = ref<OperLog[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type OperLogColumn = { key?: string | number; dataIndex?: string | number; title?: string | number; width?: string | number }
type TableSorterInfo = { field?: string; order?: string }

const searchPlaceholder = computed(
  () => t('common.form.placeholder.required', { field: [t('entity.operlog.username'), t('entity.operlog.opermodule'), t('entity.operlog.opermethod')].join('、') }) + t('common.button.query')
)

const getErrorMessage = (error: unknown, fallbackKey: string): string => {
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const messageText = (error as { message?: unknown }).message
    if (typeof messageText === 'string' && messageText.trim()) return messageText
  }
  return t(fallbackKey)
}

const getColumnKey = (column: OperLogColumn): string => {
  const key = column.key ?? column.dataIndex ?? column.title
  return key != null ? String(key) : ''
}

const getSorterInfo = (sorter: unknown): TableSorterInfo => {
  if (typeof sorter !== 'object' || sorter === null) return {}
  const sorterObj = sorter as { field?: unknown; order?: unknown }
  const result: TableSorterInfo = {}
  if (typeof sorterObj.field === 'string') result.field = sorterObj.field
  if (typeof sorterObj.order === 'string') result.order = sorterObj.order
  return result
}

/** TaktSingleTable 的 rowKey 回调入参为 TableRecord，与 OperLog 形参类型不兼容 */
const getOperLogId = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const id = (record as { operLogId?: unknown }).operLogId
  return id != null ? String(id) : ''
}
const getOperLogField = <K extends keyof OperLog>(record: OperLog, field: K): OperLog[K] => record[field]

onMounted(() => {
  loadData()
})

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'operLogId',
    key: 'operLogId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: OperLog }) => getOperLogField(record, 'operLogId') ?? ''
  },
  { title: t('entity.operlog.username'), dataIndex: 'userName', key: 'userName', width: 120, resizable: true, ellipsis: true },
  { title: t('entity.operlog.opermodule'), dataIndex: 'operModule', key: 'operModule', width: 120, resizable: true, ellipsis: true },
  { title: t('entity.operlog.opertype'), dataIndex: 'operType', key: 'operType', width: 90, resizable: true, ellipsis: true },
  { title: t('entity.operlog.opermethod'), dataIndex: 'operMethod', key: 'operMethod', width: 140, resizable: true, ellipsis: true },
  { title: t('entity.operlog.requestmethod'), dataIndex: 'requestMethod', key: 'requestMethod', width: 90 },
  { title: t('entity.operlog.operstatus'), dataIndex: 'operStatus', key: 'operStatus', width: 90 },
  { title: t('entity.operlog.operip'), dataIndex: 'operIp', key: 'operIp', width: 130, ellipsis: true },
  { title: t('entity.operlog.opertime'), dataIndex: 'operTime', key: 'operTime', width: 170, resizable: true, ellipsis: true },
  CreateActionColumn<OperLog>({
    actions: [
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'statistics:logging:operlog:delete',
        onClick: (record: OperLog) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): TableColumnsType => {
  // 避免 mergeDefaultColumns 与 TableColumnsType 泛型组合导致「类型实例化过深」
  const merged = mergeDefaultColumns(columns.value as never, t, true)
  return merged as TableColumnsType
})
const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  const filtered = merged.filter(col => {
    const colKey = getColumnKey(col as OperLogColumn)
    return Boolean(colKey && keysSet.has(colKey))
  })
  return filtered as TableColumnsType
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: OperLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: OperLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getOperLogId(selectedRow.value) === getOperLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: OperLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
  }
}))

const onClickRow = (record: OperLog) => ({
  onClick: () => {
    const key = getOperLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getOperLogId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: OperLogQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      KeyWords: queryKeyword.value || ''
    }
    const response = await getOperLogList(params)
    dataSource.value = response?.data ?? []
    total.value = response?.total ?? 0
  } catch (error: unknown) {
    logger.error('[OperLog] 加载数据失败:', error)
    message.error(getErrorMessage(error, 'common.msg.loadfail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => { currentPage.value = 1; loadData() }
const handleReset = () => { queryKeyword.value = ''; currentPage.value = 1; loadData() }
const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: unknown) => {
  const sorterInfo = getSorterInfo(sorter)
  if (sorterInfo.order) logger.debug('[OperLog] 排序:', sorterInfo.field, sorterInfo.order)
}
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}
const handleResizeColumn = (w: number, col: OperLogColumn) => {
  const column = columns.value.find((c) => getColumnKey(c as OperLogColumn) === getColumnKey(col))
  if (column) (column as OperLogColumn).width = w
}

const handleDeleteOne = (record: OperLog) => {
  const name = getOperLogField(record, 'userName') || getOperLogId(record)
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.operlog._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteOperLogById(getOperLogId(record))
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, 'common.msg.deletefail'))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.operlog._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: t('entity.operlog._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getOperLogId(r))).filter(n => !Number.isNaN(n))
        await deleteOperLogBatch(ids)
        message.success(t('common.msg.deletesuccess'))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, 'common.msg.deletefail'))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleExport = async () => {
  try {
    loading.value = true
    const params: OperLogQuery = {
      pageIndex: 1,
      pageSize: total.value || 99999,
      KeyWords: queryKeyword.value || ''
    }
    const blob = await exportOperLogData(params, undefined, t('entity.operlog._self'))
    const ts = new Date(); const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.operlog._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess'))
  } catch (error: unknown) {
    logger.error('[OperLog] 导出失败:', error)
    message.error(getErrorMessage(error, 'common.msg.exportfail'))
  } finally {
    loading.value = false
  }
}

const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => { visibleColumnKeys.value = keys.map(k => String(k)) }
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }
const handleRefresh = () => loadData()
</script>

<style scoped lang="less">
.logging-oper-log {
  padding: 16px;
}
</style>
