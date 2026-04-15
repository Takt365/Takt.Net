<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/generator/table -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：代码生成表配置管理，包含列表、查询、新增、编辑、删除、导入表、生成代码、预览 -->
<!-- ======================================== -->

<template>
  <div class="generator-table">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('code.generator.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="code:generator:create"
      update-permission="code:generator:update"
      delete-permission="code:generator:delete"
      import-permission="code:generator:import"
      export-permission="code:generator:export"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-import="true"
      :show-export="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :create-disabled="false"
      :update-disabled="!selectedRow"
      :delete-disabled="selectedRows.length === 0"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @import="() => (importVisible = true)"
      @export="handleExport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(record: any) => record.tableId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'genTemplate'">
          <a-tag>{{ record.genTemplate || 'crud' }}</a-tag>
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

    <!-- 新增/编辑表单弹窗：宽度 80%，高度 75% -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="80%"
      :centered="true"
      :body-style="{ height: '75vh', overflow: 'auto' }"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <GenForm
        ref="genFormRef"
        :form-data="formData"
        :database-configs="databaseConfigs"
        :database-tables="databaseTables"
        :database-tables-loading="databaseTablesLoading"
        @config-change="handleImportConfigChange"
      />
    </TaktModal>

    <!-- 导入表弹窗：宽度 80%，高度 75% -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('code.generator.importFromDb')"
      width="80%"
      :centered="true"
      :body-style="{ height: '75vh', overflow: 'auto' }"
      :footer="null"
      @cancel="importVisible = false"
    >
      <ImportTable
        :open="importVisible"
        :database-configs="databaseConfigs"
        :database-tables="databaseTables"
        :database-tables-loading="databaseTablesLoading"
        :import-loading="importLoading"
        @config-change="handleImportConfigChange"
        @submit="handleImportSubmit"
      />
    </TaktModal>

    <!-- 代码预览弹窗 -->
    <CodePreview v-model="previewVisible" :files="previewFiles" :loading="previewLoading" :validation-issues="previewValidationIssues" />

    <!-- 另存为：输入生成路径后确定生成 -->
    <a-modal
      v-model:open="saveAsVisible"
      :title="t('code.generator.saveAs')"
      :ok-text="t('common.button.ok')"
      :cancel-text="t('common.button.cancel')"
      :confirm-loading="loading"
      @ok="handleSaveAsOk"
      @cancel="saveAsVisible = false"
    >
      <p style="margin-bottom: 8px">{{ t('code.generator.saveAsPathHint') }}</p>
      <a-input
        v-model:value="saveAsPath"
        :placeholder="t('code.generator.saveAsPathPlaceholder')"
        allow-clear
        @press-enter="handleSaveAsOk"
      />
    </a-modal>

    <!-- 高级查询抽屉 -->
    <a-drawer
      v-model:open="advancedQueryVisible"
      :title="t('code.generator.advancedQuery')"
      placement="right"
      width="360"
      @close="advancedQueryVisible = false"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('code.generator.searchKeywordLabel')">
          <a-input v-model:value="queryKeyword" :placeholder="t('code.generator.placeholderFuzzy')" allow-clear />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleAdvancedQuerySubmit">{{ t('common.button.query') }}</a-button>
            <a-button @click="handleAdvancedQueryReset">{{ t('common.button.reset') }}</a-button>
          </a-space>
        </a-form-item>
      </a-form>
    </a-drawer>

    <!-- 列设置抽屉 -->
    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'

const { t } = useI18n()
const tableConfig = () => t('code.generator.tableConfig')
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import {
  getGenTableList,
  getGenTableById,
  createGenTable,
  updateGenTable,
  deleteGenTable,
  generateCode,
  generateCodePreview,
  getDatabaseConfigs,
  getDatabaseTables,
  importTable,
  initializeTable,
  type DatabaseConfig,
  type DatabaseTableInfo
} from '@/api/generator/table'
import type { GenTable, GenTableQuery, GenTableCreate, GenTableUpdate } from '@/types/generator/table'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine, RiEyeLine, RiCodeSSlashLine, RiRefreshLine, RiFileCopyLine, RiRestartLine } from '@remixicon/vue'
import TaktColumnDrawer from '@/components/business/takt-column-drawer/index.vue'
import GenForm from './components/gen-form.vue'
import ImportTable from './components/import-table.vue'
import CodePreview from './components/code-preview.vue'
import type { PreviewFile } from './components/code-preview.vue'
import type { PreviewValidationIssue } from './components/code-preview.vue'

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<GenTable[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<GenTable | null>(null)
const selectedRows = ref<GenTable[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formLoading = ref(false)
const genFormRef = ref<InstanceType<typeof GenForm>>()
const formData = ref<Partial<GenTable> | null>(null)
const importVisible = ref(false)
const databaseConfigs = ref<DatabaseConfig[]>([])
const databaseTables = ref<DatabaseTableInfo[]>([])
const databaseTablesLoading = ref(false)
const importLoading = ref(false)
const previewVisible = ref(false)
const previewFiles = ref<PreviewFile[]>([])
const previewLoading = ref(false)
const previewValidationIssues = ref<PreviewValidationIssue[]>([])
const advancedQueryVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])
/** 另存为弹窗：生成路径覆盖 */
const saveAsVisible = ref(false)
const saveAsPath = ref('')
const saveAsRecord = ref<GenTable | null>(null)

