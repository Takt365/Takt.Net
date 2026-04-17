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
      placeholder="请输入设置键或设置名称"
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

    <TaktSingleTable
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="(record: any) => record.settingId || ''"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :pagination="false"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'settingStatus'">
          <a-switch
            :checked="record.settingStatus === 0"
            checked-children="启用"
            un-checked-children="禁用"
            @change="(checked: any) => handleStatusChange(record, !!checked)"
          />
        </template>
        <template v-else-if="column.key === 'isBuiltIn'">
          {{ record.isBuiltIn === 0 ? '是' : '否' }}
        </template>
        <template v-else-if="column.key === 'isEncrypted'">
          {{ record.isEncrypted === 0 ? '是' : '否' }}
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
      :width="560"
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
      <a-form-item label="设置键">
        <a-input
          v-model:value="advancedQueryForm.settingKey"
          placeholder="请输入设置键"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="设置分组">
        <TaktSelect
          v-model:value="advancedQueryForm.settingGroup"
          dict-type="sys_setting_group"
          placeholder="请选择设置分组"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="设置状态">
        <TaktSelect
          v-model:value="advancedQueryForm.settingStatus"
          dict-type="sys_status"
          placeholder="请选择设置状态"
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

    <!-- 导入：模板下载 + 上传 -->
    <TaktModal
      v-model:open="importVisible"
      title="导入设置"
      :width="480"
      :footer="null"
      @cancel="importVisible = false"
    >
      <a-space
        direction="vertical"
        style="width: 100%"
      >
        <a-button
          type="link"
          @click="handleDownloadTemplate"
        >
          <template #icon>
            <DownloadOutlined />
          </template>
          下载导入模板
        </a-button>
        <a-upload
          :before-upload="handleImportFile"
          :show-upload-list="false"
          accept=".xlsx,.xls"
        >
          <a-button type="primary">
            <template #icon>
              <UploadOutlined />
            </template>
            选择 Excel 文件并导入
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
} from '@/api/routine/tasks/setting'
import type { Setting, SettingQuery } from '@/types/routine/tasks/setting'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import { logger } from '@/utils/logger'
import { CreateActionColumn } from '@/components/business/takt-action-column'
import { resolveExportDownloadFileName } from '@/utils/export-download-name'

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
const formTitle = ref('新增设置')
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

const columns = ref<TableColumnsType>([
  { title: '设置ID', dataIndex: 'settingId', key: 'settingId', width: 120, fixed: 'left' },
  { title: '设置键', dataIndex: 'settingKey', key: 'settingKey', width: 160, ellipsis: true, resizable: true },
  { title: '设置值', dataIndex: 'settingValue', key: 'settingValue', width: 180, ellipsis: true, resizable: true },
  { title: '设置名称', dataIndex: 'settingName', key: 'settingName', width: 140, ellipsis: true, resizable: true },
  { title: '设置分组', dataIndex: 'settingGroup', key: 'settingGroup', width: 100 },
  { title: '是否内置', dataIndex: 'isBuiltIn', key: 'isBuiltIn', width: 90 },
  { title: '是否加密', dataIndex: 'isEncrypted', key: 'isEncrypted', width: 90 },
  { title: '排序号', dataIndex: 'orderNum', key: 'orderNum', width: 80 },
  { title: '设置状态', dataIndex: 'settingStatus', key: 'settingStatus', width: 100 },
  { title: '备注', dataIndex: 'remark', key: 'remark', width: 160, ellipsis: true },
  CreateActionColumn({
    actions: [
      { key: 'update', label: '编辑', shape: 'plain', icon: RiEditLine, permission: 'routine:tasks:setting:update', onClick: (r: Setting) => handleEdit(r) },
      { key: 'delete', label: '删除', shape: 'plain', icon: RiDeleteBinLine, permission: 'routine:tasks:setting:delete', onClick: (r: Setting) => handleDeleteOne(r) }
    ]
  })
])

