<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-exception -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：考勤异常列表页。关键字占位与 entity.attendanceexception.keyword 检索（与 common.form.placeholder.search 拼接）；高级查询字段对应后端 TaktAttendanceExceptionQueryDto（前端 AttendanceExceptionQuery）；CRUD、导入导出、列设置。API @/api/human-resource/attendance-leave/attendance-exception，权限与 TaktAttendanceExceptionsController 一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-exception">
    <!-- 顶部：关键字（entity.attendanceexception.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendanceexception.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktAttendanceExceptionsController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendanceexception:create"
      update-permission="humanresource:attendanceleave:attendanceexception:update"
      delete-permission="humanresource:attendanceleave:attendanceexception:delete"
      import-permission="humanresource:attendanceleave:attendanceexception:import"
      template-permission="humanresource:attendanceleave:attendanceexception:template"
      export-permission="humanresource:attendanceleave:attendanceexception:export"
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

    <!-- 主表：考勤异常列表 -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getExceptionId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    />

    <!-- 底部分页 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 新增 / 编辑 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AttendanceExceptionForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询：字段与 TaktAttendanceExceptionQueryDto 一致（前端 AttendanceExceptionQuery） -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendanceexception.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceexception.exceptiontype')">
        <a-input-number
          v-model:value="advancedQueryForm.exceptionType"
          :min="1"
          style="width: 100%"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceexception.handlestatus')">
        <a-input-number
          v-model:value="advancedQueryForm.handleStatus"
          :min="0"
          :max="2"
          style="width: 100%"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceexception.datefrom')">
        <a-date-picker
          v-model:value="advancedQueryForm.from"
          value-format="YYYY-MM-DD"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceexception.dateto')">
        <a-date-picker
          v-model:value="advancedQueryForm.to"
          value-format="YYYY-MM-DD"
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet / 文件名与后端 TaktNamingHelper.ResolveExcelImportExport 及实体 TaktAttendanceException 命名约定一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendanceexception._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="exceptionExcelNames.sheet"
        :template-file-name="exceptionExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendanceexception:template"
        import-permission="humanresource:attendanceleave:attendanceexception:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.attendanceexception._self') })"
        :upload-text="t('common.action.import.uploadtext')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置 -->
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
import AttendanceExceptionForm from './components/attendance-exception-form.vue'
import {
  getAttendanceExceptionList,
  createAttendanceException,
  updateAttendanceException,
  deleteAttendanceExceptionById,
  deleteAttendanceExceptionBatch,
  getAttendanceExceptionTemplate,
  importAttendanceExceptionData,
  exportAttendanceExceptionData
} from '@/api/human-resource/attendance-leave/attendance-exception'
import type {
  AttendanceException,
  AttendanceExceptionCreate,
  AttendanceExceptionQuery,
  AttendanceExceptionUpdate
} from '@/types/human-resource/attendance-leave/attendance-exception'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const exceptionExcelNames = taktExcelEntityNames('TaktAttendanceException')

type ExceptionTableColumn = TableColumnsType[number]

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