const columns = computed(() => [
  { title: t('code.generator.tableName'), dataIndex: 'tableName', key: 'tableName', width: 180, ellipsis: true },
  { title: t('code.generator.tableComment'), dataIndex: 'tableComment', key: 'tableComment', width: 140, ellipsis: true },
  { title: t('code.generator.entityClassName'), dataIndex: 'entityClassName', key: 'entityClassName', width: 140 },
  { title: t('code.generator.genModuleName'), dataIndex: 'genModuleName', key: 'genModuleName', width: 100 },
  { title: t('code.generator.genBusinessName'), dataIndex: 'genBusinessName', key: 'genBusinessName', width: 100 },
  { title: t('code.generator.genTemplate'), dataIndex: 'genTemplate', key: 'genTemplate', width: 80 },
  CreateActionColumn({
    actions: [
      {
        key: 'preview',
        label: t('common.button.preview'),
        shape: 'plain',
        icon: RiEyeLine,
        permission: 'code:generator:preview',
        onClick: (record: GenTable) => handlePreviewOne(record)
      },
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'code:generator:update',
        onClick: (record: GenTable) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'code:generator:delete',
        onClick: (record: GenTable) => handleDeleteOne(record)
      },
      {
        key: 'generate',
        label: t('code.generator.generate'),
        shape: 'plain',
        icon: RiCodeSSlashLine,
        permission: 'code:generator:generate',
        onClick: (record: GenTable) => handleGenerateOne(record)
      },
      {
        key: 'sync',
        label: t('code.generator.sync'),
        shape: 'plain',
        icon: RiRefreshLine,
        permission: 'code:generator:sync',
        onClick: (record: GenTable) => handleSync(record)
      },
      {
        key: 'initialize',
        label: t('code.generator.initialize'),
        shape: 'plain',
        icon: RiRestartLine,
        permission: 'code:generator:initialize',
        visible: (record: GenTable) => record.inDatabase === 1,
        onClick: (record: GenTable) => handleInitialize(record)
      },
      {
        key: 'clone',
        label: t('common.button.clone'),
        shape: 'plain',
        icon: RiFileCopyLine,
        permission: 'code:generator:clone',
        onClick: (record: GenTable) => handleClone(record)
      }
    ]
  })
])

