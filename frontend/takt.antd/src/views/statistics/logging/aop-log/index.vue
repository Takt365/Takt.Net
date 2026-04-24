<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/statistics/logging/aop-log -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2026-04-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：差异日志页面，包含关键字查询、列表分页、批量删除、导出与列设置 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="logging-aop-log">
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

    <!-- 表格 -->
    <TaktSingleTable
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
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getAopLogList, deleteAopLogById, deleteAopLogBatch, exportAopLogData } from '@/api/statistics/logging/aop-log'
import type { AopLog, AopLogQuery } from '@/types/statistics/logging/aop-log'
import { logger } from '@/utils/logger'
import { RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

// 顶栏查询关键字
const queryKeyword = ref('')
// 表格加载状态与数据
const loading = ref(false)
const dataSource = ref<AopLog[]>([])
// 分页状态
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
// 行选择状态
const selectedRow = ref<AopLog | null>(null)
const selectedRows = ref<AopLog[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
// 列设置抽屉
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

type ColumnLike = { key?: string | number; dataIndex?: string | number; title?: string | number; width?: string | number }

const searchPlaceholder = computed(
  () => t('common.form.placeholder.required', { field: [t('entity.aoplog.username'), t('entity.aoplog.tablename')].join('、') }) + t('common.button.query')
)

const getErrorMessage = (error: unknown, fallbackKey: string): string => {
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const messageText = (error as { message?: unknown }).message
    if (typeof messageText === 'string' && messageText.trim()) return messageText
  }
  return t(fallbackKey)
}

/** TaktSingleTable 的 rowKey 回调入参为 TableRecord（Record<string, unknown>），与 AopLog 不能直接标注为同一形参类型 */
const getAopLogId = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const id = (record as { aopLogId?: unknown }).aopLogId
  return id != null ? String(id) : ''
}
const getAopLogField = <K extends keyof AopLog>(record: AopLog, field: K): AopLog[K] => record[field]

// 初始化加载数据
onMounted(() => {
  loadData()
})

// 表格列定义
const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'aopLogId',
    key: 'aopLogId',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: AopLog }) => String(getAopLogField(record, 'aopLogId') ?? '')
  },
  { title: t('entity.aoplog.username'), dataIndex: 'userName', key: 'userName', width: 120, resizable: true, ellipsis: true },
  { title: t('entity.aoplog.opertype'), dataIndex: 'operType', key: 'operType', width: 100, resizable: true, ellipsis: true },
  { title: t('entity.aoplog.tablename'), dataIndex: 'tableName', key: 'tableName', width: 140, resizable: true, ellipsis: true },
  { title: t('entity.aoplog.primarykeyid'), dataIndex: 'primaryKeyId', key: 'primaryKeyId', width: 100, resizable: true, ellipsis: true },
  { title: t('entity.aoplog.opertime'), dataIndex: 'operTime', key: 'operTime', width: 170, resizable: true, ellipsis: true },
  { title: t('entity.aoplog.costtime'), dataIndex: 'costTime', key: 'costTime', width: 90 },
  CreateActionColumn<AopLog>({
    actions: [
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'statistics:logging:aoplog:delete',
        onClick: (record: AopLog) => handleDeleteOne(record)
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
    const key = (col as ColumnLike).key ?? (col as ColumnLike).dataIndex ?? (col as ColumnLike).title
    return key != null && keysSet.has(String(key))
  })
})

// 行选择配置
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AopLog[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: AopLog, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getAopLogId(selectedRow.value) === getAopLogId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AopLog[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
  }
}))

// 行点击选中
const onClickRow = (record: AopLog) => ({
  onClick: () => {
    const key = getAopLogId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getAopLogId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

// 加载数据
const loadData = async () => {
  try {
    loading.value = true
    const params: AopLogQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      KeyWords: queryKeyword.value || ''
    }
    const response = await getAopLogList(params)
    const responseAny = response as { Data?: AopLog[]; Total?: number }
    dataSource.value = response?.data ?? responseAny?.Data ?? []
    total.value = response?.total ?? responseAny?.Total ?? 0
  } catch (error: unknown) {
    logger.error('[AopLog] 加载数据失败:', error)
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
  const sortInfo = Array.isArray(sorter) ? sorter[0] as Record<string, unknown> : sorter as Record<string, unknown>
  if (sortInfo?.order) logger.debug('[AopLog] 排序:', sortInfo.field, sortInfo.order)
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

const handleResizeColumn = (w: number, col: ColumnLike) => {
  const column = columns.value.find((c) => {
    const cLike = c as ColumnLike
    const sourceKey = cLike.key ?? cLike.dataIndex ?? cLike.title
    const targetKey = col.key ?? col.dataIndex ?? col.title
    return sourceKey != null && targetKey != null && String(sourceKey) === String(targetKey)
  }) as ColumnLike | undefined
  if (column) column.width = w
}

const handleDeleteOne = (record: AopLog) => {
  const name = String(getAopLogField(record, 'tableName') ?? getAopLogId(record))
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.aoplog._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAopLogById(getAopLogId(record))
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
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.aoplog._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: t('entity.aoplog._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        const ids = selectedRows.value.map(r => Number(getAopLogId(r))).filter(n => !Number.isNaN(n))
        await deleteAopLogBatch(ids)
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
    const params: AopLogQuery = {
      pageIndex: 1,
      pageSize: total.value || 99999,
      KeyWords: queryKeyword.value || ''
    }
    const blob = await exportAopLogData(params, undefined, t('entity.aoplog._self'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.aoplog._self')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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
    logger.error('[AopLog] 导出失败:', error)
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
.logging-aop-log {
  padding: 16px;
}
</style>
