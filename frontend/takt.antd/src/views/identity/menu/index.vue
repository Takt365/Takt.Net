<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/menu -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：菜单管理页面，包含菜单列表、查询、新增、编辑、删除、导入、导出等 -->
<!-- ======================================== -->

<template>
  <div class="identity-menu">
    <!-- 第一行：左 1/4 树查询栏 | 右 3/4 表查询栏 -->
    <div class="menu-query-row">
      <TaktTreeLeftQueryBar
        v-model="treeQueryKeyword"
        placeholder="树关键字"
        @search="handleTreeQuerySearch"
      />
      <TaktTreeRightQueryBar
        v-model="queryKeyword"
        :placeholder="t('common.form.placeholder.search', { keyword: t('entity.menu.name') + t('common.action.or') + t('entity.menu.code') })"
        :loading="loading"
        @search="handleSearch"
        @reset="handleReset"
      />
    </div>

    <!-- 第二行：左 1/4 树工具栏 | 右 3/4 表工具栏 -->
    <div class="menu-toolbar-row">
      <TaktTreeLeftToolsBar
        v-model:expanded="treeExpanded"
        :loading="loading"
        @search="loadData"
      />
      <TaktTreeRightToolsBar
        create-permission="identity:menu:create"
        update-permission="identity:menu:update"
        delete-permission="identity:menu:delete"
        import-permission="identity:menu:import"
        template-permission="identity:menu:template"
        export-permission="identity:menu:export"
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

    <!-- 第三行：左 1/4 树 | 右 3/4 树表 -->
    <div class="menu-tree-table-wrap">
      <TaktTreeLeftTable
        :tree-data="filteredMenuTreeData"
        :tree-field-names="{ title: 'title', key: 'key', children: 'children' }"
        :tree-width-ratio="0.2"
        v-model:expanded-keys="treeExpandedKeys"
        v-model:selected-keys="selectedTreeKeys"
        :loading="loading"
        :virtual="false"
        :draggable="true"
        @tree-select="handleTreeSelect"
        @tree-drop="handleMenuTreeDrop"
      />
      <TaktTreeRightTable
        ref="tableRef"
        :columns="displayColumns"
        :data-source="tableTreeData"
        :loading="loading"
        :row-key="getMenuId"
        :stripe="true"
        v-model:expanded-row-keys="tableExpandedRowKeys"
        :row-selection="rowSelection"
        @change="handleTableChange"
        @resize-column="handleResizeColumn"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'menuType'">
            <a-tag :color="getMenuTypeColor(getMenuField(record, 'menuType'))">
              {{ getMenuTypeLabel(getMenuField(record, 'menuType')) }}
            </a-tag>
          </template>
          <template v-else-if="column.key === 'menuStatus'">
            <TaktDictTag
              :value="getMenuField(record, 'menuStatus')"
              dict-type="sys_normal_disable"
            />
          </template>
          <template v-else-if="column.key === 'isVisible'">
            {{ getMenuField(record, 'isVisible') === 0 ? t('common.button.yes') : t('common.button.no') }}
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
      <MenuForm
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
      <a-form-item :label="t('entity.menu.name')">
        <a-input v-model:value="advancedQueryForm.menuName" />
      </a-form-item>
      <a-form-item :label="t('entity.menu.code')">
        <a-input v-model:value="advancedQueryForm.menuCode" />
      </a-form-item>
      <a-form-item :label="t('entity.menu.type')">
        <a-select
          v-model:value="advancedQueryForm.menuType"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.type') })"
          allow-clear
          :options="menuTypeOptions"
        />
      </a-form-item>
      <a-form-item :label="t('entity.menu.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.menuStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.menu.status') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.menu._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.menu._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.menu._self') })"
        template-permission="identity:menu:template"
        import-permission="identity:menu:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.menu._self') })"
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
import type { TreeDataItem } from 'ant-design-vue/es/tree'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import MenuForm from './components/menu-form.vue'
import { getList, create, update, remove, getTemplate, importMenus, exportMenus } from '@/api/identity/menu'
import type { Menu, MenuUpdate } from '@/types/identity/menu'
import type { TreeDropPayload } from '@/components/business/takt-tree-left-table/index.vue'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const treeQueryKeyword = ref('')
/** 左侧树结构展开状态：true=全部展开，false=全部折叠（仅左侧工具栏控制），默认收缩 */
const treeExpanded = ref(false)
/** 左侧树当前展开的节点 key 列表（受控传给 TaktTreeTable） */
const treeExpandedKeys = ref<(string | number)[]>([])
/** 右侧树表行展开状态：true=全部展开，false=全部折叠（仅右侧工具栏控制），默认收缩 */
const tableExpanded = ref(false)
/** 右侧树表当前展开的行 key 列表（受控传给 TaktTreeTable，用于展开树表行） */
const tableExpandedRowKeys = ref<(string | number)[]>([])
const loading = ref(false)
const dataSource = ref<Menu[]>([])
const fullTableTree = ref<any[]>([])
const selectedTreeKeys = ref<(string | number)[]>([])
const currentPage = ref(1)
const total = ref(0)
const selectedRow = ref<Menu | null>(null)
const selectedRows = ref<Menu[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Menu>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ menuName: string; menuCode: string; menuType?: number; menuStatus?: number }>({
  menuName: '',
  menuCode: '',
  menuType: undefined,
  menuStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

const menuTypeOptions = computed(() => [
  { label: t('identity.menu.menuType.dir'), value: 0 },
  { label: t('identity.menu.menuType.menu'), value: 1 }
])

/** 将 fullTableTree 转为左侧 a-tree 的 TreeDataItem（title, key, children） */
function mapFullTableTreeToTreeData(nodes: any[]): TreeDataItem[] {
  if (!nodes?.length) {
    return []
  }
  return nodes.map((n: any) => ({
      title: n.menuName ?? n.title ?? '',
      key: String(n.menuId ?? n.key ?? n.id ?? ''),
      children: n.children?.length ? mapFullTableTreeToTreeData(n.children) : undefined
    }))
}

/** 将平铺菜单列表转为树（parentId 为根标识） */
function flatToTree(list: Menu[], parentId: string | number = '0'): any[] {
  const pid = String(parentId)
  return list
    .filter(item => String(item.parentId) === pid)
    .map(item => ({
      ...item,
      key: String(item.menuId),
      children: flatToTree(list, item.menuId)
    }))
    .map(node => (node.children.length ? node : { ...node, children: undefined }))
}

/** 从树中取以某 key 为根的子树（返回单元素数组，便于作为表格根） */
function getSubtree(nodes: any[], key: string | number): any[] {
  const k = String(key)
  for (const node of nodes) {
    if (String(node.key ?? node.menuId ?? node.id) === k) {
      return [node]
    }
    if (node.children?.length) {
      const found = getSubtree(node.children, key)
      if (found.length) {
        return found
      }
    }
  }
  return []
}

/** 按关键字过滤树：保留 title 包含关键字的节点及其祖先、子孙 */
function filterTreeByKeyword(nodes: TreeDataItem[], keyword: string): TreeDataItem[] {
  const k = (keyword ?? '').trim().toLowerCase()
  if (!k) {
    return nodes
  }
  function filter(nodes: TreeDataItem[]): TreeDataItem[] {
    if (!nodes?.length) {
      return []
    }
    return nodes
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

/** 左侧树数据：由 fullTableTree 派生（与右侧树表同数据源），排除 type=2 */
const menuTreeDataFromTable = computed(() =>
  mapFullTableTreeToTreeData(fullTableTree.value)
)
const filteredMenuTreeData = computed(() =>
  filterTreeByKeyword(menuTreeDataFromTable.value, treeQueryKeyword.value)
)

/** 收集树中所有有子节点的 key（用于“全部展开”） */
function getAllParentKeys(nodes: TreeDataItem[]): (string | number)[] {
  const keys: (string | number)[] = []
  function walk(list: TreeDataItem[]) {
    if (!list?.length) {
      return
    }
    for (const node of list) {
      if (node.children?.length) {
        keys.push(node.key as string | number)
        walk(node.children)
      }
    }
  }
  walk(nodes)
  return keys
}

watch(
  [treeExpanded, filteredMenuTreeData],
  () => {
    treeExpandedKeys.value = treeExpanded.value
      ? getAllParentKeys(filteredMenuTreeData.value)
      : []
  },
  { immediate: true }
)

/** 收集树表中所有有子节点的行 key（用于右侧“全部展开”树表行） */
function getRowKeysWithChildren(
  rows: any[],
  getKey: (record: any) => string
): (string | number)[] {
  const keys: (string | number)[] = []
  function walk(list: any[]) {
    if (!list?.length) {
      return
    }
    for (const row of list) {
      if (row.children?.length) {
        keys.push(getKey(row))
        walk(row.children)
      }
    }
  }
  walk(rows)
  return keys
}

const getMenuId = (record: any): string =>
  record?.menuId != null ? String(record.menuId) : (record?.id != null ? String(record.id) : '')
const getMenuField = (record: any, field: string): any => record?.[field]

/** 右侧树表数据：仅当左侧选中一项时显示该节点子树，否则为空 */
const tableTreeData = computed(() => {
  const tree = fullTableTree.value
  if (!tree?.length) {
    return []
  }
  const keys = selectedTreeKeys.value
  if (keys.length !== 1) {
    return []
  }
  const sub = getSubtree(tree, keys[0])
  return sub.length ? sub : []
})

watch(
  [tableExpanded, tableTreeData],
  () => {
    tableExpandedRowKeys.value = tableExpanded.value
      ? getRowKeysWithChildren(tableTreeData.value, getMenuId)
      : []
  },
  { immediate: true }
)

const handleTreeSelect = (selectedKeys: (string | number)[]) => {
  selectedTreeKeys.value = selectedKeys
}

/** 从树结构中查找节点 key 的父级 key 与在同级中的序号（用于 parentId / orderNum） */
function findParentAndOrderNum(
  tree: any[],
  targetKey: string | number,
  parentKey: string = '0'
): { parentId: string; orderNum: number } | null {
  const keyStr = String(targetKey)
  for (let i = 0; i < tree.length; i++) {
    const node = tree[i]
    const k = String(node?.key ?? node?.menuId ?? '')
    if (k === keyStr) {
      return { parentId: parentKey, orderNum: i }
    }
    const children = node?.children ?? []
    if (children.length) {
      const found = findParentAndOrderNum(children, targetKey, k)
      if (found) {
        return found
      }
    }
  }
  return null
}

const handleMenuTreeDrop = async (payload: TreeDropPayload) => {
  const { newTreeData, dragKey } = payload
  const pos = findParentAndOrderNum(newTreeData, dragKey)
  if (!pos) {
    return
  }
  try {
    loading.value = true
    await update(String(dragKey), { parentId: pos.parentId, orderNum: pos.orderNum } as Partial<MenuUpdate>)
    message.success(t('identity.menu.msg.orderUpdated'))
    loadData()
  } catch (error: any) {
    message.error(error?.message ?? t('common.msg.operateFail', { action: t('common.action.operation') }))
    loadData()
  } finally {
    loading.value = false
  }
}

const handleTreeQuerySearch = () => {
  loadData()
}

onMounted(() => {
  loadData()
})

function getMenuTypeLabel(v: number | undefined): string {
  if (v === 0) {
    return t('identity.menu.menuType.dir')
  }
  if (v === 1) {
    return t('identity.menu.menuType.menu')
  }
  if (v === 2) {
    return t('identity.menu.menuType.button')
  }
  return '-'
}
function getMenuTypeColor(v: number | undefined): string {
  if (v === 0) {
    return 'blue'
  }
  if (v === 1) {
    return 'green'
  }
  if (v === 2) {
    return 'orange'
  }
  return 'default'
}

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'menuId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getMenuField(record, 'menuId') ?? getMenuField(record, 'id') ?? ''
  },
  {
    title: t('entity.menu.name'),
    dataIndex: 'menuName',
    key: 'menuName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.menu.code'),
    dataIndex: 'menuCode',
    key: 'menuCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.menu.parentid'),
    dataIndex: 'parentId',
    key: 'parentId',
    width: 90,
    ellipsis: true
  },
  {
    title: t('entity.menu.path'),
    dataIndex: 'path',
    key: 'path',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.menu.component'),
    dataIndex: 'component',
    key: 'component',
    width: 160,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.menu.icon'),
    dataIndex: 'menuIcon',
    key: 'menuIcon',
    width: 90,
    ellipsis: true
  },
  {
    title: t('entity.menu.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('entity.menu.type'),
    dataIndex: 'menuType',
    key: 'menuType',
    width: 80
  },
  {
    title: t('entity.menu.permission'),
    dataIndex: 'permission',
    key: 'permission',
    width: 140,
    ellipsis: true
  },
  {
    title: t('entity.menu.isVisible'),
    dataIndex: 'isVisible',
    key: 'isVisible',
    width: 70
  },
  {
    title: t('entity.menu.status'),
    dataIndex: 'menuStatus',
    key: 'menuStatus',
    width: 80
  },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'identity:menu:update', onClick: (record: Menu) => handleEdit(record) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'identity:menu:delete', onClick: (record: Menu) => handleDeleteOne(record) }
    ]
  })
])

