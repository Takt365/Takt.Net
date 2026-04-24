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
      :placeholder="t('common.form.placeholder.searchkeyword')"
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
      :row-key="getGenTableRowKey"
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
      :title="t('code.generator.page.importfromdb')"
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
    <CodePreview
      v-model="previewVisible"
      :files="previewFiles"
      :loading="previewLoading"
      :validation-issues="previewValidationIssues"
    />

    <!-- 另存为：输入生成路径后确定生成 -->
    <a-modal
      v-model:open="saveAsVisible"
      :title="t('code.generator.page.saveas')"
      :ok-text="t('common.button.ok')"
      :cancel-text="t('common.button.cancel')"
      :confirm-loading="loading"
      @ok="handleSaveAsOk"
      @cancel="saveAsVisible = false"
    >
      <p style="margin-bottom: 8px">
        {{ t('code.generator.page.saveaspathhint') }}
      </p>
      <a-input
        v-model:value="saveAsPath"
        :placeholder="t('code.generator.page.saveaspathplaceholder')"
        allow-clear
        @press-enter="handleSaveAsOk"
      />
    </a-modal>

    <!-- 高级查询抽屉 -->
    <a-drawer
      v-model:open="advancedQueryVisible"
      :title="t('common.button.advancedquery')"
      placement="right"
      width="360"
      @close="advancedQueryVisible = false"
    >
      <a-form layout="vertical">
        <a-form-item :label="t('common.form.placeholder.keyword')">
          <a-input
            v-model:value="queryKeyword"
            :placeholder="t('common.form.placeholder.searchkeyword')"
            allow-clear
          />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button
              type="primary"
              @click="handleAdvancedQuerySubmit"
            >
              {{ t('common.button.query') }}
            </a-button>
            <a-button @click="handleAdvancedQueryReset">
              {{ t('common.button.reset') }}
            </a-button>
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
import { RiEditLine, RiDeleteBinLine, RiEyeLine, RiCodeSSlashLine, RiRefreshLine, RiFileCopyLine, RiRestartLine } from '@remixicon/vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import TaktColumnDrawer from '@/components/business/takt-column-drawer/index.vue'
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
} from '@/api/code/generator/table'
import type { GenTable, GenTableQuery, GenTableCreate, GenTableUpdate } from '@/types/code/generator/gen-table'
import { logger } from '@/utils/logger'
import GenForm from './components/gen-form.vue'
import ImportTable from './components/import-table.vue'
import CodePreview from './components/code-preview.vue'
import type { PreviewFile, PreviewValidationIssue } from './components/code-preview.vue'

const { t } = useI18n()

/** 与后端 entity.gentable._self 一致，用于删除确认、表单标题等业务语境 */
const tableConfig = () => t('entity.gentable._self')

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

function getErrorMessage(error: unknown): string | undefined {
  if (error instanceof Error) return error.message
  if (typeof error === 'object' && error !== null && 'message' in error) {
    const msg = (error as { message?: unknown }).message
    return typeof msg === 'string' ? msg : undefined
  }
  return undefined
}

function getTableId(record: GenTable): string | number | undefined {
  if (record.tableId != null && String(record.tableId) !== '') return String(record.tableId)
  const legacyId = (record as unknown as Record<string, unknown>)['id']
  if (typeof legacyId === 'string' || typeof legacyId === 'number') return legacyId
  return undefined
}

/** 与 `TaktSingleTable` 的 `rowKey` 一致：参数为 `Record<string, unknown>`（组件内即 TableRecord） */
function getGenTableRowKey(record: Record<string, unknown>): string {
  return String(record['tableId'] ?? '')
}

const columns = computed(() => [
  { title: t('entity.gentable.tablename'), dataIndex: 'tableName', key: 'tableName', width: 180, ellipsis: true },
  { title: t('entity.gentable.tablecomment'), dataIndex: 'tableComment', key: 'tableComment', width: 140, ellipsis: true },
  { title: t('entity.gentable.entityclassname'), dataIndex: 'entityClassName', key: 'entityClassName', width: 140 },
  { title: t('entity.gentable.genmodulename'), dataIndex: 'genModuleName', key: 'genModuleName', width: 100 },
  { title: t('entity.gentable.genbusinessname'), dataIndex: 'genBusinessName', key: 'genBusinessName', width: 100 },
  { title: t('entity.gentable.gentemplate'), dataIndex: 'genTemplate', key: 'genTemplate', width: 80 },
  CreateActionColumn<GenTable>({
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
        label: t('common.button.generate'),
        shape: 'plain',
        icon: RiCodeSSlashLine,
        permission: 'code:generator:generate',
        onClick: (record: GenTable) => handleGenerateOne(record)
      },
      {
        key: 'sync',
        label: t('common.button.sync'),
        shape: 'plain',
        icon: RiRefreshLine,
        permission: 'code:generator:sync',
        onClick: (record: GenTable) => handleSync(record)
      },
      {
        key: 'initialize',
        label: t('common.button.initialize'),
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
  const cols = (columns.value || []) as Array<{ key?: unknown }>
  if (keys.length === 0) return cols as TableColumnsType
  const keySet = new Set(keys)
  return cols.filter((c) => keySet.has(String(c.key))) as TableColumnsType
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: GenTable[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  }
}))

const onClickRow = (record: GenTable) => ({
  onClick: () => {
    const key = String(record.tableId ?? '')
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item: GenTable) => selectedRowKeys.value.includes(String(item.tableId ?? '')))
    selectedRow.value = selectedRows.value.length === 1 ? (selectedRows.value[0] ?? null) : null
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: GenTableQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value,
      genBusinessName: ''
    }
    const kw = (queryKeyword.value ?? '').trim()
    if (kw.length > 0) {
      params.KeyWords = kw
    }
    const response = (await getGenTableList(params)) as { data?: GenTable[]; total?: number }
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: unknown) {
    logger.error('[GenTable] 加载失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadfail'))
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

