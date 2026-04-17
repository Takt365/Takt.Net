<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-correction -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：补卡申请列表页。关键字与 entity.attendancecorrection.keyword 检索；高级查询字段对应后端 TaktAttendanceCorrectionQueryDto（前端 AttendanceCorrectionQuery）；CRUD、导入导出、列设置；审批状态列与用户视图一致使用 `#bodyCell` + `TaktDictTag`（`hr_attendance_correction_approval`）。API @/api/human-resource/attendance-leave/attendance-correction，权限与 TaktAttendanceCorrectionsController 一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-correction">
    <!-- 顶部：关键字（entity.attendancecorrection.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendancecorrection.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktAttendanceCorrectionsController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendancecorrection:create"
      update-permission="humanresource:attendanceleave:attendancecorrection:update"
      delete-permission="humanresource:attendanceleave:attendancecorrection:delete"
      import-permission="humanresource:attendanceleave:attendancecorrection:import"
      template-permission="humanresource:attendanceleave:attendancecorrection:template"
      export-permission="humanresource:attendanceleave:attendancecorrection:export"
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

    <!-- 主表：审批状态与用户页一致，bodyCell + TaktDictTag（hr_attendance_correction_approval） -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getCorrectionId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'approvalStatus'">
          <TaktDictTag
            :value="getCorrectionField(record, 'approvalStatus')"
            dict-type="hr_attendance_correction_approval"
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
      <AttendanceCorrectionForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询：字段与 TaktAttendanceCorrectionQueryDto 一致（前端 AttendanceCorrectionQuery） -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendancecorrection.employeeid')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancecorrection.approvalstatus.label')">
        <TaktSelect
          v-model="advancedQueryForm.approvalStatus"
          dict-type="hr_attendance_correction_approval"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancecorrection.approvalstatus.label') })"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendancecorrection.targetdatefrom')">
        <a-date-picker
          v-model:value="advancedQueryForm.from"
          value-format="YYYY-MM-DD"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendancecorrection.targetdateto')">
        <a-date-picker
          v-model:value="advancedQueryForm.to"
          value-format="YYYY-MM-DD"
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet / 文件名与后端 TaktNamingHelper.ResolveExcelImportExport 及实体 TaktAttendanceCorrection 命名约定一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendancecorrection._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="correctionExcelNames.sheet"
        :template-file-name="correctionExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendancecorrection:template"
        import-permission="humanresource:attendanceleave:attendancecorrection:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.attendancecorrection._self') })"
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
import AttendanceCorrectionForm from './components/attendance-correction-form.vue'
import {
  getAttendanceCorrectionList,
  createAttendanceCorrection,
  updateAttendanceCorrection,
  deleteAttendanceCorrectionById,
  deleteAttendanceCorrectionBatch,
  getAttendanceCorrectionTemplate,
  importAttendanceCorrectionData,
  exportAttendanceCorrectionData
} from '@/api/human-resource/attendance-leave/attendance-correction'
import type {
  AttendanceCorrection,
  AttendanceCorrectionCreate,
  AttendanceCorrectionQuery,
  AttendanceCorrectionUpdate
} from '@/types/human-resource/attendance-leave/attendance-correction'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()
const correctionExcelNames = taktExcelEntityNames('TaktAttendanceCorrection')

type CorrectionTableColumn = TableColumnsType[number]

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