const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) {
    return columns.value as any
  }
  const getColumnKey = (col: any): string => (col.key || col.dataIndex || col.title) ? String(col.key || col.dataIndex || col.title) : ''
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Menu[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Menu, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getMenuId(selectedRow.value) === getMenuId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Menu[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const loadData = async () => {
  try {
    loading.value = true
    const params: any = {
      PageIndex: 1,
      PageSize: 9999
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.menuName) params.MenuName = advancedQueryForm.value.menuName
    if (advancedQueryForm.value.menuCode) params.MenuCode = advancedQueryForm.value.menuCode
    if (advancedQueryForm.value.menuType !== undefined) params.MenuType = advancedQueryForm.value.menuType
    if (advancedQueryForm.value.menuStatus !== undefined) params.MenuStatus = advancedQueryForm.value.menuStatus

    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
    fullTableTree.value = flatToTree(items)
  } catch (error: any) {
    logger.error('[Menu] 加载数据失败:', error)
    message.error(error?.message || t('common.msg.loadFail'))
    dataSource.value = []
    fullTableTree.value = []
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
  advancedQueryForm.value = { menuName: '', menuCode: '', menuType: undefined, menuStatus: undefined }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Menu] 排序:', sorter.field, sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.menu._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Menu) => {
  formTitle.value = t('common.button.edit') + t('entity.menu._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.menu._self') }))
}

