<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-result -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：考勤日结结果列表页。关键字与 entity.attendanceresult.keyword 检索；高级查询前端 AttendanceResultQuery 与后端 TaktAttendanceResultQueryDto 对应；CRUD、导入导出、列设置；出勤状态列与用户视图一致使用 `#bodyCell` + `TaktDictTag`（`hr_attendance_result_status`）。API @/api/human-resource/attendance-leave/attendance-result，权限与 TaktAttendanceResultsController 一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-result">
    <!-- 顶部：关键字检索（entity.attendanceresult.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendanceresult.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktAttendanceResultsController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendanceresult:create"
      update-permission="humanresource:attendanceleave:attendanceresult:update"
      delete-permission="humanresource:attendanceleave:attendanceresult:delete"
      import-permission="humanresource:attendanceleave:attendanceresult:import"
      template-permission="humanresource:attendanceleave:attendanceresult:template"
      export-permission="humanresource:attendanceleave:attendanceresult:export"
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

    <!-- 主表：attendanceStatus 与用户页一致，bodyCell + TaktDictTag（hr_attendance_result_status） -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getResultId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'attendanceStatus'">
          <TaktDictTag
            :value="getResultField(record, 'attendanceStatus')"
            dict-type="hr_attendance_result_status"
          />
        </template>
      </template>
    </TaktSingleTable>

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
      <AttendanceResultForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <!-- 高级查询：字段与 TaktAttendanceResultQueryDto 一致（前端 AttendanceResultQuery） -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendanceresult.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceresult.attendancestatus')">
        <TaktSelect
          v-model="advancedQueryForm.attendanceStatus"
          dict-type="hr_attendance_result_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.attendanceresult.attendancestatus') })"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceresult.datefrom')">
        <a-date-picker v-model:value="advancedQueryForm.from" value-format="YYYY-MM-DD" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('entity.attendanceresult.dateto')">
        <a-date-picker v-model:value="advancedQueryForm.to" value-format="YYYY-MM-DD" style="width: 100%" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet / 文件名与后端 TaktNamingHelper.ResolveExcelImportExport 及实体 TaktAttendanceResult 命名约定一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendanceresult._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="resultExcelNames.sheet"
        :template-file-name="resultExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendanceresult:template"
        import-permission="humanresource:attendanceleave:attendanceresult:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.attendanceresult._self') })"
        :upload-text="t('common.action.import.uploadText')"
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
import TaktSelect from '@/components/business/takt-select/index.vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import AttendanceResultForm from './components/attendance-result-form.vue'
import {
  getAttendanceResultList,
  createAttendanceResult,
  updateAttendanceResult,
  deleteAttendanceResultById,
  deleteAttendanceResultBatch,
  getAttendanceResultTemplate,
  importAttendanceResultData,
  exportAttendanceResultData
} from '@/api/human-resource/attendance-leave/attendance-result'
import type {
  AttendanceResult,
  AttendanceResultCreate,
  AttendanceResultQuery,
  AttendanceResultUpdate
} from '@/types/human-resource/attendance-leave/attendance-result'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const resultExcelNames = taktExcelEntityNames('TaktAttendanceResult')

type ResultTableColumn = TableColumnsType[number]

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

