<!-- ======================================== -->
<!-- 项目名称：节拍数字工厂 · Takt Digital Factory (TDF) -->
<!-- 命名空间：@/views/human-resource/attendance-leave/holiday -->
<!-- 文件名称：index.vue -->
<!-- 功能描述：假日管理列表页。分页列表、关键字检索、高级查询（地区/假日名称）、新增与编辑弹窗、单条与批量删除、Excel 导入与导出、列显示设置、表格排序占位与列宽拖拽；假日类型、是否工作日列与用户视图一致使用 `TaktDictTag`（`hr_holiday_type` / `hr_holiday_is_working_day`）。权限字面值与 TaktHolidaysController 声明一致。 -->
<!-- 创建时间：2026-04-14 -->
<!-- 创建人：Takt365(Cursor AI) -->
<!-- 版权信息：Copyright (c) 2025 Takt  All rights reserved. -->
<!-- 免责声明：此软件使用 MIT License，作者不承担任何使用风险。 -->
<!-- ======================================== -->

<template>
  <div class="humanresource-attendance-leave-holiday">
    <!-- 顶部：关键字检索（entity.holiday.keyword） -->
    <TaktQueryBar
      v-model="queryKeyword"
      :placeholder="t('common.form.placeholder.search', { keyword: t('entity.holiday.keyword') })"
      :loading="loading"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏：RBAC 与 TaktHolidaysController 权限码一致 -->
    <TaktToolsBar
      create-permission="humanresource:attendanceleave:holiday:create"
      update-permission="humanresource:attendanceleave:holiday:update"
      delete-permission="humanresource:attendanceleave:holiday:delete"
      import-permission="humanresource:attendanceleave:holiday:import"
      template-permission="humanresource:attendanceleave:holiday:template"
      export-permission="humanresource:attendanceleave:holiday:export"
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

    <!-- 主表：holidayType / isWorkingDay 与用户页一致，bodyCell + TaktDictTag -->
    <TaktSingleTable
      ref="tableRef"
      :columns="displayColumns"
      :data-source="dataSource"
      :loading="loading"
      :stripe="true"
      :row-key="getHolidayId"
      :row-selection="rowSelection"
      :custom-row="onClickRow"
      :large-screen-column-count="9"
      :small-screen-column-count="5"
      @change="handleTableChange"
      @resize-column="handleResizeColumn"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'holidayType'">
          <TaktDictTag
            :value="getHolidayDictTagValue(record, 'holidayType')"
            dict-type="hr_holiday_type"
          />
        </template>
        <template v-else-if="column.key === 'isWorkingDay'">
          <TaktDictTag
            :value="getHolidayDictTagValue(record, 'isWorkingDay')"
            dict-type="hr_holiday_is_working_day"
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

    <!-- 新增 / 编辑：HolidayForm，宽度 50% -->
    <TaktModal
      v-model:open="formVisible"
      :title="formTitle"
      width="50%"
      wrap-class-name="takt-form-modal-resizable"
      :confirm-loading="formLoading"
      @ok="handleFormSubmit"
      @cancel="handleFormCancel"
    >
      <HolidayForm
        ref="formRef"
        :form-data="formData"
        :loading="formLoading"
      />
    </TaktModal>

    <!-- 高级查询：地区、假日名称并入列表查询参数 -->
    <TaktQueryDrawer
      v-model:open="advancedQueryVisible"
      :form-model="advancedQueryForm"
      @submit="handleAdvancedQuerySubmit"
      @reset="handleAdvancedQueryReset"
    >
      <a-form-item :label="t('entity.holiday.region')">
        <a-input v-model:value="advancedQueryForm.region" />
      </a-form-item>
      <a-form-item :label="t('entity.holiday.holidayname')">
        <a-input v-model:value="advancedQueryForm.holidayName" />
      </a-form-item>
    </TaktQueryDrawer>

    <!-- 导入：sheet 名与模板名由 i18n 模板拼接（与后端约定一致时请改为 taktExcelEntityNames） -->
    <TaktModal
      v-model:open="importVisible"
      :title="t('common.button.import') + t('entity.holiday._self')"
      :width="600"
      :footer="null"
      :cancel-text="t('common.button.close')"
      @cancel="handleImportCancel"
    >
      <TaktImportFile
        file-type="xlsx"
        :sheet-name="t('common.action.import.sheetnametemplate', { entity: t('entity.holiday._self') })"
        :template-file-name="t('common.action.import.sheetnametemplate', { entity: t('entity.holiday._self') })"
        template-permission="humanresource:attendanceleave:holiday:template"
        import-permission="humanresource:attendanceleave:holiday:import"
        :download-template="handleDownloadTemplate"
        :import-file="handleImportFile"
        :template-text="t('common.action.import.templatetext', { entity: t('entity.holiday._self') })"
        :upload-text="t('common.action.import.uploadtext')"
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
import type { FilterValue } from 'ant-design-vue/es/table/interface'
import { CreateActionColumn } from '@/components/business/takt-action-column/index'
import { mergeDefaultColumns } from '@/utils/table-columns'
import { useI18n } from 'vue-i18n'
import HolidayForm from './components/holiday-form.vue'
import {
  getHolidayList,
  createHoliday,
  updateHoliday,
  deleteHolidayById,
  deleteHolidayBatch,
  getHolidayTemplate,
  importHolidayData,
  exportHolidayData
} from '@/api/human-resource/attendance-leave/holiday'
import type { Holiday, HolidayCreate, HolidayQuery, HolidayUpdate } from '@/types/human-resource/attendance-leave/holiday'
import type { TaktPagedResult } from '@/types/common'
import { logger } from '@/utils/logger'
import { RiEditLine, RiDeleteBinLine } from '@remixicon/vue'