const displayColumns = computed<TableColumnsType>(() => {
  const keys = visibleColumnKeys.value || []
  const cols = (columns.value || []) as any[]
  if (keys.length === 0) return cols as TableColumnsType
  const keySet = new Set(keys)
  return cols.filter((c: any) => keySet.has(String(c.key))) as TableColumnsType
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: GenTable[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: GenTable) => ({
  onClick: () => {
    const key = String(record.tableId ?? '')
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item: GenTable) => selectedRowKeys.value.includes(String(item.tableId ?? '')))
    selectedRow.value = selectedRows.value.length === 1 ? selectedRows.value[0] : null
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: GenTableQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    const response = (await getGenTableList(params)) as any
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[GenTable] 加载失败:', error)
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
  currentPage.value = 1
  loadData()
}

function handleTableChange(_pagination: any, _filters: any, _sorter: any) {}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(current: number, size: number) {
  currentPage.value = current
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('common.button.create') + t('code.generator.tableConfig')
  formData.value = null
  formVisible.value = true
}

async function handleEdit(record: GenTable) {
  formTitle.value = t('common.button.edit') + t('code.generator.tableConfig')
  const id = record.tableId ?? (record as any).id
  if (!id) {
    formData.value = { ...record, tableId: record.tableId }
    formVisible.value = true
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    formData.value = { ...detail, tableId: detail.tableId ?? String(id) }
    formVisible.value = true
  } catch (e: any) {
    message.error(e?.message || t('common.msg.loadTargetFail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: tableConfig() }))
}

function handleDeleteOne(record: GenTable) {
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: tableConfig(), name: record.tableName ?? '' }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteGenTable(String(record.tableId))
        message.success(t('common.msg.deleteSuccess'))
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: tableConfig() }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: tableConfig() }),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map((r: GenTable) => deleteGenTable(String(r.tableId))))
        message.success(t('common.msg.deleteSuccess'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail'))
      } finally {
        loading.value = false
      }
    }
  })
}

async function doGenerateCode(record: GenTable, genPathOverride?: string) {
  const id = String(record.tableId)
  const genMethod = record.genMethod != null ? Number(record.genMethod) : 0
  const result = await generateCode(id, genMethod, genPathOverride)
  if (typeof (result as any).message === 'string' && typeof (result as any).count === 'number') {
    const res = result as { message: string; count: number; files: string[] }
    message.success(res.message + (res.files?.length ? t('code.generator.genSuccessCount', { count: res.count }) : ''))
    return
  }
  const blob = result as Blob
  const url = window.URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  const ts = new Date().toISOString().replace(/[-:T]/g, '').slice(0, 14)
  link.download = `${record.tableName ?? id}_${ts}.zip`
  link.style.display = 'none'
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  setTimeout(() => window.URL.revokeObjectURL(url), 100)
  message.success(t('code.generator.codeGeneratedDownload'))
}

function showSaveAsPathModal(record: GenTable) {
  saveAsRecord.value = record
  saveAsPath.value = (record.genPath ?? '').trim() || '/'
  saveAsVisible.value = true
}

async function handleSaveAsOk() {
  const record = saveAsRecord.value
  const newPath = saveAsPath.value?.trim() || ''
  if (!record) {
    saveAsVisible.value = false
    return
  }
  if (!newPath) {
    message.warning(t('common.form.placeholder.required', { field: t('code.generator.genPath') }))
    return
  }
  try {
    loading.value = true
    await doGenerateCode(record, newPath)
    saveAsVisible.value = false
  } catch (e: any) {
    message.error(e?.message || t('common.msg.actionFail', { action: t('code.generator.generate') }))
  } finally {
    loading.value = false
  }
}

async function handleGenerateOne(record: GenTable) {
  const genMethod = record.genMethod != null ? Number(record.genMethod) : 0
  if (genMethod !== 1 && genMethod !== 2) {
    try {
      loading.value = true
      await doGenerateCode(record)
    } catch (e: any) {
      message.error(e?.message || t('common.msg.actionFail', { action: t('code.generator.generate') }))
    } finally {
      loading.value = false
    }
    return
  }
  try {
    loading.value = true
    const id = String(record.tableId)
    const genPath = (record.genPath ?? '').trim() || undefined
    const preview = await generateCodePreview(id, genPath)
    const existingFiles = preview?.existingFiles ?? []
    loading.value = false
    if (existingFiles.length > 0) {
      const fileList = existingFiles.slice(0, 20).join('\n') + (existingFiles.length > 20 ? '\n... ' + t('code.generator.existingFilesSuffix', { count: existingFiles.length }) : '')
      Modal.confirm({
        title: t('code.generator.overwriteConfirmTitle'),
        content: t('code.generator.overwriteConfirmContent') + '\n\n' + fileList,
        okText: t('code.generator.overwrite'),
        cancelText: genMethod === 2 ? t('common.button.cancel') : t('code.generator.saveAsCancel'),
        onOk: async () => {
          try {
            loading.value = true
            await doGenerateCode(record)
          } catch (e: any) {
            message.error(e?.message || t('common.msg.actionFail', { action: t('code.generator.generate') }))
          } finally {
            loading.value = false
          }
        },
        onCancel: () => {
          if (genMethod !== 2) showSaveAsPathModal(record)
        }
      })
    } else {
      try {
        loading.value = true
        await doGenerateCode(record)
      } finally {
        loading.value = false
      }
    }
  } catch (e: any) {
    loading.value = false
    message.error(e?.message || t('common.msg.actionFail', { action: t('code.generator.generate') }))
  }
}

