<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)  -->
<!-- 命名空间：@/views/routine/setting -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：设置管理页面，包含设置列表、查询、新增、编辑、删除、状态、导入导出 -->
<!-- ======================================== -->

<template>
  <div class="routine-setting">
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="
        t('common.form.placeholder.search', {
          keyword:
            t('routine.setting.page.settingkey') +
            t('common.action.or') +
            t('routine.setting.page.settingname')
        })
      "
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <TaktToolsBar
      create-permission="routine:tasks:setting:create"
      update-permission="routine:tasks:setting:update"
      delete-permission="routine:tasks:setting:delete"
      export-permission="routine:tasks:setting:export"
      import-permission="routine:tasks:setting:import"
      :show-create="true"
      :show-update="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :show-advanced-query="true"
      :show-column-setting="true"
      :show-fullscreen="true"
      :show-refresh="true"
      :update-disabled="updateDisabled"
      :delete-disabled="deleteDisabled"
      :create-loading="loading"
      :update-loading="loading"
      :delete-loading="loading"
      :export-loading="loading"
      :refresh-loading="loading"
      @create="handleCreate"
      @update="handleUpdate"
      @delete="handleDelete"
      @export="handleExport"
      @import="handleImport"
      @advanced-query="handleAdvancedQuery"
      @column-setting="handleColumnSetting"
      @refresh="handleRefresh"
    />

    <TaktSingleTable
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
            @change="(checked: unknown) => handleStatusChange(record, Boolean(checked))"
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
      <SettingForm
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
      <a-form-item :label="t('routine.setting.page.settingkey')">
        <a-input
          v-model:value="advancedQueryForm.settingKey"
          :placeholder="t('common.form.placeholder.required', { field: t('routine.setting.page.settingkey') })"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('routine.setting.page.settinggroup')">
        <TaktSelect
          v-model:value="advancedQueryForm.settingGroup"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_setting_group"
          :placeholder="t('common.form.placeholder.select', { field: t('routine.setting.page.settinggroup') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
      <a-form-item :label="t('routine.setting.page.settingstatus')">
        <TaktSelect
          v-model:value="advancedQueryForm.settingStatus"
          api-url="/api/TaktDictDatas/options?dictTypeCode=sys_status"
          :placeholder="t('common.form.placeholder.select', { field: t('routine.setting.page.settingstatus') })"
          allow-clear
          :field-names="{ label: 'dictLabel', value: 'extLabel' }"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <TaktColumnDrawer
      v-model:open="columnSettingVisible"
      :columns="columns"
      :checked-keys="visibleColumnKeys"
      :id-column-key="'id'"
      :action-column-key="'action'"
      @update:checked-keys="handleColumnKeysChange"
      @reset="handleColumnSettingReset"
    />

    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('routine.setting.page._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="settingExcelNames.sheet"
        :template-file-name="settingExcelNames.fileBase"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('routine.setting.page._self') })"
        :upload-text="t('common.action.import.uploadtext')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import { taktExcelEntityNames } from '@/utils/naming'
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
} from '@/api/routine/tasks/setting'
import type {
  Settings,
  SettingsQuery,
  SettingsCreate,
  SettingsUpdate
} from '@/types/routine/tasks/setting/settings'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
const { t } = useI18n()

function pickErrorMessage(err: unknown, fallback: string): string {
  if (err !== null && typeof err === 'object' && 'message' in err) {
    const m = (err as { message?: unknown }).message
    if (typeof m === 'string' && m.length > 0) {
      return m
    }
  }
  return fallback
}

const settingExcelNames = taktExcelEntityNames('TaktSettings')

const getSettingField = (record: unknown, field: string): unknown =>
  (record as Record<string, unknown>)?.[field]

const getSettingId = (record: unknown): string => String(getSettingField(record, 'settingId') ?? '')

function coerceAdvancedSettingStatus(value: string | number | undefined): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

const queryKeyword = ref('')
const loading = ref(false)
const dataSource = ref<Settings[]>([])
const currentPage = ref(1)
const pageSize = ref(20)
const total = ref(0)
const selectedRow = ref<Settings | null>(null)
const selectedRows = ref<Settings[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formData = ref<Partial<Settings>>({})
const formLoading = ref(false)
const formRef = ref()
const advancedQueryVisible = ref(false)
const advancedQueryForm = ref<{
  settingKey: string
  settingGroup: string | number
  settingStatus: string | number
}>({
  settingKey: '',
  settingGroup: '',
  settingStatus: ''
})
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])
const importVisible = ref(false)

