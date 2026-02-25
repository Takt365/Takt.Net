<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/tenant -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：租户管理页面，含列表、查询、增删改、导入、导出 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="identity-tenant">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.tenant.name'), t('entity.tenant.code')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="identity:tenant:create"
      update-permission="identity:tenant:update"
      delete-permission="identity:tenant:delete"
      import-permission="identity:tenant:import"
      template-permission="identity:tenant:template"
      export-permission="identity:tenant:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
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
      :row-key="getTenantId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'tenantStatus'">
          <TaktDictTag
            :value="getTenantField(record, 'tenantStatus')"
            dict-type="sys_normal_disable"
          />
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

    <!-- 新增/编辑对话框 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <TenantForm
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
      <a-form-item :label="t('entity.tenant.name')">
        <a-input v-model:value="advancedQueryForm.tenantName" />
      </a-form-item>
      <a-form-item :label="t('entity.tenant.code')">
        <a-input v-model:value="advancedQueryForm.tenantCode" />
      </a-form-item>
      <a-form-item :label="t('entity.tenant.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.tenantStatus"
          dict-type="sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.tenant.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.tenant._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.tenant._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.tenant._self') })"
        template-permission="identity:tenant:template"
        import-permission="identity:tenant:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.tenant._self') })"
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
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import TenantForm from './components/tenant-form.vue'
import {
  getList,
  create,
  update,
  remove,
  getTemplate,
  importTenants,
  exportTenants
} from '@/api/identity/tenant'
import type { Tenant } from '@/types/identity/tenant'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Tenant[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Tenant | null>(null)
const selectedRows = ref<Tenant[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Tenant>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  tenantName: string
  tenantCode: string
  tenantStatus?: number
}>({
  tenantName: '',
  tenantCode: '',
  tenantStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getTenantId = (record: any): string =>
  record?.tenantId != null ? String(record.tenantId) : record?.id != null ? String(record.id) : ''
const getTenantField = (record: any, field: string): any => record?.[field]

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'tenantId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) =>
      getTenantField(record, 'tenantId') ?? getTenantField(record, 'id') ?? ''
  },
  {
    title: t('entity.tenant.name'),
    dataIndex: 'tenantName',
    key: 'tenantName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.tenant.code'),
    dataIndex: 'tenantCode',
    key: 'tenantCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.tenant.starttime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 120
  },
  {
    title: t('entity.tenant.endtime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 120
  },
  {
    title: t('entity.tenant.status'),
    dataIndex: 'tenantStatus',
    key: 'tenantStatus',
    width: 90
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'identity:tenant:update',
        onClick: (record: Tenant) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'identity:tenant:delete',
        onClick: (record: Tenant) => handleDeleteOne(record)
      }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value as any
  const getColumnKey = (col: any): string =>
    col.key || col.dataIndex || col.title ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Tenant[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Tenant, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getTenantId(selectedRow.value) === getTenantId(record))
      selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Tenant[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: Tenant) => ({
  onClick: () => {
    const key = getTenantId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item =>
      selectedRowKeys.value.includes(getTenantId(item))
    )
    selectedRow.value =
      selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange)
      rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.tenantName)
      params.tenantName = advancedQueryForm.value.tenantName
    if (advancedQueryForm.value.tenantCode)
      params.tenantCode = advancedQueryForm.value.tenantCode
    if (advancedQueryForm.value.tenantStatus !== undefined)
      params.tenantStatus = advancedQueryForm.value.tenantStatus

    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Tenant] 加载数据失败:', error)
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
  advancedQueryForm.value = {
    tenantName: '',
    tenantCode: '',
    tenantStatus: undefined
  }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Tenant] 排序:', sorter.field, sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.tenant._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Tenant) => {
  formTitle.value = t('common.button.edit') + t('entity.tenant._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.tenant._self')
      })
    )
}

const handleDeleteOne = (record: Tenant) => {
  const name = getTenantField(record, 'tenantName') || getTenantId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t('entity.tenant._self'),
      name
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(getTenantId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.tenant._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.tenant._self') }))
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
        entity: t('entity.tenant._self')
      })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      count: selectedRows.value.length,
      entity: t('entity.tenant._self')
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(
          selectedRows.value.map(record => remove(getTenantId(record)))
        )
        message.success(t('common.msg.deleteSuccess', { target: t('entity.tenant._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.tenant._self') }))
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
    if (formData.value?.tenantId) {
      await update(formData.value.tenantId, {
        tenantName: formValues.tenantName,
        tenantCode: formValues.tenantCode,
        configId: formValues.configId,
        startTime: formValues.startTime,
        endTime: formValues.endTime,
        remark: formValues.remark,
        tenantId: formData.value.tenantId
      })
      message.success(t('common.msg.updateSuccess', { target: t('entity.tenant._self') }))
    } else {
      await create({
        tenantName: formValues.tenantName,
        tenantCode: formValues.tenantCode,
        configId: formValues.configId,
        startTime: formValues.startTime,
        endTime: formValues.endTime,
        remark: formValues.remark
      })
      message.success(t('common.msg.createSuccess', { target: t('entity.tenant._self') }))
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
  return await importTenants(file, sheetName)
}

const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => (importVisible.value = false), 2000)
}

const handleImportCancel = () => {
  importVisible.value = false
}

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: any = {}
    if (queryKeyword.value) queryParams.keyWords = queryKeyword.value
    if (advancedQueryForm.value.tenantName)
      queryParams.tenantName = advancedQueryForm.value.tenantName
    if (advancedQueryForm.value.tenantCode)
      queryParams.tenantCode = advancedQueryForm.value.tenantCode
    if (advancedQueryForm.value.tenantStatus !== undefined)
      queryParams.tenantStatus = advancedQueryForm.value.tenantStatus
    const blob = await exportTenants(
      queryParams,
      undefined,
      t('entity.tenant._self') + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.tenant._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.tenant._self') }))
  } catch (error: any) {
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.tenant._self') }))
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
    tenantName: '',
    tenantCode: '',
    tenantStatus: undefined
  }
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
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
.identity-tenant {
  padding: 16px;
}
</style>
