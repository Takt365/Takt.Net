<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/human-resource/personnel/employee-transfer -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：员工调动管理页面，包含列表、查询、新增、编辑、删除、状态展示、导出等功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-personnel-employee-transfer">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.employeetransfer.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="humanresource:personnel:employeetransfer:create"
      update-permission="humanresource:personnel:employeetransfer:update"
      delete-permission="humanresource:personnel:employeetransfer:delete"
      export-permission="humanresource:personnel:employeetransfer:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="false"
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
        <template v-if="column.key === 'transferType'">
          {{ getTransferTypeLabel(getField(record, 'transferType')) }}
        </template>
        <template v-else-if="column.key === 'transferStatus'">
          {{ getTransferStatusLabel(getField(record, 'transferStatus')) }}
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
      <EmployeeTransferForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.employeetransfer.employeeId')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.employeetransfer.transferType')">
        <a-select
          v-model:value="advancedQueryForm.transferType"
          :options="transferTypeOptions"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.employeetransfer.transferStatus')">
        <a-select
          v-model:value="advancedQueryForm.transferStatus"
          :options="transferStatusOptions"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

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
import EmployeeTransferForm from './components/transfer-form.vue'
import { getEmployeeTransferList, getEmployeeTransferById, createEmployeeTransfer, updateEmployeeTransfer, deleteEmployeeTransferById, deleteEmployeeTransferBatch, exportEmployeeTransferData } from '@/api/human-resource/personnel/employee-transfer'
import type { EmployeeTransfer } from '@/types/human-resource/personnel/employee-transfer'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const entityKey = 'entity.employeetransfer'
const transferTypeOptions = [{ label: t(`${entityKey}.transferType0`), value: 0 }, { label: t(`${entityKey}.transferType1`), value: 1 }]
const transferStatusOptions = [0, 1, 2, 3, 4].map((v) => ({ label: t(`${entityKey}.transferStatus${v}`), value: v }))

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<EmployeeTransfer[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<EmployeeTransfer | null>(null)
const selectedRows = ref<EmployeeTransfer[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<EmployeeTransfer>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ employeeId?: string; transferType?: number; transferStatus?: number }>({ employeeId: '', transferType: undefined, transferStatus: undefined })
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 初始化时加载数据
onMounted(() => loadData())
const getRowId = (r: any) => (r?.transferId != null ? String(r.transferId) : r?.id != null ? String(r.id) : '')
const getField = (r: any, f: string) => r?.[f]
function getTransferTypeLabel(v: number | undefined): string { return v !== undefined && v !== null ? t(`${entityKey}.transferType${v}`) : '-' }
function getTransferStatusLabel(v: number | undefined): string { return v !== undefined && v !== null ? t(`${entityKey}.transferStatus${v}`) : '-' }

const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'transferId', key: 'id', width: 80, resizable: true, ellipsis: true, fixed: 'left', customRender: ({ record }: { record: any }) => getField(record, 'transferId') ?? getField(record, 'id') ?? '' },
  { title: t(`${entityKey}.employeeId`), dataIndex: 'employeeId', key: 'employeeId', width: 120, ellipsis: true },
  { title: t(`${entityKey}.transferType`), dataIndex: 'transferType', key: 'transferType', width: 80 },
  { title: t(`${entityKey}.fromDeptName`), dataIndex: 'fromDeptName', key: 'fromDeptName', width: 120, ellipsis: true },
  { title: t(`${entityKey}.toDeptName`), dataIndex: 'toDeptName', key: 'toDeptName', width: 120, ellipsis: true },
  { title: t(`${entityKey}.effectiveDate`), dataIndex: 'effectiveDate', key: 'effectiveDate', width: 120 },
  { title: t(`${entityKey}.transferStatus`), dataIndex: 'transferStatus', key: 'transferStatus', width: 100 },
  { title: t('common.entity.createTime'), dataIndex: 'createdAt', key: 'createdAt', width: 160, ellipsis: true },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'humanresource:personnel:employeetransfer:update', onClick: (r: EmployeeTransfer) => handleEdit(r) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'humanresource:personnel:employeetransfer:delete', onClick: (r: EmployeeTransfer) => handleDeleteOne(r) }
    ]
  })
])
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  if (keys.length === 0) return columns.value
  return mergedColumns.value.filter((c: any) => new Set(keys.map((k) => String(k))).has(String(c.key || c.dataIndex || c.title || '')))
})
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: EmployeeTransfer[]) => { selectedRowKeys.value = keys; selectedRows.value = rows; selectedRow.value = rows.length === 1 ? rows[0] : null },
  onSelect: (r: EmployeeTransfer, selected: boolean) => { if (selected) selectedRow.value = r; else if (selectedRow.value && getRowId(selectedRow.value) === getRowId(r)) selectedRow.value = null },
  onSelectAll: (_s: boolean, rows: EmployeeTransfer[]) => { selectedRow.value = rows.length === 1 ? rows[0] : null }
}))
const onClickRow = (record: EmployeeTransfer) => ({
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
    if (advancedQueryForm.value.transferType !== undefined && advancedQueryForm.value.transferType !== null) params.transferType = advancedQueryForm.value.transferType
    if (advancedQueryForm.value.transferStatus !== undefined && advancedQueryForm.value.transferStatus !== null) params.transferStatus = advancedQueryForm.value.transferStatus
    const res = await getEmployeeTransferList(params)
    dataSource.value = (res as any)?.data ?? (res as any)?.Data ?? []
    total.value = (res as any)?.total ?? (res as any)?.Total ?? 0
  } catch (e: any) { logger.error('[EmployeeTransfer] loadData:', e); message.error(e?.message || t('common.msg.loadFail')); dataSource.value = []; total.value = 0 } finally { loading.value = false }
}
function handleSearch() { currentPage.value = 1; loadData() }
function handleReset() { queryKeyword.value = ''; advancedQueryForm.value = { employeeId: '', transferType: undefined, transferStatus: undefined }; currentPage.value = 1; loadData() }
function handleTableChange() {}
function handlePaginationChange(page: number, size: number) { currentPage.value = page; pageSize.value = size; loadData() }
function handlePaginationSizeChange(_c: number, size: number) { currentPage.value = 1; pageSize.value = size; loadData() }
function handleResizeColumn() {}
function handleCreate() { formTitle.value = t('common.button.create') + t(`${entityKey}._self`); formData.value = {}; formVisible.value = true }
async function handleEdit(record: EmployeeTransfer) {
  formTitle.value = t('common.button.edit') + t(`${entityKey}._self`)
  try { formLoading.value = true; formData.value = { ...(await getEmployeeTransferById(getRowId(record))) }; formVisible.value = true } catch (e: any) { message.error(e?.message || t('common.msg.loadFail')) } finally { formLoading.value = false }
}
function handleUpdate() { if (selectedRow.value) handleEdit(selectedRow.value); else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t(`${entityKey}._self`) })) }
function handleDeleteOne(record: EmployeeTransfer) {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t(`${entityKey}._self`), name: getField(record, 'reason') || getRowId(record) }),
    okText: t('common.button.delete'), cancelText: t('common.button.cancel'),
    onOk: async () => { try { loading.value = true; await deleteEmployeeTransferById(getRowId(record)); message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) })); loadData() } catch (e: any) { message.error(e?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false } }
  })
}
function handleDelete() {
  if (selectedRows.value.length === 0) { message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t(`${entityKey}._self`) })); return }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t(`${entityKey}._self`), count: selectedRows.value.length }),
    okText: t('common.button.delete'), cancelText: t('common.button.cancel'),
    onOk: async () => { try { loading.value = true; selectedRows.value.length === 1 ? await deleteEmployeeTransferById(getRowId(selectedRows.value[0])) : await deleteEmployeeTransferBatch(selectedRows.value.map((r) => getRowId(r))); message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) })); selectedRows.value = []; selectedRowKeys.value = []; selectedRow.value = null; loadData() } catch (e: any) { message.error(e?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false } }
  })
}
async function handleFormSubmit() {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.transferId) { await updateEmployeeTransfer(formData.value.transferId, { ...values, transferId: formData.value.transferId }); message.success(t('common.msg.updateSuccess', { target: t(`${entityKey}._self`) })) }
    else { await createEmployeeTransfer(values); message.success(t('common.msg.createSuccess', { target: t(`${entityKey}._self`) })) }
    formRef.value?.resetFields(); formData.value = {}; formVisible.value = false; loadData()
  } catch (e: any) { if (e?.errorFields) return; message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') })) } finally { formLoading.value = false }
}
function handleFormCancel() { formVisible.value = false; formData.value = {}; formRef.value?.resetFields() }
async function handleExport() {
  try {
    loading.value = true
    const queryParams: any = { pageIndex: 1, pageSize: total.value || 9999 }; if (queryKeyword.value) queryParams.employeeId = queryKeyword.value; if (advancedQueryForm.value.employeeId) queryParams.employeeId = advancedQueryForm.value.employeeId; if (advancedQueryForm.value.transferType !== undefined && advancedQueryForm.value.transferType !== null) queryParams.transferType = advancedQueryForm.value.transferType; if (advancedQueryForm.value.transferStatus !== undefined && advancedQueryForm.value.transferStatus !== null) queryParams.transferStatus = advancedQueryForm.value.transferStatus
    const blob = await exportEmployeeTransferData(queryParams, undefined, t(`${entityKey}._self`) + t('common.action.exportDataSuffix'))
    const data = (blob as any)?.data ?? blob
    const ts = new Date(); const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const name = `${t(`${entityKey}._self`) + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(data); const link = document.createElement('a'); link.href = url; link.download = name; link.style.display = 'none'; document.body.appendChild(link); link.click(); document.body.removeChild(link); setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t(`${entityKey}._self`) }))
  } catch (e: any) { logger.error('[EmployeeTransfer] export:', e); message.error(e?.message || t('common.msg.exportFail', { target: t(`${entityKey}._self`) })) } finally { loading.value = false }
}
function handleAdvancedQuery() { advancedQueryVisible.value = true }
function handleAdvancedQuerySubmit() { currentPage.value = 1; loadData(); advancedQueryVisible.value = false }
function handleAdvancedQueryReset() { advancedQueryForm.value = { employeeId: '', transferType: undefined, transferStatus: undefined } }
function handleColumnSetting() { columnSettingVisible.value = true }
function handleColumnKeysChange(keys: (string | number)[]) { visibleColumnKeys.value = keys.map((k) => String(k)) }
function handleColumnSettingReset() { visibleColumnKeys.value = [] }
function handleRefresh() { loadData() }
defineExpose({ tableRef })
</script>
<style scoped lang="less">.humanresource-personnel-employee-transfer { padding: 16px; display: flex; flex-direction: column; min-height: 0; }</style>
