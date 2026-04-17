<template>
  <div class="routine-announcement">
    <TaktQueryBar
      v-model="queryKeyword"
      placeholder="请输入公告编码或标题"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />
    <TaktToolsBar
      create-permission="routine:business:announcement:create"
      update-permission="routine:business:announcement:update"
      delete-permission="routine:business:announcement:delete"
      export-permission="routine:business:announcement:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
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
      :row-key="(r: any) => r.announcementId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="() => {}"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'announcementType'">
          {{ announcementTypeMap[record.announcementType] ?? record.announcementType }}
        </template>
        <template v-else-if="column.key === 'announcementStatus'">
          <a-tag :color="statusColorMap[record.announcementStatus]">
            {{ statusMap[record.announcementStatus] ?? record.announcementStatus }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'isTop'">
          {{ record.isTop === 1 ? '是' : '否' }}
        </template>
        <template v-else-if="column.key === 'isUrgent'">
          {{ urgentMap[record.isUrgent] ?? record.isUrgent }}
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
      :width="640"
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
      <a-form-item label="公告编码">
        <a-input
          v-model:value="advancedQueryForm.announcementCode"
          placeholder="公告编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="公告标题">
        <a-input
          v-model:value="advancedQueryForm.announcementTitle"
          placeholder="公告标题"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="公告类型">
        <a-select
          v-model:value="advancedQueryForm.announcementType"
          placeholder="请选择"
          allow-clear
          :options="[
            { label: '通知', value: 0 },
            { label: '公告', value: 1 },
            { label: '新闻', value: 2 },
            { label: '活动', value: 3 }
          ]"
        />
      </a-form-item>
      <a-form-item label="公告状态">
        <a-select
          v-model:value="advancedQueryForm.announcementStatus"
          placeholder="请选择"
          allow-clear
          :options="[
            { label: '草稿', value: 0 },
            { label: '已发布', value: 1 },
            { label: '已撤回', value: 2 },
            { label: '已过期', value: 3 }
          ]"
        />
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
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import AnnouncementForm from './components/announcement-form.vue'
import {
  getAnnouncementList,
  createAnnouncement,
  updateAnnouncement,
  deleteAnnouncement,
  deleteAnnouncementBatch,
  exportAnnouncements
} from '@/api/routine/business/announcement'
import type { Announcement, AnnouncementQuery } from '@/types/routine/business/announcement'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

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
const formData = ref<Partial<Announcement>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  announcementCode?: string
  announcementTitle?: string
  announcementType?: number
  announcementStatus?: number
}>({})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const announcementTypeMap: Record<number, string> = { 0: '通知', 1: '公告', 2: '新闻', 3: '活动' }
const statusMap: Record<number, string> = { 0: '草稿', 1: '已发布', 2: '已撤回', 3: '已过期' }
const statusColorMap: Record<number, string> = { 0: 'default', 1: 'green', 2: 'orange', 3: 'red' }
const urgentMap: Record<number, string> = { 0: '一般', 1: '紧急', 2: '非常紧急' }

const columns = ref<TableColumnsType>([
  { title: '公告ID', dataIndex: 'announcementId', key: 'announcementId', width: 120, fixed: 'left' },
  { title: '公告编码', dataIndex: 'announcementCode', key: 'announcementCode', width: 140, ellipsis: true },
  { title: '公告标题', dataIndex: 'announcementTitle', key: 'announcementTitle', width: 200, ellipsis: true },
  { title: '公告类型', dataIndex: 'announcementType', key: 'announcementType', width: 90 },
  { title: '发布人', dataIndex: 'publisherName', key: 'publisherName', width: 100 },
  { title: '状态', dataIndex: 'announcementStatus', key: 'announcementStatus', width: 90 },
  { title: '是否置顶', dataIndex: 'isTop', key: 'isTop', width: 80 },
  { title: '是否紧急', dataIndex: 'isUrgent', key: 'isUrgent', width: 90 },
  { title: '阅读次数', dataIndex: 'readCount', key: 'readCount', width: 90 },
  { title: '发布时间', dataIndex: 'publishTime', key: 'publishTime', width: 160 },
  { title: '创建时间', dataIndex: 'createdAt', key: 'createdAt', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:business:announcement:update', onClick: (r: Announcement) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:business:announcement:delete', onClick: (r: Announcement) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed(() => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = col.key || col.dataIndex || col.title
    return colKey && keysSet.has(String(colKey))
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Announcement[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: Announcement) => ({
  onClick: () => {
    const key = record.announcementId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.announcementId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: AnnouncementQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      keyWords: queryKeyword.value || undefined,
      announcementCode: advancedQueryForm.value.announcementCode,
      announcementTitle: advancedQueryForm.value.announcementTitle,
      announcementType: advancedQueryForm.value.announcementType,
      announcementStatus: advancedQueryForm.value.announcementStatus
    }
    const res = await getAnnouncementList(params) as any
    dataSource.value = res?.data ?? []
    total.value = res?.total ?? 0
  } catch (e: any) {
    logger.error('[Announcement] loadData error', e)
    message.error(e?.message || '加载失败')
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() {
  currentPage.value = 1
  loadData()
}

function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = {}
  currentPage.value = 1
  loadData()
}

function handleResizeColumn(_w: number, _col: any) {}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
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
    content: `确定要删除公告「${record.announcementTitle}」吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await deleteAnnouncement(String(record.announcementId))
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
        await deleteAnnouncementBatch(selectedRows.value.map(r => String(r.announcementId)))
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

function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

function handleAdvancedQueryReset() {
  advancedQueryForm.value = {}
}

function handleColumnSetting() {
  columnSettingVisible.value = true
}

function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map(k => String(k))
}

function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

function handleRefresh() {
  loadData()
}

async function handleExport() {
  try {
    loading.value = true
    const query: AnnouncementQuery = {
      pageIndex: 1,
      pageSize: 99999,
      keyWords: queryKeyword.value || undefined,
      ...advancedQueryForm.value
    }
    const blob = await exportAnnouncements(query)
    const name = `公告数据_${Date.now()}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.click()
    window.URL.revokeObjectURL(url)
    message.success('导出成功')
  } catch (e: any) {
    message.error(e?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (values.announcementId) {
      await updateAnnouncement(values.announcementId, values)
      message.success('更新成功')
    } else {
      await createAnnouncement(values)
      message.success('创建成功')
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || '保存失败')
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-announcement {
  padding: 16px;
}
</style>