/**
 * 国际化文案函数（`vue-i18n`）。
 */
const { t } = useI18n()

/**
 * 表格列单项类型（列宽、列设置键）。
 */
type HolidayTableColumn = TableColumnsType[number]

/** 与 `TaktSingleTable` 的 `@change` 事件参数一致（见 `takt-single-table/index.vue`）。 */
type TaktTableChangeSorter = {
  field?: string | number | readonly (string | number)[]
  order?: string
}
type TaktTableChangeFilters = Record<string, FilterValue | null>
type TaktTableChangePagination = { current?: number; pageSize?: number; total?: number }

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
 * @param {HolidayTableColumn} col - 列定义
 * @returns {string}
 */
function getHolidayColumnKey(col: HolidayTableColumn): string {
  const c = col as { key?: unknown; dataIndex?: unknown; title?: unknown }
  const key = c.key
  const dataIndex = c.dataIndex
  const title = c.title
  return key || dataIndex || title ? String(key ?? dataIndex ?? title) : ''
}

/** 顶部查询关键字，对应 `HolidayQuery.KeyWords`（与 `TaktPagedQuery` 一致）。 */
const queryKeyword = ref('')

/** 异步操作 loading。 */
const loading = ref(false)

/** 当前页表格数据。 */
const dataSource = ref<Holiday[]>([])

/** 当前页码。 */
const currentPage = ref(1)

/** 每页条数。 */
const pageSize = ref(20)

/** 总记录数。 */
const total = ref(0)

/** 单行选中语义下的当前行。 */
const selectedRow = ref<Holiday | null>(null)

/** 多选行集合。 */
const selectedRows = ref<Holiday[]>([])

/** 多选 `row-key` 列表。 */
const selectedRowKeys = ref<(string | number)[]>([])

/** 表单弹窗是否打开。 */
const formVisible = ref(false)

/** 弹窗标题。 */
const formTitle = ref('')

/** 表单初始数据。 */
const formData = ref<Partial<Holiday>>({})

/** 表单提交中。 */
const formLoading = ref(false)

/**
 * `HolidayForm` 暴露实例类型。
 */
type HolidayFormExposed = InstanceType<typeof HolidayForm>

/** 假日表单 ref。 */
const formRef = ref<HolidayFormExposed | null>(null)

/** 表格 ref（`defineExpose`）。 */
const tableRef = ref()

/** 高级查询抽屉。 */
const advancedQueryVisible = ref(false)

/**
 * 高级查询：地区、假日名称（并入 `HolidayQuery.region` / `holidayName`）。
 */
const advancedQueryForm = ref<{ region: string; holidayName: string }>({
  region: '',
  holidayName: ''
})

/** 导入弹窗。 */
const importVisible = ref(false)

