<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/attendance-device -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：考勤设备列表页。关键字占位与 entity.attendancedevice.keyword 检索（与 common.form.placeholder.search 拼接）；高级查询字段对应后端 TaktAttendanceDeviceQueryDto（前端 AttendanceDeviceQuery）；CRUD、导入导出、列设置；设备状态列与用户视图一致使用 `#bodyCell` + `TaktDictTag`（`hr_attendance_device_status`）。API @/api/human-resource/attendance-leave/attendance-device，权限与 TaktAttendanceDevicesController 一致。 -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-device">
    <!-- 顶部：关键字（entity.attendancedevice.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.attendancedevice.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktAttendanceDevicesController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:attendancedevice:create"
      update-permission="humanresource:attendanceleave:attendancedevice:update"
      delete-permission="humanresource:attendanceleave:attendancedevice:delete"
      import-permission="humanresource:attendanceleave:attendancedevice:import"
      template-permission="humanresource:attendanceleave:attendancedevice:template"
      export-permission="humanresource:attendanceleave:attendancedevice:export"
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

    <!-- 主表：deviceStatus 与用户页一致，bodyCell + TaktDictTag -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getDeviceId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'deviceStatus'">
          <TaktDictTag
            :value="getDeviceField(record, 'deviceStatus')"
            dict-type="hr_attendance_device_status"
          />
        </template>
      </template>
    </TaktSingleTable>

    <!-- 底部分页 -->
    <TaktPagination
      v-model:current="currentPage"
      v-model:page-size="pageSize"
      :total="total"
      @change="handlePaginationChange"
      @show-size-change="handlePaginationSizeChange"
    />

    <!-- 新增 / 编辑 -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <AttendanceDeviceForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询：字段与 TaktAttendanceDeviceQueryDto 一致（前端 AttendanceDeviceQuery） -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.attendancedevice.devicecode')">
        <a-input v-model:value="advancedQueryForm.deviceCode" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancedevice.devicename')">
        <a-input v-model:value="advancedQueryForm.deviceName" />
      </a-form-item>
      <a-form-item :label="t('entity.attendancedevice.manufacturer')">
        <TaktSelect
          v-model="advancedQueryForm.manufacturer"
          dict-type="hr_attendance_device_brand"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancedevice.manufacturer') })"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('entity.attendancedevice.devicestatus')">
        <TaktSelect
          v-model="advancedQueryForm.deviceStatus"
          dict-type="hr_attendance_device_status"
          :placeholder="t('common.form.placeholder.select', { field: t('entity.attendancedevice.devicestatus') })"
          allow-clear
          style="width: 100%"
        />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet / 文件名与后端 TaktNamingHelper.ResolveExcelImportExport 及实体 TaktAttendanceDevice 命名约定一致 -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.attendancedevice._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="deviceExcelNames.sheet"
        :template-file-name="deviceExcelNames.fileBase"
        template-permission="humanresource:attendanceleave:attendancedevice:template"
        import-permission="humanresource:attendanceleave:attendancedevice:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templateText', { entity: t('entity.attendancedevice._self') })"
        :upload-text="t('common.action.import.uploadText')"
        :hint="t('common.action.import.hint')"
        :max-size="10"
        :max-rows="1000"
        @success="handleImportSuccess"
      />
    </TaktModal>

    <!-- 列设置 -->
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
import { useI18n } from 'vue-i18n'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import TaktSelect from '@/components/business/takt-select/index.vue'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { logger } from '@/utils/logger'
import { taktExcelEntityNames } from '@/utils/naming'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'
import AttendanceDeviceForm from './components/attendance-device-form.vue'
import {
  getAttendanceDeviceList,
  createAttendanceDevice,
  updateAttendanceDevice,
  deleteAttendanceDeviceById,
  deleteAttendanceDeviceBatch,
  getAttendanceDeviceTemplate,
  importAttendanceDeviceData,
  exportAttendanceDeviceData
} from '@/api/human-resource/attendance-leave/attendance-device'
import type {
  AttendanceDevice,
  AttendanceDeviceCreate,
  AttendanceDeviceQuery,
  AttendanceDeviceUpdate
} from '@/types/human-resource/attendance-leave/attendance-device'
import type { TaktPagedResult } from '@/types/common'

const { t } = useI18n()

