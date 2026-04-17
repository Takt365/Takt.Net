<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/humanresource/organization/dept -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：部门管理页面，左 1/4 部门树、右 3/4 部门树表，含查询、增删改、导入导出等 -->
<!-- ======================================== -->

<template>
  <div class="organization-dept">
    <div class="dept-query-row">
      <TaktTreeLeftQueryBar
        v-model="treeQueryKeyword"
        placeholder="树关键字"
        @search="handleTreeQuerySearch"
      />
      <TaktTreeRightQueryBar
        v-model="queryKeyword"
        placeholder="请输入部门名称或编码"
        :loading="loading"
        @search="handleSearch"
        @reset="handleReset"
      />
    </div>

    <div class="dept-toolbar-row">
      <TaktTreeLeftToolsBar
        v-model:expanded="treeExpanded"
        :loading="loading"
        @search="loadDeptTree"
      />
      <TaktTreeRightToolsBar
        create-permission="humanresource:organization:dept:create"
        update-permission="humanresource:organization:dept:update"
        delete-permission="humanresource:organization:dept:delete"
        import-permission="humanresource:organization:dept:import"
        export-permission="humanresource:organization:dept:export"
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
        :expanded="tableExpanded"
        @create="handleCreate"
        @update="handleUpdate"
        @delete="handleDelete"
        @import="handleImport"
        @export="handleExport"
        @advanced-query="handleAdvancedQuery"
        @column-setting="handleColumnSetting"
        @refresh="handleRefresh"
        @update:expanded="(v: boolean) => (tableExpanded = v)"
      />
    </div>

    <div class="dept-tree-table-wrap">
      <TaktTreeLeftTable
        v-model:expanded-keys="treeExpandedKeys"
        v-model:selected-keys="selectedTreeKeys"
        :tree-data="deptTreeData"
        :tree-field-names="{ title: 'title', key: 'key', children: 'children' }"
        :tree-width-ratio="0.2"
        :loading="loading"
        :virtual="false"
        :draggable="true"
        @tree-select="handleTreeSelect"
        @tree-drop="handleTreeDrop"
      />
      <TaktTreeRightTable
        v-model:expanded-row-keys="tableExpandedRowKeys"
        :columns="displayColumns"
        :data-source="tableTreeData"
        :loading="loading"
        :row-key="getDeptId"
        :stripe="true"
        :row-selection="rowSelection"
        @change="handleTableChange"
        @resize-column="handleResizeColumn"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'deptType'">
            <TaktDictTag
              :value="getDeptField(record, 'deptType')"
              dict-type="sys_dept_type"
            />
          </template>
          <template v-else-if="column.key === 'deptStatus'">
            <TaktDictTag
              :value="getDeptField(record, 'deptStatus')"
              dict-type="sys_normal_disable"
            />
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
      <DeptForm
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
      <a-form-item label="部门名称">
        <a-input v-model:value="advancedQueryForm.deptName" />
      </a-form-item>
      <a-form-item label="部门编码">
        <a-input v-model:value="advancedQueryForm.deptCode" />
      </a-form-item>
      <a-form-item label="部门类型">
        <TaktSelect
          v-model:value="advancedQueryForm.deptType"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_dept_type"
          placeholder="请选择"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
      <a-form-item label="部门状态">
        <TaktSelect
          v-model:value="advancedQueryForm.deptStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_normal_disable"
          placeholder="请选择状态"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.dept._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="deptExcelNames.sheet"
        :template-file-name="deptExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.dept._self') })"
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
import DeptForm from './components/dept-form.vue'
import {
  getDeptList,
  getDeptTreeOptions,
  getDeptById,
  createDept,
  updateDept,
  deleteDeptById,
  getDeptTemplate,
  importDeptData,
  exportDeptData
} from '@/api/human-resource/organization/dept'
import type { Dept } from '@/types/human-resource/organization/dept'
import type { TreeDropPayload } from '@/components/business/takt-tree-left-table/index.vue'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()
const deptExcelNames = taktExcelEntityNames('TaktDept')

