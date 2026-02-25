<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/setting -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：设置管理页面，包含设置列表、查询、新增、编辑、删除、状态、导入导出 -->
<!-- ======================================== -->

<template>
  <div class="routine-setting">
    <!-- 查询栏 -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.settings.key') + t('common.action.or') + t('entity.settings.name') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <TaktToolsBar
      create-permission="routine:setting:create"
      update-permission="routine:setting:update"
      delete-permission="routine:setting:delete"
      export-permission="routine:setting:export"
      import-permission="routine:setting:import"
      template-permission="routine:setting:template"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
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
      :export-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @export="handleExport"
      @import="() => (importVisible = true)"
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
      :row-key="getSettingId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'settingStatus'">
          <a-switch
            :checked="record.settingStatus === 0"
            :checked-children="t('common.button.enable')"
            :un-checked-children="t('common.button.disable')"
            @change="(checked: any) => handleStatusChange(record, !!checked)"
          />
        </template>
        <template v-else-if="column.key === 'isBuiltIn'">
          {{ record.isBuiltIn === 0 ? t('common.button.yes') : t('common.button.no') }}
        </template>
        <template v-else-if="column.key === 'isEncrypted'">
          {{ record.isEncrypted === 0 ? t('common.button.yes') : t('common.button.no') }}
        </template>
      </template>
    </TaktSingleTable>

    <!-- 分页 -->
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
      <SettingForm
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
      <a-form-item :label="t('entity.settings.key')">
        <a-input
          v-model:value="advancedQueryForm.settingKey"
          :placeholder="t('common.form.placeholder.required', { field: t('entity.settings.key') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.settings.group')">
        <TaktSelect
          v-model:value="advancedQueryForm.settingGroup"
          dict-type="sys_setting_group"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.settings.group') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('entity.settings.status')">
        <TaktSelect
          v-model:value="advancedQueryForm.settingStatus"
          dict-type="sys_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.settings.status') })"
          allow-clear
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'settingId'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />

    <!-- 导入对话框 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.settings._self')"
      :width="480"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="importVisible = false"
    >
      <a-space direction="vertical" style="width: 100%">
        <a-button type="link" @click="handleDownloadTemplate">
          <template #icon><DownloadOutlined /></template>
          {{ t('common.action.import.templateText', { entity: t('entity.settings._self') }) }}
        </a-button>
        <a-upload
          :before-upload="handleImportFile"
          :show-upload-list="false"
          accept=".xlsx,.xls"
        >
          <a-button type="primary">
            <template #icon><UploadOutlined /></template>
            {{ t('common.button.import') }}
          </a-button>
        </a-upload>
      </a-space>
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { DownloadOutlined, UploadOutlined } from '@ant-design/icons-vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import SettingForm from './components/setting-form.vue'
import {
  getSettingList,
  createSetting,
  updateSetting,
  deleteSetting,
  updateSettingStatus,
  getSettingTemplate,
  importSettings,
  exportSettings
} from '@/api/routine/setting'
import type { Setting, SettingQuery } from '@/types/routine/setting'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'

const { t } = useI18n()
const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Setting[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Setting | null>(null)
const selectedRows = ref<Setting[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Setting>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  settingKey: string
  settingGroup?: string
  settingStatus?: number
}>({
  settingKey: '',
  settingGroup: undefined,
  settingStatus: undefined
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])
const importVisible = ref(false)

// 表格列配置（computed 以便列标题与操作列 label 随 locale 更新）
const columns = computed<TableColumnsType>(() => [
  { title: 'ID', dataIndex: 'settingId', key: 'id', width: 120, fixed: 'left', resizable: true, ellipsis: true },
  { title: t('entity.settings.key'), dataIndex: 'settingKey', key: 'settingKey', width: 160, ellipsis: true, resizable: true },
  { title: t('entity.settings.value'), dataIndex: 'settingValue', key: 'settingValue', width: 180, ellipsis: true, resizable: true },
  { title: t('entity.settings.name'), dataIndex: 'settingName', key: 'settingName', width: 140, ellipsis: true, resizable: true },
  { title: t('entity.settings.group'), dataIndex: 'settingGroup', key: 'settingGroup', width: 100 },
  { title: t('entity.settings.isbuiltin'), dataIndex: 'isBuiltIn', key: 'isBuiltIn', width: 90 },
  { title: t('entity.settings.isencrypted'), dataIndex: 'isEncrypted', key: 'isEncrypted', width: 90 },
  { title: t('entity.settings.ordernum'), dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: t('entity.settings.status'), dataIndex: 'settingStatus', key: 'settingStatus', width: 100 },
  CreateActionColumn({
    actions: [
      { key: 'update', label: t('common.button.edit'), shape: 'plain', icon: RiEditLine, permission: 'routine:setting:update', onClick: (r: Setting) => handleEdit(r) },
      { key: 'delete', label: t('common.button.delete'), shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:setting:delete', onClick: (r: Setting) => handleDeleteOne(r) }
    ]
  })
])

// 辅助函数：获取设置ID
const getSettingId = (record: any): string => String(record?.settingId ?? '')

// 合并列配置（包含审计等默认列）
const mergedColumns = computed((): any => mergeDefaultColumns(columns.value as any, t, true))
// 根据可见列过滤显示的列，保持原始列顺序
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) {
    return columns.value
  }
  const getColumnKey = (col: any): string => {
    const key = col.key || col.dataIndex || col.title
    return key ? String(key) : ''
  }
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = getColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Setting[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: Setting, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.settingId === record?.settingId) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, rows: Setting[]) => {
    selectedRow.value = selected && rows.length === 1 ? rows[0] : null
  }
}))

