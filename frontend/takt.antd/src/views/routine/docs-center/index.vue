<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/routine/docs-center -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：文控中心文档管理页面，列表、查询、新增、编辑、删除、状态、批量删除 -->
<!-- ======================================== -->

<template>
  <div class="routine-docs-center">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.document.documentcode') + t('common.action.or') + t('entity.document.documenttitle') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="routine:document:create"
      update-permission="routine:document:update"
      delete-permission="routine:document:delete"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
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
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getDocumentId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'documentStatus'">
          <TaktSelect
            :value="record.documentStatus"
            dict-type="sys_document_status"
            :disabled="true"
            style="min-width: 90px"
          />
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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <DocsCenterForm
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
      <a-form-item :label="t('entity.document.documentcode')">
        <a-input v-model:value="advancedQueryForm.documentCode" allow-clear />
      </a-form-item>
      <a-form-item :label="t('entity.document.documenttitle')">
        <a-input v-model:value="advancedQueryForm.documentTitle" allow-clear />
      </a-form-item>
      <a-form-item :label="t('entity.document.documenttype')">
        <TaktSelect v-model:value="advancedQueryForm.documentType" dict-type="sys_document_type" allow-clear />
      </a-form-item>
      <a-form-item :label="t('entity.document.documentstatus')">
        <TaktSelect v-model:value="advancedQueryForm.documentStatus" dict-type="sys_document_status" allow-clear />
      </a-form-item>
      <a-form-item :label="t('entity.document.direction')">
        <TaktSelect v-model:value="advancedQueryForm.direction" dict-type="sys_document_direction" allow-clear />
      </a-form-item>
      <a-form-item :label="t('entity.document.lifecyclestage')">
        <TaktSelect v-model:value="advancedQueryForm.lifecycleStage" dict-type="sys_document_lifecycle_stage" allow-clear />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'documentId'"
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
import DocsCenterForm from './components/docs-center-form.vue'
import {
  getDocumentList,
  createDocument,
  updateDocument,
  deleteDocument,
  deleteDocumentBatch,
  updateDocumentStatus
} from '@/api/routine/document'
import type { Document, DocumentQuery } from '@/types/routine/document'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import { useUserStore } from '@/stores/identity/user'

const { t } = useI18n()
const userStore = useUserStore()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Document[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Document | null>(null)
const selectedRows = ref<Document[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Document>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  documentCode: string
  documentTitle: string
  documentType?: number
  documentStatus?: number
  direction?: number
  lifecycleStage?: number
}>({
  documentCode: '',
  documentTitle: '',
  documentType: undefined,
  documentStatus: undefined,
  direction: undefined,
  lifecycleStage: undefined
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'documentId', key: 'id', width: 120, fixed: 'left', resizable: true, ellipsis: true },
  { title: t('entity.document.documentcode'), dataIndex: 'documentCode', key: 'documentCode', width: 140, ellipsis: true, resizable: true },
  { title: t('entity.document.documenttitle'), dataIndex: 'documentTitle', key: 'documentTitle', width: 180, ellipsis: true, resizable: true },
  { title: t('entity.document.documenttype'), dataIndex: 'documentType', key: 'documentType', width: 90 },
  { title: t('entity.document.documentversion'), dataIndex: 'documentVersion', key: 'documentVersion', width: 90 },
  { title: t('entity.document.documentstatus'), dataIndex: 'documentStatus', key: 'documentStatus', width: 100 },
  { title: t('entity.document.applicantname'), dataIndex: 'applicantName', key: 'applicantName', width: 100 },
  { title: t('entity.document.applicantdeptname'), dataIndex: 'applicantDeptName', key: 'applicantDeptName', width: 120, ellipsis: true },
  { title: t('entity.document.direction'), dataIndex: 'direction', key: 'direction', width: 80 },
  { title: t('entity.document.lifecyclestage'), dataIndex: 'lifecycleStage', key: 'lifecycleStage', width: 100 },
  { title: t('entity.document.applytime'), dataIndex: 'applyTime', key: 'applyTime', width: 160 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'routine:document:update', onClick: (r: Document) => handleEdit(r) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:document:delete', onClick: (r: Document) => handleDeleteOne(r) }
    ]
  })
])

const getDocumentId = (record: any): string => String(record?.documentId ?? '')

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: any): string => (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Document[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Document, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.documentId === record?.documentId) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, rows: Document[]) => {
    selectedRow.value = selected && rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: Document) => ({
  onClick: () => {
    const key = record.documentId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.documentId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: DocumentQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.documentCode) params.documentCode = advancedQueryForm.value.documentCode
    if (advancedQueryForm.value.documentTitle) params.documentTitle = advancedQueryForm.value.documentTitle
    if (advancedQueryForm.value.documentType !== undefined) params.documentType = advancedQueryForm.value.documentType
    if (advancedQueryForm.value.documentStatus !== undefined) params.documentStatus = advancedQueryForm.value.documentStatus
    if (advancedQueryForm.value.direction !== undefined) params.direction = advancedQueryForm.value.direction
    if (advancedQueryForm.value.lifecycleStage !== undefined) params.lifecycleStage = advancedQueryForm.value.lifecycleStage

    const response = await getDocumentList(params) as any
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Document] 加载失败:', error)
    message.error(error?.message || t('common.msg.loadFail'))
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
  advancedQueryForm.value = {
    documentCode: '',
    documentTitle: '',
    documentType: undefined,
    documentStatus: undefined,
    direction: undefined,
    lifecycleStage: undefined
  }
  currentPage.value = 1
  loadData()
}

function handleTableChange(_pagination: any, _filters: any, _sorter: any) {}
function handleResizeColumn(_w: number, _col: any) {}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(current: number, size: number) {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('common.button.create') + t('entity.document._self')
  const uid = userStore.userInfo?.userId
  const name = userStore.userInfo?.realName || userStore.userInfo?.userName || ''
  formData.value = {
    applicantId: uid != null ? String(uid) : '',
    applicantName: name
  }
  formVisible.value = true
}

function handleEdit(record: Document) {
  formTitle.value = t('common.button.edit') + t('entity.document._self')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.update'), entity: t('entity.document._self') }))
}

function handleDeleteOne(record: Document) {
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.document._self'), name: record.documentTitle }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteDocument(record.documentId)
        message.success(t('common.msg.deleteSuccess', { target: t('entity.document._self') }))
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.document._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.document._self') }))
    return
  }
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.document._self') }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteDocumentBatch(selectedRows.value.map(r => r.documentId))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.document._self') }))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.document._self') }))
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
  advancedQueryForm.value = {
    documentCode: '',
    documentTitle: '',
    documentType: undefined,
    documentStatus: undefined,
    direction: undefined,
    lifecycleStage: undefined
  }
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

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if ('documentId' in values && values.documentId) {
      await updateDocument(values.documentId, values)
      message.success(t('common.msg.updateSuccess', { target: t('entity.document._self') }))
    } else {
      await createDocument(values)
      message.success(t('common.msg.createSuccess', { target: t('entity.document._self') }))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
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
.routine-docs-center {
  padding: 16px;
}
</style>