/** 列设置抽屉。 */
const columnSettingVisible = ref(false)

/** 可见列键；空为全部。 */
const visibleColumnKeys = ref<string[]>([])

/** 挂载后加载第一页。 */
onMounted(() => {
  loadData()
})

/**
 * 行主键字符串（`row-key`、删除）。入参与 `TaktSingleTable` 的 `rowKey` 一致，为 `TableRecord`/`unknown`。
 *
 * @param {unknown} record - 列表行
 * @returns {string}
 */
const getHolidayId = (record: unknown): string => {
  if (record == null || typeof record !== 'object') return ''
  const r = record as Record<string, unknown>
  const holidayId = r['holidayId']
  if (holidayId != null && String(holidayId) !== '') return String(holidayId)
  const legacyId = r['id']
  if (legacyId != null && String(legacyId) !== '') return String(legacyId)
  return ''
}

/**
 * 读取行字段（供 `#bodyCell` 等使用）。
 *
 * @param {unknown} record - 列表行
 * @param {string} field - 字段名
 * @returns {unknown}
 */
const getHolidayField = (record: unknown, field: string): unknown =>
  record != null && typeof record === 'object' ? (record as Record<string, unknown>)[field] : undefined

/**
 * 供 `TaktDictTag` 的 `:value`（`string | number`）。
 *
 * @param {unknown} record - 列表行
 * @param {string} field - 字段名
 * @returns {string | number}
 */
function getHolidayDictTagValue(record: unknown, field: string): string | number {
  const v = getHolidayField(record, field)
  if (typeof v === 'number' && Number.isFinite(v)) return v
  if (typeof v === 'string') return v
  return String(v ?? '')
}

/**
 * 列表主列定义。
 */
const columns = computed<TableColumnsType>(() => [
    {
      title: 'ID',
      dataIndex: 'holidayId',
      key: 'id',
      width: 80,
      resizable: true,
      ellipsis: true,
      fixed: 'left',
      customRender: ({ record }: { record: Holiday }) =>
        getHolidayField(record, 'holidayId') ?? getHolidayField(record, 'id') ?? ''
    },
    {
      title: t('entity.holiday.region'),
      dataIndex: 'region',
      key: 'region',
      width: 100,
      resizable: true,
      ellipsis: true
    },
    {
      title: t('entity.holiday.holidayname'),
      dataIndex: 'holidayName',
      key: 'holidayName',
      width: 140,
      resizable: true,
      ellipsis: true
    },
    {
      title: t('entity.holiday.holidaytype'),
      dataIndex: 'holidayType',
      key: 'holidayType',
      width: 90
    },
    {
      title: t('entity.holiday.startdate'),
      dataIndex: 'startDate',
      key: 'startDate',
      width: 120,
      ellipsis: true
    },
    {
      title: t('entity.holiday.enddate'),
      dataIndex: 'endDate',
      key: 'endDate',
      width: 120,
      ellipsis: true
    },
    {
      title: t('entity.holiday.isworkingday'),
      dataIndex: 'isWorkingDay',
      key: 'isWorkingDay',
      width: 100
    },
    {
      title: t('entity.holiday.holidaygreeting'),
      dataIndex: 'holidayGreeting',
      key: 'holidayGreeting',
      width: 160,
      ellipsis: true
    },
    {
      title: t('entity.holiday.holidayquote'),
      dataIndex: 'holidayQuote',
      key: 'holidayQuote',
      width: 200,
      ellipsis: true
    },
    {
      title: t('entity.holiday.holidaytheme'),
      dataIndex: 'holidayTheme',
      key: 'holidayTheme',
      width: 100
    },
    CreateActionColumn<Holiday>({
      actions: [
        {
          key: 'update',
          label: t('common.button.edit'),
          shape: 'plain',
          icon: RiEditLine,
          permission: 'humanresource:attendanceleave:holiday:update',
          onClick: (record: Holiday) => handleEdit(record)
        },
        {
          key: 'delete',
          label: t('common.button.delete'),
          shape: 'plain',
          icon: RiDeleteBinLine,
          permission: 'humanresource:attendanceleave:holiday:delete',
          onClick: (record: Holiday) => handleDeleteOne(record)
        }
      ]
    })
  ])