const updateDisabled = computed(() => selectedRows.value.length !== 1)
const deleteDisabled = computed(() => selectedRows.value.length === 0)

const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.entity.id'),
    dataIndex: 'settingId',
    key: 'id',
    width: 80,
    resizable: true,
    ellipsis: true,
    fixed: 'left',
    customRender: ({ record }: { record: Settings }) => String(getSettingField(record, 'settingId') ?? '')
  },
  {
    title: t('routine.setting.columns.settingKey'),
    dataIndex: 'settingKey',
    key: 'settingKey',
    width: 160,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('routine.setting.columns.settingValue'),
    dataIndex: 'settingValue',
    key: 'settingValue',
    width: 180,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('routine.setting.columns.settingName'),
    dataIndex: 'settingName',
    key: 'settingName',
    width: 140,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('routine.setting.columns.settingGroup'),
    dataIndex: 'settingGroup',
    key: 'settingGroup',
    width: 100
  },
  {
    title: t('routine.setting.columns.isBuiltIn'),
    dataIndex: 'isBuiltIn',
    key: 'isBuiltIn',
    width: 90
  },
  {
    title: t('routine.setting.columns.isEncrypted'),
    dataIndex: 'isEncrypted',
    key: 'isEncrypted',
    width: 90
  },
  {
    title: t('routine.setting.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('routine.setting.columns.settingStatus'),
    dataIndex: 'settingStatus',
    key: 'settingStatus',
    width: 100
  },
  {
    title: t('common.entity.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 160,
    ellipsis: true
  },
  CreateActionColumn<Settings>({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'routine:tasks:setting:update',
        onClick: (r: Settings) => handleEdit(r)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'routine:tasks:setting:delete',
        onClick: (r: Settings) => handleDeleteOne(r)
      }
    ]
  })
])

const mergedColumns = computed((): TableColumnsType => {
  return mergeDefaultColumns(columns.value as TableColumnsType, t, true) as TableColumnsType
})

const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  const getColumnKey = (col: unknown): string => {
    if (!col || typeof col !== 'object') return ''
    const column = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
    const key = column.key ?? column.dataIndex ?? column.title
    return key != null && String(key) !== '' ? String(key) : ''
  }
  return merged.filter((col) => {
    const colKey = getColumnKey(col)
    return colKey.length > 0 && keysSet.has(colKey)
  }) as TableColumnsType
})

const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Settings[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: Settings, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value?.settingId === record?.settingId) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, rows: Settings[]) => {
    selectedRow.value = selected && rows.length === 1 ? (rows[0] ?? null) : null
  }
}))

const onClickRow = (record: Settings) => ({
  onClick: () => {
    const key = getSettingId(record)
    const idx = selectedRowKeys.value.indexOf(key)
    if (idx > -1) selectedRowKeys.value.splice(idx, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item: Settings) => selectedRowKeys.value.includes(getSettingId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    rowSelection.value.onChange?.(selectedRowKeys.value, selectedRows.value)
  }
})

async function loadData() {
  try {
    loading.value = true
    const params: SettingsQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.settingKey) params.settingKey = advancedQueryForm.value.settingKey
    if (advancedQueryForm.value.settingGroup !== undefined && advancedQueryForm.value.settingGroup !== '') {
      params.settingGroup = String(advancedQueryForm.value.settingGroup)
    }
    const advStatus = coerceAdvancedSettingStatus(advancedQueryForm.value.settingStatus)
    if (advStatus !== undefined) params.settingStatus = advStatus

    const response = await getSettingList(params)
    dataSource.value = response?.data ?? []
    total.value = response?.total ?? 0
  } catch (error: unknown) {
    logger.error('[Setting] 加载失败:', error)
    message.error(pickErrorMessage(error, t('common.msg.loadfail')))
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
  advancedQueryForm.value = { settingKey: '', settingGroup: '', settingStatus: '' }
  currentPage.value = 1
  loadData()
}

function handleTableChange(
  _pagination: unknown,
  _filters: unknown,
  sorter: { order?: string; field?: unknown } | Array<{ order?: string; field?: unknown }>
) {
  const sorterInfo = Array.isArray(sorter) ? sorter[0] : sorter
  if (sorterInfo?.order) logger.debug('[Setting] 排序:', sorterInfo.field, sorterInfo.order)
}
function handleResizeColumn(w: number, col: Record<string, unknown>) {
  const column = columns.value.find((c) => {
    const colKey = col['key'] ?? col['dataIndex'] ?? col['title']
    const columnItem = c as { key?: unknown; dataIndex?: unknown; title?: unknown }
    const cKey = columnItem.key ?? columnItem.dataIndex ?? columnItem.title
    return colKey != null && cKey != null && String(colKey) === String(cKey)
  }) as { width?: number } | undefined
  if (column) column.width = w
}

function handlePaginationChange(page: number, size: number) {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

function handlePaginationSizeChange(_current: number, size: number) {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

function handleCreate() {
  formTitle.value = t('common.button.create') + t('routine.setting.page._self')
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Settings) {
  formTitle.value = t('common.button.edit') + t('routine.setting.page._self')
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else {
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: t('routine.setting.page._self') })
    )
  }
}

