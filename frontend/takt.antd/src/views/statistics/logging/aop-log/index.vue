<template>
  <div class="logging-aop-log">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入用户名或表名"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      delete-permission="statistics:logging:aoplog:delete"
      export-permission="statistics:logging:aoplog:export"
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
      :row-key="getAopLogId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    />
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
      :id-column-key="'aopLogId'"
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
import {
  getAopLogList,
  deleteAopLog,
  deleteAopLogBatch,
  exportAopLog
} from '@/api/statistics/logging/aop-log'
import type { AopLog } from '@/types/statistics/logging/aop-log'
import { logger } from '@/utils/logger'
import { RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AopLog[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AopLog | null>(null)
const selectedRows = ref<AopLog[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const tableRef = ref()
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getAopLogId = (record: any): string => record?.aopLogId != null ? String(record.aopLogId) : ''
const getAopLogField = (record: any, field: string): any => record?.[field]

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'aopLogId',
    key: 'aopLogId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getAopLogField(record, 'aopLogId') ?? ''
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
    title: '操作类型',
    dataIndex: 'operType',
    key: 'operType',
    width: 100,
    resizable: true,
    ellipsis: true
  },
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: '主键ID',
    dataIndex: 'primaryKeyId',
    key: 'primaryKeyId',
    width: 100,
    resizable: true,
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
  {
    title: '耗时(ms)',
    dataIndex: 'costTime',
    key: 'costTime',
    width: 90
  },
  CreateActionColumn({
    actions: [
      {
        key: 'delete',
        label: '删除',
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'statistics:logging:aoplog:delete',
        onClick: (record: AopLog) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed((): any => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) {
    return columns.value
  }
  const getColumnKey = (col: any): string => (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AopLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AopLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getAopLogId(selectedRow.value) === getAopLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AopLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AopLog) => ({
  onClick: () => {
    const key = getAopLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getAopLogId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      PageIndex: currentPage.value,
      PageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const response = await getAopLogList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[AopLog] 加载数据失败:', error)
    message.error(error?.message ?? '加载数据失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => { currentPage.value = 1; loadData() }
const handleReset = () => { queryKeyword.value = ''; currentPage.value = 1; loadData() }
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[AopLog] 排序:', sorter.field, sorter.order)
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
const handleResizeColumn = (w: number, col: any) => {
  const column = columns.value.find((c: any) => String(c.key || c.dataIndex || c.title) === String(col.key || col.dataIndex || col.title))
  if (column) column.width = w
}

const handleDeleteOne = (record: AopLog) => {
  const name = getAopLogField(record, 'tableName') || getAopLogId(record)
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除差异日志 "${name}" 吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await deleteAopLog(getAopLogId(record))
        message.success('删除成功')
        loadData()
      } catch (error: any) {
        message.error(error?.message ?? '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的差异日志')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条差异日志吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getAopLogId(r))).filter(n => !Number.isNaN(n))
        await deleteAopLogBatch(ids)
        message.success('删除成功')
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message ?? '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}
const handleExport = async () => {
  try {
    loading.value = true
    const params: any = { PageIndex: 1, PageSize: total.value || 99999 }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const blob = await exportAopLog(params, undefined, '差异日志')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `差异日志_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
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
  } catch (error: any) {
    logger.error('[AopLog] 导出失败:', error)
    message.error(error?.message ?? '导出失败')
  } finally {
    loading.value = false
  }
}
const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => { visibleColumnKeys.value = keys.map(k => String(k)) }
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }
const handleRefresh = () => loadData()
defineExpose({ tableRef })
</script>

<style scoped lang="less">
.logging-aop-log {
  padding: 16px;
}
</style>