/**
 * 合并默认列后的列配置。
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
  return merged.filter((col: HolidayTableColumn) => {
    const colKey = getHolidayColumnKey(col)
    return colKey && keysSet.has(colKey)
  })
})

/**
 * 行选择配置。
 */
const rowSelection = computed(() => ({
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: (string | number)[], rows: Holiday[]) => {
    selectedRowKeys.value = keys
    selectedRows.value = rows
    selectedRow.value = rows.length === 1 ? (rows[0] ?? null) : null
  },
  onSelect: (record: Holiday, selected: boolean) => {
    if (selected) selectedRow.value = record
    else if (selectedRow.value && getHolidayId(selectedRow.value) === getHolidayId(record)) selectedRow.value = null
  },
  onSelectAll: (selected: boolean, selectedRowsData: Holiday[]) => {
    selectedRow.value = selected && selectedRowsData.length === 1 ? (selectedRowsData[0] ?? null) : null
  }
}))

/**
 * 点击行切换选中。
 *
 * @param {Holiday} record - 被点击行
 * @returns {{ onClick: () => void }}
 */
const onClickRow = (record: Holiday) => ({
  onClick: () => {
    const key = getHolidayId(record)
    const index = selectedRowKeys.value.indexOf(key)
    if (index > -1) selectedRowKeys.value.splice(index, 1)
    else selectedRowKeys.value.push(key)
    selectedRows.value = dataSource.value.filter((item) => selectedRowKeys.value.includes(getHolidayId(item)))
    selectedRow.value = selectedRowKeys.value.length === 1 ? (selectedRows.value[0] ?? null) : null
    if (rowSelection.value.onChange) rowSelection.value.onChange(selectedRowKeys.value, selectedRows.value)
  }
})

/**
 * 拉取假日分页列表。
 *
 * @returns {Promise<void>}
 */
const loadData = async () => {
  try {
    loading.value = true
    const params: HolidayQuery = {
      pageIndex: currentPage.value,
      pageSize: pageSize.value
    }
    if (queryKeyword.value) params.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.region) params.region = advancedQueryForm.value.region
    if (advancedQueryForm.value.holidayName) params.holidayName = advancedQueryForm.value.holidayName

    const response: TaktPagedResult<Holiday> = await getHolidayList(params)
    dataSource.value = response.data ?? []
    total.value = response.total ?? 0
  } catch (error: unknown) {
    logger.error('[Holiday] 加载数据失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.loadfail'))
    dataSource.value = []
    total.value = 0
  } finally {
    loading.value = false
  }
}

/**
 * 搜索：第一页并加载。
 */
const handleSearch = () => {
  currentPage.value = 1
  loadData()
}

/**
 * 重置查询条件与关键字，第一页加载。
 */
const handleReset = () => {
  queryKeyword.value = ''
  advancedQueryForm.value = { region: '', holidayName: '' }
  currentPage.value = 1
  loadData()
}

/**
 * 表格 `change`：当前仅记录排序调试信息。
 *
 * @param _pagination - 未使用
 * @param _filters - 未使用
 * @param {TableSorterLike} sorter - 排序状态
 */
const handleTableChange = (
  _pagination: TaktTableChangePagination,
  _filters: TaktTableChangeFilters,
  sorter: TaktTableChangeSorter | TaktTableChangeSorter[]
) => {
  const one = Array.isArray(sorter) ? sorter[0] : sorter
  if (one?.order) logger.debug('[Holiday] 排序:', one.field, one.order)
}

/**
 * 分页变化。
 *
 * @param {number} page - 页码
 * @param {number} size - 每页条数
 */
const handlePaginationChange = (page: number, size: number) => {
  currentPage.value = page
  pageSize.value = size
  loadData()
}

/**
 * 每页条数变化：回到第一页。
 *
 * @param _current - 当前页（未使用）
 * @param {number} size - 新每页条数
 */
const handlePaginationSizeChange = (_current: number, size: number) => {
  currentPage.value = 1
  pageSize.value = size
  loadData()
}

/**
 * 列宽拖拽结束，写回 `columns` 对应列。
 *
 * @param {number} w - 新列宽（px）
 * @param {HolidayTableColumn} col - 当前列
 */