const onClickRow = (record: Setting) => ({
  onClick: () => {
    const key = record.settingId || ''
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter(item => selectedRowKeys.value.includes(item.settingId || ''))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: SettingQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.keyWords = queryKeyword.value
    if (advancedQueryForm.value.settingKey) params.settingKey = advancedQueryForm.value.settingKey
    if (advancedQueryForm.value.settingGroup) params.settingGroup = advancedQueryForm.value.settingGroup
    if (advancedQueryForm.value.settingStatus !== undefined) params.settingStatus = advancedQueryForm.value.settingStatus

    const response = await getSettingList(params) as any
    const items = response?.data ?? []
    const totalCount = response?.total ?? 0
    dataSource.value = items
    total.value = totalCount
  } catch (error: any) {
    logger.error('[Setting] 加载失败:', error)
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
  advancedQueryForm.value = { settingKey: '', settingGroup: undefined, settingStatus: undefined }
  currentPage.value = 1
  loadData()
}

function handleTableChange(_pagination: any, _filters: any, _sorter: any) {}
function handleResizeColumn(_w: number, _col: any) {
  // 列宽由表格组件内部维护，与用户视图一致
}

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
  formTitle.value = t('common.button.create') + t('entity.settings._self')
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Setting) {
  formTitle.value = t('common.button.edit') + t('entity.settings._self')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnSelectToAction', { action: t('common.button.update'), entity: t('entity.settings._self') }))
}

async function handleStatusChange(record: Setting, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.settingStatus
  const idx = dataSource.value.findIndex(s => s.settingId === record.settingId)
  if (idx !== -1) dataSource.value[idx].settingStatus = newStatus
  try {
    await updateSettingStatus({ settingId: record.settingId, settingStatus: newStatus })
    message.success(checked ? t('common.button.enable') : t('common.button.disable'))
  } catch (e: any) {
    if (idx !== -1) dataSource.value[idx].settingStatus = oldStatus
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
  }
}

function handleDeleteOne(record: Setting) {
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.settings._self'), name: record.settingKey }),
    onOk: async () => {
      try {
        loading.value = true
        await deleteSetting(record.settingId)
        message.success(t('common.msg.deleteSuccess', { target: t('entity.settings._self') }))
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.settings._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.settings._self') }))
    return
  }
  Modal.confirm({
    title: t('common.confirm.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', { count: selectedRows.value.length, entity: t('entity.settings._self') }),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(s => deleteSetting(s.settingId)))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.settings._self') }))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || t('common.msg.deleteFail', { target: t('entity.settings._self') }))
      } finally {
        loading.value = false
      }
    }
  })
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
  advancedQueryForm.value = { settingKey: '', settingGroup: undefined, settingStatus: undefined }
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

function handleRefresh() {
  loadData()
}

async function handleExport() {
  try {
    loading.value = true
    const query: SettingQuery = {
      pageIndex: 1,
      pageSize: 99999,
      keyWords: queryKeyword.value || undefined,
      settingKey: advancedQueryForm.value.settingKey || undefined,
      settingGroup: advancedQueryForm.value.settingGroup !== undefined ? String(advancedQueryForm.value.settingGroup) : undefined,
      settingStatus: advancedQueryForm.value.settingStatus
    }
    const blob = await exportSettings(query)
    const ts = new Date()
    const pad = (n: number) => String(n).padStart(2, '0')
    const name = `${t('entity.settings._self')}${t('common.action.exportDataSuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.settings._self') }))
  } catch (e: any) {
    message.error(e?.message || t('common.msg.exportFail', { target: t('entity.settings._self') }))
  } finally {
    loading.value = false
  }
}

function handleDownloadTemplate() {
  const templateName = t('common.action.import.sheetNameTemplate', { entity: t('entity.settings._self') }) + '.xlsx'
  getSettingTemplate(undefined, templateName).then(blob => {
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = templateName
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('common.msg.actionSuccess', { action: t('common.button.download') }))
  }).catch(() => message.error(t('common.msg.loadFail')))
}

function handleImportFile(file: File) {
  importSettings(file).then(res => {
    message.success(res.fail === 0 ? t('common.msg.actionSuccess', { action: t('common.button.import') }) : `导入成功 ${res.success} 条，失败 ${res.fail} 条${res.errors?.length ? '；' + res.errors.join('；') : ''}`)
    importVisible.value = false
    loadData()
  }).catch(e => {
    message.error(e?.message || t('common.msg.actionFail', { action: t('common.button.import') }))
  })
  return false
}

async function handleFormSubmit() {
  if (!formRef.value) {
    return
  }
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if ('settingId' in values && values.settingId) {
      await updateSetting(values.settingId, values)
      message.success(t('common.msg.updateSuccess', { target: t('entity.settings._self') }))
    } else {
      await createSetting(values)
      message.success(t('common.msg.createSuccess', { target: t('entity.settings._self') }))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) {
      return
    }
    message.error(e?.message || t('common.msg.operateFail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

function handleFormCancel() {
  formVisible.value = false
  formData.value = {}
}

onMounted(() => loadData())
</script>

<style scoped lang="less">
.routine-setting {
  padding: 16px;
}
</style>
