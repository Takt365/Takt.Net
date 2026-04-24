<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-source -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：考勤源记录列表页。分页、关键字、高级查询、CRUD、导入导出、列设置；与请假/加班列表骨架一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-source">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendancesource.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendancesource:create"
      update-permission="humanresource:attendanceleave:attendancesource:update"
      delete-permission="humanresource:attendanceleave:attendancesource:delete"
      import-permission="humanresource:attendanceleave:attendancesource:import"
      template-permission="humanresource:attendanceleave:attendancesource:template"
      export-permission="humanresource:attendanceleave:attendancesource:export"
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

    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getSourceId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    />

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
      width="800px"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AttendanceSourceForm
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
      <a-form-item :label="t('entity.attendancesource.deviceid')">
        <a-input v-model:value="advancedQueryForm.deviceId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancesource.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancesource.rawpunchtimefrom')">
        <a-date-picker
          v-model:value="advancedQueryForm.from"
          show-time
          value-format="YYYY-MM-DD HH:mm:ss"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendancesource.rawpunchtimeto')">
        <a-date-picker
          v-model:value="advancedQueryForm.to"
          show-time
          value-format="YYYY-MM-DD HH:mm:ss"
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendancesource._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="sourceExcelNames.sheet"
        :template-file-name="sourceExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendancesource:template"
        import-permission="humanresource:attendanceleave:attendancesource:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.attendancesource._self') })"
        :upload-text="t('common.action.import.uploadtext')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

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
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import AttendanceSourceForm from './components/attendance-source-form.vue'
import {
  getAttendanceSourceList,
  createAttendanceSource,
  updateAttendanceSource,
  deleteAttendanceSourceById,
  deleteAttendanceSourceBatch,
  getAttendanceSourceTemplate,
  importAttendanceSourceData,
  exportAttendanceSourceData
} from '@/api/human-resource/attendance-leave/attendance-source'
import type {
  AttendanceSource,
  AttendanceSourceCreate,
  AttendanceSourceQuery,
  AttendanceSourceUpdate
} from '@/types/human-resource/attendance-leave/attendance-source'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const sourceExcelNames = taktExcelEntityNames('TaktAttendanceSource')
const entitySelf = computed(() => t('entity.attendancesource._self'))

type SourceTableColumn = TableColumnsType[number]

interface TableSorterLike {
  readonly field?: string | string[]
  readonly order?: 'ascend' | 'descend' | null
}

function getErrorMessage(err: unknown): string | undefined {
  if (err instanceof Error) return err.message
  if (typeof err === 'object' && err !== null && 'message' in err) {
    const m = (err as { message?: unknown }).message
    return typeof m === 'string' ? m : undefined
  }
  return undefined
}

