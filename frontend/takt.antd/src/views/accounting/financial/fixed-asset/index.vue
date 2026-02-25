<!-- 固定资产管理：列表、查询、增删改、导入导出 -->
<template>
  <div class="accounting-financial-fixed-asset">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: [t('entity.fixedasset.name'), t('entity.fixedasset.code')].join(t('common.action.or')) })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="accounting:financial:fixedasset:create"
      update-permission="accounting:financial:fixedasset:update"
      delete-permission="accounting:financial:fixedasset:delete"
      import-permission="accounting:financial:fixedasset:import"
      template-permission="accounting:financial:fixedasset:template"
      export-permission="accounting:financial:fixedasset:export"
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
      :row-key="getFixedAssetId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'assetStatus'">
          <TaktDictTag
            :value="getFixedAssetField(record, 'assetStatus')"
            dict-type="acct_fixed_asset_status"
          />
        </template>
        <template v-else-if="column.key === 'assetType'">
          <TaktDictTag
            :value="getFixedAssetField(record, 'assetType')"
            dict-type="acct_asset_type"
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
      width="60%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <FixedAssetForm ref="formRef" :form-data="formData" :loading="formLoading" />
    </TaktModal>

    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.fixedasset.name')">
        <a-input v-model:value="advancedQueryForm.assetName" />
      </a-form-item>
      <a-form-item :label="t('entity.fixedasset.code')">
        <a-input v-model:value="advancedQueryForm.assetCode" />
      </a-form-item>
      <a-form-item :label="t('entity.fixedasset.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.assetStatus"
          dict-type="acct_fixed_asset_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.fixedasset.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.fixedasset._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.fixedasset._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.fixedasset._self') })"
        template-permission="accounting:financial:fixedasset:template"
        import-permission="accounting:financial:fixedasset:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.fixedasset._self') })"
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
      id-column-key="id"
      action-column-key="action"
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
import FixedAssetForm from './components/fixed-asset-form.vue'
import {
  getList,
  create,
  update,
  deleteFixedAsset,
  getTemplate,
  importFixedAssets,
  exportFixedAssets
} from '@/api/accounting/financial/fixed-asset'
import type { FixedAsset } from '@/types/accounting/financial/fixed-asset'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<FixedAsset[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<FixedAsset | null>(null)
const selectedRows = ref<FixedAsset[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<FixedAsset>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  assetName: string
  assetCode: string
  assetStatus?: number
}>({
  assetName: '',
  assetCode: '',
  assetStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getFixedAssetId = (record: any): string =>
  record?.fixedAssetsId != null ? String(record.fixedAssetsId) : record?.id != null ? String(record.id) : ''
const getFixedAssetField = (record: any, field: string): any => record?.[field]

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'fixedAssetsId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) =>
      getFixedAssetField(record, 'fixedAssetsId') ?? getFixedAssetField(record, 'id') ?? ''
  },
  {
    title: t('entity.fixedasset.code'),
    dataIndex: 'assetCode',
    key: 'assetCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.fixedasset.name'),
    dataIndex: 'assetName',
    key: 'assetName',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.fixedasset.type'),
    dataIndex: 'assetType',
    key: 'assetType',
    width: 100
  },
  {
    title: t('entity.fixedasset.originalValue'),
    dataIndex: 'assetOriginalValue',
    key: 'assetOriginalValue',
    width: 120,
    align: 'right',
    customRender: ({ text }: { text: any }) => (text != null ? Number(text).toLocaleString() : '')
  },
  {
    title: t('entity.fixedasset.status'),
    dataIndex: 'assetStatus',
    key: 'assetStatus',
    width: 100
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'accounting:financial:fixedasset:update',
        onClick: (record: FixedAsset) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'accounting:financial:fixedasset:delete',
        onClick: (record: FixedAsset) => handleDeleteOne(record)
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
    col.key || col.dataIndex || (col.title ? String(col.title) : '')
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: FixedAsset[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: FixedAsset, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getFixedAssetId(selectedRow.value) === getFixedAssetId(record))
      selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: FixedAsset[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: FixedAsset) => ({
  onClick: () => {
    const key = getFixedAssetId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item =>
      selectedRowKeys.value.includes(getFixedAssetId(item))
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
    if (advancedQueryForm.value.assetName) params.assetName = advancedQueryForm.value.assetName
    if (advancedQueryForm.value.assetCode) params.assetCode = advancedQueryForm.value.assetCode
    if (advancedQueryForm.value.assetStatus !== undefined)
      params.assetStatus = advancedQueryForm.value.assetStatus
    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[FixedAsset] 加载数据失败:', error)
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
  advancedQueryForm.value = { assetName: '', assetCode: '', assetStatus: undefined }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[FixedAsset] 排序:', sorter.field, sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.fixedasset._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: FixedAsset) => {
  formTitle.value = t('common.button.edit') + t('entity.fixedasset._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', {
        action: t('common.button.edit'),
        entity: t('entity.fixedasset._self')
      })
    )
}

const handleDeleteOne = (record: FixedAsset) => {
  const name = getFixedAssetField(record, 'assetName') || getFixedAssetId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', {
      entity: t('entity.fixedasset._self'),
      name
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteFixedAsset(getFixedAssetId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.fixedasset._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.fixedasset._self') }))
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
        entity: t('entity.fixedasset._self')
      })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      count: selectedRows.value.length,
      entity: t('entity.fixedasset._self')
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(
          selectedRows.value.map(record => deleteFixedAsset(getFixedAssetId(record)))
        )
        message.success(t('common.msg.deleteSuccess', { target: t('entity.fixedasset._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.fixedasset._self') }))
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
    if (formData.value?.fixedAssetsId) {
      await update(formData.value.fixedAssetsId, {
        ...formValues,
        fixedAssetsId: formData.value.fixedAssetsId
      })
      message.success(t('common.msg.updateSuccess', { target: t('entity.fixedasset._self') }))
    } else {
      await create(formValues)
      message.success(t('common.msg.createSuccess', { target: t('entity.fixedasset._self') }))
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
  return await importFixedAssets(file, sheetName)
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
    if (advancedQueryForm.value.assetName) queryParams.assetName = advancedQueryForm.value.assetName
    if (advancedQueryForm.value.assetCode) queryParams.assetCode = advancedQueryForm.value.assetCode
    if (advancedQueryForm.value.assetStatus !== undefined)
      queryParams.assetStatus = advancedQueryForm.value.assetStatus
    const blob = await exportFixedAssets(
      queryParams,
      undefined,
      t('entity.fixedasset._self') + t('common.action.exportDataSuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.fixedasset._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.fixedasset._self') }))
  } catch (error: any) {
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.fixedasset._self') }))
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
  advancedQueryForm.value = { assetName: '', assetCode: '', assetStatus: undefined }
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
.accounting-financial-fixed-asset {
  padding: 16px;
}
</style>
