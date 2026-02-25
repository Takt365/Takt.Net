<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/announcement -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：公告通知管理页面，列表、查询、新增、编辑、删除、状态 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="routine-announcement">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入公告标题或编码"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:announcement:create"
      update-permission="routine:announcement:update"
      delete-permission="routine:announcement:delete"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
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
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />
    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(r: any) => r.announcementId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'announcementStatus'">
          <a-tag :color="getStatusColor(record.announcementStatus)">{{ getStatusText(record.announcementStatus) }}</a-tag>
        </template>
        <template v-else-if="column.key === 'announcementType'">
          {{ getTypeText(record.announcementType) }}
        </template>
        <template v-else-if="column.key === 'isTop'">
          <a-tag v-if="record.isTop === 0">置顶</a-tag>
          <span v-else>-</span>
        </template>
        <template v-else-if="column.key === 'isUrgent'">
          <a-tag v-if="record.isUrgent === 0" color="red">紧急</a-tag>
          <span v-else>-</span>
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
      <AnnouncementForm
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
      <a-form-item label="公告类型">
        <a-select v-model:value="advancedQueryForm.announcementType" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">通知</a-select-option>
          <a-select-option :value="1">公告</a-select-option>
          <a-select-option :value="2">新闻</a-select-option>
          <a-select-option :value="3">活动</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="状态">
        <a-select v-model:value="advancedQueryForm.announcementStatus" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">草稿</a-select-option>
          <a-select-option :value="1">已发布</a-select-option>
          <a-select-option :value="2">已撤回</a-select-option>
          <a-select-option :value="3">已过期</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="是否置顶">
        <a-select v-model:value="advancedQueryForm.isTop" placeholder="请选择" allow-clear style="width: 100%">
          <a-select-option :value="0">是</a-select-option>
          <a-select-option :value="1">否</a-select-option>
        </a-select>
      </a-form-item>
    </TaktQueryDrawer>
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'announcementId'"
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
import { getList, create, update, remove, removeBatch, updateStatus } from '@/api/routine/announcement'
import type { Announcement, AnnouncementQuery, AnnouncementUpdate } from '@/types/routine/announcement'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import AnnouncementForm from './components/announcement-form.vue'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Announcement[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Announcement | null>(null)
const selectedRows = ref<Announcement[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('新增公告')
const formLoading = ref(false)
const formRef = ref<InstanceType<typeof AnnouncementForm> | null>(null)
const formData = ref<Partial<Announcement>>({})
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ announcementType?: number; announcementStatus?: number; isTop?: number }>({})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = ref<TableColumnsType>([
  { title: 'ID', dataIndex: 'announcementId', key: 'announcementId', width: 120, fixed: 'left' },
  { title: '公告编码', dataIndex: 'announcementCode', key: 'announcementCode', width: 140, ellipsis: true },
  { title: '公告标题', dataIndex: 'announcementTitle', key: 'announcementTitle', width: 200, ellipsis: true },
  { title: '公告类型', dataIndex: 'announcementType', key: 'announcementType', width: 90 },
  { title: '发布人', dataIndex: 'publisherName', key: 'publisherName', width: 100 },
  { title: '状态', dataIndex: 'announcementStatus', key: 'announcementStatus', width: 90 },
  { title: '置顶', dataIndex: 'isTop', key: 'isTop', width: 70 },
  { title: '紧急', dataIndex: 'isUrgent', key: 'isUrgent', width: 70 },
  { title: '阅读数', dataIndex: 'readCount', key: 'readCount', width: 80 },
  { title: '发布时间', dataIndex: 'publishTime', key: 'publishTime', width: 160 },
  { title: '生效时间', dataIndex: 'effectiveTime', key: 'effectiveTime', width: 160 },
  { title: '失效时间', dataIndex: 'expireTime', key: 'expireTime', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:announcement:update', onClick: (r: Announcement) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:announcement:delete', onClick: (r: Announcement) => handleDeleteOne(r) }
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
  onChange: (keys: (string | number)[], rows: Announcement[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Announcement, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.announcementId === record?.announcementId) selectedRow.value = null
  },
  onSelectAll: (_: boolean, rows: Announcement[]) => {
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: Announcement) => ({
  onClick: () => {
    const id = String(record?.announcementId ?? '')
    const idx = selectedRowKeys.value.indexOf(id)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(id)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.announcementId ?? ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: AnnouncementQuery = { pageIndex: currentPage.value, pageSize: pageSize.value }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.announcementType !== undefined) params.announcementType = advancedQueryForm.value.announcementType
    if (advancedQueryForm.value.announcementStatus !== undefined) params.announcementStatus = advancedQueryForm.value.announcementStatus
    if (advancedQueryForm.value.isTop !== undefined) params.isTop = advancedQueryForm.value.isTop

    const res = await getList(params) as any
    const items = res?.data ?? res?.Data ?? []
    const totalCount = res?.total ?? res?.Total ?? 0
    dataSource.value = Array.isArray(items) ? items : []
    total.value = Number(totalCount)
  } catch (e: any) {
    logger.error('[Announcement] 加载失败:', e)
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
  formTitle.value = '新增公告'
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Announcement) {
  formTitle.value = '编辑公告'
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择一条记录')
}

function handleDeleteOne(record: Announcement) {
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除公告「${record.announcementTitle || ''}」吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await remove(record.announcementId)
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
    content: `确定要删除选中的 ${selectedRows.value.length} 条公告吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await removeBatch(selectedRows.value.map(r => r.announcementId))
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
    const id = formData.value?.announcementId
    if (id) {
      await update(id, { ...formValues, announcementId: id } as AnnouncementUpdate)
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

function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = {} }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map(k => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }

function getStatusText(s: number) {
  const map: Record<number, string> = { 0: '草稿', 1: '已发布', 2: '已撤回', 3: '已过期' }
  return map[s] ?? '-'
}
function getStatusColor(s: number) {
  const map: Record<number, string> = { 0: 'default', 1: 'green', 2: 'orange', 3: 'red' }
  return map[s] ?? 'default'
}
function getTypeText(t: number) {
  const map: Record<number, string> = { 0: '通知', 1: '公告', 2: '新闻', 3: '活动' }
  return map[t] ?? '-'
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-announcement {
  padding: 16px;
}
</style>
