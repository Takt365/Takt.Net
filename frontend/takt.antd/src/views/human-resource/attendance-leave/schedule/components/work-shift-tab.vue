<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/schedule/components -->
<!-- 文件名称：work-shift-tab.vue -->
<!-- 功能描述：班次定义子页（嵌入排班 Tab）。关键字与 entity.workshift.keyword 检索、高级查询、CRUD、导入导出、列设置；导入 sheet 与 TaktWorkShift 实体 Excel 约定一致。权限 humanresource:attendanceleave:workshift:*。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-schedule-workshift">
    <!-- 顶部：关键字 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.workshift.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      :create-permission="permissions.create"
      :update-permission="permissions.update"
      :delete-permission="permissions.delete"
      :import-permission="permissions.import"
      :template-permission="permissions.template"
      :export-permission="permissions.export"
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
      :row-key="getRowId"
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
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <WorkShiftForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="adv"
      @submit="handleAdvancedQuerySubmit"
      @reset="resetAdv"
    >
      <a-form-item :label="t('entity.workshift.shiftcode')">
        <a-input v-model:value="adv.shiftCode" />
      </a-form-item>
      <a-form-item :label="t('entity.workshift.shiftname')">
        <a-input v-model:value="adv.shiftName" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.workshift._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="workShiftExcelNames.sheet"
        :template-file-name="workShiftExcelNames.fileBase"
        :template-permission="permissions.template"
        :import-permission="permissions.import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.workshift._self') })"
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
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { useDictDataStore } from '@/stores/routine/dict/dictdata'
import WorkShiftForm from './work-shift-form.vue'
import type { WorkShift } from '@/types/human-resource/attendance-leave/work-shift'
import {
  getWorkShiftList,
  createWorkShift,
  updateWorkShift,
  deleteWorkShiftById,
  deleteWorkShiftBatch,
  getWorkShiftTemplate,
  importWorkShiftData,
  exportWorkShiftData
} from '@/api/human-resource/attendance-leave/work-shift'

const { t } = useI18n()
const dictDataStore = useDictDataStore()
const workShiftExcelNames = taktExcelEntityNames('TaktWorkShift')
const entitySelf = computed(() => t('entity.workshift._self'))

type WorkShiftTableColumn = TableColumnsType[number]

interface TableSorterLike {
  readonly field?: string | number | readonly (string | number)[]
  readonly order?: string | null
}

function getErrorMessage(err: unknown): string | undefined {
  if (err instanceof Error) return err.message
  if (typeof err === 'object' && err !== null && 'message' in err) {
    const m = (err as { message?: unknown }).message
    return typeof m === 'string' ? m : undefined
  }
  return undefined
}

function getWorkShiftColumnKey(col: WorkShiftTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  return c.key || c.dataIndex || c.title ? String(c.key ?? c.dataIndex ?? c.title) : ''
}

