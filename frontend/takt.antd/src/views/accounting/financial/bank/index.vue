<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/bank -->
<!-- 文件名称：index.vue -->
<!-- 创建时间：2025-02-17 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 功能描述：银行管理页面，含列表、查询、增删改、导入、导出 -->
<!--
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="accounting-financial-bank">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.bank.name'), t('entity.bank.code')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="accounting:financial:bank:create"
      update-permission="accounting:financial:bank:update"
      delete-permission="accounting:financial:bank:delete"
      import-permission="accounting:financial:bank:import"
      template-permission="accounting:financial:bank:template"
      export-permission="accounting:financial:bank:export"
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

    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getBankId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'bankStatus'">
          <TaktDictTag
            :value="getBankField(record, 'bankStatus')"
            dict-type="sys_normal_disable"
          />
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
      <BankForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.bank.name')">
        <a-input v-model:value="advancedQueryForm.bankName" />
      </a-form-item>
      <a-form-item :label="t('entity.bank.code')">
        <a-input v-model:value="advancedQueryForm.bankCode" />
      </a-form-item>
      <a-form-item :label="t('entity.bank.companyCode')">
        <a-input v-model:value="advancedQueryForm.companyCode" />
      </a-form-item>
      <a-form-item :label="t('entity.bank.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.bankStatus"
          dict-type="sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.bank.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.bank._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.bank._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.bank._self') })"
        template-permission="accounting:financial:bank:template"
        import-permission="accounting:financial:bank:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.bank._self') })"
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
import BankForm from './components/bank-form.vue'
import {
  getList,
  create,
  update,
  deleteBank,
  deleteBatch,
  getTemplate,
  importBanks,
  exportBanks
} from '@/api/accounting/financial/bank'
import type { Bank } from '@/types/accounting/financial/bank'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Bank[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Bank | null>(null)
const selectedRows = ref<Bank[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Bank>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  bankName: string
  bankCode: string
  companyCode: string
  bankStatus?: number
}>({
  bankName: '',
  bankCode: '',
  companyCode: '',
  bankStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getBankId = (record: any): string =>
  record?.bankId != null ? String(record.bankId) : record?.id != null ? String(record.id) : ''
const getBankField = (record: any, field: string): any => record?.[field]

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'bankId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) =>
      getBankField(record, 'bankId') ?? getBankField(record, 'id') ?? ''
  },
  {
    title: t('entity.bank.companyCode'),
    dataIndex: 'companyCode',
    key: 'companyCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.bank.code'),
    dataIndex: 'bankCode',
    key: 'bankCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.bank.name'),
    dataIndex: 'bankName',
    key: 'bankName',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.bank.status'),
    dataIndex: 'bankStatus',
    key: 'bankStatus',
    width: 90
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'accounting:financial:bank:update',
        onClick: (record: Bank) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'accounting:financial:bank:delete',
        onClick: (record: Bank) => handleDeleteOne(record)
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
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Bank[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Bank, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getBankId(selectedRow.value) === getBankId(record))
      selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Bank[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: Bank) => ({
  onClick: () => {
    const key = getBankId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) =>
      selectedRowKeys.value.includes(getBankId(item))
    )
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
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
    if (advancedQueryForm.value.bankName) params.bankName = advancedQueryForm.value.bankName
    if (advancedQueryForm.value.bankCode) params.bankCode = advancedQueryForm.value.bankCode
    if (advancedQueryForm.value.companyCode) params.companyCode = advancedQueryForm.value.companyCode
    if (advancedQueryForm.value.bankStatus !== undefined) params.bankStatus = advancedQueryForm.value.bankStatus

    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Bank] 加载数据失败:', error)
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
    bankName: '',
    bankCode: '',
    companyCode: '',
    bankStatus: undefined
  }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Bank] 排序:', sorter.field, sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.bank._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Bank) => {
  formTitle.value = t('common.button.edit') + t('entity.bank._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.bank._self')
      })
    )
}

const handleDeleteOne = (record: Bank) => {
  const name = getBankField(record, 'bankName') || getBankId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t('entity.bank._self'),
      name
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteBank(getBankId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.bank._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.bank._self') }))
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
        entity: t('entity.bank._self')
      })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      count: selectedRows.value.length,
      entity: t('entity.bank._self')
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteBank(getBankId(selectedRows.value[0]))
        } else {
          await deleteBatch(selectedRows.value.map((r) => getBankId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.bank._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.bank._self') }))
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
    if (formData.value?.bankId) {
      await update(formData.value.bankId, {
        ...formValues,
        bankId: formData.value.bankId
      })
      message.success(t('common.msg.updateSuccess', { target: t('entity.bank._self') }))
    } else {
      await create(formValues)
      message.success(t('common.msg.createSuccess', { target: t('entity.bank._self') }))
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
  return await importBanks(file, sheetName)
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
    if (advancedQueryForm.value.bankName) queryParams.bankName = advancedQueryForm.value.bankName
    if (advancedQueryForm.value.bankCode) queryParams.bankCode = advancedQueryForm.value.bankCode
    if (advancedQueryForm.value.companyCode) queryParams.companyCode = advancedQueryForm.value.companyCode
    if (advancedQueryForm.value.bankStatus !== undefined) queryParams.bankStatus = advancedQueryForm.value.bankStatus
    const blob = await exportBanks(
      queryParams,
      undefined,
      t('entity.bank._self') + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.bank._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.bank._self') }))
  } catch (error: any) {
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.bank._self') }))
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
    bankName: '',
    bankCode: '',
    companyCode: '',
    bankStatus: undefined
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
.accounting-financial-bank {
  padding: 16px;
}
</style>