async function handleStatusChange(record: Settings, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.settingStatus
  const idx = dataSource.value.findIndex((s: { settingId?: string | number }) => s.settingId === record.settingId)
  const row = idx !== -1 ? dataSource.value[idx] : undefined
  if (row) {
    row.settingStatus = newStatus
  }
  try {
    await updateSettingStatus({ settingId: record.settingId, settingStatus: newStatus })
    message.success(t('common.msg.updatesuccess'))
  } catch (e: unknown) {
    if (row) {
      row.settingStatus = oldStatus
    }
    message.error(pickErrorMessage(e, t('common.msg.operatefail')))
  }
}

function handleDeleteOne(record: Settings) {
  const name = record.settingKey || t('common.action.thistarget', { target: t('routine.setting.page._self') })
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('routine.setting.page._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteSetting(record.settingId)
        message.success(t('common.msg.deletesuccess'))
        loadData()
      } catch (e: unknown) {
        message.error(pickErrorMessage(e, t('common.msg.deletefail')))
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('routine.setting.page._self') })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', {
      entity: t('routine.setting.page._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(s => deleteSetting(s.settingId)))
        message.success(t('common.msg.deletesuccess'))
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: unknown) {
        message.error(pickErrorMessage(e, t('common.msg.deletefail')))
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
  advancedQueryForm.value = { settingKey: '', settingGroup: '', settingStatus: '' }
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
    const query: SettingsQuery = {
      pageIndex: 1,
      pageSize: 99999
    }
    if (queryKeyword.value) query.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.settingKey) query.settingKey = advancedQueryForm.value.settingKey
    if (advancedQueryForm.value.settingGroup !== undefined && advancedQueryForm.value.settingGroup !== '') {
      query.settingGroup = String(advancedQueryForm.value.settingGroup)
    }
    const advStatus = coerceAdvancedSettingStatus(advancedQueryForm.value.settingStatus)
    if (advStatus !== undefined) query.settingStatus = advStatus
    const blob = await exportSettings(query)
    const ts = new Date()
    const pad = (n: number) => String(n).padStart(2, '0')
    const name = `${settingExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess'))
  } catch (e: unknown) {
    message.error(pickErrorMessage(e, t('common.msg.exportfail')))
  } finally {
    loading.value = false
  }
}

const handleImport = () => {
  importVisible.value = true
}

const handleImportCancel = () => {
  importVisible.value = false
}

const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getSettingTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  const res = await importSettings(file, sheetName)
  return {
    success: res.success,
    fail: res.fail,
    errors: res.errors ?? []
  }
}

const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if ('settingId' in values && values.settingId) {
      const payload: SettingsUpdate = {
        settingId: values.settingId,
        settingKey: values.settingKey,
        settingValue: values.settingValue,
        settingName: values.settingName,
        settingGroup: values.settingGroup,
        isBuiltIn: values.isBuiltIn,
        isEncrypted: values.isEncrypted,
        orderNum: values.orderNum,
        remark: values.remark
      }
      await updateSetting(values.settingId, payload)
      message.success(t('common.msg.updatesuccess'))
    } else {
      const createPayload: SettingsCreate = {
        settingKey: values.settingKey,
        settingValue: values.settingValue,
        settingName: values.settingName,
        settingGroup: values.settingGroup,
        isBuiltIn: values.isBuiltIn,
        isEncrypted: values.isEncrypted,
        orderNum: values.orderNum,
        remark: values.remark
      }
      await createSetting(createPayload)
      message.success(t('common.msg.createsuccess'))
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: unknown) {
    if (e !== null && typeof e === 'object' && 'errorFields' in e) {
      return
    }
    message.error(pickErrorMessage(e, t('common.msg.operatefail')))
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