async function handleSync(record: GenTable) {
  const id = record.tableId ?? (record as any).id
  if (!id) {
    message.warning(t('code.generator.noTableIdSync'))
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    formData.value = { ...detail, tableId: detail.tableId ?? String(id) }
    formTitle.value = t('code.generator.sync') + t('code.generator.tableConfig')
    formVisible.value = true
    message.info(t('code.generator.syncFormHint'))
  } catch (e: any) {
    message.error(e?.message || t('common.msg.loadTargetFail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

async function handleInitialize(record: GenTable) {
  const id = record.tableId ?? (record as any).id
  if (!id) {
    message.warning(t('code.generator.noTableIdInit'))
    return
  }
  try {
    loading.value = true
    await initializeTable(String(id))
    message.success(t('common.msg.actionSuccess', { action: t('code.generator.initialize') }))
    loadData()
  } catch (e: any) {
    message.error(e?.message || t('common.msg.actionFail', { action: t('code.generator.initialize') }))
  } finally {
    loading.value = false
  }
}

async function handleClone(record: GenTable) {
  const id = record.tableId ?? (record as any).id
  if (!id) {
    formData.value = { ...record, tableId: undefined, tableName: `${record.tableName ?? 'table'}_copy` }
    formTitle.value = t('common.button.clone') + t('code.generator.tableConfig')
    formVisible.value = true
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    const columns = (detail as any).columns
    const cloneData: Partial<GenTable> = { ...detail, tableId: undefined, tableName: `${detail.tableName ?? 'table'}_copy` }
    delete (cloneData as any).tableId
    if (Array.isArray(columns)) {
      (cloneData as any).columns = columns.map((col: any) => {
        const c = { ...col }
        delete c.columnId
        delete c.tableId
        return c
      })
    }
    formData.value = cloneData
    formTitle.value = t('common.button.clone') + t('code.generator.tableConfig')
    formVisible.value = true
    message.success(t('code.generator.cloneSuccess'))
  } catch (e: any) {
    message.error(e?.message || t('common.msg.loadTargetFail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

function handleRefresh() {
  loadData()
}

function handleExport() {
  if (dataSource.value.length === 0) {
    message.warning(t('code.generator.noDataToExport'))
    return
  }
  try {
    const headers = [t('code.generator.tableName'), t('code.generator.tableComment'), t('code.generator.entityClassName'), t('code.generator.genModuleName'), t('code.generator.genBusinessName'), t('code.generator.genTemplate')]
    const rows = dataSource.value.map((r: GenTable) =>
      [r.tableName ?? '', r.tableComment ?? '', r.entityClassName ?? '', r.genModuleName ?? '', r.genBusinessName ?? '', r.genTemplate ?? 'crud'].join(',')
    )
    const csv = '\uFEFF' + [headers.join(','), ...rows].join('\n')
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `${t('code.generator.exportFileName')}_${new Date().toISOString().slice(0, 10)}.csv`
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportSuccess'))
  } catch (e: any) {
    message.error(e?.message || t('common.msg.exportFail'))
  }
}

function handleAdvancedQuery() {
  advancedQueryVisible.value = true
}

function handleAdvancedQuerySubmit() {
  advancedQueryVisible.value = false
  handleSearch()
}

function handleAdvancedQueryReset() {
  queryKeyword.value = ''
  advancedQueryVisible.value = false
  handleSearch()
}

function handleColumnSetting() {
  columnSettingVisible.value = true
}

function handleColumnKeysChange(keys: (string | number)[]) {
  visibleColumnKeys.value = keys.map(k => String(k))
}

function handleColumnSettingReset() {
  visibleColumnKeys.value = []
}

async function handleFormSubmit() {
  try {
    await genFormRef.value?.validate()
    const values = genFormRef.value?.getValues()
    if (!values) return
    formLoading.value = true
    if (values.tableId) {
      await updateGenTable(values.tableId, values as GenTableUpdate)
      message.success(t('common.msg.updateSuccess'))
    } else {
      await createGenTable(values as GenTableCreate)
      message.success(t('common.msg.createSuccess'))
    }
    genFormRef.value?.reset()
    formVisible.value = false
    formData.value = null
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || t('common.msg.operateFail'))
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  genFormRef.value?.reset()
  formVisible.value = false
  formData.value = null
}

async function handlePreviewOne(record: GenTable) {
  const id = record.tableId ?? (record as any).id
  if (!id) {
    message.warning(t('code.generator.noTableIdPreview'))
    return
  }

  selectedRow.value = record
  previewFiles.value = []
  previewValidationIssues.value = []
  previewVisible.value = true
  previewLoading.value = true
  try {
    const genPath = (record.genPath ?? '').trim() || undefined
    const preview = await generateCodePreview(String(id), genPath)
    previewValidationIssues.value = preview?.validationIssues ?? []
    const previewItems = preview?.previewFiles ?? []
    if (previewItems.length > 0) {
      previewFiles.value = previewItems.map((item) => ({
        name: item.path,
        content: item.content,
        isExisting: item.isExisting
      }))
    } else {
      const files = preview?.files ?? []
      const existingSet = new Set(preview?.existingFiles ?? [])
      previewFiles.value = files.map((name) => ({
        name,
        content: t('code.generator.previewPathContent', { path: name }),
        isExisting: existingSet.has(name)
      }))
    }
    if (previewValidationIssues.value.length > 0) {
      message.warning(t('code.generator.previewValidationIssueToast', { count: previewValidationIssues.value.length }))
    }
  } catch (e: any) {
    message.error(e?.message || t('code.generator.previewLoadFail'))
    previewFiles.value = []
  } finally {
    previewLoading.value = false
  }
}

async function handleImportConfigChange(configId: string) {
  databaseTables.value = []
  if (!configId) return
  try {
    databaseTablesLoading.value = true
    const list = await getDatabaseTables(configId)
    databaseTables.value = list ?? []
  } catch (e: any) {
    message.error(e?.message || t('common.msg.loadFail'))
    databaseTables.value = []
  } finally {
    databaseTablesLoading.value = false
  }
}

async function handleImportSubmit(payload: { configId: string; tableName: string }) {
  try {
    importLoading.value = true
    const imported = await importTable({ configId: payload.configId, tableName: payload.tableName })
    message.success(t('common.msg.createSuccess'))
    importVisible.value = false
    databaseTables.value = []
    await loadData()
    const id = String(imported?.tableId ?? imported?.tableName ?? '')
    if (id) {
      selectedRowKeys.value = [id]
      const found = dataSource.value.find((r: GenTable) => String(r.tableId ?? '') === id || String(r.tableName) === payload.tableName)
      selectedRows.value = found ? [found] : (imported ? [imported] : [])
      selectedRow.value = found ?? imported ?? null
    }
  } catch (e: any) {
    const msg = e?.response?.data?.message ?? e?.message ?? t('common.msg.actionFail', { action: t('common.button.import') })
    message.error(msg)
  } finally {
    importLoading.value = false
  }
}

onMounted(() => {
  loadData()
  getDatabaseConfigs().then(list => {
    databaseConfigs.value = list ?? []
  }).catch(() => {
    databaseConfigs.value = []
  })
})
</script>

<style scoped lang="less">
.generator-table {
  padding: 16px;
}
</style>
