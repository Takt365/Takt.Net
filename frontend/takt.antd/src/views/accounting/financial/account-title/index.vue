<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/accounting/financial/account-title -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：会计科目管理，左树右表、增删改、导入导出 -->
<!-- ======================================== -->

<template>
  <div class="accounting-financial-account-title">
    <div class="title-query-row">
      <TaktTreeLeftQueryBar
        v-model="treeQueryKeyword"
        placeholder="树关键字"
        @search="handleTreeQuerySearch"
      />
      <TaktTreeRightQueryBar
        v-model="queryKeyword"
        :placeholder="t('common.form.placeholder.search', { keyword: [t('accounting.title.name'), t('accounting.title.code')].join(t('common.action.or')) })"
        :loading="loading"
        @search="handleSearch"
        @reset="handleReset"
      />
    </div>

    <div class="title-toolbar-row">
      <TaktTreeLeftToolsBar
        v-model:expanded="treeExpanded"
        :loading="loading"
        @search="loadTitleTree"
      />
      <TaktTreeRightToolsBar
        create-permission="accounting:title:create"
        update-permission="accounting:title:update"
        delete-permission="accounting:title:delete"
        import-permission="accounting:title:import"
        template-permission="accounting:title:template"
        export-permission="accounting:title:export"
        :show-create="true"
        :show-update="true"
        :show-delete="true"
        :show-import="true"
        :show-export="true"
        :show-advanced-query="true"
        :show-column-setting="true"
        :show-fullscreen="true"
        :show-refresh="true"
        :show-expand="true"
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
        :expanded="tableExpanded"
        @update:expanded="(v: boolean) => (tableExpanded = v)"
      />
    </div>

    <div class="title-tree-table-wrap">
      <TaktTreeLeftTable
        :tree-data="filteredTitleTreeData"
        :tree-field-names="{ title: 'title', key: 'key', children: 'children' }"
        :tree-width-ratio="0.2"
        v-model:expanded-keys="treeExpandedKeys"
        v-model:selected-keys="selectedTreeKeys"
        :loading="loading"
        :virtual="false"
        :draggable="false"
        @tree-select="handleTreeSelect"
      />
      <TaktTreeRightTable
        ref="tableRef"
        :columns="displayColumns"
        :data-source="tableTreeData"
        :loading="loading"
        :row-key="getTitleId"
        :stripe="true"
        v-model:expanded-row-keys="tableExpandedRowKeys"
        :row-selection="rowSelection"
        @change="handleTableChange"
        @resize-column="handleResizeColumn"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'titleType'">
            <a-tag :color="getTitleTypeColor(getTitleField(record, 'titleType'))">
              {{ getTitleTypeLabel(getTitleField(record, 'titleType')) }}
            </a-tag>
          </template>
          <template v-else-if="column.key === 'balanceDirection'">
            {{ getTitleField(record, 'balanceDirection') === 0 ? t('accounting.title.debit') : t('accounting.title.credit') }}
          </template>
          <template v-else-if="column.key === 'titleStatus'">
            <TaktDictTag
              :value="getTitleField(record, 'titleStatus')"
              dict-type="sys_normal_disable"
            />
          </template>
          <template v-else-if="column.key === 'isReconciliationAccount'">
            {{ getTitleField(record, 'isReconciliationAccount') === 1 ? t('common.button.yes') : t('common.button.no') }}
          </template>
        </template>
      </TaktTreeRightTable>
    </div>

    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AccountTitleForm
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
      <a-form-item :label="t('accounting.title.name')">
        <a-input v-model:value="advancedQueryForm.titleName" />
      </a-form-item>
      <a-form-item :label="t('accounting.title.code')">
        <a-input v-model:value="advancedQueryForm.titleCode" />
      </a-form-item>
      <a-form-item :label="t('accounting.title.type')">
        <a-select
          v-model:value="advancedQueryForm.titleType"
          :placeholder="t('common.form.placeholder.select', { field: t('accounting.title.type') })"
          allow-clear
          :options="titleTypeOptions"
        />
      </a-form-item>
      <a-form-item :label="t('accounting.title.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.titleStatus"
          dict-type="sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('accounting.title.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('accounting.title._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('accounting.title._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('accounting.title._self') })"
        template-permission="accounting:title:template"
        import-permission="accounting:title:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('accounting.title._self') })"
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
import { ref, computed, watch, onMounted } from 'vue'
import type { TreeDataItem } from 'ant-design-vue/es/tree'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import AccountTitleForm from './components/account-title-form.vue'
import {
  getTree,
  create,
  update,
  remove,
  getTemplate,
  importTitles,
  exportTitles
} from '@/api/accounting/financial/account-title'
import type { AccountTitle, AccountTitleTree, AccountTitleQuery } from '@/types/accounting/financial/account-title'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const treeQueryKeyword = ref('')
const queryKeyword = ref('')
const treeExpanded = ref(false)
const treeExpandedKeys = ref<(string | number)[]>([])
const tableExpanded = ref(false)
const tableExpandedRowKeys = ref<(string | number)[]>([])
const loading = ref(false)
const fullTableTree = ref<any[]>([])
const titleTreeData = ref<TreeDataItem[]>([])
const selectedTreeKeys = ref<(string | number)[]>([])
const selectedRow = ref<AccountTitle | null>(null)
const selectedRows = ref<AccountTitle[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<AccountTitle> | null>(null)
const formLoading = ref(false)
const formRef = ref<InstanceType<typeof AccountTitleForm>>()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ titleName: string; titleCode: string; titleType?: number; titleStatus?: number }>({
  titleName: '',
  titleCode: '',
  titleType: undefined,
  titleStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const titleTypeOptions = [
  { label: '资产', value: 0 },
  { label: '负债', value: 1 },
  { label: '所有者权益', value: 2 },
  { label: '收入', value: 3 },
  { label: '费用', value: 4 },
  { label: '成本', value: 5 }
]

function getTitleTypeLabel(type: number): string {
  const o = titleTypeOptions.find(x => x.value === type)
  return o?.label ?? String(type)
}
function getTitleTypeColor(type: number): string {
  const colors: Record<number, string> = {
    0: 'blue',
    1: 'orange',
    2: 'green',
    3: 'cyan',
    4: 'purple',
    5: 'red'
  }
  return colors[type] ?? 'default'
}

/** 将后端树节点转为左侧 a-tree 格式（title, key, children） */
function mapTitleTreeToTreeData(nodes: AccountTitleTree[]): TreeDataItem[] {
  if (!nodes?.length) return []
  return nodes.map((n: AccountTitleTree) => ({
    title: n.titleName ?? '',
    key: String(n.titleId ?? ''),
    children: n.children?.length ? mapTitleTreeToTreeData(n.children) : undefined
  }))
}

/** 为表格树添加 key 并保持 children */
function mapTitleTreeToTableTree(nodes: AccountTitleTree[]): any[] {
  if (!nodes?.length) return []
  return nodes.map((n: AccountTitleTree) => ({
    ...n,
    key: String(n.titleId ?? ''),
    children: n.children?.length ? mapTitleTreeToTableTree(n.children) : undefined
  }))
}

function getSubtree(nodes: any[], key: string | number): any[] {
  const k = String(key)
  for (const node of nodes) {
    if (String(node.key ?? node.titleId ?? node.id) === k) return [node]
    if (node.children?.length) {
      const found = getSubtree(node.children, key)
      if (found.length) return found
    }
  }
  return []
}

function filterTreeByKeyword(nodes: TreeDataItem[], keyword: string): TreeDataItem[] {
  const k = (keyword ?? '').trim().toLowerCase()
  if (!k) return nodes
  function filter(list: TreeDataItem[]): TreeDataItem[] {
    if (!list?.length) return []
    return list
      .map(node => {
        const title = String(node.title ?? '').toLowerCase()
        const matched = title.includes(k)
        const filteredChildren = node.children?.length ? filter(node.children) : undefined
        const hasMatchInChildren = filteredChildren && filteredChildren.length > 0
        if (matched || hasMatchInChildren) {
          return { ...node, children: filteredChildren } as TreeDataItem
        }
        return null
      })
      .filter(Boolean) as TreeDataItem[]
  }
  return filter(nodes)
}

function collectTreeExpandableKeys(nodes: TreeDataItem[]): (string | number)[] {
  if (!nodes?.length) return []
  const keys: (string | number)[] = []
  for (const node of nodes) {
    const key = node.key
    if (key != null && node.children?.length) {
      keys.push(key)
      keys.push(...collectTreeExpandableKeys(node.children))
    }
  }
  return keys
}

function collectTableExpandableRowKeys(rows: any[], getKey: (r: any) => string): (string | number)[] {
  if (!rows?.length) return []
  const keys: (string | number)[] = []
  for (const row of rows) {
    const key = getKey(row)
    if (key && (row.children?.length ?? 0) > 0) {
      keys.push(key)
      keys.push(...collectTableExpandableRowKeys(row.children, getKey))
    }
  }
  return keys
}

const filteredTitleTreeData = computed(() =>
  filterTreeByKeyword(titleTreeData.value, treeQueryKeyword.value)
)

const tableTreeData = computed(() => {
  const tree = fullTableTree.value
  if (!tree?.length) return []
  const keys = selectedTreeKeys.value
  if (keys.length === 1) {
    const sub = getSubtree(tree, keys[0])
    return sub.length ? sub : tree
  }
  return tree
})

const getTitleId = (record: any): string =>
  record?.titleId != null ? String(record.titleId) : (record?.id != null ? String(record.id) : '')
const getTitleField = (record: any, field: string): any => record?.[field]

const columns = ref<TableColumnsType>([
  {
    title: t('accounting.title.code'),
    dataIndex: 'titleCode',
    key: 'titleCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('accounting.title.name'),
    dataIndex: 'titleName',
    key: 'titleName',
    width: 180,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('accounting.title.type'),
    dataIndex: 'titleType',
    key: 'titleType',
    width: 100
  },
  {
    title: t('accounting.title.balanceDirection'),
    dataIndex: 'balanceDirection',
    key: 'balanceDirection',
    width: 80
  },
  {
    title: t('accounting.title.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('accounting.title.effectiveDate'),
    dataIndex: 'effectiveDate',
    key: 'effectiveDate',
    width: 110
  },
  {
    title: t('accounting.title.expiryDate'),
    dataIndex: 'expiryDate',
    key: 'expiryDate',
    width: 110
  },
  {
    title: t('accounting.title.isReconciliationAccount'),
    dataIndex: 'isReconciliationAccount',
    key: 'isReconciliationAccount',
    width: 100
  },
  {
    title: t('accounting.title.status'),
    dataIndex: 'titleStatus',
    key: 'titleStatus',
    width: 80
  },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.update'), shape: 'plain', icon: RiEditLine, permission: 'accounting:title:update', onClick: (record: AccountTitle) => handleEdit(record) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'accounting:title:delete', onClick: (record: AccountTitle) => handleDeleteOne(record) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: any): string => String(col.key || col.dataIndex || col.title || '')
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => keysSet.has(getColumnKey(col)))
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AccountTitle[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AccountTitle, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getTitleId(selectedRow.value) === getTitleId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AccountTitle[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

async function loadTitleTree() {
  try {
    loading.value = true
    const tree = await getTree('0', true)
    const list = Array.isArray(tree) ? tree : []
    titleTreeData.value = mapTitleTreeToTreeData(list)
    fullTableTree.value = mapTitleTreeToTableTree(list)
    if (treeExpanded.value) {
      treeExpandedKeys.value = collectTreeExpandableKeys(titleTreeData.value)
    }
    if (tableExpanded.value) {
      tableExpandedRowKeys.value = collectTableExpandableRowKeys(tableTreeData.value, getTitleId)
    }
  } catch (e: any) {
    logger.error('[AccountingTitle] 加载科目树失败:', e)
    message.error(e?.message ?? '加载失败')
    titleTreeData.value = []
    fullTableTree.value = []
  } finally {
    loading.value = false
  }
}

watch(
  treeExpanded,
  (expanded) => {
    if (expanded) treeExpandedKeys.value = collectTreeExpandableKeys(titleTreeData.value)
    else treeExpandedKeys.value = []
  },
  { immediate: false }
)
watch(
  tableExpanded,
  (expanded) => {
    if (expanded) tableExpandedRowKeys.value = collectTableExpandableRowKeys(tableTreeData.value, getTitleId)
    else tableExpandedRowKeys.value = []
  },
  { immediate: false }
)

const handleTreeSelect = (selectedKeys: (string | number)[]) => {
  selectedTreeKeys.value = selectedKeys
}
const handleTreeQuerySearch = () => loadTitleTree()
const handleSearch = () => loadTitleTree()
const handleReset = () => {
  treeQueryKeyword.value = ''
  queryKeyword.value = ''
  advancedQueryForm.value = { titleName: '', titleCode: '', titleType: undefined, titleStatus: undefined }
  loadTitleTree()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[AccountingTitle] 排序:', sorter.field, sorter.order)
}
const handleResizeColumn = (w: number, col: any) => {
  const column = columns.value.find((c: any) => String(c.key || c.dataIndex || c.title) === String(col.key || col.dataIndex || col.title))
  if (column) (column as any).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('accounting.title._self')
  formData.value = selectedRow.value ? { parentId: getTitleId(selectedRow.value) } : {}
  formVisible.value = true
}
const handleEdit = (record: AccountTitle) => {
  formTitle.value = t('common.button.update') + t('accounting.title._self')
  formData.value = { ...record }
  formVisible.value = true
}
const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('accounting.title._self') }))
}
const handleDeleteOne = (record: AccountTitle) => {
  const name = getTitleField(record, 'titleName') || getTitleId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('accounting.title._self'), name: String(name) }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(getTitleId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('accounting.title._self') }))
        loadTitleTree()
      } catch (error: any) {
        message.error(error?.message ?? t('common.msg.deleteFail', { target: t('accounting.title._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('accounting.title._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('accounting.title._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(r => remove(getTitleId(r))))
        message.success(t('common.msg.deleteSuccess', { target: t('accounting.title._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadTitleTree()
      } catch (error: any) {
        message.error(error?.message ?? t('common.msg.deleteFail', { target: t('accounting.title._self') }))
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
    const values = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.titleId) {
      await update(formData.value.titleId, { ...values, titleId: formData.value.titleId })
      message.success(t('common.msg.updateSuccess', { target: t('accounting.title._self') }))
    } else {
      await create(values)
      message.success(t('common.msg.createSuccess', { target: t('accounting.title._self') }))
    }
    formRef.value?.resetFields()
    formData.value = null
    formVisible.value = false
    loadTitleTree()
  } catch (error: any) {
    if (error?.errorFields) return
    message.error(error?.message ?? t('common.msg.operateFail', { action: t('common.button.confirm') }))
  } finally {
    formLoading.value = false
  }
}
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = null
  formRef.value?.resetFields()
}

const handleImport = () => { importVisible.value = true }
const handleDownloadTemplate = async (sheetName?: string, fileName?: string): Promise<Blob> =>
  getTemplate(sheetName, fileName)
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> =>
  importTitles(file, sheetName)
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadTitleTree()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}
const handleImportCancel = () => { importVisible.value = false }

const handleExport = async () => {
  try {
    loading.value = true
    const query: AccountTitleQuery = {
      pageIndex: 1,
      pageSize: 99999,
      keyWords: queryKeyword.value || undefined,
      titleName: advancedQueryForm.value.titleName || undefined,
      titleCode: advancedQueryForm.value.titleCode || undefined,
      titleType: advancedQueryForm.value.titleType,
      titleStatus: advancedQueryForm.value.titleStatus
    }
    const blob = await exportTitles(query, undefined, t('accounting.title.exportFileName'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `会计科目_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('accounting.title._self') }))
  } catch (error: any) {
    message.error(error?.message ?? t('common.msg.exportFail', { target: t('accounting.title._self') }))
  } finally {
    loading.value = false
  }
}

const handleAdvancedQuery = () => { advancedQueryVisible.value = true }
const handleAdvancedQuerySubmit = () => {
  loadTitleTree()
  advancedQueryVisible.value = false
}
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { titleName: '', titleCode: '', titleType: undefined, titleStatus: undefined }
}
const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }
const handleRefresh = () => loadTitleTree()

onMounted(() => loadTitleTree())
</script>

<style scoped lang="less">
.accounting-financial-account-title {
  padding: 0 4px 0 0;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
.title-query-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}
.title-toolbar-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}
.title-tree-table-wrap {
  flex: 1;
  min-height: 400px;
  display: flex;
  flex-direction: row;
  min-width: 0;
}
</style>
