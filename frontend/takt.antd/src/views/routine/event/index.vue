<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/event -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-21 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：活动组织管理页面，列表、查询、新增、编辑、删除、导出 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-event">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入活动编码、名称或地点"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:event:create"
      update-permission="routine:event:update"
      delete-permission="routine:event:delete"
      export-permission="routine:event:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
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
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(r: any) => r.eventId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'eventStatus'">
          <a-tag :color="getEventStatusColor(record.eventStatus)">{{ getEventStatusText(record.eventStatus) }}</a-tag>
        </template>
        <template v-else-if="column.key === 'eventType'">
          {{ getEventTypeText(record.eventType) }}
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
      :width="720"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <EventForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item label="活动类型">
        <a-select v-model:value="advancedQueryForm.eventType" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">培训</a-select-option>
          <a-select-option :value="1">团建</a-select-option>
          <a-select-option :value="2">会议活动</a-select-option>
          <a-select-option :value="3">庆典</a-select-option>
          <a-select-option :value="4">其他</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="活动状态">
        <a-select v-model:value="advancedQueryForm.eventStatus" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">草稿</a-select-option>
          <a-select-option :value="1">已发布</a-select-option>
          <a-select-option :value="2">进行中</a-select-option>
          <a-select-option :value="3">已结束</a-select-option>
          <a-select-option :value="4">已取消</a-select-option>
        </a-select>
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'eventId'"
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
import { getList, create, update, remove, removeBatch, exportEvents } from '@/api/routine/event'
import type { Event, EventQuery, EventUpdate } from '@/types/routine/event'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import EventForm from './components/event-form.vue'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Event[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Event | null>(null)
const selectedRows = ref<Event[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('新增活动')
const formLoading = ref(false)
const formRef = ref<InstanceType<typeof EventForm> | null>(null)
const formData = ref<Partial<Event>>({})
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ eventType?: number; eventStatus?: number }>({})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = ref<TableColumnsType>([
  { title: 'ID', dataIndex: 'eventId', key: 'eventId', width: 120, fixed: 'left' },
  { title: '活动编码', dataIndex: 'eventCode', key: 'eventCode', width: 160, ellipsis: true },
  { title: '活动名称', dataIndex: 'eventName', key: 'eventName', width: 180, ellipsis: true },
  { title: '活动类型', dataIndex: 'eventType', key: 'eventType', width: 100 },
  { title: '开始时间', dataIndex: 'startTime', key: 'startTime', width: 160 },
  { title: '结束时间', dataIndex: 'endTime', key: 'endTime', width: 160 },
  { title: '地点', dataIndex: 'location', key: 'location', width: 120, ellipsis: true },
  { title: '组织人', dataIndex: 'organizerName', key: 'organizerName', width: 100 },
  { title: '组织部门', dataIndex: 'deptName', key: 'deptName', width: 120, ellipsis: true },
  { title: '状态', dataIndex: 'eventStatus', key: 'eventStatus', width: 90 },
  { title: '排序号', dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: '创建时间', dataIndex: 'createTime', key: 'createTime', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:event:update', onClick: (r: Event) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:event:delete', onClick: (r: Event) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getKey = (col: any) => String(col.key || col.dataIndex || col.title || '')
  const set = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => set.has(getKey(col)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Event[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Event, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.eventId === record?.eventId) selectedRow.value = null
  },
  onSelectAll: (_: boolean, rows: Event[]) => {
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: Event) => ({
  onClick: () => {
    const id = String(record?.eventId ?? '')
    const idx = selectedRowKeys.value.indexOf(id)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(id)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.eventId ?? ''))
    selectedRow.value = selectedRows.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: EventQuery = { pageIndex: currentPage.value, pageSize: pageSize.value }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.eventType !== undefined) params.eventType = advancedQueryForm.value.eventType
    if (advancedQueryForm.value.eventStatus !== undefined) params.eventStatus = advancedQueryForm.value.eventStatus

    const res = await getList(params) as any
    const items = res?.data ?? res?.Data ?? []
    const totalCount = res?.total ?? res?.Total ?? 0
    dataSource.value = Array.isArray(items) ? items : []
    total.value = Number(totalCount)
  } catch (e: any) {
    logger.error('[Event] 加载失败:', e)
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
  advancedQueryForm.value = {}
  currentPage.value = 1
  loadData()
}
function handleTableChange(_p: any, _f: any, _s: any) {}
function handleResizeColumn(_w: number, _col: any) {}
function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}
function handlePaginationSizeChange(_: number, size: number) {
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = '新增活动'
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Event) {
  formTitle.value = '编辑活动'
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择一条记录')
}

function handleDeleteOne(record: Event) {
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除活动「${record.eventName || ''}」吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await remove(record.eventId)
        message.success('删除成功')
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
    content: `确定要删除选中的 ${selectedRows.value.length} 条活动吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await removeBatch(selectedRows.value.map(r => r.eventId))
        message.success('删除成功')
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

async function handleFormSubmit() {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    formLoading.value = true
    const formValues = formRef.value.getValues()
    const id = formData.value?.eventId
    if (id) {
      await update(id, { ...formValues, eventId: id } as EventUpdate)
      message.success('更新成功')
    } else {
      await create(formValues)
      message.success('新增成功')
    }
    formVisible.value = false
    formData.value = {}
    formRef.value?.resetFields()
    loadData()
  } catch (e: any) {
    if (!e?.errorFields) message.error(e?.message || '保存失败')
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

async function handleExport() {
  try {
    loading.value = true
    const queryParams: EventQuery = { pageIndex: 1, pageSize: total.value || 9999 }
    if (queryKeyword.value) queryParams.keyWords = queryKeyword.value
    if (advancedQueryForm.value.eventType !== undefined) queryParams.eventType = advancedQueryForm.value.eventType
    if (advancedQueryForm.value.eventStatus !== undefined) queryParams.eventStatus = advancedQueryForm.value.eventStatus

    const blob = await exportEvents(
      queryParams,
      undefined,
      t('menu.routine.event') + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('menu.routine.event')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportSuccess', { target: t('menu.routine.event') }))
  } catch (e: any) {
    message.error(e?.message || t('common.msg.exportFail', { target: t('menu.routine.event') }))
  } finally {
    loading.value = false
  }
}

function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = {} }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map(k => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }

function getEventTypeText(type: number) {
  const map: Record<number, string> = { 0: '培训', 1: '团建', 2: '会议活动', 3: '庆典', 4: '其他' }
  return map[type] ?? '-'
}
function getEventStatusText(status: number) {
  const map: Record<number, string> = { 0: '草稿', 1: '已发布', 2: '进行中', 3: '已结束', 4: '已取消' }
  return map[status] ?? '-'
}
function getEventStatusColor(status: number) {
  const map: Record<number, string> = { 0: 'default', 1: 'blue', 2: 'processing', 3: 'success', 4: 'error' }
  return map[status] ?? 'default'
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-event {
  padding: 16px;
}
</style>
