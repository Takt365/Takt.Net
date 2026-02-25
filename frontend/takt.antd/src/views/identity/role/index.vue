<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/identity/role -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：角色管理页面，包含角色列表、查询、新增、编辑、删除、导入、导出等 -->
<!-- ======================================== -->

<template>
  <div class="identity-role">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.role.name') + t('common.action.or') + t('entity.role.code') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="identity:role:create"
      update-permission="identity:role:update"
      delete-permission="identity:role:delete"
      import-permission="identity:role:import"
      template-permission="identity:role:template"
      export-permission="identity:role:export"
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
      :row-key="getRoleId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <!-- 自定义列渲染 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'roleStatus'">
          <TaktDictTag
            :value="getRoleField(record, 'roleStatus')"
            dict-type="sys_normal_disable"
          />
        </template>
        <template v-else-if="column.key === 'dataScope'">
          {{ getDataScopeLabel(getRoleField(record, 'dataScope')) }}
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
      <RoleForm
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
      <a-form-item :label="t('entity.role.name')">
        <a-input v-model:value="advancedQueryForm.roleName" />
      </a-form-item>
      <a-form-item :label="t('entity.role.code')">
        <a-input v-model:value="advancedQueryForm.roleCode" />
      </a-form-item>
      <a-form-item :label="t('entity.role.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.roleStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_normal_disable"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.role.status') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.role._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.role._self') })"
        :template-file-name="t('common.action.import.sheetNameTemplate', { entity: t('entity.role._self') })"
        template-permission="identity:role:template"
        import-permission="identity:role:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.role._self') })"
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
import RoleForm from './components/role-form.vue'
import { getList, create, update, remove, getTemplate, importRoles, exportRoles } from '@/api/identity/role'
import type { Role } from '@/types/identity/role'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

const { t } = useI18n()

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Role[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Role | null>(null)
const selectedRows = ref<Role[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Role>>({})
const formLoading = ref(false)
const formRef = ref()
const tableRef = ref()
defineExpose({ tableRef })
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{ roleName: string; roleCode: string; roleStatus?: number }>({
  roleName: '',
  roleCode: '',
  roleStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

const getRoleId = (record: any): string => record?.roleId != null ? String(record.roleId) : (record?.id != null ? String(record.id) : '')
const getRoleField = (record: any, field: string): any => record?.[field]

function getDataScopeLabel(v: number | undefined): string {
  const map: Record<number, string> = {
    0: t('identity.role.dataScope.all'),
    1: t('identity.role.dataScope.dept'),
    2: t('identity.role.dataScope.deptAndBelow'),
    3: t('identity.role.dataScope.self'),
    4: t('identity.role.dataScope.custom')
  }
  return v !== undefined && v !== null ? (map[v] ?? '-') : '-'
}

const columns = computed<TableColumnsType>(() => [
  {
    title: 'ID',
    dataIndex: 'roleId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: any }) => getRoleField(record, 'roleId') ?? getRoleField(record, 'id') ?? ''
  },
  {
    title: t('entity.role.name'),
    dataIndex: 'roleName',
    key: 'roleName',
    width: 140,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.role.code'),
    dataIndex: 'roleCode',
    key: 'roleCode',
    width: 120,
    resizable: true,
    ellipsis: true
  },
  {
    title: t('entity.role.ordernum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 90
  },
  {
    title: t('entity.role.datascope'),
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 120
  },
  {
    title: t('entity.role.customscope'),
    dataIndex: 'customScope',
    key: 'customScope',
    width: 140,
    ellipsis: true
  },
  {
    title: t('entity.role.status'),
    dataIndex: 'roleStatus',
    key: 'roleStatus',
    width: 90
  },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'identity:role:update', onClick: (record: Role) => handleEdit(record) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'identity:role:delete', onClick: (record: Role) => handleDeleteOne(record) }
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
  onChange: (keys: (string | number)[], rows: Role[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Role, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getRoleId(selectedRow.value) === getRoleId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Role[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

const onClickRow = (record: Role) => ({
  onClick: () => {
    const key = getRoleId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(getRoleId(item)))
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
    if (advancedQueryForm.value.roleName) params.RoleName = advancedQueryForm.value.roleName
    if (advancedQueryForm.value.roleCode) params.RoleCode = advancedQueryForm.value.roleCode
    if (advancedQueryForm.value.roleStatus !== undefined) params.RoleStatus = advancedQueryForm.value.roleStatus

    const response = await getList(params)
    const responseAny = response as any
    const items = response?.data ?? responseAny?.Data ?? []
    const totalCount = response?.total ?? responseAny?.Total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Role] 加载数据失败:', error)
    message.error(error?.message || t('common.msg.loadFail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

const handleSearch = () => { currentPage.value = 1; loadData() }
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { roleName: '', roleCode: '', roleStatus: undefined }
  currentPage.value = 1
  loadData()
}

const handleTableChange = (_pagination: any, _filters: any, sorter: any) => {
  if (sorter?.order) logger.debug('[Role] 排序:', sorter.field, sorter.order)
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
  formTitle.value = t('common.button.create') + t('entity.role._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: Role) => {
  formTitle.value = t('common.button.edit') + t('entity.role._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.role._self') }))
}

const handleDeleteOne = (record: Role) => {
  const name = getRoleField(record, 'roleName') || getRoleId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.role._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await remove(getRoleId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.role._self') }))
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.role._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.role._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.role._self') }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(record => remove(getRoleId(record))))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.role._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: any) {
        message.error(error?.message || t('common.msg.deleteFail', { target: t('entity.role._self') }))
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
    if (formData.value?.roleId) {
      await update(formData.value.roleId, { ...formValues, roleId: formData.value.roleId })
      message.success(t('common.msg.updateSuccess', { target: t('entity.role._self') }))
    } else {
    await create(formValues)
    message.success(t('common.msg.createSuccess', { target: t('entity.role._self') }))
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
  const res = await getTemplate(sheetName, fileName)
  return (res as any)?.data ?? res
}

const handleImportFile = async (file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importRoles(file, sheetName)
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
    if (advancedQueryForm.value.roleName) queryParams.RoleName = advancedQueryForm.value.roleName
    if (advancedQueryForm.value.roleCode) queryParams.RoleCode = advancedQueryForm.value.roleCode
    if (advancedQueryForm.value.roleStatus !== undefined) queryParams.RoleStatus = advancedQueryForm.value.roleStatus
    const blob = await exportRoles(queryParams, undefined, t('entity.role._self') + t('common.action.exportDataSuffix'))
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.role._self') + t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.role._self') }))
  } catch (error: any) {
    message.error(error?.message || t('common.msg.exportFail', { target: t('entity.role._self') }))
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
  advancedQueryForm.value = { roleName: '', roleCode: '', roleStatus: undefined }
}

const handleColumnSetting = () => { columnSettingVisible.value = true }
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map(k => String(k))
}
const handleColumnSettingReset = () => { visibleColumnKeys.value = [] }

const handleRefresh = () => { loadData() }
</script>

<style scoped lang="less">
.identity-role {
  padding: 16px;
}
</style>