const adv = ref({ shiftCode: '', shiftName: '' })
const formRef = ref<{ validate: () => Promise<unknown>; getValues: () => Record<string, unknown>; resetFields: () => void }>()
function resetAdv() {
  adv.value = { shiftCode: '', shiftName: '' }
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Record<string, unknown>[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Record<string, unknown> | null>(null)
const selectedRows = ref<Record<string, unknown>[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<WorkShift>>({})
const formLoading = ref(false)
const tableRef = ref()
const advancedQueryVisible = ref(false)
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])
const permissions = {
  create: 'humanresource:attendanceleave:workshift:create',
  update: 'humanresource:attendanceleave:workshift:update',
  delete: 'humanresource:attendanceleave:workshift:delete',
  import: 'humanresource:attendanceleave:workshift:import',
  template: 'humanresource:attendanceleave:workshift:template',
  export: 'humanresource:attendanceleave:workshift:export'
}
const getRowId = (row: Record<string, unknown>) =>
  row.shiftId != null ? String(row.shiftId) : row.id != null ? String(row.id) : ''
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Record<string, unknown>[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Record<string, unknown>, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getRowId(selectedRow.value) === getRowId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Record<string, unknown>[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))
const onClickRow = (record: Record<string, unknown>) => ({
  onClick: () => {
    const key = getRowId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getRowId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})
const loadData = async () => {
  try {
    loading.value = true
    const params: Record<string, unknown> = { PageIndex: currentPage.value, PageSize: pageSize.value }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (adv.value.shiftCode) params.ShiftCode = adv.value.shiftCode
    if (adv.value.shiftName) params.ShiftName = adv.value.shiftName
    const response = await getWorkShiftList(params)
    const responseAny = response as unknown as Record<string, unknown>
    dataSource.value = (responseAny?.data ?? responseAny?.Data ?? []) as Record<string, unknown>[]
    total.value = (responseAny?.total ?? responseAny?.Total ?? 0) as number
  } catch (error: unknown) {
    logger.error('[WorkShift] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}
const handleSearch = () => {
  currentPage.value = 1
  void loadData()
}
const handleReset = () => {
  queryKeyword.value = ''
  resetAdv()
  currentPage.value = 1
  void loadData()
}
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  void loadData()
}
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  void loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike | TableSorterLike[]) => {
  const currentSorter = Array.isArray(sorter) ? sorter[0] : sorter
  if (currentSorter?.order) logger.debug('[WorkShift] 排序:', currentSorter.field, currentSorter.order)
}

const handleResizeColumn = (w: number, col: WorkShiftTableColumn) => {
  const column = columns.value.find((c: WorkShiftTableColumn) => {
    const a = getWorkShiftColumnKey(col)
    const b = getWorkShiftColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as WorkShiftTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + entitySelf.value
  formData.value = {}
  formVisible.value = true
}
const handleEdit = (record: Record<string, unknown>) => {
  formTitle.value = t('common.button.edit') + entitySelf.value
  formData.value = { ...record }
  formVisible.value = true
}
const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: entitySelf.value }))
}
const handleDeleteOne = (record: Record<string, unknown>) => {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: entitySelf.value, name: getRowId(record) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteWorkShiftById(getRowId(record))
        message.success(t('common.msg.deleteSuccess', { target: entitySelf.value }))
        void loadData()
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
        if (selectedRows.value.length === 1) await deleteWorkShiftById(getRowId(selectedRows.value[0]))
        else await deleteWorkShiftBatch(selectedRows.value.map((r) => getRowId(r)))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        message.success(t('common.msg.deleteSuccess', { target: entitySelf.value }))
        void loadData()
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
    const id = formData.value.shiftId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      await updateWorkShift(idStr, { ...formValues, shiftId: idStr } as Record<string, unknown>)
      message.success(t('common.msg.updateSuccess', { target: entitySelf.value }))
    } else {
      await createWorkShift(formValues)
      message.success(t('common.msg.createSuccess', { target: entitySelf.value }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    void loadData()
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
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) =>
  await getWorkShiftTemplate(sheetName, fileName)
const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => await importWorkShiftData(file, sheetName)
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  void loadData()
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
    if (adv.value.shiftCode) queryParams.ShiftCode = adv.value.shiftCode
    if (adv.value.shiftName) queryParams.ShiftName = adv.value.shiftName
    const blob = await exportWorkShiftData(
      queryParams,
      undefined,
      entitySelf.value + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${entitySelf.value + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
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
    logger.error('[WorkShift] 导出失败:', error)
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
  void loadData()
  advancedQueryVisible.value = false
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
  void loadData()
}

onMounted(() => {
  void loadData()
})

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'shiftId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.workshift.shiftcode'),
    dataIndex: 'shiftCode',
    key: 'shiftCode',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.workshift.shiftname'),
    dataIndex: 'shiftName',
    key: 'shiftName',
    width: 140,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.workshift.starttime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 88,
    resizable: true
  },
  {
    title: t('entity.workshift.endtime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 88,
    resizable: true
  },
  {
    title: t('entity.workshift.crossmidnight'),
    dataIndex: 'crossMidnight',
    key: 'crossMidnight',
    width: 64,
    resizable: true,
    customRender: ({ record }: { record: Record<string, unknown> }) =>
      dictDataStore.getDictLabel((record.crossMidnight ?? '') as string | number, 'sys_yes_no')
  },
  {
    title: t('entity.workshift.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 72,
    resizable: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:workshift:update',
        onClick: (r: Record<string, unknown>) => handleEdit(r)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:workshift:delete',
        onClick: (r: Record<string, unknown>) => handleDeleteOne(r)
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
  return merged.filter((col: WorkShiftTableColumn) => {
    const colKey = getWorkShiftColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

defineExpose({ tableRef })
</script>

<style scoped lang="less">
.humanresource-attendance-leave-schedule-workshift {
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
