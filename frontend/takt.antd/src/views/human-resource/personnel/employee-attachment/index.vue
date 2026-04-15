<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/human-resource/personnel/employee-attachment -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：员工附件管理页面，包含列表、查询、新增、编辑、删除、导入、模板下载、导出等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-personnel-employee-attachment">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.employeeattachment.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="humanresource:personnel:employeeattachment:create"
      update-permission="humanresource:personnel:employeeattachment:update"
      delete-permission="humanresource:personnel:employeeattachment:delete"
      import-permission="humanresource:personnel:employeeattachment:import"
      template-permission="humanresource:personnel:employeeattachment:template"
      export-permission="humanresource:personnel:employeeattachment:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :update-disabled="!selectedRow"
      :delete-disabled="!selectedRow && selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @import="handleImport"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <!-- 表格 -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getRowId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="8"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'attachmentType'">
          {{ getAttachmentTypeLabel(getField(record, 'attachmentType')) }}
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

    <!-- 新增/编辑对话框：视口宽 50%、高 75vh，可拖拽调整宽高 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <EmployeeAttachmentForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.employeeattachment.employeeId')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.employeeattachment.attachmentType')">
        <a-select v-model:value="advancedQueryForm.attachmentType" :options="attachmentTypeOptions" allow-clear style="width: 100%" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.employeeattachment._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="importVisible = false"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.employeeattachment._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.employeeattachment._self') })"
        template-permission="humanresource:personnel:employeeattachment:template"
        import-permission="humanresource:personnel:employeeattachment:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.employeeattachment._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'id'"
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
import EmployeeAttachmentForm from './components/attachment-form.vue'
import {
  getEmployeeAttachmentList,
  getEmployeeAttachmentById,
  createEmployeeAttachment,
  updateEmployeeAttachment,
  deleteEmployeeAttachmentById,
  deleteEmployeeAttachmentBatch,
  getEmployeeAttachmentTemplate,
  importEmployeeAttachmentData,
  exportEmployeeAttachmentData
} from '@/api/human-resource/personnel/employee-attachment'
import type { EmployeeAttachment } from '@/types/human-resource/personnel/employee-attachment'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const entityKey = 'entity.employeeattachment'

const attachmentTypeOptions = [0, 1, 2, 3, 4, 5].map((v) => ({ label: t(`${entityKey}.attachmentType${v}`), value: v }))

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<EmployeeAttachment[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<EmployeeAttachment | null>(null)
const selectedRows = ref<EmployeeAttachment[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<EmployeeAttachment>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ employeeId?: string; attachmentType?: number }>({ employeeId: '', attachmentType: undefined })
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 初始化时加载数据
onMounted(() => loadData())

const getRowId = (r: any) => (r?.attachmentId != null ? String(r.attachmentId) : r?.id != null ? String(r.id) : '')
const getField = (r: any, f: string) => r?.[f]

function getAttachmentTypeLabel(v: number | undefined): string {
  if (v === undefined || v === null) return '-'
  return t(`${entityKey}.attachmentType${v}`)
}

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'attachmentId', key: 'id', width: 80, resizable: true, ellipsis: true, fixed: 'left', customRender: ({ record }: { record: any }) => getField(record, 'attachmentId') ?? getField(record, 'id') ?? '' },
  { title: t(`${entityKey}.employeeId`), dataIndex: 'employeeId', key: 'employeeId', width: 120, ellipsis: true },
  { title: t(`${entityKey}.fileName`), dataIndex: 'fileName', key: 'fileName', width: 160, ellipsis: true },
  { title: t(`${entityKey}.fileType`), dataIndex: 'fileType', key: 'fileType', width: 100 },
  { title: t(`${entityKey}.attachmentType`), dataIndex: 'attachmentType', key: 'attachmentType', width: 100 },
  { title: t(`${entityKey}.orderNum`), dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: t('common.entity.createTime'), dataIndex: 'createdAt', key: 'createdAt', width: 160, ellipsis: true },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'humanresource:personnel:employeeattachment:update', onClick: (r: EmployeeAttachment) => handleEdit(r) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'humanresource:personnel:employeeattachment:delete', onClick: (r: EmployeeAttachment) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  if (keys.length === 0) return columns.value
  const getColKey = (c: any) => String(c.key || c.dataIndex || c.title || '')
  const set = new Set(keys.map((k) => String(k)))
  return mergedColumns.value.filter((c: any) => set.has(getColKey(c)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: EmployeeAttachment[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (r: EmployeeAttachment, selected: boolean) => { if (selected) selectedRow.value = r; else if (selectedRow.value && getRowId(selectedRow.value) === getRowId(r)) selectedRow.value = null },
  onSelectAll: (_s: boolean, rows: EmployeeAttachment[]) => { selectedRow.value = rows.length === 1 ? rows[0] : null }
}))