const mergedColumns = computed(() => mergeDefaultColumns(columns.value as any, t, true))
const displayColumns = computed(() => {
  const keys = visibleColumnKeys.value || []
  const merged: any = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map(k => String(k)))
  return merged.filter((col: any) => {
    const colKey = col.key || col.dataIndex || col.title
    return colKey && keysSet.has(String(colKey))
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
    message.error(error?.message || '加载失败')
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
function handleResizeColumn(w: number, col: any) {
  const column = columns.value.find((c: any) => (c.key || c.dataIndex) === (col.key || col.dataIndex))
  if (column) (column as any).width = w
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
  formTitle.value = '新增设置'
  formData.value = {}
  formVisible.value = true
}

function handleEdit(record: Setting) {
  formTitle.value = '编辑设置'
  formData.value = { ...record }
  formVisible.value = true
}

function handleUpdate() {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning('请选择一条记录')
}

async function handleStatusChange(record: Setting, checked: boolean) {
  const newStatus = checked ? 0 : 1
  const oldStatus = record.settingStatus
  const idx = dataSource.value.findIndex(s => s.settingId === record.settingId)
  if (idx !== -1) dataSource.value[idx].settingStatus = newStatus
  try {
    await updateSettingStatus({ settingId: record.settingId, settingStatus: newStatus })
    message.success(checked ? '已启用' : '已禁用')
  } catch (e: any) {
    if (idx !== -1) dataSource.value[idx].settingStatus = oldStatus
    message.error(e?.message || '操作失败')
  }
}

function handleDeleteOne(record: Setting) {
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除设置 "${record.settingKey}" 吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await deleteSetting(record.settingId)
        message.success('删除成功')
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
      } finally {
        loading.value = false
      }
    }
  })
}

function handleDelete() {
  if (selectedRows.value.length === 0) {
    message.warning('请选择要删除的记录')
    return
  }
  Modal.confirm({
    title: '确认删除',
    content: `确定要删除选中的 ${selectedRows.value.length} 条设置吗？`,
    onOk: async () => {
      try {
        loading.value = true
        await Promise.all(selectedRows.value.map(s => deleteSetting(s.settingId)))
        message.success('删除成功')
        selectedRowKeys.value = []
        selectedRows.value = []
        selectedRow.value = null
        loadData()
      } catch (e: any) {
        message.error(e?.message || '删除失败')
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
    const name = `设置数据_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = name
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success('导出成功')
  } catch (e: any) {
    message.error(e?.message || '导出失败')
  } finally {
    loading.value = false
  }
}

function handleDownloadTemplate() {
  getSettingTemplate(undefined, '设置导入模板.xlsx')
    .then((meta) => {
      const d = new Date()
      const pad = (n: number) => String(n).padStart(2, '0')
      const fallbackBase = `设置导入模板_${d.getFullYear()}${pad(d.getMonth() + 1)}${pad(d.getDate())}${pad(d.getHours())}${pad(d.getMinutes())}${pad(d.getSeconds())}`
      const url = window.URL.createObjectURL(meta.blob)
      const link = document.createElement('a')
      link.href = url
      link.download = resolveExportDownloadFileName({
        contentDisposition: meta.contentDisposition,
        contentType: meta.contentType,
        fallbackBase
      })
      link.click()
      window.URL.revokeObjectURL(url)
      message.success('模板已下载')
    })
    .catch(() => message.error('下载模板失败'))
}

function handleImportFile(file: File) {
  importSettings(file).then(res => {
    message.success(`导入成功 ${res.success} 条，失败 ${res.fail} 条${res.errors?.length ? '；错误：' + res.errors.join('；') : ''}`)
    importVisible.value = false
    loadData()
  }).catch(e => {
    message.error(e?.message || '导入失败')
  })
  return false
}

async function handleFormSubmit() {
  if (!formRef.value) return
  try {
    await formRef.value.validate()
    const values = formRef.value.getValues()
    formLoading.value = true
    if ('settingId' in values && values.settingId) {
      await updateSetting(values.settingId, values)
      message.success('更新成功')
    } else {
      await createSetting(values)
      message.success('创建成功')
    }
    formVisible.value = false
    formData.value = {}
    loadData()
  } catch (e: any) {
    if (e?.errorFields) return
    message.error(e?.message || '保存失败')
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
