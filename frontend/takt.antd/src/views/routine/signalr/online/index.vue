<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/signalr/online -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：在线用户列表，查询、删除、导出（不创建） -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-signalr-online">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="用户名、连接ID、IP、地点"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      delete-permission="signalr:online:delete"
      export-permission="signalr:online:export"
      :show-create="false"
      :show-update="false"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :delete-disabled="selectedRows.length === 0"
      :export-loading="loading"
      :refresh-loading="loading"
      @delete="handleDelete"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(r: any) => r.onlineId || r.connectionId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'onlineStatus'">
          <a-tag :color="record.onlineStatus === 0 ? 'green' : record.onlineStatus === 1 ? 'default' : 'orange'">
            {{ record.onlineStatus === 0 ? '在线' : record.onlineStatus === 1 ? '离线' : '离开' }}
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
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item label="用户名">
        <a-input v-model:value="advancedQueryForm.userName" placeholder="用户名" allow-clear />
      </a-form-item>
      <a-form-item label="连接ID">
        <a-input v-model:value="advancedQueryForm.connectionId" placeholder="连接ID" allow-clear />
      </a-form-item>
      <a-form-item label="在线状态">
        <a-select v-model:value="advancedQueryForm.onlineStatus" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">在线</a-select-option>
          <a-select-option :value="1">离线</a-select-option>
          <a-select-option :value="2">离开</a-select-option>
        </a-select>
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'onlineId'"
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
import { mergeDefaultColumns } from '@/utils/table-columns'
import { getList, remove, removeBatch, exportOnline } from '@/api/routine/signalr/online'
import type { Online, OnlineQuery } from '@/types/routine/signalr/online'
import { RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Online[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Online | null>(null)
const selectedRows = ref<Online[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<OnlineQuery>({ pageIndex: 1, pageSize: 20 })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = ref<TableColumnsType>([
  { title: 'ID', dataIndex: 'onlineId', key: 'onlineId', width: 120, fixed: 'left' },
  { title: '连接ID', dataIndex: 'connectionId', key: 'connectionId', width: 180, ellipsis: true },
  { title: '用户名', dataIndex: 'userName', key: 'userName', width: 120 },
  { title: '用户ID', dataIndex: 'userId', key: 'userId', width: 100 },
  { title: '在线状态', dataIndex: 'onlineStatus', key: 'onlineStatus', width: 90 },
  { title: '连接IP', dataIndex: 'connectIp', key: 'connectIp', width: 130 },
  { title: '连接地点', dataIndex: 'connectLocation', key: 'connectLocation', width: 160, ellipsis: true },
  { title: '设备类型', dataIndex: 'deviceType', key: 'deviceType', width: 90 },
  { title: '浏览器', dataIndex: 'browserType', key: 'browserType', width: 100 },
  { title: '操作系统', dataIndex: 'operatingSystem', key: 'operatingSystem', width: 100 },
  { title: '连接时间', dataIndex: 'connectTime', key: 'connectTime', width: 160 },
  { title: '最后活动', dataIndex: 'lastActiveTime', key: 'lastActiveTime', width: 160 },
  { title: '断开时间', dataIndex: 'disconnectTime', key: 'disconnectTime', width: 160 },
  { title: '连接时长(秒)', dataIndex: 'connectionDuration', key: 'connectionDuration', width: 110 },
  CreateActionColumn({
    actions: [
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'signalr:online:delete', onClick: (r: Online) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getKey = (col: any) => String(col.key || col.dataIndex || col.title || '')
  return merged.filter((col: any) => new Set(keys.map(k => String(k))).has(getKey(col)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Online[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Online, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.onlineId === record?.onlineId) selectedRow.value = null
  },
  onSelectAll: (_: boolean, rows: Online[]) => { selectedRow.value = rows.length === 1 ? rows[0] : null }
}))

const onClickRow = (record: Online) => ({
  onClick: () => {
    const id = String(record?.onlineId ?? record?.connectionId ?? '')
    const idx = selectedRowKeys.value.indexOf(id)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(id)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.onlineId ?? '') || selectedRowKeys.value.includes(item.connectionId ?? ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: OnlineQuery = { pageIndex: currentPage.value, pageSize: pageSize.value }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.userName) params.userName = advancedQueryForm.value.userName
    if (advancedQueryForm.value.connectionId) params.connectionId = advancedQueryForm.value.connectionId
    if (advancedQueryForm.value.onlineStatus !== undefined) params.onlineStatus = advancedQueryForm.value.onlineStatus

    const res = await getList(params) as any
    const items = res?.data ?? res?.Data ?? []
    const totalCount = res?.total ?? res?.Total ?? 0
    dataSource.value = Array.isArray(items) ? items : []
    total.value = Number(totalCount)
  } catch (e: any) {
    logger.error('[Online] 加载失败:', e)
    message.error(e?.message || '加载失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() { currentPage.value = 1; loadData() }
function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { pageIndex: 1, pageSize: pageSize.value }
  currentPage.value = 1
  loadData()
}
function handleTableChange(_p: any, _f: any, _s: any) {}
function handleResizeColumn(_w: number, _col: any) {}
function handlePaginationChange(page: number, size: number) { currentPage.value = page; pageSize.value = size; loadData() }
function handlePaginationSizeChange(_: number, size: number) { pageSize.value = size; loadData() }

function handleDeleteOne(record: Online) {
  Modal.confirm({
    title: '确认删除',
    content: `确定要踢下线用户「${record.userName}」吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await remove(String(record.onlineId))
        message.success('已删除')
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要踢下线选中的 ${selectedRows.value.length} 个用户吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await removeBatch(selectedRows.value.map(r => String(r.onlineId)))
        message.success('已删除')
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

async function handleExport() {
  try {
    loading.value = true
    const params: OnlineQuery = { pageIndex: 1, pageSize: 99999 }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.userName) params.userName = advancedQueryForm.value.userName
    if (advancedQueryForm.value.connectionId) params.connectionId = advancedQueryForm.value.connectionId
    if (advancedQueryForm.value.onlineStatus !== undefined) params.onlineStatus = advancedQueryForm.value.onlineStatus

    const blob = await exportOnline(params, undefined, '在线用户')
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `在线用户_${new Date().toISOString().slice(0, 19).replace(/[-:T]/g, '')}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (e: any) {
    logger.error('[Online] 导出失败:', e)
    message.error(e?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = { pageIndex: 1, pageSize: pageSize.value } }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map(k => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-signalr-online {
  padding: 16px;
}
</style>