const handleResizeColumn = (w: number, col: HolidayTableColumn) => {
  const column = columns.value.find((c: HolidayTableColumn) => {
    const colKey = getHolidayColumnKey(col)
    const cKey = getHolidayColumnKey(c)
    return colKey && cKey && colKey === cKey
  })
  if (column) (column as HolidayTableColumn & { width?: number }).width = w
}

/**
 * 新建假日。
 */
const handleCreate = () => {
  formTitle.value = t('common.button.create') + t('entity.holiday._self')
  formData.value = {}
  formVisible.value = true
}

/**
 * 编辑指定假日行。
 *
 * @param {Holiday} record - 行数据
 */
const handleEdit = (record: Holiday) => {
  formTitle.value = t('common.button.edit') + t('entity.holiday._self')
  formData.value = { ...record }
  formVisible.value = true
}

/**
 * 工具栏编辑：需先选中单行。
 */
const handleUpdate = () => {
  if (selectedRow.value) handleEdit(selectedRow.value)
  else message.warning(t('common.action.warnselecttoaction', { action: t('common.button.edit'), entity: t('entity.holiday._self') }))
}

/**
 * 删除单条假日。
 *
 * @param {Holiday} record - 待删除行
 */
const handleDeleteOne = (record: Holiday) => {
  const hn = getHolidayField(record, 'holidayName')
  const name =
    (typeof hn === 'string' && hn.trim() !== '' ? hn : null) ??
    (typeof hn === 'number' ? String(hn) : null) ??
    getHolidayId(record)
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deleteentity', { entity: t('entity.holiday._self'), name }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        await deleteHolidayById(getHolidayId(record))
        message.success(t('common.msg.deletesuccess', { target: t('entity.holiday._self') }))
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: t('entity.holiday._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 批量删除假日。
 */
const handleDelete = () => {
  if (selectedRows.value.length === 0) {
    message.warning(t('common.action.warnselecttoaction', { action: t('common.button.delete'), entity: t('entity.holiday._self') }))
    return
  }
  Modal.confirm({
    title: t('common.action.confirmdelete'),
    content: t('common.confirm.deletecountentity', { entity: t('entity.holiday._self'), count: selectedRows.value.length }),
    okText: t('common.button.delete'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        loading.value = true
        if (selectedRows.value.length === 1) {
          const first = selectedRows.value[0]
          if (first) await deleteHolidayById(getHolidayId(first))
        } else {
          await deleteHolidayBatch(selectedRows.value.map((r) => getHolidayId(r)))
        }
        message.success(t('common.msg.deletesuccess', { target: t('entity.holiday._self') }))
        selectedRows.value = []
        selectedRowKeys.value = []
        selectedRow.value = null
        loadData()
      } catch (error: unknown) {
        message.error(getErrorMessage(error) || t('common.msg.deletefail', { target: t('entity.holiday._self') }))
      } finally {
        loading.value = false
      }
    }
  })
}

/**
 * 表单提交：`createHoliday` / `updateHoliday`；校验错误带 `errorFields` 时不提示通用失败。
 *
 * @returns {Promise<void>}
 */
const handleFormSubmit = async () => {
  try {
    if (!formRef.value) return
    await formRef.value.validate()
    const formValues = formRef.value.getValues()
    formLoading.value = true
    if (formData.value?.holidayId) {
      const id = formData.value.holidayId
      const payload: HolidayUpdate = { ...(formValues as HolidayCreate), holidayId: id }
      await updateHoliday(id, payload)
      message.success(t('common.msg.updatesuccess', { target: t('entity.holiday._self') }))
    } else {
      await createHoliday(formValues as HolidayCreate)
      message.success(t('common.msg.createsuccess', { target: t('entity.holiday._self') }))
    }
    formRef.value?.resetFields()
    formData.value = {}
    formVisible.value = false
    loadData()
  } catch (error: unknown) {
    if (typeof error === 'object' && error !== null && 'errorFields' in error) return
    message.error(getErrorMessage(error) || t('common.msg.operatefail', { action: t('common.action.operation') }))
  } finally {
    formLoading.value = false
  }
}

/**
 * 取消编辑：关弹窗并重置表单。
 */
const handleFormCancel = () => {
  formVisible.value = false
  formData.value = {}
  formRef.value?.resetFields()
}

/**
 * 打开导入弹窗。
 */
const handleImport = () => {
  importVisible.value = true
}

/**
 * 下载假日导入模板。
 *
 * @param {string} [sheetName] - 工作表名
 * @param {string} [fileName] - 文件名
 * @returns {Promise<import('@/api/request').BlobDownloadWithMeta>}
 */
const handleDownloadTemplate = async (sheetName?: string, fileName?: string) => {
  return await getHolidayTemplate(sheetName, fileName)
}

/**
 * 上传并导入假日 Excel。
 *
 * @param {File} file - 文件
 * @param {string} [sheetName] - 工作表名
 * @returns {Promise<{ success: number; fail: number; errors: string[] }>}
 */
const handleImportFile = async (
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> => {
  return await importHolidayData(file, sheetName)
}

/**
 * 导入成功回调。
 *
 * @param {{ success: number; fail: number; errors: string[] }} result - 统计结果
 */
const handleImportSuccess = (result: { success: number; fail: number; errors: string[] }) => {
  loadData()
  if (result.fail === 0) setTimeout(() => { importVisible.value = false }, 2000)
}

/**
 * 关闭导入弹窗。
 */
const handleImportCancel = () => {
  importVisible.value = false
}

/**
 * 导出假日：`HolidayQuery` 含大 `pageSize` 以满足类型；后端导出按 `QueryExpression` 过滤，与分页字段无耦合（见 `TaktHolidayService.ExportHolidayAsync`）。
 *
 * @returns {Promise<void>}
 */
const handleExport = async () => {
  try {
    loading.value = true
    const queryParams: HolidayQuery = {
      pageIndex: 1,
      pageSize: 100000
    }
    if (queryKeyword.value) queryParams.KeyWords = queryKeyword.value
    if (advancedQueryForm.value.region) queryParams.region = advancedQueryForm.value.region
    if (advancedQueryForm.value.holidayName) queryParams.holidayName = advancedQueryForm.value.holidayName

    const blob = await exportHolidayData(
      queryParams,
      undefined,
      t('entity.holiday._self') + t('common.action.exportdatasuffix')
    )
    const ts = new Date()
    const pad = (n: number, w = 2) => String(n).padStart(w, '0')
    const fileName = `${t('entity.holiday._self') + t('common.action.exportdatasuffix')}_${ts.getFullYear()}${pad(ts.getMonth() + 1)}${pad(ts.getDate())}${pad(ts.getHours())}${pad(ts.getMinutes())}${pad(ts.getSeconds())}.xlsx`
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    link.style.display = 'none'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    setTimeout(() => window.URL.revokeObjectURL(url), 100)
    message.success(t('common.msg.exportsuccess', { target: t('entity.holiday._self') }))
  } catch (error: unknown) {
    logger.error('[Holiday] 导出失败:', error)
    message.error(getErrorMessage(error) || t('common.msg.exportfail', { target: t('entity.holiday._self') }))
  } finally {
    loading.value = false
  }
}

/**
 * 打开高级查询抽屉。
 */
const handleAdvancedQuery = () => {
  advancedQueryVisible.value = true
}

/**
 * 高级查询确定：第一页加载并关闭抽屉。
 */
const handleAdvancedQuerySubmit = () => {
  currentPage.value = 1
  loadData()
  advancedQueryVisible.value = false
}

/**
 * 重置高级查询表单。
 */
const handleAdvancedQueryReset = () => {
  advancedQueryForm.value = { region: '', holidayName: '' }
}

/**
 * 打开列设置。
 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

/**
 * 列勾选变更。
 *
 * @param {(string | number)[]} keys - 列键
 */
const handleColumnKeysChange = (keys: (string | number)[]) => {
  visibleColumnKeys.value = keys.map((k) => String(k))
}

/**
 * 重置列设置为全列展示。
 */
const handleColumnSettingReset = () => {
  visibleColumnKeys.value = []
}

/**
 * 刷新列表数据。
 */
const handleRefresh = () => {
  loadData()
}

/** 供父级或调试使用的表格 ref */
defineExpose({ tableRef })
</script>

<style scoped lang="less">
/* 根容器：内边距 + 纵向 flex，避免表格高度塌陷 */
.humanresource-attendance-leave-holiday {
  padding: 16px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}
</style>