function getCorrectionColumnKey(col: CorrectionTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  return c.key || c.dataIndex || c.title ? String(c.key ?? c.dataIndex ?? c.title) : ''
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendanceCorrection[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendanceCorrection | null>(null)
const selectedRows = ref<AttendanceCorrection[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendanceCorrection>>({})
const formLoading = ref(false)
type CorrectionFormExposed = InstanceType<typeof AttendanceCorrectionForm>
const formRef = ref<CorrectionFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  employeeId: string
  approvalStatus?: string | number
  from: string
  to: string
}>({
  employeeId: '',
  approvalStatus: undefined,
  from: '',
  to: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getCorrectionId = (record: AttendanceCorrection): string => {
  if (record?.correctionId != null && String(record.correctionId) !== '') return String(record.correctionId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

/** 与用户页 `getUserField` 一致，供 `TaktDictTag` 的 `:value`。 */
const getCorrectionField = (record: AttendanceCorrection, field: string): unknown =>
  (record as unknown as Record<string, unknown>)[field]

function coerceAdvancedApprovalStatus(value: string | number | undefined | null): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.field.id'),
    dataIndex: 'correctionId',
    key: 'id',
    width: 88,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: AttendanceCorrection }) =>
      record.correctionId ?? (record as unknown as Record<string, unknown>)['id'] ?? ''
  },
  {
    title: t('entity.attendancecorrection.employeeid'),
    dataIndex: 'employeeId',
    key: 'employeeId',
    width: 100,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.attendancecorrection.targetdate'),
    dataIndex: 'targetDate',
    key: 'targetDate',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.attendancecorrection.correctionkind'),
    dataIndex: 'correctionKind',
    key: 'correctionKind',
    width: 96,
    resizable: true
  },
  {
    title: t('entity.attendancecorrection.requestpunchtime'),
    dataIndex: 'requestPunchTime',
    key: 'requestPunchTime',
    width: 168,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.attendancecorrection.reason'),
    dataIndex: 'reason',
    key: 'reason',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.attendancecorrection.approvalstatus.label'),
    dataIndex: 'approvalStatus',
    key: 'approvalStatus',
    width: 100,
    resizable: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendancecorrection:update',
        onClick: (record: AttendanceCorrection) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendancecorrection:delete',
        onClick: (record: AttendanceCorrection) => handleDeleteOne(record)
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
  return merged.filter((col: CorrectionTableColumn) => {
    const colKey = getCorrectionColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceCorrection[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceCorrection, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getCorrectionId(selectedRow.value) === getCorrectionId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceCorrection[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendanceCorrection) => ({
  onClick: () => {
    const key = getCorrectionId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getCorrectionId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: AttendanceCorrectionQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    const eid = advancedQueryForm.value.employeeId?.trim()
    if (eid) {
      const n = Number(eid)
      if (!Number.isNaN(n)) params.employeeId = n
    }
    const adv = coerceAdvancedApprovalStatus(advancedQueryForm.value.approvalStatus)
    if (adv !== undefined) params.approvalStatus = adv
    if (advancedQueryForm.value.from) params.targetDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) params.targetDateTo = advancedQueryForm.value.to

    const response: TaktPagedResult<AttendanceCorrection> = await getAttendanceCorrectionList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceCorrection] 加载数据失败:', error)
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
  advancedQueryForm.value = { employeeId: '', approvalStatus: undefined, from: '', to: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceCorrection] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: CorrectionTableColumn) => {
  const column = columns.value.find((c: CorrectionTableColumn) => {
    const a = getCorrectionColumnKey(col)
    const b = getCorrectionColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as CorrectionTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.attendancecorrection._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceCorrection) => {
  formTitle.value = t('common.button.edit') + t('entity.attendancecorrection._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.attendancecorrection._self')
      })
    )
}

const handleDeleteOne = (record: AttendanceCorrection) => {
  const name = record.reason || getCorrectionId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.attendancecorrection._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceCorrectionById(getCorrectionId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendancecorrection._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendancecorrection._self') }))
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
        entity: t('entity.attendancecorrection._self')
      })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      entity: t('entity.attendancecorrection._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendanceCorrectionById(getCorrectionId(selectedRows.value[0]))
        } else {
          await deleteAttendanceCorrectionBatch(selectedRows.value.map((r) => getCorrectionId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendancecorrection._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendancecorrection._self') }))
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
    const cid = formData.value.correctionId
    if (cid != null && String(cid).length > 0) {
      const idStr = String(cid)
      const payload: AttendanceCorrectionUpdate = {
        ...(formValues as AttendanceCorrectionCreate),
        correctionId: idStr
      }
      await updateAttendanceCorrection(idStr, payload)
      message.success(t('common.msg.updateSuccess', { target: t('entity.attendancecorrection._self') }))
    } else {
      await createAttendanceCorrection(formValues as AttendanceCorrectionCreate)
      message.success(t('common.msg.createSuccess', { target: t('entity.attendancecorrection._self') }))
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
  return await getAttendanceCorrectionTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceCorrectionData(file, sheetName)
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
    const adv = coerceAdvancedApprovalStatus(advancedQueryForm.value.approvalStatus)
    if (adv !== undefined) queryParams.ApprovalStatus = adv
    if (advancedQueryForm.value.from) queryParams.TargetDateFrom = advancedQueryForm.value.from
    if (advancedQueryForm.value.to) queryParams.TargetDateTo = advancedQueryForm.value.to

    const blob = await exportAttendanceCorrectionData(
      queryParams,
      correctionExcelNames.sheet,
      correctionExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${correctionExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.attendancecorrection._self') }))
  } catch (error: unknown) {
    logger.error('[AttendanceCorrection] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportFail', { target: t('entity.attendancecorrection._self') }))
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
  advancedQueryForm.value = { employeeId: '', approvalStatus: undefined, from: '', to: '' }
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
.humanresource-attendance-leave-correction {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
