<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/human-resource/personnel/employee-work -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-01-20 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：员工工作经历管理页面，包含列表、查询、新增、编辑、删除、导入、导出功能 -->
<!--  -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-personnel-employee-work">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.employeework.companyName') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="humanresource:personnel:employeework:create"
      update-permission="humanresource:personnel:employeework:update"
      delete-permission="humanresource:personnel:employeework:delete"
      import-permission="humanresource:personnel:employeework:import"
      template-permission="humanresource:personnel:employeework:template"
      export-permission="humanresource:personnel:employeework:export"
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
    />

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
      <WorkForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <!-- 高级查询抽屉 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.employeework.employeeId')">
        <a-input v-model:value="advancedQueryForm.employeeId" />
      </a-form-item>
      <a-form-item :label="t('entity.employeework.companyName')">
        <a-input v-model:value="advancedQueryForm.companyName" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.employeework._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.employeework._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.employeework._self') })"
        template-permission="humanresource:personnel:employeework:template"
        import-permission="humanresource:personnel:employeework:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.employeework._self') })"
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
import { computed, onMounted, ref } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { RiDeleteBinLine, RiEditLine } from '@remixicon/vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import WorkForm from './components/work-form.vue'
import {
  createEmployeeWork,
  deleteEmployeeWorkBatch,
  deleteEmployeeWorkById,
  exportEmployeeWorkData,
  getEmployeeWorkById,
  getEmployeeWorkList,
  getEmployeeWorkTemplate,
  importEmployeeWorkData,
  updateEmployeeWork
} from '@/api/human-resource/personnel/employee-work'
import type { EmployeeWork, EmployeeWorkQuery } from '@/types/human-resource/personnel/employee-work'

const { t } = useI18n()
const entityKey = 'entity.employeework'

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<EmployeeWork[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<EmployeeWork | null>(null)
const selectedRows = ref<EmployeeWork[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<EmployeeWork>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ employeeId?: string; companyName?: string }>({
  employeeId: '',
  companyName: ''
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

// 初始化时加载数据
onMounted(() => {
  loadData()
})

const getRowId = (row: any): string =>
  row?.employeeWorkId != null ? String(row.employeeWorkId) : row?.id != null ? String(row.id) : ''

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'employeeWorkId',
    key: 'id',
    width: 90,
    fixed: 'left',
    ellipsis: true
  },
  { title: t(`${entityKey}.employeeId`), dataIndex: 'employeeId', key: 'employeeId', width: 120 },
  { title: t(`${entityKey}.companyName`), dataIndex: 'companyName', key: 'companyName', width: 180, ellipsis: true },
  { title: t(`${entityKey}.positionName`), dataIndex: 'positionName', key: 'positionName', width: 120, ellipsis: true },
  { title: t(`${entityKey}.witnessName`), dataIndex: 'witnessName', key: 'witnessName', width: 120, ellipsis: true },
  { title: t('common.entity.createTime'), dataIndex: 'createdAt', key: 'createdAt', width: 160, ellipsis: true },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:personnel:employeework:update',
        onClick: (record: EmployeeWork) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:personnel:employeework:delete',
        onClick: (record: EmployeeWork) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))

