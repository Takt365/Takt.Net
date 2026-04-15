<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/clock-in -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：打卡记录列表页。分页、关键字、高级查询、CRUD、导入导出、列设置；与请假/加班列表骨架一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-clockin">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendancepunch.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="humanresource:attendanceleave:clockin:create"
      update-permission="humanresource:attendanceleave:clockin:update"
      delete-permission="humanresource:attendanceleave:clockin:delete"
      import-permission="humanresource:attendanceleave:clockin:import"
      template-permission="humanresource:attendanceleave:clockin:template"
      export-permission="humanresource:attendanceleave:clockin:export"
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
      :row-key="getPunchId"
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
      width="720px"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AttendancePunchForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendancepunch.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancepunch.punchtype')">
        <a-select v-model:value="advancedQueryForm.punchType" allow-clear style="width: 100%" :options="punchTypeOptions" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancepunch.punchtimefrom')">
        <a-date-picker
          v-model:value="advancedQueryForm.from"
          show-time
          value-format="YYYY-MM-DD HH:mm:ss"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendancepunch.punchtimeto')">
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
      :title="t('common.button.import') + t('entity.attendancepunch._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="punchExcelNames.sheet"
        :template-file-name="punchExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:clockin:template"
        import-permission="humanresource:attendanceleave:clockin:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.attendancepunch._self') })"
        :upload-text="t('common.action.import.uploadText')"
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
import AttendancePunchForm from './components/attendance-punch-form.vue'
import {
  getAttendancePunchList,
  createAttendancePunch,
  updateAttendancePunch,
  deleteAttendancePunchById,
  deleteAttendancePunchBatch,
  getAttendancePunchTemplate,
  importAttendancePunchData,
  exportAttendancePunchData
} from '@/api/human-resource/attendance-leave/attendance-punch'
import type {
  AttendancePunch,
  AttendancePunchCreate,
  AttendancePunchQuery,
  AttendancePunchUpdate
} from '@/types/human-resource/attendance-leave/attendance-punch'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const punchExcelNames = taktExcelEntityNames('TaktAttendancePunch')

const entitySelf = computed(() => t('entity.attendancepunch._self'))

const punchTypeOptions = computed(() => [
  { label: t('entity.attendancepunch.punchtypeenum.1'), value: 1 },
  { label: t('entity.attendancepunch.punchtypeenum.2'), value: 2 },
  { label: t('entity.attendancepunch.punchtypeenum.3'), value: 3 }
])

type PunchTableColumn = TableColumnsType[number]

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

function getPunchColumnKey(col: PunchTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  return c.key || c.dataIndex || c.title ? String(c.key ?? c.dataIndex ?? c.title) : ''
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendancePunch[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendancePunch | null>(null)
const selectedRows = ref<AttendancePunch[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendancePunch>>({})
const formLoading = ref(false)
type PunchFormExposed = InstanceType<typeof AttendancePunchForm>
const formRef = ref<PunchFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  employeeId: string
  punchType: number | undefined
  from: string
  to: string
}>({
  employeeId: '',
  punchType: undefined,
  from: '',
  to: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getPunchId = (record: AttendancePunch): string => {
  if (record?.punchId != null && String(record.punchId) !== '') return String(record.punchId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'punchId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendancePunch }) =>
      record.punchId ?? (record as unknown as Record<string, unknown>)['id'] ?? ''
  },
  { title: t('entity.attendancepunch.employeeid'), dataIndex: 'employeeId', key: 'employeeId', width: 100, resizable: true },
  { title: t('entity.attendancepunch.punchtime'), dataIndex: 'punchTime', key: 'punchTime', width: 168, resizable: true },
  { title: t('entity.attendancepunch.punchtype'), dataIndex: 'punchType', key: 'punchType', width: 72, resizable: true },
  { title: t('entity.attendancepunch.punchsource'), dataIndex: 'punchSource', key: 'punchSource', width: 88, resizable: true },
  { title: t('entity.attendancepunch.punchaddress'), dataIndex: 'punchAddress', key: 'punchAddress', ellipsis: true, resizable: true },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:clockin:update',
        onClick: (record: AttendancePunch) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:clockin:delete',
        onClick: (record: AttendancePunch) => handleDeleteOne(record)
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
  return merged.filter((col: PunchTableColumn) => {
    const colKey = getPunchColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendancePunch[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendancePunch, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getPunchId(selectedRow.value) === getPunchId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendancePunch[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendancePunch) => ({
  onClick: () => {
    const key = getPunchId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getPunchId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: AttendancePunchQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) params.employeeId = n
    }
    if (advancedQueryForm.value.punchType != null) params.punchType = advancedQueryForm.value.punchType
    if (advancedQueryForm.value.from) params.punchTimeFrom = advancedQueryForm.value.from.replace(' ', 'T')
    if (advancedQueryForm.value.to) params.punchTimeTo = advancedQueryForm.value.to.replace(' ', 'T')

    const response: TaktPagedResult<AttendancePunch> = await getAttendancePunchList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendancePunch] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadFail'))
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
  advancedQueryForm.value = { employeeId: '', punchType: undefined, from: '', to: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendancePunch] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: PunchTableColumn) => {
  const column = columns.value.find((c: PunchTableColumn) => {
    const a = getPunchColumnKey(col)
    const b = getPunchColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as PunchTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + entitySelf.value
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendancePunch) => {
  formTitle.value = t('common.button.edit') + entitySelf.value
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: entitySelf.value }))
}

const handleDeleteOne = (record: AttendancePunch) => {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: entitySelf.value, name: getPunchId(record) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendancePunchById(getPunchId(record))
        message.success(t('common.msg.deleteSuccess', { target: entitySelf.value }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: entitySelf.value }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: entitySelf.value }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: entitySelf.value, count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendancePunchById(getPunchId(selectedRows.value[0]))
        } else {
          await deleteAttendancePunchBatch(selectedRows.value.map((r) => getPunchId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: entitySelf.value }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: entitySelf.value }))
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
    const id = formData.value.punchId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendancePunchUpdate = { ...(formValues as AttendancePunchCreate), punchId: idStr }
      await updateAttendancePunch(idStr, payload)
      message.success(t('common.msg.updateSuccess', { target: entitySelf.value }))
    } else {
      await createAttendancePunch(formValues as AttendancePunchCreate)
      message.success(t('common.msg.createSuccess', { target: entitySelf.value }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operateFail', { action: t('common.action.operation') }))
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
  return await getAttendancePunchTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendancePunchData(file, sheetName)
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
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) queryParams.EmployeeId = n
    }
    if (advancedQueryForm.value.punchType != null) queryParams.PunchType = advancedQueryForm.value.punchType
    if (advancedQueryForm.value.from) queryParams.PunchTimeFrom = advancedQueryForm.value.from.replace(' ', 'T')
    if (advancedQueryForm.value.to) queryParams.PunchTimeTo = advancedQueryForm.value.to.replace(' ', 'T')

    const blob = await exportAttendancePunchData(
      queryParams,
      punchExcelNames.sheet,
      punchExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob as Blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${punchExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: entitySelf.value }))
  } catch (error: unknown) {
    logger.error('[AttendancePunch] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportFail', { target: entitySelf.value }))
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
  advancedQueryForm.value = { employeeId: '', punchType: undefined, from: '', to: '' }
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
.humanresource-attendance-leave-clockin {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