const handleDeleteOne = (record: Menu) => {
  const name = getMenuField(record, 'menuName') || getMenuId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.menu._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(getMenuId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.menu._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.menu._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.menu._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.menu._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(record => remove(getMenuId(record))))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.menu._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.menu._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleFormSubmit = async () => {
  try {
    if (!formRef.value) {
      return
    }
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.menuId) {
      await update(formData.value.menuId, { ...formValues, menuId: formData.value.menuId })
      message.success(t('common.msg.updateSuccess', { target: t('entity.menu._self') }))
    } else {
      await create(formValues)
      message.success(t('common.msg.createSuccess', { target: t('entity.menu._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: any) {
    if (error?.errorFields) {
      return
    }
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

const handleImport = () => { importVisible.value = true }
const handleDownloadTemplate = async (sheetName?: string, fileName?: string): Promise<Blob> => {
  return await getTemplate(sheetName, fileName)
}
const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importMenus(file, sheetName)
}
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}
const handleImportCancel = () => { importVisible.value = false }

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: any = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.menuName) queryParams.MenuName = advancedQueryForm.value.menuName
    if (advancedQueryForm.value.menuCode) queryParams.MenuCode = advancedQueryForm.value.menuCode
    if (advancedQueryForm.value.menuType !== undefined) queryParams.MenuType = advancedQueryForm.value.menuType
    if (advancedQueryForm.value.menuStatus !== undefined) queryParams.MenuStatus = advancedQueryForm.value.menuStatus
    const blob = await exportMenus(queryParams, undefined, t('entity.menu._self') + t('common.action.exportDataSuffix'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.menu._self') + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.menu._self') }))
  } catch (error: any) {
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.menu._self') }))
  } finally {
    loading.value = false
  }
}

const handleAdvancedQuery = () => { advancedQueryVisible.value = true }
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { menuName: '', menuCode: '', menuType: undefined, menuStatus: undefined }
}

const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }
defineExpose({ tableRef })

const handleRefresh = () => { loadData() }
</script>

<style scoped lang="less">
/* 边距由子组件（takt-tree-left-* / takt-tree-right-*）统一设置，本视图不重复设置 */
.identity-menu {
  padding: 0;
  display: flex;
  flex-direction: column;
  min-height: 0;
}

.menu-query-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}

.menu-toolbar-row {
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 100%;
  flex-wrap: nowrap;
  min-width: 0;
}

.menu-tree-table-wrap {
  flex: 1;
  min-height: 400px;
  display: flex;
  flex-direction: row;
  min-width: 0;
}
</style>