const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  if (keys.length === 0) return columns.value
  const keySet = new Set(keys.map((k) => String(k)))
  const getColumnKey = (col: any) => String(col.key || col.dataIndex || col.title || '')
  return mergedColumns.value.filter((col: any) => keySet.has(getColumnKey(col)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: EmployeeWork[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (row: EmployeeWork, selected: boolean) => {
    if (selected) {
      selectedRow.value = row
      return
    }
    if (selectedRow.value && getRowId(selectedRow.value) === getRowId(row)) {
      selectedRow.value = null
    }
  },
  onSelectAll: (_selected: boolean, rows: EmployeeWork[]) => {
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: EmployeeWork) => ({
  onClick: () => {
    const key = getRowId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getRowId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: EmployeeWorkQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.companyName = queryKeyword.value
    if (advancedQueryForm.value.employeeId) params.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.companyName) params.companyName = advancedQueryForm.value.companyName

    const response = await getEmployeeWorkList(params)
    const responseAny = response as any
    dataSource.value = responseAny?.data ?? responseAny?.Data ?? []
    total.value = responseAny?.total ?? responseAny?.Total ?? 0
  } catch (error: any) {
    logger.error('[EmployeeWork] loadData:', error)
    message.error(error?.message || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

function handleSearch() {
  currentPage.value = 1
  loadData()
}

function handleReset() {
  queryKeyword.value = ''
  advancedQueryForm.value = { employeeId: '', companyName: '' }
  currentPage.value = 1
  loadData()
}

function handleTableChange() {}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

function handleResizeColumn() {}

function handleCreate() {
  formTitle.value = t('common.button.create') + t(`${entityKey}._self`)
  formData.value = {}
  formVisible.value = true
}

async function handleEdit(record: EmployeeWork) {
  formTitle.value = t('common.button.edit') + t(`${entityKey}._self`)
  try {
    formLoading.value = true
    formData.value = { ...(await getEmployeeWorkById(getRowId(record))) }
    formVisible.value = true
  } catch (error: any) {
    message.error(error?.message || t('common.msg.loadFail'))
  } finally {
    formLoading.value = false
  }
}

function handleUpdate() {
  if (selectedRow.value) {
    handleEdit(selectedRow.value)
    return
  }
  message.warning(
    t('common.action.warnSelectToAction', {
      action: t('common.button.edit'),
      entity: t(`${entityKey}._self`)
    })
  )
}

function handleDeleteOne(record: EmployeeWork) {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t(`${entityKey}._self`),
      name: record.companyName || getRowId(record)
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteEmployeeWorkById(getRowId(record))
        message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) }))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.delete'),
        entity: t(`${entityKey}._self`)
      })
    )
    return
  }

  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      entity: t(`${entityKey}._self`),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteEmployeeWorkById(getRowId(selectedRows.value[0]))
        } else {
          await deleteEmployeeWorkBatch(selectedRows.value.map((row) => getRowId(row)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t(`${entityKey}._self`) }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t(`${entityKey}._self`) }))
      } finally {
        loading.value = false
      }
    }
  })
}

async function handleFormSubmit() {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true

    if (formData.value?.employeeWorkId) {
      await updateEmployeeWork(formData.value.employeeWorkId, {
        ...values,
        employeeWorkId: formData.value.employeeWorkId
      })
      message.success(t('common.msg.updateSuccess', { target: t(`${entityKey}._self`) }))
    } else {
      await createEmployeeWork(values)
      message.success(t('common.msg.createSuccess', { target: t(`${entityKey}._self`) }))
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

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

function handleImport() {
  importVisible.value = true
}

function handleImportCancel() {
  importVisible.value = false
}

async function handleDownloadTemplate(sheetName?: string, fileName?: string) {
  return await getEmployeeWorkTemplate(sheetName, fileName)
}

async function handleImportFile(file: File, sheetName?: string) {
  return importEmployeeWorkData(file, sheetName)
}

function handleImportSuccess(result: { success: number; fail: number; errors: string[] }) {
  loadData()
  if (result.fail === 0) {
    setTimeout(() => {
      importVisible.value = false
    }, 2000)
  }
}

async function handleExport() {
  try {
    loading.value = true
    const queryParams: EmployeeWorkQuery = {
      pageIndex: 1,
      pageSize: total.value || 9999
    }
    if (queryKeyword.value) queryParams.companyName = queryKeyword.value
    if (advancedQueryForm.value.employeeId) queryParams.employeeId = advancedQueryForm.value.employeeId
    if (advancedQueryForm.value.companyName) queryParams.companyName = advancedQueryForm.value.companyName
    const blob = await exportEmployeeWorkData(
      queryParams,
      undefined,
      t(`${entityKey}._self`) + t('common.action.exportDataSuffix')
    )
    const data = (blob as any)?.data ?? blob
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t(`${entityKey}._self`) + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(data)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t(`${entityKey}._self`) }))
  } catch (error: any) {
    logger.error('[EmployeeWork] export:', error)
    message.error(error?.message || t('common.msg.exportFail', { target: t(`${entityKey}._self`) }))
  } finally {
    loading.value = false
  }
}

function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

function handleAdvancedQuerySubmit() {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

function handleAdvancedQueryReset() {
  advancedQueryForm.value = { employeeId: '', companyName: '' }
}

function handleColumnSetting() {
  columnSettingVisible.value = true
}

function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

function handleRefresh() {
  loadData()
}

defineExpose({ tableRef })
</script>

<style scoped lang="less">
.humanresource-personnel-employee-work {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