function getResultColumnKey(col: ResultTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

/** 与用户页 `getUserField` 一致，供 `TaktDictTag` 的 `:value`。 */
function getResultField(record: AttendanceResult, field: string): any {
  return (record as unknown as Record<string, unknown>)[field]
}

function coerceAdvancedAttendanceStatus(value: string | number | undefined | null): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

function formatDeleteRowHint(record: AttendanceResult): string {
  const eid = record.employeeId != null ? String(record.employeeId) : ''
  const d = record.attendanceDate != null ? String(record.attendanceDate).slice(0, 10) : ''
  if (eid && d) return `${eid} · ${d}`
  return getResultId(record)
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendanceResult[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendanceResult | null>(null)
const selectedRows = ref<AttendanceResult[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendanceResult>>({})
const formLoading = ref(false)
type ResultFormExposed = InstanceType<typeof AttendanceResultForm>
const formRef = ref<ResultFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ employeeId: string; attendanceStatus?: string | number; from: string; to: string }>({
  employeeId: '',
  attendanceStatus: undefined,
  from: '',
  to: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getResultId = (record: AttendanceResult): string => {
  if (record?.resultId != null && String(record.resultId) !== '') return String(record.resultId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'resultId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendanceResult }) =>
      record.resultId ?? (record as unknown as Record<string, unknown>)['id'] ?? ''
  },
  {
    title: t('entity.attendanceresult.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.attendancedate'),
    dataIndex: 'attendanceDate',
    key: 'attendanceDate',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.attendanceresult.shiftscheduleid'),
    dataIndex: 'shiftScheduleId',
    key: 'shiftScheduleId',
    width: 100,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.attendancestatus'),
    dataIndex: 'attendanceStatus',
    key: 'attendanceStatus',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.firstintime'),
    dataIndex: 'firstInTime',
    key: 'firstInTime',
    width: 168,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.lastouttime'),
    dataIndex: 'lastOutTime',
    key: 'lastOutTime',
    width: 168,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.workminutes'),
    dataIndex: 'workMinutes',
    key: 'workMinutes',
    width: 112,
    resizable: true
  },
  {
    title: t('entity.attendanceresult.calculatedat'),
    dataIndex: 'calculatedAt',
    key: 'calculatedAt',
    width: 168,
    ellipsis: true,
    resizable: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendanceresult:update',
        onClick: (record: AttendanceResult) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendanceresult:delete',
        onClick: (record: AttendanceResult) => handleDeleteOne(record)
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
  return merged.filter((col: ResultTableColumn) => {
    const colKey = getResultColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceResult[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceResult, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getResultId(selectedRow.value) === getResultId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceResult[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendanceResult) => ({
  onClick: () => {
    const key = getResultId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getResultId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

function buildListQuery(): AttendanceResultQuery {
  const params: AttendanceResultQuery = {
    pageIndex: currentPage.value,
    pageSize: pageSize.value
  }
  if (queryKeyword.value) params.KeyWords = queryKeyword.value
  const eid = advancedQueryForm.value.employeeId?.trim()
  if (eid) {
    const n = Number(eid)
    if (!Number.isNaN(n)) params.employeeId = n
  }
  const advSt = coerceAdvancedAttendanceStatus(advancedQueryForm.value.attendanceStatus)
  if (advSt !== undefined) {
    params.attendanceStatus = advSt
  }
  if (advancedQueryForm.value.from) params.attendanceDateFrom = advancedQueryForm.value.from
  if (advancedQueryForm.value.to) params.attendanceDateTo = advancedQueryForm.value.to
  return params
}

const loadData = async () => {
  try {
    loading.value = true
    const params = buildListQuery()
    const response: TaktPagedResult<AttendanceResult> = await getAttendanceResultList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceResult] 加载数据失败:', error)
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
  advancedQueryForm.value = { employeeId: '', attendanceStatus: undefined, from: '', to: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceResult] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: ResultTableColumn) => {
  const column = columns.value.find((c: ResultTableColumn) => {
    const a = getResultColumnKey(col)
    const b = getResultColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as ResultTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.attendanceresult._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceResult) => {
  formTitle.value = t('common.button.edit') + t('entity.attendanceresult._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.attendanceresult._self')
      })
    )
}

const handleDeleteOne = (record: AttendanceResult) => {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t('entity.attendanceresult._self'),
      name: formatDeleteRowHint(record)
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceResultById(getResultId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendanceresult._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendanceresult._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.delete'),
        entity: t('entity.attendanceresult._self')
      })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      entity: t('entity.attendanceresult._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendanceResultById(getResultId(selectedRows.value[0]))
        } else {
          await deleteAttendanceResultBatch(selectedRows.value.map((r) => getResultId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendanceresult._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendanceresult._self') }))
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
    const id = formData.value.resultId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendanceResultUpdate = { ...(formValues as AttendanceResultCreate), resultId: idStr }
      await updateAttendanceResult(idStr, payload)
      message.success(t('common.msg.updateSuccess', { target: t('entity.attendanceresult._self') }))
    } else {
      await createAttendanceResult(formValues as AttendanceResultCreate)
      message.success(t('common.msg.createSuccess', { target: t('entity.attendanceresult._self') }))
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
  return await getAttendanceResultTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceResultData(file, sheetName)
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
    const queryParams: AttendanceResultQuery = {
      pageIndex: 1,
      pageSize: 100000
    }
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) queryParams.employeeId = n
    }
    const advSt = coerceAdvancedAttendanceStatus(advancedQueryForm.value.attendanceStatus)
    if (advSt !== undefined) {
      queryParams.attendanceStatus = advSt
    }
    if (advancedQueryForm.value.from) queryParams.attendanceDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) queryParams.attendanceDateTo = advancedQueryForm.value.to

    const blob = await exportAttendanceResultData(
      queryParams as unknown as Record<string, unknown>,
      resultExcelNames.sheet,
      resultExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob as Blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${resultExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.attendanceresult._self') }))
  } catch (error: unknown) {
    logger.error('[AttendanceResult] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportFail', { target: t('entity.attendanceresult._self') }))
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
  advancedQueryForm.value = { employeeId: '', attendanceStatus: undefined, from: '', to: '' }
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
.humanresource-attendance-leave-result {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