function handleTableChange(
  _pagination: unknown,
  _filters: unknown,
  _sorter: unknown
) {}

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
  formTitle.value = t('common.button.create') + tableConfig()
  formData.value = null
  formVisible.value = true
}

async function handleEdit(record: GenTable) {
  formTitle.value = t('common.button.edit') + tableConfig()
  const id = getTableId(record)
  if (!id) {
    formData.value = { ...record, tableId: record.tableId }
    formVisible.value = true
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    const fallbackTableId = String(id)
    formData.value = {
      ...detail,
      tableId: detail.tableId != null ? String(detail.tableId) : (fallbackTableId || undefined)
    } as Partial<GenTable>
    formVisible.value = true
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.loadtargetfail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: tableConfig() }))
}

function handleDeleteOne(record: GenTable) {
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: tableConfig(), name: record.tableName ?? '' }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteGenTable(String(record.tableId))
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (e: unknown) {
        message.error(getErrorMessage(e) || t('common.msg.deletefail'))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: tableConfig() }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { count: selectedRows.value.length, entity: tableConfig() }),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map((r: GenTable) => deleteGenTable(String(r.tableId))))
        message.success(t('common.msg.deletesuccess'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: unknown) {
        message.error(getErrorMessage(e) || t('common.msg.deletefail'))
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
  if (
    typeof (result as { message?: unknown }).message === 'string' &&
    typeof (result as { count?: unknown }).count === 'number'
  ) {
    const res = result as { message: string; count: number; files: string[] }
    message.success(res.message + (res.files?.length ? t('code.generator.page.gensuccesscount', { count: res.count }) : ''))
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
  message.success(t('code.generator.page.codegenerateddownload'))
}

function showSaveAsPathModal(record: GenTable) {
  saveAsRecord.value = record
  saveAsPath.value = String(record.genPath ?? '').trim() || '/'
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
    message.warning(t('common.form.placeholder.required', { field: t('entity.gentable.genpath') }))
    return
  }
  try {
    loading.value = true
    await doGenerateCode(record, newPath)
    saveAsVisible.value = false
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.actionfail', { action: t('common.button.generate') }))
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
    } catch (e: unknown) {
      message.error(getErrorMessage(e) || t('common.msg.actionfail', { action: t('common.button.generate') }))
    } finally {
      loading.value = false
    }
    return
  }
  try {
    loading.value = true
    const id = String(record.tableId)
    const genPath = String(record.genPath ?? '').trim() || undefined
    const preview = await generateCodePreview(id, genPath)
    const existingFiles = preview?.existingFiles ?? []
    loading.value = false
    if (existingFiles.length > 0) {
      const fileList = existingFiles.slice(0, 20).join('\n') + (existingFiles.length > 20 ? '\n... ' + t('code.generator.page.existingfilessuffix', { count: existingFiles.length }) : '')
      Modal.confirm({
        title: t('code.generator.page.overwriteconfirmtitle'),
        content: t('code.generator.page.overwriteconfirmcontent') + '\n\n' + fileList,
        okText: t('code.generator.page.overwrite'),
        cancelText: genMethod === 2 ? t('common.button.cancel') : t('code.generator.page.saveascancel'),
        onOk: async () => {
          try {
            loading.value = true
            await doGenerateCode(record)
          } catch (e: unknown) {
            message.error(getErrorMessage(e) || t('common.msg.actionfail', { action: t('common.button.generate') }))
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
  } catch (e: unknown) {
    loading.value = false
    message.error(getErrorMessage(e) || t('common.msg.actionfail', { action: t('common.button.generate') }))
  }
}

async function handleSync(record: GenTable) {
  const id = getTableId(record)
  if (!id) {
    message.warning(t('code.generator.page.notableidsync'))
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    const fallbackTableId = String(id)
    formData.value = {
      ...detail,
      tableId: detail.tableId != null ? String(detail.tableId) : (fallbackTableId || undefined)
    } as Partial<GenTable>
    formTitle.value = t('common.button.sync') + tableConfig()
    formVisible.value = true
    message.info(t('code.generator.page.syncformhint'))
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.loadtargetfail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

async function handleInitialize(record: GenTable) {
  const id = getTableId(record)
  if (!id) {
    message.warning(t('code.generator.page.notableidinit'))
    return
  }
  try {
    loading.value = true
    await initializeTable(String(id))
    message.success(t('common.msg.actionsuccess', { action: t('common.button.initialize') }))
    loadData()
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.actionfail', { action: t('common.button.initialize') }))
  } finally {
    loading.value = false
  }
}

async function handleClone(record: GenTable) {
  const id = getTableId(record)
  if (!id) {
    const { tableId: _omitId, ...recordWithoutId } = record
    formData.value = { ...recordWithoutId, tableName: `${record.tableName ?? 'table'}_copy` }
    formTitle.value = t('common.button.clone') + tableConfig()
    formVisible.value = true
    return
  }
  try {
    loading.value = true
    const detail = await getGenTableById(String(id))
    const columns = (detail as { columns?: unknown }).columns
    const { tableId: _omitDetailId, ...detailWithoutId } = detail as GenTable
    const cloneData: Partial<GenTable> = { ...detailWithoutId, tableName: `${detail.tableName ?? 'table'}_copy` }
    if (Array.isArray(columns)) {
      ;(cloneData as { columns?: Record<string, unknown>[] }).columns = columns.map((col) => {
        const c = { ...(col as Record<string, unknown>) }
        delete c.columnId
        delete c.tableId
        return c
      })
    }
    formData.value = cloneData
    formTitle.value = t('common.button.clone') + tableConfig()
    formVisible.value = true
    message.success(t('code.generator.page.clonesuccess'))
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.loadtargetfail', { target: tableConfig() }))
  } finally {
    loading.value = false
  }
}

function handleRefresh() {
  loadData()
}

function handleExport() {
  if (dataSource.value.length === 0) {
    message.warning(t('code.generator.page.nodataexport'))
    return
  }
  try {
    const headers = [
      t('entity.gentable.tablename'),
      t('entity.gentable.tablecomment'),
      t('entity.gentable.entityclassname'),
      t('entity.gentable.genmodulename'),
      t('entity.gentable.genbusinessname'),
      t('entity.gentable.gentemplate')
    ]
    const rows = dataSource.value.map((r: GenTable) =>
      [r.tableName ?? '', r.tableComment ?? '', r.entityClassName ?? '', r.genModuleName ?? '', r.genBusinessName ?? '', r.genTemplate ?? 'crud'].join(',')
    )
    const csv = '\uFEFF' + [headers.join(','), ...rows].join('\n')
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `${tableConfig()}_${new Date().toISOString().slice(0, 10)}.csv`
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.exportsuccess'))
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.exportfail'))
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
      await updateGenTable(values.tableId, values as unknown as GenTableUpdate)
      message.success(t('common.msg.updatesuccess'))
    } else {
      await createGenTable(values as unknown as GenTableCreate)
      message.success(t('common.msg.createsuccess'))
    }
    genFormRef.value?.reset()
    formVisible.value = false
    formData.value = null
    loadData()
  } catch (e: unknown) {
    if (typeof e === 'object' && e !== null && 'errorFields' in e) return
    message.error(getErrorMessage(e) || t('common.msg.operatefail'))
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
  const id = getTableId(record)
  if (!id) {
    message.warning(t('code.generator.page.notableidpreview'))
    return
  }

  selectedRow.value = record
  previewFiles.value = []
  previewValidationIssues.value = []
  previewVisible.value = true
  previewLoading.value = true
  try {
    const genPath = String(record.genPath ?? '').trim() || undefined
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
        content: t('code.generator.page.preview.pathcontent', { path: name }),
        isExisting: existingSet.has(name)
      }))
    }
    if (previewValidationIssues.value.length > 0) {
      message.warning(t('code.generator.page.preview.validationissuetoast', { count: previewValidationIssues.value.length }))
    }
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('code.generator.page.preview.loadfail'))
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
  } catch (e: unknown) {
    message.error(getErrorMessage(e) || t('common.msg.loadfail'))
    databaseTables.value = []
  } finally {
    databaseTablesLoading.value = false
  }
}

async function handleImportSubmit(payload: { configId: string; tableName: string }) {
  try {
    importLoading.value = true
    const imported = await importTable({ configId: payload.configId, tableName: payload.tableName })
    message.success(t('common.msg.createsuccess'))
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
  } catch (e: unknown) {
    const msg =
      (e as { response?: { data?: { message?: string } } }).response?.data?.message ??
      getErrorMessage(e) ??
      t('common.msg.actionfail', { action: t('common.button.import') })
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
