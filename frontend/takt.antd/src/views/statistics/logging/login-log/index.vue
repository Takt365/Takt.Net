<template>
  <div class="logging-login-log">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入用户名或登录IP"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      delete-permission="statistics:logging:loginlog:delete"
      export-permission="statistics:logging:loginlog:export"
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
      :row-key="getLoginLogId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'loginStatus'">
          <a-tag :color="record.loginStatus === 0 ? 'success' : 'error'">
            {{ record.loginStatus === 0 ? '成功' : '失败' }}
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
      :id-column-key="'loginLogId'"
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
  getLoginLogList,
  deleteLoginLog,
  deleteLoginLogBatch,
  exportLoginLog
} from '@/api/statistics/logging/login-log'
import type { LoginLog } from '@/types/statistics/logging/login-log'
import { logger } from '@/utils/logger'
import { RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<LoginLog[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<LoginLog | null>(null)
const selectedRows = ref<LoginLog[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const tableRef = ref()
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getLoginLogId = (record: any): string => {
  return record?.loginLogId != null ? String(record.loginLogId) : ''
}
const getLoginLogField = (record: any, field: string): any => record?.[field]

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'loginLogId',
    key: 'loginLogId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getLoginLogField(record, 'loginLogId') ?? ''
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
    title: '登录IP',
    dataIndex: 'loginIp',
    key: 'loginIp',
    width: 130,
    resizable: true,
    ellipsis: true
  },
  {
    title: '登录地点',
    dataIndex: 'loginLocation',
    key: 'loginLocation',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: '登录方式',
    dataIndex: 'loginType',
    key: 'loginType',
    width: 100,
    resizable: true,
    ellipsis: true
  },
  {
    title: '登录状态',
    dataIndex: 'loginStatus',
    key: 'loginStatus',
    width: 90
  },
  {
    title: '登录消息',
    dataIndex: 'loginMsg',
    key: 'loginMsg',
    width: 150,
    ellipsis: true
  },
  {
    title: '登录时间',
    dataIndex: 'loginTime',
    key: 'loginTime',
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
        permission: 'statistics:logging:loginlog:delete',
        onClick: (record: LoginLog) => handleDeleteOne(record)
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
  onChange: (keys: (string | number)[], rows: LoginLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: LoginLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getLoginLogId(selectedRow.value) === getLoginLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: LoginLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: LoginLog) => ({
  onClick: () => {
    const key = getLoginLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getLoginLogId(item)))
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
    const response = await getLoginLogList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[LoginLog] 加载数据失败:', error)
    message.error(error?.message ?? '加载数据失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => {
  currentPage.value = 1
  loadData()
}
const handleReset = () => {
  queryKeyword.value = ''
  currentPage.value = 1
  loadData()
}
const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[LoginLog] 排序:', sorter.field, sorter.order)
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

const handleDeleteOne = (record: LoginLog) => {
  const name = getLoginLogField(record, 'userName') || getLoginLogId(record)
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除登录日志 "${name}" 吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await deleteLoginLog(getLoginLogId(record))
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
    message.warning('请选择要删除的登录日志')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条登录日志吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getLoginLogId(r))).filter(n => !Number.isNaN(n))
        await deleteLoginLogBatch(ids)
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
    const blob = await exportLoginLog(params, undefined, '登录日志')
    const ts = new Date(); const t = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `登录日志_${ts.getFullYear()}${t(ts.getMonth()+1)}${t(ts.getDate())}${t(ts.getHours())}${t(ts.getMinutes())}${t(ts.getSeconds())}.xlsx`
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
    logger.error('[LoginLog] 导出失败:', error)
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
.logging-login-log {
  padding: 16px;
}
</style>
