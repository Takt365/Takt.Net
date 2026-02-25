<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/humanresource/attendance-leave/holiday -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：假日管理页面，包含假日列表、查询、新增、编辑、删除、导入、导出（以用户实体为标准） -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-holiday">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.holiday.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="humanresource:attendanceleave:holiday:create"
      update-permission="humanresource:attendanceleave:holiday:update"
      delete-permission="humanresource:attendanceleave:holiday:delete"
      import-permission="humanresource:attendanceleave:holiday:import"
      template-permission="humanresource:attendanceleave:holiday:template"
      export-permission="humanresource:attendanceleave:holiday:export"
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
      :row-key="getHolidayId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'holidayType'">
          {{ getHolidayTypeLabel(getHolidayField(record, 'holidayType')) }}
        </template>
        <template v-else-if="column.key === 'isWorkingDay'">
          {{ getIsWorkingDayLabel(getHolidayField(record, 'isWorkingDay')) }}
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
      <HolidayForm
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
      <a-form-item :label="t('entity.holiday.region')">
        <a-input v-model:value="advancedQueryForm.region" />
      </a-form-item>
      <a-form-item :label="t('entity.holiday.holidayname')">
        <a-input v-model:value="advancedQueryForm.holidayName" />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.holiday._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.holiday._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.holiday._self') })"
        template-permission="humanresource:attendanceleave:holiday:template"
        import-permission="humanresource:attendanceleave:holiday:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.holiday._self') })"
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
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import HolidayForm from './components/holiday-form.vue'
import {
  getList,
  create,
  update,
  remove,
  removeBatch,
  getTemplate,
  importHolidays,
  exportHolidays
} from '@/api/humanresource/attendance-leave/holiday'
import type { Holiday } from '@/types/humanresource/attendance-leave/holiday'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Holiday[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Holiday | null>(null)
const selectedRows = ref<Holiday[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Holiday>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ region: string; holidayName: string }>({
  region: '',
  holidayName: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getHolidayId = (record: any): string =>
  record?.holidayId != null ? String(record.holidayId) : record?.id != null ? String(record.id) : ''
const getHolidayField = (record: any, field: string): any => record?.[field]

function getHolidayTypeLabel(v: number | undefined): string {
  const map: Record<number, string> = { 0: '法定', 1: '调休', 2: '公司' }
  return v !== undefined && v !== null ? map[v] ?? '-' : '-'
}

function getIsWorkingDayLabel(v: number | undefined): string {
  const map: Record<number, string> = { 0: '非工作日', 1: '工作日', 2: '半天等' }
  return v !== undefined && v !== null ? map[v] ?? '-' : '-'
}

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'holidayId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getHolidayField(record, 'holidayId') ?? getHolidayField(record, 'id') ?? ''
  },
  {
    title: t('entity.holiday.region'),
    dataIndex: 'region',
    key: 'region',
    width: 100,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.holiday.holidayname'),
    dataIndex: 'holidayName',
    key: 'holidayName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.holiday.holidaytype'),
    dataIndex: 'holidayType',
    key: 'holidayType',
    width: 90
  },
  {
    title: t('entity.holiday.startdate'),
    dataIndex: 'startDate',
    key: 'startDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('entity.holiday.enddate'),
    dataIndex: 'endDate',
    key: 'endDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('entity.holiday.isworkingday'),
    dataIndex: 'isWorkingDay',
    key: 'isWorkingDay',
    width: 100
  },
  {
    title: t('entity.holiday.holidaygreeting'),
    dataIndex: 'holidayGreeting',
    key: 'holidayGreeting',
    width: 160,
    ellipsis: true
  },
  {
    title: t('entity.holiday.holidayquote'),
    dataIndex: 'holidayQuote',
    key: 'holidayQuote',
    width: 200,
    ellipsis: true
  },
  {
    title: t('entity.holiday.holidaytheme'),
    dataIndex: 'holidayTheme',
    key: 'holidayTheme',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:holiday:update',
        onClick: (record: Holiday) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:holiday:delete',
        onClick: (record: Holiday) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: any): string =>
    col.key || col.dataIndex || col.title ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Holiday[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Holiday, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getHolidayId(selectedRow.value) === getHolidayId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Holiday[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: Holiday) => ({
  onClick: () => {
    const key = getHolidayId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getHolidayId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      PageIndex: currentPage.value,
      PageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.region) params.region = advancedQueryForm.value.region
    if (advancedQueryForm.value.holidayName) params.HolidayName = advancedQueryForm.value.holidayName

    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Holiday] 加载数据失败:', error)
    message.error(error?.message || t('common.msg.loadFail'))
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
  advancedQueryForm.value = { region: '', holidayName: '' }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Holiday] 排序:', sorter.field, sorter.order)
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

const handleResizeColumn = (w: number, col: any) => {
  const column = columns.value.find((c: any) => {
    const colKey = col.key || col.dataIndex || col.title
    const cKey = c.key || c.dataIndex || c.title
    return colKey && cKey && String(colKey) === String(cKey)
  })
  if (column) (column as any).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.holiday._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Holiday) => {
  formTitle.value = t('common.button.edit') + t('entity.holiday._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.holiday._self') }))
}

const handleDeleteOne = (record: Holiday) => {
  const name = getHolidayField(record, 'holidayName') || getHolidayId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.holiday._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(getHolidayId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.holiday._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.holiday._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.holiday._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { entity: t('entity.holiday._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await remove(getHolidayId(selectedRows.value[0]))
        } else {
          await removeBatch(selectedRows.value.map((r) => getHolidayId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.holiday._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.holiday._self') }))
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
    if (formData.value?.holidayId) {
      await update(formData.value.holidayId, { ...formValues, holidayId: formData.value.holidayId })
      message.success(t('common.msg.updateSuccess', { target: t('entity.holiday._self') }))
    } else {
      await create(formValues)
      message.success(t('common.msg.createSuccess', { target: t('entity.holiday._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error?.errorFields) return
    message.error(error?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
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

const handleDownloadTemplate = async (sheetName?: string, fileName?: string): Promise<Blob> => {
  const res = await getTemplate(sheetName, fileName)
  return (res as any)?.data ?? res
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importHolidays(file, sheetName)
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
    const queryParams: any = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.region) queryParams.region = advancedQueryForm.value.region
    if (advancedQueryForm.value.holidayName) queryParams.HolidayName = advancedQueryForm.value.holidayName

    const blob = await exportHolidays(
      queryParams,
      undefined,
      t('entity.holiday._self') + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.holiday._self') + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.holiday._self') }))
  } catch (error: any) {
    logger.error('[Holiday] 导出失败:', error)
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.holiday._self') }))
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
  advancedQueryForm.value = { region: '', holidayName: '' }
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
.humanresource-attendance-leave-holiday {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