const treeQueryKeyword = ref('')
const queryKeyword = ref('')
const treeExpanded = ref(false)
const treeExpandedKeys = ref<(string | number)[]>([])
const tableExpanded = ref(false)
const tableExpandedRowKeys = ref<(string | number)[]>([])
const loading = ref(false)
const dataSource = ref<Dept[]>([])
const fullTableTree = ref<Record<string, unknown>[]>([])
const deptTreeData = ref<TreeDataItem[]>([])
const selectedTreeKeys = ref<(string | number)[]>([])
const total = ref(0)
const selectedRow = ref<Dept | null>(null)
const selectedRows = ref<Dept[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('新增部门')
const formData = ref<Partial<Dept>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ deptName: string; deptCode: string; deptType?: number; deptStatus?: number }>({
  deptName: '',
  deptCode: '',
  deptType: undefined,
  deptStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

/** 将 tree-options 接口数据转为 a-tree 的 TreeDataItem */
function mapOptionsToTreeData(opts: Array<Record<string, unknown>>): TreeDataItem[] {
  if (!opts?.length) return []
  return opts.map((o) => ({
    title: o.dictLabel ?? o.title ?? '',
    key: String(o.dictValue ?? o.value ?? o.deptId ?? ''),
    children: o.children?.length ? mapOptionsToTreeData(o.children) : undefined
  }))
}

/** 将平铺部门列表转为树（parentId 为根标识） */
function flatToTree(list: Dept[], parentId: string | number = '0'): Array<Record<string, unknown>> {
  const pid = String(parentId)
  return list
    .filter(item => String(item.parentId) === pid)
    .map(item => ({
      ...item,
      key: String(item.deptId),
      children: flatToTree(list, item.deptId)
    }))
    .map(node => (node.children.length ? node : { ...node, children: undefined }))
}

/** 从树中取以某 key 为根的子树 */
function getSubtree(nodes: Array<Record<string, unknown>>, key: string | number): Array<Record<string, unknown>> {
  const k = String(key)
  for (const node of nodes) {
    if (String(node.key ?? node.deptId ?? node.id) === k) return [node]
    if (node.children?.length) {
      const found = getSubtree(node.children, key)
      if (found.length) return found
    }
  }
  return []
}

/** 从树数据中收集所有有子节点的 key（用于左侧树展开全部） */
function collectTreeExpandableKeys(nodes: Array<Record<string, unknown>>): (string | number)[] {
  if (!nodes?.length) return []
  const keys: (string | number)[] = []
  for (const node of nodes) {
    const key = node.key ?? node.deptId ?? node.id
    if (key == null) continue
    const children = node.children ?? []
    if (children.length > 0) {
      keys.push(key)
      keys.push(...collectTreeExpandableKeys(children))
    }
  }
  return keys
}

/** 从表格树数据中收集所有有子节点的行 key（用于右侧表展开全部） */
function collectTableExpandableRowKeys(
  rows: Array<Record<string, unknown>>,
  getKey: (record: Record<string, unknown>) => string
): (string | number)[] {
  if (!rows?.length) return []
  const keys: (string | number)[] = []
  for (const row of rows) {
    const key = getKey(row)
    if (!key) continue
    const children = row.children ?? []
    if (children.length > 0) {
      keys.push(key)
      keys.push(...collectTableExpandableRowKeys(children, getKey))
    }
  }
  return keys
}

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

const loadDeptTree = async () => {
  try {
    const res = await getDeptTreeOptions()
    const raw = (res as { data?: unknown })?.data ?? res
    const list = Array.isArray(raw) ? raw : []
    deptTreeData.value = mapOptionsToTreeData(list)
    if (treeExpanded.value) {
      treeExpandedKeys.value = collectTreeExpandableKeys(deptTreeData.value)
    }
  } catch (e: unknown) {
    logger.error('[Dept] 加载部门树失败:', e)
    deptTreeData.value = []
  }
}

const handleTreeSelect = (selectedKeys: (string | number)[]) => {
  selectedTreeKeys.value = selectedKeys
}

/** 从树结构中查找节点 key 的父级 key 与在同级中的序号（用于 parentId / orderNum） */
function findParentAndOrderNum(
  tree: Array<Record<string, unknown>>,
  targetKey: string | number,
  parentKey: string = '0'
): { parentId: string; orderNum: number } | null {
  const keyStr = String(targetKey)
  for (let i = 0; i < tree.length; i++) {
    const node = tree[i]
    const k = String(node?.key ?? node?.deptId ?? '')
    if (k === keyStr) {
      return { parentId: parentKey, orderNum: i }
    }
    const children = node?.children ?? []
    if (children.length) {
      const found = findParentAndOrderNum(children, targetKey, k)
      if (found) return found
    }
  }
  return null
}

const handleTreeDrop = async (payload: TreeDropPayload) => {
  const { newTreeData, dragKey } = payload
  const pos = findParentAndOrderNum(newTreeData, dragKey)
  if (!pos) return
  try {
    loading.value = true
    deptTreeData.value = newTreeData
    const fullRes = await getDeptById(String(dragKey))
    const full = (fullRes as { data?: unknown })?.data ?? fullRes
    await updateDept(String(dragKey), {
      ...full,
      parentId: pos.parentId,
      orderNum: pos.orderNum,
      deptId: String(full?.deptId ?? full?.DeptId ?? dragKey)
    })
    message.success('排序/父级已更新')
    loadData()
  } catch (error: unknown) {
    message.error(error?.message ?? '更新失败')
    loadDeptTree()
  } finally {
    loading.value = false
  }
}

const handleTreeQuerySearch = () => {
  loadDeptTree()
}

/** 左侧展开/收缩：工具栏展开状态与树展开 key 联动 */
watch(
  treeExpanded,
  (expanded) => {
    if (expanded) {
      treeExpandedKeys.value = collectTreeExpandableKeys(deptTreeData.value)
    } else {
      treeExpandedKeys.value = []
    }
  },
  { immediate: false }
)

/** 右侧展开/收缩：工具栏展开状态与表格行展开 key 联动 */
watch(
  tableExpanded,
  (expanded) => {
    if (expanded) {
      tableExpandedRowKeys.value = collectTableExpandableRowKeys(tableTreeData.value, getDeptId)
    } else {
      tableExpandedRowKeys.value = []
    }
  },
  { immediate: false }
)

onMounted(() => {
  loadDeptTree()
  loadData()
})

const getDeptId = (record: Record<string, unknown>): string =>
  record?.deptId != null ? String(record.deptId) : (record?.id != null ? String(record.id) : '')
const getDeptField = (record: Record<string, unknown>, field: string): unknown => record?.[field]

const columns = ref<TableColumnsType>([
  {
    title: 'ID',
    dataIndex: 'deptId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: Record<string, unknown> }) =>
      getDeptField(record, 'deptId') ?? getDeptField(record, 'id') ?? ''
  },
  {
    title: '部门名称',
    dataIndex: 'deptName',
    key: 'deptName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: '部门编码',
    dataIndex: 'deptCode',
    key: 'deptCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: '父级ID',
    dataIndex: 'parentId',
    key: 'parentId',
    width: 90,
    ellipsis: true
  },
  {
    title: '部门类型',
    dataIndex: 'deptType',
    key: 'deptType',
    width: 90
  },
  {
    title: '部门负责人',
    dataIndex: 'deptHead',
    key: 'deptHead',
    width: 100,
    ellipsis: true
  },
  {
    title: '部门电话',
    dataIndex: 'deptPhone',
    key: 'deptPhone',
    width: 120,
    ellipsis: true
  },
  {
    title: '排序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: '数据范围',
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'deptStatus',
    key: 'deptStatus',
    width: 80
  },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'humanresource:organization:dept:update', onClick: (record: Dept) => handleEdit(record) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'humanresource:organization:dept:delete', onClick: (record: Dept) => handleDeleteOne(record) }
    ]
  })
])

