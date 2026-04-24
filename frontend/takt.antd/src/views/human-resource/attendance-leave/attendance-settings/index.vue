<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-settings -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：考勤方案（考勤设置）列表页。关键字与 entity.attendancesetting.keyword 检索；高级查询按方案编码/名称；CRUD、导入导出、列设置。权限与 TaktAttendanceSettingsController 一致；实体文案键 entity.attendancesetting.*。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-settings">
    <!-- 顶部：关键字（与种子 entity.attendancesetting.keyword 一致） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendancesetting.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：与 TaktAttendanceSettingsController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendancesettings:create"
      update-permission="humanresource:attendanceleave:attendancesettings:update"
      delete-permission="humanresource:attendanceleave:attendancesettings:delete"
      import-permission="humanresource:attendanceleave:attendancesettings:import"
      template-permission="humanresource:attendanceleave:attendancesettings:template"
      export-permission="humanresource:attendanceleave:attendancesettings:export"
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

    <!-- 主表 -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getSettingId"
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
      <AttendanceSettingForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendancesetting.settingcode')">
        <a-input v-model:value="advancedQueryForm.settingCode" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancesetting.settingname')">
        <a-input v-model:value="advancedQueryForm.settingName" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet 与 TaktAttendanceSetting Excel 约定一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendancesetting._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="settingExcelNames.sheet"
        :template-file-name="settingExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendancesettings:template"
        import-permission="humanresource:attendanceleave:attendancesettings:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.attendancesetting._self') })"
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
import AttendanceSettingForm from './components/attendance-setting-form.vue'
import {
  getAttendanceSettingList,
  createAttendanceSetting,
  updateAttendanceSetting,
  deleteAttendanceSettingById,
  deleteAttendanceSettingBatch,
  getAttendanceSettingTemplate,
  importAttendanceSettingData,
  exportAttendanceSettingData
} from '@/api/human-resource/attendance-leave/attendance-setting'
import type {
  AttendanceSetting,
  AttendanceSettingCreate,
  AttendanceSettingQuery,
  AttendanceSettingUpdate
} from '@/types/human-resource/attendance-leave/attendance-setting'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

const settingExcelNames = taktExcelEntityNames('TaktAttendanceSetting')
const entitySelf = computed(() => t('entity.attendancesetting._self'))

type SettingTableColumn = TableColumnsType[number]

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

function getSettingColumnKey(col: SettingTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<AttendanceSetting[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<AttendanceSetting | null>(null)
const selectedRows = ref<AttendanceSetting[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AttendanceSetting>>({})
const formLoading = ref(false)
type SettingFormExposed = InstanceType<typeof AttendanceSettingForm>
const formRef = ref<SettingFormExposed | null>(null)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref({ settingCode: '', settingName: '' })
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getSettingId = (record: AttendanceSetting): string => {
  if (record?.settingId != null && String(record.settingId) !== '') return String(record.settingId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'settingId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendanceSetting }) =>
      (record as unknown as Record<string, unknown>)['settingId'] ??
      (record as unknown as Record<string, unknown>)['id'] ??
      ''
  },
  {
    title: t('entity.attendancesetting.settingcode'),
    dataIndex: 'settingCode',
    key: 'settingCode',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.settingname'),
    dataIndex: 'settingName',
    key: 'settingName',
    width: 140,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.workstarttime'),
    dataIndex: 'workStartTime',
    key: 'workStartTime',
    width: 88,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.workendtime'),
    dataIndex: 'workEndTime',
    key: 'workEndTime',
    width: 88,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.lategraceminutes'),
    dataIndex: 'lateGraceMinutes',
    key: 'lateGraceMinutes',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.earlyleavegraceminutes'),
    dataIndex: 'earlyLeaveGraceMinutes',
    key: 'earlyLeaveGraceMinutes',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.isdefault'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 72,
    resizable: true
  },
  {
    title: t('entity.attendancesetting.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 72,
    resizable: true
  },
  CreateActionColumn<AttendanceSetting>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendancesettings:update',
        onClick: (record: AttendanceSetting) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendancesettings:delete',
        onClick: (record: AttendanceSetting) => handleDeleteOne(record)
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
  return merged.filter((col: SettingTableColumn) => {
    const colKey = getSettingColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceSetting[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceSetting, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getSettingId(selectedRow.value) === getSettingId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceSetting[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: AttendanceSetting) => ({
  onClick: () => {
    const key = getSettingId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getSettingId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: AttendanceSettingQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.settingCode?.trim()) params.settingCode = advancedQueryForm.value.settingCode.trim()
    if (advancedQueryForm.value.settingName?.trim()) params.settingName = advancedQueryForm.value.settingName.trim()

    const response: TaktPagedResult<AttendanceSetting> = await getAttendanceSettingList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceSetting] 加载数据失败:', error)
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
  advancedQueryForm.value = { settingCode: '', settingName: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceSetting] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: SettingTableColumn) => {
  const column = columns.value.find((c: SettingTableColumn) => {
    const a = getSettingColumnKey(col)
    const b = getSettingColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as SettingTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + entitySelf.value
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceSetting) => {
  formTitle.value = t('common.button.edit') + entitySelf.value
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: entitySelf.value }))
}

const handleDeleteOne = (record: AttendanceSetting) => {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: entitySelf.value, name: getSettingId(record) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceSettingById(getSettingId(record))
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
          await deleteAttendanceSettingById(getSettingId(selectedRows.value[0]))
        } else {
          await deleteAttendanceSettingBatch(selectedRows.value.map((r) => getSettingId(r)))
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
    const id = formData.value.settingId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendanceSettingUpdate = { ...(formValues as AttendanceSettingCreate), settingId: idStr }
      await updateAttendanceSetting(idStr, payload)
      message.success(t('common.msg.updatesuccess', { target: entitySelf.value }))
    } else {
      await createAttendanceSetting(formValues as AttendanceSettingCreate)
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
  return await getAttendanceSettingTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceSettingData(file, sheetName)
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
    if (advancedQueryForm.value.settingCode?.trim()) queryParams.SettingCode = advancedQueryForm.value.settingCode.trim()
    if (advancedQueryForm.value.settingName?.trim()) queryParams.SettingName = advancedQueryForm.value.settingName.trim()

    const blob = await exportAttendanceSettingData(
      queryParams,
      settingExcelNames.sheet,
      settingExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${settingExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
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
    logger.error('[AttendanceSetting] 导出失败:', error)
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
  advancedQueryForm.value = { settingCode: '', settingName: '' }
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
.humanresource-attendance-leave-settings {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