function getExceptionColumnKey(col: ExceptionTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendanceException[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendanceException | null>(null)
const selectedRows = ref<AttendanceException[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendanceException>>({})
const formLoading = ref(false)
type ExceptionFormExposed = InstanceType<typeof AttendanceExceptionForm>
const formRef = ref<ExceptionFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  employeeId: string
  exceptionType?: number
  handleStatus?: number
  from: string
  to: string
}>({
  employeeId: '',
  exceptionType: undefined,
  handleStatus: undefined,
  from: '',
  to: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getExceptionId = (record: AttendanceException): string => {
  if (record?.exceptionId != null && String(record.exceptionId) !== '') return String(record.exceptionId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

const getExceptionField = (record: AttendanceException, field: string): unknown =>
  (record as unknown as Record<string, unknown>)[field]

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.field.id'),
    dataIndex: 'exceptionId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendanceException }) =>
      getExceptionField(record, 'exceptionId') ?? getExceptionField(record, 'id') ?? ''
  },
  {
    title: t('entity.attendanceexception.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendanceexception.exceptiondate'),
    dataIndex: 'exceptionDate',
    key: 'exceptionDate',
    width: 120,
    resizable: true
  },
  {
    title: t('entity.attendanceexception.exceptiontype'),
    dataIndex: 'exceptionType',
    key: 'exceptionType',
    width: 96,
    resizable: true
  },
  {
    title: t('entity.attendanceexception.summary'),
    dataIndex: 'summary',
    key: 'summary',
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendanceexception.handlestatus'),
    dataIndex: 'handleStatus',
    key: 'handleStatus',
    width: 100,
    resizable: true
  },
  CreateActionColumn<AttendanceException>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendanceexception:update',
        onClick: (record: AttendanceException) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendanceexception:delete',
        onClick: (record: AttendanceException) => handleDeleteOne(record)
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
  return merged.filter((col: ExceptionTableColumn) => {
    const colKey = getExceptionColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceException[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceException, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getExceptionId(selectedRow.value) === getExceptionId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceException[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendanceException) => ({
  onClick: () => {
    const key = getExceptionId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getExceptionId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: AttendanceExceptionQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) params.employeeId = n
    }
    if (advancedQueryForm.value.exceptionType != null) params.exceptionType = advancedQueryForm.value.exceptionType
    if (advancedQueryForm.value.handleStatus != null) params.handleStatus = advancedQueryForm.value.handleStatus
    if (advancedQueryForm.value.from) params.exceptionDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) params.exceptionDateTo = advancedQueryForm.value.to

    const response: TaktPagedResult<AttendanceException> = await getAttendanceExceptionList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceException] 加载数据失败:', error)
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
  advancedQueryForm.value = {
    employeeId: '',
    exceptionType: undefined,
    handleStatus: undefined,
    from: '',
    to: ''
  }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceException] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: ExceptionTableColumn) => {
  const column = columns.value.find((c: ExceptionTableColumn) => {
    const a = getExceptionColumnKey(col)
    const b = getExceptionColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as ExceptionTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.attendanceexception._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceException) => {
  formTitle.value = t('common.button.edit') + t('entity.attendanceexception._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: t('entity.attendanceexception._self') })
    )
}

const handleDeleteOne = (record: AttendanceException) => {
  const summary = getExceptionField(record, 'summary')
  const name =
    (typeof summary === 'string' && summary.trim() !== '' ? summary : null) ?? getExceptionId(record)
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.attendanceexception._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceExceptionById(getExceptionId(record))
        message.success(t('common.msg.deletesuccess', { target: t('entity.attendanceexception._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: t('entity.attendanceexception._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.attendanceexception._self') })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      entity: t('entity.attendanceexception._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendanceExceptionById(getExceptionId(selectedRows.value[0]))
        } else {
          await deleteAttendanceExceptionBatch(selectedRows.value.map((r) => getExceptionId(r)))
        }
        message.success(t('common.msg.deletesuccess', { target: t('entity.attendanceexception._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: t('entity.attendanceexception._self') }))
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
    const id = formData.value.exceptionId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendanceExceptionUpdate = { ...(formValues as AttendanceExceptionCreate), exceptionId: idStr }
      await updateAttendanceException(idStr, payload)
      message.success(t('common.msg.updatesuccess', { target: t('entity.attendanceexception._self') }))
    } else {
      await createAttendanceException(formValues as AttendanceExceptionCreate)
      message.success(t('common.msg.createsuccess', { target: t('entity.attendanceexception._self') }))
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
  return await getAttendanceExceptionTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceExceptionData(file, sheetName)
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
    if (advancedQueryForm.value.exceptionType != null) queryParams.ExceptionType = advancedQueryForm.value.exceptionType
    if (advancedQueryForm.value.handleStatus != null) queryParams.HandleStatus = advancedQueryForm.value.handleStatus
    if (advancedQueryForm.value.from) queryParams.ExceptionDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) queryParams.ExceptionDateTo = advancedQueryForm.value.to

    const blob = await exportAttendanceExceptionData(
      queryParams,
      exceptionExcelNames.sheet,
      exceptionExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${exceptionExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess', { target: t('entity.attendanceexception._self') }))
  } catch (error: unknown) {
    logger.error('[AttendanceException] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportfail', { target: t('entity.attendanceexception._self') }))
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
  advancedQueryForm.value = {
    employeeId: '',
    exceptionType: undefined,
    handleStatus: undefined,
    from: '',
    to: ''
  }
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
.humanresource-attendance-leave-exception {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