const onClickRow = (record: EmployeeAttachment) => ({
  onClick: () => {
    const key = getRowId(record)
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getRowId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: any = { pageIndex: currentPage.value, pageSize: pageSize.value }
    if (queryKeyword.value) params.employeeId = queryKeyword.value
    if (advancedQueryForm.value.employeeId) params.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.attachmentType !== undefined && advancedQueryForm.value.attachmentType !== null) params.attachmentType = advancedQueryForm.value.attachmentType
    const res = await getEmployeeAttachmentList(params)
    const data = (res as any)?.data ?? (res as any)?.Data ?? []
    const tot = (res as any)?.total ?? (res as any)?.Total ?? 0
    dataSource.value = data
    total.value = tot
  } catch (e: any) {
    logger.error('[EmployeeAttachment] loadData:', e)
    message.error(e?.message || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() { currentPage.value = 1; loadData() }
function handleReset() { queryKeyword.value = ''; advancedQueryForm.value = { employeeId: '', attachmentType: undefined }; currentPage.value = 1; loadData() }
function handleTableChange(_p: any, _f: any, _s: any) {}
function handlePaginationChange(page: number, size: number) { currentPage.value = page; pageSize.value = size; loadData() }
function handlePaginationSizeChange(_c: number, size: number) { currentPage.value = 1; pageSize.value = size; loadData() }
function handleResizeColumn(_w: number, _col: any) {}

function handleCreate() { formTitle.value = t('common.button.create') + t(`${entityKey}._self`); formData.value = {}; formVisible.value = true }
async function handleEdit(record: EmployeeAttachment) {
  formTitle.value = t('common.button.edit') + t(`${entityKey}._self`)
  try {
    formLoading.value = true
    const detail = await getEmployeeAttachmentById(getRowId(record))
    formData.value = { ...detail }
    formVisible.value = true
  } catch (e: any) { message.error(e?.message || t('common.msg.loadFail')) } finally { formLoading.value = false }
}
function handleUpdate() { if (selectedRow.value) handleEdit(selectedRow.value); else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t(`${entityKey}._self`) })) }

function handleDeleteOne(record: EmployeeAttachment) {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t(`${entityKey}._self`), name: getField(record, 'fileName') || getRowId(record) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => { try { loading.value = true; await deleteEmployeeAttachmentById(getRowId(record)); message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) })); loadData() } catch (e: any) { message.error(e?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false } }
  })
}
function handleDelete() {
  if (selectedRows.value.length === 0) { message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t(`${entityKey}._self`) })); return }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t(`${entityKey}._self`), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) await deleteEmployeeAttachmentById(getRowId(selectedRows.value[0]))
        else await deleteEmployeeAttachmentBatch(selectedRows.value.map((r) => getRowId(r)))
        message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) }))
        selectedRows.value = []; selectedRowKeys.value = []; selectedRow.value = null
        loadData()
      } catch (e: any) { message.error(e?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false }
    }
  })
}

async function handleFormSubmit() {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.attachmentId) {
      await updateEmployeeAttachment(formData.value.attachmentId, { ...values, attachmentId: formData.value.attachmentId })
      message.success(t('common.msg.updateSuccess', { target: t(`${entityKey}._self`) }))
    } else {
      await createEmployeeAttachment(values)
      message.success(t('common.msg.createSuccess', { target: t(`${entityKey}._self`) }))
    }
    formRef.value?.resetFields(); formData.value = {}; formVisible.value = false
    loadData()
  } catch (e: any) {
    // AntD 表单校验失败时会携带 errorFields，不需要弹出重复错误
    if (e?.errorFields) return
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}
function handleFormCancel() { formVisible.value = false; formData.value = {}; formRef.value?.resetFields() }

function handleImport() { importVisible.value = true }
async function handleDownloadTemplate(sheetName?: string, fileName?: string) {
  return await getEmployeeAttachmentTemplate(sheetName, fileName)
}
async function handleImportFile(file: File, sheetName?: string) { return importEmployeeAttachmentData(file, sheetName) }
function handleImportSuccess(result: { success: number; fail: number; errors: string[] }) { loadData(); if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000) }

async function handleExport() {
  try {
    loading.value = true
    const queryParams: any = { pageIndex: 1, pageSize: total.value || 9999 }
    if (queryKeyword.value) queryParams.employeeId = queryKeyword.value
    if (advancedQueryForm.value.employeeId) queryParams.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.attachmentType !== undefined && advancedQueryForm.value.attachmentType !== null) queryParams.attachmentType = advancedQueryForm.value.attachmentType
    const blob = await exportEmployeeAttachmentData(queryParams, undefined, t(`${entityKey}._self`) + t('common.action.exportDataSuffix'))
    const data = (blob as any)?.data ?? blob
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const name = `${t(`${entityKey}._self`) + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(data)
    const link = document.createElement('a'); link.href = url; link.download = name; link.style.display = 'none'; document.body.appendChild(link); link.click(); document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t(`${entityKey}._self`) }))
  } catch (e: any) { logger.error('[EmployeeAttachment] export:', e); message.error(e?.message || t('common.msg.exportFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false }
}

function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = { employeeId: '', attachmentType: undefined } }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map((k) => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }

defineExpose({ tableRef })
</script>

<style scoped lang="less">
.humanresource-personnel-employee-attachment { padding: 16px; display: flex; flex-direction: column; min-height: 0; }
</style>