const mergedColumns = computed(() => mergeDefaultColumns(columns.value as unknown as TableColumnsType, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const getColumnKey = (col: { key?: unknown; dataIndex?: unknown; title?: unknown }): string =>
    (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: { key?: unknown; dataIndex?: unknown; title?: unknown }) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Dept[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Dept, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getDeptId(selectedRow.value) === getDeptId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Dept[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const loadData = async () => {
  try {
    loading.value = true
    const params: Record<string, unknown> = { PageIndex: 1, PageSize: 9999 }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.deptName) params.DeptName = advancedQueryForm.value.deptName
    if (advancedQueryForm.value.deptCode) params.DeptCode = advancedQueryForm.value.deptCode
    if (advancedQueryForm.value.deptType !== undefined) params.DeptType = advancedQueryForm.value.deptType
    if (advancedQueryForm.value.deptStatus !== undefined) params.DeptStatus = advancedQueryForm.value.deptStatus

    const response = await getDeptList(params)
    const responseAny = response as { Data?: Dept[]; Total?: number }
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
    fullTableTree.value = flatToTree(items)
    if (tableExpanded.value) {
      tableExpandedRowKeys.value = collectTableExpandableRowKeys(tableTreeData.value, getDeptId)
    }
    await loadDeptTree()
  } catch (error: unknown) {
    logger.error('[Dept] 加载数据失败:', error)
    message.error(error?.message || '加载数据失败')
    dataSource.value = []
    fullTableTree.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => loadData()
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { deptName: '', deptCode: '', deptType: undefined, deptStatus: undefined }
  loadData()
}

const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: { field?: unknown; order?: unknown }) => {
  if (sorter?.order) logger.debug('[Dept] 排序:', sorter.field, sorter.order)
}

const handleResizeColumn = (w: number, col: { key?: unknown; dataIndex?: unknown; title?: unknown }) => {
  const column = columns.value.find((c: { key?: unknown; dataIndex?: unknown; title?: unknown }) => {
    const colKey = col.key || col.dataIndex || col.title
    const cKey = c.key || c.dataIndex || c.title
    return colKey && cKey && String(colKey) === String(cKey)
  })
  if (column) (column as { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = '新增部门'
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Dept) => {
  formTitle.value = '编辑部门'
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择要编辑的部门')
}

const handleDeleteOne = (record: Dept) => {
  const name = getDeptField(record, 'deptName') || getDeptId(record)
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除部门 "${name}" 吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await deleteDeptById(getDeptId(record))
        message.success('删除成功')
        loadData()
      } catch (error: unknown) {
        message.error(error?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的部门')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 个部门吗？`,
    okText: '删除',
    cancelText: '取消',
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(record => deleteDeptById(getDeptId(record))))
        message.success('删除成功')
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(error?.message || '删除失败')
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
    if (formData.value?.deptId) {
      await updateDept(formData.value.deptId, { ...formValues, deptId: formData.value.deptId })
      message.success('更新成功')
    } else {
      await createDept(formValues)
      message.success('创建成功')
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (error?.errorFields) return
    message.error(error?.message || '操作失败')
  } finally {
    formLoading.value = false
  }
}

const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

const handleImport = () => { importVisible.value = true }
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getDeptTemplate(sheetName, fileName)
}
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importDeptData(file, sheetName)
}
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}
const handleImportCancel = () => { importVisible.value = false }

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: Record<string, unknown> = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.deptName) queryParams.DeptName = advancedQueryForm.value.deptName
    if (advancedQueryForm.value.deptCode) queryParams.DeptCode = advancedQueryForm.value.deptCode
    if (advancedQueryForm.value.deptType !== undefined) queryParams.DeptType = advancedQueryForm.value.deptType
    if (advancedQueryForm.value.deptStatus !== undefined) queryParams.DeptStatus = advancedQueryForm.value.deptStatus
    const blob = await exportDeptData(queryParams, undefined, '部门数据')
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `部门数据_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success('导出成功')
  } catch (error: unknown) {
    message.error(error?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

const handleAdvancedQuery = () => { advancedQueryVisible.value = true }
const handleAdvancedQuerySubmit = () => {
  loadData()
  advancedQueryVisible.value = false
}
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { deptName: '', deptCode: '', deptType: undefined, deptStatus: undefined }
}

const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }

const handleRefresh = () => loadData()
</script>

<style scoped lang="less">
/* 边距由子组件（takt-tree-left-* / takt-tree-right-*）统一设置，本视图不重复设置 */
.organization-dept {
  padding: 0 4px 0 0;
  display: flex;
  flex-direction: column;
  min-height: 0;
}

.dept-query-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}

.dept-toolbar-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}

.dept-tree-table-wrap {
  flex: 1;
  min-height: 400px;
  display: flex;
  flex-direction: row;
  min-width: 0;
}
</style>