function getSourceColumnKey(col: SourceTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  return c.key || c.dataIndex || c.title ? String(c.key ?? c.dataIndex ?? c.title) : ''
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendanceSource[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendanceSource | null>(null)
const selectedRows = ref<AttendanceSource[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendanceSource>>({})
const formLoading = ref(false)
type SourceFormExposed = InstanceType<typeof AttendanceSourceForm>
const formRef = ref<SourceFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({ deviceId: '', employeeId: '', from: '', to: '' })
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getSourceId = (record: AttendanceSource): string => {
  if (record?.sourceId != null && String(record.sourceId) !== '') return String(record.sourceId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'sourceId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendanceSource }) =>
      record.sourceId ?? (record as unknown as Record<string, unknown>)['id'] ?? ''
  },
  { title: t('entity.attendancesource.devicecode'), dataIndex: 'deviceCode', key: 'deviceCode', width: 100, ellipsis: true, resizable: true },
  { title: t('entity.attendancesource.deviceid'), dataIndex: 'deviceId', key: 'deviceId', width: 88, resizable: true },
  { title: t('entity.attendancesource.employeeid'), dataIndex: 'employeeId', key: 'employeeId', width: 88, resizable: true },
  { title: t('entity.attendancesource.enrollnumber'), dataIndex: 'enrollNumber', key: 'enrollNumber', width: 100, resizable: true },
  { title: t('entity.attendancesource.rawpunchtime'), dataIndex: 'rawPunchTime', key: 'rawPunchTime', width: 168, resizable: true },
  { title: t('entity.attendancesource.verifymode'), dataIndex: 'verifyMode', key: 'verifyMode', width: 64, resizable: true },
  CreateActionColumn<AttendanceSource>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendancesource:update',
        onClick: (record: AttendanceSource) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendancesource:delete',
        onClick: (record: AttendanceSource) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): TableColumnsType => mergeDefaultColumns(columns.value, t, true))

const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: SourceTableColumn) => {
    const colKey = getSourceColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceSource[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceSource, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getSourceId(selectedRow.value) === getSourceId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceSource[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendanceSource) => ({
  onClick: () => {
    const key = getSourceId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getSourceId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: AttendanceSourceQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const did = advancedQueryForm.value.deviceId?.trim()
    if (did) {
      const n = Number(did)
      if (!Number.isNaN(n)) params.deviceId = n
    }
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) params.employeeId = n
    }
    if (advancedQueryForm.value.from) params.rawPunchTimeFrom = advancedQueryForm.value.from.replace(' ', 'T')
    if (advancedQueryForm.value.to) params.rawPunchTimeTo = advancedQueryForm.value.to.replace(' ', 'T')

    const response: TaktPagedResult<AttendanceSource> = await getAttendanceSourceList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceSource] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadfail'))
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
  advancedQueryForm.value = { deviceId: '', employeeId: '', from: '', to: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceSource] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: SourceTableColumn) => {
  const column = columns.value.find((c: SourceTableColumn) => {
    const a = getSourceColumnKey(col)
    const b = getSourceColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as SourceTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + entitySelf.value
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceSource) => {
  formTitle.value = t('common.button.edit') + entitySelf.value
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: entitySelf.value }))
}

const handleDeleteOne = (record: AttendanceSource) => {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: entitySelf.value, name: getSourceId(record) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceSourceById(getSourceId(record))
        message.success(t('common.msg.deletesuccess', { target: entitySelf.value }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: entitySelf.value }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: entitySelf.value }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: entitySelf.value, count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendanceSourceById(getSourceId(selectedRows.value[0]))
        } else {
          await deleteAttendanceSourceBatch(selectedRows.value.map((r) => getSourceId(r)))
        }
        message.success(t('common.msg.deletesuccess', { target: entitySelf.value }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: entitySelf.value }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    const id = formData.value.sourceId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendanceSourceUpdate = { ...(formValues as AttendanceSourceCreate), sourceId: idStr }
      await updateAttendanceSource(idStr, payload)
      message.success(t('common.msg.updatesuccess', { target: entitySelf.value }))
    } else {
      await createAttendanceSource(formValues as AttendanceSourceCreate)
      message.success(t('common.msg.createsuccess', { target: entitySelf.value }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operatefail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

const handleImport = () => {
  importVisible.value = true
}

const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getAttendanceSourceTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceSourceData(file, sheetName)
}

const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

const handleImportCancel = () => {
  importVisible.value = false
}

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: Record<string, unknown> = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    const did = advancedQueryForm.value.deviceId?.trim()
    if (did) {
      const n = Number(did)
      if (!Number.isNaN(n)) queryParams.DeviceId = n
    }
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) queryParams.EmployeeId = n
    }
    if (advancedQueryForm.value.from) queryParams.RawPunchTimeFrom = advancedQueryForm.value.from.replace(' ', 'T')
    if (advancedQueryForm.value.to) queryParams.RawPunchTimeTo = advancedQueryForm.value.to.replace(' ', 'T')

    const blob = await exportAttendanceSourceData(
      queryParams,
      sourceExcelNames.sheet,
      sourceExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${sourceExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess', { target: entitySelf.value }))
  } catch (error: unknown) {
    logger.error('[AttendanceSource] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportfail', { target: entitySelf.value }))
  } finally {
    loading.value = false
  }
}

const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { deviceId: '', employeeId: '', from: '', to: '' }
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

const handleRefresh = () => {
  loadData()
}

defineExpose({ tableRef })
</script>

<style scoped lang="less">
.humanresource-attendance-leave-source {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
