<template>
  <div class="logging-oper-log">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入用户名、操作模块或操作方法"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      delete-permission="logging:operlog:delete"
      export-permission="logging:operlog:export"
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
      ref="tableRef"
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
            {{ record.operStatus === 0 ? '成功' : '失败' }}
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
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import TaktSingleTable from '@/components/business/takt-single-table/index.vue'
import {
  getOperLogList,
  deleteOperLog,
  deleteOperLogBatch,
  exportOperLog
} from '@/api/statistics/logging/oper-log'
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
const tableRef = ref<InstanceType<typeof TaktSingleTable> | null>(null)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type OperLogColumn = {
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

function getColumnKey(column: OperLogColumn): string {
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

onMounted(() => {
  loadData()
})

const getOperLogId = (record: OperLog): string => record?.operLogId != null ? String(record.operLogId) : ''
function getOperLogField<K extends keyof OperLog>(record: OperLog, field: K): OperLog[K] {
  return record[field]
}

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
  {
    title: '用户名',
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: '操作模块',
    dataIndex: 'operModule',
    key: 'operModule',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: '操作类型',
    dataIndex: 'operType',
    key: 'operType',
    width: 90,
    resizable: true,
    ellipsis: true
  },
  {
    title: '操作方法',
    dataIndex: 'operMethod',
    key: 'operMethod',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: '请求方式',
    dataIndex: 'requestMethod',
    key: 'requestMethod',
    width: 90
  },
  {
    title: '操作状态',
    dataIndex: 'operStatus',
    key: 'operStatus',
    width: 90
  },
  {
    title: '操作IP',
    dataIndex: 'operIp',
    key: 'operIp',
    width: 130,
    ellipsis: true
  },
  {
    title: '操作时间',
    dataIndex: 'operTime',
    key: 'operTime',
    width: 170,
    resizable: true,
    ellipsis: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'delete',
        label: '删除',
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'logging:operlog:delete',
        onClick: (record: OperLog) => handleDeleteOne(record)
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
    const colKey = getColumnKey(col as OperLogColumn)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: OperLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: OperLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getOperLogId(selectedRow.value) === getOperLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: OperLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: OperLog) => ({
  onClick: () => {
    const key = getOperLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getOperLogId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: OperLogQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    const response = await getOperLogList(params)
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: unknown) {
    logger.error('[OperLog] 加载数据失败:', error)
    message.error(getErrorMessage(error, '加载数据失败'))
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
    title: '确认删除',
    content: `确定要删除操作日志 "${name}" 吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await deleteOperLog(getOperLogId(record))
        message.success('删除成功')
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, '删除失败'))
      } finally {
        loading.value = false
      }
    }
  })
}
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的操作日志')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条操作日志吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getOperLogId(r))).filter(n => !Number.isNaN(n))
        await deleteOperLogBatch(ids)
        message.success('删除成功')
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error, '删除失败'))
      } finally {
        loading.value = false
      }
    }
  })
}
const handleExport = async () => {
  try {
    loading.value = true
    const params: OperLogQuery = { pageIndex: 1, pageSize: total.value || 99999 }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    const blob = await exportOperLog(params, undefined, '操作日志')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `操作日志_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success('导出成功')
  } catch (error: unknown) {
    logger.error('[OperLog] 导出失败:', error)
    message.error(getErrorMessage(error, '导出失败'))
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