/**
 * 与后端 Excel 约定一致的工作表名与模板文件名前缀（实体 `TaktAttendanceDevice`）。
 */
const deviceExcelNames = taktExcelEntityNames('TaktAttendanceDevice')

/** 表格列单项类型 */
type DeviceTableColumn = TableColumnsType[number]

/** a-table `change` 排序参数最小形状 */
interface TableSorterLike {
  /** 排序列字段 */
  readonly field?: string | string[]
  /** 排序方向 */
  readonly order?: 'ascend' | 'descend' | null
}

/**
 * 从 `catch` 未知错误取可读消息。
 *
 * @param {unknown} err - 捕获值
 * @returns {string | undefined}
 */
function getErrorMessage(err: unknown): string | undefined {
  if (err instanceof Error) return err.message
  if (typeof err === 'object' && err !== null && 'message' in err) {
    const m = (err as { message?: unknown }).message
    return typeof m === 'string' ? m : undefined
  }
  return undefined
}

/**
 * 列配置稳定键（列设置、列宽匹配）。
 *
 * @param {DeviceTableColumn} col - 列定义
 * @returns {string}
 */
function getDeviceColumnKey(col: DeviceTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

/** 顶部查询关键字，写入 `AttendanceDeviceQuery.KeyWords` */
const queryKeyword = ref('')
/** 列表等异步 loading */
const loading = ref(false)
/** 当前页数据 */
const dataSource = ref<AttendanceDevice[]>([])
/** 当前页码 */
const currentPage = ref(1)
/** 每页条数 */
const pageSize = ref(20)
/** 总条数 */
const total = ref(0)
/** 工具栏编辑依赖的单行选中 */
const selectedRow = ref<AttendanceDevice | null>(null)
/** 多选行 */
const selectedRows = ref<AttendanceDevice[]>([])
/** 多选 row-key */
const selectedRowKeys = ref<(string | number)[]>([])
/** 表单弹窗 */
const formVisible = ref(false)
/** 弹窗标题 */
const formTitle = ref('')
/** 表单初始数据 */
const formData = ref<Partial<AttendanceDevice>>({})
/** 表单提交 loading */
const formLoading = ref(false)
/** 子表单实例类型 */
type DeviceFormExposed = InstanceType<typeof AttendanceDeviceForm>
const formRef = ref<DeviceFormExposed | null>(null)
const tableRef = ref()
/** 高级查询抽屉 */
const advancedQueryVisible = ref(false)
/** 高级查询模型 */
const advancedQueryForm = ref<{ deviceCode: string; deviceName: string; manufacturer?: string; deviceStatus?: string | number }>({
  deviceCode: '',
  deviceName: '',
  manufacturer: undefined,
  deviceStatus: undefined
})
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const visibleColumnKeys = ref<string[]>([])

onMounted(() => {
  loadData()
})

/**
 * 行主键（`row-key`、删除）。
 *
 * @param {AttendanceDevice} record - 行
 * @returns {string}
 */
const getDeviceId = (record: AttendanceDevice): string => {
  if (record?.deviceId != null && String(record.deviceId) !== '') return String(record.deviceId)
  const legacy = (record as unknown as Record<string, unknown>)['id']
  if (legacy != null && legacy !== '') return String(legacy)
  return ''
}

/**
 * 读取行字段（与用户页 `getUserField` 一致，供 `TaktDictTag` 的 `:value`）。
 *
 * @param {AttendanceDevice} record - 行
 * @param {string} field - 字段名
 * @returns {unknown}
 */
const getDeviceField = (record: AttendanceDevice, field: string): unknown =>
  (record as unknown as Record<string, unknown>)[field]

function coerceAdvancedDeviceStatus(value: string | number | undefined | null): number | undefined {
  if (value === undefined || value === null || value === '') return undefined
  if (typeof value === 'number' && Number.isFinite(value)) return value
  const n = Number(value)
  return Number.isFinite(n) ? n : undefined
}

/**
 * 列表主列。
 */
const columns = computed<TableColumnsType>(() => [
  {
    title: t('common.field.id'),
    dataIndex: 'deviceId',
    key: 'id',
    width: 88,
    fixed: 'left',
    ellipsis: true,
    resizable: true,
    customRender: ({ record }: { record: AttendanceDevice }) =>
      getDeviceField(record, 'deviceId') ?? getDeviceField(record, 'id') ?? ''
  },
  {
    title: t('entity.attendancedevice.devicecode'),
    dataIndex: 'deviceCode',
    key: 'deviceCode',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.devicename'),
    dataIndex: 'deviceName',
    key: 'deviceName',
    width: 140,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.manufacturer'),
    dataIndex: 'manufacturer',
    key: 'manufacturer',
    width: 110,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.ipaddress'),
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.port'),
    dataIndex: 'port',
    key: 'port',
    width: 72,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.devicemodel'),
    dataIndex: 'deviceModel',
    key: 'deviceModel',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.devicestatus'),
    dataIndex: 'deviceStatus',
    key: 'deviceStatus',
    width: 100,
    resizable: true
  },
  {
    title: t('entity.attendancedevice.lastpullat'),
    dataIndex: 'lastPullAt',
    key: 'lastPullAt',
    width: 168,
    ellipsis: true,
    resizable: true
  },
  CreateActionColumn({
    actions: [
      {
        key: 'update',
        label: t('common.button.edit'),
        shape: 'plain',
        icon: RiEditLine,
        permission: 'humanresource:attendanceleave:attendancedevice:update',
        onClick: (record: AttendanceDevice) => handleEdit(record)
      },
      {
        key: 'delete',
        label: t('common.button.delete'),
        shape: 'plain',
        icon: RiDeleteBinLine,
        permission: 'humanresource:attendanceleave:attendancedevice:delete',
        onClick: (record: AttendanceDevice) => handleDeleteOne(record)
      }
    ]
  })
])

/**
 * 合并审计等默认列。
 */
const mergedColumns = computed((): TableColumnsType => mergeDefaultColumns(columns.value, t, true))

/**
 * 按列设置过滤后的展示列。
 */
const displayColumns = computed((): TableColumnsType => {
  const keys = visibleColumnKeys.value || []
  const merged = mergedColumns.value || []
  if (keys.length === 0) return columns.value
  const keysSet = new Set(keys.map((k) => String(k)))
  return merged.filter((col: DeviceTableColumn) => {
    const colKey = getDeviceColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

/**
 * 行选择配置。
 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: AttendanceDevice[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? rows[0] : null
  },
  onSelect: (record: AttendanceDevice, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getDeviceId(selectedRow.value) === getDeviceId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: AttendanceDevice[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? selectedRowsData[0] : null
  }
}))

/**
 * 点击行切换选中。
 *
 * @param {AttendanceDevice} record - 行
 * @returns {{ onClick: () => void }}
 */
const onClickRow = (record: AttendanceDevice) => ({
  onClick: () => {
    const key = getDeviceId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getDeviceId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? selectedRows.value[0] : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/**
 * 拉取分页列表（`getAttendanceDeviceList`）。
 *
 * @returns {Promise<void>}
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: AttendanceDeviceQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.deviceCode?.trim()) params.deviceCode = advancedQueryForm.value.deviceCode.trim()
    if (advancedQueryForm.value.deviceName?.trim()) params.deviceName = advancedQueryForm.value.deviceName.trim()
    if (advancedQueryForm.value.manufacturer?.trim()) params.manufacturer = advancedQueryForm.value.manufacturer.trim()
    const advDs = coerceAdvancedDeviceStatus(advancedQueryForm.value.deviceStatus)
    if (advDs !== undefined) params.deviceStatus = advDs

    const response: TaktPagedResult<AttendanceDevice> = await getAttendanceDeviceList(
      params as unknown as Record<string, unknown>
    )
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[AttendanceDevice] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadFail'))
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
  advancedQueryForm.value = { deviceCode: '', deviceName: '', manufacturer: undefined, deviceStatus: undefined }
  currentPage.value = 1
  loadData()
}

/**
 * @param _pagination - 未使用
 * @param _filters - 未使用
 * @param {TableSorterLike} sorter - 排序
 */
const handleTableChange = (_pagination: unknown, _filters: unknown, sorter: TableSorterLike) => {
  if (sorter?.order) logger.debug('[AttendanceDevice] 排序:', sorter.field, sorter.order)
}

/**
 * @param {number} page - 页码
 * @param {number} size - 每页条数
 */
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/**
 * @param _current - 未使用
 * @param {number} size - 每页条数
 */
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/**
 * @param {number} w - 新宽度
 * @param {DeviceTableColumn} col - 列
 */
const handleResizeColumn = (w: number, col: DeviceTableColumn) => {
  const column = columns.value.find((c: DeviceTableColumn) => {
    const a = getDeviceColumnKey(col)
    const b = getDeviceColumnKey(c)
    return a && b && a === b
  })
  if (column) (column as DeviceTableColumn & { width?: number }).width = w
}

const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.attendancedevice._self')
  formData.value = {}
  formVisible.value = true
}

const handleEdit = (record: AttendanceDevice) => {
  formTitle.value = t('common.button.edit') + t('entity.attendancedevice._self')
  formData.value = { ...record }
  formVisible.value = true
}

const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else
    message.warning(
      t('common.action.warnSelectToAction', { action: t('common.button.edit'), entity: t('entity.attendancedevice._self') })
    )
}

const handleDeleteOne = (record: AttendanceDevice) => {
  const nameRaw = getDeviceField(record, 'deviceName')
  const name =
    (typeof nameRaw === 'string' && nameRaw.trim() !== '' ? nameRaw : null) ?? getDeviceId(record)
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteEntity', { entity: t('entity.attendancedevice._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteAttendanceDeviceById(getDeviceId(record))
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendancedevice._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendancedevice._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(
      t('common.action.warnSelectToAction', { action: t('common.button.delete'), entity: t('entity.attendancedevice._self') })
    )
    return
  }
  Modal.confirm({
    title: t('common.action.confirmDelete'),
    content: t('common.confirm.deleteCountEntity', {
      entity: t('entity.attendancedevice._self'),
      count: selectedRows.value.length
    }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          await deleteAttendanceDeviceById(getDeviceId(selectedRows.value[0]))
        } else {
          await deleteAttendanceDeviceBatch(selectedRows.value.map((r) => getDeviceId(r)))
        }
        message.success(t('common.msg.deleteSuccess', { target: t('entity.attendancedevice._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deleteFail', { target: t('entity.attendancedevice._self') }))
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
    const id = formData.value.deviceId
    if (id != null && String(id).length > 0) {
      const idStr = String(id)
      const payload: AttendanceDeviceUpdate = { ...(formValues as AttendanceDeviceCreate), deviceId: idStr }
      await updateAttendanceDevice(idStr, payload)
      message.success(t('common.msg.updateSuccess', { target: t('entity.attendancedevice._self') }))
    } else {
      await createAttendanceDevice(formValues as AttendanceDeviceCreate)
      message.success(t('common.msg.createSuccess', { target: t('entity.attendancedevice._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operateFail', { action: t('common.action.operation') }))
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

const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getAttendanceDeviceTemplate(sheetName, fileName)
}

const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importAttendanceDeviceData(file, sheetName)
}

const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

const handleImportCancel = () => {
  importVisible.value = false
}

const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: Record<string, unknown> = {}
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.deviceCode?.trim()) queryParams.DeviceCode = advancedQueryForm.value.deviceCode.trim()
    if (advancedQueryForm.value.deviceName?.trim()) queryParams.DeviceName = advancedQueryForm.value.deviceName.trim()
    const advDs = coerceAdvancedDeviceStatus(advancedQueryForm.value.deviceStatus)
    if (advDs !== undefined) queryParams.DeviceStatus = advDs

    const blob = await exportAttendanceDeviceData(
      queryParams,
      deviceExcelNames.sheet,
      deviceExcelNames.fileBase
    )
    const resBlob =
      typeof blob === 'object' && blob !== null && 'data' in blob && blob.data instanceof Blob
        ? blob.data
        : (blob)
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${deviceExcelNames.fileBase}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(resBlob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportSuccess', { target: t('entity.attendancedevice._self') }))
  } catch (error: unknown) {
    logger.error('[AttendanceDevice] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportFail', { target: t('entity.attendancedevice._self') }))
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
  advancedQueryForm.value = { deviceCode: '', deviceName: '', manufacturer: undefined, deviceStatus: undefined }
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

const handleRefresh = () => {
  loadData()
}

/** 供父级或调试 */
defineExpose({ tableRef })
</script>

<style scoped lang="less">
/* 与请假/加班列表页一致：内边距 + 纵向 flex，避免表格高度塌陷 */
.humanresource-attendance-leave-device {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>
