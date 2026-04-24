<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/statistics/logging/login-log -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-04-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：登录日志页面，包含关键字查询、列表分页、批量删除、导出与列设置 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="logging-login-log">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="searchPlaceholder"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
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

    <!-- 表格 -->
    <TaktSingleTable
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
            {{ record.loginStatus === 0 ? t('common.state.success') : t('common.state.failed') }}
          </a-tag>
        </template>
      </template>
    </TaktSingleTable>

    <!-- 分页组件 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 列设置抽屉 -->
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
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getLoginLogList, deleteLoginLogById, deleteLoginLogBatch, exportLoginLogData } from '@/api/statistics/logging/login-log'
import type { LoginLog, LoginLogQuery } from '@/types/statistics/logging/login-log'
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
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type LoginLogColumn = { key?: string | number; dataIndex?: string | number; title?: string | number; width?: string | number }
type TableSorterInfo = { field?: string; order?: string }

const searchPlaceholder = computed(
  () => t('common.form.placeholder.required', { field: [t('entity.loginlog.username'), t('entity.loginlog.loginip')].join('、') }) + t('common.button.query')
)

const getErrorMessage = (error: unknown, fallbackKey: string): string => {
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const messageText = (error as { message?: unknown }).message
    if (typeof messageText === 'string' && messageText.trim()) return messageText
  }
  return t(fallbackKey)
}

const getColumnKey = (column: LoginLogColumn): string => {
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

/** TaktSingleTable 的 rowKey 回调入参为 TableRecord，与 LoginLog 形参类型不兼容 */
const getLoginLogId = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const id = (record as { loginLogId?: unknown }).loginLogId
  return id != null ? String(id) : ''
}
const getLoginLogField = <K extends keyof LoginLog>(record: LoginLog, field: K): LoginLog[K] => record[field]

onMounted(() => {
  loadData()
})

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'loginLogId',
    key: 'loginLogId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: LoginLog }) => getLoginLogField(record, 'loginLogId') ?? ''
  },
  { title: t('entity.loginlog.username'), dataIndex: 'userName', key: 'userName', width: 120, resizable: true, ellipsis: true },
  { title: t('entity.loginlog.loginip'), dataIndex: 'loginIp', key: 'loginIp', width: 130, resizable: true, ellipsis: true },
  { title: t('entity.loginlog.loginlocation'), dataIndex: 'loginLocation', key: 'loginLocation', width: 140, resizable: true, ellipsis: true },
  { title: t('entity.loginlog.logintype'), dataIndex: 'loginType', key: 'loginType', width: 100, resizable: true, ellipsis: true },
  { title: t('entity.loginlog.loginstatus'), dataIndex: 'loginStatus', key: 'loginStatus', width: 90 },
  { title: t('entity.loginlog.loginmsg'), dataIndex: 'loginMsg', key: 'loginMsg', width: 150, ellipsis: true },
  { title: t('entity.loginlog.logintime'), dataIndex: 'loginTime', key: 'loginTime', width: 170, resizable: true, ellipsis: true },
  CreateActionColumn<LoginLog>({
    actions: [
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'statistics:logging:loginlog:delete',
        onClick: (record: LoginLog) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed<TableColumnsType>(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: unknown) => {
    const colKey = getColumnKey(col as LoginLogColumn)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: LoginLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: LoginLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getLoginLogId(selectedRow.value) === getLoginLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: LoginLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
  }
}))

const onClickRow = (record: LoginLog) => ({
  onClick: () => {
    const key = getLoginLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getLoginLogId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: LoginLogQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      KeyWords: queryKeyword.value || ''
    }
    const response = await getLoginLogList(params)
    dataSource.value = response?.data ?? []
    total.value = response?.total ?? 0
  } catch (error: unknown) {
    logger.error('[LoginLog] 加载数据失败:', error)
    message.error(getErrorMessage(error, 'common.msg.loadfail'))
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

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: unknown) => {
  const sorterInfo = getSorterInfo(sorter)
  if (sorterInfo.order) logger.debug('[LoginLog] 排序:', sorterInfo.field, sorterInfo.order)
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

const handleResizeColumn = (w: number, col: LoginLogColumn) => {
  const column = columns.value.find((c) => getColumnKey(c as LoginLogColumn) === getColumnKey(col))
  if (column) (column as LoginLogColumn).width = w
}

const handleDeleteOne = (record: LoginLog) => {
  const name = getLoginLogField(record, 'userName') || getLoginLogId(record)
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.loginlog._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteLoginLogById(getLoginLogId(record))
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
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.loginlog._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: t('entity.loginlog._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getLoginLogId(r))).filter(n => !Number.isNaN(n))
        await deleteLoginLogBatch(ids)
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
    const params: LoginLogQuery = {
      pageIndex: 1,
      pageSize: total.value || 99999,
      KeyWords: queryKeyword.value || ''
    }
    const blob = await exportLoginLogData(params, undefined, t('entity.loginlog._self'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.loginlog._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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
    logger.error('[LoginLog] 导出失败:', error)
    message.error(getErrorMessage(error, 'common.msg.exportfail'))
  } finally {
    loading.value = false
  }
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}

const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

const handleRefresh = () => {
  loadData()
}
</script>

<style scoped lang="less">
.logging-login-log {
  padding: 16px;
}
</style>
